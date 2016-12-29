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
namespace Rmes.WebApp.Rmes.Qms.qms3300
{
    public partial class qms3300 : BasePage
    {
        private dataConn dc = new dataConn();
        PubCs thePubCs = new PubCs();

        public string theCompanyCode, theUserId, theUserCode;

        public Database db = DB.GetInstance();
        protected void Page_Load(object sender, EventArgs e)
        {
            userManager theUserManager = (userManager)Session["theUserManager"];
            theCompanyCode = theUserManager.getCompanyCode();
            theUserId = theUserManager.getUserId();
            theUserCode = theUserManager.getUserCode();

            setCondition();
        }
        private void setCondition()
        {
            //绑定表数据
            //20161101改为此方式获得查询数据，按录入事件倒序排列
            string sql = "select RMES_ID,COMPANY_CODE,FAULT_CODE,FAULT_NAME,FAULT_DESC,FAULT_CLASS,FAULT_TYPE,INPUT_TIME,INPUT_PERSON "
                      + " from CODE_FAULT where company_code='" + theCompanyCode + "' order by INPUT_TIME desc nulls last,FAULT_CODE";
            DataTable dt1 = dc.GetTable(sql);
            ASPxGridView1.DataSource = dt1;
            ASPxGridView1.DataBind();
        }
         protected void ASPxGridView1_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        //判断当前记录是否可以删除
        //string strDelCode = e.Values["PLAN_CODE"].ToString();

        string rmes_id = e.Values["RMES_ID"].ToString();
        FaultEntity fauEntity = new FaultEntity { RMES_ID = rmes_id };

        //插入到日志表20161101
        try
        {
            string Sql2 = "INSERT INTO CODE_FAULT_LOG (RMES_ID,COMPANY_CODE,FAULT_CODE,FAULT_NAME,FAULT_DESC,FAULT_CLASS,FAULT_TYPE,user_code,flag,rqsj)"
                + " SELECT RMES_ID,COMPANY_CODE,FAULT_CODE,FAULT_NAME,FAULT_DESC,FAULT_CLASS,FAULT_TYPE,'" + theUserCode + "' , 'DEL', SYSDATE FROM CODE_FAULT WHERE  RMES_ID =  '"
                + rmes_id + "'";
            dc.ExeSql(Sql2);
        }
        catch
        {
            return;
        }

        db.Delete(fauEntity);
        

        setCondition();
        e.Cancel = true;
    }


    protected void ASPxGridView1_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        //插入新纪录，应先做检查是否合法

        FaultEntity fauEntity = new FaultEntity();

        fauEntity.COMPANY_CODE = theCompanyCode;
        fauEntity.FAULT_CODE = e.NewValues["FAULT_CODE"].ToString().Trim();
        fauEntity.FAULT_NAME = e.NewValues["FAULT_NAME"].ToString().Trim();
        fauEntity.FAULT_DESC = e.NewValues["FAULT_DESC"].ToString().Trim();
        fauEntity.FAULT_CLASS = e.NewValues["FAULT_CLASS"].ToString().Trim();
        fauEntity.FAULT_TYPE = e.NewValues["FAULT_TYPE"].ToString().Trim();
        fauEntity.INPUT_TIME = DateTime.Now;
        fauEntity.INPUT_PERSON = theUserId;

        
        db.Insert(fauEntity);

        //插入到日志表20161101
        try
        {
            string Sql2 = "INSERT INTO CODE_FAULT_LOG (RMES_ID,COMPANY_CODE,FAULT_CODE,FAULT_NAME,FAULT_DESC,FAULT_CLASS,FAULT_TYPE,user_code,flag,rqsj)"
                + " SELECT RMES_ID,COMPANY_CODE,FAULT_CODE,FAULT_NAME,FAULT_DESC,FAULT_CLASS,FAULT_TYPE,'" + theUserCode + "' , 'ADD', SYSDATE FROM CODE_FAULT WHERE  FAULT_CODE =  '"
                + e.NewValues["FAULT_CODE"].ToString().Trim() + "' AND FAULT_NAME = '" + e.NewValues["FAULT_NAME"].ToString().Trim() + "' ";
            dc.ExeSql(Sql2);
        }
        catch
        {
            return;
        }

        e.Cancel = true;
        ASPxGridView1.CancelEdit();
        setCondition();

    }


    protected void ASPxGridView1_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        //修改记录

        string rmes_id = e.OldValues["RMES_ID"].ToString();

        FaultEntity fauEntity = db.First<FaultEntity>("where RMES_ID=@0",rmes_id);

        fauEntity.FAULT_CODE = e.NewValues["FAULT_CODE"].ToString().Trim();
        fauEntity.FAULT_NAME = e.NewValues["FAULT_NAME"].ToString().Trim();
        fauEntity.FAULT_DESC = e.NewValues["FAULT_DESC"].ToString().Trim();
        fauEntity.FAULT_CLASS = e.NewValues["FAULT_CLASS"].ToString().Trim();
        fauEntity.FAULT_TYPE = e.NewValues["FAULT_TYPE"].ToString().Trim();
        fauEntity.INPUT_TIME = DateTime.Now;
        fauEntity.INPUT_PERSON = theUserId;

        db.Update(fauEntity);

        //插入到日志表20161101
        try
        {
            string Sql2 = "INSERT INTO CODE_FAULT_LOG (RMES_ID,COMPANY_CODE,FAULT_CODE,FAULT_NAME,FAULT_DESC,FAULT_CLASS,FAULT_TYPE,user_code,flag,rqsj)"
                        + " VALUES( '" + rmes_id + "','" + theCompanyCode + "','" + e.OldValues["FAULT_CODE"].ToString() + "',"
                        + e.OldValues["FAULT_NAME"].ToString() + "','"
                        + e.OldValues["FAULT_DESC"].ToString() + "','"
                        + e.OldValues["FAULT_CLASS"].ToString() + "','"
                        + e.OldValues["FAULT_TYPE"].ToString() + "','"
                        + theUserCode + "','BEFOREEDIT',SYSDATE) ";
            dc.ExeSql(Sql2);
            string Sql3 = "INSERT INTO CODE_FAULT_LOG (RMES_ID,COMPANY_CODE,FAULT_CODE,FAULT_NAME,FAULT_DESC,FAULT_CLASS,FAULT_TYPE,user_code,flag,rqsj)"
                        + " VALUES( '" + rmes_id + "','" + theCompanyCode + "','" + e.NewValues["FAULT_CODE"].ToString() + "',"
                        + e.NewValues["FAULT_NAME"].ToString() + "','"
                        + e.NewValues["FAULT_DESC"].ToString() + "','"
                        + e.NewValues["FAULT_CLASS"].ToString() + "','"
                        + e.NewValues["FAULT_TYPE"].ToString() + "','"
                        + theUserCode + "','AFTEREDIT',SYSDATE) ";
            dc.ExeSql(Sql3);
        }
        catch
        {
            return;
        }

        e.Cancel = true;
        ASPxGridView1.CancelEdit();
        setCondition();

    }
    protected void ASPxGridView1_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
    {
        if (ASPxGridView1.IsNewRowEditing)
        {
            
            checkUnique(e);
            if (e.NewValues["FAULT_CODE"].ToString() == "" || e.NewValues["FAULT_CODE"].ToString() == null)
            {
                e.RowError = "缺陷代码不能为空！";
            }
            if (e.NewValues["FAULT_NAME"].ToString() == "" || e.NewValues["FAULT_NAME"].ToString() == null)
            {
                e.RowError = "缺陷名称不能为空！";
            }
        }
        
    }
    private void checkUnique(DevExpress.Web.Data.ASPxDataValidationEventArgs e)
    {
        string sql = string.Format("select * from CODE_FAULT where FAULT_CODE='{0}'", e.NewValues["FAULT_CODE"].ToString().Trim());
        DataTable dt = dc.GetTable(sql);
        if (dt.Rows.Count == 1)
        {
            e.RowError = "缺陷代码重复！";
        }
        
    }
    protected void ASPxGridView1_HtmlEditFormCreated(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewEditFormEventArgs e)
    {
        ////创建Editform，对其中某些字段进行属性设置

        //if (!ASPxGridView1.IsNewRowEditing && ASPxGridView1.IsEditing)
        //{
        //    ///主键不可以修改
        //    (ASPxGridView1.FindEditFormTemplateControl("txtDeptCode") as ASPxTextBox).Enabled = false;
        //}
    }

    }
}