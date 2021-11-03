using System;

namespace BankApplication.Oops
{
    public class Validation
    {
        public static int ValidPhoneNumber(int PhoneNumber)
        {
            bool Num = false;
            while (!Num)
            {
                try
                {
                    PhoneNumber = UserMessages.MobileNumber();
                    if(PhoneNumber<100000000)
                    {
                        throw new FormatException("Mobile Number Should have 10 digits");
                    }
                    else
                    {
                        Num = true;
                    }
                    
                }
                catch (FormatException)
                {
                    UserMessages.Output("Enter Valid Mobile Number");

                }
            }
            return PhoneNumber;
        }
       
    }
}