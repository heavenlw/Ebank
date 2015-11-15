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
        public string CheckAndChange(Trans trans)
        {
            MysqlHelper mysqlhelper = new MysqlHelper();
            var status = mysqlhelper.ChecktheAccount(trans);
            if (status == "success")
            {
                return mysqlhelper.UpdateAccount(trans);
            }
            else
            {
                return status;
            }


        }

        [HttpPost]
        public string Transfer([FromBody]Trans trans)
        {
            //return "Success";
            var status = CheckAndChange(trans);
            if (status == "Success")
            {
                MysqlHelper mysqlhelper = new MysqlHelper();
                return mysqlhelper.TransPush(trans);
            }
            else
            {
                return status;
            }
        }
        public string CheckPassword([FromBody]Card card)
        {
            MysqlHelper mysqlhelper = new MysqlHelper();
          return   mysqlhelper.CheckSavingAccount(ref card).Status;
        }
        [HttpPost]
        public string CheckRec([FromBody] Card card)
        {
            return "Success";

        }
        [HttpPost]
        public string CheckBalance([FromBody]Card card)
        {
            return "Success";
        }

    }   
}
