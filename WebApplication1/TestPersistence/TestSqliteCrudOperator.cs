using Moq;
using NUnit.Framework;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestPersistence
{
    [TestFixture]
    public class TestSqliteCrudOperator
    {
        private Mock<ICrudOperator> _mock;
        [SetUp]
        public void SetUp()
        {
            _mock = new Mock<ICrudOperator>();
        }
        [TearDown]
        public void TearDown()
        {
            _mock = null;
        }


        
    }
}
