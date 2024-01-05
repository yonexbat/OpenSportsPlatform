using NetTopologySuite.Geometries;

namespace OpenSportsPlatform.Lib.Model.Entities;

public class Sample : IEntity
{
    public virtual int Id { get; set; }
    public virtual int SegmentId { get; set; }
    public virtual float? AltitudeInMeters { get; set; }
    public virtual float? DistanceInKm { get; set; }
    public virtual float? SpeedKmh { get; set; }
    public virtual DateTimeOffset? Timestamp { get; set; }
    public virtual Point? Location { get; set; }
    public virtual double? Longitude { get; set; }
    public virtual double? Latitude { get; set; }
    public virtual float? HeartRateBpm { get; set; }
    public virtual float? CadenceRpm { get; set; }
    public virtual string? InsertUser { get; set; }
    public virtual DateTimeOffset? InsertDate { get; set; }
    public virtual string? UpdateUser { get; set; }
    public virtual DateTimeOffset? UpdateDate { get; set; }
    public virtual required Segment Segment { get; set; }
}