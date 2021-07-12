using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ElasticSearchDataMigrationFunctionApp
{
    public interface IElasticCommandRepository
    {
         Task<bool> IndexPartialDocument(ElasticSearchDBModel elasticSearchDBModel);
    }
}
