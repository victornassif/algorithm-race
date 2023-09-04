using Microsoft.Azure.Functions.Worker;
using sort_functions.Models;

namespace sort_functions.ActivitiesFunctions
{
    public static class BucketSort
    {
        [Function(nameof(BucketSorting))]
        public static int[] BucketSorting([ActivityTrigger] ParamModel model)
        {
            var array = model.nonSortedArray;

            if (array == null)
                throw new ArgumentNullException("null array");
            else if (array.Length == 0)
                throw new ArgumentException("empty array");
            //else
            //    foreach (var item in array)
            //    {
            //        Console.Write(item);
            //    }
            try
            {


            if (array == null || array.Length <= 1)
            {
                return array;
            }
            int maxValue = array[0];
            int minValue = array[0];
            for (int i = 1; i < array.Length; i++)
            {
                if (array[i] > maxValue)
                {
                    maxValue = array[i];
                }
                if (array[i] < minValue)
                {
                    minValue = array[i];
                }
            }
            LinkedList<int>[] bucket = new LinkedList<int>[maxValue - minValue + 1];
            for (int i = 0; i < array.Length; i++)
            {
                if (bucket[array[i] - minValue] == null)
                {
                    bucket[array[i] - minValue] = new LinkedList<int>();
                }
                bucket[array[i] - minValue].AddLast(array[i]);
            }
            var index = 0;

            for (int i = 0; i < bucket.Length; i++)
            {
                if (bucket[i] != null)
                {
                    LinkedListNode<int> node = bucket[i].First;
                    while (node != null)
                    {
                        array[index] = node.Value;
                        node = node.Next;
                        index++;
                    }
                }
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
