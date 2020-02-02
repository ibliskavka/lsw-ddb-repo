using Newtonsoft.Json;

namespace Lsw.Ddb.Repo
{
    public interface IBaseModel
    {
        /// <summary>
        /// Partition Key
        /// </summary>
        [JsonProperty(PropertyName = "pk")]
        string Pk { get; }

        /// <summary>
        /// Sort Key
        /// </summary>
        [JsonProperty(PropertyName = "sk")]
        string Sk { get; }

        /// <summary>
        /// Custom validation. Called before writing to db.
        /// </summary>
        bool IsValid();
    }
}
