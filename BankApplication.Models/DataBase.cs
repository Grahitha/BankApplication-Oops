using System;
using System.Collections.Generic;
using System.Text;

namespace BankApplication.Models
{
    public class DataBase
    {
        public static List<Bank> Banks = new List<Bank>();

        public static Dictionary<string, double> curr = new Dictionary<string, double>();
        public static void CurrencyInitialization()
        {
            curr.Add("ARS", 70.5);
            curr.Add("AUD", 1.5);
            curr.Add("EUR", 0.9);
            curr.Add("BRL", 5.2);
            curr.Add("BGN", 1.7);
            curr.Add("CAD", 1.3);
            curr.Add("CLF", 792.7);
            curr.Add("CNY", 6.9);
            curr.Add("COU", 3694.9);
            curr.Add("CRC", 584.9);
            curr.Add("HRK", 6.6);
            curr.Add("CZK", 23.2);
            curr.Add("DKK", 6.5);
            curr.Add("HUF", 308.0);
            curr.Add("ISK", 135.4);
            curr.Add("INR", 74.1);
            curr.Add("IDR", 14582.2);
            curr.Add("ILS", 3.4);
            curr.Add("JPY", 106.8);
            curr.Add("KPW", 1180.3);
            curr.Add("MXN", 21.5);
            curr.Add("NZD", 1.5);
            curr.Add("MKD", 54.1);
            curr.Add("NOK", 9.4);
            curr.Add("PLN", 3.9);
            curr.Add("RON", 4.2);
            curr.Add("RUB", 72.1);
            curr.Add("SAR", 3.8);
            curr.Add("ZAR", 16.5);
            curr.Add("SEK", 9.2);
            curr.Add("CHE", 0.9);
            curr.Add("TRY", 7.0);
            curr.Add("GBP", 0.8);
            curr.Add("USD", 1.0);
            curr.Add("ZMW", 18.3);
        }
        public double convert(string fromCode, string toCode, double amount)
        {
            return Math.Round(amount * (curr[fromCode] / curr[toCode]), 2);
        }

    }

}
