namespace Ebank.Controllers
{
    public class Card
    {
        public string No { get; set; }
        public string Password { get; set; }
        public string Status { get; internal set; }
        public string Name { get; set; }
        public string Bank_Id { get; set; }
        public string User_Id { get; set; }
        public string Id { get; set; }
        public string Message_Code { get; set; }
    }
}