using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace OpenSportsPlatform.Lib.Model.Dtos.Polar
{
    public class UserExtraInfoResponse
    {
        [JsonPropertyName("value")]
        public string Value { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("index")]
        public int? Index { get; set; }

    }
}
