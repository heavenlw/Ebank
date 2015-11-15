using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Ebank.Controllers
{
    public class savingController : ApiController
    {
        [HttpPost]
        public List<Saving> CheckAccount(User user)
        {
            
            MysqlHelper mysqlhelper = new MysqlHelper();
            var user_id = mysqlhelper.SearchID(user.Name, user.Password);
           return  mysqlhelper.GetAllSavingAccount(user_id);
        }
        [HttpPost]
        public List<Saving> CheckAccountById(User user)
        {

                MysqlHelper mysqlhelper = new MysqlHelper();
            //var user_id = mysqlhelper.SearchID(user.Name, user.Password);
            return mysqlhelper.GetAllSavingAccount(user.Id);
        }
        [HttpPost]
        public string GetHisList([FromBody]History history)
        {
            var html = "";
            MysqlHelper mysqlhelper = new MysqlHelper();
           List<History> his_list =  mysqlhelper.GetUserHistory(history);
            foreach (History his in his_list)
            {
                if (his.Type == "1")
                {
                    html += string.Format("<tr><td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td><td>{4}</td></tr>","Out",his.From,his.To,his.Amount,his.InsertTime);
                }
                if (his.Type== "2")
                {
                    html += string.Format("<tr><td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td><td>{4}</td></tr>", "In", his.From, his.To, his.Amount, his.InsertTime);
                }
            }
            return html;

        }
        //public string GetSavingAccount()
    }
}
