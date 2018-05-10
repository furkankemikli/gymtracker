using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GymTracker.Models.RepositoryInterfaces
{
    public interface ITraineeMeasurementsRepository
    {
        IEnumerable<TraineeMeasurements> GetTraineeMeasurements(string traineeId);

        void CreateTraineeMeasurements(TraineeMeasurements traineeMeasurements);

        void UpdateTraineeMeasurements(TraineeMeasurements traineeMeasurements);

        void DeleteTraineeMeasurements(int measurementId);
    }
}
