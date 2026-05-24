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

namespace AccountPasswordManager
{
    class Program
    {
        //Declare the constant file path names:
        private const string accountFile = "accountsList.json";
        private const string SchemaFile = "jsonSchema.json";


        static void Main(string[] args)
        {
            Console.WriteLine("Hello party people!");

            Account acct = new Account();



            string json = JsonSerializer.Serialize(acct);

            File.WriteAllText(accountFile, json);

            try
            {
                string accounts = File.ReadAllText(accountFile);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading the json file {ex.Message}");
            }

        }
    }
}
