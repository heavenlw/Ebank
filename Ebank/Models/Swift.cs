namespace Ebank.Controllers
{
    public class Swift
    {
        public string Swift_Code{ get;  set; }
        public string Payee_Address { get;  set; }
        public string Payee_Name { get;  set; }
        public string Payee_Account_Num { get; set; }
        public string Payee_Account_Bank { get; set; }
        public string Payee_Account_Address { get; set; }
        public string Payer_Account_Num { get; set; }
        public string Payer_Name { get; set; }
        public string Payer_Hk_Id { get; set; }

        public string Payer_Amount { get; set; }
        public string Payer_Currency { get; set; }
        public string Code { get; internal set; }
        public string Bank_Name { get; internal set; }
        public string Bank_Address { get; internal set; }
    }
}