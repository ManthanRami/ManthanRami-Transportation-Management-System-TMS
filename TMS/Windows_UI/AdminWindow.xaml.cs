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
using System.Windows.Shapes;
using TMS.Pages_UI.Pages_Admin;
using TMS.Pages_UI.Pages_Buyer;
using TMS.Pages_UI.Pages_Planner;
using TMS.Utils;

namespace TMS
{
    //=======================================================================================================================
    /// <summary>
    /// This is the Main Window for a user with the Admin Role.  It provides them with a Main Window that offers them
    /// quick and easy navigation to their tasks, utilizing a button bar on one side of the screen, and utilizing the rest
    /// as workspace to display page layouts with the required inputs and functions
    /// </summary>
    //=======================================================================================================================
    public partial class AdminWindow : Window
    {
        //=======================================================================================================================
        /// <summary>
        ///     Initializes the Admin window screen and sets the contain of its main screen to an admin startup page.
        /// </summary>
        //=======================================================================================================================
        public AdminWindow()
        {
            InitializeComponent();
            UnselectButtons();
            AdminMain.Content = new AdminStartup();
        }

        //=======================================================================================================================
        /// <summary>
        /// Description :   btnAdminLogout_Click logs the user out of the application, and reopens a login window to allow
        ///                 login to occur again
        /// </summary>
        /// <param name="sender">   The sender is the control that the action is for (say OnClick, it's the button).</param>
        /// <param name="e">    Contains state information and event data associated with a routed event.</param>
        //=======================================================================================================================
        private void btnAdminLogout_Click(object sender, RoutedEventArgs e)
        {
            LoginScreen login = new LoginScreen();
            login.Show();
            this.Close();
            Logger.Info(LogOrigin.Ui, "Admin logged off of system.");
        }

        //=======================================================================================================================
        /// <summary>
        /// Description :   btnReview_Click swaps the page on screen to allow the admin to review log file data without needing
        ///                 to leave the TMS application
        /// </summary>
        /// <param name="sender">   The sender is the control that the action is for (say OnClick, it's the button).</param>
        /// <param name="e">    Contains state information and event data associated with a routed event.</param>
        //=======================================================================================================================
        private void btnReview_Click(object sender, RoutedEventArgs e)
        {
            UnselectButtons();
            btnReview.Background = Brushes.LightBlue;
            AdminMain.Content = new ReviewLogFiles();
        }

        //=======================================================================================================================
        /// <summary>
        /// Description :   btnLogDirectory_Click swaps the page on screen to the Log Directory page
        /// </summary>
        /// <param name="sender">   The sender is the control that the action is for (say OnClick, it's the button).</param>
        /// <param name="e">    Contains state information and event data associated with a routed event.</param>
        //=======================================================================================================================
        private void btnLogDirectory_Click(object sender, RoutedEventArgs e)
        {
            UnselectButtons();
            btnLogDirect.Background = Brushes.LightBlue;
            AdminMain.Content = new LogDirectoryOptions();
        }

        //=======================================================================================================================
        /// <summary>
        /// Description :   btnDBMS_Config_Click swaps the page on screen to the DBMS Configuration editing page
        /// </summary>
        /// <param name="sender">   The sender is the control that the action is for (say OnClick, it's the button).</param>
        /// <param name="e">    Contains state information and event data associated with a routed event.</param>
        //=======================================================================================================================
        private void btnDBMS_Config_Click(object sender, RoutedEventArgs e)
        {
            UnselectButtons();
            btnDBMS.Background = Brushes.LightBlue;
            AdminMain.Content = new DBMSConfig();
        }

        //=======================================================================================================================
        /// <summary>
        /// Description :   btnBackup_Click swaps the page on screen to the Rate Data editing page
        /// </summary>
        /// <param name="sender">   The sender is the control that the action is for (say OnClick, it's the button).</param>
        /// <param name="e">    Contains state information and event data associated with a routed event.</param>
        //=======================================================================================================================
        private void Backup_Click(object sender, RoutedEventArgs e)
        {
            UnselectButtons();
            btnBackup.Background = Brushes.LightBlue;
            AdminMain.Content = new InitiateBackup();
        }

        //=======================================================================================================================
        /// <summary>
        /// Description :   btnCarrierData_Click swaps the page on screen to the Carrier Data editing page
        /// </summary>
        /// <param name="sender">   The sender is the control that the action is for (say OnClick, it's the button).</param>
        /// <param name="e">    Contains state information and event data associated with a routed event.</param>
        //=======================================================================================================================
        private void btnCarrierData_Click(object sender, RoutedEventArgs e)
        {
            UnselectButtons();
            btnCarrier.Background = Brushes.LightBlue;
            AdminMain.Content = new ModifyCarrierData();
        }

        private void UnselectButtons()
        {
            btnReview.Background = Brushes.LightGray;
            btnLogDirect.Background = Brushes.LightGray;
            btnDBMS.Background = Brushes.LightGray;
            btnBackup.Background = Brushes.LightGray;
            btnCarrier.Background = Brushes.LightGray;
        }

    }
}
