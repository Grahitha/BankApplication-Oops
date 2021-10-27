using System;
using System.Collections.Generic;
using System.Text;

namespace BankApplication.Models
{
    public class BankStaff : BankAccount
    {
        public BankStaff(string name, int number, string password, string gender) :base(name,number,password,gender)
        {
            this.Name = name;
            this.PhoneNumber = number;
            this.Password = password;
            this.Gender=gender;
            this.UserId = BankAccountIdGenerator(name);
        }
    }
}
