using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GymTracker.Models.TraineeViewModels
{
    public class NewTraineeViewModel
    {
        public string Name { get; set; }

        public string Surame { get; set; }

        public string Email { get; set; }

        public DateTime DateOfBirth { get; set; }

        public byte[] Image { get; set; }

        public string Phone { get; set; }

        public double Weight { get; set; }

        public double Height { get; set; }

        public double FatRatio { get; set; }

        public string City { get; set; }

        public string Gender { get; set; }
    }
}
