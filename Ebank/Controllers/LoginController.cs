using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.ServiceModel.Channels;
using System.Web;
using System.Web.Http;

namespace Ebank.Controllers
{
    public class LoginController : ApiController
    {
        [HttpGet]
        public List<Curr> getCurrency()
        {

            MysqlHelper mysqlhelper = new MysqlHelper();
            return mysqlhelper.GetCurrency();
        }
        [HttpGet]
        public Question getquestion(string name)
        {
            
            MysqlHelper mysqlhelper = new MysqlHelper();
            return mysqlhelper.GetUserQueston(name);
        }
        [HttpPost]
        public User LoginUser([FromBody]User user)
        {
             MysqlHelper mysqlhelper = new MysqlHelper();
           // return "Success";
          var status =   mysqlhelper.SearchUser(user);
            if (status.Status == "Success")
            {
              string ip =  GetClientIp();
                {
                    mysqlhelper.UpdateLoginStatus(status,ip);
                }
            }
            return status;
        }
        [HttpPost]
        public User CheckQuestion([FromBody]User user)
        {
            MysqlHelper mysqlhelper = new MysqlHelper();
            var status= mysqlhelper.CheckQestion(user);
            if (status.Status == "Success")
            {
                string ip = GetClientIp();
                {
                    mysqlhelper.UpdateLoginStatus(status, ip);
                }
            }
            return status;

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

    }
}
