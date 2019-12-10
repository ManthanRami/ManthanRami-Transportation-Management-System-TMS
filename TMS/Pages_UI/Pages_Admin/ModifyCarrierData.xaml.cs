/*===============================================================================================================
*  FILE          : ModifyCarrierData.xaml.cs
*  PROJECT       : TMS 
*  PROGRAMMER    : Team 404
*  Date          : 2019-12-09
*  DESCRIPTION   : This is file containt all logic to Modify the Carrier data as oer the Admin selection and 
*                  data provided.
*================================================================================================================*/
using TMS.Data;
using System.Windows;
using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Data;
using System.Text.RegularExpressions;

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
        Regex onlyAlphaWithOneSpace = new Regex("^[a-z ,a-z]+$");
        Regex onlyAlpha = new Regex("^[aA - zZ] +$");
        Regex onlynumDec = new Regex("^[0-9.]+$");

        Carrier carrier = new Carrier();
        Carrier checkCarrier = new Carrier();
        TmsDal tms = new TmsDal();
        DataSet ds = new DataSet();
        public uint cID = 0;
        List<Carrier> list = new List<Carrier>();
        public ModifyCarrierData()
        {
            InitializeComponent();
            GetCarrierList();
            LoadCity();
            cityList.SelectedIndex = 0;
        }
        /*================================================================================================
        *  Function    : UpdateCarrier
        *  Description : This function will updates the Contract market place Database Selection
        *  Parameters  : object sender:
                         RoutedEventArgs e:
        *  Returns     : Nothing as return type is void
`       ================================================================================================*/
        private void UpdateCarrier(object sender, RoutedEventArgs e)
        {
            carrier.Name = txtCarrier_Name.Text;
            
            TMS.Data.City city = (TMS.Data.City)Enum.Parse(typeof(TMS.Data.City), txtDepot_City.Text);
            carrier.DepotCity = city;
            carrier.FtlAvailability = Convert.ToInt32(txtFTL_Avail.Text);
            carrier.LtlAvailability = Convert.ToInt32(txtLTL_Avail.Text);
            carrier.FTLRate         = Convert.ToInt32(txtFTL_Rate.Text);
            carrier.LTLRate         = Convert.ToInt32(txtLTL_Rate.Text);
            carrier.ReeferCharge    = Convert.ToInt32(txtReefer_Rate.Text);
            
            list = tms.SearchCarriers(carrier.Name, txtDepot_City.Text);
            if (list.Count != 0)
            {
                tms.UpdateCarrier(Convert.ToUInt32(cID), carrier);
                MessageBox.Show("Carrier Updated Successfully ", "Done", MessageBoxButton.OK, MessageBoxImage.Information);
                GetCarrierList();
                MakeFieldEmpty();
            }
            else if (MessageBox.Show("Carrier does not exist !\n Do you want to add ?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Error) == MessageBoxResult.Yes)
            {
                tms.CreateCarrier(carrier);
                MessageBox.Show("Carrier Added Successfully ", "Done", MessageBoxButton.OK, MessageBoxImage.Information);
                MakeFieldEmpty();
                GetCarrierList();
            }
        }
        /*================================================================================================
        *  Function    : GetCarrierList
        *  Description : This function will updates the Contract market place Database Selection
        *  Parameters  : object sender:
                         RoutedEventArgs e:
        *  Returns     : Nothing as return type is void
`       ================================================================================================*/
        private void GetCarrierList()
        {
            list = tms.GetCarriers();
            CarrierList.ItemsSource = list;
        }

        private void SelectCarrier(object sender, SelectionChangedEventArgs e)
        {
            DataGrid gd = (DataGrid)sender;
            dynamic rowView = gd.SelectedItem;
            if (rowView != null)
            {
                cID = (uint)rowView.CarrierID;
                if (cID != 4294967295)
                {
                    carrier = tms.GetCarrier(cID);
                    txtCarrier_Name.Text = carrier.Name;
                    txtDepot_City.Text = carrier.DepotCity.ToString();
                    txtFTL_Avail.Text = carrier.FtlAvailability.ToString();
                    txtLTL_Avail.Text = carrier.LtlAvailability.ToString();
                    txtFTL_Rate.Text = carrier.FTLRate.ToString();
                    txtLTL_Rate.Text = carrier.LTLRate.ToString();
                    txtReefer_Rate.Text = carrier.ReeferCharge.ToString();
                }
            }

        }
        /*================================================================================================
        *  Function    : MakeFieldEmpty
        *  Description : This function will updates the Contract market place Database Selection
        *  Parameters  : object sender:
                         RoutedEventArgs e:
        *  Returns     : Nothing as return type is void
`       ================================================================================================*/
        private void MakeFieldEmpty()
        {
            txtCarrier_Name.Text = "";
            txtDepot_City.Text = "";
            txtFTL_Avail.Text = "";
            txtLTL_Avail.Text = "";
            txtFTL_Rate.Text = "";
            txtLTL_Rate.Text = "";
            txtReefer_Rate.Text = "";
        }
        /*================================================================================================
        *  Function    : Searchbtn_Click
        *  Description : This function will updates the Contract market place Database Selection
        *  Parameters  : object sender:
                         RoutedEventArgs e:
        *  Returns     : Nothing as return type is void
`       ================================================================================================*/
        private void Searchbtn_Click(object sender, RoutedEventArgs e)
        {
            if (cityList.SelectedIndex > 0)
            {
                string name = searchbox.Text;
                string city = cityList.Text;
                list = tms.SearchCarriers(name, city);
                CarrierList.ItemsSource = list;
            }
            else if (cityList.SelectedIndex == 0)
            {
                GetCarrierList();
            }
            else
            {
                MessageBox.Show("Please Select City !!", "Empty Selction", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        /*================================================================================================
        *  Function    : LoadCity
        *  Description : This function will updates the Contract market place Database Selection
        *  Parameters  : object sender:
                         RoutedEventArgs e:
        *  Returns     : Nothing as return type is void
`       ================================================================================================*/
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
        /*================================================================================================
        *  Function    : ShowCarrier_Click
        *  Description : This function will updates the Contract market place Database Selection
        *  Parameters  : object sender:
                         RoutedEventArgs e:
        *  Returns     : Nothing as return type is void
`       ================================================================================================*/
        private void ShowCarrier_Click(object sender, RoutedEventArgs e)
        {
            GetCarrierList();
        }
        private void CheckFields()
        {
            if(onlyAlphaWithOneSpace.IsMatch( txtCarrier_Name.Text))
            {
                if(onlyAlpha.IsMatch((txtDepot_City.Text)))
                {
                    if(onlynumDec.IsMatch(txtFTL_Rate.Text))
                    {

                    }
                }
            }

        }
    }
}
