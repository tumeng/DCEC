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
 * 功能概述：部门定义
 * 作    者：caoly
 * 创建时间：2013-12-11
 * 修改时间：2013-12-11
 */

public partial class Rmes_epd1900 : Rmes.Web.Base.BasePage
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
        ASPxGridView1.DataSource = DepartmentFactory.GetAll();
        ASPxGridView1.DataBind();
    }

    protected void ASPxGridView1_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        //判断当前记录是否可以删除
        //string strDelCode = e.Values["PLAN_CODE"].ToString();

        string rmes_id = e.Values["RMES_ID"].ToString();
        DepartmentEntity detEntity = new DepartmentEntity { RMES_ID = rmes_id };

        db.Delete(detEntity);

        setCondition();
        e.Cancel = true;
    }


    protected void ASPxGridView1_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        //插入新纪录，应先做检查是否合法

        DepartmentEntity detEntity = new DepartmentEntity();

        detEntity.COMPANY_CODE = theCompanyCode;
        detEntity.DEPT_CODE = e.NewValues["DEPT_CODE"].ToString().Trim();
        detEntity.DEPT_NAME = e.NewValues["DEPT_NAME"].ToString().Trim();
        detEntity.PARENT_DEPT = e.NewValues["PARENT_DEPT"].ToString().Trim();
        detEntity.DEPT_REMARK = e.NewValues["DEPT_REMARK"].ToString().Trim();
        
        db.Insert(detEntity);

        e.Cancel = true;
        ASPxGridView1.CancelEdit();
        setCondition();

    }


    protected void ASPxGridView1_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        //修改记录

        string rmes_id = e.OldValues["RMES_ID"].ToString();

        DepartmentEntity detEntity = DepartmentFactory.GetByKey(rmes_id);

        detEntity.DEPT_NAME = e.NewValues["DEPT_NAME"].ToString().Trim();
        detEntity.PARENT_DEPT = e.NewValues["PARENT_DEPT"].ToString().Trim();
        detEntity.DEPT_REMARK = e.NewValues["DEPT_REMARK"].ToString().Trim();

        db.Update(detEntity);

        e.Cancel = true;
        ASPxGridView1.CancelEdit();
        setCondition();

    }
    protected void ASPxGridView1_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
    {
        if (ASPxGridView1.IsNewRowEditing)
        {
            
            checkUnique(e);
            if (e.NewValues["DEPT_CODE"].ToString() == "" || e.NewValues["DEPT_CODE"].ToString() == null)
            {
                e.RowError = "部门代码不能为空！";
            }
            if (e.NewValues["DEPT_NAME"].ToString() == "" || e.NewValues["DEPT_NAME"].ToString() == null)
            {
                e.RowError = "部门名称不能为空！";
            }
        }
        
    }
    private void checkUnique(DevExpress.Web.Data.ASPxDataValidationEventArgs e)
    {
        string sql = string.Format("select * from CODE_DEPT where DEPT_CODE='{0}'", e.NewValues["DEPT_CODE"].ToString().Trim());
        DataTable dt = dc.GetTable(sql);
        if (dt.Rows.Count == 1)
        {
            e.RowError = "部门代码重复！";
        }
    }
    protected void ASPxGridView1_HtmlEditFormCreated(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewEditFormEventArgs e)
    {
        //创建Editform，对其中某些字段进行属性设置

        if (!ASPxGridView1.IsNewRowEditing && ASPxGridView1.IsEditing)
        {
            ///主键不可以修改
            (ASPxGridView1.FindEditFormTemplateControl("txtDeptCode") as ASPxTextBox).Enabled = false;
        }
    }
}