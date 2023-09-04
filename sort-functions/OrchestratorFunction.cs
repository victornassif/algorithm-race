using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.DurableTask;
using Microsoft.DurableTask.Client;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using sort_functions.ActivitiesFunctions;
using sort_functions.Models;

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

            List<Task<WinnerModel>> tasks = new List<Task<WinnerModel>>
            {
                context.CallActivityAsync<WinnerModel>(nameof(BucketSort.BucketSorting), bodyString),
                context.CallActivityAsync<WinnerModel>(nameof(BubbleSort.BubbleSorting), bodyString),
                context.CallActivityAsync<WinnerModel>(nameof(CountingSort.CountingSorting), bodyString),
                context.CallActivityAsync<WinnerModel>(nameof(HeapSort.HeapSorting), bodyString),
                context.CallActivityAsync<WinnerModel>(nameof(InsertionSort.InsertionSorting), bodyString),
                context.CallActivityAsync<WinnerModel>(nameof(LinqSort.LinqSorting), bodyString),
                context.CallActivityAsync<WinnerModel>(nameof(MergeSort.MergeSorting), bodyString),
                context.CallActivityAsync<WinnerModel>(nameof(QuickSort.QuickSorting), bodyString),
                context.CallActivityAsync<WinnerModel>(nameof(RadixSort.RadixSorting), bodyString),
                context.CallActivityAsync<WinnerModel>(nameof(SelectionSort.SelectionSorting), bodyString),
                context.CallActivityAsync<WinnerModel>(nameof(ShellSort.ShellSorting), bodyString)
            };

            Task<WinnerModel> tresult = await Task.WhenAny(tasks);
            int[] t = new int[0];
            try
            {
                t = tresult.Result.sortedArray;

            }
            catch (AggregateException ae)
            {
                ae.Handle((x) =>
                {
                    Console.WriteLine(x.Message);
                    return false;
                });
            }

            //int[] t = tresult.Result.sortedArray;

            Console.WriteLine($"HERE'S WINNER!");
            Console.WriteLine($"Winner: {tresult.Result.winnerName}");
            Console.WriteLine($"TimeElapsed: {tresult.Result.timeElapsed.ToString()}");
            

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
