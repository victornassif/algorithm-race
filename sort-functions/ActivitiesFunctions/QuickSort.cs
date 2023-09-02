using Microsoft.Azure.Functions.Worker;

namespace sort_functions.ActivitiesFunctions
{
    public static class QuickSort
    {
        [Function(nameof(QuickSorting))]
        public static int[] QuickSorting([ActivityTrigger] int[] array, int size)
        {
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

            return array;
        }
    }
}
