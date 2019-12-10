using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TMS.Data;

namespace TMS.Pages_UI.Pages_Planner
{
    //=======================================================================================================================
    /// <summary>
    /// This is the Active Orders page for the Planner. They will be provided with an overview off all the currently active
    /// orders.
    /// </summary>
    //=======================================================================================================================
    public partial class ViewActiveOrders : Page
    {
        List<Contract> lc = new List<Contract>();
        TmsDal td = new TmsDal();
        static CitiesData[] listOfCities = GetCityData();
        const int MAX_LOAD = 26;
        Dictionary<uint, List<TripLogic>> contractTripDictionary = new Dictionary<uint, List<TripLogic>>();
        
        public ViewActiveOrders()
        {
            InitializeComponent();
            LoadActiveOrders();
            GetCityData();
            contractTrip(listOfCities);
        }


        private void LoadActiveOrders()
        {
            lc = td.GetContractsByStatus(Status.STARTED);
            
            ActiveOrderData.ItemsSource = lc;
        }

        private void btnSimulate_Time_Click(object sender, RoutedEventArgs e)
        {
            foreach (int id in contractTripDictionary.Keys)
            {
                bool complete = true;
                foreach (TripLogic tl in contractTripDictionary[(uint)id])
                {
                    tl.AddTime(listOfCities);

                    if (tl.direction != 0)
                    {
                        complete = false;
                    }
                    
                }

                td.SetContractStatus((uint)id, Status.DELIVERED);
            }
            LoadActiveOrders();
        }


        static private CitiesData[] GetCityData()
        {
            TmsDal CityData = new TmsDal();
            List<Trip> CityInfo = CityData.GetTrips();
            CitiesData[] CityList = new CitiesData[CityInfo.Count];
            int indexNum = 0;
            foreach (Trip City in CityInfo)
            {
                CityList[indexNum] = new CitiesData(indexNum, City.Distance, City.TravelTime);
                indexNum++;
            }

            return CityList;
        }


        public Dictionary<uint,List<TripLogic>> contractTrip(CitiesData[] cities)
        {

            int ftlp = 0;
            int ltlp = 0;


            foreach (Contract c in lc)
            {
                List<TripLogic> tripLogics = new List<TripLogic>();

                if (c.Quantity == 0)
                {
                    TripLogic logic = new TripLogic(c.Quantity, (int)c.Origin, (int)c.Destination, listOfCities);
                    tripLogics.Add(logic);
                }
                else
                {
                    PlannerCombine combine = new PlannerCombine(c.Carrier.FTLRate, c.Carrier.LTLRate, c.Quantity);
                    while ((ftlp > 0) || (ltlp > 0))
                    {
                        if (ftlp > 0)
                        {
                            TripLogic newLogic = new TripLogic(0, (int)c.Origin, (int)c.Destination, listOfCities);
                            tripLogics.Add(newLogic);
                            ftlp--;
                        }
                        else
                        {
                            if (ltlp > MAX_LOAD)
                            {
                                TripLogic moreLogic = new TripLogic(MAX_LOAD, (int)c.Origin, (int)c.Destination, listOfCities);
                                tripLogics.Add(moreLogic);
                                ltlp -= MAX_LOAD;
                            }
                            else
                            {
                                TripLogic moreLogic = new TripLogic(ltlp, (int)c.Origin, (int)c.Destination, listOfCities);
                                tripLogics.Add(moreLogic);
                                ltlp = 0;
                            }
                        }
                    }

                    contractTripDictionary.Add(c.ContractID, tripLogics);

                }

                ftlp = 0;
                ltlp = 0;
            }

            return contractTripDictionary;
        }

        private void btnSimulate_Time_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void btnToggleComplete_Click(object sender, RoutedEventArgs e)
        {
            td.SetContractStatus(uint.Parse(ActiveOrderData.SelectedCells[1].ToString()), Status.FINISHED);
            LoadActiveOrders();
        }
    }
}
