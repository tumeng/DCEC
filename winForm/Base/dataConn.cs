using System;
using System.Configuration;
using System.Data;
using Oracle.DataAccess.Client;
using Rmes.DA.Base;
using System.ComponentModel;
using System.Collections.Generic;
namespace Rmes.Pub.Data1
{
    public class dataConn
    {
        private static OracleConnection theConn = null;
        private static OracleCommand theComd = null;
        private static string theSql = null;
        private static string theConnStr = null;
        private static string theProviderName = null;
        private static void _openConn()
        {
            theConn = DB.GetInstance().Connection as OracleConnection;

            //string connstr = System.Configuration.ConfigurationManager.ConnectionStrings["oracle"].ToString();
            //Oracle.DataAccess.Client.OracleConnection conn = new Oracle.DataAccess.Client.OracleConnection(connstr);
            //theConn = conn;
            //conn.Open();

            OpenConn();
            theConnStr = theConn.ConnectionString;
            theProviderName = "Oracle.DataAccess.Client";
            theComd = theConn.CreateCommand();
            theSql = "";
        }
        static dataConn()
        {
            if (theConn == null)
                _openConn();
            else
                OpenConn();
        }
        //static dataConn(string thisSql)
        //{
        //    _openConn();
        //    theSql = thisSql;
        //    theComd.CommandText = theSql;
        //}

        //public static void setTheSql(string thisSql)
        //{
        //    theSql = thisSql;
        //    theComd.CommandText = theSql;
        //}
        public static DataTable GetTable()
        {
            return GetTable(theSql);

        }
        public static DataTable GetTable(string thisSql)
        {
            OracleDataAdapter oracleDataAdapter = new OracleDataAdapter();
            DataTable dt = new DataTable();
            theComd.CommandText = thisSql;
            oracleDataAdapter.SelectCommand = theComd;
            oracleDataAdapter.Fill(dt);
            return dt;

            //DataTable dt = new DataTable();
            //List<Object> lst = DB.GetInstance().Fetch<Object>(thisSql);
            //dt = PublicClass1.ListToDataTable(lst,"A");
            //return dt;
        }

        public static bool GetState()
        {
            //OracleDataAdapter oracleDataAdapter = new OracleDataAdapter();
            //DataSet dataSet = new DataSet();
            //this.theComd.CommandText = this.theSql;
            //oracleDataAdapter.SelectCommand = this.theComd;
            //oracleDataAdapter.Fill(dataSet);
            //return dataSet.Tables[0].Rows.Count > 0;
            return GetState(theSql);
        }
        public static bool GetState(string thisSql)
        {
            object obj = GetValue(thisSql);
            if (obj == null) return false;
            return true;

        }
        public static string GetValue()
        {
            return GetValue(theSql);

        }
        public static string GetValue(string thisSql)
        {
            return DB.GetInstance().ExecuteScalar<string>(thisSql);

        }
        public static int ExeSql(string theSql)
        {
            return DB.GetInstance().Execute(theSql);
            //this.theComd.CommandText = theSql;
            //this.OpenConn();
            //return this.theComd.ExecuteNonQuery();
        }

        //public void InsertBlob(byte[] blobContent)
        //{
        //    this.OpenConn();
        //    this.theComd.Parameters.Add("b",OracleDbType.Blob, blobContent.Length).Value = blobContent;
        //    this.theComd.ExecuteNonQuery();
        //}
        public static void OpenConn()
        {
            if (theConn.State != ConnectionState.Open)
            {
                theConn.Open();
            }
        }
        public static void CloseConn()
        {
            if (theConn.State != ConnectionState.Closed)
            {
                theConn.Close();
                theComd = null;
                theConn = null;
            }
        }
    }
}