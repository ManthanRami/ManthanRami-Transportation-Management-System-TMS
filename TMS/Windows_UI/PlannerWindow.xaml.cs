/*
* FILE          : 	File Name
* PROJECT       : 	Course Code - Assignment Name
* PROGRAMMER    : 	Alex MacCumber - 8573909
* FIRST VERSION : 	Date Started YYYY-MM-DD
* DESCRIPTION   : 	Description of what this file does
*/


//=======================================================================================================================
// WIP NOTES
//=======================================================================================================================
// Planner needs to be able to:
// Get orders from buyers that are waiting for a carrier / carriers to be applied to the order
    // Trip Management? Fulfill Order?  Fetch Awaiting Orders?
// Produce reports of aggregate activity in the OHST
// Confirm Complete Orders
// Active order Summary screen
// Generate Summary report of all invoice data for
    // All time
    // or
    // Past two weeks of simulated Time
// Simulate time has passed
    // Buttons with increments? Dropdown to select number of hours to simulate?
//=======================================================================================================================
//=======================================================================================================================


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
using TMS.Pages_UI.Pages_Planner;

namespace TMS
{
    //=======================================================================================================================
    /// <summary>
    /// This is the Main Window for a user with the Planner Role.  It provides them with a Main Window that offers them
    /// quick and easy navigation to their tasks, utilizing a button bar on one side of the screen, and utilizing the rest
    /// as workspace to display page layouts with the required inputs and functions
    /// </summary>
    //=======================================================================================================================
    public partial class PlannerWindow : Window
    {

        //=======================================================================================================================
        /// <summary>
        ///     Initializes the planner window screen and sets the contain of its main screen to a planner startup page.
        /// </summary>
        //=======================================================================================================================
        public PlannerWindow()
        {
            InitializeComponent();
            PlannerMain.Content = new PlannerStartup();
        }


        //=======================================================================================================================
        /// <summary>
        /// Description :   btnPlannerLogout_Click logs the user out of the application, and reopens a login window to allow
        ///                 login to happen again
        /// </summary>
        /// <param name="sender">   The sender is the control that the action is for (say OnClick, it's the button).</param>
        /// <param name="e">    Contains state information and event data associated with a routed event.</param>
        //=======================================================================================================================
        private void btnPlanner_Logout_Click(object sender, RoutedEventArgs e)
        {
            LoginScreen login = new LoginScreen();
            login.Show();
            this.Close();
        }

        //=======================================================================================================================
        /// <summary>
        /// Description :   btnActive_Orders_Click pulls up a summary screen of all the active orders.
        /// </summary>
        /// <param name="sender">   The sender is the control that the action is for (say OnClick, it's the button).</param>
        /// <param name="e">    Contains state information and event data associated with a routed event.</param>
        //=======================================================================================================================
        private void btnActive_Orders_Click(object sender, RoutedEventArgs e)
        {
            PlannerMain.Content = new ViewActiveOrders();
        }

  


    }
}
