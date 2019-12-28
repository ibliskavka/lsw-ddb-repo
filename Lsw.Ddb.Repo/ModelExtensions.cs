using Amazon.DynamoDBv2.DocumentModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Collections.Generic;

namespace Lsw.Ddb.Repo
{
    public static class ModelExtensions
    {

        static readonly JsonSerializerSettings Settings = new JsonSerializerSettings()
        {
            ContractResolver = new DefaultContractResolver()
            {
                NamingStrategy = new CamelCaseNamingStrategy(),
            }
        };

        /// <summary>
        /// Maps ddb model to object
        /// </summary>
        public static TModel Map<TModel>(this Document doc)
        {
            string json = doc.ToJson();
            return JsonConvert.DeserializeObject<TModel>(json);
        }

        /// <summary>
        /// Maps ddb model to object
        /// </summary>
        public static List<TModel> Map<TModel>(this IEnumerable<Document> doc)
        {
            var results = new List<TModel>();
            foreach (var d in doc)
            {
                results.Add(d.Map<TModel>());
            }
            return results;
        }

        /// <summary>
        /// Map object to ddb
        /// </summary>
        public static Document Map(this BaseModel model)
        {
            string json = JsonConvert.SerializeObject(model, Settings);
            return Document.FromJson(json);
        }
    }
}
