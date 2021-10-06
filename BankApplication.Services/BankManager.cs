using BankApplication.Models;
using System;
using System.Linq;

namespace BankApplication.Services
{
    public class BankManager
    {
        public Bank bank;
        public BankManager()
        {
            this.bank = new Bank("IndiaBank", 2, "Kollipara", "SBI");
    }
        public void createaccount(string accountHolder, int number, string pass)
        {

            if (string.IsNullOrEmpty(accountHolder))
                throw new Exception("Name is not valid!");
            if (bank.BankAccounts.Count != 0 & bank.BankAccounts.Any(p => p.name == accountHolder) == true)
                throw new Exception("Account already exists!");

            BankAccount ba = new BankAccount();
            ba.name = accountHolder;
            ba.phoneNumber = number;
            ba.id = bank.BankAccounts.Count + 1;
            ba.bankId = bank.id;
            ba.password = pass;
            bank.BankAccounts.Add(ba);
            Bank.transactions.Add(accountHolder + " account created successfully!");
        }
    }
}
