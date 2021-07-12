using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using Elasticsearch.Net;
using ElasticSearchDataMigrationFunctionApp;
using ElasticSearchDataMigrationFunctionApp.Models;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace ElasticSearchDataMigrationFunctionApp
{
    public  class ElasticSearchDataMigrationFunctionApp
    {
        private IElasticCommandRepository _elasticCommandRepository;

        public ElasticSearchDataMigrationFunctionApp(IElasticCommandRepository elasticCommandRepository)
        {
            _elasticCommandRepository = elasticCommandRepository;
        }

        [FunctionName("ELasticSearchDataConversionApp")]
        public async Task RunAsync([BlobTrigger("patientfeedcontainer/{name}", Connection = "AzureWebJobsStorage")]Stream myBlob, string name, ILogger log)
        {
            // Console.WriteLine("Test Logger");

            CloudStorageAccount account = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=storageaccountipasnbb3a;AccountKey=HpqmldPFxepZmjFiOK8YN1jZbt7T4kIGSSszpNSIQjjWRYhi3GEzhgbfi1kr+JCnPzDvWi8vNlmrqK/+LdEO5g==;EndpointSuffix=core.windows.net");
            CloudBlobClient blobClient = account.CreateCloudBlobClient();

            CloudBlobContainer SourceBlobContainer = blobClient.GetContainerReference("patientfeedcontainer");
            CloudBlobContainer ProcessBlobContainer = blobClient.GetContainerReference("patientfeedprocessed");
            CloudBlobContainer IgnoredBlobContainer = blobClient.GetContainerReference("patientfeedignored");

            CloudBlockBlob sourceBlob = SourceBlobContainer.GetBlockBlobReference(name);


            var mymodel = DeserializeFromStream(myBlob);
            var ConvertedModel = ConvertModel(mymodel);
           var response =await _elasticCommandRepository.IndexPartialDocument(ConvertedModel);
            if (response)
              await  MoveBlobBetweenContainers(sourceBlob, ProcessBlobContainer);
            else
              await  MoveBlobBetweenContainers(sourceBlob, IgnoredBlobContainer);        // Console.WriteLine(response);

            
        }


        public static PatientVisitModel DeserializeFromStream(Stream blob)
        {
            try
            {              

                StreamReader reader = new StreamReader(blob, Encoding.UTF8);
                var text = reader.ReadToEnd();            
               return JsonConvert.DeserializeObject<PatientVisitModel>(text);
                
            }
            catch (Exception ex)
            {

                throw ex;
            }
           
        }

        public ElasticSearchDBModel ConvertModel(PatientVisitModel patientVisitModel)
        {
            ElasticSearchDBModel elasticSearchDB = new ElasticSearchDBModel
            {
                

                TenantID = patientVisitModel.TenantId,
                PatientVisitId = patientVisitModel.PatientVisitId,
                AccountNumber=patientVisitModel.Visit.AccountNumber,
                VisitNumber=patientVisitModel.Visit.VisitNumber,
                TenantPatientIdentifier =  patientVisitModel.Patient?.TenantPatientIdentifier?.ToList().Select(
                    x => new Tenantpatientidentifier
                    {
                        TenantPatientId = x.TenantPatientId,
                        TenantPatientIdentifierDescription = x.TenantPatientIdentifierDescription,
                        TenantPatientIdentifierType = x.TenantPatientIdentifierType
                    }).ToArray(),
                    
                PatientAccountId = patientVisitModel.PatientAccountId
            };

            return elasticSearchDB;
        }


        private async Task  MoveBlobBetweenContainers(CloudBlockBlob srcBlob, CloudBlobContainer destContainer)
        {
            try
            {
                CloudBlockBlob destBlob;

                //Copy source blob to destination container
                using (MemoryStream memoryStream = new MemoryStream())
                {
                  await  srcBlob.DownloadToStreamAsync(memoryStream);

                    //put the time stamp
                    var newBlobName = srcBlob.Name;

                    destBlob = destContainer.GetBlockBlobReference(newBlobName);

                    //copy source blob content to destination blob
                   await destBlob.StartCopyAsync(srcBlob);
                    
                }
                //remove source blob after copy is done.
                await srcBlob.DeleteAsync();

            }
            catch (Exception ex)
            {
                
                throw ex;
            }
           
        }

    }
}
