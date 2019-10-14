using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMapper.Models
{
    [Serializable]
    public class ParkingReceipt
    {
        public string ParkingId { get; set; }
        public string ParkingLotId { get; set; }
        public string EntryDateTime { get; set; }
        public string Name { get; set; }
        public string VehichleNumber { get; set; }
    }
}
