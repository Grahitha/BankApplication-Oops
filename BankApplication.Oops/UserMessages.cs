using System;
using System.Collections.Generic;
using System.Text;

namespace BankApplication.Oops
{
    public class UserMessages
    {
        public static void Welcome()
        {
            Console.WriteLine("Welcome!");
        }
        public static void Output(string message)
        {
            Console.WriteLine(message);
        }
        public static string ReadInput()
        {
            return Console.ReadLine();
        }
        public static void ValueOutput(decimal number)
        {
            Console.WriteLine(number);
        }
        public static void LoginMenu()
        {
            Console.WriteLine("1.Create Bank\n2.Create Account\n3.Login\n3.Exit\n");
        }
        public static void TransactionMenu()
        {
            UserMessages.Output("1.Deposit\n2.Withdraw\n3.Transfer\n4.View Balance\n5.Transaction History\n6.Logout");
        }
        /*public static CreateBankAccount()
        {
            
        }*/

       
    }
}
