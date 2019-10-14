using DataMapper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence
{
    public interface ICrudOperator
    {
        BookingSavedStatus Save(VehichleDataDb data);
        int GetOccupiedParking(string vehichleType);
        int Load();
        void UpdateAndGetData(double duration, double amount, DateTime dateTime, int parkingId);
        Dictionary<string, string> GetParkingEntryDateTime(int parkingId);
    }
}
