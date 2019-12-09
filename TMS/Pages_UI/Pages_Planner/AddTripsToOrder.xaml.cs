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

        }

        private List<Carrier> GetCarriersByCity_Stub(TMS.Data.City city)
        {
            // Populates the list of carriers that are valid for the City provided
            List<Carrier> carriers = new List<Carrier>();



            return carriers;
        }

        private void SelectContract(object sender, SelectionChangedEventArgs e)
        {
            // Populates text boxes with current information except for the Carrier, which will be selected from a dropdown list of valid carriers
            DataGrid gd = (DataGrid)sender;
            dynamic rowView = gd.SelectedItem;
            if (rowView != null)
            {
                txtClientName.Text   = rowView.Customer.Name;
                txtJobType.Text      = rowView.JobType.ToString();
                txtQuantity.Text     = rowView.Quantity.ToString();
                txtOrigin.Text       = rowView.Origin.ToString();
                txtDestination.Text  = rowView.Destination.ToString();
            }
        }

        private void btnLoadOrders_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
