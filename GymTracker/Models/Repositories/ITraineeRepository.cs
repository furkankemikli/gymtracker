using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GymTracker.Models.Repositories
{
    public interface ITraineeRepository
    {
        IEnumerable<Trainee> GetTrainees(string trainerId); 

        Trainee GetTraineeById(string traineeId);

        void NewTraineeInsert();
    }
}
