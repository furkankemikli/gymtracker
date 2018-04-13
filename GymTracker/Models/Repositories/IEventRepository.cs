using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GymTracker.Models.Repositories
{
    public interface IEventRepository
    {
        IEnumerable<Event> Events(string trainerId);

        Event GetEventById(int eventId);

        void NewEventInsert();
    }
}
