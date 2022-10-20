using ThirdParty.Json.LitJson;

namespace CompanyDetailMinimalApi.Contracts.Requests
{
    public class CompanyDetailCreateRequest
    {
        public string Id { get; set; }
        public string Company { get; set; }
        public string Status { get; set; }
        public List<ContactDetailRequest> ContactDetails { get; set; }
        public List<string> Domains { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zipcode { get; set; }
        public string CompanySize { get; set; }
        public Journeys Journeys { get; set; }
    }
}
