/*
* FILE          : 	File Name
* PROJECT       : 	Course Code - Assignment Name
* PROGRAMMER    : 	Alex MacCumber - 8573909
* FIRST VERSION : 	Date Started YYYY-MM-DD
* DESCRIPTION   : 	Description of what this file does
*/

using System;
using System.Collections.Generic;
using System.IO;
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
using TMS.Utils;

namespace TMS.Pages_UI.Pages_Admin
{
    //=======================================================================================================================
    /// <summary>
    /// This is the page for the Admin that contains the options and input fields needed to allow them to review log files.
    /// </summary>
    //=======================================================================================================================
    public partial class ReviewLogFiles : Page
    {
        public ReviewLogFiles()
        {
            InitializeComponent();
            LoadToLogReview();

        }

        private void LoadToLogReview()
        {
            string location = Logger.GetPath() + @"\logs\TMS.log";
            string discription = null;
            using (StreamReader text = new StreamReader(location))
            {
                discription = text.ReadToEnd();
            }
            LogData.AppendText(discription);
        }
        
    }
}
