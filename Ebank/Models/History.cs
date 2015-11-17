using System;

namespace Ebank.Controllers
{
    public class History
    {
        public string User_Id { get; set; }
        public DateTime Start_Date { get; set; }
        public DateTime End_Date { get; set; }
        public string From { get; internal set; }
        public string InsertTime { get; internal set; }
        public string To { get; internal set; }
        public string Status { get; internal set; }
        public string FinishTime { get; internal set; }
        public string Amount { get; internal set; }
        public string Type { get; internal set; }
        public string Account { get; set; }
        public string Currency { get; set; }
        public string Summary { get; internal set; }
    }
}