using System;
using System.Collections.Generic;
using System.Text;
using BankApplication.Con;
namespace BankApplication.Models
{
    public class BankAccount
    {
        public string name { get; set; }
        public int phoneNumber { get; set; }
        public int id { get; set; }
        public int bankId { get; set; }
        public string password { get; set; }
        public decimal amount = 0;

        public void deposit(decimal amt)
        {
            this.amount += amt;
            UserMessages.Output("Amount Deposited Successfully!");
            Bank.transactions.Add("Amount " + amount + " deposited to " + name + " account");
        }
        public void withdraw(decimal amt)
        {
            if (this.amount >= amt)
            {
                this.amount -= amt;
                Bank.transactions.Add("Amount " + amount + " withdrawn from " + name + " account");
            }
            else
            {
                UserMessages.Output("Insufficient Amount to Withdraw!");
            }
        }
        public void transfer(decimal amt, BankAccount rcvr)
        {
            if (this.amount >= amt)
            {
                this.amount -= amt;
                rcvr.amount += amt;
                Bank.transactions.Add("Amount " + amount + " transferred from " + name + " account to " + rcvr.name + " account");
            }
            else
            {
                UserMessages.Output("Insufficient Amount to Transfer!");
            }
        }
        public decimal viewBalance()
        {
            return this.amount;
        }
    }
}
