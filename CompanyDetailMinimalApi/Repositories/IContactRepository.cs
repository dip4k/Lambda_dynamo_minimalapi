using CompanyDetailMinimalApi.Contracts.Data;

namespace CompanyDetailMinimalApi.Repositories
{
    public interface IContactRepository
    {
        Task<bool> CreateAsync(ContactDetailDto contactDetail);

        Task<ContactDetailDto> GetAsync(Guid id);

        Task<bool> UpdateAsync(ContactDetailDto contactDetail);

        Task<bool> DeleteAsync(Guid id);

        Task<bool> BatchWriteAsync(List<ContactDetailDto> contactDetailList);
    }
}
