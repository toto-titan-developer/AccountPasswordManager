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
        private const string AppName = "+------------------------------------------------------------------------+\n" +
                                       "|                            Joe & Wyatt's                               |\n" +
                                       "|                           Password Manager                             |\n" +
                                       "+------------------------------------------------------------------------+\n";
        /// <summary>
        /// Clears the console and adds the AppName header.
        /// Resets the menu in a sense
        /// </summary>
        public void ClearMenu()
        {
            Console.Clear();
            Console.WriteLine(AppName);
        }//End ClearMenu()

        /// <summary>
        /// Clears the specified number of lines.
        /// Must be 0 or more to execute.
        /// Clears the current line
        /// </summary>
        /// <param name="lines"></param>
        public void ClearLine(int lines)
        {
            if(lines < 0) return;
            string blankLine = new string(' ', Console.WindowWidth);
            int count = 0;
            int pos = Console.GetCursorPosition().Top;
            while (lines > count)
            {
                Console.SetCursorPosition(0, pos);
                Console.Write(blankLine);

                pos--;
                count++;
            }
            
            Console.SetCursorPosition(0, pos);
            Console.Write(blankLine);
            Console.SetCursorPosition(0, pos);
        }

        public void DisplayAllEntries(List<Account> list)
        {
            // Header for Page
            Console.WriteLine(
            "+------------------------------------------------------------------------+\n" +
            "|                             Account Entries                            |\n" +                                                        
            "+------------------------------------------------------------------------+\n");

            // Checks for empty list
            if (list.Count == 0)
            {
                Console.WriteLine("There are currently no saved accounts.");
            }

            // Loops through the list and Displays The Account Names
            for (int i = 0; i < list.Count; i++)
            {
                Console.WriteLine($" {i + 1}. {list[i].Description}");
            }
        }// End DisplayAllEntries()

        /// <summary>
        /// Gets the number of days between today and the specified date
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        private int GetNumberOfWeeks(string date)
        {
            // get the last resest date
            var resetDate = DateOnly.Parse(date);

            // get todays date
            var today = DateOnly.FromDateTime(DateTime.Today);

            // Caculate the number of weeks since the last time the password was changed
            return (today.DayNumber - resetDate.DayNumber) / 7;

        }//End GetNumberOfWeeks(string date)

        public List<Account> GetListOfPassNotChanged(List<Account> list, int weeks)
        {
            List<Account> passAccounts = new List<Account>();

            foreach(Account acct in list)
            {
                
                // Check if account Password info is null
                if (acct.PasswordInfo == null) continue;


                int numberOfWeeks = GetNumberOfWeeks(acct.PasswordInfo.LastReset);

                // Check if the number of weeks since being changed is greater than or equal to weeks
                if(numberOfWeeks >= weeks)
                    passAccounts.Add(acct);
                

            }
            DisplayPassNotChanged(list, passAccounts, weeks);
            return passAccounts;
        }// End GetListOfPassNotChanged(List<Account> list, int weeks)

        /// <summary>
        /// Private method to print the list of Accounts with password greater or equal to the specified number of weeks.
        /// </summary>
        /// <param name="accts">List of all account data</param>
        /// <param name="passAccounts">List of accounts that last reset is great or equal to the specified number of weeks</param>
        /// <param name="weeks">Specified number of weeks</param>
        private void DisplayPassNotChanged(List<Account> accts, List<Account> passAccounts, int weeks)
        {
            Console.WriteLine("+------------------------------------------------------------------------+\n" +
                             $"|        Accounts With Passwords That Are {weeks} Or More Week(s) Old          |\n" +
                              "+------------------------------------------------------------------------+\n");


            if(passAccounts.Count <= 0)
            {
                Console.WriteLine("+------------------------------------------------------------------------+\n" +
                                 $"|      There are no accounts with a password {weeks} Or More Week(s) Old       |\n" +
                                  "+------------------------------------------------------------------------+\n");
                return;
            }

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
        }// End DisplayPassNotChanged()


        public void DisplayMainOptions()
        {
            Console.WriteLine(
                    "\nPress # from the above list to select an entry\n" +
                    "Press A to list accounts by password age.\n" +
                    "Press N to add a new entry.\n" +
                    "Press X to quit.");
        }//End DisplayMainOptions()

        public void DisplayPasswordOptions()
        {
            Console.WriteLine(
                    "\nPress # from the above list to select an entry. \n" +
                    "Press M to return to the main menu.");
        }//End DisplayPasswordOptions()
        public void DisplayUpdateOptions()
        {
            //I just copied the MainOptions...
            Console.WriteLine(
                    "\nPress P to change this password.\n" +
                    "Press D to delete this entry.\n" +
                    "Press M to return to the main menu." +
                    "\n\n+------------------------------------------------------------------------+");
        }//End DisplayPasswordOptions()

        public void SelectAccount(List<Account> accountList, int n)
        {
            {
                //keeps the program header by using ClearMenu()
                ClearMenu(); 
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
        }// End SelectedAccount()

        public void DisplayAddAccount()
        {
            Console.WriteLine(
                "Please key-in values for the following fields...\n\n" +
                "Description:\n" +
                "User ID:\n" +
                "Password:\n" +
                "Login url:\n" +
                "Notes:"
                );
        }// End DisplayAddAccount()

        public void DeleteAccount(List<Account> accountList, int n)
        {
            // Assigns variable the account to delete
            Account accountToDelete = accountList[n];

            // Makes sure the user wants to delete this account
            Console.WriteLine("\nAre you sure you want to delete this account? (Y/N)");
            string input = Console.ReadLine();

            // checks for a yes or no
            if (input != null && input.ToUpper() == "Y")
            {
                // Removes the account at a specified index
                accountList.RemoveAt(n);
                Console.WriteLine("\nAccount deleted successfully.");
            }
            else
            {
                // Prevented deletion
                Console.WriteLine("\nDelete cancelled.");
            }
        }// End DeleteAccount()

        public void EditAccountPassword(List<Account> accountList, int n)
        {
            while (true)
            {
                // Promp user for new password
                Console.WriteLine("Enter new password: ");
                string newPassword = Console.ReadLine();

                //Validate the password
                if (string.IsNullOrWhiteSpace(newPassword))
                {
                    Console.WriteLine("Password cannot be empty.");
                    continue;
                }
                // Validate password using your validator
                if (!Program.IsPasswordValid(newPassword))
                {
                    Console.WriteLine("Password does not meet the requirements.");
                    continue;
                }
                else
                {

                    // Update the old password
                    accountList[n].PasswordInfo.Password = newPassword;

                    // update reset date
                    accountList[n].PasswordInfo.LastReset = DateTime.Today.ToString("yyyy-MM-dd");
                    //Print success!
                    Console.WriteLine("Password updated successfully.");
                    break;
                }
            }

        }// End EditAccountPassword()


    }
}
