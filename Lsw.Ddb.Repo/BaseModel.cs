using Newtonsoft.Json;

namespace Lsw.Ddb.Repo
{
    public abstract class BaseModel
    {
        /// <summary>
        /// Partition Key
        /// </summary>
        [JsonProperty(PropertyName = "pk")]
        public string Pk { get; set; }

        /// <summary>
        /// Sort Key
        /// </summary>
        [JsonProperty(PropertyName = "sk")]
        public string Sk { get; set; }

        /// <summary>
        /// Custom validation. Called before writing to db.
        /// </summary>
        public virtual bool IsValid()
        {
            return true;
        }
    }
}
