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
using TMS.Pages_UI.Pages_Buyer;

namespace TMS
{
    //=======================================================================================================================
    /// <summary>
    /// This is the Main Window for a user with the Buyer Role.  It provides them with a Main Window that offers them
    /// quick and easy navigation to their tasks, utilizing a button bar on one side of the screen, and utilizing the rest
    /// as workspace to display page layouts with the required inputs and functions
    /// </summary>
    //=======================================================================================================================
    public partial class BuyerWindow : Window
    {
        public BuyerWindow()
        {
            InitializeComponent();
            BuyerMain.Content = new BuyerStartup();
        }

        //=======================================================================================================================
        /// <summary>
        /// Description :   btnBuyerLogout_Click logs the user out of the application, and reopens a login window to allow
        ///                 login to happen again
        /// </summary>
        /// <param name="sender">   The sender is the control that the action is for (say OnClick, it's the button).</param>
        /// <param name="e">    Contains state information and event data associated with a routed event.</param>
        //=======================================================================================================================
        private void BuyerLogout_Click(object sender, RoutedEventArgs e)
        {
            LoginScreen login = new LoginScreen();
            login.Show();
            this.Close();
        }


        //=======================================================================================================================
        /// <summary>
        /// Description :   btnMarketplace_Click swaps the page content to allow the buyer to utilize the contract marketplace
        ///                 screen
        /// </summary>
        /// <param name="sender">   The sender is the control that the action is for (say OnClick, it's the button).</param>
        /// <param name="e">    Contains state information and event data associated with a routed event.</param>
        //=======================================================================================================================
        private void btnMarketplace_Click(object sender, RoutedEventArgs e)
        {
            BuyerMain.Content = new ContractMarketplace();
        }


        //=======================================================================================================================
        /// <summary>
        /// Description :   btnCust_Management_Click changes the page content to display the customer management screen
        /// </summary>
        /// <param name="sender">   The sender is the control that the action is for (say OnClick, it's the button).</param>
        /// <param name="e">    Contains state information and event data associated with a routed event.</param>
        //=======================================================================================================================
        private void btnCust_Management_Click(object sender, RoutedEventArgs e)
        {
            BuyerMain.Content = new CustomerManagement();
        }

        //=======================================================================================================================
        /// <summary>
        /// Description :   btnNew_Order_Click opens the New order creation screen in the main window
        /// </summary>
        /// <param name="sender">   The sender is the control that the action is for (say OnClick, it's the button).</param>
        /// <param name="e">    Contains state information and event data associated with a routed event.</param>
        //=======================================================================================================================
        private void btnNew_Order_Click(object sender, RoutedEventArgs e)
        {
            BuyerMain.Content = new InitiateNewOrder();
        }

        //=======================================================================================================================
        /// <summary>
        /// Description :   btnCompleted_Orders_Click changes the main windows contents to display the completed orders
        ///                 page to allow the buyer to review all completed orders
        /// </summary>
        /// <param name="sender">   The sender is the control that the action is for (say OnClick, it's the button).</param>
        /// <param name="e">    Contains state information and event data associated with a routed event.</param>
        //=======================================================================================================================
        private void btnCompleted_Orders_Click(object sender, RoutedEventArgs e)
        {
            BuyerMain.Content = new CompletedOrders();
        }
    }
}
