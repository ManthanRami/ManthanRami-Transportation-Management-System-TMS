/*
* FILE          : 	File Name
* PROJECT       : 	Course Code - Assignment Name
* PROGRAMMER    : 	Alex MacCumber - 8573909
* FIRST VERSION : 	Date Started YYYY-MM-DD
* DESCRIPTION   : 	Description of what this file does
*/


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
        Regex dbName = new Regex(@"^[a-zA-Z]+$");
        Regex portNum = new Regex(@"^[a-zA-Z]+$");
        public DBMSConfig()
        {
            InitializeComponent();
            getCurrentDBMS();
        }

        private void btnSave_Edits_Click(object sender, RoutedEventArgs e)
        {
            string newsettings = "server" + txtIP_Address.Text + ";user id=" + txtID.Text + ";port=" + txtPort_Number.Text + ";password=" + txtPassword.Text + "!;database=" + txtDatabase.Text + "";
            CmpDal.connectionString= newsettings;
            try
            {
                CmpDal cmp = new CmpDal();
            }
            catch(Exception ex)
            {
              if(MessageBox.Show("Connection data Incorrect !!\nDo you want to go with previous setting ?", "Invaild Data", MessageBoxButton.YesNo, MessageBoxImage.Error)==MessageBoxResult.No)
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

        private void CheckAllFields()
        {

        }
        private void btnEdit_Options_Click(object sender, RoutedEventArgs e)
        {
            txtIP_Address.IsReadOnly = false;
            txtPort_Number.IsReadOnly = false;
        }

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
