using System;
using System.Collections.Generic;
using System.Text;

namespace BankApplication.Con
{
    public class UserMessages
    {
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
    }
}
