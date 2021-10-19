using System;
using System.Collections.Generic;

namespace BankApplication.Models
{
    public class BankAccount
    {
        public string Name { get; set; }
        public int PhoneNumber { get; set; }
        public string UserId { get; set; }
        public int BankId { get; set; }
        public string Password { get; set; }
        public decimal Balance = 0;
        public string Gender { get; set; }

        public List<Transactions> Transactions = new List<Transactions>();
        public BankAccount(string AccountHolderName,int PhoneNumber,string pass,string gender)
        {
            this.Name = AccountHolderName;
            this.PhoneNumber = PhoneNumber;
            this.UserId = BankAccountIdGenerator(AccountHolderName);
            this.Password = pass;
            this.Gender = gender;
        }
        public string  BankAccountIdGenerator(string accountHolderName)
        {
            string dt= DateTime.Now.ToString("yyyyMMddHHmmss");
            string accountId = accountHolderName.Substring(0, 3) + dt;
            return accountId;
        }
        
    }

}
