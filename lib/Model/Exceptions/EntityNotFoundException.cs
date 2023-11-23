namespace OpenSportsPlatform.Lib.Model.Exceptions;

public class EntityNotFoundException : Exception
{
    public Type? EntityType { get; set; }

    public int? EntityId { get; set; }
    public EntityNotFoundException(Type entityType, int entityId):
        base($"Entitiy not found. Type: {entityType?.Name}, Id: {entityId}")
    {
        EntityType = entityType;
        EntityId = entityId;
    }
}