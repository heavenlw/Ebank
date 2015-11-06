using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Ebank.Controllers
{
    public class LoginController : ApiController
    {
        [HttpGet]
        public Question getquestion(string name)
        {
            
            MysqlHelper mysqlhelper = new MysqlHelper();
            return mysqlhelper.GetUserQueston(name);
        }
        [HttpPost]
        public string LoginUser([FromBody]User user)
        {
            MysqlHelper mysqlhelper = new MysqlHelper();
           return  mysqlhelper.SearchUser(user);
        }
    }
}
