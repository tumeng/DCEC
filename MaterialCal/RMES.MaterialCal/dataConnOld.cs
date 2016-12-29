using System;
using System.Collections.Generic;
using System.Text;
//using System.Data.OracleClient;
using Oracle.DataAccess.Client;
using System.Data;

namespace RMES.MaterialCal_OLD
{//取消
    public class dataConn
    {
        public OracleConnection theConn = null;
        public OracleCommand theComd = null;
        public string theSql = null;

        public dataConn()
        {
            //
            // 构造函数
            //
            theConn = new OracleConnection();
            //WINFORM里面的写法，与WEB有区别
            theConn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["oracle"].ToString();
 
            theComd = new OracleCommand();
            theComd.Connection = theConn;
            theSql = "";

        }
        public dataConn(string thisSql)
        {
            //
            // 带参数的构造函数
            //
            theConn = new OracleConnection();
            theConn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["oracle"].ToString();
            theComd = new OracleCommand();
            theComd.Connection = theConn;
            theSql = thisSql;
            theComd.CommandText = theSql;
        }

        public void setTheSql(string thisSql)
        {
            theSql = thisSql;
        }
        public DataTable GetTable()
        {
            return GetTable(this.theSql);
        }
        public DataTable GetTable(String thisSql)
        {
            //
            //  得到数据集
            //
            theSql = thisSql;
            if (theConn.State != ConnectionState.Open) theConn.Open();
            OracleDataAdapter theDa = new OracleDataAdapter(thisSql,theConn);
            DataTable dtTemp = new DataTable();
            theDa.Fill(dtTemp);

            return dtTemp;

        }
        public bool GetState()
        {
            return GetState(this.theSql);
        }
        public Boolean GetState(String thisSql)
        {
            //
            // 得到语句执行状态，看是否有结果记录，针对select语句，判断数据库是否有满足条件的记录
            //
            OracleDataAdapter theDa = new OracleDataAdapter();
            DataSet theDs = new DataSet();
            theSql = thisSql;
            theComd.CommandText = theSql;
            theDa.SelectCommand = theComd;
            theDa.Fill(theDs);

            if (theDs.Tables[0].Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }


        }
        public string GetValue()
        {
            return GetValue(this.theSql);
        }
        public string GetValue(String thisSql)
        {
            //
            // 得到语句值
            //
            OracleDataAdapter theDa = new OracleDataAdapter();
            DataTable theDt = new DataTable();
            theSql = thisSql;
            theComd.CommandText = theSql;
            theDa.SelectCommand = theComd;
            theDa.Fill(theDt);

            if (theDt.Rows.Count > 0)
            {
                return theDt.Rows[0][0].ToString();
            }
            else
            {
                return "error";
            }

        }
 

        public int ExeSql(String thisSql)
        {
            theSql = thisSql;
            theComd.CommandText = theSql;
            OpenConn();
            int i = theComd.ExecuteNonQuery();
            CloseConn();
            return i;
        }

        public void OpenConn()
        {
            //
            // 打开数据连接
            //
            if (theConn.State != ConnectionState.Open)
            {
                theConn.Open();
            }

        }

        public void CloseConn()
        {
            //
            // 关闭数据连接
            //
            if (theConn.State != ConnectionState.Closed)
            {
                theConn.Close();
                //theComd = null;
                //theConn = null;

            }
        }
    }
}
