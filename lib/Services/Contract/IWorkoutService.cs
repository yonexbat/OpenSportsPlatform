﻿using OpenSportsPlatform.Lib.Model.Dtos.Common;
using OpenSportsPlatform.Lib.Model.Dtos.Workout;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OpenSportsPlatform.Lib.Services.Contract
{
    public interface IWorkoutService
    {
        Task<WorkoutDto> GetWorkout(int id);

        Task DeleteWorkout(int id);

        Task SaveWorkout(SaveWorkoutDto dto);

        Task<EditWorkoutDto> GetEditWorkout(int id);

        Task AddTag(AddTagDto dto);

        Task RemoveTag(RemoveTagDto dto);

        Task<IList<SelectItemDto>> SerachTags(string name);
    }
}
