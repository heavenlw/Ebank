using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Ebank.Controllers
{
    public class BillController : ApiController
    {
        [HttpPost]
        public string GetBill(SelfBill bill)
        {
            return "Success";

        }
        
    }
}
