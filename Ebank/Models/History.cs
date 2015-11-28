using System;

namespace Ebank.Controllers
{
    public class History
    {
        public string User_Id { get; set; }
        public DateTime Start_Date { get; set; }
        public DateTime End_Date { get; set; }
        public string From { get;set; }
        public string InsertTime { get; set; }
        public string To { get;  set; }
        public string Status { get; set; }
        public string FinishTime { get;set; }
        public string Amount { get; internal set; }
        public string Type { get;  set; }
        public string Account { get; set; }
        public string Currency { get; set; }
        public string Summary { get; set; }
    }
}