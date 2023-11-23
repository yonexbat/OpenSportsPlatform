namespace OpenSportsPlatform.Lib.Model.Entities;

public class Tag : IEntity
{
    public virtual int Id { get ; set; }
    public virtual required string Name { get; set; }
    public virtual string? InsertUser { get; set; }
    public virtual DateTimeOffset? InsertDate { get; set; }
    public virtual string? UpdateUser { get; set; }
    public virtual DateTimeOffset? UpdateDate { get; set; }
    public virtual IList<TagWorkout>? TagWorkouts { get; set; }
}