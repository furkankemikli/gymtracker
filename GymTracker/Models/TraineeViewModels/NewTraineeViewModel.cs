using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GymTracker.Models.TraineeViewModels
{
    public class NewTraineeViewModel
    {
        [Required]
        [StringLength(150)]
        public string Name { get; set; }

        [Required]
        [StringLength(150)]
        public string Surname { get; set; }

        [Required]
        [StringLength(150)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Phone]
        public string Phone { get; set; }

        [StringLength(150)]
        public string City { get; set; }

        public DateTime DateOfBirth { get; set; }

        public IFormFile Image { get; set; }

        public double Weight { get; set; }

        public double Height { get; set; }

        public double FatRatio { get; set; }

        public string Gender { get; set; }
    }
}
