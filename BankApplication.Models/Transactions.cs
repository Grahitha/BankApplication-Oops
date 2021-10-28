using System;

namespace BankApplication.Models
{
    public class Transactions
    {
        public string Id { set; get; }

        public TransactionType Type { set; get; }
        public string SenderBankID { set; get; }
        public string RecieverBankId { set; get; }
        public string SenderAccountId { set; get; }
        public string RecieverAccountId { set; get; }
        public decimal Amount { set; get; }
        public DateTime On {set;get;}
        public Transactions(decimal amt,int type,string SenderId,string RecieverId,string BankID,string RcvrBankId)
        {
            Id = TransactionIdGenerator(SenderId, BankID);
            Type = (TransactionType)type;
            SenderAccountId = SenderId;
            RecieverAccountId =RecieverId;
            Amount = amt;
            On = DateTime.Now;
            SenderAccountId = BankID;
            RecieverBankId = RcvrBankId;
        }
        public string TransactionIdGenerator(string AccountId,string BankId)
        {
            string dt = DateTime.Now.ToString("yyyyMMddHHmmss");
            string TransID = "TXN"+BankId+AccountId+ dt;
            return TransID;
        }

    }
}
