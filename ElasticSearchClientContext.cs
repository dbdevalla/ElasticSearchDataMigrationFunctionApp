using System;
using System.Collections.Generic;
using System.Text;
using Elasticsearch.Net;
using Nest;

namespace ElasticSearchDataMigrationFunctionApp
{
    public class ElasticSearchClientContext: IElasticSearchClientContext
    {
        private readonly IElasticClient _client;
        public ElasticSearchClientContext(IElasticClient client)
        {
            client = CreateElasticClient();
            _client = client;
        }

        //public ElasticSearchClientContext() : this(CreateElasticClient()) { }

        public IElasticClient GetClient()
        {
            return _client;
        }

        private static ElasticClient CreateElasticClient()
        {
            var pool = new SingleNodeConnectionPool(new Uri("http://172.19.20.98:9200"));
            var setting = new ConnectionSettings(pool).BasicAuthentication("elastic", "Pel1t@sDev0ps");
            setting.DefaultIndex("tenantid");
            return new ElasticClient(setting);
        }
    }
}
