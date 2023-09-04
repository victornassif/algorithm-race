using Microsoft.Azure.Functions.Worker;
using sort_functions.Models;
using System.Diagnostics;
using System.Drawing;

namespace sort_functions.ActivitiesFunctions
{
    public static class CountingSort
    {
        [Function(nameof(CountingSorting))]
        public static WinnerModel CountingSorting([ActivityTrigger] ParamModel model)
        {
            Stopwatch timer = Stopwatch.StartNew();
            var array = model.nonSortedArray;
            
            if (array == null)
                throw new ArgumentNullException("null array");
            else if (array.Length == 0)
                throw new ArgumentException("empty array");
            else if (array.Length == 1)
                throw new ArgumentException("array already sorted");

            var size = array.Length;
            var maxElement = GetMaxVal(array, size);
            var occurrences = new int[maxElement + 1];
            for (int i = 0; i < maxElement + 1; i++)
            {
                occurrences[i] = 0;
            }
            for (int i = 0; i < size; i++)
            {
                occurrences[array[i]]++;
            }
            for (int i = 0, j = 0; i <= maxElement; i++)
            {
                while (occurrences[i] > 0)
                {
                    array[j] = i;
                    j++;
                    occurrences[i]--;
                }
            }
            
            timer.Stop();
            return new WinnerModel(array, "CountingSort", timer.Elapsed);
        }

        public static int GetMaxVal(int[] array, int size)
        {
            var maxVal = array[0];
            for (int i = 1; i < size; i++)
                if (array[i] > maxVal)
                    maxVal = array[i];
            return maxVal;
        }
    }
}
