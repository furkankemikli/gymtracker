using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GymTracker.Models;
using Microsoft.AspNetCore.Authorization;

namespace GymTracker.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
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
