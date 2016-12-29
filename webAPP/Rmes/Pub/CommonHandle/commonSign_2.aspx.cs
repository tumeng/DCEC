using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Rmes.Pub.Function;
using Rmes.Pub.Data;

public partial class Rmes_Pub_CommonHandle_commonSign_2 : System.Web.UI.Page
{
    private String theCompanyCode;
    private PubJs thePubJs = new PubJs();
    private dataConn theDc = new dataConn();
    
    
    private static String REMEND_NOTE_ID;



    private static String WASTER_NOTE_ID;
    private static String state;


    protected void Page_Load(object sender, EventArgs e)
    {
        userManager theUserManager = (userManager)Session["theUserManager"];
        theCompanyCode = theUserManager.getCompanyCode();

        state = (String)Session["state"];

        if (state == "F" || state == "D")
        {

            REMEND_NOTE_ID = (String)Session["REMEND_NOTE_ID"];
        }
        if (state == "F_FP" || state == "D_FP")
        {
            WASTER_NOTE_ID = (String)Session["WASTER_NOTE_ID"];
        }
      
        
    }
    
    protected void btnSure_Click(object sender, ImageClickEventArgs e)
    {
        string selSql = "select USER_PASSWORD,USER_NAME from CODE_USER where COMPANY_CODE='" + theCompanyCode + "' AND USER_CODE='" + txtUserCode.Text.Trim() + "'";
        dataConn selDc = new dataConn(selSql);
        DataTable selDt = selDc.GetTable();


        if (selDt.Rows.Count <= 0)
        {
            thePubJs.Alert("无此用户，请确认");
            return;
        }
        else
        {
            if (selDt.Rows[0]["USER_PASSWORD"].ToString() != txtPassOne.Text.Trim())
            {
                thePubJs.Alert("输入密码错误，请重新输入");
                return;
            }

            string Upd_str="";
            
            //取当前时间
           string ENTER_TIME = DateTime.Now.ToString().Trim();

            if (state == "F")
            {


                Upd_str = "update qms_remend_inform set FIND_UNIT_MAN='" + this.txtUserCode.Text.Trim() + "',FIND_UNIT_DATE=to_date('"+
                ENTER_TIME+"','yyyy-mm-dd hh24:mi:ss')"+
               " where REMEND_NOTE_ID='" + REMEND_NOTE_ID + "'";
            }
            if (state == "D")
            {
                Upd_str = "update qms_remend_inform set DUTY_UNIT_MAN='" + this.txtUserCode.Text.Trim() + "',DUTY_UNIT_DATE=to_date('" +
                ENTER_TIME + "','yyyy-mm-dd hh24:mi:ss')" +
               " where REMEND_NOTE_ID='" + REMEND_NOTE_ID + "'";
            }

            if (state == "F_FP")
            {

                Upd_str = "update qms_waster_inform set CHECK_LEADER_MAN='" + this.txtUserCode.Text.Trim() + "',CHECK_LEADER_DATE=to_date('" +
              ENTER_TIME + "','yyyy-mm-dd hh24:mi:ss')" +
             " where WASTER_NOTE_ID='" + WASTER_NOTE_ID + "'";
                
            }


            if (state == "D_FP")
            {

                Upd_str = "update qms_waster_inform set DUTY_UNIT_MAN='" + this.txtUserCode.Text.Trim() + "',DUTY_UNIT_DATE=to_date('" +
              ENTER_TIME + "','yyyy-mm-dd hh24:mi:ss')" +
             " where WASTER_NOTE_ID='" + WASTER_NOTE_ID + "'";
            }

          
       
            selDc.ExeSql(Upd_str);


            if (state == "F_FP" || state == "D_FP")
            {
                //取得检查站站长签字和责任站签字
                string s = "select CHECK_LEADER_MAN,DUTY_UNIT_MAN,UNQUALIFIED_NOTE_ID,UNQUALIFIED_REASON from  qms_waster_inform where WASTER_NOTE_ID='" + WASTER_NOTE_ID + "'";
                
               selDc.setTheSql(s);
               selDt = selDc.GetTable();

                string CHECK_LEADER_MAN = selDt.Rows[0]["CHECK_LEADER_MAN"].ToString().Trim();
                string DUTY_UNIT_MAN = selDt.Rows[0]["DUTY_UNIT_MAN"].ToString().Trim();
               
                
                //如果都签了，则反填
                if (DUTY_UNIT_MAN != "" && CHECK_LEADER_MAN != "")
                {
                  
                    
                    
                    string UNQUALIFIED_NOTE_ID=selDt.Rows[0]["UNQUALIFIED_NOTE_ID"].ToString().Trim();
                    string UNQUALIFIED_REASON=selDt.Rows[0]["UNQUALIFIED_REASON"].ToString().Trim();




                    string s_upd = "update qms_unqualified_handle set UNQUALIFIED_REASON='" + UNQUALIFIED_REASON + "',CHECK_CONCLUSION='FP' where " +

                        " UNQUALIFIED_NOTE_ID='" + UNQUALIFIED_NOTE_ID + "'";

                    selDc.ExeSql(s_upd);

                }
                
                
            }

            Response.Write("<script>");
            
            Response.Write("opener.location=opener.location;");

            Response.Write("window.close();");

            Response.Write("</script>");

        }
        
    }
}
