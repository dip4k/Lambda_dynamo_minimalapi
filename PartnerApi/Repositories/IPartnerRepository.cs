using PartnerApi.Contracts.Data;

namespace PartnerApi.Repositories
{
    public interface IPartnerRepository
    {
        Task<List<PartnerDetailDto>> GetAllPartnersAsync();
    }
}
