using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace OpenSportsPlatform.Lib.Model.Dtos.Polar
{
    public class TransactionResponse
    {
        [JsonPropertyName("transaction-id")]
        public ulong? TransactionId { get; set; }


        [JsonPropertyName("resource-uri")]
        public string ResourceUri { get; set; }
    }
}
