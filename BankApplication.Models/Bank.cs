using System;
using System.Collections.Generic;

namespace BankApplication.Models
{
    public class Bank
    {

        public string Name { get; set; }
        public string Id { get; set; }
        public string address { get; set; }
        public string branch { get; set; }
        public string CurrencyCode { get; set; }
        public double SameRTGS=0;
        public double SameIMPS = 5;
        public double DiffRTGS =2;
        public double DiffIMPS =6;

        public List<BankAccount> BankAccounts = new List<BankAccount>();
        public List<BankStaff> StaffAcounts = new List<BankStaff>();
        public Bank(string name,string address, string branch,string code)
        {
            this.Name = name;
            this.Id = BankIdGenerator(name);
            this.address = address;
            this.branch = branch;
            this.CurrencyCode = code;
           

        }
        public string BankIdGenerator(string name)
        {
            string dt = DateTime.Now.ToString("yyyyMMddHHmmss");
            string BankId;
            if (name.Length <= 3)
                BankId = name + dt;
            else
                BankId= name.Substring(0, 3) + dt;
            return BankId;
        }


    }
    

}
