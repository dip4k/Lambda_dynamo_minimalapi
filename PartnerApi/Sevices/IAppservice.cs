using PartnerApi.Contracts.Response;

namespace PartnerApi.Sevices
{
    public interface IAppservice
    {
        Task<PartnerDetailsResponse> GetAllPartnersAsync();
    }
}
