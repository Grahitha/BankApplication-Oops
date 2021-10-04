using IndiaBank.Oops.services;
using System;
using System.IO;
namespace BankApplication1.Oops
{
    class Program
    {
        static void Main(string[] args)
        {
            BankManager bankManager = new BankManager();
            while (true)
            {
                Console.WriteLine("1.Create Account\n2.Login\n3.Exit\n");
                int choice = int.Parse(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        Console.WriteLine("Please Enter Account Holder Name");
                        string name = Console.ReadLine();
                        Console.WriteLine("Please Enter valid Mobile Number");
                        int phoneNumber = 0;
                        bool num = false;
                        while (!num)
                        {
                            try
                            {
                                phoneNumber = int.Parse(Console.ReadLine());
                                num = true;
                            }
                            catch (FormatException)
                            {
                                Console.WriteLine("Enter Valid Mobile Number");
                            }
                        }


                        Console.WriteLine("Please Create Password:");
                        string password = Console.ReadLine();
                        Console.WriteLine("Re-enter Password:");
                        while (password != Console.ReadLine())
                            Console.WriteLine("Password not matched!");
                        try
                        {
                            bankManager.createaccount(name, phoneNumber, password);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e);
                        }
                        break;
                    case 2:
                        Console.WriteLine("Please Enter Account Holder Name:");
                        name = Console.ReadLine();
                        Console.WriteLine("Please Enter Password:");
                        password = Console.ReadLine();
                        BankAccount bankAccount = new BankAccount();
                        bankAccount = bankManager.bank.login(name, password);
                        if (bankAccount != null)
                        {
                            Console.WriteLine("Login Successfull!");
                            bool logout = false;
                            while (!logout)
                            {
                                Console.WriteLine("1.Deposit\n2.Withdraw\n3.Transfer\n4.View Balance\n5.Logout");
                                choice = int.Parse(Console.ReadLine());
                                switch (choice)
                                {
                                    case 1:
                                        Console.WriteLine("Please Enter Valid Amount to Deposit:");
                                        int amount = int.Parse(Console.ReadLine());
                                        bankAccount.deposit(amount);


                                        break;
                                    case 2:
                                        Console.WriteLine("Please Enter Amount to Withdraw:");
                                        amount = int.Parse(Console.ReadLine());
                                        bankAccount.withdraw(amount);
                                        break;
                                    case 3:
                                        Console.WriteLine("Enter Account Holder name to Transfer:");
                                        name = Console.ReadLine();
                                        BankAccount reciever = bankManager.bank.checkAccount(name);
                                        if (reciever != null)
                                        {
                                            Console.WriteLine("Enter Amount to Transfer:");
                                            decimal amtToTransfer = Decimal.Parse(Console.ReadLine());
                                            bankAccount.transfer(amtToTransfer, reciever);
                                        }
                                        else
                                        {
                                            Console.WriteLine("Receiver Account does not Exist!");
                                        }
                                        break;
                                    case 4:
                                        Console.WriteLine(bankAccount.viewBalance());
                                        break;
                                    default:
                                        logout = true;
                                      
                                        break;

                                }
                            }

                        }
                        else
                        {
                            Console.WriteLine("Invalid Credentials!");
                        }

                        break;
                    default:
                        for (int i = 0; i < Bank.transactions.Count; i++)
                        {
                            Console.WriteLine(Bank.transactions[i]);
                        }
                        Environment.Exit(1);
                        break;


                }
            }


        }
    }
}
