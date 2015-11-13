using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;

namespace Ebank.Controllers
{
    internal class MysqlHelper
    {
        private static string connStr_local = System.Configuration.ConfigurationManager.AppSettings["Conn1"];
        public MysqlHelper()
        {
        }

        public List<Question> GetQuestionList()
        {
            List<Question> question_list = new List<Question>();
            string sql = string.Format("select * from question_list");
            DataSet testDataSet = null;
            MySqlConnection conn = new MySqlConnection(connStr_local);
            try
            {
                conn.Open();
                // 创建一个适配器
                MySqlDataAdapter adapter = new MySqlDataAdapter(sql, conn);
                // 创建DataSet，用于存储数据.
                testDataSet = new DataSet();
                // 执行查询，并将数据导入DataSet.
                adapter.Fill(testDataSet, "result_data");
            }
            // 关闭数据库连接.
            catch (Exception e)
            {
                //log4net.ILog log = log4net.LogManager.GetLogger("MyLogger");
                //log.Debug(e.Message);
                Console.WriteLine(e.Message);
                return question_list;
                //Console.ReadLine();

            }
            finally
            {
                conn.Close();
            }
            if (testDataSet != null && testDataSet.Tables["result_data"] != null && testDataSet.Tables["result_data"].Rows != null)
            {
                foreach (DataRow testRow in testDataSet.Tables["result_data"].Rows)
                {
                    Question question = new Question();
                    int id = Convert.ToInt32(testRow["id"].ToString());
                    string name = testRow["question"].ToString();
                    question.Id = id;
                    question.Word = name;
                    question_list.Add(question);

                }
            }
            return question_list;
        }

        internal Card CheckSavingAccount(ref Card card)
        {
            string sql = string.Format("select * from saving_account where id='{0}'",card.No);
            DataSet testDataSet = null;
            MySqlConnection conn = new MySqlConnection(connStr_local);
            try
            {
                conn.Open();
                // 创建一个适配器
                MySqlDataAdapter adapter = new MySqlDataAdapter(sql, conn);
                // 创建DataSet，用于存储数据.
                testDataSet = new DataSet();
                // 执行查询，并将数据导入DataSet.
                adapter.Fill(testDataSet, "result_data");
            }
            // 关闭数据库连接.
            catch (Exception e)
            {
                //log4net.ILog log = log4net.LogManager.GetLogger("MyLogger");
                //log.Debug(e.Message);
                Console.WriteLine(e.Message);
                //Console.ReadLine();

            }
            finally
            {
                conn.Close();
            }
            if (testDataSet != null && testDataSet.Tables["result_data"] != null && testDataSet.Tables["result_data"].Rows != null && testDataSet.Tables["result_data"].Rows.Count > 0)
            {
                if (testDataSet.Tables["result_data"].Rows[0]["password"].ToString() ==card.Password)
                {
                    card.Status = "Success";
                    return card;
                }
                else
                {

                    updateErrorPassword(card);
                    card.Status = "Password Error";
                    return card;
                }
            }
            else
            {
                try
                {
                    card.Status = "No this user";
                    return card;
                }
                catch (Exception t)
                {
                    card.Status = "System Error";
                    return card;
                }
            }
        }

        private void updateErrorPassword(Card card)
        {
            throw new NotImplementedException();
        }

        internal string TransPush(Trans trans)
        {
            string sql = string.Format("insert into trans_log set tr_from='{0}',tr_to='{1}',amount='{2}'", trans.From,trans.To,trans.Amount);
            string error = null;
            MySqlConnection conn = null;
            try
            {
                conn = new MySqlConnection(connStr_local);
                conn.Open();
                MySqlDataAdapter adapter = new MySqlDataAdapter(sql, conn);
                // 创建DataSet，用于存储数据.
                DataSet testDataSet = new DataSet();
                // 执行查询，并将数据导入DataSet.
                adapter.Fill(testDataSet, "result_data");
                return "Success";
            }
            catch (Exception t)
            {
                return "error";
            }
        }

        internal List<Bank> GetBankList()
        {
            List<Bank> bank_list = new List<Bank>();
            string sql = string.Format("select * from bank");
            DataSet testDataSet = null;
            MySqlConnection conn = new MySqlConnection(connStr_local);
            try
            {
                conn.Open();
                // 创建一个适配器
                MySqlDataAdapter adapter = new MySqlDataAdapter(sql, conn);
                // 创建DataSet，用于存储数据.
                testDataSet = new DataSet();
                // 执行查询，并将数据导入DataSet.
                adapter.Fill(testDataSet, "result_data");
            }
            // 关闭数据库连接.
            catch (Exception e)
            {
                //log4net.ILog log = log4net.LogManager.GetLogger("MyLogger");
                //log.Debug(e.Message);
                Console.WriteLine(e.Message);
                return bank_list;
                //Console.ReadLine();

            }
            finally
            {
                conn.Close();
            }
            if (testDataSet != null && testDataSet.Tables["result_data"] != null && testDataSet.Tables["result_data"].Rows != null)
            {
                foreach (DataRow testRow in testDataSet.Tables["result_data"].Rows)
                {
                    Bank bank = new Bank();
                    int id = Convert.ToInt32(testRow["id"].ToString());
                    string name = testRow["name"].ToString();
                    bank.Id = id;
                    bank.Word = name;
                    bank_list.Add(bank);

                }
            }
            return bank_list;
        }

        internal List<Saving> GetAllSavingAccount(string user_id)
        {
            List<Saving> saving_list = new List<Saving>();
            string sql = string.Format("select * from saving_account where user_id ='{0}'", user_id);
            DataSet testDataSet = null;
            MySqlConnection conn = new MySqlConnection(connStr_local);
            try
            {
                conn.Open();
                // 创建一个适配器
                MySqlDataAdapter adapter = new MySqlDataAdapter(sql, conn);
                // 创建DataSet，用于存储数据.
                testDataSet = new DataSet();
                // 执行查询，并将数据导入DataSet.
                adapter.Fill(testDataSet, "result_data");
            }
            // 关闭数据库连接.
            catch (Exception e)
            {
                //log4net.ILog log = log4net.LogManager.GetLogger("MyLogger");
                //log.Debug(e.Message);
                Console.WriteLine(e.Message);
                //Console.ReadLine();

            }
            finally
            {
                conn.Close();
            }
            if (testDataSet != null && testDataSet.Tables["result_data"] != null && testDataSet.Tables["result_data"].Rows != null && testDataSet.Tables["result_data"].Rows.Count > 0)
            {
                foreach (DataRow testRow in testDataSet.Tables["result_data"].Rows)
                {
                    Saving saving = new Saving();
                    string Account_Id = testRow["id"].ToString();
                    string card_num = testRow["card_num"].ToString();
                    string balance = testRow["balance"].ToString();
                    string branch_id = testRow["branch_id"].ToString();
                    string expired_date = testRow["expired_date"].ToString();
                    string open_date = testRow["open_date"].ToString();
                    string currency_id = testRow["currency_id"].ToString();
                    string status = testRow["status"].ToString();
                    string common_use = testRow["common_use"].ToString();
                    saving.Account_Id = Account_Id;
                    saving.No = card_num;
                    saving.Balance = balance;
                    saving.Branch = branch_id;
                    saving.Open_Date = open_date;
                    saving.Exp_Date = expired_date;
                    saving.Currency_Id = currency_id;
                    saving.Status = status;
                    saving.Common_use = common_use;
                   saving_list.Add(saving);
                }
            }
            else
            {
                return null;
            }
            return saving_list;
        }

        internal void UpdateLoginStatus(User user,string ip)
        {
            string sql = string.Format("update user set login_ip ='{1}' where user.id='{0}'", user.Id,ip);
            string error = null;
            MySqlConnection conn = null;
            try
            {
                conn = new MySqlConnection(connStr_local);
                conn.Open();
                MySqlDataAdapter adapter = new MySqlDataAdapter(sql, conn);
                // 创建DataSet，用于存储数据.
                DataSet testDataSet = new DataSet();
                // 执行查询，并将数据导入DataSet.
                adapter.Fill(testDataSet, "result_data");
            }
            catch (Exception t)
            {

            }
        }

        internal User CheckQestion(User user)
        {
            string sql = string.Format("select * from user where name='{0}'", user.Name);
            DataSet testDataSet = null;
            MySqlConnection conn = new MySqlConnection(connStr_local);
            try
            {
                conn.Open();
                // 创建一个适配器
                MySqlDataAdapter adapter = new MySqlDataAdapter(sql, conn);
                // 创建DataSet，用于存储数据.
                testDataSet = new DataSet();
                // 执行查询，并将数据导入DataSet.
                adapter.Fill(testDataSet, "result_data");
            }
            // 关闭数据库连接.
            catch (Exception e)
            {
                //log4net.ILog log = log4net.LogManager.GetLogger("MyLogger");
                //log.Debug(e.Message);
                Console.WriteLine(e.Message);
                //Console.ReadLine();

            }
            finally
            {
                conn.Close();
            }
            if (testDataSet != null && testDataSet.Tables["result_data"] != null && testDataSet.Tables["result_data"].Rows != null && testDataSet.Tables["result_data"].Rows.Count > 0)
            {
                if (testDataSet.Tables["result_data"].Rows[0]["question_answer"].ToString() == user.Question_Answer)
                {
                    user.Status =  "Success";
                    return user;
                }
                else
                {

                    updateLoginTime(user);
                    user.Status = "Password Error";
                    return user;
                }
            }
            else
            {
                try
                {
                    user.Status = "No this user";
                    return user;
                }
                catch (Exception t)
                {
                    user.Status = "System Error";
                    return user;
                }
            }
        }

        internal bool UpdateTheUser(User user)
        {
            string sql = string.Format("update user set name='{0}',password='{1}',sign_up_date='{2}',question_id='{3}',question_answer='{4}',status=1 where hk_id='{5}'",user.Name,user.Password, DateTime.Now.ToString("yyyy-MM-dd"),user.Question_Id,user.Question_Answer,user.Hk_Id);
            string error = null;
            MySqlConnection conn = null;
            try
            {
                conn = new MySqlConnection(connStr_local);
                conn.Open();
                MySqlDataAdapter adapter = new MySqlDataAdapter(sql, conn);
                // 创建DataSet，用于存储数据.
                DataSet testDataSet = new DataSet();
                // 执行查询，并将数据导入DataSet.
                adapter.Fill(testDataSet, "result_data");
                string sql_two = string.Format("select * from user where hk_id='{0}'", user.Hk_Id);

                DataSet testDataSet_2 = new DataSet();
                try
                {
                    
                    adapter = new MySqlDataAdapter(sql_two, conn);
                    adapter.Fill(testDataSet_2, "result_data");

                }
                catch (Exception e)
                {
                    //return F
                    return false;
                }
                if (testDataSet_2 != null && testDataSet_2.Tables["result_data"] != null && testDataSet_2.Tables["result_data"].Rows != null && testDataSet_2.Tables["result_data"].Rows.Count > 0)
                {
                    if (testDataSet_2.Tables["result_data"].Rows[0]["status"].ToString() == "1")
                        //return "true";
                        return true;
                    else
                        return false;
                    //return false;

                }
                else
                //{
                //    return "dataset_2_0";
                //}
                    return false;
            }
            catch (Exception e)
            {
                error = e.Message;

                return false;
            }
            finally
            {
                conn.Close();
            }
            return false;
        }

        internal User  SearchUser(User user)
        {
            string sql = string.Format("select * from user where name = '{0}'", user.Name);
            DataSet testDataSet = null;
            MySqlConnection conn = new MySqlConnection(connStr_local);
            try
            {
                conn.Open();
                // 创建一个适配器
                MySqlDataAdapter adapter = new MySqlDataAdapter(sql, conn);
                // 创建DataSet，用于存储数据.
                testDataSet = new DataSet();
                // 执行查询，并将数据导入DataSet.
                adapter.Fill(testDataSet, "result_data");
            }
            // 关闭数据库连接.
            catch (Exception e)
            {
                //log4net.ILog log = log4net.LogManager.GetLogger("MyLogger");
                //log.Debug(e.Message);
                Console.WriteLine(e.Message);
                //Console.ReadLine();

            }
            finally
            {
                conn.Close();
            }
            if (testDataSet != null && testDataSet.Tables["result_data"] != null && testDataSet.Tables["result_data"].Rows != null)
            {
                user.Id = testDataSet.Tables["result_data"].Rows[0]["id"].ToString();
                if (testDataSet.Tables["result_data"].Rows[0]["password"].ToString() == user.Password)
                {
                    user.Status = "Success";
                    return user;
                }
                else
                {
                    
                    updateLoginTime(user);
                    user.Status = "Password Error";
                    return user;
                }
            }
            else
            {
                try
                {
                    user.Status = "No this User";
                    return user;
                }
                catch (Exception t)
                {
                   user.Status = "System Error";
                    return user;
                }
            }
        }

        private void updateLoginTime(User user)
        {
            string sql = string.Format("update user set login_count = login_count+1 where user.id='{0}'", user.Id);
            string error = null;
            MySqlConnection conn = null;
            try
            {
                conn = new MySqlConnection(connStr_local);
                conn.Open();
                MySqlDataAdapter adapter = new MySqlDataAdapter(sql, conn);
                // 创建DataSet，用于存储数据.
                DataSet testDataSet = new DataSet();
                // 执行查询，并将数据导入DataSet.
                adapter.Fill(testDataSet, "result_data");
            }
            catch (Exception t)
            {

            }

        }

        internal Question GetUserQueston(string name)
        {
            Question question = new Question();
            string sql = string.Format("select q.id,q.question from user u,question_list q where u.name = '{0}' and u.question_id = q.id", name);
            DataSet testDataSet = null;
            MySqlConnection conn = new MySqlConnection(connStr_local);
            try
            {
                conn.Open();
                // 创建一个适配器
                MySqlDataAdapter adapter = new MySqlDataAdapter(sql, conn);
                // 创建DataSet，用于存储数据.
                testDataSet = new DataSet();
                // 执行查询，并将数据导入DataSet.
                adapter.Fill(testDataSet, "result_data");
            }
            // 关闭数据库连接.
            catch (Exception e)
            {
                //log4net.ILog log = log4net.LogManager.GetLogger("MyLogger");
                //log.Debug(e.Message);
                Console.WriteLine(e.Message);
                //Console.ReadLine();

            }
            finally
            {
                conn.Close();
            }
            if (testDataSet != null && testDataSet.Tables["result_data"] != null && testDataSet.Tables["result_data"].Rows != null && testDataSet.Tables["result_data"].Rows.Count > 0)
            {
                //Question question = new Question();
                question.Word = testDataSet.Tables["result_data"].Rows[0]["question"].ToString();
                question.Id = Convert.ToInt32(testDataSet.Tables["result_data"].Rows[0]["id"].ToString());
                return question;
            }
            else
            {
                return null;
            }
           // return null;
               
        }

        internal bool CheckId(string hkid,string type)
        {
            string sql = string.Format("select * from user where hk_id ='{0}' and hk_id_type='{1}'",hkid,type);
            DataSet testDataSet = null;
            MySqlConnection conn = new MySqlConnection(connStr_local);
            try
            {
                conn.Open();
                // 创建一个适配器
                MySqlDataAdapter adapter = new MySqlDataAdapter(sql, conn);
                // 创建DataSet，用于存储数据.
                testDataSet = new DataSet();
                // 执行查询，并将数据导入DataSet.
                adapter.Fill(testDataSet, "result_data");
            }
            // 关闭数据库连接.
            catch (Exception e)
            {
                //log4net.ILog log = log4net.LogManager.GetLogger("MyLogger");
                //log.Debug(e.Message);
                Console.WriteLine(e.Message);
                //Console.ReadLine();

            }
            finally
            {
                conn.Close();
            }
            if (testDataSet != null && testDataSet.Tables["result_data"] != null && testDataSet.Tables["result_data"].Rows != null && testDataSet.Tables["result_data"].Rows.Count > 0)
            {

                if (testDataSet.Tables["result_data"].Rows[0]["status"].ToString() == "0")
                {
                    return false;
                }
                else
                    return true;
                

            }
            else
            {
                return false;
            }


        }

        internal List<News> GetNews()
        {
            List<News> news_list = new List<News>();
            string sql = string.Format("select * from bank_news");
            DataSet testDataSet = null;
            MySqlConnection conn = new MySqlConnection(connStr_local);
            try
            {
                conn.Open();
                // 创建一个适配器
                MySqlDataAdapter adapter = new MySqlDataAdapter(sql, conn);
                // 创建DataSet，用于存储数据.
                testDataSet = new DataSet();
                // 执行查询，并将数据导入DataSet.
                adapter.Fill(testDataSet, "result_data");
            }
            // 关闭数据库连接.
          catch (Exception e)
            {
                //log4net.ILog log = log4net.LogManager.GetLogger("MyLogger");
                //log.Debug(e.Message);
                Console.WriteLine(e.Message);
                //return question_list;
                //Console.ReadLine();

            }
            finally
            {
                conn.Close();
            }
            if (testDataSet != null && testDataSet.Tables["result_data"] != null && testDataSet.Tables["result_data"].Rows != null)
            {
                foreach (DataRow testRow in testDataSet.Tables["result_data"].Rows)
                {
                    News one_news = new News();

                    string title =testRow["title"].ToString();
                    string summary = testRow["summary"].ToString();
                    string url = testRow["url"].ToString();
                    one_news.Title = title;
                    one_news.Summary = summary;
                    one_news.Url = url;
                    news_list.Add(one_news);

                }
            }
            return news_list;
        }

        internal bool SearchName(string name)
        {
            string sql = string.Format("select * from user where name='{0}'", name);
            DataSet testDataSet = null;
            MySqlConnection conn = new MySqlConnection(connStr_local);
            try
            {
                conn.Open();
                // 创建一个适配器
                MySqlDataAdapter adapter = new MySqlDataAdapter(sql, conn);
                // 创建DataSet，用于存储数据.
                testDataSet = new DataSet();
                // 执行查询，并将数据导入DataSet.
                adapter.Fill(testDataSet, "result_data");
            }
            // 关闭数据库连接.
            catch (Exception e)
            {
                //log4net.ILog log = log4net.LogManager.GetLogger("MyLogger");
                //log.Debug(e.Message);
                Console.WriteLine(e.Message);
                //Console.ReadLine();

            }
            finally
            {
                conn.Close();
            }
            if (testDataSet != null && testDataSet.Tables["result_data"] != null && testDataSet.Tables["result_data"].Rows != null && testDataSet.Tables["result_data"].Rows.Count > 0)
            {

                return true;

            }
            else
            {
                return false;
            }

        }

        public string SearchID(string name, string pass)
        {
            Question question = new Question();
            string sql = string.Format("select * from user where name ='{0}' and password='{1}'",name,pass);
            DataSet testDataSet = null;
            MySqlConnection conn = new MySqlConnection(connStr_local);
            try
            {
                conn.Open();
                // 创建一个适配器
                MySqlDataAdapter adapter = new MySqlDataAdapter(sql, conn);
                // 创建DataSet，用于存储数据.
                testDataSet = new DataSet();
                // 执行查询，并将数据导入DataSet.
                adapter.Fill(testDataSet, "result_data");
            }
            // 关闭数据库连接.
            catch (Exception e)
            {
                //log4net.ILog log = log4net.LogManager.GetLogger("MyLogger");
                //log.Debug(e.Message);
                Console.WriteLine(e.Message);
                //Console.ReadLine();

            }
            finally
            {
                conn.Close();
            }
            if (testDataSet != null && testDataSet.Tables["result_data"] != null && testDataSet.Tables["result_data"].Rows != null && testDataSet.Tables["result_data"].Rows.Count > 0)
            {
                //Question question = new Question();
                var id= testDataSet.Tables["result_data"].Rows[0]["id"].ToString();

                return id;
            }
            else
            {
                return null;
            }
        }
    }
}