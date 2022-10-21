using PartnerApi.Contracts.Response;
using PartnerApi.Mappings;
using PartnerApi.Repositories;

namespace PartnerApi.Sevices
{
    public class AppService : IAppservice
    {
        private readonly IPartnerRepository _partnerRepository;

        public AppService(IPartnerRepository partnerRepository)
        {
            _partnerRepository = partnerRepository;
        }

        public async Task<PartnerDetailsResponse> GetAllPartnersAsync()
        {
            var partnerList = await _partnerRepository.GetAllPartnersAsync();
            return DtoToResponseMapping.ToPartnerResponse(partnerList);
        }
    }
}
