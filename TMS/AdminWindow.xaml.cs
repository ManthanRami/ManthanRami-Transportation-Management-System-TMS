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
using TMS.Pages_UI.Pages_Admin;
using TMS.Pages_UI.Pages_Buyer;
using TMS.Pages_UI.Pages_Planner;

namespace TMS
{
    /// <summary>
    /// Interaction logic for AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        public AdminWindow()
        {
            InitializeComponent();
        }


        private void AdminLogout_Click(object sender, RoutedEventArgs e)
        {
            MainWindow login = new MainWindow();
            login.Show();
            this.Close();
        }


        private void Review_Click(object sender, RoutedEventArgs e)
        {
            AdminMain.Content = new ReviewLogFiles();
        }


        private void LogDirectory_Click(object sender, RoutedEventArgs e)
        {
            AdminMain.Content = new LogDirectoryOptions();
        }


        private void DBMS_Config_Click(object sender, RoutedEventArgs e)
        {
            AdminMain.Content = new DBMSConfig();
        }


        private void Backup_Click(object sender, RoutedEventArgs e)
        {
            AdminMain.Content = new InitiateBackup();
        }


        private void CarrierData_Click(object sender, RoutedEventArgs e)
        {
            AdminMain.Content = new ModifyCarrierData();
        }


        private void RateData_Click(object sender, RoutedEventArgs e)
        {
            AdminMain.Content = new ModifyRateData();
        }


        private void FeeData_Click(object sender, RoutedEventArgs e)
        {
            AdminMain.Content = new ModifyFeeData();
        }


        private void RouteData_Click(object sender, RoutedEventArgs e)
        {
            AdminMain.Content = new ModifyRouteData();
        }

    }
}
