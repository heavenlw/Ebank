using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;

namespace Ebank.Controllers
{
    internal class MysqlHelper
    {
        private static string connStr_local = System.Configuration.ConfigurationManager.AppSettings["conn"];
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