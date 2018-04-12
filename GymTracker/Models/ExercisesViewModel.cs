using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GymTracker.Models
{
    public class ExercisesViewModel
    {
        public IEnumerable<Exercise> Exercises { get; set; }

        //for new exercise modal
        public string Name { get; set; }

        public double CalorieBySet { get; set; }

        public byte[] GifPicture { get; set; }

        public string Category { get; set; }
    }
}
