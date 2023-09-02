using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.DurableTask;
using Microsoft.DurableTask.Client;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace sort_functions.ActivitiesFunctions
{
    public static class LinqSort
    {
        [Function(nameof(Sort))]
        //public static IEnumerable<int> LinqSort_Function([ActivityTrigger] IEnumerable<int> list, FunctionContext functionContext)
        public static string Sort([ActivityTrigger] string name, FunctionContext executionContext)
        {
            //if (list == null)
            //    return new List<int>();
            //else
            //{
            //    return list.Order();
            //}


            ILogger logger = executionContext.GetLogger("SayHello");
            logger.LogInformation("Saying hello to {name}.", name);
            return $"Hello {name}!";
        }
    }
}
