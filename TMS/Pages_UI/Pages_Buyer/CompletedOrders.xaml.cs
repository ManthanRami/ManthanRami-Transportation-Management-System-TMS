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

namespace TMS.Pages_UI.Pages_Buyer
{
    /// <summary>
    /// Interaction logic for CompletedOrders.xaml
    /// </summary>
    public partial class CompletedOrders : Page
    {
        public CompletedOrders()
        {
            InitializeComponent();
        }


        public List<Contract> GetContractsByStatus()
        {
            List<Contract> theList = new List<Contract>();

            return theList;
        }

        private void completeOrders_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btnGenerateInvoice_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnCompleteOrders_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
