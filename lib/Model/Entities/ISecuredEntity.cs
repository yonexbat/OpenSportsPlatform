namespace OpenSportsPlatform.Lib.Model.Entities;

public interface ISecuredEntity
{
    string OwnerUserId { get; }
}