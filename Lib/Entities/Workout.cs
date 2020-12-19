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
        public virtual float? DurationInSec { get; set; }
        public virtual float? CaloriesInKCal { get; set; }
        public virtual float? AltitudeMinInMeters { get; set; }
        public virtual float? AltitudeMaxInMeters { get; set; }
        public virtual float? AscendInMeters { get; set; }
        public virtual float? DescendInMeters { get; set; }
        public virtual float? DistanceInKm { get; set; }
        public virtual float? HeartRateAvgBpm { get; set; }
        public virtual float? HeartRateMaxBpm { get; set; }
        public virtual float? CadenceAvgRpm { get; set; }
        public virtual float? CadenceMaxRpm { get; set; }
        public virtual float? SpeedMaxKmh {get;set;}
        public virtual float? SpeedAvgKmh { get; set; }
        public virtual string InsertUser { get; set; }
        public virtual DateTime InsertDate { get; set; }
        public virtual string UpdateUser { get; set; }
        public virtual DateTime UpdateDate { get; set; }
        public virtual SportsCategory SportsCategory { get; set; }
    }
}
