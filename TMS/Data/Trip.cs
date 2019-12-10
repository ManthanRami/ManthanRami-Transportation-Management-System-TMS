using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xaml;

namespace TMS.Data
{
    /// <summary>
    /// This class models a possible trip stored within the database
    /// </summary>
    public class Trip
    {
        public uint TripID { get; set; }
        public City Destination { get; set; }
        public City West { get; set; }
        public City East { get; set; }
        public int TravelTime { get; set; }
        public int Distance { get; set; }
    }
}
