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
            UserMessages.Output("Please Enter Account Holder Name");
            string Name = UserMessages.ReadInput();
            UserMessages.Output("Please Enter valid Mobile Number");
            int PhoneNumber = 0;
            bool Num = false;
            while (!Num)
            {
                try
                {
                    PhoneNumber = int.Parse(UserMessages.ReadInput());
                    Num = true;
                }
                catch (FormatException)
                {
                    UserMessages.Output("Enter Valid Mobile Number");

                }
            }


            UserMessages.Output("Please Create Password:");
            string Password = UserMessages.ReadInput();
            UserMessages.Output("Re-enter Password:");
            while (Password != UserMessages.ReadInput())
                UserMessages.Output("Password not matched!");
            UserMessages.Output("Please Enter Gender(Male/Female):");
            string Gender = UserMessages.ReadInput();
            string Id;
            try
            {
                Id = bankManager.createaccount(Name, PhoneNumber, Password, Gender);
                UserMessages.Output("Account created successfully!");
                UserMessages.Output("Your account id is:" + Id);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }*/

       
    }
}
