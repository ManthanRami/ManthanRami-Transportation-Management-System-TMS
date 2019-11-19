using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using TMS.Data;

namespace TMS
{


    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // For testing purposes
        bool testingNoValidation = true;

        protected string username;
        protected string password;
        protected bool allowLogin;

        protected bool newPageLoaded;
        public MainWindow()
        {
            InitializeComponent();

            //List<Contract> contracts;

            //CmpDal cmp = new CmpDal();

            //contracts = cmp.GetContracts();

            //foreach (var contract in contracts)
            //{
            //    Trace.WriteLine(contract.Client + " " + contract.Quantity + " " + contract.JobType.ToString() + " " + contract.VanType.ToString());
            //}
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            username = UserNameBox.Text;
            password = PasswordBox.Password;

            if (testingNoValidation == false)
            {
                if (username == "" && password == "")
                {
                    Error.Content = "Please Enter Usename and Password !!";
                }
                else if (username == "")
                {
                    Error.Content = "Please Enter the Username !!";
                }
                else if (password == "")
                {
                    Error.Content = "Please Enter the Password !!";
                }
                LoginAccess obj = new LoginAccess();
                allowLogin = obj.verifyAccount(username, password);

                if (allowLogin)
                {

                }
                else
                {
                    Error.Content = "Your Password or Username is incorrect !!";
                    UserNameBox.Text = "";
                    PasswordBox.Password = "";
                }
            }


            if (testingNoValidation == true)
            {
                // Will need to be put into proper if statements when user type is determined properly
                if (username == "admin")
                {
                    AdminWindow admin = new AdminWindow();
                
                    admin.Show();

                    newPageLoaded = true;
                }
            

                if (username == "buyer")
                {
                    BuyerWindow buyer = new BuyerWindow();
                
                    buyer.Show();
                    newPageLoaded = true;
                }
            

                if (username == "planner")
                {
                    PlannerWindow planner = new PlannerWindow();
                
                    planner.Show();
                    newPageLoaded = true;
                }

                if (newPageLoaded == true)
                {
                    this.Close();
                }
                else
                {

                }
            }


        }
    }
}
