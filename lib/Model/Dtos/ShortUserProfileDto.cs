using System;
using System.Collections.Generic;
using System.Text;

namespace OpenSportsPlatform.Lib.Model.Dtos
{
    public class ShortUserProfileDto
    {
        public string UserId { get; init; }
        public string Name { get; init; }
        public bool Authenticated { get; init; }
        public IList<string>? Roles { get; set; }
    }
}
