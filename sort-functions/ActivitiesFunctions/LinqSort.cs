using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.DurableTask;
using Microsoft.DurableTask.Client;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using sort_functions.Models;
using System.Diagnostics;

namespace sort_functions.ActivitiesFunctions
{
    public static class LinqSort
    {
        [Function(nameof(LinqSorting))]
        public static WinnerModel LinqSorting([ActivityTrigger] ParamModel model)
        {
            Stopwatch timer = Stopwatch.StartNew();
            var array = model.nonSortedArray;

            if (array == null)
                throw new ArgumentNullException("null array");
            else if (array.Length == 0)
                throw new ArgumentException("empty array");
            else if (array.Length == 1)
                throw new ArgumentException("array already sorted");


            List<int> list = array.ToList().Order().ToList();

            timer.Stop();
            return new WinnerModel(list.ToArray<int>(), "LinqSort", timer.Elapsed);
        }
    }
}
