using Newtonsoft.Json;

namespace Lsw.Ddb.Repo
{
    /// <summary>
    /// Note: Keep properties publicly accessible for auto-mapper compatibility.
    /// </summary>
    public interface IBaseModel
    {
        /// <summary>
        /// Partition Key
        /// </summary>
        [JsonProperty(PropertyName = "pk")]
        string Pk { get; set; }

        /// <summary>
        /// Sort Key
        /// </summary>
        [JsonProperty(PropertyName = "sk")]
        string Sk { get; set; }

        /// <summary>
        /// Custom validation. Called before writing to db.
        /// </summary>
        bool IsValid();
    }
}
