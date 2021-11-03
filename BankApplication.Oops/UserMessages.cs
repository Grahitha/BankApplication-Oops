using BankApplication.Models;
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
        public static int ReadIntegerInput()
        {
            return Convert.ToInt32(Console.ReadLine());
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
        public static void MainMenu()
        {
            Console.WriteLine("1.Create Bank\n2.Create Staff Account\n3.Login\n4.Exit\n");
        }
        public static void History(Transactions i)
        {
            Console.WriteLine("Transaction ID:" + i.Id);
            Console.WriteLine(i.Amount);
            Console.WriteLine(i.Type + " to/from your account ");
            if (i.SenderAccountId != i.RecieverAccountId)
            {
                Console.WriteLine("From " + i.SenderAccountId + " to    " + i.RecieverAccountId);
            }
            Console.WriteLine(i.On.ToString());
        }
        public static string BankName()
        {
            Console.WriteLine("Please Enter BankName:");
            return ReadInput();
        }
        public static string BankAddress()
        {
            Console.WriteLine("Please Enter BankAddress:");
            return ReadInput();
        }
        public static string BankBranch()
        {
            Console.WriteLine("Please Enter BankBranch:");
            return ReadInput();
        }
        public static string BankCurrencyCode()
        {
            Console.WriteLine("Please Enter Currency Code:");
            return ReadInput();
        }

        public static string BankId()
        {
            Console.WriteLine("Please Enter BankId:");
            return ReadInput();
        }
        public static string BankStaffName()
        {
            Console.WriteLine("Please Enter Staff Name:");
            return ReadInput();
        }
        public static int MobileNumber()
        {
            Console.WriteLine("Please Enter Valid Mobile Number:");
            return ReadIntegerInput();
        }
        public static string Password()
        {
            Console.WriteLine("Please Enter Password:");
            return ReadInput();
        }
        public static string Gender()
        {
            Console.WriteLine("Please Enter Gender(Male/Female):");
            return ReadInput();
        }
        public static void StaffAccountCreated(string id)
        {
            Console.WriteLine("Staff Account Created Successfully!");
            ID("Account", id);
        }
        public static void CustomerAccountCreated(string id)
        {
            Console.WriteLine("Customer Account Created Successfully!");
            ID("Account", id);
        }
        public static void ID(string name, string id)
        {
            Console.WriteLine("Your " + name + "id is:" + id);
        }
        public static int LoginMenu()
        {
            Output("1.staff\n2.Customer");
            return ReadIntegerInput();
        }
        public static string InputId(string name)
        {
            Output("Please Enter " + name + " Id:");
            return ReadInput();
        }
        public static int StaffMenu()
        {
            Output("1.Create Account\n2.Update Account\n3.Delete Account\n4.Add Currency\n5.Add service charge for same bank\n6.Add service charge for other bank account\n7.View Account history\n8.Revert transaction\n9.Exit");
            return ReadIntegerInput();
        }
        public static int CustomerMenu()
        {
            Output("1.Deposit\n2.Withdraw\n3.Transfer\n4.Transaction History\n5.Exit");
            return ReadIntegerInput();
        }
        public static string AccountHolderName()
        {
            Output("Please Enter Name");
            return ReadInput();
        }
        public static int UpdateMenu()
        {
            Output("What do you want to update!");
            Output("1.Name\n2.Phone Number\n3.Password\n");
            return ReadIntegerInput();
        }
        //in other file
        public static string ReenterPassword(string Password)
        {
            Output("Re-enter Password:");
            while (Password != ReadInput())
                Output("Password not matched!");
            return Password;
        }
        public static double ExchangeRate()
        {
            Output("Enter Exchange rate:");
            return Convert.ToDouble(ReadInput());
        }
        public static int Charge(string type)
        {
            Output("Enter new charge for " + type);
            return Convert.ToInt32(ReadInput());
        }
        public static decimal Amount()
        {
            Output("Please Enter valid Amount");
            return Convert.ToDecimal(ReadInput());
        }
        public static void Invalid()
        {
            Output("Invalid");
        }
        public static void AccountExists()
        {
            Output("Account Exists!");
        }
        public static void Insufficent()
        {
            Output("Insufficient Amount!");
        }
        public static string ReciverName()
        {
            Output("Enter Account Holder name to Transfer:");
            return ReadInput();
        }
        public static int ChargeChoice()
        {
            Output("Select type:\n1.RTGS\n2.IMPS");
            return ReadIntegerInput();
        }
        public static void ReciverNotExist()
        {
            Output("Receiver Account does not Exist!");
        }
        public static void Exceptions(Exception e)
        {
            Console.WriteLine(e);
        }

    }
}
