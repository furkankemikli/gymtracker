using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GymTracker.Models
{
    public class ExercisesViewModel
    {
        public IEnumerable<Exercise> Exercises { get; set; }

        //for new exercise modal
        [Required]
        [StringLength(150)]
        public string Name { get; set; }

        public double CalorieBySet { get; set; }

        public byte[] GifPicture { get; set; }

        [Required]
        [StringLength(150)]
        public string Category { get; set; }
    }
}
