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

namespace GymTracker.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IEventRepository _eventRepository;

        public HomeController(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
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

        public IActionResult NewTrainee()
        {
            return View();
        }

        public IActionResult Exercises()
        {
            return View();
        }

        public IActionResult Trainees()
        {
            return View();
        }

        public IActionResult TraineeDetails(string Id)
        {
            return View();
        }

        public IActionResult EditTrainee()
        {
            return View();
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
