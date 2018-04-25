using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GymTracker.Models
{
    public class TraineeInfoModel
    {
        public string TraineeId { get; set; }

        public string TrainerId { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public DateTime? Birthday { get; set; }

        public double? Weight { get; set; }

        public double? Height { get; set; }

        public string Gender { get; set; }

        public double? FatRatio { get; set; }
        
        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public int? GymId { get; set; }

        public string City { get; set; }

        public string Picture { get; set; }

        public DateTime EntryDate { get; set; }

    }
}
