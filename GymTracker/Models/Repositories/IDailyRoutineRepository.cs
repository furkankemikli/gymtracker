using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GymTracker.Models.Repositories
{
    public interface IDailyRoutineRepository
    {
        IEnumerable<DailyRoutine> DailyRoutines(string traineeId);

        void NewDailyRoutineInsert();
    }
}
