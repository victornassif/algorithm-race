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
        [Function(nameof(LinqSorting))]
        public static int[] LinqSorting([ActivityTrigger] int[] array)
        {
            List<int> list = array.ToList().Order().ToList();
            return list.ToArray<int>();
        }
    }
}
