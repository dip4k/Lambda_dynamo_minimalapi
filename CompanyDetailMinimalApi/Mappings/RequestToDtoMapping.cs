using System.Linq;

using CompanyDetailMinimalApi.Contracts.Data;
using CompanyDetailMinimalApi.Contracts.Requests;

namespace CompanyDetailMinimalApi.Mappings
{
    public static class RequestToDtoMapping
    {
        public static CompanyDetailDto ToCompanyDetail(this CompanyDetailCreateRequest companyDetailCreateRequest)
        {
            return new CompanyDetailDto
            {
                Id = Guid.NewGuid().ToString(),
                Company = companyDetailCreateRequest.Company,
                CompanySize = companyDetailCreateRequest.CompanySize,
                Address1 = companyDetailCreateRequest.Address1,
                Address2 = companyDetailCreateRequest.Address2,
                City = companyDetailCreateRequest.City,
                State = companyDetailCreateRequest.State,
                Status = companyDetailCreateRequest.Status,
                Zipcode = companyDetailCreateRequest.Zipcode,
                Domains = string.Join(",", companyDetailCreateRequest.Domains),
                IsCaregiving = companyDetailCreateRequest.Journeys.Caregiving.Enabled,
                CaregivingPartners = string.Join(",", companyDetailCreateRequest.Journeys.Caregiving.Partners),
                IsParenting = companyDetailCreateRequest.Journeys.Parenting.Enabled,
                ParentingPartners = string.Join(",", companyDetailCreateRequest.Journeys.Parenting.Partners)
            };
        }

        public static ContactDetailDto ToContactDetail(this ContactDetailRequest contactDetail, string companyDetailId)
        {
            return new ContactDetailDto
            {
                Id = Guid.NewGuid().ToString(),
                CompanyDetailId = companyDetailId,
                Contactnumber = contactDetail.Contactnumber,
                Email = contactDetail.Email,
                Name = contactDetail.Name
            };
        }
    }
}
