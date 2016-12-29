using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Oracle.DataAccess.Client;
using Rmes.Pub.Data;
using Rmes.DA.Base;
using Rmes.DA.Procedures;
 

namespace Rmes.Pub.Data
{
    /// <summary>
    /// management login
    /// </summary>
    public class loginManager
    {
        public static string status_Succeed="0";
        public static string Status_NoUser = "1";
        public static string Status_WrongPassword = "2";
        public static string Status_UserForbid = "3";
        public static string Status_IpForbid = "4";
        public static string Status_UserOverFlow = "5";
        public static string Status_CompanyUserOverFlow = "6";
        public static string Status_UserLocked = "7";
        public static string Status_UnknowError = "8";


        public Boolean theHaveInited = false;
        public string theLoginFlag = Status_UnknowError;
        public string theCompanyCode = "";
        public string theSessionCode = "";

        //定义用户名称
        public string theUserName = "";
        public string theUserCode = "";

        public loginManager()
        {
            //
            // 构造函数
            //
            theHaveInited = true;

        }
        public string getTheLoginFlag() {
            return theLoginFlag;

        }

        public void setCompanyCode(string thisCompanyCode)
        {
            theCompanyCode = thisCompanyCode;
        }
        public string getTheCompanyCode()
        {
            return theCompanyCode;

        }
        public void setUserName(string thisUserName)
        {
            theUserName = thisUserName;
        }
        public string getUserName()
        {
            return theUserName;

        }
        public void setUserCode(string thisUserCode)
        {
            theUserCode = thisUserCode;
        }
        public string getUserCode()
        {
            return theUserCode;

        }
        public Boolean loginIn(string thisUserId,string thisUserPassword,string thisClientIp,string thisPlineCode) { 
        //用存储过程来处理，减少并发数量
            if (!theHaveInited) {
                return false;
            }
            try 
            { 
                string theRetStr="";
                string theRetSessionCode="";
                string theRetUserName = "";
                string theRetUserCode = "";

                MW_HANDLE_LOGIN sp = new MW_HANDLE_LOGIN() { 
                    THECOMPANYCODE1=theCompanyCode,
                    THEUSERID1 = thisUserId,
                    THEPASSWORD1 = thisUserPassword,
                    THECLIENTIP1 = thisClientIp,
                    THEPLINECODE1 = thisPlineCode,
                    THERETSTR1="",
                    THERETSESSIONCODE1="",
                    THERETUSERNAME1="",
                    THERETUSERCODE1=""
                };
                Procedure.run(sp);
                theRetStr = sp.THERETSTR1;
                theRetSessionCode = sp.THERETSESSIONCODE1;
                theRetUserName = sp.THERETUSERNAME1;
                theRetUserCode = sp.THERETUSERCODE1;

                //dataConn theDataConn=new dataConn();
                //theDataConn.theComd.CommandType=CommandType.StoredProcedure;
                //theDataConn.theComd.CommandText ="MW_HANDLE_LOGIN";

                //theDataConn.theComd.Parameters.Add("THECOMPANYCODE1",OracleDbType.Varchar2).Value = theCompanyCode;
                ////theDataConn.theComd.Parameters.Add("@THECOMPANYCODE1", SqlDbType.VarChar).Direction = ParameterDirection.Input;

                //theDataConn.theComd.Parameters.Add("THEUSERID1", OracleDbType.Varchar2).Value = thisUserId;
                ////theDataConn.theComd.Parameters.Add("@THEUSERCODE1", SqlDbType.VarChar).Direction = ParameterDirection.Input;

                //theDataConn.theComd.Parameters.Add("THEPASSWORD1", OracleDbType.Varchar2).Value = thisUserPassword;
                ////theDataConn.theComd.Parameters.Add("@THEPASSWORD1", SqlDbType.VarChar).Direction = ParameterDirection.Input;

                //theDataConn.theComd.Parameters.Add("THECLIENTIP1", OracleDbType.Varchar2).Value = thisClientIp;
                ////theDataConn.theComd.Parameters.Add("@THECLIENTIP1", SqlDbType.VarChar).Direction = ParameterDirection.Input;

                //theDataConn.theComd.Parameters.Add("THEPLINECODE1", OracleDbType.Varchar2).Value = thisPlineCode;
                ////theDataConn.theComd.Parameters.Add("@THEPLINECODE1", SqlDbType.VarChar).Direction = ParameterDirection.Input;

                //theDataConn.theComd.Parameters.Add("THERETSTR1", OracleDbType.Varchar2, 50).Direction = ParameterDirection.Output;
                ////theRetStr = (string)theDataConn.theComd.Parameters.Add("THERETSTR1", OracleDbType.Varchar2).Value;

                //theDataConn.theComd.Parameters.Add("THERETSESSIONCODE1", OracleDbType.Varchar2, 50).Direction = ParameterDirection.Output;
                ////theRetSessionCode = (string)theDataConn.theComd.Parameters.Add("THERETSESSIONCODE1", OracleDbType.Varchar2).Value;

                //theDataConn.theComd.Parameters.Add("THERETUSERNAME1", OracleDbType.Varchar2, 50).Direction = ParameterDirection.Output;
                ////theRetSessionCode = (string)theDataConn.theComd.Parameters.Add("THERETSESSIONCODE1", OracleDbType.Varchar2).Value;

                //theDataConn.theComd.Parameters.Add("THERETUSERCODE1", OracleDbType.Varchar2, 50).Direction = ParameterDirection.Output;
                ////theRetSessionCode = (string)theDataConn.theComd.Parameters.Add("THERETSESSIONCODE1", OracleDbType.Varchar2).Value;

                //theDataConn.OpenConn();
                //theDataConn.theComd.ExecuteNonQuery();

                //theRetStr = theDataConn.theComd.Parameters["THERETSTR1"].Value.ToString();
                //theRetSessionCode = theDataConn.theComd.Parameters["THERETSESSIONCODE1"].Value.ToString();
                //theRetUserName = theDataConn.theComd.Parameters["THERETUSERNAME1"].Value.ToString();
                //theRetUserCode = theDataConn.theComd.Parameters["THERETUSERCODE1"].Value.ToString();       
         
                //theDataConn.CloseConn();


                theLoginFlag = theRetStr;

                theSessionCode = theRetSessionCode;
                theUserName = theRetUserName;
                theUserCode = theRetUserCode;
                return true;

            }
            catch(Exception e){
                Console.WriteLine("error in loginManage:"+e.Message);
                return false;
            
            }


        }
        public Boolean loginOut(string thisSessionCode)
        {
            //用存储过程来处理，减少并发数量,处理用户退出
            if (!theHaveInited)
            {
                return false;
            }
            try
            {
                MW_HANDLE_LOGOUT sp = new MW_HANDLE_LOGOUT() { 
                    THECOMPANYCODE1 = theCompanyCode,
                    THESESSIONCODE1 = thisSessionCode
                };
                Procedure.run(sp);

                //dataConn theDataConn = new dataConn();
                //theDataConn.theComd.CommandType = CommandType.StoredProcedure;
                //theDataConn.theComd.CommandText = "MW_HANDLE_LOGOUT";

                //theDataConn.theComd.Parameters.Add("THECOMPANYCODE1", OracleDbType.Varchar2).Value = theCompanyCode;
                ////theDataConn.theComd.Parameters.Add("@THECOMPANYCODE1", SqlDbType.VarChar).Direction = ParameterDirection.Input;

                //theDataConn.theComd.Parameters.Add("THESESSIONCODE1", OracleDbType.Varchar2).Value = thisSessionCode;
                ////theDataConn.theComd.Parameters.Add("@THESESSIONCODE1", SqlDbType.VarChar).Direction = ParameterDirection.Input;

                //theDataConn.OpenConn();
                //theDataConn.theComd.ExecuteNonQuery();


                //theDataConn.CloseConn();

                return true;

            }
            catch (Exception e)
            {
                Console.WriteLine("error in loginOutManage:" + e.Message);
                return false;

            }


        }

        //处理重新登录
        public Boolean ReLoginIn(string thisUserId, string thisUserPassword, string thisClientIp, string thisSessionCode,string thisPlineCode)
        {
            //用存储过程来处理，减少并发数量
            if (!theHaveInited)
            {
                return false;
            }
            try
            {
                string theRetStr = "";
                string theRetSessionCode = "";
                string theRetUserName = "";
                string theRetUserCode = "";
                MW_HANDLE_RELOGIN sp = new MW_HANDLE_RELOGIN()
                {
                    THECOMPANYCODE1 = theCompanyCode,
                    THEUSERID1 = thisUserId,
                    THEPASSWORD1 = thisUserPassword,
                    THECLIENTIP1 = thisClientIp,
                    THESESSIONCODE1 = thisSessionCode,
                    THEPLINECODE1 = thisPlineCode,
                    THERETSTR1 = "",
                    THERETSESSIONCODE1="",
                    THERETUSERNAME1 ="",
                    THERETUSERCODE1 = "",
                };
                Procedure.run(sp);

                theRetStr = sp.THERETSTR1;
                theRetSessionCode = sp.THERETSESSIONCODE1;

                theRetUserName = sp.THERETUSERNAME1;
                theRetUserCode = sp.THERETUSERCODE1;

                //dataConn theDataConn = new dataConn();
                //theDataConn.theComd.CommandType = CommandType.StoredProcedure;
                //theDataConn.theComd.CommandText = "MW_HANDLE_RELOGIN";

                //theDataConn.theComd.Parameters.Add("THECOMPANYCODE1", OracleDbType.Varchar2).Value = theCompanyCode;
                ////theDataConn.theComd.Parameters.Add("@THECOMPANYCODE1", SqlDbType.VarChar).Direction = ParameterDirection.Input;

                //theDataConn.theComd.Parameters.Add("THEUSERID1", OracleDbType.Varchar2).Value = thisUserId;
                ////theDataConn.theComd.Parameters.Add("@THEUSERCODE1", SqlDbType.VarChar).Direction = ParameterDirection.Input;

                //theDataConn.theComd.Parameters.Add("THEPASSWORD1", OracleDbType.Varchar2).Value = thisUserPassword;
                ////theDataConn.theComd.Parameters.Add("@THEPASSWORD1", SqlDbType.VarChar).Direction = ParameterDirection.Input;

                //theDataConn.theComd.Parameters.Add("THECLIENTIP1", OracleDbType.Varchar2).Value = thisClientIp;
                ////theDataConn.theComd.Parameters.Add("@THECLIENTIP1", SqlDbType.VarChar).Direction = ParameterDirection.Input;

                //theDataConn.theComd.Parameters.Add("THESESSIONCODE1", OracleDbType.Varchar2).Value = thisSessionCode;
                ////theDataConn.theComd.Parameters.Add("@THESESSIONCODE1", SqlDbType.VarChar).Direction = ParameterDirection.Input;

                //theDataConn.theComd.Parameters.Add("THEPLINECODE1", OracleDbType.Varchar2).Value = thisPlineCode;
                ////theDataConn.theComd.Parameters.Add("@THEPLINECODE1", SqlDbType.VarChar).Direction = ParameterDirection.Input;

                //theDataConn.theComd.Parameters.Add("THERETSTR1", OracleDbType.Varchar2, 50).Direction = ParameterDirection.Output;
                ////theRetStr = (string)theDataConn.theComd.Parameters.Add("THERETSTR1", OracleDbType.Varchar2).Value;

                //theDataConn.theComd.Parameters.Add("THERETSESSIONCODE1", OracleDbType.Varchar2, 50).Direction = ParameterDirection.Output;
                ////theRetSessionCode = (string)theDataConn.theComd.Parameters.Add("THERETSESSIONCODE1", OracleDbType.Varchar2).Value;

                //theDataConn.theComd.Parameters.Add("THERETUSERNAME1", OracleDbType.Varchar2, 50).Direction = ParameterDirection.Output;
                ////theRetSessionCode = (string)theDataConn.theComd.Parameters.Add("THERETSESSIONCODE1", OracleDbType.Varchar2).Value;

                //theDataConn.theComd.Parameters.Add("THERETUSERCODE1", OracleDbType.Varchar2, 50).Direction = ParameterDirection.Output;
                ////theRetSessionCode = (string)theDataConn.theComd.Parameters.Add("THERETSESSIONCODE1", OracleDbType.Varchar2).Value;

                //theDataConn.OpenConn();
                //int n = theDataConn.theComd.ExecuteNonQuery();


 
                //theRetStr = theDataConn.theComd.Parameters["THERETSTR1"].Value.ToString();
                //theRetSessionCode = theDataConn.theComd.Parameters["THERETSESSIONCODE1"].Value.ToString();

                //theRetUserName = theDataConn.theComd.Parameters["THERETUSERNAME1"].Value.ToString();
                //theRetUserCode = theDataConn.theComd.Parameters["THERETUSERCODE1"].Value.ToString(); 

                //theDataConn.CloseConn();



                theLoginFlag = theRetStr;
                theSessionCode = theRetSessionCode;
                theUserName  = theRetUserName;
                theUserCode = theRetUserCode; 
                return true;
            }
            catch (Exception e) {
                Console.WriteLine("error in reloginManage:" + e.Message);
                return false;
            }



        }

        ~loginManager() { }

    }
}
