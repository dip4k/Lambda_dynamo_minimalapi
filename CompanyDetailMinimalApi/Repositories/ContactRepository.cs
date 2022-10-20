using System.Net;
using System.Text.Json;

using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;

using CompanyDetailMinimalApi.Contracts.Data;
using CompanyDetailMinimalApi.Repositories.DynamoDbUtils;

namespace CompanyDetailMinimalApi.Repositories
{
    public class ContactRepository : IContactRepository
    {
        private readonly IAmazonDynamoDB _dynamoDb;
        private readonly string _tableName;

        public ContactRepository(IAmazonDynamoDB dynamoDb, string tableName)
        {
            _dynamoDb = dynamoDb;
            _tableName = tableName;
        }
        public async Task<bool> BatchWriteAsync(List<ContactDetailDto> contactDetailList)
        {
            List<WriteRequest> contactBatchItems = new List<WriteRequest>();
            foreach (var contactItem in contactDetailList)
            {
                contactBatchItems.Add(new WriteRequest
                {
                    PutRequest = DynamoUtils.CreatePutRequestObj(contactItem)
                });
            }


            // Construct table-keys mapping
            Dictionary<string, List<WriteRequest>> requestItems = new Dictionary<string, List<WriteRequest>>();
            requestItems[_tableName] = contactBatchItems;

            BatchWriteItemRequest request = new BatchWriteItemRequest { RequestItems = requestItems };

            BatchWriteItemResponse response;
            int callCount = 0;
            do
            {
                Console.WriteLine("Making request");
                response = await _dynamoDb.BatchWriteItemAsync(request);
                callCount++;


                var unprocessed = response.UnprocessedItems;
                request.RequestItems = unprocessed;
            } while (response.UnprocessedItems.Count > 0);

            //Console.WriteLine("Total # of batch write API calls made: {0}", callCount);
            return callCount == contactDetailList.Count;

        }

        public async Task<bool> CreateAsync(ContactDetailDto contactDetail)
        {
            PutItemRequest createItemRequest = DynamoUtils.CreatePutItemRequestObj(contactDetail, _tableName);

            var response = await _dynamoDb.PutItemAsync(createItemRequest);
            return response.HttpStatusCode == HttpStatusCode.OK;
        }

        public async Task<ContactDetailDto> GetAsync(Guid id)
        {
            var getItemRequest = new GetItemRequest
            {
                TableName = _tableName,
                Key = new Dictionary<string, AttributeValue>()
            {
                { "pk", new AttributeValue { S = id.ToString() } },
                { "sk", new AttributeValue { S = id.ToString() } }
            },
                ConsistentRead = true
            };

            var response = await _dynamoDb.GetItemAsync(getItemRequest);
            if (response.Item.Count == 0)
            {
                return null;
            }

            var itemAsDocument = Document.FromAttributeMap(response.Item);
            return JsonSerializer.Deserialize<ContactDetailDto>(itemAsDocument.ToJson());
        }

        public async Task<bool> UpdateAsync(ContactDetailDto contactDetail)
        {
            var contactDetailAsJson = JsonSerializer.Serialize(contactDetail);
            var itemAsDocument = Document.FromJson(contactDetailAsJson);
            var itemAsAttributes = itemAsDocument.ToAttributeMap();
            var updateItemRequest = new PutItemRequest
            {
                TableName = _tableName,
                Item = itemAsAttributes
            };

            var response = await _dynamoDb.PutItemAsync(updateItemRequest);
            return response.HttpStatusCode == HttpStatusCode.OK;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var deleteItemRequest = new DeleteItemRequest
            {
                TableName = _tableName,
                Key = new Dictionary<string, AttributeValue>()
            {
                { "pk", new AttributeValue { S = id.ToString() } },
                { "sk", new AttributeValue { S = id.ToString() } }
            }
            };
            var response = await _dynamoDb.DeleteItemAsync(deleteItemRequest);
            return response.HttpStatusCode == HttpStatusCode.OK;
        }
    }
}
