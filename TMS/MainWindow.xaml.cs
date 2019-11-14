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
        public MainWindow()
        {
            InitializeComponent();

            List<Contract> contracts;

            CmpDal cmp = new CmpDal();

            contracts = cmp.GetContracts();

            foreach (var contract in contracts)
            {
                Trace.WriteLine(contract.Client + " " + contract.Quantity + " " + contract.JobType.ToString() + " " + contract.VanType.ToString());
            }
        }
    }
}
