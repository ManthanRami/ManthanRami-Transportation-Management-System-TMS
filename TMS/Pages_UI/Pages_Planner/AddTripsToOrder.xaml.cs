using System;
using System.Collections.Generic;
using System.Data;
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
    /// <summary>
    /// Interaction logic for AddTripsToOrder.xaml
    /// </summary>
    public partial class AddTripsToOrder : Page
    {
        List<Carrier> carrierList = new List<Carrier>();
        Carrier carrier = new Carrier();
        public uint cID = 0;
        Carrier checkCarrier = new Carrier();
        TmsDal tms = new TmsDal();
        DataSet ds = new DataSet();

        public AddTripsToOrder()
        {
            InitializeComponent();
            listOfCarriers.IsEnabled = false;
        }


        private void SelectContract(object sender, SelectionChangedEventArgs e)
        {
            // Populates text boxes with current information except for the Carrier, which will be selected from a dropdown list of valid carriers
            List<Carrier> validCarriers = new List<Carrier>();

            DataGrid gd = (DataGrid)sender;
            dynamic rowView = gd.SelectedItem;
            if (rowView != null)
            {
                txtContractID.Text = rowView.ContractID.ToString();
                txtClientName.Text = rowView.Customer.Name;
                txtJobType.Text = rowView.JobType.ToString();
                txtQuantity.Text = rowView.Quantity.ToString();
                txtOrigin.Text = rowView.Origin.ToString();
                txtDestination.Text = rowView.Destination.ToString();
            }

            TMS.Data.City originCity = (TMS.Data.City)Enum.Parse(typeof(TMS.Data.City), txtOrigin.Text.ToString());

            validCarriers = tms.GetCarriersByCity(originCity);

            listOfCarriers.ItemsSource = validCarriers;
            listOfCarriers.IsEnabled = true;
        }

        private void btnLoadOrders_Click(object sender, RoutedEventArgs e)
        {
            List<Contract> contracts = new List<Contract>();
            Status status = Status.PENDING;

            contracts = tms.GetContractsByStatus(status);
            orderData.ItemsSource = contracts;
        }

        private void btnAdd_Trip_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
