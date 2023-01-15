using OpenSportsPlatform.Lib.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSportsPlatform.Lib.Model.Dtos.Security
{
    class SecuredEntityDto : ISecuredEntity
    {
        public string OwnerUserId { get; set; } = null!;
    }
}
