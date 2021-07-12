using Nest;
using System;
using System.Collections.Generic;
using System.Text;

namespace ElasticSearchDataMigrationFunctionApp
{
    public interface IElasticSearchClientContext
    {
        IElasticClient GetClient();
    }
}
