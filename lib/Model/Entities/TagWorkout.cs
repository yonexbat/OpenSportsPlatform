using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSportsPlatform.Lib.Model.Entities
{
    public class TagWorkout : IEntity
    {
        public int Id { get; set; }
        public virtual int WorkoutId { get; set; }
        public virtual int TagId { get; set; }
        public virtual string? InsertUser { get; set; }
        public virtual DateTimeOffset? InsertDate { get; set; }
        public virtual string? UpdateUser { get; set; }
        public virtual DateTimeOffset? UpdateDate { get; set; }
        public virtual required Workout Workout { get; set;}
        public virtual required Tag Tag { get; set; }
    }
}
