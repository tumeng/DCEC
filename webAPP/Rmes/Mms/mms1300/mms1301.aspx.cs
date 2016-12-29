/**************************
 * 功能：SAP项目信息导入及维护
 * 创建：20130119
 * 作者：李蒙蒙
 * 

 * 
 **************************/
using System;
using System.Collections;
using System.Configuration;
using System.Data;
using Oracle.DataAccess.Client;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using DevExpress.Web.ASPxEditors;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxClasses;
using Rmes.Pub.Data;
using Rmes.Pub.Function;

public partial class Rmes_mms1301 : Rmes.Web.Base.BasePage
{
    private string theCompanyCode, theUserCode;
    private dataConn theDc = new dataConn();

    protected void Page_Load(object sender, EventArgs e)
    {
        userManager theUserManager = (userManager)Session["theUserManager"];
        theCompanyCode = theUserManager.getCompanyCode();
        theUserCode = theUserManager.getUserId();


        //修改新开页面title，方便用户查看

        string theDisplayProgramName = Request.QueryString["progName"];
        this.Master.Page.Title = theDisplayProgramName;

        initGrid();
    }

    private void initGrid()
    {
        string sql = "SELECT VENDOR_CODE,VENDOR_NAME,'A' VENDOR_TYPE_CODE,'供应商' VENDOR_TYPE_NAME,SAP_TIME,STATUS,DECODE(STATUS,'0','未处理','1','已处理') STATUS_NAME,"+
                     "USERNAME,WMS_TIME FROM SAP_CODE_VENDOR A WHERE STATUS='0' AND NOT EXISTS(SELECT * FROM CODE_VENDOR B WHERE A.VENDOR_CODE=B.VENDOR_CODE AND B.TYPE='A')  ";
        sql += " UNION ALL(SELECT CUSTOMER_CODE VENODR_CODE,CUSTOMER_NAME VENDOR_NAME,'B' VENDOR_TYPE_CODE,'销售商' VENDOR_TYPE_NAME,SAP_TIME,STATUS,"+
               "DECODE(STATUS,'0','未处理','1','已处理') STATUS_NAME,USERNAME,WMS_TIME FROM SAP_CODE_CUSTOMER C WHERE STATUS='0' "+
               "AND NOT EXISTS(SELECT * FROM CODE_VENDOR D WHERE C.CUSTOMER_CODE=D.VENDOR_CODE AND D.TYPE='B') ) ";

        ASPxGridView1.DataSource = theDc.GetTable(sql);
        ASPxGridView1.DataBind();
    }

    protected void btnXlsExport_Click(object sender, EventArgs e)
    {
        //gridExport.WriteXlsToResponse("SAP项目信息导入清单");
    }

    protected void ASPxGridView1_CustomButtonCallback(object sender, ASPxGridViewCustomButtonCallbackEventArgs e)
    {
        string SQL = "";
        DataTable dt = new DataTable();
        //string itemcode =ASPxGridView1.GetRowValues(e.VisibleIndex, "ITEM_CODE").ToString();
        string itemcode = ASPxGridView1.GetRowValues(e.VisibleIndex, "VENDOR_CODE").ToString();
        string itemname = ASPxGridView1.GetRowValues(e.VisibleIndex, "VENDOR_NAME").ToString();
        string dealstatus = ASPxGridView1.GetRowValues(e.VisibleIndex, "STATUS").ToString();
        string itemtype = ASPxGridView1.GetRowValues(e.VisibleIndex, "VENDOR_TYPE_CODE").ToString();

        if (e.ButtonID == "btnDealItem")
        {
            if (dealstatus != "0")
            {
                ASPxGridView1.JSProperties.Add("cpCallbackName", "alert");
                ASPxGridView1.JSProperties.Add("cpCallbackValue", "此项目已导入，可在往来单位信息维护中继续进行相应维护！");
                return;
            }
            else
            {
                SQL = "select * from code_vendor where vendor_code='" + itemcode + "' and type='"+itemtype+"'";
                dt = theDc.GetTable(SQL);
                if (dt.Rows.Count > 0)
                {
                    ASPxGridView1.JSProperties.Add("cpCallbackName", "alert");
                    ASPxGridView1.JSProperties.Add("cpCallbackValue", "此项目已存在，可在往来单位信息维护中对其进行相应修改！");
                    return;
                }
                else

                    SQL = "insert into code_vendor(company_code,vendor_code,vendor_name,type) values('1100','" + itemcode + "','" + itemname + "','" + itemtype + "')";
                    theDc.ExeSql(SQL);

                    if (itemtype == "A")
                    {
                        SQL = "update sap_code_vendor set status='1' where vendor_code='" + itemcode + "' and status='0'";
                    }
                    else
                    {
                        SQL = "update sap_code_customer set status='1' where customer_code='" + itemcode + "' and status='0'";
                    }
                    theDc.ExeSql(SQL);

                    ASPxGridView1.JSProperties.Add("cpCallbackName", "alert");
                    ASPxGridView1.JSProperties.Add("cpCallbackValue", "处理成功！");
                    initGrid();
                    
                }

            }
        }


    



}
