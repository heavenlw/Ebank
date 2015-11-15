namespace Ebank.Controllers
{
    public class Saving
    {
        public string No { get; set; }
        public string Account_Id { get; set; }
        public string Account_Num { get; set; }
        public string Branch { get; set; }
        public string Open_Date { get; set; }
        public string Balance { get; set;}
        public string Exp_Date { get; internal set; }
        public string Currency_Id { get; internal set; }
        public string Status { get; internal set; }
        public string Common_use { get; internal set; }
    }
}