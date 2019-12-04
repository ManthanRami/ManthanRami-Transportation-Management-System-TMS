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

        private void INI_Click(object sender, RoutedEventArgs e)
        {
            cmp = new CmpDal();
            if(cmp!=null)
            {
                connectSignal.Fill = Brushes.LightGreen;
            }
        }

        private void Getorder_Click(object sender, RoutedEventArgs e)
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
                JoBType.Text = rowView.JobType.ToString();
                vanType.Text = rowView.VanType.ToString();
                Quantity.Text = rowView.Quantity.ToString();
            }
        }
    }
}
