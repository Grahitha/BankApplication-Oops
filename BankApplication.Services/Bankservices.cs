using BankApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BankApplication.Services
{
    public class Bankservices
    {
        private Bank bank;
        public Bankservices()
        {
            this.bank = new Bank("IndiaBank","Kollipara", "SBI");
        }
        public string createaccount(string accountHolder, int number, string pass,string gender)
        {

            if (string.IsNullOrEmpty(accountHolder))
                throw new Exception("Name is not valid!");
            if (bank.BankAccounts.Count != 0 & bank.BankAccounts.Any(p => p.Name == accountHolder) == true)
                throw new Exception("Account already exists!");

            BankAccount ba = new BankAccount(accountHolder,number,pass,gender);
            bank.BankAccounts.Add(ba);
            return ba.UserId;
        }

        public BankAccount login(string Id, string pass)
        {
            BankAccount user = null;
            foreach (BankAccount account in bank.BankAccounts)
            {
                if (account.UserId == Id & account.Password == pass)
                {
                    user = account;
                }

            }
            return user;
        }
        public BankAccount checkAccount(string accountHolder)
        {
            BankAccount user = null;
            foreach (BankAccount account in bank.BankAccounts)
            {
                if (account.Name == accountHolder)
                    user = account;
            }
            return user;
        }
        public void deposit(BankAccount user,decimal amt)
        {
            user.Balance += amt;
            Transactions trans = new Transactions(amt,0,user.UserId,user.UserId,bank.Id);
            user.Transactions.Add(trans);
        }
        public bool withdraw(BankAccount user,decimal amt)
        {
            if (user.Balance >= amt)
            {
                user.Balance -= amt;
                Transactions trans = new Transactions(amt,1, user.UserId, user.UserId,bank.Id);
                user.Transactions.Add(trans);
                return true;
            }
            return false;
        }
        public bool transfer(BankAccount user,decimal amt, BankAccount rcvr)
        {
            if (user.Balance >= amt)
            {
                user.Balance -= amt;
                rcvr.Balance += amt;
                Transactions trans = new Transactions(amt,1,user.UserId,rcvr.UserId,bank.Id);
                user.Transactions.Add(trans);
                Transactions rcvrtrans = new Transactions(amt,0, rcvr.UserId, user.UserId,bank.Id);
                rcvr.Transactions.Add(rcvrtrans);
                return true;
            }
            return false;
        }
        public decimal viewBalance(BankAccount user)
        {
            return user.Balance;
        }

    }
}
