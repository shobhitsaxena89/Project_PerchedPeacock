using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMapper.Models
{
    public class BookingSavedStatus
    {
        public bool IsSaved { get; set; }
        public ParkingReceipt ParkingReceipt { get; set; }
    }
}
