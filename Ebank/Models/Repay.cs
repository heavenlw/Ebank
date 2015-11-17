namespace Ebank.Controllers
{
    public class Repay
    {
        public string Bill_Id { get; set; }
        public string Amount { get; set; }
        public string Repay_Account { get; set;}
        public string Type { get; set; }
        public string Credit_Account { get; set;}
        public string User_Id { get; set; }
    }
}