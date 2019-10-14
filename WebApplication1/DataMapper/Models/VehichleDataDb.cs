using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMapper.Models
{
    [Serializable]
    public class VehichleDataDb
    {
        public string VehichleNumber { get; set; }
        public double VehichleWeight { get; set; }
        public string VehichleType { get; set; }
        public string ParkingLotId { get; set; }
        public string CustomerFullName { get; set; }
        public string ContactNumber { get; set; }      
        public string entryDateTime { get; set; }

    }
}
