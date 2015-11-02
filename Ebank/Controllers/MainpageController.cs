using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Ebank.Controllers
{
    public class MainpageController : ApiController
    {
        [HttpGet]
        public IHttpActionResult GetNews()
        {

            //User user = new User();
            //user.Name = "Awol";
            //user.Password = "test";
            //user.Hk_Id = "11";
            //user.Question_Id = 1;
            //user.Question_Answer = "luowei";
            //string json = JsonConvert.SerializeObject(user);
            //MysqlHelper mysqlhelper = new MysqlHelper();
           
                MysqlHelper mysqlhelper = new MysqlHelper();
                //return mysqlhelper.GetQuestionList();
                return Ok(mysqlhelper.GetNews());
        }
    }
}
