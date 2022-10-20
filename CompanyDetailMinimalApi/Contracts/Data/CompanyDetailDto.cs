
using System.Text.Json.Serialization;

namespace CompanyDetailMinimalApi.Contracts.Data
{
    public class CompanyDetailDto
    {
        [JsonPropertyName("pk")]
        public string Pk => Id;

        [JsonPropertyName("sk")]
        public string Sk => Id;

        public string Id { get; init; } = default!;

        public string Company { get; init; }
        public string Status { get; init; }
        public string Domains { get; init; }
        public string Address1 { get; init; }
        public string Address2 { get; init; }
        public string City { get; init; }
        public string State { get; init; }
        public string Zipcode { get; init; }
        public string CompanySize { get; init; }

        public bool? IsCaregiving { get; init; }
        public string CaregivingPartners { get; init; }

        public bool? IsParenting { get; init; }
        public string ParentingPartners { get; init; }
        public string Contacts { get; set; }
    }
}
