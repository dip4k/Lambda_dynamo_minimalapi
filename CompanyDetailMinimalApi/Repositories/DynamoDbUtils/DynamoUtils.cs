using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;

using System.Text.Json;

namespace CompanyDetailMinimalApi.Repositories.DynamoDbUtils
{
    public static class DynamoUtils
    {
        public static PutItemRequest CreatePutItemRequestObj<T>(T dtoModel, string _tableName)
        {
            var contactDetailAsJson = JsonSerializer.Serialize(dtoModel);
            var itemAsDocument = Document.FromJson(contactDetailAsJson);
            var itemAsAttributes = itemAsDocument.ToAttributeMap();
            var createItemRequest = new PutItemRequest
            {
                TableName = _tableName,
                Item = itemAsAttributes
            };
            return createItemRequest;
        }

        public static PutRequest CreatePutRequestObj<T>(T dtoModel)
        {
            var contactDetailAsJson = JsonSerializer.Serialize(dtoModel);
            var itemAsDocument = Document.FromJson(contactDetailAsJson);
            var itemAsAttributes = itemAsDocument.ToAttributeMap();
            var createItemRequest = new PutRequest
            {
                Item = itemAsAttributes
            };
            return createItemRequest;
        }
    }
}
