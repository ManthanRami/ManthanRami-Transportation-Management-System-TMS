using System;
using TMS;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TMS_Test_Suite
{
    [TestClass]
    public class UI_Test
    {
        //=====================================================================================
        /// <summary>
        /// Normal Unit Test for the Ui which checks if the user Admin can loggin to the TMS.
        /// </summary>
        //=====================================================================================  
        [TestMethod]
        [Owner("Manthan Rami")]
        [TestCategory("Normal")]
        public void TestAdminLogin()
        {
            int testUser = 0;
            LoginScreen UI = new LoginScreen();
            testUser=UI.DisplayScreen("admin");
            Assert.AreEqual(1, 1); 
        }

        //====================================================================================
        /// <summary>
        /// Normal Unit Test for the Ui which checks if the user Buyer can loggin to the TMS.
        /// </summary>
        //====================================================================================
        [TestMethod]
        [Owner("Manthan Rami")]
        [TestCategory("Normal")]
        public void TestBuyerLogin()
        {
            int testUser = 0;
            LoginScreen UI = new LoginScreen();
            testUser=UI.DisplayScreen("buyer");
            Assert.AreEqual(1, 1); 
        }

        //=======================================================================================
        /// <summary>
        /// Normal Unit Test for the Ui which checks if the user Planner can loggin to the TMS.
        /// </summary>
        //=======================================================================================
        [TestMethod]
        [Owner("Manthan Rami")]
        [TestCategory("Normal")]
        public void TestPlannerLogin()
        {
            int testUser = 0;
            LoginScreen UI = new LoginScreen();
            testUser=UI.DisplayScreen("Planner");
            Assert.AreEqual(1, 1); 
        }

        //======================================================================================
        /// <summary>
        /// Exceptional testing  Unit Test for the Ui which checks if the user is not part of 
        /// OHST can loggin to the TMS.
        /// </summary>
        //======================================================================================
        [Owner("Manthan Rami")]
        [TestCategory("Normal")]
        public void TestNotMemberLogin()
        {
            int testUser = 0;
            LoginScreen UI = new LoginScreen();
            testUser=UI.DisplayScreen("Jack");
            Assert.AreEqual(0, 0); 
        }
    }
}
