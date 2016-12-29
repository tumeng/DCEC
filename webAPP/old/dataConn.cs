using System;
using System.Configuration;
using System.Data;
using Oracle.DataAccess.Client;
using Rmes.DA.Base;
namespace Rmes.Pub.Data
{
	public class dataConn
	{
		public OracleConnection theConn = null;
		public OracleCommand theComd = null;
		public string theSql = null;
		public string theConnStr = null;
		public string theProviderName = null;
        private void _openConn()
        {
            //this.theConn = DB.GetInstance().Connection as OracleConnection;

            string connstr = System.Configuration.ConfigurationManager.ConnectionStrings["oracle"].ToString();
                    Oracle.DataAccess.Client.OracleConnection conn = new Oracle.DataAccess.Client.OracleConnection(connstr);
                    theConn = conn;
                   //conn.Open();
            
            OpenConn();
            this.theConnStr = this.theConn.ConnectionString;
            this.theProviderName = "Oracle.DataAccess.Client";
            this.theComd = theConn.CreateCommand();
            this.theSql = "";
        }
		public dataConn()
		{
            if (theConn == null) 
                _openConn();
            else 
                OpenConn();
		}
		public dataConn(string thisSql)
		{
            _openConn();
			this.theSql = thisSql;
			this.theComd.CommandText = this.theSql;
		}
		public void setTheSql(string thisSql)
		{
			this.theSql = thisSql;
            this.theComd.CommandText = this.theSql;
		}
		public DataTable GetTable()
		{
            return GetTable(this.theSql);
           
		}
		public DataTable GetTable(string thisSql)
		{
			OracleDataAdapter oracleDataAdapter = new OracleDataAdapter();
            DataTable dt = new DataTable();
            this.theComd.CommandText = thisSql;
			oracleDataAdapter.SelectCommand = this.theComd;
			oracleDataAdapter.Fill(dt);
			return dt;
		}
		public bool GetState()
		{
            //OracleDataAdapter oracleDataAdapter = new OracleDataAdapter();
            //DataSet dataSet = new DataSet();
            //this.theComd.CommandText = this.theSql;
            //oracleDataAdapter.SelectCommand = this.theComd;
            //oracleDataAdapter.Fill(dataSet);
            //return dataSet.Tables[0].Rows.Count > 0;
            return GetState(this.theSql);
		}
		public bool GetState(string thisSql)
		{
            object obj = GetValue(thisSql);
            if (obj == null) return false;
            return true;
            
		}
		public string GetValue()
		{
            return GetValue(this.theSql);
           
		}
		public string GetValue(string thisSql)
		{
            return DB.GetInstance().ExecuteScalar<string>(thisSql);
           
		}
		public int ExeSql(string theSql)
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
		public void OpenConn()
		{
			if (this.theConn.State != ConnectionState.Open)
			{
				this.theConn.Open();
			}
		}
		public void CloseConn()
		{
			if (this.theConn.State != ConnectionState.Closed)
			{
				this.theConn.Close();
				this.theComd = null;
				this.theConn = null;
			}
		}
	}
}
