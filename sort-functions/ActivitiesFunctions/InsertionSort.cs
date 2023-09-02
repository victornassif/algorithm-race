using Microsoft.Azure.Functions.Worker;

namespace sort_functions.ActivitiesFunctions
{
    public static class InsertionSort
    {
        [Function(nameof(InsertionSorting))]
        public static int[] InsertionSorting([ActivityTrigger] int[] array, int leftIndex, int rightIndex)
        {
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
                InsertionSorting(array, leftIndex, j);
            if (i < rightIndex)
                InsertionSorting(array, i, rightIndex);
            return array;
        }
    }
}
