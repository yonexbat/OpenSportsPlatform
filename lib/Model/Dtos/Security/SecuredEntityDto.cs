using OpenSportsPlatform.Lib.Model.Entities;

namespace OpenSportsPlatform.Lib.Model.Dtos.Security;

class SecuredEntityDto : ISecuredEntity
{
    public string OwnerUserId { get; set; } = null!;
}