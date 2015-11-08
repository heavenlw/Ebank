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

        internal bool UpdateTheUser(User user)
        {
            string sql = string.Format("update user set name='{0}',password='{1}',sign_up_date='{2}',question_id='{3}',question_answer='{4}',status=1 where hk_id='{5}'",user.Name,user.Password,DateTime.Now,user.Question_Id,user.Question_Answer,user.Hk_Id);
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
                    return false;
                }
                if (testDataSet_2 != null && testDataSet_2.Tables["result_data"] != null && testDataSet_2.Tables["result_data"].Rows != null && testDataSet_2.Tables["result_data"].Rows.Count > 0)
                {
                    if (testDataSet_2.Tables["result_data"].Rows[0]["status"].ToString() == "1")
                        return true;
                    else
                        return false;

                }
                else
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

        internal string  SearchUser(User user)
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
            if (testDataSet != null && testDataSet.Tables["result_data"] != null && testDataSet.Tables["result_data"].Rows != null && testDataSet.Tables["result_data"].Rows.Count > 0)
            {
                if (testDataSet.Tables["result_data"].Rows[0]["password"].ToString() == user.Password)
                {
                    return "Success";
                }
                else
                {
                    return "Password Error";
                }
            }
            else
            {
                try
                {
                    return "No this user";
                }
                catch (Exception t)
                {
                    return "System Error";
                }
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
    }
}