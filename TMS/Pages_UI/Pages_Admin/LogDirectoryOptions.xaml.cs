/*
* FILE          : 	File Name
* PROJECT       : 	Course Code - Assignment Name
* PROGRAMMER    : 	Alex MacCumber - 8573909
* FIRST VERSION : 	Date Started YYYY-MM-DD
* DESCRIPTION   : 	Description of what this file does
*/

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using TMS.Utils;

namespace TMS.Pages_UI.Pages_Admin
{
    //=======================================================================================================================
    /// <summary>
    /// This is the page for the Admin that contains the options and input fields needed to allow them to configure setting
    /// of the Log directory.
    /// </summary>
    //=======================================================================================================================
    public partial class LogDirectoryOptions : Page
    {

        private string location = Logger.GetPath() + @"\logs\TMS.log";
        public LogDirectoryOptions()
        {
            InitializeComponent();
            ///here we will load current log file details on rich text box
            CurrentLocation.AppendText(location);
            LoadLogFileDetails();
        }

        private void LoadLogFileDetails()
        {
            logfileDetails.Document.Blocks.Clear();
            string location = Logger.GetPath()+@"\logs\TMS.log";
            string details = "File Name: TMS.log\nFile Location :"+location+"\nDate : "+ DateTime.Now.ToShortDateString();
            logfileDetails.AppendText(details);
        }
        private void ChangeLocation_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new FolderBrowserDialog();
            dlg.Description = "Select Location";
            dlg.ShowNewFolderButton = true;
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                location = dlg.SelectedPath;
                CurrentLocation.Document.Blocks.Clear();
                CurrentLocation.AppendText(location);
            }
        }



        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            Logger.ChangeLogPath(location);
            System.Windows.MessageBox.Show("Log Directory has been Changed Successfully !!", "Done", MessageBoxButton.OK, MessageBoxImage.Information);
            LoadLogFileDetails();
        }
    }
}
