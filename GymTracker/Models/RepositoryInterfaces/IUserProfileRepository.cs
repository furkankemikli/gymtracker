using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GymTracker.Models.RepositoryInterfaces
{
    public interface IUserProfileRepository
    {
        int ChangeName(ApplicationUser user, string name);

        int ChangeSurname(ApplicationUser user, string surname);        

        int ChangeCity(ApplicationUser user, string city);

        int ChangePicture(ApplicationUser user, byte[] picture);
        
    }
}
