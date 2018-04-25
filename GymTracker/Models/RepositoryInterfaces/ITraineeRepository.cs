using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GymTracker.Models.Repositories
{
    public interface ITraineeRepository
    {
        IEnumerable<TraineeInfoModel> GetTrainees(string trainerId); 

        TraineeInfoModel GetTraineeById(string traineeId);

        string GetUserId(string email, string name, string surname);

        void CreateTrainee(Trainee trainee);

        void UpdateTrainee(TraineeInfoModel trainee);

        void DeleteTrainee(string traineeId);
    }
}
