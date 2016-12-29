using System;
using System.Collections.Generic;
using System.Text;
//using System.Data.OracleClient;
using Oracle.DataAccess.Client;
using System.Data;

namespace RMES.MaterialCal_OLD
{//ȡ��
    public class dataConn
    {
        public OracleConnection theConn = null;
        public OracleCommand theComd = null;
        public string theSql = null;

        public dataConn()
        {
            //
            // ���캯��
            //
            theConn = new OracleConnection();
            //WINFORM�����д������WEB������
            theConn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["oracle"].ToString();
 
            theComd = new OracleCommand();
            theComd.Connection = theConn;
            theSql = "";

        }
        public dataConn(string thisSql)
        {
            //
            // �������Ĺ��캯��
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
            //  �õ����ݼ�
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
            // �õ����ִ��״̬�����Ƿ��н����¼�����select��䣬�ж����ݿ��Ƿ������������ļ�¼
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
            // �õ����ֵ
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
            // ����������
            //
            if (theConn.State != ConnectionState.Open)
            {
                theConn.Open();
            }

        }

        public void CloseConn()
        {
            //
            // �ر���������
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
