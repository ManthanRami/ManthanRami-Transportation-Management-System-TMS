/*===============================================================================================================
*  FILE          : DBMSConfig.xaml.cs
*  PROJECT       : TMS 
*  PROGRAMMER    : Team 404
*  Date          : 2019-12-09
*  DESCRIPTION   : This is file containt all logic to select the contract market place using information provided 
*                  by the Admin.
*================================================================================================================*/

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
using MySql.Data;
using System.Configuration;
using TMS.Data;
using System.Text.RegularExpressions;
using System.Data.OleDb;
using MySql.Data.MySqlClient;

namespace TMS.Pages_UI.Pages_Admin
{
    //=======================================================================================================================
    /// <summary>
    /// This is the page for the Admin that contains the options and input fields needed to allow them to configure setting
    /// of the DDMS connection.
    /// </summary>
    //=======================================================================================================================
    public partial class DBMSConfig : Page
    {
        public string currentSetting = null;
        Regex name = new Regex(@"^[a-zA-Z]+$");
        Regex portNum = new Regex(@"^[+]?\d+([.]\d+)?$");
        Regex ipNum = new Regex(@"\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}");


        public DBMSConfig()
        {
            InitializeComponent();
            getCurrentDBMS();
        }
        /*================================================================================================
        *  Function    : btnSave_Edits_Click
        *  Description : This function will updates the Contract market place Database Selection
        *  Parameters  : object sender:
                         RoutedEventArgs e:
        *  Returns     : Nothing as return type is void
`       ================================================================================================*/
        private void btnSave_Edits_Click(object sender, RoutedEventArgs e)
        {
            if(CheckAllFields())
            {
                string newsettings = "server=" + txtIP_Address.Text + ";user id=" + txtID.Text + ";port=" + txtPort_Number.Text + ";password=" + txtPassword.Text + ";database=" + txtDatabase.Text + "";
                
                if(TestConnection(newsettings))
                {
                    CmpDal.connectionString = newsettings;
                    MessageBox.Show("Database Setting Updated Successfully !! ", "Invalid Entry", MessageBoxButton.OK, MessageBoxImage.Information);

                }
                else
                {
                    if (MessageBox.Show("Connection data Incorrect !!\nDo you want to go with previous setting ?", "Invaild Data", MessageBoxButton.YesNo, MessageBoxImage.Error) == MessageBoxResult.No)
                    {
                        CmpDal.connectionString = currentSetting;
                    }
                    else
                    {
                        CmpDal.connectionString = currentSetting;
                        getCurrentDBMS();
                    }
                }                                              
            }

        }
        /*================================================================================================
        *  Function    : TestConnection
        *  Description : This function will search for customer in the database.
        *  Parameters  : string connection :connection string provided by admin
        *  Returns     : bool true if connection string can connect otherwise false
`       ================================================================================================*/
        private bool TestConnection(string connectionString)
        {
            try
            {
                using (var connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    connection.Close();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
        /*================================================================================================
        *  Function    : CheckAllFields
        *  Description : This function will validate all the field on the DBMSConfig.xmal page.
        *  Parameters  : 
        *  Returns     : bool true if all are in correct format
`       ================================================================================================*/
        private bool CheckAllFields()
        {
            if(name.IsMatch(txtID.Text))
            {
                if(ipNum.IsMatch(txtIP_Address.Text))
                {
                    if(name.IsMatch(txtDatabase.Text))
                    {
                        if(portNum.IsMatch(txtPort_Number.Text))
                        {
                            return true;
                        }
                        else
                        {
                            MessageBox.Show("Port number must be positive number ", "Invalid Entry", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Database Nuame must have only alphabet letters !!", "Invalid Entry", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show("IP Address is not in proper Format !!", "Invalid Entry", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("User ID must have only alphabet letters !!", "Invalid Entry", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return false;
        }
        /*================================================================================================
        *  Function    : btnEdit_Options_Click
        *  Description : This function will search for customer in the database.
        *  Parameters  : object sender:
                         RoutedEventArgs e:
        *  Returns     : Nothing as return type is void
`       ================================================================================================*/
        private void btnEdit_Options_Click(object sender, RoutedEventArgs e)
        {
            txtIP_Address.IsReadOnly = false;
            txtPort_Number.IsReadOnly = false;
        }
        /*================================================================================================
        *  Function    : getCurrentDBMS
        *  Description : This function will get the current configuration of the contract market palce 
        *                and populate the fields.
        *  Parameters  : Nothing
        *  Returns     : Nothing as return type is void
`       ================================================================================================*/
        private void getCurrentDBMS()
        {           
            currentSetting = CmpDal.connectionString;
            string[] cs = currentSetting.Split(';');
            string[] server = cs[0].Split('=');
            string[] id = cs[1].Split('=');
            string[] port = cs[2].Split('=');
            string[] pwd = cs[3].Split('=');
            string[] dbName = cs[4].Split('=');
            txtDatabase.Text = dbName[1];
            txtID.Text = id[1];
            txtIP_Address.Text = server[1];
            txtPassword.Text = pwd[1];
            txtPort_Number.Text = port[1];

        }
    }
}
