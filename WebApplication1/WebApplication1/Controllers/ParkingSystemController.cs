using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DataMapper.Models;
using BusinessLogic;

namespace WebApplication1.Controllers
{

    //[System.Web.Http.Cors.EnableCors(origins: "http://localhost:3000", headers: "*", methods: "*")]    
    public class ParkingSystemController : ApiController
    {
        private IParkingSystem _parkingSystem = ParkingSystem.ParkingSystemInstance;
        public LiveParkingStatusData[] Get()
        {            
            return _parkingSystem != null ? _parkingSystem.GetLiveParkingStatus():
                ParkingSystem.ParkingSystemInstance.GetLiveParkingStatus();            
        }
       
        public ExitParkingData Get(int id)
        {
            ExitParkingData exitParkingData = _parkingSystem != null ? _parkingSystem.ExitParking(id) :
                ParkingSystem.ParkingSystemInstance.ExitParking(id);
            return exitParkingData;
        }


        public BookingSavedStatus Post([FromBody]VehichleData data)
        {
            BookingSavedStatus Status = _parkingSystem != null ? _parkingSystem.SaveNewParkingData(data) :
                    ParkingSystem.ParkingSystemInstance.SaveNewParkingData(data);
            return Status;
        }     
    }
}
