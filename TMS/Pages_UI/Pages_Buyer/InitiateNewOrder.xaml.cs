using System;
using System.Collections.Generic;
using System.Linq;
using TMS.Data;
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
using System.Data;

namespace TMS.Pages_UI.Pages_Buyer
{
    /// <summary>
    /// Interaction logic for InitiateNewOrder.xaml
    /// </summary>
    public partial class InitiateNewOrder : Page
    {
        CmpDal cmp;
        List<Contract> crt;
        public InitiateNewOrder()
        {
            InitializeComponent();
        }

        private void GetOrder_Click(object sender, RoutedEventArgs e)
        {
            cmp = new CmpDal();
            crt = new List<Contract>();
            crt = cmp.GetContracts();
            contractData.ItemsSource = crt;
        }

        private void contractData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid gd = (DataGrid)sender;
            dynamic rowView = gd.SelectedItem;           
            if (rowView!=null)
            {
                
                ClientName.Text = rowView.Client;
                JobType.Text = rowView.JobType.ToString();
                Quantity.Text = rowView.Quantity.ToString();
                txtOriginCity.Text = rowView.Origin.ToString();
                txtDestinationCity.Text = rowView.Destination.ToString();
                vanType.Text = rowView.VanType.ToString();
            }
        }

    }
}
