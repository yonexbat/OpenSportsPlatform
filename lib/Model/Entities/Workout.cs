namespace OpenSportsPlatform.Lib.Model.Entities;

public class Workout : IEntity, ISecuredEntity
{
    public virtual int Id { get; set; }
    public virtual int SportsCategoryId { get; set; }
    public virtual int UserProfileId { get; set; }
    public virtual string? Name { get; set; }
    public virtual string? Notes { get; set; }
    public virtual DateTimeOffset? StartTime { get; set; }
    public virtual DateTimeOffset? EndTime { get; set; }
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
    public virtual float? SpeedMaxKmh { get; set; }
    public virtual float? SpeedAvgKmh { get; set; }
    public virtual string? InsertUser { get; set; }
    public virtual DateTimeOffset? InsertDate { get; set; }
    public virtual string? UpdateUser { get; set; }
    public virtual DateTimeOffset? UpdateDate { get; set; }
    public virtual SportsCategory? SportsCategory { get; set; }
    public virtual IList<Segment>? Segments { get; set; }
    public virtual UserProfile? UserProfile { get; set; }
    public virtual IList<TagWorkout>? TagWorkouts { get; set; } = null!;
    public string OwnerUserId => this.UserProfile?.UserId ?? string.Empty;
}