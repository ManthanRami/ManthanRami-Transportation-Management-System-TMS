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
using System.Windows.Shapes;
using TMS.Pages_UI.Pages_Buyer;

namespace TMS
{
    /// <summary>
    /// Interaction logic for BuyerWindow.xaml
    /// </summary>
    public partial class BuyerWindow : Window
    {
        public BuyerWindow()
        {
            InitializeComponent();
        }

        private void BuyerLogout_Click(object sender, RoutedEventArgs e)
        {
            MainWindow login = new MainWindow();
            login.Show();
            this.Close();
        }

        private void Marketplace_Click(object sender, RoutedEventArgs e)
        {
            BuyerMain.Content = new ContractMarketplace();
        }

        private void Cust_Management_Click(object sender, RoutedEventArgs e)
        {
            BuyerMain.Content = new CustomerManagement();
        }
        private void New_Order_Click(object sender, RoutedEventArgs e)
        {
            BuyerMain.Content = new InitiateNewOrder();
        }

        private void Completed_Orders_Click(object sender, RoutedEventArgs e)
        {
            BuyerMain.Content = new CompletedOrders();
        }
    }
}
