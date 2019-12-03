﻿/*
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
        public DBMSConfig()
        {
            InitializeComponent();
        }

        private void btnSave_Edits_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnEdit_Options_Click(object sender, RoutedEventArgs e)
        {
            txtIP_Address.IsReadOnly = false;
            txtPort_Number.IsReadOnly = false;
        }
    }
}
