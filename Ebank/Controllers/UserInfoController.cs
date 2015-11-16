using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Ebank.Controllers
{
    public class UserInfoController : ApiController
    {
        [HttpPost]
        public User GetInfo(User user)
        {
            user.RealName = "Awol Law";
            user.Address = "flat2017,tower1,HarbourView Horizion";
            user.Hk_Id = "154***02";
            return user;
        }
    }
}
