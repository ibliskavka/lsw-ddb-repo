namespace Lsw.Ddb.Repo
{
    public abstract class BaseModel
    {
        /// <summary>
        /// Partition Key
        /// </summary>
        public abstract string Pk { get; }

        /// <summary>
        /// Sort Key
        /// </summary>
        public abstract string Sk { get; }

        /// <summary>
        /// Custom validation. Called before writing to db.
        /// </summary>
        public virtual bool IsValid()
        {
            return true;
        }
    }
}
