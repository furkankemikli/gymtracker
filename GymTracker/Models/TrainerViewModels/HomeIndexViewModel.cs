using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GymTracker.Models.TrainerViewModels
{
    public class TraineeInviteModel
    {
        public string TraineeId { get; set; }

        public string TraineeName { get; set; }

        public string TraineeSurname { get; set; }

        public string TraineeEmail { get; set; }

        public bool IsChecked { get; set; }

        public TraineeInviteModel(string traineeId, string traineeName, string traineeSurname, string traineeEmail, Boolean isChecked)
        {
            TraineeId = traineeId;
            TraineeName = traineeName;
            TraineeSurname = traineeSurname;
            TraineeEmail = traineeEmail;
            IsChecked = isChecked;
        }

        public TraineeInviteModel()
        {
            TraineeId = "";
            TraineeName = "";
            TraineeSurname = "";
            TraineeEmail = "";
            IsChecked = false;
        }
    }

    public class HomeIndexViewModel
    {
        //for new event adding and update
        public int EventId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Location { get; set; }

        public string StrStartDate { get; set; }

        public string StrEndDate { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        //To list events
        public List<Event> Events { get; set; }

        public string jsonEvents { get; set; }

        public List<TraineeInviteModel> TraineeList { get; set;}

        public string InviteEventList { get; set; }
    }
}
