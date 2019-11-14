using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TMS.Data;

namespace TMS_Test_Suite
{
    [TestClass]
    public class CmpDalTest
    {
        [TestMethod]
        public void TestGetProducts()
        {
            CmpDal dal = new CmpDal();

            List<Contract> contracts = dal.GetContracts();

            Assert.IsTrue(contracts.Count > 0);
        }
    }
}
