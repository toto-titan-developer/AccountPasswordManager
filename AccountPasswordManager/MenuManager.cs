using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountPasswordManager
{
    internal class MenuManager
    {
        //private static int maxLineLength = 50; 
     
        public void ClearMenu(int endRow)
        {
            int start = Console.GetCursorPosition().Top;
            string blankLine = new string(' ', Console.WindowWidth);
            for(int i = start; i > endRow; i--)
            {
                Console.SetCursorPosition(0, i);
                Console.Write(blankLine);
            }
            Console.SetCursorPosition(0, endRow);
        }//End ClearMenu()

        public void DisplayAllEntries(List<Account> list)
        {
            //Incrementing number for account
            int accountNumber = 0;

            //Header for Page
            Console.WriteLine(
            "+------------------------------------------------------------------------+\n" +
            "|                             Account Entries                            |\n" +                                                        
            "+------------------------------------------------------------------------+\n");

            //Loops through the list and Displays The Account Names
            foreach (Account account in list)
            {
                accountNumber++;
                
                Console.WriteLine(
                    $" {accountNumber}.{account.Description}");
            }
        }//End DisplayAllEntries()

        public void DisplayPassChangedN(List<Account> list, int weeks)
        {

        }//End DisplayPassChangedN()

        public void DisplayMainOptions()
        {
            //Needs updating
            Console.WriteLine(
                    "\nPress # from the above list to select an entry\n" +
                    "Press A to list accounts by password age.\n" +
                    "Press N to add a new entry.\n" +
                    "Press X to quit.");
        }//End DisplayMainOptions()

        public void DisplayPasswordOptions()
        {
            //I just copied the MainOptions...
            Console.WriteLine(
                    "\nPress P to change this password.\n" +
                    "Press D to delete this entry.\n" +
                    "Press M to return to the main menu.");
        }//End DisplayPasswordOptions()

        public void SelectAccount(List<Account> accountList, int n)
        {
            {
                Console.Clear();
                Account aView = accountList[n]; 

                Console.WriteLine(
                "+------------------------------------------------------------------------+");

                Console.WriteLine($" {n + 1}. {aView.Description}");

                Console.WriteLine(
                "+------------------------------------------------------------------------+");

                Console.WriteLine($" User ID:           {aView.UserId}");
                Console.WriteLine($" Password:          {aView.PasswordInfo.Password}");

                Console.WriteLine(
                    $" Password Strength: {aView.PasswordInfo.StrengthText} ({aView.PasswordInfo.StrengthNumber})");

                Console.WriteLine(
                    $" Password Reset:    {aView.PasswordInfo.LastReset:yyyy-MM-dd}");

                Console.WriteLine($" Login URL:         {aView.LoginUrl}");
                Console.WriteLine($" Notes:             {aView.Notes}");

                Console.WriteLine(
                "+------------------------------------------------------------------------+");
            }
        }//End SelectedAccount()


        public void AddNewAccount(List<Account> accounts)     //Validate before adding to the List
        {

        }//End AddNewAccount()

        public void DeleteAccount()
        {

        }//End DeleteAccount()

        public void EditAccountPassword()
        {

        }//End EditAccountPassword()


    }
}
