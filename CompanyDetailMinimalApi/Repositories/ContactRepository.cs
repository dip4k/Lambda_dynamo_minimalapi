using System.Collections.Generic;
using System.Net;
using System.Text.Json;

using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;

using CompanyDetailMinimalApi.Contracts.Data;

using DynamoDbUtils;

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
                response = await _dynamoDb.BatchWriteItemAsync(request);
                callCount++;


                var unprocessed = response.UnprocessedItems;
                request.RequestItems = unprocessed;
            } while (response.UnprocessedItems.Count > 0);

            //Console.WriteLine("Total # of batch write API calls made: {0}", callCount);
            return true;

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
            return DynamoUtils.MapDynamoResponseToDto<ContactDetailDto>(response.Item);
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

        public async Task<List<ContactDetailDto>> BatchGetAsync(Guid companyId, List<Guid> contactIds)
        {
            var res = new List<ContactDetailDto>();
            List<Dictionary<string, AttributeValue>> keys = new List<Dictionary<string, AttributeValue>>();
            foreach (var contactId in contactIds)
            {
                keys.Add(
                    new Dictionary<string, AttributeValue>(){
                        { "pk", new AttributeValue { S = companyId.ToString() } },
                        { "sk", new AttributeValue { S = contactId.ToString() } }
                    });
            }
            var request = new BatchGetItemRequest
            {
                RequestItems = new Dictionary<string, KeysAndAttributes>()
                {
                    { _tableName,
                        new KeysAndAttributes
                        {
                            Keys = keys
                        }
                    }
                }
            };

            var result = await _dynamoDb.BatchGetItemAsync(request);

            var responses = result.Responses; // The attribute list in the response.

            var table1Results = responses[_tableName];

            foreach (var item in table1Results)
            {
                var obj = DynamoUtils.MapDynamoResponseToDto<ContactDetailDto>(item);
                res.Add(obj);
            }

            // Any unprocessed keys? could happen if you exceed ProvisionedThroughput or some other error.
            Dictionary<string, KeysAndAttributes> unprocessedKeys = result.UnprocessedKeys;
            foreach (KeyValuePair<string, KeysAndAttributes> pair in unprocessedKeys)
            {
                Console.WriteLine(pair.Key, pair.Value);
            }

            return res;
        }
    }
}
