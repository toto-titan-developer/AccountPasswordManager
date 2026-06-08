////
///Summary
///Project Account Password Manager - Project 1 - INFO3138
///Contributers: Wyatt Henderson StudentID: 1297741, Joe Whitton StudentID: 1313038
///Description: Using JSON serialization and parsing to pull and validate a JSON file of account information with a JSON Schema
///Start Date: May 24, 2026
///Version: 1.01
///Due Date: June 5th 2026
///

using Easy_Password_Validator;
using Easy_Password_Validator.Models;
using Json.Schema;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace AccountPasswordManager
{
    class Program
    {
        //Declare the constant file path names:
        private const string accountFile = "accountsList.json";
        private const string SchemaFile = "jsonSchema.json";
        //Gloabal Variables
        private static List<Account> accountList = new List<Account>();
        private static MenuManager MenuManager = new MenuManager();
        private static PasswordValidatorService passwordValidator = new PasswordValidatorService(new PasswordRequirements());

        private static bool programRunning = true;

        private static string mainRegex = @"^[ANX]$|^\d+";

        


        static void Main(string[] args)
        {
            MenuManager.ClearMenu();

            // Initialize the JSON data if it exists
            if (File.Exists(accountFile))
            {
                try
                {
                    // File exists lets read and populate
                    string json = File.ReadAllText(accountFile);
                    var tempList = JsonSerializer.Deserialize<List<Account>>(json) ?? new List<Account>();


                    accountList.AddRange(tempList);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                
            }
            else
            {
                accountList = new();
            }


            // Enter into a controlled infinite loop with a flag value for exiting (Ending the program)
            while (programRunning)
            {
                // Valid input comes from method. Only a number from 1 - 9 or A, N or X
                string input = HandleMainMenu();

                // Use switch statement to determine selected option
                // Implement a switch statemet for select #, A, N, X
                // Selecting an account by number
                if (int.TryParse(input, out int index))
                {
                    index--; //decrement to match indexs
                    if (index >= 0 && index < accountList.Count)
                    {
                        MenuManager.SelectAccount(accountList, index);
                        
                        HandleUpdateMenu(accountList, index);
                      
                        // Clear Menu and Reset for main menu
                        MenuManager.ClearMenu();
                    }
                    else
                    {
                        MenuManager.ClearMenu();
                        Console.WriteLine($"Select '{index + 1}' is not an option from above. ");
                    }
                }
                else
                {
                    switch (Char.ToUpper(input[0]))
                    {
                        // Display Old Passwords
                        case 'A':
                            HandleOldPasswordSelection();
                            break;

                        // Add Account goes here
                        case 'N':
                            HandleAddNewAccount();
                            break;

                        // Exit the console application
                        case 'X':
                            programRunning = false; //ends the program
                            break;
                    }
                }
            }
        }

        /// <summary>
        /// Handles the initialization of the main menu and gets the validated entry to check against the switch statement.
        /// This controls the main selection page
        /// Ensures that the entry either meets a char selection or a valid number selection
        /// </summary>
        /// <returns>char which is the validated input from the user</returns>
        private static string HandleMainMenu()
        {
            // AccountList call to display 
            MenuManager.DisplayAllEntries(accountList);

            // add the menu options to the console
            MenuManager.DisplayMainOptions();

            // ask the user for input
            Console.Write("Enter a command: ");
            string input = Console.ReadLine() ?? "";

            //Validate the entry. Make sure either A, N, or X, Or a Number above 0 is entered
            bool hadError = false;
            while (true)
            {
                if (Regex.IsMatch(input.ToUpper(), mainRegex))
                { 
                    
                    if(int.TryParse(input, out int index))
                    {
                        index--;
                        //If the index is in range (BREAK)
                        if (index >= 0 && index < accountList.Count)
                        {
                            break;
                        }
                    }
                    else
                    {
                        //Not a number and matches REGEX (VALID ENTRY BREAK)
                        break;
                    }
                    
                
                }

                //Get New Entry -> display invalid entry
                if (hadError)
                {
                    MenuManager.ClearLine(2);
                }
                else
                {
                    MenuManager.ClearLine(1);
                }

                Console.WriteLine($"Invalid entry of '{input}', Select from Menu");
                Console.Write("Enter a command: ");
                input = Console.ReadLine() ?? "";
                hadError = true;
            }

            //remove the error line
            if (hadError)
            {
                MenuManager.ClearLine(1);
                Console.Write($"Enter a command: {input}");
            }

            return input;
        }


        /// <summary>
        /// Handles the Menu Selection for the account manipulations,
        /// Allows a user to select a option from a list
        /// We could make a method that contains
        /// </summary>
        private static void HandleUpdateMenu(List<Account> accountList, int n)
        {
            bool running = true;
            MenuManager.DisplayUpdateOptions();

            while (running)
            {
               
                Console.Write("Enter a Command: ");
                char input = Console.ReadKey().KeyChar;

                switch (Char.ToUpper(input))
                {
                    case 'P':
                        MenuManager.EditAccountPassword(accountList, n);
                           break;
                    case 'D':
                        MenuManager.DeleteAccount(accountList, n);
                        running = false;
                        break;
                       
                    case 'M':
                        running = false;
                        MenuManager.ClearMenu();
                        MenuManager.DisplayAllEntries(accountList);
                        MenuManager.DisplayMainOptions();
                        break;

                    default:
                        Console.WriteLine("Invalid option. Try Again.");
                        
                            break;
                }
            }
        }


        /// <summary>
        /// Handles the functionality of selection option for list of old passwords.
        /// Includes functionality of selecting the entry (This will need to be refactored in)
        /// We could make a method that contains
        /// </summary>
        private static void HandleOldPasswordSelection()
        {
            int numWeeks = 0;
            bool hadError = false;
            string input;
            //Validation of selecting a whole number for how many weeks
            while (true)
            {
                try
                {
                    Console.Write("\nEnter minimum password age in weeks: ");
                    var str = Console.ReadLine();
                    int weeks = int.Parse(str!);
                    numWeeks = weeks;
                    if (numWeeks >= 0)
                    {
                        break;
                    }
                    else if(numWeeks < 0)
                    {
                        if (hadError)
                        {
                            MenuManager.ClearLine(3);
                        }
                        else
                        {
                            MenuManager.ClearLine(1);
                        }

                        Console.WriteLine($"Invalid entry of {numWeeks}. Must be 0 or greater");
                        hadError = true;
                    }
                }
                catch (Exception e)
                {


                    if (hadError)
                    {
                        MenuManager.ClearLine(4);
                    }
                    else
                    {
                        MenuManager.ClearLine(1);
                    }

                    Console.WriteLine($"{e.Message} \n" +
                        $"Enter a Whole Number");
                    hadError = true;
                }

            }
            MenuManager.ClearMenu();
            List<Account> passAccts = MenuManager.GetListOfPassNotChanged(accountList, numWeeks);
            MenuManager.DisplayPasswordOptions();

            hadError = false;
            while (true)
            {
                Console.Write("Enter a command: ");
                input = Console.ReadLine() ?? "";

                if (int.TryParse(input, out int index))
                {
                    index--;
                    if (index >= 0 && index < accountList.Count && passAccts.Contains(accountList[index]))
                    {
                        MenuManager.SelectAccount(accountList, index);
                        HandleUpdateMenu(accountList, index);

                        //Clear Menu and Reset for main menu
                        MenuManager.ClearMenu();
                        break;
                    }
                    else
                    {
                        if (hadError)
                            MenuManager.ClearLine(2);
                        else
                            MenuManager.ClearLine(1);

                        Console.WriteLine($"Select '{index + 1}' is not an option from above. ");
                        hadError = true;
                    }
                }
                else if (Char.ToUpper(input[0]) == 'M' && input.Length == 1)
                {
                    //Clears the menu and breaks the loop to go back to our original loop controlling main menu
                    MenuManager.ClearMenu();
                    break;
                }
                else
                {
                    if (hadError)
                        MenuManager.ClearLine(2);
                    else
                        MenuManager.ClearLine(1);

                    Console.WriteLine($"Invalid entry of '{input}' try again");
                    hadError = true;
                }
            }
        }

        /// <summary>
        /// Handles the menu functionality of adding a new account to memory.
        /// </summary>
        private static void HandleAddNewAccount()
        {
            bool addAcct;
            Account newAccount = new Account();
            const int startCol = 25;
            const int descLine = 7;
            const int userIDLine = 8;
            const int passLine = 9;
            const int loginLine = 10;
            const int noteLine = 11;
            const int confLine = 13;

            string blankLine = new string(' ', Console.WindowWidth - startCol);

            bool hadError = false;

            MenuManager.ClearMenu();
            MenuManager.DisplayAddAccount();
            Console.Write("\nAdd new account?  (Y/N): ");

            
            while (true)
            {
                Console.SetCursorPosition(startCol, descLine);
                if(hadError)
                {
                    Console.Write(blankLine);
                    Console.SetCursorPosition(startCol, descLine);
                }
                newAccount.Description = Console.ReadLine() ?? "";

                Console.SetCursorPosition(startCol, userIDLine);
                if (hadError)
                {
                    Console.Write(blankLine);
                    Console.SetCursorPosition(startCol, userIDLine);
                }
                newAccount.UserId = Console.ReadLine() ?? "";

                Console.SetCursorPosition(startCol, passLine);
                if (hadError)
                {
                    Console.Write(blankLine);
                    Console.SetCursorPosition(startCol, passLine);
                }
                newAccount.PasswordInfo.Password = Console.ReadLine() ?? "";

                Console.SetCursorPosition(startCol, loginLine);
                if (hadError)
                {
                    Console.Write(blankLine);
                    Console.SetCursorPosition(startCol, loginLine);
                }
                newAccount.LoginUrl = Console.ReadLine() ?? "";

                Console.SetCursorPosition(startCol, noteLine);
                if (hadError)
                {
                    Console.Write(blankLine);
                    Console.SetCursorPosition(startCol, noteLine);
                }
                newAccount.Notes = Console.ReadLine() ?? "";

                Console.SetCursorPosition(startCol, confLine);
                if (hadError)
                {
                    Console.Write(blankLine);
                    Console.SetCursorPosition(startCol, confLine);
                }
                addAcct = Char.ToUpper(Console.ReadKey().KeyChar) == 'Y';
                Console.WriteLine(); //for spacing for the error messages

                if (addAcct)
                {
                    //Perform finalizing the account -> Get Password strengthNum, strengthText, LastReset(today)

                    TestNUpdatePassInfo(newAccount);


                    //Validate that the account is valid.
                    bool isValid = ValidateAccount(newAccount, out List<string> messages);

                    //if it is valid add the account and return to the main menu
                    if (isValid)
                    {
                        AddToAccountList(newAccount);
                        break;
                    }


                    //Clear all previous errors
                    Console.SetCursorPosition(0, Console.GetCursorPosition().Top + 6);
                    MenuManager.ClearLine(6);


                    messages.ForEach(msg => Console.WriteLine(msg));
                    //else it isn't valid, Identify what is wrong and then retype those lines
                    hadError = true;
                }
                else { break; }
            }
            
            MenuManager.ClearMenu();
        }

        /// <summary>
        /// Used for the initial creation of an account to test the entered password and generate a Password Strength and Password Strength Test.
        /// Works on the real McCoy!
        /// </summary>
        /// <param name="acct"></param>
        private static void TestNUpdatePassInfo(Account acct)
        {
            //Perform test of password strength.
            passwordValidator.TestAndScore(acct.PasswordInfo.Password);

            //add the password strength score to the new account.
            acct.PasswordInfo.StrengthNum = passwordValidator.Score;

            int score = acct.PasswordInfo.StrengthNum;

            if (score < -40)
            {
                acct.PasswordInfo.StrengthText = "very weak";
            }
            else if (score >= -40 && score < 0)
            {
                acct.PasswordInfo.StrengthText = "weak";
            }
            else if (score >= 0 && score < 40)
            {
                acct.PasswordInfo.StrengthText = "medium";
            }
            else if (score >= 40 && score < 80)
            {
                acct.PasswordInfo.StrengthText = "strong";
            }
            else if (score >= 80)
            {
                acct.PasswordInfo.StrengthText = "very strong";
            }
        }


        /// <summary>
        /// Takes an account object converts it into a JSON as well as the SCHEMA into a Schema object
        /// Parses the JSON into a JsonDocument to be validated against the Schema
        /// We could make a method that contains
        /// </summary>
        public static bool ValidateAccount(Account account, out List<string> messages)
        {
            messages = new List<string>();
            try
            {
                // Read the JSON schema file
                string schemaText =
                    File.ReadAllText(SchemaFile);

                // Convert schema text into a JsonSchema object
                JsonSchema schema =
                    JsonSchema.FromText(schemaText);

                // Serialize the account object into JSON
                string json =
                    JsonSerializer.Serialize(account);

                // Parse JSON into a JsonDocument
                JsonDocument document =
                    JsonDocument.Parse(json);

                // Validate JSON against schema
                EvaluationResults results =
                    schema.Evaluate(document.RootElement, new EvaluationOptions
                    {
                        OutputFormat = OutputFormat.List,
                        RequireFormatValidation = true
                    });

                // If validation fails, display errors
                if (!results.IsValid)
                {
                    if (results.Details != null)
                    {
                        // Loop through validation details
                        foreach (var detail in results.Details) 
                        {
                            // Gets errors to display what they are missing
                            if (detail.Errors != null)
                            {
                                foreach (var error in detail.Errors)
                                {
                                    // Prints each error
                                    int index = detail.InstanceLocation.SegmentCount - 1;
                                    string property = detail.InstanceLocation.GetSegment(index).ToString();
                                    JsonElement? failedValue = detail.InstanceLocation.Evaluate(document.RootElement);
                                    messages.Add($"ERROR:{property}:\"{failedValue}\" {error.Value}");
                                }
                            }
                        }
                    }

                    // Validation failed
                    return false;
                }

                // Validation passed
                return true;
            }
            catch (Exception ex)
            {
                // Handles unexpected issues
                Console.WriteLine($"\nERROR: Validation system failure: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Adds an account to the active list and updates the Json data file
        /// </summary>
        /// <param name="acct">passes a Account object with all its data completed</param>
        private static void AddToAccountList(Account acct)
        {
            //add new account to the accountList
            accountList.Add(acct);

            //Serialize the list and update the json file;
            string json = JsonSerializer.Serialize(accountList);
            File.WriteAllTextAsync(accountFile, json);

        }


    }
}
