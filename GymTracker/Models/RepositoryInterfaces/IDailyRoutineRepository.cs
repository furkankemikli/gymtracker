using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GymTracker.Models.Repositories
{
    public interface IDailyRoutineRepository
    {
        IEnumerable<DailyRoutine> DailyRoutines(string traineeId);

        int CreateDailyRoutine(DailyRoutine dailyRoutine);

        void UpdateDailyRoutine(DailyRoutine dailyRoutine);

        void DeleteDailyRoutine(int routineId);
    }
}
