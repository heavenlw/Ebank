using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Security;
using System.Web;
using System.ServiceModel.Channels;

namespace Ebank.Controllers
{
    public class SignUpController : ApiController
    {
        [HttpGet]
        public IHttpActionResult GetQuestion()
        {

                MysqlHelper mysqlhelper = new MysqlHelper();
                //return mysqlhelper.GetQuestionList();
                return Ok(mysqlhelper.GetQuestionList());
            
        }
        [HttpGet]
        public string checkip()
        {
            return GetClientIp();
        }
        [HttpGet]
        public bool CheckID(string hkid,string type)
        {

         // string value =   GetClientIp(null);
                //MysqlHelper mysqlhelper = new MysqlHelper();

                MysqlHelper mysqlhelper = new MysqlHelper();
                //return mysqlhelper.GetQuestionList();
                return mysqlhelper.CheckId(hkid,type);
        
        }
        [HttpGet]
        public bool NameSearch(string name)
            {
            MysqlHelper mysqlhelper = new MysqlHelper();
            return mysqlhelper.SearchName(name);
        }
        [HttpPost]
        public bool CreateUser([FromBody]User users)
        {

           
            MysqlHelper mysqlhelper = new MysqlHelper();
          return   mysqlhelper.UpdateTheUser(users);
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
      
       
        private string GetClientIp(HttpRequestMessage request = null)
        {
            request = request ?? Request;

            if (request.Properties.ContainsKey("MS_HttpContext"))
            {
                return ((HttpContextWrapper)request.Properties["MS_HttpContext"]).Request.UserHostAddress;
            }
            else if (request.Properties.ContainsKey(RemoteEndpointMessageProperty.Name))
            {
                RemoteEndpointMessageProperty prop = (RemoteEndpointMessageProperty)request.Properties[RemoteEndpointMessageProperty.Name];
                return prop.Address;
            }
            else if (HttpContext.Current != null)
            {
                return HttpContext.Current.Request.UserHostAddress;
            }
            else
            {
                return null;
            }
        }

        [HttpPost]
        public string CheckBase([FromBody]User user)
        {
            return null;
        }


    }
}