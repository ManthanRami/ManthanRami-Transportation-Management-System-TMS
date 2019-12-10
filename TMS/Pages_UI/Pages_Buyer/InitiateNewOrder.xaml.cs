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
        TmsDal tms = new TmsDal();
        Contract contract = new Contract();
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
                ClientName.Text = rowView.Customer.Name;
                JobType.Text = rowView.JobType.ToString();
                Quantity.Text = rowView.Quantity.ToString();
                txtOriginCity.Text = rowView.Origin.ToString();
                txtDestinationCity.Text = rowView.Destination.ToString();
                vanType.Text = rowView.VanType.ToString();
            }
        }

        private void CreateOrder_Click(object sender, RoutedEventArgs e)
        {

            if(!string.IsNullOrEmpty(ClientName.Text)||!string.IsNullOrEmpty(txtDestinationCity.Text)||!string.IsNullOrEmpty(txtOriginCity.Text)||!string.IsNullOrEmpty(JobType.Text)||!string.IsNullOrEmpty(vanType.Text))
            {
                TMS.Data.City city = (TMS.Data.City)Enum.Parse(typeof(TMS.Data.City), txtDestinationCity.Text);
                contract.Destination = city;
                contract.Customer = new Customer();
                contract.Customer.Name = ClientName.Text;
                tms.CreateCustomer(contract.Customer);
                city = (TMS.Data.City)Enum.Parse(typeof(TMS.Data.City), txtOriginCity.Text);
                contract.Origin = city;
                JobType job = (JobType)Enum.Parse(typeof(JobType), JobType.Text);
                contract.JobType = job;
                VanType van = (VanType)Enum.Parse(typeof(VanType), vanType.Text);
                contract.VanType = van;
                contract.Quantity = Convert.ToInt32(Quantity.Text);
                TMS.Data.Status status = (TMS.Data.Status)Enum.Parse(typeof(TMS.Data.Status), "1");
                contract.Status = status;
                tms.CreateContract(contract);
                MessageBox.Show("Order Started", "Done", MessageBoxButton.OK, MessageBoxImage.Information);
                MakeFileEmpty();
            }
            else
            {
                MessageBox.Show("Please Select an order first !!", "Empty Selection", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        
        public void MakeFileEmpty()
        {
            ClientName.Text = "";
            txtDestinationCity.Text = "";
            txtOriginCity.Text = "";
            Quantity.Text = "";
            vanType.Text = "";
            JobType.Text = "";

        }
    }
}
