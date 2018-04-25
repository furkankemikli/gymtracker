using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GymTracker.Models.Repositories
{
    public interface ITraineeRepository
    {
        IEnumerable<Trainee> GetTrainees(string trainerId); 

        Trainee GetTraineeById(string traineeId);

        void CreateTrainee(Trainee trainee);

        void UpdateTrainee(Trainee trainee, ApplicationUser user);

        void DeleteTrainee(string traineeId);
    }
}
