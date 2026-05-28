////
///Summary
///Project Account Password Manager - Project 1 - INFO3138
///Conttributers: Wyatt Henderson, Joe Whitton
///Description: Using JSON serialization and parsing to pull and validate a JSOn file of account information with a JSON Schema
///Start Date: May 24, 2026
///Version: 1.01
///Due Date: June 5th 2026
///

using System.Text.Json;
using System.Text.Json.Serialization;
using System.Collections.Generic;

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

        private const string AppName = "+------------------------------------------------------------------------+\n" +
                                       "|                            Joe & Wyatt's                               |\n" +
                                       "|                           Password Manager                             |\n" +
                                       "+------------------------------------------------------------------------+\n";


        static void Main(string[] args)
        {
            Console.Write(AppName);

            //Initialize the JSON data if it exists
            if (File.Exists(accountFile))
            {
                //File exists lets read and populate
                string json = File.ReadAllText(accountFile);
                accountList = JsonSerializer.Deserialize<List<Account>>(json);
            }
            else
            {
                Console.WriteLine("There are currently no saved accounts.");
            }


            //Enter into a controlled infinite loop with a sentinel value for exiting (Ending the program)
            while (true)
            {
                // add the menu options to the console
                MenuManager.DisplayMainOptions();

                //ask the user for input

                Console.Write("Enter a command: ");
                char input = Console.ReadKey().KeyChar;

                //CLEARS THE MENU SO THAT WE CAN HAVE A TITLE AND THE CONSOLE WILL SEEM DYNAMIC
                //NEEDS TO BE MOVED BUT CHECK IT OUT
                MenuManager.ClearMenu(4);

                //validate the input

 
                //Use switch statement to determine selected option
                //Implement a switch statemet for select #, A, N, X
                if(Char.ToUpper(input) == 'X')
                {
                    break;
                }
            }
        }
    }
}
