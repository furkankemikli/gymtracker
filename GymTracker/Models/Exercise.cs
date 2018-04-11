using System;
using System.Collections.Generic;

namespace GymTracker.Models
{
    public partial class Exercise
    {
        public Exercise()
        {
            DailyProgress = new HashSet<DailyProgress>();
            DailyRoutine = new HashSet<DailyRoutine>();
        }

        public int ExerciseId { get; set; }
        public string Name { get; set; }
        public double CalorieBySet { get; set; }
        public byte[] GifPicture { get; set; }
        public string Category { get; set; }

        public ICollection<DailyProgress> DailyProgress { get; set; }
        public ICollection<DailyRoutine> DailyRoutine { get; set; }
    }
}
