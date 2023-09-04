using Microsoft.Azure.Functions.Worker;
using sort_functions.Models;
using System.Diagnostics;

namespace sort_functions.ActivitiesFunctions
{
    public static class BubbleSort
    {
        [Function(nameof(BubbleSorting))]
        public static WinnerModel BubbleSorting([ActivityTrigger] ParamModel model)
        {
            Stopwatch timer = Stopwatch.StartNew();

            var array = model.nonSortedArray;

            if (array == null)
                throw new ArgumentNullException("null array");
            else if (array.Length == 0)
                throw new ArgumentException("empty array");
            else if (array.Length == 1)
                throw new ArgumentException("array already sorted");

            try
            { 
                var n = array.Length;
                bool swapRequired;
                for (int i = 0; i < n - 1; i++)
                {
                    swapRequired = false;
                    for (int j = 0; j < n - i - 1; j++)
                        if (array[j] > array[j + 1])
                        {
                            var tempVar = array[j];
                            array[j] = array[j + 1];
                            array[j + 1] = tempVar;
                            swapRequired = true;
                        }
                    if (swapRequired == false)
                        break;
                }
                timer.Stop();
                return new WinnerModel(array, "BubbleSort", timer.Elapsed);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
