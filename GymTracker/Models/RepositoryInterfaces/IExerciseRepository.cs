using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GymTracker.Models.Repositories
{
    public interface IExerciseRepository
    {
        IEnumerable<Exercise> Exercises { get; }

        Exercise GetExerciseById(int exerciseId);

        void CreateExercise(Exercise exercise);

    }
}
