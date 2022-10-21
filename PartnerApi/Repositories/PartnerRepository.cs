using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;

using PartnerApi.Contracts.Data;
using DynamoDbUtils;

namespace PartnerApi.Repositories
{
    public class PartnerRepository : IPartnerRepository
    {
        private readonly IAmazonDynamoDB _dynamoDb;
        private readonly string _tableName;

        public PartnerRepository(IAmazonDynamoDB dynamoDb, string tableName)
        {
            _dynamoDb = dynamoDb;
            _tableName = tableName;
        }

        public async Task<List<PartnerDetailDto>> GetAllPartnersAsync()
        {
            var partnerList = new List<PartnerDetailDto>();
            var request = new ScanRequest
            {
                TableName = _tableName,
            };

            var response = await _dynamoDb.ScanAsync(request);

            foreach (Dictionary<string, AttributeValue> item in response.Items)
            {
                var obj = DynamoUtils.MapDynamoResponseToDto<PartnerDetailDto>(item);
                partnerList.Add(obj);
            }
            return partnerList;
        }
    }
}
