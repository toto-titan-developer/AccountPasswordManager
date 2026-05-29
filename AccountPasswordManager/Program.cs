////
///Summary
///Project Account Password Manager - Project 1 - INFO3138
///Conttributers: Wyatt Henderson, Joe Whitton
///Description: Using JSON serialization and parsing to pull and validate a JSOn file of account information with a JSON Schema
///Start Date: May 24, 2026
///Version: 1.01
///Due Date: June 5th 2026
///

using Json.Schema;
using Json.Schema.Keywords;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace AccountPasswordManager
{
    class Program
    {
        //Declare the constant file path names:
        private const string accountFile = "accountsList.json";
        private const string SchemaFile = "jsonSchema.json";
        //Gloabal Variables
        private static List<Account>? accountList = new List<Account>();
        private static MenuManager MenuManager = new MenuManager();

        private static bool programRunning = true;

        private static string mainRegex = @"^[ANX]$|^[1-9]\d*$";

        private const string AppName = "+------------------------------------------------------------------------+\n" +
                                       "|                            Joe & Wyatt's                               |\n" +
                                       "|                           Password Manager                             |\n" +
                                       "+------------------------------------------------------------------------+\n";


        static void Main(string[] args)
        {
            Console.Write(AppName);

            // Initialize the JSON data if it exists
            if (File.Exists(accountFile))
            {
                // File exists lets read and populate
                string json = File.ReadAllText(accountFile);
                accountList = JsonSerializer.Deserialize<List<Account>>(json);
            }
            else
            {
                accountList = new();
                Console.WriteLine("There are currently no saved accounts.");
            }


            //Enter into a controlled infinite loop with a flag value for exiting (Ending the program)
            while (programRunning)
            {
                // AccountList call to display 
                MenuManager.DisplayAllEntries(accountList);

                // add the menu options to the console
                MenuManager.DisplayMainOptions();

                // ask the user for input

                Console.Write("Enter a command: ");
                char input = Console.ReadKey().KeyChar;

                //Validate the entry
                while(true)
                {
                    if(Regex.IsMatch(Char.ToUpper(input).ToString(), mainRegex))
                    { break; }

                    //Get New Entry -> display invalid entry
                    Console.Write("Enter a command: ");
                    input = Console.ReadKey().KeyChar;
                }


                
 
                //Use switch statement to determine selected option
                //Implement a switch statemet for select #, A, N, X
                switch (Char.ToUpper(input))
                {
                    case 'X':
                        return;

                    case 'N':
                        // Add Account goes here
                        break;

                    case 'A':
                        // Display Old Passwords
                        break;

                    default:
                        // Checks to see if the input was a number
                        if (Char.IsDigit(input))
                        {
                            // Passes the input -1, because an array starts at 0
                            int index = int.Parse(input.ToString()) - 1;

                            // Validates Range of input
                            if (index >= 0 && index < accountList.Count)
                            { MenuManager.SelectAccount(accountList, index); }
                        }
                        break;
                }
                if(Char.ToUpper(input) == 'X')
                {
                    switch (Char.ToUpper(input))
                    {
                        case 'A':
                            int numWeeks = 0;
                            while(true)
                            {
                                try
                                {
                                    Console.Write("\nEnter minimum password age in weeks: ");
                                    int weeks = int.Parse(Console.ReadLine());
                                    numWeeks = weeks;
                                    if (numWeeks > 0)
                                    {
                                        break;
                                    }
                                }
                                catch(Exception e)
                                {
                                    Console.WriteLine($"{e.Message}");
                                }
                                
                            }
                            MenuManager.ClearMenu();
                            List<Account> passAccts = MenuManager.GetListOfPassNotChanged(accountList, numWeeks);
                            MenuManager.DisplayPasswordOptions();
                            Console.Write("Enter a command: ");
                            input = Console.ReadKey().KeyChar;

                            MenuManager.ClearMenu();

                            break;
                        case 'N':

                            break;
                        case 'X':
                            programRunning = false; //ends the program
                            break;

                    }
                }
                

                //CLEARS THE MENU SO THAT WE CAN HAVE A TITLE AND THE CONSOLE WILL SEEM DYNAMIC
                //NEEDS TO BE MOVED BUT CHECK IT OUT
                //MenuManager.ClearMenu();

            }
        }



        // Can be moved....
        public static bool ValidateAccount(Account account)
        {
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
                    schema.Evaluate(document.RootElement);

                // If validation fails, display errors
                if (!results.IsValid)
                {
                    Console.WriteLine("\nERROR: Account validation failed:\n");

                    // Loop through validation details
                    foreach (var detail in results.Details)
                    {
                        // Gets erros to display what they are missing
                        if (detail.Errors != null)
                        {
                            foreach (var error in detail.Errors)
                            {
                                // Prints each error
                                Console.WriteLine($"ERROR: {error.Value}");
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


    }
}
