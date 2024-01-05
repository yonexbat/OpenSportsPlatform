namespace OpenSportsPlatform.Lib.Model.Entities;

public class Segment : IEntity
{
    public virtual int Id { get; set; }
    public virtual int WorkoutId { get; set; }
    public virtual string? InsertUser { get; set; }
    public virtual DateTimeOffset? InsertDate { get; set; }
    public virtual string? UpdateUser { get; set; }
    public virtual DateTimeOffset? UpdateDate { get; set; }
    public virtual required Workout Workout { get; set; }
    public virtual IList<Sample>? Samples { get; set; }
}