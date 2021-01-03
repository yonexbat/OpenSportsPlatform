using System;
using System.Collections.Generic;
using System.Text;

namespace OpenSportsPlatform.Lib.Model.Entities
{
    public class UserProfile : IEntity
    {
        public string Name { get; set; }
        public string UserId { get; set; }
        public int Id { get; set; }
        public bool IsAdmin { get; set; }
        public virtual string InsertUser { get; set; }
        public virtual DateTime InsertDate { get; set; }
        public virtual string UpdateUser { get; set; }
        public virtual DateTime UpdateDate { get; set; }

        public virtual IList<Workout> Workouts { get; set; }
    }
}
