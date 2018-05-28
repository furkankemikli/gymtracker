using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GymTracker.Models.TraineeViewModels
{
    public class TraineeDetailsPageViewModel
    {
        //to reach trainee info
        public string Id { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Email { get; set; }

        public DateTime DateOfBirth { get; set; }

        public byte[] Image { get; set; }

        public string Phone { get; set; }

        public string City { get; set; }

        public string Gender { get; set; }

        public DateTime EntryDate { get; set; }

        //from trainee measurements to show last measurements of the trainee
        public double Weight { get; set; }

        public double Height { get; set; }

        public int FatRatio { get; set; }

        public DateTime MeasureDate { get; set; }

        public string StrMeasureDate { get; set; }

        //to list done exercises with percentage
        public IEnumerable<DailyProgress> DailyProgresses { get; set; }

        //to list exercises assigned to the trainee(current and passed 15 days after since enddate)
        public IEnumerable<DailyRoutine> DailyRoutines { get; set; }

        //for listing them to assign to the trainee
        public IEnumerable<Exercise> Exercises { get; set; }

        //to assign exercise
        public int ExId { get; set; }

        public double ExSets { get; set; }

        public DateTime ExStartDate { get; set; }

        public string StrExStartDate { get; set; }

        public DateTime ExEndDate { get; set; }

        public string StrExEndDate { get; set; }

        public int ExInterval { get; set; }

        //to show and edit trainee measurements
        public IEnumerable<TraineeMeasurements> Measurements { get; set; }

        public int MeasurementId { get; set; }

        public double EditWeight { get; set; }

        public double EditHeight { get; set; }

        public int EditFatRatio { get; set; }

        public DateTime EditMeasureDate { get; set; }

        public string StrEditMeasureDate { get; set; }

        //to edit assigned exercise
        public int RoutineId { get; set; }

        public string EditExName { get; set; }
        
        public string EditExCategory { get; set; }

        public double EditExSets { get; set; }

        public DateTime EditExStartDate { get; set; }

        public string StrEditExStartDate { get; set; }

        public DateTime EditExEndDate { get; set; }

        public string StrEditExEndDate { get; set; }

        public int EditExInterval { get; set; }

    }
}
