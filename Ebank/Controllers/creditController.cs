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
           // return "Success";
            MysqlHelper mysqlhelper = new MysqlHelper();
            var status = mysqlhelper.ChecktheAccount(trans);
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
    }
}
