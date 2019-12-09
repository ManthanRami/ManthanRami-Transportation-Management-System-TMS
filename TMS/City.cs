using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMS
{
    /// <summary>
    /// The City class is used to store the information for the city network.
    /// This will allow for the TMS system to determine both time and cost of carriers
    /// </summary>
    public class City
    {
        public readonly string CityName;
        //readonly string WestCity;
        //readonly string EastCity;
        //readonly int WestKM;
        public readonly int EastKM;
        //readonly int WestMinutes;
        public readonly int EastMinutes;

        /// <summary>
        /// The City class is used to store the information for the city network.
        /// This will allow for the TMS system to determine both time and cost of carriers
        /// </summary>
        /// <param name="CityN">Name of the current city</param>
        // <param name="WestName">Name of city to the west</param>
        // <param name="EastName">Name of city to the east</param>
        // <param name="WestDis">Distance in kilometers to the western city</param>
        /// <param name="EastDis">Distance in kilometers to the eastern city</param>
        // <param name="WestTime">Time in hours travel to the western city</param>
        /// <param name="EastTime">Time in hours to travel to the eatern city</param>
        public City(string CityN, int EastDis, int EastTime)
        {
            CityName = CityN;
            //WestCity = WestName;
            //EastCity = EastName;
            //WestKM = WestDis;
            EastKM = EastDis;
            //WestMinutes = WestTime;
            EastMinutes = EastTime;
        }
    }
}
