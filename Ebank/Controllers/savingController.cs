﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Ebank.Controllers
{
    public class savingController : ApiController
    {
        [HttpPost]
        public List<Saving> CheckAccount(User user)
        {
            
            MysqlHelper mysqlhelper = new MysqlHelper();
            var user_id = mysqlhelper.SearchID(user.Name, user.Session);
           return  mysqlhelper.GetAllSavingAccount(user_id);
        }
        [HttpPost]
        public List<Saving> CheckAccountById(User user)
        {

                MysqlHelper mysqlhelper = new MysqlHelper();
            //var user_id = mysqlhelper.SearchID(user.Name, user.Password);
            return mysqlhelper.GetAllSavingAccount(user.Id);
        }
        [HttpPost]
        public string GetHisList([FromBody]History history)
        {
            if (history.Start_Date > history.End_Date)
                return "";
            DateTime beijing = DateTime.Now.ToUniversalTime().AddHours(8);
            var html = "";
            if (history.Start_Date.Year == 1 || history.End_Date.Year == 1||history.Start_Date<beijing.AddMonths(-3))
            {
                history.Start_Date = beijing.AddMonths(-3);
                history.End_Date =beijing;
            }
            MysqlHelper mysqlhelper = new MysqlHelper();
           List<History> his_list =  mysqlhelper.GetHistory(history);
            foreach (History his in his_list)
            {
               
                    html += string.Format("<tr><td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td><td>{4}</td></tr>",his.Type,his.From,his.To,his.Amount+" "+his.Currency,his.Start_Date);
              
            }
            return html;

        }
        [HttpPost]
        public string GetOneHisList([FromBody]History history)
        {
            if (history.Start_Date > history.End_Date)
                return "";
            DateTime beijing = DateTime.Now.ToUniversalTime().AddHours(8);
            var html = "";
            if (history.Start_Date.Year == 1 || history.End_Date.Year == 1 || history.Start_Date < beijing.AddMonths(-3))
            {
                history.Start_Date = beijing.AddMonths(-3);
                history.End_Date = beijing;
            }
            MysqlHelper mysqlhelper = new MysqlHelper();
            List<History> his_list = mysqlhelper.GetAccountHistory(history);
            foreach (History his in his_list)
            {
                  html += string.Format("<tr><td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td></tr>", his.Type, his.Start_Date,his.Amount, his.Currency);
               
               
            }
            return html;

        }

        [HttpPost]
        public string SetCommon(Saving saving)
        {
            MysqlHelper mysqlhelper = new MysqlHelper();
            var status = mysqlhelper.SetCommon(saving);
            return status;
           
           
        }
    }
}
