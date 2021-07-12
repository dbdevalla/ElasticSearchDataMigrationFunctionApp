using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElasticSearchDataMigrationFunctionApp
{
    public class ElasticCommandRepository: IElasticCommandRepository

    {
        public IElasticSearchClientContext ElasticSearchClient;
        public ElasticCommandRepository(IElasticSearchClientContext elasticClient)
        {
            this.ElasticSearchClient = elasticClient ?? throw new System.ArgumentNullException(nameof(elasticClient));
        }

        public async Task<bool> IndexPartialDocument(ElasticSearchDBModel elasticSearchDBModel)
        {
            var ClientIndex = elasticSearchDBModel.TenantID.Replace(" ", string.Empty).ToLower();
            try
            {
                var exist = await GetDocumentByVisitID(ClientIndex, elasticSearchDBModel.PatientVisitId);
                if (exist != null)
                {

                    var resultresponse = await ElasticSearchClient.GetClient().UpdateAsync(DocumentPath<ElasticSearchDBModel>
                                                                                        .Id(elasticSearchDBModel.PatientVisitId), m => m
                                                                                                                .Index(ClientIndex)
                                                                                                                .DocAsUpsert(true)
                                                                                                                .Doc(elasticSearchDBModel));

                    if (resultresponse.IsValid && (resultresponse.Result.ToString() == "Created" || resultresponse.Result.ToString() == "Updated" || resultresponse.Result.ToString()=="Noop"))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return false;
        }




        public async Task<ElasticSearchDBModel> GetDocumentByVisitID(string indexName, string patientVisitID)
        {
            try
            {
                var GetRecord = await ElasticSearchClient.GetClient().SearchAsync<ElasticSearchDBModel>(s => s
                                  .Index(indexName.ToLower())
                                  .Query(q => q.MatchPhrase(m => m.Field(f => f.PatientVisitId).Query(patientVisitID))));
                return GetRecord.Documents.FirstOrDefault();
            }
            catch (Exception)
            {

                throw;
            }
           
        }
    }
}
