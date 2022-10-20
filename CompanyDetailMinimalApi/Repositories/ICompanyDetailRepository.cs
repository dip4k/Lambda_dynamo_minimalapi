using CompanyDetailMinimalApi.Contracts.Data;

namespace CompanyDetailMinimalApi.Repositories
{
    public interface ICompanyDetailRepository
    {
        Task<bool> CreateAsync(CompanyDetailDto companyDetail);

        Task<CompanyDetailDto> GetAsync(Guid id);

        Task<bool> UpdateAsync(CompanyDetailDto companyDetail);

        Task<bool> DeleteAsync(Guid id);
    }
}
