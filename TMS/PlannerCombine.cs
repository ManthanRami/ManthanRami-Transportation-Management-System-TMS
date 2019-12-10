using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMS.Data;

namespace TMS
{
    public class PlannerCombine
    {
        static int ftlMax = 26; //only 26 skids on a truck
        public int ftlTrucks;
        public int ltlPallets;

        public PlannerCombine(float ftlRate, float ltlRate, int distance,int pallets)
        {
            ftlTrucks = 0;
            ltlPallets = 0;
            int ftlMin = 0;
            while ((ltlRate * ftlMin) < ftlRate)
            {
                ftlMin++;
            }
            ftlMin--;


        }
    }

}
