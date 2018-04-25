using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GymTracker.Models.Repositories
{
    public interface IEventRepository
    {
        List<Event> Events(string trainerId);

        Event GetEventById(int eventId);

        void CreateEvent(Event newEvent);

        void UpdateEvent(Event newEvent);

        void DeleteEvent(int eventId);
    }
}
