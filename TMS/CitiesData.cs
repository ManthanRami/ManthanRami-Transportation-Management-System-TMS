//File          : CitiesData.cs
//Project       : TMS Software Quality Project
//Course        : SENG2020 Software Quality
//Programmer    : David Obeda
//ID            : 8031148
//Summary       : CityClass to hold data for trip logic

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
    public class CitiesData
    {
        public readonly int CityName;
        public readonly int EastKM;
        public readonly int EastMinutes;

        /// <summary>
        /// The City class is used to store the information for the city network.
        /// This will allow for the TMS system to determine both time and cost of carriers
        /// </summary>
        /// <param name="CityN">Int index of the current city</param>
        // <param name="WestName">Name of city to the west</param>
        // <param name="EastName">Name of city to the east</param>
        // <param name="WestDis">Distance in kilometers to the western city</param>
        /// <param name="EastDis">Distance in kilometers to the eastern city</param>
        // <param name="WestTime">Time in hours travel to the western city</param>
        /// <param name="EastTime">Time in hours to travel to the eatern city</param>
        public CitiesData(int CityN, int EastDis, int EastTime)
        {
            CityName = CityN;
            EastKM = EastDis;
            EastMinutes = EastTime;
        }
    }
}
