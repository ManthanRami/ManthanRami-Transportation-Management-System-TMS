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
using TMS.Exceptions;

namespace TMS
{


    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        protected string username;
        protected string password;
        protected bool allowLogin;

        public MainWindow()
        {
            InitializeComponent();

            TmsDal dal = new TmsDal();

            User user = new User();
            user.Username = "admin";
            user.Password = "password";
            user.Email = "admin@test.com";
            user.FirstName = "Testing";
            user.LastName = "Testerson";
            user.Type = UserType.Admin;

            dal.CreateUser(user);
            //uint id = dal.GetUserID("admin");
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            username = UserNameBox.Text;
            password = PasswordBox.Password;

            if(username=="" && password=="" )
            {
                Error.Content = "Please Enter Usename and Password !!";
            }
            else if(username=="")
            {
                Error.Content = "Please Enter the Username !!";
            }
            else if(password=="")
            {
                Error.Content = "Please Enter the Password !!";
            }
            LoginAccess obj = new LoginAccess();
            allowLogin = obj.verifyAccount(username, password);
            
            if(allowLogin)
            {

            }
            else
            {
                Error.Content = "Your Passoword or Username is incorrect !!";
                UserNameBox.Text = "";
                PasswordBox.Password = "";
            }
        }
    }
}
