using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace GymTracker.Models
{
    public partial class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            DailyProgress = new HashSet<DailyProgress>();
            DailyRoutine = new HashSet<DailyRoutine>();
            Event = new HashSet<Event>();
            TraineeGoals = new HashSet<TraineeGoals>();
            TraineeTrainer = new HashSet<Trainee>();
        }

        public override string Id { get; set; }
        public override int AccessFailedCount { get; set; }
        public override string ConcurrencyStamp { get; set; }
        public override string Email { get; set; }
        public override bool EmailConfirmed { get; set; }
        public override bool LockoutEnabled { get; set; }
        public override DateTimeOffset? LockoutEnd { get; set; }
        public override string NormalizedEmail { get; set; }
        public override string NormalizedUserName { get; set; }
        public override string PasswordHash { get; set; }
        public override string PhoneNumber { get; set; }
        public override bool PhoneNumberConfirmed { get; set; }
        public override string SecurityStamp { get; set; }
        public override bool TwoFactorEnabled { get; set; }
        public int? GymId { get; set; }
        public override string UserName { get; set; }
        public string City { get; set; }
        public byte[] Picture { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }

        public Gym Gym { get; set; }
        public Trainee TraineeTraineeNavigation { get; set; }
        public ICollection<DailyProgress> DailyProgress { get; set; }
        public ICollection<DailyRoutine> DailyRoutine { get; set; }
        public ICollection<Event> Event { get; set; }
        public ICollection<TraineeGoals> TraineeGoals { get; set; }
        public ICollection<Trainee> TraineeTrainer { get; set; }
    }
}
