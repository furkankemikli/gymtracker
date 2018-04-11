using System;
using System.Collections.Generic;

namespace GymTracker.Models
{
    public partial class Gym
    {
        public Gym()
        {
            ApplicationUser = new HashSet<ApplicationUser>();
        }

        public int GymId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

        public ICollection<ApplicationUser> ApplicationUser { get; set; }
    }
}
