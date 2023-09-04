using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace sort_functions.Models
{
    public class WinnerModel
    {
        public TimeSpan timeElapsed { get; set; }
        public int[] sortedArray { get; set; } = new int[0];
        public string winnerName { get; set; } = "";

        public WinnerModel() { }
        public WinnerModel(int[] _sortedArray, string _winnerName, TimeSpan _timeElapsed)
        {
            winnerName = _winnerName;
            timeElapsed = _timeElapsed;
            sortedArray = _sortedArray;
        }
    }
}
