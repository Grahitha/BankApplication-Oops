using BankApplication.Models;
using BankApplication.Services;
using System;
namespace BankApplication.Oops
{
    class Program   
    {
        static void Main(string[] args)
        {
            UserMessages.Welcome();
            Bankservices bankManager = new Bankservices();          
            while (true)
            {
                UserMessages.LoginMenu();
                int choice = int.Parse(UserMessages.ReadInput());
                switch (choice)
                {
                    case 1:
                        UserMessages.Output("Enter Bank Name:");
                        string BankName = Console.ReadLine();
                        UserMessages.Output("Enter Bank Address:");
                        string BankAddress = Console.ReadLine();
                        UserMessages.Output("Enter Bank Branch");
                        string BankBranch = Console.ReadLine();
                        UserMessages.Output("Enter Currency code for bank:");
                        string CurrencyCode = Console.ReadLine();
                        UserMessages.Output("Gender:(Male/Female)");
                        string Gender = Console.ReadLine();
                        try
                        {
                            bankManager.CreateBank(BankName, BankAddress, BankBranch, CurrencyCode);
                        }
                        catch(Exception e)
                        {
                            Console.WriteLine(e);
                        }
                        break;
                    case 2:
                        UserMessages.Output("Please Enter Account Id:");
                        string Id = UserMessages.ReadInput();
                        UserMessages.Output("Please Enter Password:");
                        string Password = UserMessages.ReadInput();
                        BankAccount bankAccount = bankManager.login(Id, Password);
                        if (bankAccount != null)
                        {
                            UserMessages.Output("Login Successfull!");
                            bool logout = false;
                            while (!logout)
                            {
                                UserMessages.TransactionMenu();
                                choice = int.Parse(UserMessages.ReadInput());
                                switch (choice)
                                {
                                    case 1:
                                        UserMessages.Output("Please Enter Valid Amount to Deposit:");
                                        int amount = int.Parse(UserMessages.ReadInput());
                                        bankManager.deposit(bankAccount,amount);
                                        UserMessages.Output(amount + " deposited successfully!");

                                        break;
                                    case 2:
                                        UserMessages.Output("Please Enter Amount to Withdraw:");
                                        amount = int.Parse(UserMessages.ReadInput());
                                        if(bankManager.withdraw(bankAccount,amount))
                                        {
                                            UserMessages.Output(amount + " withdrawn successfully!");
                                        }
                                        else
                                        {
                                            UserMessages.Output("Insufficient Amount to Withdraw!");
                                        }
                                        break;
                                    case 3:
                                        UserMessages.Output("Enter Account Holder name to Transfer:");
                                        string Name = UserMessages.ReadInput();
                                        BankAccount reciever = bankManager.checkAccount(Name);
                                        if (reciever != null)
                                        {
                                            UserMessages.Output("Enter Amount to Transfer:");
                                            decimal amtToTransfer = Decimal.Parse(UserMessages.ReadInput());
                                            if(bankManager.transfer(bankAccount,amtToTransfer, reciever))
                                            {
                                                UserMessages.Output(amtToTransfer + " transferred successfully!");
                                            }
                                            else
                                            {
                                                UserMessages.Output("Insufficient amount to transfer!");
                                            }
                                        }
                                        else
                                        {
                                            UserMessages.Output("Receiver Account does not Exist!");
                                        }
                                        break;
                                    case 4:
                                        UserMessages.ValueOutput(bankManager.viewBalance(bankAccount));
                                        break;
                                    case 5:
                                        Console.WriteLine();
                                        foreach(var i in bankAccount.Transactions)
                                        {
                                            Console.WriteLine("Transaction ID:" + i.Id);
                                            Console.WriteLine(i.Amount);
                                            Console.WriteLine(i.Type+" to/from your account ");
                                            if(i.SenderAccountId!=i.RecieverAccountId)
                                            {
                                                Console.WriteLine("From " + i.SenderAccountId + " to    " + i.RecieverAccountId);
                                            }
                                            Console.WriteLine(i.On.ToString());
                                        }
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
                        
                        Environment.Exit(1);
                        break;


                }
            }


        }
    }
}
