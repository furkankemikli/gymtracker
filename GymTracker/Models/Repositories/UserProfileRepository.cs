using GymTracker.Models.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GymTracker.Models.Repositories
{
    public class UserProfileRepository : IUserProfileRepository
    {
        private readonly Aspnet_GymTrackerContext _aspnetGymTrackerContext;

        public UserProfileRepository(Aspnet_GymTrackerContext aspnet_GymTrackerContext)
        {
            _aspnetGymTrackerContext = aspnet_GymTrackerContext;
        }

        public int ChangeName(ApplicationUser user, string name)
        {
            var result = _aspnetGymTrackerContext.ApplicationUser.SingleOrDefault(b => b.Id == user.Id);
            var count = 0;
            if (result != null)
            {
                result.Name = name;
                count = _aspnetGymTrackerContext.SaveChanges();
            }
            return count;
        }

        public int ChangeSurname(ApplicationUser user, string surname)
        {
            var result = _aspnetGymTrackerContext.ApplicationUser.SingleOrDefault(b => b.Id == user.Id);
            var count = 0;
            if (result != null)
            {
                result.Surname = surname;
                count = _aspnetGymTrackerContext.SaveChanges();
            }
            return count;
        }

        public int ChangeCity(ApplicationUser user, string city)
        {
            var result = _aspnetGymTrackerContext.ApplicationUser.SingleOrDefault(b => b.Id == user.Id);
            var count = 0;
            if (result != null)
            {
                result.City = city;
                count = _aspnetGymTrackerContext.SaveChanges();
            }
            return count;
        }

        public int ChangePicture(ApplicationUser user, string picture)
        {
            var result = _aspnetGymTrackerContext.ApplicationUser.SingleOrDefault(b => b.Id == user.Id);
            var count = 0;
            if (result != null)
            {
                result.Picture = picture;
                count = _aspnetGymTrackerContext.SaveChanges();
            }
            return count;
        }
    }
}
