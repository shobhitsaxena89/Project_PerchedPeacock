using DataMapper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public interface IParkingSystem
    {
        BookingSavedStatus SaveNewParkingData(VehichleData vehichleData);
        LiveParkingStatusData[] GetLiveParkingStatus();
        ExitParkingData ExitParking(int parkingLotId);

    }
}
