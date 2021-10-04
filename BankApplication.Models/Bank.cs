using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
//using Newtonsoft.Json;

namespace BankApplication1.Oops
{
    public class Bank
    {

        public string name { get; set; }
        public int id { get; set; }
        public string address { get; set; }
        public string branch { get; set; }

        public List<BankAccount> BankAccounts = new List<BankAccount>();
        public static List<string> transactions = new List<string>();
        public Bank(string name, int id, string address, string branch)
        {
            this.name = name;
            this.id = id;
            this.address = address;
            this.branch = branch;
            //string output = File.ReadAllText(@"E:\\account.json");
            //BankAccounts= JsonConvert.DeserializeObject<List<BankAccount>>(output);
            Console.WriteLine("Welcome to India Bank!");

        }
        public BankAccount login(string accountHolder, string pass)
        {
            BankAccount user = null;
            foreach (BankAccount account in BankAccounts)
            {
                if (account.name == accountHolder & account.password == pass)
                {
                    user = account;
                    Bank.transactions.Add(accountHolder + " account logged in!");
                }

            }
            return user;
        }
        public BankAccount checkAccount(string accountHolder)
        {
            BankAccount user = null;
            foreach (BankAccount account in BankAccounts)
            {
                if (account.name == accountHolder)
                    user = account;
            }
            return user;
        }

    }
}
