using BankApplication.Con;
using BankApplication.Models;
using BankApplication.Services;
using System;
namespace BankApplication.Oops
{
    class Program
    {
        static void Main(string[] args)
        {
            BankManager bankManager = new BankManager();    
            while (true)
            {
                UserMessages.Output("1.Create Account\n2.Login\n3.Exit\n");
                int choice = int.Parse(UserMessages.ReadInput());
                switch (choice)
                {
                    case 1:
                        UserMessages.Output("Please Enter Account Holder Name");
                        string name = UserMessages.ReadInput();
                        UserMessages.Output("Please Enter valid Mobile Number");
                        int PhoneNumber = 0;
                        bool num = false;
                        while (!num)
                        {
                            try
                            {
                                PhoneNumber = int.Parse(UserMessages.ReadInput());
                                num = true;
                            }
                            catch (FormatException)
                            {
                                UserMessages.Output("Enter Valid Mobile Number");
                            }
                        }


                        UserMessages.Output("Please Create Password:");
                        string password = UserMessages.ReadInput();
                        UserMessages.Output("Re-enter Password:");
                        while (password != UserMessages.ReadInput())
                            UserMessages.Output("Password not matched!");
                        try
                        {
                            bankManager.createaccount(name, PhoneNumber, password);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e);
                        }
                        break;
                    case 2:
                        UserMessages.Output("Please Enter Account Holder Name:");
                        name = UserMessages.ReadInput();
                        UserMessages.Output("Please Enter Password:");
                        password = UserMessages.ReadInput();
                        BankAccount bankAccount = new BankAccount();
                        bankAccount = bankManager.bank.login(name, password);
                        if (bankAccount != null)
                        {
                            UserMessages.Output("Login Successfull!");
                            bool logout = false;
                            while (!logout)
                            {
                                UserMessages.Output("1.Deposit\n2.Withdraw\n3.Transfer\n4.View Balance\n5.Logout");
                                choice = int.Parse(UserMessages.ReadInput());
                                switch (choice)
                                {
                                    case 1:
                                        UserMessages.Output("Please Enter Valid Amount to Deposit:");
                                        int amount = int.Parse(UserMessages.ReadInput());
                                        bankAccount.deposit(amount);


                                        break;
                                    case 2:
                                        UserMessages.Output("Please Enter Amount to Withdraw:");
                                        amount = int.Parse(UserMessages.ReadInput());
                                        bankAccount.withdraw(amount);
                                        break;
                                    case 3:
                                        UserMessages.Output("Enter Account Holder name to Transfer:");
                                        name = UserMessages.ReadInput();
                                        BankAccount reciever = bankManager.bank.checkAccount(name);
                                        if (reciever != null)
                                        {
                                            UserMessages.Output("Enter Amount to Transfer:");
                                            decimal amtToTransfer = Decimal.Parse(UserMessages.ReadInput());
                                            bankAccount.transfer(amtToTransfer, reciever);
                                        }
                                        else
                                        {
                                            UserMessages.Output("Receiver Account does not Exist!");
                                        }
                                        break;
                                    case 4:
                                        UserMessages.ValueOutput(bankAccount.viewBalance());
                                        break;
                                    default:
                                        logout = true;
                                      
                                        break;

                                }
                            }

                        }
                        else
                        {
                            UserMessages.Output("Invalid Credentials!");
                        }

                        break;
                    default:
                        for (int i = 0; i < Bank.transactions.Count; i++)
                        {
                            UserMessages.Output(Bank.transactions[i]);
                        }
                        Environment.Exit(1);
                        break;


                }
            }


        }
    }
}
