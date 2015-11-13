using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Ebank.Controllers
{
    public class TranController : ApiController
    {
        [HttpGet]
        public List<Bank> GetBank()
        {
            MysqlHelper mysqlhelper = new MysqlHelper();
          return   mysqlhelper.GetBankList();
        }
        public string 

        [HttpPost]
        public string Transfer([FromBody]Trans trans)
        {
            
            MysqlHelper mysqlhelper = new MysqlHelper();
            return mysqlhelper.TransPush(trans);
        }
        public string CheckPassword([FromBody]Card card)
        {
            MysqlHelper mysqlhelper = new MysqlHelper();
          return   mysqlhelper.CheckSavingAccount(ref card).Status;
        }
    }   
}
