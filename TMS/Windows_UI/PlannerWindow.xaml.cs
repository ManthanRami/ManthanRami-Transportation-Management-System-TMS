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
using TMS.Pages_UI.Pages_Planner;
using TMS.Utils;

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
            UnselectButtons();
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
            Logger.Info(LogOrigin.Ui, "Planner logged off of system.");
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
            UnselectButtons();
            btnActive_Orders.Background = Brushes.LightBlue;
            PlannerMain.Content = new ViewActiveOrders();
        }

        private void btnTrips_To_Orders_Click(object sender, RoutedEventArgs e)
        {
            UnselectButtons();
            btnTrips_To_Orders.Background = Brushes.LightBlue;
            PlannerMain.Content = new AddTripsToOrder();
        }

        private void btnAggregate_Activity_Click(object sender, RoutedEventArgs e)
        {
            UnselectButtons();
            btnAggregate_Activity.Background = Brushes.LightBlue;
            PlannerMain.Content = new AggregateActivity();
        }

        private void UnselectButtons()
        {
            btnActive_Orders.Background = Brushes.LightGray;
            btnTrips_To_Orders.Background = Brushes.LightGray;
            btnAggregate_Activity.Background = Brushes.LightGray;
        }
    }
}
