using PartnerApi.Contracts.Data;
using PartnerApi.Contracts.Response;

namespace PartnerApi.Mappings
{
    public static class DtoToResponseMapping
    {
        public static PartnerDetailsResponse ToPartnerResponse(List<PartnerDetailDto> partnerDetailList)
        {

            return new PartnerDetailsResponse
            {
                Caregiving = partnerDetailList
                    .Where(x => x.TypeId == PartnerTypes.CaregivingId)
                    .Select(x => x.Name).ToList(),
                Parenting = partnerDetailList
                    .Where(x => x.TypeId == PartnerTypes.ParentingId)
                    .Select(x => x.Name).ToList()
            };
        }
    }
}
