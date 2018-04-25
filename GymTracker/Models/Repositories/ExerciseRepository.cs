using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GymTracker.Models.Repositories
{
    public class ExerciseRepository : IExerciseRepository
    {
        private readonly Aspnet_GymTrackerContext _aspnetGymTrackerContext;

        public ExerciseRepository(Aspnet_GymTrackerContext aspnet_GymTrackerContext)
        {
            _aspnetGymTrackerContext = aspnet_GymTrackerContext;
        }

        public IEnumerable<Exercise> Exercises
        {
            get
            {
                return _aspnetGymTrackerContext.Exercise.ToList();

            }
        }

        public Exercise GetExerciseById(int exerciseId)
        {
            return _aspnetGymTrackerContext.Exercise.FirstOrDefault(d => d.ExerciseId == exerciseId);
        }

        public void CreateExercise(Exercise exercise)
        {
            _aspnetGymTrackerContext.Exercise.Add(exercise);

            _aspnetGymTrackerContext.SaveChanges();
        }
    }
}
