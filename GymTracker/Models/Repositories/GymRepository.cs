using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GymTracker.Models.Repositories
{
    public class GymRepository : IGymRepository
    {
        private readonly Aspnet_GymTrackerContext _aspnetGymTrackerContext;

        public GymRepository(Aspnet_GymTrackerContext aspnet_GymTrackerContext)
        {
            _aspnetGymTrackerContext = aspnet_GymTrackerContext;
        }

        public IEnumerable<Gym> Gyms
        {
            get
            {
                return _aspnetGymTrackerContext.Gym.ToList();
            }
        }

        public Gym GetGymById(int gymId)
        {
            return _aspnetGymTrackerContext.Gym.FirstOrDefault(d => d.GymId == gymId);
        }

        public void CreateGym(Gym gym)
        {
            _aspnetGymTrackerContext.Gym.Add(gym);

            _aspnetGymTrackerContext.SaveChanges();
        }
    }
}
