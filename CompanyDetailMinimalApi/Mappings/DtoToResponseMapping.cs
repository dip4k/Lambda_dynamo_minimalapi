using CompanyDetailMinimalApi.Contracts.Data;
using CompanyDetailMinimalApi.Contracts.Requests;
using CompanyDetailMinimalApi.Contracts.Responses;

namespace CompanyDetailMinimalApi.Mappings
{
    public static class DtoToResponseMapping
    {
        public static CompanyDetailResponse FromCompanyDto(this CompanyDetailDto companyDetail,List<ContactDetailDto> contactDetails)
        {
            return new CompanyDetailResponse
            {
                Id = companyDetail.Id,
                Company = companyDetail.Company,
                City = companyDetail.City,
                CompanySize = companyDetail.CompanySize,
                Address1 = companyDetail.Address1,
                Address2 = companyDetail.Address2,
                State = companyDetail.State,
                Status = companyDetail.Status,
                Zipcode = companyDetail.Zipcode,
                Domains = companyDetail.Domains.Split(",").ToList(),
                ContactDetails = contactDetails.Select(x => new ContactDetailRequest { CompanyDetailId = x.CompanyDetailId, Id = x.Id, Contactnumber = x.Contactnumber, Email = x.Email, Name = x.Name }).ToList(),
                Journeys = new Journeys
                {
                    Caregiving = new Caregiving { Enabled = companyDetail.IsCaregiving, Partners = companyDetail.CaregivingPartners.Split(",").ToList() },
                    Parenting = new Parenting { Enabled = companyDetail.IsParenting, Partners = companyDetail.ParentingPartners.Split(",").ToList() },
                }
            };
        }
    }
}
