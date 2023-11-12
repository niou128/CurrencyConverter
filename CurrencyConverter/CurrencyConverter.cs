using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace CurrencyConverter
{
    public static class Function1
    {
        [FunctionName("Function1")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("CurrencyConverter function processed a request.");

            string amountStr = req.Query["amount"];
            string currency = req.Query["currency"];

            if (!double.TryParse(amountStr, out double amount))
            {
                return new BadRequestObjectResult("Please pass a valid amount on the query string.");
            }

            if (currency != "USD")
            {
                return new BadRequestObjectResult("Currently only 'USD' currency conversion is supported.");
            }

            double conversionRate = 1.1;
            double convertedAmount = amount * conversionRate;

            return new OkObjectResult($"{amount} EUR is {convertedAmount} USD.");
        }
    }
}
