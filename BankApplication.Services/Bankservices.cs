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
        public string CreateBank(string bankname, string bankaddress, string branch, string currencycode)
        {
            if (string.IsNullOrEmpty(bankname))
                throw new Exception("Name is not valid!");
            if (DataBase.Banks.Count != 0 & DataBase.Banks.Any(p => p.Name == bankname) == true)
                throw new Exception("Bank already exists!");
            if (!DataBase.curr.ContainsKey(currencycode))
                throw new Exception("Invalid currency code!");

            Bank bank = new Bank(bankname, bankaddress, branch, currencycode);
            DataBase.Banks.Add(bank);
            return bank.Id;
        }
        public string createaccount(string BankId, string accountHolder, int number, string pass, string gender, int choice)
        {
            string Id;
            bank = FindBank(BankId);
            
            if (string.IsNullOrEmpty(accountHolder))
                throw new Exception("Name is not valid!");
            if (bank.BankAccounts.Count != 0 & bank.BankAccounts.Any(p => p.Name == accountHolder) == true)
                throw new Exception("Account already exists!");
            if (DataBase.Banks.Count != 0 & DataBase.Banks.Any(p => p.Id == BankId) != true)
                throw new Exception("Bank doesn't exists!");

            if (choice == 1)
            {
                BankAccount ba = new BankAccount(accountHolder, number, pass, gender);
                bank.BankAccounts.Add(ba);
                Id = ba.UserId;
            }
            else
            {
                BankStaff bs = new BankStaff(accountHolder, number, pass, gender);
                bank.StaffAcounts.Add(bs);
                Id = bs.UserId;
            }

            return Id;
        }

        public BankAccount login(string BankId, string UserId, string pass)
        {
            bank = FindBank(BankId);

            BankAccount user = null;
            foreach (BankAccount account in bank.BankAccounts)
            {
                if (account.UserId == UserId & account.Password == pass)
                    user = account;

            }
            return user;


        }
        public BankStaff stafflogin(string BankId, string UserId, string pass)
        {
            bank = FindBank(BankId);
            BankStaff user = null;
            foreach (BankStaff account in bank.StaffAcounts)
            {
                if (account.UserId == UserId & account.Password == pass)
                    user = account;

            }
            return user;
        }
        public BankAccount checkAccount(string BankId,string accountHolder)
        {
            BankAccount user = null;
            bank = FindBank(BankId);
            foreach (BankAccount account in bank.BankAccounts)
            {
                if (account.Name == accountHolder)
                    user = account;
            }
            return user;
        }
        public void deposit(BankAccount user, decimal amt,string code)
        {
            user.Balance += Math.Round(amt * (decimal)(DataBase.curr[code] / DataBase.curr["INR"]), 2);
            Transactions trans = new Transactions(amt, 0, user.UserId, user.UserId, bank.Id,bank.Id);
            user.Transactions.Add(trans);
        }
        public bool withdraw(BankAccount user, decimal amt)
        {
            if (user.Balance >= amt)
            {
                user.Balance -= amt;
                Transactions trans = new Transactions(amt, 1, user.UserId, user.UserId, bank.Id,bank.Id);
                user.Transactions.Add(trans);
                return true;
            }
            return false;
        }
        public bool transfer(BankAccount user, decimal amt, BankAccount rcvr,string fromid ,string toid,int choice)
        {
            Bank reciever = null;
            foreach (var i in DataBase.Banks)
            {
                if (i.Id == fromid)
                {
                    bank = i;
                }
                if (i.Id == toid)
                {
                    reciever = i;
                }
            }
            decimal charge;
            if(fromid==toid)
            {
                if(choice==1)
                {
                    charge = deductCharges(amt,bank.SameRTGS );
                }
                else
                {
                    charge = deductCharges(amt, bank.SameIMPS);
                }
                
            }
            else
            {
                if (choice == 1)
                {
                    charge = deductCharges(amt, bank.DiffRTGS);
                }
                else
                {
                    charge = deductCharges(amt, bank.DiffIMPS);
                }
            }

            

            if (user.Balance >= amt)
            {
                amt = amt - charge;
                user.Balance -= amt;

                rcvr.Balance += Math.Round(amt * (decimal)(DataBase.curr[bank.CurrencyCode] / DataBase.curr[reciever.CurrencyCode]), 2);
                Transactions trans = new Transactions(amt, 1, user.UserId, rcvr.UserId, fromid,toid);
                user.Transactions.Add(trans);
                Transactions rcvrtrans = new Transactions(amt, 0, rcvr.UserId, user.UserId, fromid,toid);
                rcvr.Transactions.Add(rcvrtrans);
                return true;
            }
            return false;
        }
        public decimal viewBalance(BankAccount user)
        {
            return user.Balance;
        }
        public void DeleteAccount(string BankId, string userid)
        {
            bank = FindBank(BankId);

            BankAccount user = null;
            user = FindAccount(bank, userid);
            bank.BankAccounts.Remove(user);
        }
        public void AddCurrency(string code, double rate)
        {
            DataBase.curr[code] = rate;
        }
        public void UpdateCharges(double rtgs, double imps, int choice)
        {
            if (choice == 1)
            {
                bank.SameRTGS = rtgs;
                bank.SameIMPS = imps;
            }
            else
            {
                bank.DiffRTGS = rtgs;
                bank.DiffIMPS = imps;
            }
        }
        public BankAccount ViewHistory(string userid)
        {
            BankAccount user = null;
            user = FindAccount(bank, userid);
            return user;
        }
        public decimal deductCharges(decimal amount, double percent)
        {
            return (decimal)Math.Round(amount * ((100 - Convert.ToDecimal(percent)) / 100), 2);
        }
        public void RevertTransaction(string bankid,string accountid,string transid)
        {
            Transactions revert = null;
            BankAccount sender = null;
            BankAccount rcvr = null;
            foreach(var i in DataBase.Banks)
            {
                if(i.Id==bankid)
                {
                    foreach(var j in i.BankAccounts)
                    {
                        if(j.UserId==accountid)
                        {
                            foreach(var k in j.Transactions)
                            {
                                if(k.Id==transid)
                                {
                                    revert = k;
                                    sender = j;
                                 
                                }
                            }
                        }
                    }
                }
                
            }
            foreach(var i in DataBase.Banks)
            {
                if(i.Id==revert.RecieverBankId)
                {
                    foreach(var j in i.BankAccounts)
                    {
                        if(j.UserId==revert.RecieverAccountId)
                        {
                            rcvr = j;
                        }
                    }
                }
            }
            sender.Balance += revert.Amount;
            rcvr.Balance -= revert.Amount;
        }
        public BankAccount UpdateChanges(string BankId,string userid)
        {
            bank = FindBank(BankId);

            BankAccount user = null;
            user = FindAccount(bank, userid);
            return user;

        }
        public static Bank FindBank(string BankId)
        {
            foreach (var i in DataBase.Banks)
            {
                if (i.Id == BankId)
                {
                    return i;
                }
            }
            return null;
        }
        public static BankAccount FindAccount(Bank bank,string userid)
        {
            foreach (BankAccount account in bank.BankAccounts)
            {
                if (account.UserId == userid)
                    return account;

            }
            return null;
        }
     
    }

}
