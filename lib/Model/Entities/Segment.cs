﻿using System;
using System.Collections.Generic;
using System.Text;

namespace OpenSportsPlatform.Lib.Model.Entities
{
    public class Segment : IEntity
    {
        public virtual int Id { get; set; }
        public virtual int WorkoutId { get; set; }
        public virtual string InsertUser { get; set; }
        public virtual DateTime InsertDate { get; set; }
        public virtual string UpdateUser { get; set; }
        public virtual DateTime UpdateDate { get; set; }
        public virtual Workout Workout { get; set; }
        public virtual IList<Sample> Samples { get; set; }
    }
}
