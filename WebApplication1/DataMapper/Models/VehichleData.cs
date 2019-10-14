using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataMapper.Models
{
    [Serializable]
    public class VehichleData
    {   
        public string VehichleNumber { get; set; }
        public double VehichleWeight { get; set; }
        public string vehichleType { get; set; }      
        public string CustomerFullName { get; set; }
        public string ContactNumber { get; set; }
        public string EntryDateTime { get; set; }
    }
}