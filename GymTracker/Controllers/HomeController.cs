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
using GymTracker.Models.RepositoryInterfaces;

namespace GymTracker.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IEventRepository _eventRepository;
        private readonly IExerciseRepository _exerciseRepository;
        private readonly ITraineeRepository _traineeRepository;
        private readonly IDailyProgressRepository _dailyProgressRepository;
        private readonly IDailyRoutineRepository _dailyRoutineRepository;
        private readonly ITraineeMeasurementsRepository _traineeMeasurementsRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailSender _emailSender;
        private readonly IHostingEnvironment _hostingEnvironment;


        public HomeController(
            IEventRepository eventRepository,
            IExerciseRepository exerciseRepository,
            ITraineeRepository traineeRepository,
            IDailyProgressRepository dailyProgressRepository,
            IDailyRoutineRepository dailyRoutineRepository,
            ITraineeMeasurementsRepository traineeMeasurementsRepository,
            UserManager<ApplicationUser> userManager,
            IEmailSender emailSender,
            IHostingEnvironment hostingEnvironment)
        {
            _eventRepository = eventRepository;
            _exerciseRepository = exerciseRepository;
            _traineeRepository = traineeRepository;
            _dailyProgressRepository = dailyProgressRepository;
            _dailyRoutineRepository = dailyRoutineRepository;
            _traineeMeasurementsRepository = traineeMeasurementsRepository;
            _emailSender = emailSender;
            _userManager = userManager;
            _hostingEnvironment = hostingEnvironment;
            _userManager.PasswordHasher = new CustomPasswordHasher<ApplicationUser>();
        }

        public IActionResult Index()
        {
            string userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            List<Event> myList = _eventRepository.Events(userId);
            List<TraineeInfoModel> trainees = _traineeRepository.GetTrainees(userId).ToList();
            var json = JsonConvert.SerializeObject(myList, Formatting.None,
                        new JsonSerializerSettings()
                        {
                            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                        });

            List<TraineeInviteModel> inviteTraineeList = new List<TraineeInviteModel>();
            for (int i = 0; i < trainees.Count; i++)
            {
                inviteTraineeList.Add(new TraineeInviteModel(trainees[i].TraineeId, trainees[i].Name, trainees[i].Surname, trainees[i].Email, false));
            }
            List<Event> invitedEventList = new List<Event>();
            for(int i = 0; i < myList.Count; i++)
            {
                invitedEventList.AddRange(_eventRepository.GetInvitedTraineeEvent(myList[i].EventId));
            }
            var inviteJson = JsonConvert.SerializeObject(invitedEventList, Formatting.None,
                        new JsonSerializerSettings()
                        {
                            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                        });
            HomeIndexViewModel homeIndexViewModel = new HomeIndexViewModel
            {
                Events = myList,
                jsonEvents = json,
                TraineeList = inviteTraineeList,
                InviteEventList = inviteJson
            };

            return View(homeIndexViewModel);
        }

        public async Task<ApplicationUser> GetUserInfo()
        {
            var trainer = await _userManager.GetUserAsync(User);
            return new ApplicationUser
            {
                Name = trainer.Name,
                Surname = trainer.Surname,
                Image = trainer.Image
            };
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
                    UserId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value
                };
                int holderEventId = _eventRepository.CreateEvent(newEvent);
                for (int i = 0; i < model.TraineeList.Count(); i++)
                {
                    if (model.TraineeList[i].IsChecked)
                    {
                        // create event invitation for trainee with waiting status
                        Event eventInvite = new Event
                        {
                            Name = model.Name,
                            Description = model.Description,
                            Location = model.Location,
                            StartDate = model.StartDate,
                            EndDate = model.EndDate,
                            ApporavalStatus = "waiting",
                            HolderEventId = holderEventId,
                            UserId = model.TraineeList[i].TraineeId
                        };
                        _eventRepository.CreateEvent(eventInvite);
                    }
                }
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public IActionResult UpdateEvent(HomeIndexViewModel model)
        {
            int count = 0;
            for(int i=0; i< model.TraineeList.Count(); i++)
            {
                if (model.TraineeList[i].IsChecked)
                {
                    count += 1;
                }
            }
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
                    UserId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value
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
                using (var memoryStream = new MemoryStream())
                {
                    await model.GifPicture.CopyToAsync(memoryStream);
                    exercise.GifPicture = memoryStream.ToArray();
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
            model.DateOfBirth = DateTime.ParseExact(model.StrDateOfBirth, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            var trainer = await _userManager.GetUserAsync(User);
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email,
                    Name = model.Name, Surname = model.Surname,
                    PhoneNumber = model.Phone, City = model.City,
                    GymId = trainer.GymId
                };

                if (model.Image != null && model.Image.Length > 0)
                {       
                    using (var memoryStream = new MemoryStream())
                    {
                        await model.Image.CopyToAsync(memoryStream);
                        user.Image = memoryStream.ToArray();
                    }
            }

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
                        Gender = model.Gender,
                        TrainerId = trainer.Id,
                        TraineeId = _traineeRepository.GetUserId(model.Email, model.Name, model.Surname)                        
                    };
                    _traineeRepository.CreateTrainee(trainee);
                    TraineeMeasurements traineeMeasurements = new TraineeMeasurements
                    {
                        Weight = model.Weight,
                        Height = model.Height,
                        FatRatio = model.FatRatio,
                        Date = DateTime.Today,
                        TraineeId = _traineeRepository.GetUserId(model.Email, model.Name, model.Surname)
                    };
                    _traineeMeasurementsRepository.CreateTraineeMeasurements(traineeMeasurements);
                    return RedirectToAction("Trainees", "Home");
                }
            return View(model);
        }

        //to open edit trainee page
        public IActionResult EditTrainee(string Id)
        {
            TraineeInfoModel model = _traineeRepository.GetTraineeById(Id);
            EditTraineeViewModel trainee = new EditTraineeViewModel
            {
                Name = model.Name,
                Surname = model.Surname,
                Email = model.Email,
                Phone = model.PhoneNumber,
                City = model.City,
                DateOfBirth = (DateTime)model.Birthday,
                Gender = model.Gender,
                CurrentImage = model.Image,
                TraineeId = model.TraineeId
            };
            return View(trainee);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateTraineeInfo(EditTraineeViewModel model)
        {
            model.DateOfBirth = DateTime.ParseExact(model.StrDateOfBirth, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            TraineeInfoModel trainee = new TraineeInfoModel
            {
                Name = model.Name,
                Surname = model.Surname,
                Email = model.Email,
                City = model.City,
                PhoneNumber = model.Phone,                
                Birthday = model.DateOfBirth,
                Gender = model.Gender,
                TraineeId = model.TraineeId
            };

            if (model.Image != null && model.Image.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await model.Image.CopyToAsync(memoryStream);
                    trainee.Image = memoryStream.ToArray();
                }
            }
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
            TraineeDetailsPageViewModel model = new TraineeDetailsPageViewModel
            {
                Id = personalInfo.TraineeId,
                Name = personalInfo.Name,
                Surname = personalInfo.Surname,
                Email = personalInfo.Email,
                DateOfBirth = (DateTime)personalInfo.Birthday,
                Image = personalInfo.Image,
                Phone = personalInfo.PhoneNumber,
                Weight = (personalInfo.Weight != null ? (double)personalInfo.Weight : double.NaN),
                Height = (personalInfo.Height != null ? (double)personalInfo.Height : double.NaN),
                FatRatio = personalInfo.FatRatio,
                City = personalInfo.City,
                Gender = personalInfo.Gender,
                EntryDate = personalInfo.EntryDate,
                DailyProgresses = _dailyProgressRepository.DailyProgresses(Id),
                DailyRoutines = _dailyRoutineRepository.DailyRoutines(Id),
                Exercises = _exerciseRepository.Exercises,
                Measurements = _traineeMeasurementsRepository.GetTraineeMeasurements(Id)
            };
            return View(model);
        }

        [HttpGet]
        public byte[] GetExerciseGif(int exerciseId)
        {
            Exercise exercise = _exerciseRepository.GetExerciseById(exerciseId);
            return exercise.GifPicture;
        }

        public IActionResult CreateMeasurements(TraineeDetailsPageViewModel model)
        {
            model.MeasureDate = DateTime.ParseExact(model.StrMeasureDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            TraineeMeasurements measurements = new TraineeMeasurements
            {
                FatRatio = model.FatRatio,
                Weight = model.Weight,
                Height = model.Height,
                TraineeId = model.Id,
                Date = model.MeasureDate
            };
            _traineeMeasurementsRepository.CreateTraineeMeasurements(measurements);
            return RedirectToAction("TraineeDetails", "Home", new { Id = model.Id });
        }

        [HttpPost]
        public IActionResult EditMeasurements(TraineeDetailsPageViewModel model)
        {
            model.EditMeasureDate = DateTime.ParseExact(model.StrEditMeasureDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            TraineeMeasurements measurements = new TraineeMeasurements
            {
                FatRatio = model.EditFatRatio,
                Weight = model.EditWeight,
                Height = model.EditHeight,
                MeasurementId = model.MeasurementId,
                Date = model.EditMeasureDate
            };
            _traineeMeasurementsRepository.UpdateTraineeMeasurements(measurements);
            return RedirectToAction("TraineeDetails", "Home", new { Id = model.Id });
        }

        public IActionResult DeleteMeasurements(int measurementId, string traineeId)
        {
            _traineeMeasurementsRepository.DeleteTraineeMeasurements(measurementId);

            return RedirectToAction("TraineeDetails", "Home", new { Id = traineeId });
        }

        public IActionResult AssignExercise(TraineeDetailsPageViewModel model)
        {
            model.ExStartDate = DateTime.ParseExact(model.StrExStartDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            model.ExEndDate = DateTime.ParseExact(model.StrExEndDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DailyRoutine dailyRoutine = new DailyRoutine
            {
                ExerciseId = model.ExId,
                TraineeId = model.Id,
                StartDate = model.ExStartDate,
                EndDate = model.ExEndDate,
                Interval = model.ExInterval,
                Sets = model.ExSets
            };
            dailyRoutine.RoutineId = _dailyRoutineRepository.CreateDailyRoutine(dailyRoutine);
            _dailyProgressRepository.CreateDailyProgress(dailyRoutine);
            return RedirectToAction("TraineeDetails", "Home", new { Id = model.Id });
        }

        public IActionResult EditAssignedExercise(TraineeDetailsPageViewModel model)
        {
            model.EditExStartDate = DateTime.ParseExact(model.StrEditExStartDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            model.EditExEndDate = DateTime.ParseExact(model.StrEditExEndDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DailyRoutine dailyRoutine = new DailyRoutine
            {
                RoutineId = model.RoutineId,
                StartDate = model.EditExStartDate,
                EndDate = model.EditExEndDate,
                Interval = model.EditExInterval,
                Sets = model.EditExSets,
                TraineeId = model.Id,
                ExerciseId = model.ExId
            };
            _dailyRoutineRepository.UpdateDailyRoutine(dailyRoutine);
            _dailyProgressRepository.UpdateDailyProgress(dailyRoutine);

            return RedirectToAction("TraineeDetails", "Home", new { Id = model.Id });
        }

        public IActionResult DeleteAssignedExercise(int routineId, string traineeId)
        {
            _dailyRoutineRepository.DeleteDailyRoutine(routineId);

            return RedirectToAction("TraineeDetails", "Home", new { Id = traineeId });
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
