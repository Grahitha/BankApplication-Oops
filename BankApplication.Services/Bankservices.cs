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
            DataBase.CurrencyInitialization();
        }
        public string CreateBank(string bankname,string bankaddress, string branch,string currencycode)
        {
            if (string.IsNullOrEmpty(bankname))
                throw new Exception("Name is not valid!");
            if (DataBase.Banks.Count != 0 & DataBase.Banks.Any(p => p.Name == bankname) == true)
                throw new Exception("Bank already exists!");
            if(DataBase.curr.Any(p=>p.Key==currencycode)==true)
                throw new Exception("Invalid currency code!");

            Bank bank = new Bank(bankname, bankaddress,branch,currencycode);
            DataBase.Banks.Add(bank);
            return bank.Id;
        }
        public string createaccount(string BankId,string accountHolder, int number, string pass,string gender,int choice)
        {
            string Id;
            if (string.IsNullOrEmpty(accountHolder))
                throw new Exception("Name is not valid!");
            if (bank.BankAccounts.Count != 0 & bank.BankAccounts.Any(p => p.Name == accountHolder) == true)
                throw new Exception("Account already exists!");
            if (DataBase.Banks.Count != 0 & DataBase.Banks.Any(p => p.Id == BankId) != true)
                throw new Exception("Bank doesn't exists!");
            foreach(var i in DataBase.Banks)
            {
                if(i.Id==BankId)
                {
                    bank = i;
                }
            }
            if (choice==1)
            {
                BankAccount ba = new BankAccount(accountHolder, number, pass, gender);
                bank.BankAccounts.Add(ba);
                Id = ba.UserId;
            }
            else
            {
                BankStaff bs = new BankStaff(accountHolder,number,pass,gender);
                bank.StaffAcounts.Add(bs);
                Id = bs.UserId;
            }
            
            return Id;
        }

        public BankAccount login(string Id, string pass)
        {
            BankAccount user = null;
            foreach (BankAccount account in bank.BankAccounts)
            {
                if (account.UserId == Id & account.Password == pass)
                    user = account;

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
