using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMapper.Models
{
    [Serializable]
    public class ExitParkingData
    {
        public string EntryDateTime { get; set; }
        public string ExitDateTime { get; set; }
        public string Amount { get; set; }
        public string Duration { get; set; }
    }
}
