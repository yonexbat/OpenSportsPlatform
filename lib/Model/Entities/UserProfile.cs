namespace OpenSportsPlatform.Lib.Model.Entities;

public class UserProfile : IEntity
{
    public virtual string? Name { get; set; }
    public virtual string? UserId { get; set; }
    public virtual int Id { get; set; }
    public virtual bool? IsAdmin { get; set; }
    public virtual string? PolarAccessToken { get; set; }
    public virtual DateTimeOffset? PolarAccessTokenValidUntil { get; set; }
    public virtual string? PolarUserId { get; set; }
    public virtual string? InsertUser { get; set; }
    public virtual DateTimeOffset? InsertDate { get; set; }
    public virtual string? UpdateUser { get; set; }
    public virtual DateTimeOffset? UpdateDate { get; set; }
    public virtual IList<Workout>? Workouts { get; set; }
}