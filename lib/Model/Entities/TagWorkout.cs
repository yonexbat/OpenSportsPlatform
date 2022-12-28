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
        public string? InsertUser { get; set; }
        public DateTimeOffset? InsertDate { get; set; }
        public string? UpdateUser { get; set; }
        public DateTimeOffset? UpdateDate { get; set; }

        public Workout? Workout { get; set;}

        public Tag? Tag { get; set; }
    }
}
