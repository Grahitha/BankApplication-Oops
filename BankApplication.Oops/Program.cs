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
                UserMessages.MainMenu();
                int choice1 = int.Parse(UserMessages.ReadInput());
                switch (choice1)
                {
                    case 1:
                        //bank account 
                        string BankName = UserMessages.BankName();
                        string BankAddress = UserMessages.BankAddress();
                        string BankBranch = UserMessages.BankBranch();
                        string CurrencyCode = UserMessages.BankCurrencyCode();
                        try
                        {
                            string bankid=bankManager.CreateBank(BankName, BankAddress, BankBranch, CurrencyCode);
                            UserMessages.ID("Bank",bankid);
                        }
                        catch (Exception e)
                        {
                            UserMessages.Exceptions(e);
                        }
                        break;

                    case 2://staff account
                        string BankId = UserMessages.BankId();
                        string Name = UserMessages.BankStaffName();
                        int PhoneNumber = 0;            
                        PhoneNumber=Validation.ValidPhoneNumber(PhoneNumber);
                        string Password = UserMessages.Password();
                        Password = UserMessages.ReenterPassword(Password);
                        string Gender = UserMessages.Gender();
                        string Id;
                        try
                        {
                            Id = bankManager.createaccount(BankId,Name, PhoneNumber, Password, Gender,0);
                            UserMessages.StaffAccountCreated(Id);
                        }
                        catch (Exception e)
                        {
                            UserMessages.Exceptions(e);
                        }
                        break;
                    case 3:
                        //enter bankid
                        int choice2 = UserMessages.LoginMenu();
                        BankId = UserMessages.InputId("Bank");
                        Id = UserMessages.InputId("Account");
                        Password = UserMessages.Password();
                        BankAccount bankAccount;
                        BankStaff bankstaff;
                        if (choice2==1)
                        {
                            bankstaff = bankManager.stafflogin(BankId,Id, Password);
                            if (bankstaff != null)
                            {
                                UserMessages.Output("Login Successfull!");
                                bool logout = false;
                                while (!logout)
                                {
                                    
                                    int choice3 = UserMessages.StaffMenu();
                                    switch (choice3)
                                    {
                                        case 1:
                                            //create account
                                            Name = UserMessages.AccountHolderName() ;
                                            PhoneNumber = 0;
                                            PhoneNumber=Validation.ValidPhoneNumber(PhoneNumber);
                                            Password = UserMessages.Password();
                                            Password = UserMessages.ReenterPassword(Password);
                                            Gender = UserMessages.Gender();
                                            try
                                            {
                                                Id = bankManager.createaccount(BankId, Name, PhoneNumber, Password, Gender, 1);
                                                UserMessages.CustomerAccountCreated(Id);
                                            }
                                            catch (Exception e)
                                            {
                                                Console.WriteLine(e);
                                            }
                                            break;
                                        case 2:

                                            int choice6 = UserMessages.UpdateMenu();
                                            string UserId = UserMessages.InputId("user");
                                            bankAccount=bankManager.UpdateChanges(BankId,UserId);
                                            switch(choice6)
                                            {
                                                case 1:
                                                    bankAccount.Name = UserMessages.AccountHolderName();
                                                    break;
                                                case 2:
                                                    bankAccount.PhoneNumber = UserMessages.MobileNumber();
                                                    break;
                                                case 3:
                                                    bankAccount.Password = UserMessages.Password();
                                                    break;
                                                default:
                                                    Environment.Exit(1);
                                                    break;
                                            }
                                            break;
                                        case 3:
                                            UserId = UserMessages.InputId("user");
                                            bankManager.DeleteAccount(BankId, UserId);
                                            break;
                                        case 4: 
                                            string code = UserMessages.BankCurrencyCode();
                                            double rate = UserMessages.ExchangeRate();
                                            bankManager.AddCurrency(code, rate);
                                            break;
                                        case 5:
                                            int Rtgs = UserMessages.Charge("RTGS");
                                            int Imps = UserMessages.Charge("IMPS");
                                            bankManager.UpdateCharges(Rtgs, Imps, 1);
                                            break;
                                        case 6:
                                            Rtgs = UserMessages.Charge("RTGS");
                                            Imps = UserMessages.Charge("IMPS");
                                            bankManager.UpdateCharges(Rtgs, Imps, 1);
                                            break;
                                        case 7:
                                            string id = UserMessages.InputId("Transaction:");
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
                                            BankId = UserMessages.BankId();
                                            UserId = UserMessages.InputId("User");
                                            Id = UserMessages.InputId("Transaction");
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
                                UserMessages.Invalid();
                            }
                            

                        }
                        else
                        {
                            bankAccount = bankManager.login(BankId,Id, Password);
                            if (bankAccount == null)
                            {
                                UserMessages.Invalid();
                                break;
                            }
                            UserMessages.Output("Login Successfull!");
                            if (bankAccount != null)
                            {
                                
                                bool logout = false;
                                while (!logout)
                                {

                                    int choice4 = UserMessages.CustomerMenu();
                                    switch (choice4)
                                    {
                                        case 1:
                                            decimal amount = UserMessages.Amount();
                                            CurrencyCode = UserMessages.BankCurrencyCode();
                                            bankManager.deposit(bankAccount, amount,CurrencyCode);
                                            UserMessages.Output(amount + " deposited successfully!");
                                            break;
                                        case 2:

                                            amount = UserMessages.Amount();
                                            if (bankManager.withdraw(bankAccount, amount))
                                                UserMessages.Output(amount + " withdrawn successfully!");
                                            
                                            else
                                                UserMessages.Insufficent();
                                            
                                            break;
                                        case 3:
                                            string ToBankId = UserMessages.InputId("Receiver Bank");
                                            int choice5 = UserMessages.ChargeChoice();
                                            Name = UserMessages.ReciverName();
                                            BankAccount reciever = bankManager.checkAccount(ToBankId,Name);
                                            if (reciever != null)
                                            {
                                                decimal amtToTransfer = UserMessages.Amount();
                                                if (bankManager.transfer(bankAccount, amtToTransfer, reciever, BankId, ToBankId, choice5))
                                                    UserMessages.Output(amtToTransfer + " transferred successfully!");
                                                else
                                                    UserMessages.Insufficent();

                                            }
                                            else
                                                UserMessages.ReciverNotExist();
                                            
                                            break;
                                        case 4:
                                            foreach (var i in bankAccount.Transactions)
                                                UserMessages.History(i);
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
                                UserMessages.AccountExists();
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
