/*===============================================================================================================
*  FILE          : ModifyCarrierData.xaml.cs
*  PROJECT       : TMS 
*  PROGRAMMER    : Team 404
*  Date          : 2019-12-09
*  DESCRIPTION   : This is file containt all logic to change the log file saving directory according to the admin
*                  selection provided.
*================================================================================================================*/
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
        /*================================================================================================
        *  Function    : LoadLogFileDetails
        *  Description : This function will Load all the data of the log file to the UI
        *  Parameters  : Nothing
        *  Returns     : Nothing as return type is void
`       ================================================================================================*/
        private void LoadLogFileDetails()
        {
            logfileDetails.Document.Blocks.Clear();
            string location = Logger.GetPath()+@"\logs\TMS.log";
            string details = "File Name: TMS.log\nFile Location :"+location+"\nDate : "+ DateTime.Now.ToShortDateString();
            logfileDetails.AppendText(details);
        }
        /*================================================================================================
        *  Function    : ChangeLocation_Click
        *  Description : This function will open and Folder browser box to select the directory to select
        *                to save log file.
        *  Parameters  : object sender:
                         RoutedEventArgs e:
        *  Returns     : Nothing as return type is void
`       ================================================================================================*/
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
        /*================================================================================================
        *  Function    : Confirm_Click
        *  Description : This function will Create log into log file about directory changed.
        *  Parameters  : object sender:
                         RoutedEventArgs e:
        *  Returns     : Nothing as return type is void
`       ================================================================================================*/
        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            Logger.ChangeLogPath(location);
            System.Windows.MessageBox.Show("Log Directory has been Changed Successfully !!", "Done", MessageBoxButton.OK, MessageBoxImage.Information);
            LoadLogFileDetails();
        }
    }
}
