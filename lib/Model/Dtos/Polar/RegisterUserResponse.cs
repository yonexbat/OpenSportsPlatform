using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace OpenSportsPlatform.Lib.Model.Dtos.Polar
{
    public class RegisterUserResponse
    {
        [JsonPropertyName("polar-user-id")]
        public ulong? PolarUserId { get; set; }

        [JsonPropertyName("member-id")]
        public string? MemberId { get; set; }

        [JsonPropertyName("registration-date")]
        public DateTime? RegistrationDate { get; set; }

        [JsonPropertyName("first-name")]
        public string FirstName { get; set; }

        [JsonPropertyName("last-name")]
        public string LastName { get; set; }

        [JsonPropertyName("birthdate")]
        public DateTime? BirthDate { get; set; }

        [JsonPropertyName("gender")]
        public string Gender { get; set; }

        [JsonPropertyName("weight")]
        public float? Weight { get; set; }

        [JsonPropertyName("height")]
        public float? Height { get; set; }

        [JsonPropertyName("extra-info")]
        public IList<UserExtraInfoResponse> ExtraInfo { get; set; }
    }
}
