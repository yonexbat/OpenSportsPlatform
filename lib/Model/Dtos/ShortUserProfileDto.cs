using System;
using System.Collections.Generic;
using System.Text;

namespace OpenSportsPlatform.Lib.Model.Dtos
{
    public class ShortUserProfileDto
    {
        public string UserId { get; init; } = null!;
        public string Name { get; init; } = null!;
        public bool Authenticated { get; init; }
        public IList<string>? Roles { get; set; }
    }
}
