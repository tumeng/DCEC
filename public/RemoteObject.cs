using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Data.Odbc;

namespace Rmes.Public
{
    public class RemoteObject: MarshalByRefObject
    {
        public DataTable GetXikaiInformixDBTable(string sql)
        {
            OdbcConnection conn2;
            string connStr2 = "DSN=ifmx;DRIVER=IBM INFORMIX ODBC DRIVER (64-bit);UID=mis8;PWD=atmn63;PRO=onsoctcp;SERV=1525;SRVR=ids94;HOST=192.1.1.192;DATABASE=x_xkscgl0605";
            conn2 = new OdbcConnection(connStr2);
            conn2.Open();
            DataTable table1 = new DataTable();
            OdbcDataAdapter oda = new OdbcDataAdapter(conn2.CreateCommand());
            oda.SelectCommand.CommandText = sql;
            oda.SelectCommand.CommandType = CommandType.Text;
            oda.Fill(table1);
            conn2.Close();
            return table1;
        }

        public List<string> GetXikaiInformixDBList(string xmdh, string lx)
        {
            OdbcConnection conn2, conn1;
            string connStr1 = "DSN=ifmx;DRIVER=IBM INFORMIX ODBC DRIVER (64-bit);UID=mis8;PWD=atmn63;PRO=onsoctcp;SERV=1525;SRVR=ids94;HOST=192.1.1.192;DATABASE=x_xkscgl0605";
            string connStr2 = "DSN=ifmx;DRIVER=IBM INFORMIX ODBC DRIVER (64-bit);UID=mis8;PWD=atmn63;PRO=onsoctcp;SERV=1525;SRVR=ids94;HOST=192.1.1.192;DATABASE=xk40_test";
            conn1 = new OdbcConnection(connStr1);
            conn2 = new OdbcConnection(connStr2);
            conn1.Open();
            conn2.Open();
            string _sqltemp = "select first 1 xmdj,xmbgym,xmckdm,xmgydm from xmzwj_jcsj where xmdh='{0}'";
            string LLDJ = "0.0", LLBGY = "", LLKFDM = "", LLGYDM = "";
            _sqltemp = string.Format(_sqltemp, xmdh);
            OdbcDataReader _odr;
            if (xmdh != null && lx.Equals("WX"))
            {
                //如果是外协部件，切换到另外一个xk40_test库去查询单价、保管员等信息
                OdbcCommand _cmd2 = conn2.CreateCommand();
                _cmd2.CommandText = _sqltemp;
                _odr = _cmd2.ExecuteReader();

            }
            else
            {
                OdbcCommand _cmd1 = conn1.CreateCommand();
                _cmd1.CommandText = _sqltemp;
                _odr = _cmd1.ExecuteReader();

            }

            List<string> result = new List<string>();
            if (_odr.HasRows)
            {
                result.Add(_odr[0].ToString());
                result.Add(_odr[1].ToString());
                result.Add(_odr[2].ToString());
                result.Add(_odr[3].ToString());
            }
            _odr.Close();
            conn1.Close();
            conn2.Close();
            return result;
        }
    }

}
