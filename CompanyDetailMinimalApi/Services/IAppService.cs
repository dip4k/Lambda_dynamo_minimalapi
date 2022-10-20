using CompanyDetailMinimalApi.Contracts.Requests;
using CompanyDetailMinimalApi.Contracts.Responses;

namespace CompanyDetailMinimalApi.Services
{
    public interface IAppService
    {
        Task<CompanyDetailResponse> CreateAsync(CompanyDetailCreateRequest companyDetail);

        Task<CompanyDetailResponse> GetCompanyAsync(Guid id);

        Task<bool> UpdateCompanyAsync(CompanyDetailCreateRequest company);

        Task<bool> DeleteCompanyAsync(Guid id);

        Task<ContactDetailResponse> GetContactAsync(Guid id);

        Task<bool> UpdateContactAsync(ContactDetailRequest contact);

        Task<bool> DeleteContactAsync(Guid id);


    }
}
