using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GymTracker.Models
{
    public class GymProfileViewModel
    {
        public Gym Gym { get; set; }

        public IEnumerable<Gym> Gyms { get; set; }
    }
}
