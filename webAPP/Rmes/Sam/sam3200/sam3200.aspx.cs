/**************************
 * ���ܣ��û����ʼ�¼��ѯ
 * 
 * ˵�������û�һ��ʱ���ڷ��ʵĳ����¼���в�ѯ
 * 
 * ���ߣ�������
 * 
 * �޶���20110803
 * 
 * ��֪���⣺1����̬��ѯ������Ҫ����������һ�����ž�����
 *          
 */

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
using Rmes.Pub.Data;
using Rmes.Pub.Function;

public partial class Rmes_Sam_sam3200_sam3200 : Rmes.Web.Base.BasePage
{
    public String theSql;
    public String companyCode;
    public dataConn theDC = new dataConn();
    protected void Page_Load(object sender, EventArgs e)
    {
        userManager theUserManager = (userManager)Session["theUserManager"];
        companyCode = theUserManager.getCompanyCode();
        this.TranslateASPxControl(ASPxGridView1);
        theSql = "SELECT * FROM VW_DATA_USER_LOG WHERE company_code='"+companyCode+"'";

        DataTable dt = theDC.GetTable(theSql + " order by visit_time");

        ASPxGridView1.DataSource = dt;
        ASPxGridView1.DataBind();

    }
    //protected void Button2_Click(object sender, EventArgs e)
    //{
    //    setCondition();
    //}
    //private void setCondition()
    //{
    //    String selDate = ((DateTime)Calender1.Value).ToString("yyyy-MM-dd");
    //    String userName = txtUserName.Text.Trim();

    //    String theConditonStr = "";
    //    if (selDate != "" && selDate != null)
    //    {
    //        theConditonStr += " and trunc(a.login_time)=to_date('" + selDate + "','yyyy-mm-dd') ";
    //    }
    //    if (userName != "" && userName != null)
    //    {
    //        theConditonStr += " and b.user_name like '" + userName + "' ";
    //    }

    //    DataTable dt = theDC.GetTable(theSql + " " + theConditonStr + " order by a.COMPANY_CODE,SESSION_CODE");

    //    ASPxGridView1.DataSource = dt;
    //    ASPxGridView1.DataBind();

    //}

}
