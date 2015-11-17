using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Ebank.Controllers
{
  
    public class creditController : ApiController
    {
        [HttpPost]
        public List<Credit> CheckAccount([FromBody]User user)
        {

            MysqlHelper mysqlhelper = new MysqlHelper();
            var user_id = mysqlhelper.SearchID(user.Name, user.Password);
            return mysqlhelper.GetAllCreditAccount(user_id);
        }
        [HttpPost]
        public string Repayment([FromBody] Repay repayment)
        {
            Trans trans = new Trans();
            trans.From = repayment.Repay_Account;
            trans.Amount = repayment.Amount;
            trans.To = repayment.Credit_Account;
            trans.User_Id = repayment.User_Id;
            trans.Type = repayment.Type;
           // return "Success";
            MysqlHelper mysqlhelper = new MysqlHelper();
            var status = mysqlhelper.ChecktheAccount(ref trans);
            if (status == "success")
            {
                mysqlhelper.PushToRepayLog(repayment);
                return mysqlhelper.UpdateAccount(trans);
            }
            else
            {
                return status;
            }
        }
        [HttpPost]
        public Bill CheckBill([FromBody]Bill account)
        {
            MysqlHelper mysqlhelper = new MysqlHelper();
            Bill bill = mysqlhelper.GetBill(account);

            return bill;
        }
    }
}
