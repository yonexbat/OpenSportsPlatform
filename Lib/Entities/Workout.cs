using System;
using System.Collections.Generic;
using System.Text;

namespace OpenSportsPlatform.Lib.Entities
{
    public class Workout : IEntity
    {
        public virtual int Id { get; set; }
        public virtual int SportsCategoryId { get; set; }
        public virtual string Name { get; set; }
        public virtual DateTime? StartTime { get; set; }
        public virtual DateTime? EndTime { get; set; }
        public virtual string InsertUser { get; set; }
        public virtual DateTime InsertDate { get; set; }
        public virtual string UpdateUser { get; set; }
        public virtual DateTime UpdateDate { get; set; }
        public virtual SportsCategory SportsCategory { get; set; }
    }
}
