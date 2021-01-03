using System;
using System.Collections.Generic;
using System.Text;

namespace OpenSportsPlatform.Lib.Model.Dtos
{
    public class ShortUserProfileDto
    {
        public string UserId { get; set; }
        public string Name { get; set; }
        public bool Authenticated { get; set; }
        public IList<string> Roles { get; set; }
    }
}
