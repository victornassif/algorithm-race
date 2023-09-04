using Microsoft.Azure.Functions.Worker;
using sort_functions.Models;
using System.Diagnostics;

namespace sort_functions.ActivitiesFunctions
{
    public static class BucketSort
    {
        [Function(nameof(BucketSorting))]
        public static WinnerModel BucketSorting([ActivityTrigger] ParamModel model)
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
                timer.Stop();
                return new WinnerModel(array, "BucketSort", timer.Elapsed);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
