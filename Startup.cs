using ElasticSearchDataMigrationFunctionApp;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;

[assembly: FunctionsStartup(typeof(Startup))]
namespace ElasticSearchDataMigrationFunctionApp
{
    
    public class Startup : FunctionsStartup
    {       

        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddSingleton<IElasticSearchClientContext, ElasticSearchClientContext>();
            builder.Services.AddSingleton<IElasticCommandRepository, ElasticCommandRepository>();           
        }
    }
}
