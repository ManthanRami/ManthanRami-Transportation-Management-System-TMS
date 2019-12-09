using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TMS;
using TMS.Data;

namespace TMS_Test_Suite
{
    [TestClass]
    public class TripsTest
    {
        CitiesData[] CityList;
        public TripsTest()
        {
            TmsDal daldavidsucks = new TmsDal();
            List<Trip> CityInfo = daldavidsucks.GetTrips();
            CitiesData[] CityList = null;
            foreach (Trip City in CityInfo)
            {
                CityList[(int)((City.TripID) - 1)] = new CitiesData((int)((City.TripID) - 1), City.Distance, City.TravelTime);
            }
        }
        [TestMethod]
        public void CityListValid(CitiesData[] CityList)
        {
            //Validate KMs
            Assert.AreEqual(CityList[0].EastKM, 191);
            Assert.AreEqual(CityList[1].EastKM, 128);
            Assert.AreEqual(CityList[2].EastKM, 68);
            Assert.AreEqual(CityList[3].EastKM, 60);
            Assert.AreEqual(CityList[4].EastKM, 134);
            Assert.AreEqual(CityList[5].EastKM, 82);
            Assert.AreEqual(CityList[6].EastKM, 196);

            //Validate Time
            Assert.AreEqual(CityList[0].EastMinutes, 150);
            Assert.AreEqual(CityList[0].EastMinutes, 105);
            Assert.AreEqual(CityList[0].EastMinutes, 75);
            Assert.AreEqual(CityList[0].EastMinutes, 78);
            Assert.AreEqual(CityList[0].EastMinutes, 99);
            Assert.AreEqual(CityList[0].EastMinutes, 72);
            Assert.AreEqual(CityList[0].EastMinutes, 150);
        }
    }
}
