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

        public void UpdateGym(Gym gym, string UserId)
        {
            var result = _aspnetGymTrackerContext.ApplicationUser.FirstOrDefault(d => d.Id == UserId);
            if(result.GymId == null)
            {
                _aspnetGymTrackerContext.Gym.Add(gym);
                result.GymId = gym.GymId;
            }
            else
            {
                var dataGym = _aspnetGymTrackerContext.Gym.FirstOrDefault(d => d.GymId == result.GymId);
                dataGym.Name = gym.Name;
                dataGym.Address = gym.Address;
                dataGym.City = gym.City;
                dataGym.Phone = gym.Phone;
                dataGym.Email = gym.Email;
            }
            _aspnetGymTrackerContext.SaveChanges();
        }
    }
}
