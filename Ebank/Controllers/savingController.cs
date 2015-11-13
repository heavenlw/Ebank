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
        public List<History> GetHisList([FromBody]History history)
        {
            MysqlHelper mysqlhelper = new MysqlHelper();
        return    mysqlhelper.GetUserHistory(history);

        }
    }
}
