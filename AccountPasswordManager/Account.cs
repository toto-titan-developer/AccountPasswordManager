////
///Summary
///Project Account Password Manager - Project 1 - INFO3138
///Conttributers: Wyatt Henderson, Joe Whitton
///Description: Class files for the Account and Password Objects
///Start Date: May 24, 2026
///Due Date: June 5th 2026
///


namespace AccountInformation
{
    internal class Account
    {
        
        public string? Description { get; set; }
        public string? UserId { get; set; }
        public string? LoginUrl { get; set; }
        public PasswordInfo? PasswordInfo { get; set; }
        public string? Notes { get; set; }

        public Account(string desc = "facebook", string userID ="user101", string loginUrl = "https//www.facebooklogin.com", PasswordInfo? passInfo = default , string notes ="some note")
        {
            Description = desc;
            UserId = userID;
            LoginUrl = loginUrl;
            if(passInfo == default)
            {
                PasswordInfo = new PasswordInfo();
            }
            else
            {
                PasswordInfo = passInfo;
            }

            Notes = notes;
        }
    }

    internal class PasswordInfo
    {
        public string? Password { get; set; }
        public int? StrengthNumber { get; set; }
        public string? StrengthText { get; set; }
        public string? LastReset {  get; set; }

        public PasswordInfo(string pass ="123456", int str=0, string strTxt="very weak", string lastReset="1990-01-01")
        {
            Password = pass;
            StrengthNumber = str;
            StrengthText = strTxt;
            LastReset = lastReset;
        }
    }
}
