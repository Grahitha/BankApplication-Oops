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
                        try
                        {
                            bankManager.CreateBank(BankName, BankAddress, BankBranch, CurrencyCode);
                        }
                        catch(Exception e)
                        {
                            Console.WriteLine(e);
                        }
                        break;
                    case 2://staff account
                        UserMessages.Output("Enter BankId:");
                        string BankId = UserMessages.ReadInput();
                        UserMessages.Output("Please Enter Staff Name");
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
                            Id = bankManager.createaccount(BankId,Name, PhoneNumber, Password, Gender,0);
                            UserMessages.Output("Staff Account created successfully!");
                            UserMessages.Output("Your account id is:" + Id);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e);
                        }
                        break;
                    case 3:
                        UserMessages.Output("1.staff\n2.Customer");
                        choice = Convert.ToInt32(UserMessages.ReadInput());
                        UserMessages.Output("Please Enter Account Id:");
                        Id = UserMessages.ReadInput();
                        UserMessages.Output("Please Enter Password:");
                        Password = UserMessages.ReadInput();
                        BankAccount bankAccount;
                        if (choice==1)
                        {
                            bankAccount = bankManager.login(Id, Password,1);
                            UserMessages.Output("1.Create Account\n2.Update Account\n3.Delete Account\n4.Add Currency\n5.Add service charge for same bank\n6.Add service charge for other bank account\n7.View Account history\n8.Revert transaction");

                        }
                        else
                        {
                            bankAccount = bankManager.login(Id, Password,0);
                            UserMessages.Output("1.Deposit\n2.Withdraw\n3.Transfer\n4.Transaction History");
                        }
                        
                        
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
                                        Name = UserMessages.ReadInput();
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
