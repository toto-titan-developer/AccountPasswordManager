////
///Summary
///Project Account Password Manager - Project 1 - INFO3138
///Conttributers: Wyatt Henderson, Joe Whitton
///Description: Class files for the Account and Password Objects
///Start Date: May 24, 2026
///Due Date: June 5th 2026
///


namespace AccountPasswordManager
{
    internal class Account
    {
        
        public string? Description { get; set; }
        public string? UserId { get; set; }
        public string? LoginUrl { get; set; }
        public PasswordInfo? PasswordInfo { get; set; }
        public string? Notes { get; set; }
    }

    internal class PasswordInfo
    {
        public string? Password { get; set; }
        public int? StrengthNumber { get; set; }
        public string? StrengthText { get; set; }
        public string? LastReset {  get; set; }
    }
}
