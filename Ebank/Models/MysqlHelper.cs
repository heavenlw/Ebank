using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;

namespace Ebank.Controllers
{
    internal class MysqlHelper
    {
        private static string connStr_local = System.Configuration.ConfigurationManager.AppSettings["Conn2"];
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

        internal string CheckServoceCode(User users)
        {
            return "true";
        }

        internal string UpdatePassword(User user)
        {
            string sql = string.Format("update user set password='{0}' where id ='{0}'", user.New_Password);
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
            finally
            {
                conn.Close();
            }
        }

        internal string CheckSession(User user)
        {
            string sql = string.Format("select * from user where id= '{0}'", user.Id);
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

                if (testDataSet != null && testDataSet.Tables["result_data"] != null && testDataSet.Tables["result_data"].Rows != null && testDataSet.Tables["result_data"].Rows.Count > 0)
                {
                    if (user.Session == testDataSet.Tables["result_data"].Rows[0]["session"].ToString())
                    {
                        return "OK";
                    }
                    else
                    {
                        return "Error";
                    }

                }
                else
                {
                    return "Error";
                }
            }
            catch (Exception t)
            {
                return "Error";
            }
            finally
            {
                conn.Close();
            }

        }

        internal string CheckTheInnerAccount(Trans trans)
        {
            string sql = string.Format("select * from saving_account where card_num = '{0}'", trans.To);
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

                if (testDataSet != null && testDataSet.Tables["result_data"] != null && testDataSet.Tables["result_data"].Rows != null && testDataSet.Tables["result_data"].Rows.Count > 0)
                {
                    return "1";

                }
                else
                {
                    return "6";
                }
            }
            catch (Exception t)
            {
                return "6";
            }
            finally
            {
                conn.Close();
            }
        }

        internal string SetCommon(Saving saving)
        {
            string sql = string.Format("update saving_account set common_use=1 where id ='{0}'",saving.Account_Id);
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
            finally
            {
                conn.Close();
            }
        }

        internal Bill GetBill(Bill account)
        {
            Bill bill = new Bill();
            string sql = string.Format("select * from credit_bill as b,currency as c where b.credit_account_num='{0}' and b.currency_id=c.id order by repayment_deadline desc limit 1", account.Account);
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
                return bill;
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
                    int id = Convert.ToInt32(testRow["id"].ToString());
                    int remain_repayment = Convert.ToInt32(testRow["remain_repayment"].ToString());
                    string minimum_repayment = testRow["minimum_repayment"].ToString();
                    string deadline = testRow["repayment_deadline"].ToString();
                    string currency_name = testRow["currency_name"].ToString();
                    bill.Id = id;
                    bill.Remain_repayment = remain_repayment;
                    bill.Minimum_repayment = minimum_repayment;
                    bill.Deadline = deadline;
                    bill.Currency = currency_name;

                }
            }
            return bill;
        }

        public List<Curr> GetCurrency()
        {
            List<Curr> curr_list = new List<Curr>();
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
                return curr_list;
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
                    Curr curr = new Curr();
                    int id = Convert.ToInt32(testRow["id"].ToString());
                    string name = testRow["question"].ToString();
                    curr.Id = id;
                    curr.Word = name;
                    curr_list.Add(curr);

                }
            }
            return curr_list;
        }

        internal string OverSeaTrans(Swift swift)     
        {
            string sql_1 = string.Format("insert into consumption_log set account='{0}',to_account='{1}',amount='{2}',type='{4}',user_id='{3}',currency='{5}';", swift.Payer_Account_Num,swift.Payee_Account_Num,swift.Payer_Amount,swift.User_Id,5,swift.Payer_Currency);
            string sql_2 = string.Format("update saving_account set balance= balance-'{0}' where card_num='{1}'", swift.Payer_Amount, swift.Payer_Account_Num);
            string error = null;
            MySqlConnection conn = null;
            try
            {
                conn = new MySqlConnection(connStr_local);
                conn.Open();
                MySqlDataAdapter adapter = new MySqlDataAdapter(sql_1 + sql_2, conn);
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
            finally
            {
                conn.Close();
            }
        }

        internal void PushToRepayLog(Repay repayment)
        {
            string sql_1 = string.Format("insert into repayment_log set bill_id='{0}',amount='{1}',repay_account='{2}',type='{3}';", repayment.Bill_Id,repayment.Amount,repayment.Repay_Account,repayment.Type);
           string sql_2 = string.Format("update credit_bill set remain_repayment=remain_repayment-{0} where id ='{1}'",repayment.Amount,repayment.Bill_Id);
           // string sql_3 = string.Format("update credit_bill set remain_repayment=remain_repayment-{0} where id ='{1}'", repayment.Amount, repayment.Bill_Id);
            string error = null;
            MySqlConnection conn = null;
            try
            {
                conn = new MySqlConnection(connStr_local);
                conn.Open();
                MySqlDataAdapter adapter = new MySqlDataAdapter(sql_1+sql_2, conn);
                // 创建DataSet，用于存储数据.
                DataSet testDataSet = new DataSet();
                // 执行查询，并将数据导入DataSet.
                adapter.Fill(testDataSet, "result_data");
               
            }
            catch (Exception t)
            {
               
            }
            finally
            {
                conn.Close();
            }
        }

        internal List<Credit> GetAllCreditAccount(string user_id)
        {
            List<Credit> credit_list = new List<Credit>();
            string sql = string.Format("select * from credit_account as ca,currency as c where ca.user_id ='{0}'  and ca.currency_id = c.id", user_id);
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
                    Credit credit_card = new Credit();
                    string Account_Id = testRow["id"].ToString();
                    string card_num = testRow["card_num"].ToString();
                    string credit_amount = testRow["credit_amount"].ToString();
                    string branch_id = testRow["branch_id"].ToString();
                    string type = testRow["type"].ToString();
                    string currency_id = testRow["currency_name"].ToString();
                    string status = testRow["status"].ToString();
                    string a_credit = testRow["available_credit"].ToString();
                    credit_card.Id = Account_Id;
                    credit_card.No = card_num;
                    credit_card.Amount = credit_amount;
                    credit_card.Branch_Id = branch_id;
                    credit_card.Type = type;
                    credit_card.Currency = currency_id;
                    credit_card.Status = status;
                    credit_card.A_Credit = a_credit;
                    credit_list.Add(credit_card);
                 
                }
            }
            else
            {
                return null;
            }
            return credit_list;
        }

        internal List<History> GetAccountHistory(History history)
        {
            List<History> list = new List<History>();
            string sql = string.Format("select * from consumption_log as l,log_type as t,currency as c where l.account='{0}'  and l.type=t.id  and l.currency = c.id order by l.date desc", history.Account, history.End_Date, history.Start_Date);
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
                return list;
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
                    History one_history = new History();
                    string From = testRow["account"].ToString();
                    string To = testRow["to_account"].ToString();
                    string insert_time = testRow["date"].ToString();
                    string currency = testRow["currency_name"].ToString();
                    string status = testRow["status"].ToString();
                    string amount = testRow["amount"].ToString();
                    string type = testRow["type_name"].ToString();
                    string summary = testRow["summary"].ToString();
                    
                    one_history.From = From;
                    one_history.To = To;
                    one_history.InsertTime = insert_time;
                    one_history.Amount = amount;
                    //one_history.FinishTime = finish_time;
                    one_history.Status = status;
                    one_history.Type = type;
                    one_history.Currency = currency;
                    one_history.Summary = summary;
                    one_history.Start_Date = Convert.ToDateTime(insert_time).AddHours(8);
                    if (one_history.Start_Date >= history.Start_Date && one_history.Start_Date <= history.End_Date)
                    {
                        list.Add(one_history);
                    }
                    

                }
            }
            return list;
        }

        internal List<History> GetHistory(History history)
        {
            List<History> list = new List<History>();
            string sql = string.Format("select * from consumption_log as l,log_type as t,currency as c where l.user_id='{0}' and (l.type=1||l.type=6) and l.type=t.id and l.currency=c.id order by l.date desc", history.User_Id,history.End_Date,history.Start_Date);
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
                return list;
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
                    History one_history = new History();
                    string From = testRow["account"].ToString();
                    string To = testRow["to_account"].ToString();
                    string insert_time = testRow["date"].ToString();
                    string currency = testRow["currency_name"].ToString();
                    string status = testRow["status"].ToString();
                    string amount = testRow["amount"].ToString();
                    string type = testRow["type_name"].ToString();
                    string summary = testRow["summary"].ToString();
                    string date = testRow["date"].ToString();
                    one_history.From = From;
                    one_history.To = To;
                    one_history.InsertTime = insert_time;
                    one_history.Amount = amount;
                    one_history.Currency = currency;
                    one_history.Status = status;
                    one_history.Type = type;
                    one_history.Start_Date = Convert.ToDateTime(date).AddHours(8);
                    if (one_history.Start_Date >= history.Start_Date && one_history.Start_Date <= history.End_Date)
                    {
                        list.Add(one_history);
                    }
                }
            }
            return list;
        }

        internal string UpdateAccount(Trans trans)
        {
            string sql_1 = string.Format("insert into consumption_log set account='{0}',to_account='{1}',amount='{2}',type='{4}',user_id='{3}', currency='{5}';", trans.From, trans.To, trans.Amount, trans.User_Id, trans.Type,trans.Currency);
            string sql_2 = string.Format("update saving_account set balance= balance-'{0}' where card_num='{1}';", trans.Amount,trans.From);
            string sql_3 = string.Format("update credit_account set available_credit= available_credit+{0} where card_num='{1}';", trans.Amount,trans.To);
            string sql_4 = string.Format("insert into consumption_log set account='{0}',amount='{1}',type='{3}',user_id='{2}', currency='{4}';", trans.To,trans.Amount, trans.User_Id,"8", trans.Currency);
            string error = null;
            MySqlConnection conn = null;
            try
            {
                conn = new MySqlConnection(connStr_local);
                conn.Open();
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                if (trans.Type == "4")
                {

                    adapter = new MySqlDataAdapter(sql_1 + sql_2 + sql_3 + sql_4, conn);
                }
                else
                {
                    if (trans.Type == "6")
                    {
                        adapter = new MySqlDataAdapter(sql_2, conn);
                    }
                    else
                    {
                        adapter = new MySqlDataAdapter(sql_1 + sql_2, conn);
                    }
                }
                // 创建DataSet，用于存储数据.
                DataSet testDataSet = new DataSet();
                // 执行查询，并将数据导入DataSet.
                adapter.Fill(testDataSet, "result_data");
                return "Success";
            }
            catch (Exception t)
            {
                return "Error";
            }
            finally
            {
                conn.Close();
            }
        }

        internal string ChecktheAccount(ref Trans tran)
        {
            string sql = string.Format("select * from saving_account where card_num='{0}'", tran.From);
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
                if (Convert.ToInt32(testDataSet.Tables["result_data"].Rows[0]["balance"].ToString())>=Convert.ToInt32(tran.Amount))
                {
                    tran.Currency = testDataSet.Tables["result_data"].Rows[0]["currency_id"].ToString();
                    return "success";
                }
                else
                {

                    return "Insufficient balance";
                }
            }
            else
            {
                return "error";
            }
        }

        internal Card CheckSavingAccount(ref Card card)
        {
            string sql = string.Format("select * from saving_account where card_num='{0}'",card.Id);
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
            string sql = string.Format("update saving_account set error_count = error_count+1 where id='{0}'", card.Id);
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
            finally
            {
                conn.Close();
            }
        }

        internal string TransPush(Trans trans)
        {
            string sql_1 = string.Format("insert into consumption_log set account='{0}',to_account='{1}',amount='{2}',type='{4}',user_id='{3}',currency='{5}';", trans.From,trans.To,trans.Amount,trans.User_Id,trans.Type,trans.Currency);
           string sql_2 = string.Format("insert into trans_log set tr_from='{0}',tr_to='{1}',amount='{2}',type='{4}',user_id='{3}';", trans.From, trans.To, trans.Amount, trans.User_Id, trans.Type);
            string sql_3 = "";
            string sql_4 = "";
            if (trans.Type == "1")
            {
                sql_1 = "";
                sql_2 = string.Format("insert into trans_log set tr_from='{0}',tr_to='{1}',amount='{2}',type='{4}',user_id='{3}',status='2';", trans.From, trans.To, trans.Amount, trans.User_Id, trans.Type);
                sql_3 = string.Format("update saving_account set balance = balance+{0} where card_num='{1}';",trans.Amount,trans.To);
                sql_4 = string.Format("insert into consumption_log set account='{0}',amount='{1}',type='{3}',user_id='{2}',currency='{4}',from_account='{5}';", trans.To, trans.Amount, trans.User_Id, "2", trans.Currency,trans.From);
            }
            string error = null;
            MySqlConnection conn = null;
            try
            {
                conn = new MySqlConnection(connStr_local);
                conn.Open();
                MySqlDataAdapter adapter = new MySqlDataAdapter(sql_1+sql_2+sql_3+sql_4, conn);
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
            finally
            {
                conn.Close();
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
            string sql = string.Format("select * from saving_account as s,currency as c,account_status as ast,branch as b where  s.user_id ='{0}' and s.currency_id = c.id  and s.branch_id=b.id and s.status= ast.id", user_id);
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
                    string branch_id = testRow["branch_name"].ToString();
                    string expired_date = testRow["expired_date"].ToString();
                    string open_date = testRow["open_date"].ToString();
                    string currency_id = testRow["currency_name"].ToString();
                    string status = testRow["status_name"].ToString();
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
            finally
            {
                conn.Close();
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
                    user.Id = testDataSet.Tables["result_data"].Rows[0]["id"].ToString();
                    user.Session = testDataSet.Tables["result_data"].Rows[0]["session"].ToString();
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
            string sql = string.Format("update user set name='{0}',password='{1}',sign_up_date='{2}',question_id='{3}',question_answer='{4}',status=1 and real_name='{6}' where hk_id='{5}'", user.Name,user.Password, DateTime.Now.ToString("yyyy-MM-dd"),user.Question_Id,user.Question_Answer,user.Hk_Id,user.RealName);
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
            if (testDataSet != null && testDataSet.Tables["result_data"] != null && testDataSet.Tables["result_data"].Rows != null&& testDataSet.Tables["result_data"].Rows.Count>0)
            {
                user.Id = testDataSet.Tables["result_data"].Rows[0]["id"].ToString();
                if (testDataSet.Tables["result_data"].Rows[0]["password"].ToString() == user.Password)
                {
                    user.Status = "Success";
                    user.Id = testDataSet.Tables["result_data"].Rows[0]["id"].ToString();
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
            finally
            {
                conn.Close();
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
            string sql = string.Format("select * from user where hk_id ='{0}' and hk_id_type='{1}' and status='1'",hkid,type);
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
            string sql = string.Format("select * from user where name ='{0}' and session='{1}'",name,pass);
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