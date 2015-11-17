namespace Ebank.Controllers
{
    public class Bill
    {
        public object Account { get; set; }
        public string Currency { get;  set; }
        public string Deadline { get; set; }
        public int Id { get;  set; }
        public string Minimum_repayment { get; set; }
        public int Remain_repayment { get;  set; }
    }
}