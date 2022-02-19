using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSportsPlatform.Lib.Services.Contract
{
    public interface ICalculateWorkoutStatisticsService
    {
        Task Calculate(int workoutid);
    }
}
