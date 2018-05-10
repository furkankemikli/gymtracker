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

        public Exercise(int exerciseId, string name, string category, double calorieBySet)
        {
            ExerciseId = exerciseId;
            Name = name;
            Category = category;
            CalorieBySet = calorieBySet;
        }

        public int ExerciseId { get; set; }
        public string Name { get; set; }
        public double CalorieBySet { get; set; }
        public string Category { get; set; }
        public byte[] GifPicture { get; set; }

        public ICollection<DailyProgress> DailyProgress { get; set; }
        public ICollection<DailyRoutine> DailyRoutine { get; set; }
    }
}
