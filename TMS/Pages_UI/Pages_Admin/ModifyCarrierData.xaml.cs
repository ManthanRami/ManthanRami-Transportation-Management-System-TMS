/*
* FILE          : 	File Name
* PROJECT       : 	Course Code - Assignment Name
* PROGRAMMER    : 	Alex MacCumber - 8573909
* FIRST VERSION : 	Date Started YYYY-MM-DD
* DESCRIPTION   : 	Description of what this file does
*/
using TMS.Data;
using System.Windows;
using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Data;

namespace TMS.Pages_UI.Pages_Admin
{
    //=======================================================================================================================
    /// <summary>
    /// This is the page for the Admin that contains the options and input fields needed to allow them to modify the data
    /// for carriers.  Including: Adding new carriers, removing carriers, and modify existing carrier data.
    /// </summary>
    //=======================================================================================================================
    public partial class ModifyCarrierData : Page
    {
        Carrier carrier = new Carrier();
        Carrier checkCarrier = new Carrier();
        TmsDal tms = new TmsDal();
        DataSet ds = new DataSet();
        List<Carrier> list = new List<Carrier>();
        public ModifyCarrierData()
        {
            InitializeComponent();
            GetCarrierList();
            LoadCity();
            cityList.SelectedIndex = 0;
        }

        private void UpdateCarrier(object sender, RoutedEventArgs e)
        {
            carrier.Name = txtCarrier_ID.Text;
            City city = (City)Enum.Parse(typeof(City), txtDepot_City.Text);
            carrier.DepotCity = city;
            carrier.FtlAvailability = Convert.ToInt32(txtFTL_Avail.Text);
            carrier.LtlAvailability = Convert.ToInt32(txtLTL_Avail.Text);
            list = tms.SearchCarriers(carrier.Name, txtDepot_City.Text);
            if (list.Count != 0)
            {
                tms.UpdateCarrier(Convert.ToUInt32(CarrierList.SelectedIndex), carrier);
                MessageBox.Show("Carrier Updated Successfully ", "Done", MessageBoxButton.OK, MessageBoxImage.Information);
                GetCarrierList();
                MakeFieldEmpty();
            }
            else if (MessageBox.Show("Carrier is Not Exist !!\n Do you want to add ?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Error) == MessageBoxResult.Yes)
            {
                tms.CreateCarrier(carrier);
                MessageBox.Show("Carrier Added Successfully ", "Done", MessageBoxButton.OK, MessageBoxImage.Information);
                MakeFieldEmpty();
                GetCarrierList();
            }
        }

        private void GetCarrierList()
        {
            list = tms.GetCarriers();
            CarrierList.ItemsSource = list;
        }

        private void SelectCustomer(object sender, SelectionChangedEventArgs e)
        {
            uint id = (uint)CarrierList.SelectedIndex;
            if (id != 4294967295)
            {
                carrier = tms.GetCarrier(id + 1);
                txtCarrier_ID.Text = carrier.Name;
                txtDepot_City.Text = carrier.DepotCity.ToString();
                txtFTL_Avail.Text = carrier.FtlAvailability.ToString();
                txtLTL_Avail.Text = carrier.LtlAvailability.ToString();
            }

        }
        private void MakeFieldEmpty()
        {
            txtCarrier_ID.Text = "";
            txtDepot_City.Text = "";
            txtFTL_Avail.Text = "";
            txtLTL_Avail.Text = "";
        }
        private void Searchbtn_Click(object sender, RoutedEventArgs e)
        {
            if (cityList.SelectedIndex > 0)
            {
                string name = searchbox.Text;
                string city = cityList.Text;
                list = tms.SearchCarriers(name, city);
                CarrierList.ItemsSource = list;
            }
            else
            {
                MessageBox.Show("Please Select City !!", "EMpty Selction", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void LoadCity()
        {
            cityList.Items.Add("Select City");
            cityList.Items.Add("Windsor");
            cityList.Items.Add("London");
            cityList.Items.Add("Hamilton");
            cityList.Items.Add("Toronto");
            cityList.Items.Add("Oshawa");
            cityList.Items.Add("Belleville");
            cityList.Items.Add("Kingston");
            cityList.Items.Add("Ottawa");
        }

        private void ShowCarrier_Click(object sender, RoutedEventArgs e)
        {
            GetCarrierList();
        }
    }
}
