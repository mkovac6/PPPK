using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Cosmos.Dao
{
    public static class CosmosDbServiceProvider
    {

        private const string Account = "https://todoperson.documents.azure.com:443/";
        private const string Key = "qoj5nbHaBgpgrtW0BjTQKO3hk0692lX8bSTwdXSMz9bKKOzuTy6nHPvqzOmDi98MaWvyTD0oL7OxACDbBjGr5g==";
        private const string ContainerName = "Tasks";
        private const string DatabaseName = "Tasks";
        private static ICosmosDbService cosmosDbService;

        public static ICosmosDbService CosmosDbService { get => cosmosDbService;  }

        public async static Task Init()
        {
            CosmosClient cosmosClient = new CosmosClient(Account, Key);
            cosmosDbService = new CosmosDbService(cosmosClient, DatabaseName, ContainerName);
            DatabaseResponse database = await cosmosClient.CreateDatabaseIfNotExistsAsync(DatabaseName);
            await database.Database.CreateContainerIfNotExistsAsync(ContainerName, "/id");
        }
    }
}