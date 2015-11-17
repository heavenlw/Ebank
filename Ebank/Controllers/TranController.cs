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
            var status = mysqlhelper.ChecktheAccount(ref trans);
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
                if (trans.Type == "1")
                {
                    MysqlHelper mysqlhelper = new MysqlHelper();
                    trans.Type = mysqlhelper.CheckTheInnerAccount(trans);

                    return mysqlhelper.TransPush(trans);
                }
                else
                {
                    return "Not a Transfer";
                }
               
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

        [HttpGet]
        public Swift CheckSwift(string code)
        {
            Swift swift = new Swift();
            swift.Code = "BKCHCNBJ42A";
            swift.Bank_Name = "BANK OF CHINA GUANGZHOU BRANCH";
            swift.Bank_Address = "19 CHANGDI ROAD - GUANGZHOU - CHINA";
            return swift;
        }
        [HttpPost]
        public string Swift(Swift swift)
        {
            MysqlHelper mysqlhelper = new MysqlHelper();
            var status =  mysqlhelper.OverSeaTrans(swift);
            return status;
        }

    }   
}
