using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace OpenSportsPlatform.Lib.Model.Dtos.Polar
{
    public class RegisterUserRequest
    {
        [JsonPropertyName("member-id")]
        public string MemberId { get; set; }
    }
}
