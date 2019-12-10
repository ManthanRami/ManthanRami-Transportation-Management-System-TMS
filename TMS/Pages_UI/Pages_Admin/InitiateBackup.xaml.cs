/*===============================================================================================================
*  FILE          : ModifyCarrierData.xaml.cs
*  PROJECT       : TMS 
*  PROGRAMMER    : Team 404
*  Date          : 2019-12-09
*  DESCRIPTION   : This is file containt all logic to change the backup file saving directory according to the admin
*                  selection provided.
*================================================================================================================*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.IO;
using System.Text;
using TMS.Data;
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
using System.Windows.Forms;
using MessageBox = System.Windows.MessageBox;

namespace TMS.Pages_UI.Pages_Admin
{
    //=======================================================================================================================
    /// <summary>
    /// This is the page for the Admin that contains the options and input fields needed to allow them to initiate a backup
    /// of the database.
    /// </summary>
    //=======================================================================================================================
    public partial class InitiateBackup : Page
    {
        private string backupPath = System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
        private string tempLocation = null;
        TmsDal tms = new TmsDal();
        
        public InitiateBackup()
        {
            InitializeComponent();
            CurrentLocation.AppendText(backupPath);
        }
        /// <summary>
        /// This function will run backup of current databse using tmsDal function
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRun_Backup_Click(object sender, RoutedEventArgs e)
        {
            tms.BackupDatabase(backupPath);
            MessageBox.Show("Backup file  has been successfully Done !!", "Done", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        /// <summary>
        /// This function will allow admin to change the location of the backup file 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChangeLocation_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new FolderBrowserDialog();
            dlg.Description = "Select Location";
            dlg.ShowNewFolderButton = true;
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                tempLocation = dlg.SelectedPath;
                CurrentLocation.Document.Blocks.Clear();
                CurrentLocation.AppendText(backupPath);
            }
        }
        /// <summary>
        /// this function will save the selected path to backup database file.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Edits_Click(object sender, RoutedEventArgs e)
        {
            backupPath = tempLocation;
            MessageBox.Show("Backup file Location has been changed successfully !!", "Done", MessageBoxButton.OK,MessageBoxImage.Information ) ;
        }
    }
}
