using GymTracker.Models.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GymTracker.Models.Repositories
{
    public class TraineeMeasurementsRepository : ITraineeMeasurementsRepository
    {
        private readonly GymTrackerContext _aspnetGymTrackerContext;

        public TraineeMeasurementsRepository(GymTrackerContext aspnet_GymTrackerContext)
        {
            _aspnetGymTrackerContext = aspnet_GymTrackerContext;
        }

        public IEnumerable<TraineeMeasurements> GetTraineeMeasurements(string traineeId)
        {
            return _aspnetGymTrackerContext.TraineeMeasurements.Where(f => f.TraineeId == traineeId).OrderByDescending(f => f.Date);
        }

        public void CreateTraineeMeasurements(TraineeMeasurements traineeMeasurements)
        {
            _aspnetGymTrackerContext.TraineeMeasurements.Add(traineeMeasurements);

            _aspnetGymTrackerContext.SaveChanges();
        }

        public void UpdateTraineeMeasurements(TraineeMeasurements traineeMeasurements)
        {
            var result = _aspnetGymTrackerContext.TraineeMeasurements.SingleOrDefault(b => b.MeasurementId == traineeMeasurements.MeasurementId);
            if (result != null)
            {
                result.Weight = traineeMeasurements.Weight;
                result.Height = traineeMeasurements.Height;
                result.FatRatio = traineeMeasurements.FatRatio;
                _aspnetGymTrackerContext.SaveChanges();
            }
        }

        public void DeleteTraineeMeasurements(int measurementId)
        {
            _aspnetGymTrackerContext.TraineeMeasurements.Remove(_aspnetGymTrackerContext.TraineeMeasurements.Where(c => c.MeasurementId == measurementId).FirstOrDefault());
            _aspnetGymTrackerContext.SaveChanges();
        }
    }
}
