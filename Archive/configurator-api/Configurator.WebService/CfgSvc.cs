namespace Configurator.WebService
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Azure.WebJobs;
    using Microsoft.Azure.WebJobs.Extensions.Http;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Logging;

    public static class CfgSvc
    {
        /// <summary>
        /// Get Storage Account from environment variable.
        /// </summary>
        public static string StorageAccount
        {
            get
            {
                string key = string.Empty;

                try
                {
                    key = Environment.GetEnvironmentVariable("STORAGE_ACCOUNT", EnvironmentVariableTarget.Process);
                }
                catch { }

                return key;
            }
        }

        /// <summary>
        /// Get Storage Container from environment variable.
        /// </summary>
        public static string StorageContainer
        {
            get
            {
                string container = string.Empty;

                try
                {
                    container = Environment.GetEnvironmentVariable("STORAGE_CONTAINER", EnvironmentVariableTarget.Process);
                }
                catch { }

                return container;
            }
        }

        /// <summary>
        /// Initialize Storage Client.
        /// </summary>
        /// <returns></returns>
        public static bool Set()
        {
            return StorageClient.Initialize(StorageAccount, StorageContainer);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="req"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        [FunctionName("Cfg")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation($"Storage Client Status: {Set()}");

            string cfkKey = req.Query["key"];

            string cfgApp = req.Query["app"];

            if (!string.IsNullOrEmpty(cfkKey) || !string.IsNullOrEmpty(cfgApp))
            {
                log.LogInformation($"CfgKey: {cfkKey}, CfkApp: {cfgApp}");
                var document = CfgIO.GetCfg(cfkKey, cfgApp);

                if (document != null)
                {
                    log.LogInformation($"PASS");
                    return new OkObjectResult(document);
                }
                else
                {
                    log.LogInformation($"FAIL: NULL DOC");
                    return new OkObjectResult(null);
                }
            }
            else
            {
                log.LogInformation($"FAIL: NULL KEY/APP");
                return new OkObjectResult(null);
            }
        }
    }
}
