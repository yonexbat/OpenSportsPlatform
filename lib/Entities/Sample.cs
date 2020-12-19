using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.Text;

namespace OpenSportsPlatform.Lib.Entities
{
    public class Sample : IEntity
    {
        public virtual int Id { get; set; }
        public virtual int SegmentId { get; set; }
        public virtual float? AltitudeInMeters { get; set; }
        public virtual float? DistanceInKm { get; set; }
        public virtual float? SpeedKmh { get; set; }
        public virtual DateTime? Timestamp { get; set; }
        public virtual Point Location { get; set; }
        public virtual string InsertUser { get; set; }
        public virtual DateTime InsertDate { get; set; }
        public virtual string UpdateUser { get; set; }
        public virtual DateTime UpdateDate { get; set; }
        public virtual Segment Segment { get; set; }
    }
}
