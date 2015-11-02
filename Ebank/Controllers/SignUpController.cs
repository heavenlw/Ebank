using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Security;
namespace Ebank.Controllers
{
    public class SignUpController : ApiController
    {
        [HttpGet]
        public IHttpActionResult GetQuestion(string question)
        {

            User user = new User();
            user.Name = "Awol";
            user.Password = "test";
            user.Hk_Id = "11";
            user.Question_Id = 1;
            user.Question_Answer = "luowei";
            string json = JsonConvert.SerializeObject(user);
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
        [HttpPost]
        public string CreateUser([FromBody]User users)
        {
            //XmlDocument xdoc = new XmlDocument();
            string xml = "<code>luowei</code>";

            string secode = Awol.SecurityHelper.GenerateMD5Hash(DateTime.Now.ToString());
           // string code = System.
            User user = new User();
            user.Name = "Awol";
            user.Password = "test";
            user.Hk_Id = "11";
            user.Question_Id = 1;
            user.Question_Answer = "luowei";
           return   JsonConvert.SerializeObject(user);
        }
        [HttpPost]
        public string CreateAccount([FromBody]User users)
        {
            //XmlDocument xdoc = new XmlDocument();
            string xml = "<code>luowei</code>";

            string secode = Awol.SecurityHelper.GenerateMD5Hash(DateTime.Now.ToString());
            // string code = System.
            User user = new User();
            user.Name = "Awol";
            user.Password = "test";
            user.Hk_Id = "11";
            user.Question_Id = 1;
            user.Question_Answer = "luowei";
            return JsonConvert.SerializeObject(user);
        }


    }
}