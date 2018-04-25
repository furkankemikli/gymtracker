using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GymTracker.Models.Repositories
{
    public interface IGymRepository
    {
        IEnumerable<Gym> Gyms { get; }

        Gym GetGymById(int gymId);

        void CreateGym(Gym gym);
    }
}
