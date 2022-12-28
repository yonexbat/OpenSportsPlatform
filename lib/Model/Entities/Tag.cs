using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSportsPlatform.Lib.Model.Entities
{
    public class Tag : IEntity
    {
        public int Id { get ; set; }
        public string Name { get; set; }
        public string? InsertUser { get; set; }
        public DateTimeOffset? InsertDate { get; set; }
        public string? UpdateUser { get; set; }
        public DateTimeOffset? UpdateDate { get; set; }

        public virtual IList<TagWorkout> TagWorkouts { get; set; }
    }
}
