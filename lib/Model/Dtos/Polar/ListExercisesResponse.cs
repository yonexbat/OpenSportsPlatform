using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace OpenSportsPlatform.Lib.Model.Dtos.Polar
{
    public class ListExercisesResponse
    {
        [JsonPropertyName("exercises")]
        public IList<string>? Exercises { get; set; }
    }
}
