/*
* FILE          : 	File Name
* PROJECT       : 	Course Code - Assignment Name
* PROGRAMMER    : 	Alex MacCumber - 8573909
* FIRST VERSION : 	Date Started YYYY-MM-DD
* DESCRIPTION   : 	Description of what this file does
*/

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TMS.Utils;


namespace TMS
{

    //=======================================================================================================================
    /// <summary>
    /// This is the Login Screen for the user that enables the TMS system to identify the user and navigate the user throught 
    /// the Application according to their role
    /// </summary>
    //=======================================================================================================================
    public partial class LoginScreen : Window
    {

        protected string username;
        protected string password;
        protected bool newPageLoaded;
        
        public LoginScreen()
        {
            InitializeComponent();
            Logger.Info(LogOrigin.Ui, "OSHT Application started successfully.");
        }
        //=======================================================================================================================
        /// <summary>
        /// Description :   btnLogin_Click function calls two different functions called ValidateFields & DisplayScreen
        ///                 which allows user to navigate through the TMS application
        /// </summary>
        /// <param name="sender">   The sender is the control that the action is for (say OnClick, it's the button).</param>
        /// <param name="e">    Contains state information and event data associated with a routed event.</param>
        //=======================================================================================================================
        public void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            string whichScreen = ""; 
            whichScreen=ValidateFields(UserNameBox, PasswordBox);
            if(whichScreen!="")
            {
              DisplayScreen(whichScreen);
            }         
        }
        //=======================================================================================================================
        /// <summary>
        /// ValidateFields  : This function will takes value from the user login screen and validate all the fields checks 
        ///                   for if the field is empty or not also check for appropraite user name.
        /// </summary>
        /// <param name="name"> TextBox on the Login screen which is used to get username input from the current user which 
        ///                     helps to identify the user role</param>
        /// <param name="pass"> PasswordBox on the Login screen which is used to get password for the current user account 
        ///                     to login in</param>
        /// <returns></returns>
        //========================================================================================================================
        public string ValidateFields(TextBox name, PasswordBox pass)
        {
            username = name.Text;
            password = pass.Password;

            if ((username == "") && (password == ""))
            {
                username = "";
                name.Text="";
                pass.Password = "";
                Error.Content = "Please Enter Usename and Password !!";
                Logger.Error(LogOrigin.Ui, "No login credentials detected on login attempt.");
            }
            else if (username == "")
            {
                username = "";
                name.Text = "";
              //  pass.Password = "";
                Error.Content = "Please Enter the Username !!";
                Logger.Warn(LogOrigin.Ui, "Empty username credential detected on login attempt.");
            }
            else if (password == "")
            {
                username = "";
              //  name.Text = "";
                pass.Password = "";
                Error.Content = "Please Enter the Password !!";
                Logger.Error(LogOrigin.Ui, "Empty password credential detected on login attempt.");
            }            
            else if((username!="admin") && (username!="buyer") && (username!="planner"))
            {
                username = "";
                name.Text = "";
                pass.Password = "";
                Error.Content = "Please Enter Appropriate User Name !!";
                Logger.Info(LogOrigin.Ui, "Invalid username credentials detected on login attempt.");
            }

            return username;
            
        }
        //=====================================================================================================================
        /// <summary>
        /// DisplayScreen : This funtion will identify the current user and Navigate user according to his/her role in TMS.
        /// </summary>
        /// <param name="Screen"> Name of the Screen need to be display after the login Screen.</param>
        //======================================================================================================================
        public int DisplayScreen(string Screen)
        {
            int userType = 0;

            if (Screen == "admin")
            {
                AdminWindow admin = new AdminWindow();
                admin.Show();
                newPageLoaded = true;
                userType = 1;
                
                Logger.Info(LogOrigin.Ui, "Admin successfully logged in to the OHST System");
            }
            else if (Screen == "buyer")
            {
                BuyerWindow buyer = new BuyerWindow();
                buyer.Show();
                newPageLoaded = true;
                userType = 2;
                Logger.Info(LogOrigin.Ui, "Buyer successfully logged in to the OHST System");
            }
            else if (Screen == "planner")
            {
                PlannerWindow planner = new PlannerWindow();
                planner.Show();
                newPageLoaded = true;
                Logger.Info(LogOrigin.Ui, "Planner successfully logged in to the OHST System");
            }
            if (newPageLoaded == true)
            {
                this.Close();
            }
            return userType;
        }
    }
}
