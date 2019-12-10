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

        public PlannerCombine(double ftlRate, double ltlRate, int pallets)
        {
            ftlTrucks = 0;
            ltlPallets = 0;
            int ftlMin = 0;
            while ((ltlRate * ftlMin) < ftlRate)
            {
                ftlMin++;
            }
            ftlMin--;

            while (pallets > 0)
            {
                if (pallets >= ftlMin)
                {
                    if (pallets <= ftlMax)
                    {
                        ftlTrucks++;
                        pallets = 0;
                    }
                    else
                    {
                        ftlTrucks++;
                        pallets -= ftlMax;
                    }
                }
                else if (pallets <= ftlMax)
                {
                    ltlPallets += pallets;
                    pallets = 0;
                }
                else
                {
                    ltlPallets += ftlMax;
                    pallets -= ftlMax;
                }
            }
        }
    }

}
