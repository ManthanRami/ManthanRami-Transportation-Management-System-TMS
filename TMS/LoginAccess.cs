/*==========================================================================
*   FILE            : MainWindow.xaml.cs
*   PROJECT         : PROG2121 - Assignment #4
*   PROGRAMMER      : Manthan Rami
*   FIRST VERSION   : 2019-10-18
*   DESCRIPTION     : In this file containt Logic of LoginAccess which 
*                     is used in TMS project to verify the user Account 
=============================================================================*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMS
{
    //this class will communicate with database and 
    //verify the user account login access 

    class LoginAccess
    {
        /*====================================================================================================================================
        *  Function    : verifyAccount  -   Method
        *  Description : This function will verify user account in database if it is exist or given info is wrong 
        *  Parameters  : string     username : Username provided by the TMS application userto login in to thhe application.
        *                string     password : Password provided by the TMS application user to login in to the application.
        *  Returns     : bool       loginOk  : True  if the user provided right information of the account otherwise False.
        =======================================================================================================================================*/
        public bool verifyAccount(string username,string password)
        {
            bool loginOK = false;
            
            // code to communicate with the database,account table to verify

            return loginOK;
        }



    }
}
