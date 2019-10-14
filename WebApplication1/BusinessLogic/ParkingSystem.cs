using DataMapper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Persistence;
using DataMapper;
using ApplicationLogger;
using System.Configuration;

namespace BusinessLogic
{
    public class ParkingSystem : IParkingSystem
    {
        private DbType _dbType;
        private IDbConnectorFactory _factory;
        private ICrudOperator _crudOperator;
        private DataMapper<VehichleData, VehichleDataDb> _dataMapperToDb;
        private ILogger _logger;
        private ILoggerFactory _loggerFactory;

        private static IParkingSystem _parkingSystem;
        private string _logPath;
        private ParkingSystem()
        {
            InitComponents();
        }
        Dictionary<string, Queue<int>> _availableParkingSpacesFloorWise;

        public Dictionary<string, Queue<int>> AvailableParkingSpacesFloorWise
        {
            get
            {
                return _availableParkingSpacesFloorWise;
            }
            set
            {
                _availableParkingSpacesFloorWise = value;
            }
        }
        private void InitComponents()
        {
            _loggerFactory = new LoggerFactoryImpl();
            _logger = _loggerFactory.GetLogger(LogType.FileBased);
            _dataMapperToDb = new DataMapper<VehichleData, VehichleDataDb>();
            _factory = new DbConnectorFactoryImpl(_logger);
            _logPath = ConfigurationManager.AppSettings[Utils.LogPath];

            string dbTypeStr = ConfigurationManager.AppSettings[Utils.Storage];            
            Enum.TryParse<DbType>(dbTypeStr, out _dbType);
            _crudOperator = _factory.GetDb(_dbType);

            string configuredParkingSpaces = ConfigurationManager.AppSettings[Utils.ParkingSpaces];
            string[] spaces = configuredParkingSpaces.Split('|');
            _availableParkingSpacesFloorWise = spaces.
            Select((s) =>
            {
                Queue<int> q = new Queue<int>();
                for (int i = 1; i <= Int32.Parse(s.Split('-')[1]); i++)
                {
                    q.Enqueue(i);
                }

                return new KeyValuePair<string, Queue<int>>(s.Split('-')[0], q);
            }).ToDictionary(x => x.Key, y => y.Value);
        }      

        public static IParkingSystem ParkingSystemInstance
        {
            
            get
            {                
                _parkingSystem = _parkingSystem ?? new ParkingSystem();
                return _parkingSystem;
            }
        }

        public BookingSavedStatus SaveNewParkingData(VehichleData vehichleData)
        {
            VehichleDataDb data =  _dataMapperToDb.Translate(vehichleData);
            data.ParkingLotId = GetParkingLotId();
            return _crudOperator.Save(data);
        }
        private string GetParkingLotId()
        {
            string parkingLotId = string.Empty;
            foreach (var item in AvailableParkingSpacesFloorWise)
            {
                if (item.Value.Count > 0)
                {
                    parkingLotId = item.Key + "-" + item.Value.Dequeue().ToString();
                    break;
                }
            }
            return parkingLotId;
        }

        public LiveParkingStatusData[] GetLiveParkingStatus()
        {
            List<LiveParkingStatusData> status = new List<LiveParkingStatusData>();
            int max4W = Int32.Parse(ConfigurationManager.AppSettings[Utils.MaxParkingLimit4W]);
            int max2W = Int32.Parse(ConfigurationManager.AppSettings[Utils.MaxParkingLimit2W]);

            int occupied2W = _crudOperator.GetOccupiedParking(Utils.ParkingType2W);
            int occupied4W = _crudOperator.GetOccupiedParking(Utils.ParkingType4W);

            status.Add(new LiveParkingStatusData() { ParkingType = "2 Wheeler", Count = max2W - occupied2W });
            status.Add(new LiveParkingStatusData() { ParkingType = "4 Wheeler", Count = max4W - occupied4W });
           return status.ToArray();
        }

        public ExitParkingData ExitParking(int parkingId)
        {
            ExitParkingData exitParkingData = new ExitParkingData();
            try
            {
                Dictionary<string, string> parkingDetails = _crudOperator.GetParkingEntryDateTime(parkingId);
                DateTime entry = DateTime.Parse(parkingDetails[Utils.EntryDateTime]);
                DateTime exit = System.DateTime.Now;

                double duration = CalculateDuration(entry, exit);
                double amount = CalculateAmountPayable(duration, parkingDetails[Utils.VehichleType]);
                string releasedParking = parkingDetails[Utils.ParkingLotId];
                
                _crudOperator.UpdateAndGetData(duration, amount, exit, parkingId);
                UpdateAvailableParking(releasedParking);

                exitParkingData.Amount = amount.ToString();
                exitParkingData.Duration = duration.ToString();
                exitParkingData.EntryDateTime = entry.ToString();
                exitParkingData.ExitDateTime = exit.ToString();
            }
            catch(Exception ex)
            {
                _logger.WriteLogs(string.Format(_logPath + "{0}\\PerchedPeackockLogs.txt", Environment.UserName), ex.Message);
                throw ex;
            }

            return exitParkingData;
        }

        private void UpdateAvailableParking(string parking)
        {
            string[] releasedParking = parking.Split('-');
            AvailableParkingSpacesFloorWise[releasedParking[0]].Enqueue(Int32.Parse(releasedParking[1]));
        }

        private double CalculateDuration(DateTime entry, DateTime exit)
        {
            var duration = exit - entry;
            return duration.TotalHours;
        }

        private double CalculateAmountPayable(double hours, string vehichleType)
        {
            double hourlyCharge ;
            Double.TryParse(vehichleType.Equals(Utils.ParkingType2W) 
                ? ConfigurationManager.AppSettings[Utils.HourlyFare2W] 
                : ConfigurationManager.AppSettings[Utils.HourlyFare4W] , out hourlyCharge);
            return hours * hourlyCharge;
        }
    }
}
