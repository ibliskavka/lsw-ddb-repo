using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;

namespace Lsw.Ddb.Repo
{
    /// <summary>
    /// Convenience extensions for getting/deleting data
    /// Mostly used in integration tests to validate that dat was written/serialized correctly.
    /// </summary>
    public static class DbExtensions
    {
        public static async Task<GetItemResponse> GetItem(this IAmazonDynamoDB ddb, IDynamoConfig cfg, string pk, string sk)
        {
            var request = new GetItemRequest { TableName = cfg.TableName };
            request.Key.Add(cfg.PkName, new AttributeValue(pk));
            request.Key.Add(cfg.SkName, new AttributeValue(sk));
            return await ddb.GetItemAsync(request);
        }

        public static async Task<DeleteItemResponse> DeleteItem(this IAmazonDynamoDB ddb, IDynamoConfig cfg, string pk, string sk)
        {
            var request = new DeleteItemRequest { TableName = cfg.TableName };
            request.Key.Add(cfg.PkName, new AttributeValue(pk));
            request.Key.Add(cfg.SkName, new AttributeValue(sk));
            return await ddb.DeleteItemAsync(request);
        }
    }
}
