using DataMapper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data;
using ApplicationLogger;
using System.Configuration;
using DataMapper;

namespace Persistence
{
    public class SqliteCrudOperator : ICrudOperator, IDisposable
    {
        private ILogger _logger;
        private bool _disposed;
        private string _connectionString;
        private string _logPath;

        private const string INSERT_FULL_FORM_DATA = 
            "INSERT INTO T_PARKING_DATA(VehichleNumber,VehichleWeight,VehichleType,parkingLotId,customerName,contactNumber,entryDateTime)" +
                "VALUES('{0}',{1},'{2}','{3}','{4}','{5}','{6}')";

        private const string GET_OCCUPIED_PARKING_COUNT = 
            "select Count(*) from T_PARKING_DATA where entryDateTime IS NOT NUll and exitDateTime IS NUll";

        private const string GET_PARKING_DETAILS_BY_VEHICHLE_NUMBER_AND_DATE =
            "select parkingId, parkingLotId, entryDateTime, customerName, VehichleNumber from T_PARKING_DATA where VehichleNumber = '{0}' and exitDateTime IS NULL";

        private const string GET_PARKING_ENTRY_DETAILS =
            "select entryDateTime, VehichleType, parkingLotId from T_PARKING_DATA where parkingId = {0}";

        private const string UPDATE_TABLE = 
            "Update T_PARKING_DATA SET amount = {0}, duration = '{1}', exitDateTime = '{2}' where parkingId = {3}";

       
      
        public SqliteCrudOperator(ILogger logger)
        {
            _connectionString = ConfigurationManager.AppSettings[Utils.SqliteConnectionString];
            _logPath = ConfigurationManager.AppSettings[Utils.LogPath];
            _logger = logger;

        }

        #region Interface Implementation
        public BookingSavedStatus Save(VehichleDataDb data)
        {
            DateTime dateTime = System.DateTime.Now;

            string query = String.Format(INSERT_FULL_FORM_DATA,
                data.VehichleNumber, data.VehichleWeight, data.VehichleType, data.ParkingLotId, data.CustomerFullName,
                data.ContactNumber, dateTime.ToString("yyyy-MM-dd HH:MM"));

            BookingSavedStatus bookingSavedStatus = new BookingSavedStatus();
            try
            {
                ExecuteQuerry(query);
                bookingSavedStatus.IsSaved = true;
            }
            catch (Exception ex)
            {
                _logger.WriteLogs(string.Format(_logPath + "{0}\\PerchedPeackockLogs.txt", Environment.UserName), ex.Message);
                bookingSavedStatus.IsSaved = false;
                return bookingSavedStatus;
            }

            ParkingReceipt parkingReceipt = PrepareParkingReceipt(new string[] { data.VehichleNumber, data.entryDateTime });
            bookingSavedStatus.ParkingReceipt = parkingReceipt;
            return bookingSavedStatus;
        }

        public int GetOccupiedParking(string vehichleType)
        {
            int occupiedParking = 0;
            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(_connectionString))
                {
                    connection.Open();
                    using (SQLiteCommand command = connection.CreateCommand())
                    {
                        command.CommandText = string.Format(GET_OCCUPIED_PARKING_COUNT + " and vehichleType= '{0}'", vehichleType);
                        SQLiteDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            occupiedParking = Int32.Parse(reader[0].ToString());
                        }

                        connection.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.WriteLogs(string.Format(_logPath + "{0}\\PerchedPeackockLogs.txt", Environment.UserName), ex.Message);
                return occupiedParking;
            }
            return occupiedParking;
        }

        public int Load()
        {
            throw new NotImplementedException();
        }

        public void UpdateAndGetData(double duration, double amount, DateTime dateTime, int parkingId)
        {
            string query = string.Format(UPDATE_TABLE, amount, duration, dateTime.ToString("yyyy-MM-dd HH:MM"), parkingId);
            ExecuteQuerry(query);            
        }

        public Dictionary<string, string> GetParkingEntryDateTime(int parkingId)
        {
            string query = string.Format(GET_PARKING_ENTRY_DETAILS, parkingId);
            Dictionary<string, string> parkingData = new Dictionary<string, string>();
            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(_connectionString))
                {
                    connection.Open();
                    using (SQLiteCommand command = connection.CreateCommand())
                    {
                        command.CommandText = query;
                        SQLiteDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            parkingData.Add(Utils.EntryDateTime, reader[Utils.EntryDateTime].ToString());
                            parkingData.Add(Utils.VehichleType, reader[Utils.VehichleType].ToString());
                            parkingData.Add(Utils.ParkingLotId, reader[Utils.ParkingLotId].ToString());
                        }
                        connection.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.WriteLogs(string.Format(_logPath + "{0}\\PerchedPeackockLogs.txt", Environment.UserName), ex.Message);
                return parkingData;
            }
            return parkingData;
        }

        #endregion Interface Implementation

        #region private methods
        private void ExecuteQuerry(string querry)
        {
            using (SQLiteConnection context = new SQLiteConnection(_connectionString))
            {
                context.Open();
                using (SQLiteCommand command = context.CreateCommand())
                {
                    try
                    {
                        command.CommandText = querry;
                        command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
                context.Close();
            }
        }

        private ParkingReceipt PrepareParkingReceipt(string[] whereClause)
        {
            ParkingReceipt parkingReceipt = new ParkingReceipt();
            string query = string.Format(GET_PARKING_DETAILS_BY_VEHICHLE_NUMBER_AND_DATE, whereClause[0]);
            using (SQLiteConnection connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                using (SQLiteCommand command = connection.CreateCommand())
                {
                    command.CommandText = query;
                    SQLiteDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        parkingReceipt.Name = reader["customerName"].ToString();
                        parkingReceipt.ParkingId = reader["parkingId"].ToString();
                        parkingReceipt.ParkingLotId = reader["parkingLotId"].ToString();
                        parkingReceipt.EntryDateTime = reader["entryDateTime"].ToString();
                        parkingReceipt.VehichleNumber = reader["VehichleNumber"].ToString();
                    }
                    connection.Close();
                }
            }
            return parkingReceipt;
        }
        private Dictionary<string, int> OccupiedParkingSpaces = new Dictionary<string, int>();       

        #endregion private methods

        public void Dispose()
        {
            if (!_disposed)
            {
                Dispose(true);
            }
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _disposed = true;
            }
        }
    }
}
