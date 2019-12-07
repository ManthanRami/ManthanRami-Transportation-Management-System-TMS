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
using TMS.Data;

namespace TMS.Pages_UI.Pages_Planner
{
    //=======================================================================================================================
    /// <summary>
    /// This is the Active Orders page for the Planner. They will be provided with an overview off all the currently active
    /// orders.
    /// </summary>
    //=======================================================================================================================
    public partial class ViewActiveOrders : Page
    {
        
        // List<Contract> lc = new List<Contract>();
        
        // Carriers are used for testing functionality
        // Will need to change to proper list type
        List<Carrier> lc = new List<Carrier>();
        TmsDal td = new TmsDal();

        public ViewActiveOrders()
        {
            InitializeComponent();
            LoadActiveOrders();
        }


        private void LoadActiveOrders()
        {
            //lc = td.GetActiveOrders();
            // Used for testing, will need to change from carriers
            lc = td.GetCarriers();
            ActiveOrderData.ItemsSource = lc;
        }
    }
}
