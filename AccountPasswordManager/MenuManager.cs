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
     
        public void ClearMenu()
        {
            int endRow = 4;

            int start = Console.GetCursorPosition().Top;
            string blankLine = new string(' ', Console.WindowWidth);
            for(int i = start; i > endRow; i--)
            {
                Console.SetCursorPosition(0, i);
                Console.Write(blankLine);
            }
            Console.SetCursorPosition(0, endRow);
        }

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
        }

        public void DisplayPassChangedN(List<Account> list, int weeks)
        {

        }

        private int GetNumberOfWeeks(string date)
        {
            // get the last resest date
            var resetDate = DateOnly.Parse(date);
            //get todays date
            var today = DateOnly.FromDateTime(DateTime.Today);
            //Caculate the number of weeks since the last time the password was changed
            return (today.DayNumber - resetDate.DayNumber) / 7;
        }

        public List<Account> GetListOfPassNotChanged(List<Account> list, int weeks)
        {
            List<Account> passAccounts = new List<Account>();

            foreach(Account acct in list)
            {
                
                //Check if account Password info is null
                if (acct.PasswordInfo == null) continue;


                int numberOfWeeks = GetNumberOfWeeks(acct.PasswordInfo.LastReset);

                //Check if the number of weeks since being changed is greater than or equal to weeks
                if(numberOfWeeks >= weeks)
                    passAccounts.Add(acct);
                

            }
            DisplayPassNotChanged(list, passAccounts, weeks);
            return passAccounts;
        }

        public void DisplayPassNotChanged(List<Account> accts, List<Account> passAccounts, int weeks)
        {
            Console.WriteLine("+------------------------------------------------------------------------+\n" +
                              $"|         Accounts With Passwords That Are {weeks} Or More Week(s) Old        |\n" +
                              "+------------------------------------------------------------------------+\n");

            int index = 1;
            foreach (Account acct in accts)
            {
                if(passAccounts.Contains(acct))
                {
                    if(acct.PasswordInfo == null) continue;

                    Console.WriteLine(
                        $" {index}. {acct.Description}, {GetNumberOfWeeks(acct.PasswordInfo.LastReset)} week(s)"
                        );
                }
                index++;
            }
        }


        public void DisplayMainOptions()
        {
            //Needs updating
            Console.WriteLine(
                    "\nPress # from the above list to select an entry\n" +
                    "Press A to list accounts by password age.\n" +
                    "Press N to add a new entry.\n" +
                    "Press X to quit.");
        }
        public void DisplayPasswordOptions()
        {
            //I just copied the MainOptions...
            Console.WriteLine(
                    "\nPress # from the above list to select an entry. \n" +
                    "Press M to return to the main menu.");
        }
        public void DisplayUpdateOptions()
        {
            //I just copied the MainOptions...
            Console.WriteLine(
                    "\nPress P to change this password.\n" +
                    "Press D to delete this entry.\n" +
                    "Press M to return to the main menu.");
        }

        public void SelectAccount(List<Account> list, int n)
        {
            //checks
            if (n < 0 && n >= list.Count())
            { 
                
            }

        }


        public void AddNewAccount(List<Account> accounts)     //Validate before adding to the List
        {

        }

        public void DeleteAccount()
        {

        }

        public void EditAccountPassword()
        {

        }


    }
}
