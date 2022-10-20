using System.Text.Json.Serialization;

namespace CompanyDetailMinimalApi.Contracts.Data
{
    public class ContactDetailDto
    {
        [JsonPropertyName("pk")]
        public string Pk => Id;

        [JsonPropertyName("sk")]
        public string Sk => Id;

        public string Id { get; init; } = default!;
        public string Name { get; init; }
        public string Email { get; init; }
        public string Contactnumber { get; init; }
        public string CompanyDetailId { get; set; }
    }
}
