/**************************
 * 功能：用户在线管理
 * 
 * 说明：对在线用户进行查询，可按显示的字段进行动态查询
 * 
 * 作者：李蒙蒙
 * 
 * 修订：20110803
 * 
 * 已知问题：1、导出功能按钮需要在皮肤文件中设置格式风格（待杨工开发）
 *           2、动态查询功能需要汉化（待下一步安排决定）
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

public partial class Rmes_Sam_sam3100_sam3100 :Rmes.Web.Base.BasePage
{
    public String theSql;
    public String companyCode;
    public dataConn theDC = new dataConn();
    protected void Page_Load(object sender, EventArgs e)
    {
        userManager theUserManager = (userManager)Session["theUserManager"];
        companyCode = theUserManager.getCompanyCode();

        theSql = "SELECT * FROM VW_DATA_SESSION WHERE company_code='"+companyCode+"'";

        DataTable dt = theDC.GetTable(theSql + " order by COMPANY_CODE,SESSION_CODE");

        ASPxGridView1.DataSource = dt;
        ASPxGridView1.DataBind();


    }

    protected void btnPdfExport_Click(object sender, EventArgs e)
    {
        gridExport.WritePdfToResponse();
    }
    protected void btnXlsExport_Click(object sender, EventArgs e)
    {
        gridExport.WriteXlsToResponse();
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
