using Microsoft.Azure.Functions.Worker;

namespace sort_functions.ActivitiesFunctions
{
    public static class QuickSort
    {
        [Function(nameof(QuickSorting))]
        public static int[] QuickSorting([ActivityTrigger] int[] array, int leftIndex = 0, int rightIndex = int.MinValue)
        {
            if (rightIndex == int.MinValue) 
                rightIndex = array.Length - 1;

            var i = leftIndex;
            var j = rightIndex;
            var pivot = array[leftIndex];
            while (i <= j)
            {
                while (array[i] < pivot)
                {
                    i++;
                }

                while (array[j] > pivot)
                {
                    j--;
                }
                if (i <= j)
                {
                    int temp = array[i];
                    array[i] = array[j];
                    array[j] = temp;
                    i++;
                    j--;
                }
            }

            if (leftIndex < j)
                QuickSorting(array, leftIndex, j);
            if (i < rightIndex)
                QuickSorting(array, i, rightIndex);
            return array;
        }
    }
}
