using Microsoft.Azure.Functions.Worker;

namespace sort_functions.ActivitiesFunctions
{
    public static class BubbleSort
    {
        [Function(nameof(BubbleSorting))]
        public static int[] BubbleSorting([ActivityTrigger] int[] array)
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
            return array;
        }
    }
}
