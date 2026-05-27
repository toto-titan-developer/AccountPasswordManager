////
///Summary
///Project Account Password Manager - Project 1 - INFO3138
///Conttributers: Wyatt Henderson, Joe Whitton
///Description: Using JSON serialization and parsing to pull and validate a JSOn file of account information with a JSON Schema
///Start Date: May 24, 2026
///Due Date: June 5th 2026
///

using AccountInformation;
using System.Text.Json;
using System.Collections.Generic;

namespace AccountPasswordManager
{
    class Program
    {
        //Declare the constant file path names:
        private const string accountFile = "accountsList.json";
        private const string SchemaFile = "jsonSchema.json";
        private List<Account> accountList = new();


        static void Main(string[] args)
        {
            //Initialize the JSON data if it exists
            if (File.Exists(accountFile))
            {
                //File exists lets read and populate

                //NEED TO ACTION //For Joe

            }
            else
            {
                Console.WriteLine("There are currently no saved accounts.");
            }


            //Enter into a controlled infinite loop with a sentianal value for exiting (Ending the program)
            while (true)
            {
                // add the menu options to the console

                //ask the user for input

                //validate the input


                //Use switch statement to determine selected option

                // If End selection choosen then break
                break;

            }



        }
    }
}
