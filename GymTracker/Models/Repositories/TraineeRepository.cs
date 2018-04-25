using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GymTracker.Models.Repositories
{
    public class TraineeRepository : ITraineeRepository
    {
        private readonly Aspnet_GymTrackerContext _aspnetGymTrackerContext;

        public TraineeRepository(Aspnet_GymTrackerContext aspnet_GymTrackerContext)
        {
            _aspnetGymTrackerContext = aspnet_GymTrackerContext;
        }

        public IEnumerable<Trainee> GetTrainees(string trainerId)
        {
            return _aspnetGymTrackerContext.Trainee.Where(f => f.TrainerId == trainerId).ToList();
        }

        public Trainee GetTraineeById(string traineeId)
        {
            return _aspnetGymTrackerContext.Trainee.FirstOrDefault(d => d.TraineeId == traineeId);
        }

        public void CreateTrainee(Trainee trainee)
        {
            _aspnetGymTrackerContext.Trainee.Add(trainee);

            _aspnetGymTrackerContext.SaveChanges();
        }

        public void UpdateTrainee(Trainee trainee, ApplicationUser user)
        {
            var result = _aspnetGymTrackerContext.Trainee.SingleOrDefault(b => b.TraineeId == trainee.TraineeId);
            if (result != null)
            {
                result.Birthday = trainee.Birthday;
                result.Weight = trainee.Weight;
                result.Height = trainee.Height;
                result.Gender = trainee.Gender;
                result.FatRatio = trainee.FatRatio;
                _aspnetGymTrackerContext.SaveChanges();
            }
            var result2 = _aspnetGymTrackerContext.ApplicationUser.SingleOrDefault(b => b.Id == trainee.TraineeId);
            if (result2 != null)
            {
                result2.Name = user.Name;
                result2.Email = user.Email;
                result2.PhoneNumber = user.PhoneNumber;
                result2.City = user.City;
                result2.Picture = user.Picture;
                _aspnetGymTrackerContext.SaveChanges();
            }
        }

        public void DeleteTrainee(string traineeId)
        {
            _aspnetGymTrackerContext.Trainee.Remove(_aspnetGymTrackerContext.Trainee.Where(c => c.TraineeId == traineeId).FirstOrDefault());
            _aspnetGymTrackerContext.ApplicationUser.Remove(_aspnetGymTrackerContext.ApplicationUser.Where(c => c.Id == traineeId).FirstOrDefault());
            _aspnetGymTrackerContext.SaveChanges();
        }
    }
}
