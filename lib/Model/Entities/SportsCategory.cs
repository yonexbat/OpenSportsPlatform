using System;
using System.Collections.Generic;
using System.Text;

namespace OpenSportsPlatform.Lib.Model.Entities
{
    public class SportsCategory : IEntity
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string? InsertUser { get; set; }
        public virtual DateTimeOffset? InsertDate { get; set; }
        public virtual string? UpdateUser { get; set; }
        public virtual DateTimeOffset? UpdateDate { get; set; }
        public virtual IList<Workout> Workouts { get; set; }
    }
}
