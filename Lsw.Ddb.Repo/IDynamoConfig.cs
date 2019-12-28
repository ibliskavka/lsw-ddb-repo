using System;
using System.Collections.Generic;
using System.Text;

namespace Lsw.Ddb.Repo
{
    public interface IDynamoConfig
    {
        string TableName { get; }
        string PkName { get; }
        string SkName { get; }
    }
}
