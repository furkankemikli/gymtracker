using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GymTracker.Models;
using Microsoft.AspNetCore.Authorization;
using GymTracker.Models.Repositories;
using System.Security.Claims;
using Newtonsoft.Json;
using GymTracker.Models.TrainerViewModels;
using System.Globalization;
using GymTracker.Models.TraineeViewModels;
using GymTracker.Services;
using Microsoft.AspNetCore.Identity;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace GymTracker.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IEventRepository _eventRepository;
        private readonly IExerciseRepository _exerciseRepository;
        private readonly ITraineeGoalsRepository _traineeGoalsRepository;
        private readonly ITraineeRepository _traineeRepository;
        private readonly IDailyProgressRepository _dailyProgressRepository;
        private readonly IDailyRoutineRepository _dailyRoutineRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailSender _emailSender;
        private readonly IHostingEnvironment _hostingEnvironment;


        public HomeController(
            IEventRepository eventRepository,
            IExerciseRepository exerciseRepository,
            ITraineeGoalsRepository traineeGoalsRepository,
            ITraineeRepository traineeRepository,
            IDailyProgressRepository dailyProgressRepository,
            IDailyRoutineRepository dailyRoutineRepository,
            UserManager<ApplicationUser> userManager,
            IEmailSender emailSender,
            IHostingEnvironment hostingEnvironment)
        {
            _eventRepository = eventRepository;
            _exerciseRepository = exerciseRepository;
            _traineeGoalsRepository = traineeGoalsRepository;
            _traineeRepository = traineeRepository;
            _dailyProgressRepository = dailyProgressRepository;
            _dailyRoutineRepository = dailyRoutineRepository;
            _emailSender = emailSender;
            _userManager = userManager;
            _hostingEnvironment = hostingEnvironment;
        }

        public IActionResult Index()
        {
            string userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            List<Event> myList = _eventRepository.Events(userId);
            var json = JsonConvert.SerializeObject(myList, Formatting.None,
                        new JsonSerializerSettings()
                        {
                            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                        });
            HomeIndexViewModel homeIndexViewModel = new HomeIndexViewModel
            {
                Events = myList,
                jsonEvents = json
            };

            return View(homeIndexViewModel);
        }

        [HttpPost]
        public IActionResult AddNewEvent(HomeIndexViewModel model)
        {
            try
            {
                //tries to convert string date into datetime object if it is in valid format => dd.MM.yyyy hh:mm tt
                model.StartDate = DateTime.ParseExact(model.StrStartDate, "dd.MM.yyyy hh:mm tt", CultureInfo.InvariantCulture);
                model.EndDate = DateTime.ParseExact(model.StrEndDate, "dd.MM.yyyy hh:mm tt", CultureInfo.InvariantCulture);
            }
            catch (System.FormatException)
            {
                //date format is wrong or date is wrong
            }
            if (model.EndDate > model.StartDate)
            {
                Event newEvent = new Event
                {
                    Name = model.Name,
                    Description = model.Description,
                    Location = model.Location,
                    StartDate = model.StartDate,
                    EndDate = model.EndDate,
                    TrainerId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value
                };
                _eventRepository.CreateEvent(newEvent);
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public IActionResult UpdateEvent(HomeIndexViewModel model)
        {
            try
            {
                //tries to convert string date into datetime object if it is in valid format => dd.MM.yyyy hh:mm tt
                model.StartDate = DateTime.ParseExact(model.StrStartDate, "dd.MM.yyyy hh:mm tt", CultureInfo.InvariantCulture);
                model.EndDate = DateTime.ParseExact(model.StrEndDate, "dd.MM.yyyy hh:mm tt", CultureInfo.InvariantCulture);
            }
            catch (System.FormatException)
            {
                //date format is wrong or date is wrong
            }
            if (model.EndDate > model.StartDate)
            {
                Event newEvent = new Event
                {
                    EventId = model.EventId,
                    Name = model.Name,
                    Description = model.Description,
                    Location = model.Location,
                    StartDate = model.StartDate,
                    EndDate = model.EndDate,
                    TrainerId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value
                };
                _eventRepository.UpdateEvent(newEvent);
            }
            return RedirectToAction("Index", "Home");
        }
        
        public IActionResult DeleteEvent(int eventId)
        {
            _eventRepository.DeleteEvent(eventId);
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Exercises()
        {
            ExercisesViewModel homeIndexViewModel = new ExercisesViewModel
            {
                Exercises = _exerciseRepository.Exercises
            };
            return View(homeIndexViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddNewExercise(ExercisesViewModel model)
        {
            Exercise exercise = new Exercise
            {
                Name = model.Name,
                CalorieBySet = model.CalorieBySet,
                //GifPicture = model.GifPicture,
                Category = model.Category
            };
            if (model.GifPicture != null && model.GifPicture.Length > 0)
            {
                var guid = Guid.NewGuid().ToString();
                exercise.GifPicture = "Uploads/" + guid + Path.GetExtension(model.GifPicture.FileName);
                var userPath = Path.Combine(_hostingEnvironment.ContentRootPath, "Uploads", guid + Path.GetExtension(model.GifPicture.FileName));

                using (var stream = new FileStream(userPath, FileMode.Create))
                {
                    await model.GifPicture.CopyToAsync(stream);
                }
            }
            _exerciseRepository.CreateExercise(exercise);
            return RedirectToAction("Exercises", "Home");
        }

        public IActionResult Trainees()
        {
            string userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            TraineesViewModel traineesViewModel = new TraineesViewModel
            {
                Trainees = _traineeRepository.GetTrainees(userId)
            };

            return View(traineesViewModel);
        }
        
        //to open new trainee page
        public IActionResult NewTrainee()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> NewTrainee(NewTraineeViewModel model)
        {
            if (ModelState.IsValid)
            {
                var trainer = await _userManager.GetUserAsync(User);
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email,
                    Name = model.Name, Surname = model.Surname,
                    PhoneNumber = model.Phone, City = model.City,
                    GymId = trainer.GymId
                };

                if (model.Image != null && model.Image.Length > 0)
                {
                    var guid = Guid.NewGuid().ToString();
                    user.Picture = "Uploads/" + guid + Path.GetExtension(model.Image.FileName);
                    var userPath = Path.Combine(_hostingEnvironment.ContentRootPath, "Uploads", guid + Path.GetExtension(model.Image.FileName));
                    
                    using (var stream = new FileStream(userPath, FileMode.Create))
                    {
                        await model.Image.CopyToAsync(stream);
                    }
                }
                #warning "Remove after adding password field."
                model.Password = "hello";

                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var callbackUrl = Url.EmailConfirmationLink(user.Id, code, Request.Scheme);
                    await _emailSender.SendEmailConfirmationAsync(model.Email, callbackUrl);
                    Trainee trainee = new Trainee
                    {
                        Birthday = model.DateOfBirth,
                        EntryDate = DateTime.Today,
                        FatRatio = model.FatRatio,
                        Gender = model.Gender,
                        Height = model.Height,
                        TrainerId = trainer.Id,
                        TraineeId = _traineeRepository.GetUserId(model.Email, model.Name, model.Surname),
                        Weight = model.Weight

                    };
                    _traineeRepository.CreateTrainee(trainee);
                    return RedirectToAction("Trainees", "Home");
                }
            }
            return View(model);
        }

        //to open edit trainee page
        public IActionResult EditTrainee()
        {
            return View();
        }

        public IActionResult UpdateTraineeInfo(EditTraineeViewModel model)
        {
            TraineeInfoModel trainee = new TraineeInfoModel
            {
                Name = model.Name,
                Surname = model.Surname,
                Email = model.Email,
                Picture = model.Image,
                City = model.City,
                PhoneNumber = model.Phone,                
                Birthday = model.DateOfBirth,
                FatRatio = model.FatRatio,
                Gender = model.Gender,
                Height = model.Height,
                TraineeId = model.TraineeId,
                Weight = model.Weight
            };
            _traineeRepository.UpdateTrainee(trainee);
            return RedirectToAction("Trainees", "Home");
        }

        public IActionResult DeleteTrainee(string traineeId)
        {
            _traineeRepository.DeleteTrainee(traineeId);
            return RedirectToAction("Trainees", "Home");
        }

        public IActionResult TraineeDetails(string Id)
        {
            TraineeInfoModel personalInfo = _traineeRepository.GetTraineeById(Id);
            TraineeGoals goals = _traineeGoalsRepository.GetTraineeGoals(Id);
            TraineeDetailsPageViewModel model = new TraineeDetailsPageViewModel
            {
                Name = personalInfo.Name,
                Surname = personalInfo.Surname,
                Email = personalInfo.Email,
                DateOfBirth = (DateTime)personalInfo.Birthday,
                Image = personalInfo.Picture,
                Phone = personalInfo.PhoneNumber,
                Weight = (double)personalInfo.Weight,
                Height = (double)personalInfo.Height,
                FatRatio = (double)personalInfo.FatRatio,
                City = personalInfo.City,
                Gender = personalInfo.Gender,
                EntryDate = personalInfo.EntryDate,
                DailyProgresses = _dailyProgressRepository.DailyProgresses(Id),
                DailyRoutines = _dailyRoutineRepository.DailyRoutines(Id),
                Exercises = _exerciseRepository.Exercises,
                GoalWeight = (double)goals.Weight,
                GoalFatRatio = (double)goals.FatRatio,
                GoalDate = (DateTime)goals.ByDate                
            };
            return View(model);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
