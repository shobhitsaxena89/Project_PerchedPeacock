using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using BusinessLogic;
using DataMapper.Models;
using NUnit.Framework;

namespace TestBusinessLogic
{
    [TestFixture]
    public class TestParkingSystem
    {
        private Mock<IParkingSystem> _mock;
        [SetUp]
        public void SetUp()
        {
            _mock = new Mock<IParkingSystem>();
        }
        [TearDown]
        public void TearDown()
        {
            _mock = null;
        }
        
        [Test]
        public void TestSaveNewParkingData()
        {
             VehichleData vd = GetVehichleData();
             _mock.Setup(i => i.SaveNewParkingData(vd));
             _mock.Verify((m => m.SaveNewParkingData(vd)), Times.Once());
            

             IParkingSystem ps = ParkingSystem.ParkingSystemInstance;
             ps.SaveNewParkingData(vd);

        }
        [Test]        
        public void TestSaveNewParkingData_WithNullFields_MustFail()
        {
            VehichleData vd = new VehichleData();
            _mock.Setup(i => i.SaveNewParkingData(vd));
            _mock.Verify((m => m.SaveNewParkingData(vd)), Times.Once());

            IParkingSystem ps = ParkingSystem.ParkingSystemInstance;
            ps.SaveNewParkingData(vd);
            Assert.That(() => ps.SaveNewParkingData(vd), Throws.TypeOf<ArgumentNullException>());           

        }

        [Test]
        public void TestGetLiveParkingStatus()
        {
            VehichleData vd = GetVehichleData();
            LiveParkingStatusData data = new LiveParkingStatusData();
            data.Count = 2;
            data.ParkingType = "2W";
            _mock.Setup(i => i.GetLiveParkingStatus()).Returns(new LiveParkingStatusData[] {data});
            _mock.Verify((m => m.GetLiveParkingStatus()), Times.Once());

            
            IParkingSystem ps = ParkingSystem.ParkingSystemInstance;
            ps.GetLiveParkingStatus();           

        }

        [Test]
        public void TestExitParking()
        {
            VehichleData vd = GetVehichleData();           

        }

        private static VehichleData GetVehichleData()
        {
            VehichleData vd = new VehichleData();
            vd.ContactNumber = "8884896424";
            vd.CustomerFullName = "SomeCustomer";
            vd.EntryDateTime = "CurrentDateTime";
            vd.VehichleNumber = "SomeVehichleNumber";
            vd.vehichleType = "2W";
            vd.VehichleWeight = 100d;
            return vd;
        }
    }
}
