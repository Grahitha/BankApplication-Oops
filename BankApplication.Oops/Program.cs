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
                int choice1 = int.Parse(UserMessages.ReadInput());
                switch (choice1)
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
                            string bankid=bankManager.CreateBank(BankName, BankAddress, BankBranch, CurrencyCode);
                            Console.WriteLine("BankID is:"+bankid);
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
                        //enter bankid
                        UserMessages.Output("1.staff\n2.Customer");
                        int choice2 = Convert.ToInt32(UserMessages.ReadInput());
                        UserMessages.Output("Enter BankID;");
                        BankId = UserMessages.ReadInput();
                        UserMessages.Output("Please Enter Account Id:");
                        Id = UserMessages.ReadInput();
                        UserMessages.Output("Please Enter Password:");
                        Password = UserMessages.ReadInput();
                        BankAccount bankAccount;
                        BankStaff bankstaff;
                        if (choice2==1)
                        {
                            bankstaff = bankManager.stafflogin(BankId,Id, Password);
                            if(bankstaff==null)
                            {
                                UserMessages.Output("Invalid details");
                                break;
                            }
                            UserMessages.Output("Login Successfull!");
                            
                            if (bankstaff != null)
                            {
                                
                                bool logout = false;
                                while (!logout)
                                {
                                    UserMessages.Output("1.Create Account\n2.Update Account\n3.Delete Account\n4.Add Currency\n5.Add service charge for same bank\n6.Add service charge for other bank account\n7.View Account history\n8.Revert transaction\n9.Exit");
                                    int choice3 = Convert.ToInt32(UserMessages.ReadInput());
                                    switch (choice3)
                                    {
                                        case 1:
                                            //create account
                                            UserMessages.Output("Please Enter Account Holder Name");
                                            Name = UserMessages.ReadInput();
                                            UserMessages.Output("Please Enter valid Mobile Number");
                                            PhoneNumber = 0;
                                            //valid number
                                            Num = false;
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
                                            Password = UserMessages.ReadInput();
                                            UserMessages.Output("Re-enter Password:");
                                            while (Password != UserMessages.ReadInput())
                                                UserMessages.Output("Password not matched!");
                                            UserMessages.Output("Please Enter Gender(Male/Female):");
                                            Gender = UserMessages.ReadInput();
                                            try
                                            {
                                                Id = bankManager.createaccount(BankId, Name, PhoneNumber, Password, Gender, 1);
                                                UserMessages.Output("Account created successfully!");
                                                UserMessages.Output("Your account id is:" + Id);
                                            }
                                            catch (Exception e)
                                            {
                                                Console.WriteLine(e);
                                            }
                                            break;
                                        case 2:
                                            //update
                                            UserMessages.Output("What do you want to update!");
                                            UserMessages.Output("1.Name\n2.Phone Number\n3.Password\n");
                                            int choice6 = Convert.ToInt32(UserMessages.ReadInput());
                                            UserMessages.Output("Enter UserId you want to update:");
                                            string UserId = UserMessages.ReadInput();
                                            bankAccount=bankManager.UpdateChanges(BankId,UserId);
                                            switch(choice6)
                                            {
                                                case 1:
                                                    UserMessages.Output("Enter Name:");
                                                    bankAccount.Name = UserMessages.ReadInput();
                                                    break;
                                                case 2:
                                                    UserMessages.Output("Enter Phone Number:");
                                                    bankAccount.PhoneNumber = Convert.ToInt32(UserMessages.ReadInput());
                                                    break;
                                                case 3:
                                                    UserMessages.Output("Enter Password:");
                                                    bankAccount.Password = UserMessages.ReadInput();
                                                    break;
                                                default:
                                                    Environment.Exit(1);
                                                    break;
                                            }
                                            break;
                                        case 3:
                                            //delete
                                            UserMessages.Output("Enter UserId you want to delete:");
                                            UserId = UserMessages.ReadInput();
                                            bankManager.DeleteAccount(BankId, UserId);
                                            break;
                                        case 4:
                                            //add currency
                                            UserMessages.Output("Enter currencycode:");
                                            string code = UserMessages.ReadInput();
                                            UserMessages.Output("Enter Exchange rate:");
                                            double rate = Convert.ToDouble(UserMessages.ReadInput());
                                            bankManager.AddCurrency(code, rate);
                                            break;
                                        case 5:
                                            //add charge same account
                                            UserMessages.Output("Enter new charge for RTGS:");
                                            int Rtgs = Convert.ToInt32(Console.ReadLine());
                                            UserMessages.Output("Enter new charge for IMPS");
                                            int Imps = Convert.ToInt32(Console.ReadLine());
                                            bankManager.UpdateCharges(Rtgs, Imps, 1);

                                            break;
                                        case 6:
                                            //addd charge dif acount
                                            UserMessages.Output("Enter new charge for RTGS:");
                                            Rtgs = Convert.ToInt32(Console.ReadLine());
                                            UserMessages.Output("Enter new charge for IMPS");
                                            Imps = Convert.ToInt32(Console.ReadLine());
                                            bankManager.UpdateCharges(Rtgs, Imps, 1);
                                            break;
                                        case 7:
                                            //transaction history
                                            UserMessages.Output("Enter TransactionID :");
                                            string id = UserMessages.ReadInput();
                                            bankAccount = bankManager.ViewHistory(id);
                                            if(bankAccount==null)
                                            {
                                                UserMessages.Output("Invalid!");
                                                break;
                                            }
                                            foreach (var i in bankAccount.Transactions)
                                            {
                                                UserMessages.History(i);
                                            }
                                            break;

                                        case 8:
                                            //revert
                                            UserMessages.Output("Enter Bank Id to revert:");
                                            BankId= UserMessages.ReadInput();
                                            UserMessages.Output("Enter User Id to revert:");
                                            UserId= UserMessages.ReadInput();
                                            UserMessages.Output("Enter Transaction Id to revert:");
                                            Id = UserMessages.ReadInput();
                                            bankManager.RevertTransaction(BankId,UserId,Id);
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
                            

                        }
                        else
                        {
                            bankAccount = bankManager.login(BankId,Id, Password);
                            if (bankAccount == null)
                            {
                                UserMessages.Output("Invalid details");
                                break;
                            }
                            UserMessages.Output("Login Successfull!");
                            if (bankAccount != null)
                            {
                                
                                bool logout = false;
                                while (!logout)
                                {
                                    UserMessages.Output("1.Deposit\n2.Withdraw\n3.Transfer\n4.Transaction History\n5.Exit");
                                    int choice4 = Convert.ToInt32(UserMessages.ReadInput());
                                    switch (choice4)
                                    {
                                        case 1:
                                            UserMessages.Output("Please Enter Valid Amount to Deposit:");
                                            int amount = int.Parse(UserMessages.ReadInput());
                                            UserMessages.Output("Enter currency code");
                                            CurrencyCode = UserMessages.ReadInput();
                                            bankManager.deposit(bankAccount, amount,CurrencyCode);
                                            UserMessages.Output(amount + " deposited successfully!");
                                            break;
                                        case 2:
                                            //withdraw
                                            UserMessages.Output("Please Enter Amount to Withdraw:");
                                            amount = int.Parse(UserMessages.ReadInput());
                                            if (bankManager.withdraw(bankAccount, amount))
                                            {
                                                UserMessages.Output(amount + " withdrawn successfully!");
                                            }
                                            else
                                            {
                                                UserMessages.Output("Insufficient Amount to Withdraw!");
                                            }
                                            break;
                                        case 3:
                                            //transfer
                                            UserMessages.Output("Enter REceiver BankId");
                                            string ToBankId = UserMessages.ReadInput();
                                            UserMessages.Output("Select type:\n1.RTGS\n2.IMPS");
                                            int choice5 = Convert.ToInt32(UserMessages.ReadInput());
                                            UserMessages.Output("Enter Account Holder name to Transfer:");
                                            Name = UserMessages.ReadInput();
                                            BankAccount reciever = bankManager.checkAccount(ToBankId,Name);
                                            if (reciever != null)
                                            {
                                                UserMessages.Output("Enter Amount to Transfer:");
                                                decimal amtToTransfer = Decimal.Parse(UserMessages.ReadInput());
                                                if (bankManager.transfer(bankAccount, amtToTransfer, reciever,BankId,ToBankId,choice5))
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
                                            //transaction history
                                            Console.WriteLine();
                                            foreach (var i in bankAccount.Transactions)
                                            {
                                                UserMessages.History(i);
                                            }
                                            break;
                                        case 5:
                                            UserMessages.ValueOutput(bankManager.viewBalance(bankAccount));
                                            break;
                                        default:
                                            logout = true;
                                            break;
                                    }
                                }

                            }
                            else
                            {
                                
                            }
                            
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
