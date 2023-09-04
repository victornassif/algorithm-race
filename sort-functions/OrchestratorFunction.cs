using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.DurableTask;
using Microsoft.DurableTask.Client;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using sort_functions.ActivitiesFunctions;

namespace sort_functions
{
    public static class OrchestratorFunction
    {
        [Function("Orchestrator")]
        public static async Task RunOrchestrator(
            [OrchestrationTrigger] TaskOrchestrationContext context)
        {
            //CREATE LOGS 
            ILogger logger = context.CreateReplaySafeLogger("Orchestrator");
            logger.LogInformation("Saying hello.");

            //PROCESS REQUEST
            string? bodyString = context.GetInput<string>();

            if (string.IsNullOrEmpty(bodyString))
                throw new ArgumentNullException("inform the parameters correctly");

            List<Task<int[]>> tasks = new List<Task<int[]>>
            {
                context.CallActivityAsync<int[]>(nameof(BubbleSort.BubbleSorting), bodyString),
                context.CallActivityAsync<int[]>(nameof(BucketSort.BucketSorting), bodyString)
            };
            //var t3 = context.CallActivityAsync<string>(nameof(CountingSort.CountingSorting), array);
            //var t4 = context.CallActivityAsync<string>(nameof(HeapSort.HeapSorting), array);
            //var t5 = context.CallActivityAsync<string>(nameof(InsertionSort.InsertionSorting), array);
            //var t6 = context.CallActivityAsync<string>(nameof(LinqSort.LinqSorting), array);
            //var t7 = context.CallActivityAsync<string>(nameof(MergeSort.MergeSorting), array);
            //var t8 = context.CallActivityAsync<string>(nameof(QuickSort.QuickSorting), array);
            //var t9 = context.CallActivityAsync<string>(nameof(RadixSort.RadixSorting), array);
            //var t10 = context.CallActivityAsync<string>(nameof(SelectionSort.SelectionSorting), array);
            //var t11 = context.CallActivityAsync<string>(nameof(ShellSort.ShellSorting), array);

            Task<int[]> tresult = await Task.WhenAny(tasks);

            int[] t = tresult.Result;

            Console.WriteLine("there goes the result");
            foreach (var item in t)
            {
                Console.WriteLine(item);
            }
            //var tResult = await Task.WhenAny(t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11);
            //return 
            //var tResult = await Task.WhenAny(t1, t2, t3, t6);

            // returns ["Hello Tokyo!", "Hello Seattle!", "Hello London!"]
            //return outputs;
        }

        //[Function(nameof(SayHello))]
        //public static string SayHello([ActivityTrigger] string name, FunctionContext executionContext)
        //{
        //    ILogger logger = executionContext.GetLogger("SayHello");
        //    logger.LogInformation("Saying hello to {name}.", name);
        //    return $"Hello {name}!";
        //}

        [Function("FunctionSortArray_HttpStart")]
        public static async Task<HttpResponseData> HttpStart(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequestData req,
            [DurableClient] DurableTaskClient client,
            FunctionContext executionContext)
        {
            ILogger logger = executionContext.GetLogger("FunctionSortArray_HttpStart");

            string body = await new StreamReader(req.Body).ReadToEndAsync();

            // Function input comes from the request content.
            string instanceId = await client.ScheduleNewOrchestrationInstanceAsync(
                "Orchestrator", body);

            logger.LogInformation("Started orchestration with ID = '{instanceId}'.", instanceId);

            // Returns an HTTP 202 response with an instance management payload.
            // See https://learn.microsoft.com/azure/azure-functions/durable/durable-functions-http-api#start-orchestration
            return client.CreateCheckStatusResponse(req, instanceId);
        }
    }
}
