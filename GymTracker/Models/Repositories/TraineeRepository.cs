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
        

        public IEnumerable<TraineeInfoModel> GetTrainees(string trainerId)
        {
            var traineesGeneral = (from t in _aspnetGymTrackerContext.Trainee
                                   join u in _aspnetGymTrackerContext.ApplicationUser
                                   on t.TraineeId equals u.Id
                                   where t.TrainerId == trainerId
                                   select new { t.TraineeId, t.TrainerId, u.Name, u.Surname, t.Birthday, t.Weight, t.Height, t.Gender, t.FatRatio, u.Email, u.PhoneNumber, u.GymId, u.City, u.Picture, t.EntryDate }).ToList();
            List<TraineeInfoModel> trainees = new List<TraineeInfoModel>();
            foreach (var elmt in traineesGeneral)
            {
                TraineeInfoModel trainee = new TraineeInfoModel
                {
                    TraineeId = elmt.TraineeId,
                    TrainerId = elmt.TrainerId,
                    Name = elmt.Name,
                    Surname = elmt.Surname,
                    Birthday = elmt.Birthday,
                    Weight = elmt.Weight,
                    Height = elmt.Height,
                    Gender = elmt.Gender,
                    FatRatio = elmt.FatRatio,
                    Email = elmt.Email,
                    PhoneNumber = elmt.PhoneNumber,
                    GymId = elmt.GymId,
                    City = elmt.City,
                    Picture = elmt.Picture,
                    EntryDate = elmt.EntryDate
                };
                trainees.Add(trainee);
            }
            return trainees;
        }

        public TraineeInfoModel GetTraineeById(string traineeId)
        {
            var elmt = (from t in _aspnetGymTrackerContext.Trainee
                                   join u in _aspnetGymTrackerContext.ApplicationUser
                                   on t.TraineeId equals u.Id
                                   where t.TraineeId == traineeId
                                   select new { t.TraineeId, t.TrainerId, u.Name, u.Surname, t.Birthday, t.Weight, t.Height, t.Gender, t.FatRatio, u.Email, u.PhoneNumber, u.GymId, u.City, u.Picture, t.EntryDate }).FirstOrDefault();
            return new TraineeInfoModel
            {
                TraineeId = elmt.TraineeId,
                TrainerId = elmt.TrainerId,
                Name = elmt.Name,
                Surname = elmt.Surname,
                Birthday = elmt.Birthday,
                Weight = elmt.Weight,
                Height = elmt.Height,
                Gender = elmt.Gender,
                FatRatio = elmt.FatRatio,
                Email = elmt.Email,
                PhoneNumber = elmt.PhoneNumber,
                GymId = elmt.GymId,
                City = elmt.City,
                Picture = elmt.Picture,
                EntryDate = elmt.EntryDate
            };
        }

        public string GetUserId(string email, string name, string surname)
        {
            return _aspnetGymTrackerContext.ApplicationUser.FirstOrDefault(u => u.Email == email && u.Name == name && u.Surname == surname).Id;
        }

        public void CreateTrainee(Trainee trainee)
        {
            _aspnetGymTrackerContext.Trainee.Add(trainee);

            _aspnetGymTrackerContext.SaveChanges();
        }

        public void UpdateTrainee(TraineeInfoModel trainee)
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
                result2.Name = trainee.Name;
                result2.Email = trainee.Email;
                result2.PhoneNumber = trainee.PhoneNumber;
                result2.City = trainee.City;
                result2.Picture = trainee.Picture;
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
