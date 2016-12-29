using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using DevExpress.Web.ASPxGridLookup;
using DevExpress.Web.ASPxClasses;
using DevExpress.Web.ASPxEditors;

using PetaPoco;
using Rmes.DA.Base;
using Rmes.DA.Entity;
using Rmes.DA.Procedures;
using Rmes.Pub.Data;
using Rmes.Web.Base;

/**
 * 功能概述：新增物料替换规则专用页面
 * 作    者：caoly
 * 创建时间：2014-04-19
 */


public partial class Rmes_mms1402 : BasePage
{
    private dataConn dc = new dataConn();
    private DataTable dt = new DataTable();

    public Database db = DB.GetInstance();
    private PubCs pc = new PubCs();
    public string theCompanyCode, theUserID;

    protected void Page_Load(object sender, EventArgs e)
    {
        userManager theUserManager = (userManager)Session["theUserManager"];
        theCompanyCode = theUserManager.getCompanyCode();
        theUserID = theUserManager.getUserId();
    }


    protected void butConfirm_Click(object sender, EventArgs e)
    {
        string oldBom, oldName, newBom, newName;

        oldBom = TextOldBOM.Text.Trim();
        oldName = TextOldName.Text.Trim();
        newBom = TextNewBOM.Text.Trim();
        newName = TextNewName.Text.Trim();

        string sql = "select * from data_xk_bom_exchange where item_code_from='" + oldBom + "' and  item_code_to='" + newBom + "' and enable_flag='Y'";
        if (dc.GetTable(sql).Rows.Count != 0)
        {
            lblMessage.Text = "已存在相同物料替换规则！原物料代码：" + oldBom + "；新代码：" + newBom + "";
            TextOldBOM.Text = "";
            TextOldName.Text = "";
            TextNewBOM.Text = "";
            TextNewName.Text = "";
            return;
        }

        BomExchangeEntity bomRule = new BomExchangeEntity
        {
            PROJECT_CODE = "",
            ITEM_CODE_FROM = oldBom,
            ITEM_NAME_FROM = oldName,
            ITEM_CODE_TO = newBom,
            ITEM_NAME_TO = newName,
            CREAT_TIME = DateTime.Now,
            ENABLE_FLAG = "Y",
            USER_ID = theUserID,
            USE_COUNT = 0,
            WORK_CODE = ""
        };

        string id = db.Insert(bomRule).ToString();

        TextOldBOM.Text = "";
        TextOldName.Text = "";
        TextNewBOM.Text = "";
        TextNewName.Text = "";

        lblMessage.Text = "新增物料替换规则成功！原物料代码：" + oldBom + "；新代码：" + newBom + "";
    }


    protected void butCloseWindow_Click(object sender, EventArgs e)
    {
        Response.Write("<script type='text/javascript'>window.opener.location.reload();window.close();</script>");
    }

    protected void ASPxCbSubmit_Callback(object source, DevExpress.Web.ASPxCallback.CallbackEventArgs e)
    {
        char[] charSeparators = new char[] { ',' };
        string[] collection = e.Parameter.Split(charSeparators);
        string p = collection[0].ToString();
        string code = collection[1].ToString();

        string sql = "", strName;

        switch (p)
        {
            case "checkOld":
                sql = "select item_name from data_xk_oldbom where item_code='" + code + "'";
                dt = dc.GetTable(sql);
                if (dt.Rows.Count == 0)
                {
                    e.Result = "noOldCode," + code; ;
                }
                else
                {
                    strName=dt.Rows[0][0].ToString();
                    e.Result = "oldCode," + strName;
                }
                break;
        }
    }
}