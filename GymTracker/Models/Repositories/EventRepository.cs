using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GymTracker.Models.Repositories
{
    public class EventRepository : IEventRepository
    {
        private readonly GymTrackerContext _aspnetGymTrackerContext;

        public EventRepository(GymTrackerContext aspnet_GymTrackerContext)
        {
            _aspnetGymTrackerContext = aspnet_GymTrackerContext;
        }

        public List<Event> Events(string trainerId)
        {
            return _aspnetGymTrackerContext.Event.Where(f => f.UserId == trainerId).ToList();
        }

        public Event GetEventById(int eventId)
        {
            return _aspnetGymTrackerContext.Event.FirstOrDefault(d => d.EventId == eventId);
        }

        public List<Event> GetInvitedTraineeEvent(int holderEventId)
        {
            return _aspnetGymTrackerContext.Event.Where(f => f.HolderEventId == holderEventId).ToList();
        }

        public Event GetEventByHolderAndTrainee(int holderId, string traineeId)
        {
            return _aspnetGymTrackerContext.Event.FirstOrDefault(f => f.HolderEventId == holderId && f.UserId == traineeId);
        }

        public int CreateEvent(Event newEvent)
        {
            _aspnetGymTrackerContext.Event.Add(newEvent);

            _aspnetGymTrackerContext.SaveChanges();

            return newEvent.EventId;
        }

        public void UpdateEvent(Event editEvent)
        {
            var result = _aspnetGymTrackerContext.Event.SingleOrDefault(b => b.EventId == editEvent.EventId);
            if (result != null)
            {
                result.Name = editEvent.Name;
                result.Description = editEvent.Description;
                result.Location = editEvent.Location;
                result.StartDate = editEvent.StartDate;
                result.EndDate = editEvent.EndDate;
                _aspnetGymTrackerContext.SaveChanges();
            }
        }

        public void DeleteEvent(int eventId)
        {
            _aspnetGymTrackerContext.Event.Remove(_aspnetGymTrackerContext.Event.Where(c => c.EventId == eventId).FirstOrDefault());
            _aspnetGymTrackerContext.SaveChanges();
        }
    }
}
