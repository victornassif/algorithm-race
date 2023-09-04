using Microsoft.Azure.Functions.Worker;
using sort_functions.Models;

namespace sort_functions.ActivitiesFunctions
{
    public static class BubbleSort
    {
        [Function(nameof(BubbleSorting))]
        public static int[] BubbleSorting([ActivityTrigger] ParamModel model)
        {
            var array = model.nonSortedArray;

            if (array == null)
                throw new ArgumentNullException("null array");
            else if(array.Length == 0)
                throw new ArgumentException("empty array");
            //else
            //    foreach (var item in array)
            //    {
            //        Console.Write(item);
            //    }

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
            return array;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
