using Microsoft.Azure.Functions.Worker;

namespace sort_functions.ActivitiesFunctions
{
    public static class RadixSort
    {
        [Function(nameof(RadixSorting))]
        public static int[] RadixSorting([ActivityTrigger] int[] array, int size)
        {
            {
                var maxVal = GetMaxVal(array, size);
                for (int exponent = 1; maxVal / exponent > 0; exponent *= 10)
                    CountingSortIntern(array, size, exponent);
                return array;
            }
        }

        public static int GetMaxVal(int[] array, int size)
        {
            var maxVal = array[0];
            for (int i = 1; i < size; i++)
                if (array[i] > maxVal)
                    maxVal = array[i];
            return maxVal;
        }

        public static void CountingSortIntern(int[] array, int size, int exponent)
        {
            var outputArr = new int[size];
            var occurences = new int[10];
            for (int i = 0; i < 10; i++)
                occurences[i] = 0;
            for (int i = 0; i < size; i++)
                occurences[(array[i] / exponent) % 10]++;
            for (int i = 1; i < 10; i++)
                occurences[i] += occurences[i - 1];
            for (int i = size - 1; i >= 0; i--)
            {
                outputArr[occurences[(array[i] / exponent) % 10] - 1] = array[i];
                occurences[(array[i] / exponent) % 10]--;
            }
            for (int i = 0; i < size; i++)
                array[i] = outputArr[i];
        }
    }
}
