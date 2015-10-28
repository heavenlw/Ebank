using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Ebank.Controllers
{
    public class SignUpController : ApiController
    {
        [HttpGet]
        public IHttpActionResult GetQuestion(string question)
        {
            //MysqlHelper mysqlhelper = new MysqlHelper();
            if (question == "1")
            {
                MysqlHelper mysqlhelper = new MysqlHelper();
                //return mysqlhelper.GetQuestionList();
                return Ok(mysqlhelper.GetQuestionList());
            }
            else return null;
        }
        [HttpGet]
        public bool NameSearch(string name)
        {
            MysqlHelper mysqlhelper = new MysqlHelper();
            return mysqlhelper.SearchName(name);
        }
       
       
    }
}