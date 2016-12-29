using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

using DevExpress.Web.ASPxEditors;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxClasses;

//以下引用，如果采用实体类，请全部复制到新页面
using PetaPoco;
using Rmes.DA.Base;
using Rmes.DA.Entity;
using Rmes.DA.Dal;
using Rmes.DA.Factory;
using Rmes.Pub.Data;
using Rmes.Web.Base;

/**
 * 功能概述：车间定义
 * 作    者：caoly
 * 创建时间：2014-04-02
 */

public partial class Rmes_epd2200 : BasePage
{
    private dataConn dc = new dataConn();
    PubCs thePubCs = new PubCs();

    public string theCompanyCode;

   
    public Database db = DB.GetInstance();
    
    protected void Page_Load(object sender, EventArgs e)
    {
        userManager theUserManager = (userManager)Session["theUserManager"];
        theCompanyCode = theUserManager.getCompanyCode();

        setCondition();
    }

    private void setCondition()
    {
        //绑定表数据

        ASPxGridView1.DataSource = WorkShopFactory.GetAll();
        ASPxGridView1.DataBind();
    }

    protected void ASPxGridView1_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        //判断当前记录是否可以删除

        string strCode = e.Values["RMES_ID"].ToString();
        WorkShopEntity entSub = WorkShopFactory.GetByNumber(strCode);

        db.Delete(entSub);

        setCondition();
        e.Cancel = true;
    }


    protected void ASPxGridView1_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        //插入新纪录，应先做检查是否合法

        WorkShopEntity entSub = new WorkShopEntity();

        entSub.COMPANY_CODE = theCompanyCode;
        entSub.WORKSHOP_CODE = e.NewValues["WORKSHOP_CODE"].ToString();
        entSub.WORKSHOP_NAME = e.NewValues["WORKSHOP_NAME"].ToString();
        entSub.WORKSHOP_ADDRESS = e.NewValues["WORKSHOP_ADDRESS"].ToString();
        
        db.Insert(entSub);

        e.Cancel = true;
        ASPxGridView1.CancelEdit();
        setCondition();

    }


    protected void ASPxGridView1_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        //修改记录

        string strCode = e.OldValues["RMES_ID"].ToString();
        WorkShopEntity entSub = WorkShopFactory.GetByNumber(strCode);

        entSub.WORKSHOP_CODE = e.NewValues["WORKSHOP_CODE"].ToString();
        entSub.WORKSHOP_NAME = e.NewValues["WORKSHOP_NAME"].ToString();
        entSub.WORKSHOP_ADDRESS = e.NewValues["WORKSHOP_ADDRESS"].ToString();

        db.Update(entSub);

        e.Cancel = true;
        ASPxGridView1.CancelEdit();
        setCondition();

    }
    protected void ASPxGridView1_HtmlEditFormCreated(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewEditFormEventArgs e)
    {
        //创建Editform，对其中某些字段进行属性设置
        if (!ASPxGridView1.IsNewRowEditing && ASPxGridView1.IsEditing)
        {
            ///主键不可以修改
            (ASPxGridView1.FindEditFormTemplateControl("txtSubCode") as ASPxTextBox).Enabled = false;
        }
    }

    protected void ASPxGridView1_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
    {
        //数据提交前进行校验

    }
}