using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GymTracker.Models.TraineeViewModels
{
    public class EditTraineeViewModel
    {
        public string TraineeId { get; set; }

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

        public DateTime DateOfBirth { get; set; }

        public string CurrentImage { get; set; }

        public IFormFile Image { get; set; }

        [Phone]
        public string Phone { get; set; }

        public double Weight { get; set; }

        public double Height { get; set; }

        public double FatRatio { get; set; }
        
        [StringLength(150)]
        public string City { get; set; }

        public string Gender { get; set; }
    }
}
