using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace Lsw.Ddb.Repo
{
    public abstract class DynamoRepository
    {
        protected IDynamoConfig Config { get; set; }
        protected readonly IAmazonDynamoDB DbClient;
        protected Table Table;

        protected DynamoRepository(
            IDynamoConfig cfg,
            IAmazonDynamoDB dbClient)
        {
            DbClient = dbClient;
            Config = cfg;

            Table = Table.LoadTable(DbClient, Config.TableName);
        }

        public async Task<string> GetJson(string partitionKey, string sortKey)
        {
            var doc = await Table.GetItemAsync(partitionKey, sortKey);
            return doc?.ToJson();
        }

        public async Task PutJson(string json)
        {
            var doc = Document.FromJson(json);
            await Table.PutItemAsync(doc, new PutItemOperationConfig
            {
                ReturnValues = ReturnValues.None
            });
        }

        public async Task<TModel> GetItem<TModel>(string partitionKey, string sortKey)
            where TModel : BaseModel
        {
            var doc = await Table.GetItemAsync(partitionKey, sortKey);
            return doc?.Map<TModel>();
        }

        /// <summary>
        /// Standard put operation
        /// </summary>
        public virtual async Task PutItem<TModel>(TModel model)
            where TModel : BaseModel
        {
            IsValid(model);
            var doc = model.Map();

            await Table.PutItemAsync(doc, new PutItemOperationConfig
            {
                ReturnValues = ReturnValues.None
            });
        }

        public async Task DeleteItem(string partitionKey, string sortKey)
        {
            var request = new DeleteItemRequest
            {
                TableName = Config.TableName,
                Key = MakeKey(partitionKey, sortKey)
            };
            var result = await DbClient.DeleteItemAsync(request);
        }

        protected Dictionary<string, AttributeValue> MakeKey(string partitionKey, string sortKey)
        {
            return new Dictionary<string, AttributeValue>
            {
                { Config.PkName, new AttributeValue(partitionKey) },
                { Config.SkName, new AttributeValue(sortKey) }
            };
        }

        /// <summary>
        /// Called before saving the object
        /// </summary>
        protected void IsValid(BaseModel model)
        {
            if (string.IsNullOrWhiteSpace(model.Pk))
            {
                throw new ArgumentNullException(nameof(model.Sk), "Required");
            }
            if (string.IsNullOrWhiteSpace(model.Sk))
            {
                throw new ArgumentNullException(nameof(model.Sk), "Required");
            }

            if (!model.IsValid())
            {
                throw new Exception("BaseModel object did not pass validation.");
            }
        }
    }
}
