using Microsoft.Azure.Functions.Worker;
using sort_functions.Models;
using System.Diagnostics;

namespace sort_functions.ActivitiesFunctions
{
    public static class ShellSort
    {

        [Function(nameof(ShellSorting))]
        public static WinnerModel ShellSorting([ActivityTrigger] ParamModel model)
        {
            Stopwatch timer = Stopwatch.StartNew();
            var array = model.nonSortedArray;

            if (array == null)
                throw new ArgumentNullException("null array");
            else if (array.Length == 0)
                throw new ArgumentException("empty array");
            else if (array.Length == 1)
                throw new ArgumentException("array already sorted");

            int size = array.Length;
            for (int interval = size / 2; interval > 0; interval /= 2)
            {
                for (int i = interval; i < size; i++)
                {
                    var currentKey = array[i];
                    var k = i;

                    while (k >= interval && array[k - interval] > currentKey)
                    {
                        array[k] = array[k - interval];
                        k -= interval;
                    }

                    array[k] = currentKey;
                }
            }

            timer.Stop();
            return new WinnerModel(array, "ShellSort", timer.Elapsed);
        }
    }
}