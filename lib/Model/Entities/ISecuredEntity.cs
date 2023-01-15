using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSportsPlatform.Lib.Model.Entities
{
    public interface ISecuredEntity
    {
        string OwnerUserId { get; }
    }
}
