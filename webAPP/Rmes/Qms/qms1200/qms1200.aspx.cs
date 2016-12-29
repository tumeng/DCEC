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
 * 功能概述：产品系列检验项目
 * 作    者：涂猛
 * 创建时间：2013-12-03
 * 修改时间：2013-12-04
 */

public partial class Rmes_qms1200 : Rmes.Web.Base.BasePage
{
    private dataConn dc = new dataConn();
    PubCs thePubCs = new PubCs();

    public string theCompanyCode;

    public DateTime OracleServerTime;  //获取服务器时间，示例

    public Database db = DB.GetInstance();
    
    protected void Page_Load(object sender, EventArgs e)
    {
        userManager theUserManager = (userManager)Session["theUserManager"];
        theCompanyCode = theUserManager.getCompanyCode();

        //if (!IsPostBack)
        //{
        //    //获取服务器时间，示例
        //    OracleServerTime = DB.GetServerTime();
        //}

        setCondition();
    }

    private void setCondition()
    {
        //绑定表数据
        string sql = "select cp.pline_name,cq.* from code_quality_item cq,code_product_line cp where cp.pline_code=cq.pline_code and cq.company_code=" + theCompanyCode;
        DataTable dt = dc.GetTable(sql);
        ASPxGridView1.DataSource = dt;
        ASPxGridView1.DataBind();
    }

    protected void ASPxGridView1_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        //判断当前记录是否可以删除
        //string strDelCode = e.Values["PLAN_CODE"].ToString();

        string rmes_id = e.Values["RMES_ID"].ToString();
        QualityItemEntity detEntity = new QualityItemEntity { RMES_ID = rmes_id };

        db.Delete(detEntity);

        setCondition();
        e.Cancel = true;
    }


    protected void ASPxGridView1_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        //插入新纪录，应先做检查是否合法

        //设置初始值示例
        //PlanEntity entPlan = new PlanEntity()
        //{
        //    PLAN_CODE = "",
        //    PLAN_SO = ""
        //};
        //entPlan.PLAN_CODE = e.NewValues["PLAN_CODE"].ToString();
        //entPlan.PLAN_MODELCODE = e.NewValues["PLAN_MODELCODE"].ToString().Trim();
        //entPlan.PLAN_SO = e.NewValues["PLAN_SO"].ToString().Trim();
        //entPlan.PLAN_MODIFYDATE = DateTime.Parse(e.NewValues["PLAN_MODIFYDATE"].ToString());
        ASPxComboBox plineCode = ASPxGridView1.FindEditFormTemplateControl("plineCode") as ASPxComboBox;
        ASPxComboBox productSeries = ASPxGridView1.FindEditFormTemplateControl("productSeries") as ASPxComboBox;

        QualityItemEntity quaEntity = new QualityItemEntity();

        quaEntity.COMPANY_CODE = theCompanyCode;
        quaEntity.PLINE_CODE = plineCode.SelectedItem.Value.ToString().Trim();
        quaEntity.SERIES_CODE = productSeries.SelectedItem.Value.ToString().Trim();
        quaEntity.QUALITY_ITEM_CODE = e.NewValues["QUALITY_ITEM_CODE"].ToString().Trim();
        quaEntity.QUALITY_ITEM_NAME = e.NewValues["QUALITY_ITEM_NAME"].ToString().Trim();
        quaEntity.QUALITY_ITEM_DESC = e.NewValues["QUALITY_ITEM_DESC"].ToString().Trim();

        db.Insert(quaEntity);

        e.Cancel = true;
        ASPxGridView1.CancelEdit();
        setCondition();

    }


    protected void ASPxGridView1_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        //修改记录
        //string strCode = e.OldValues["PLAN_CODE"].ToString();
        //string newCode = e.NewValues["PLAN_CODE"].ToString();

        string rmes_id = e.OldValues["RMES_ID"].ToString();
        ASPxComboBox plineCode = ASPxGridView1.FindEditFormTemplateControl("plineCode") as ASPxComboBox;
        ASPxComboBox productSeries = ASPxGridView1.FindEditFormTemplateControl("productSeries") as ASPxComboBox;

        QualityItemEntity quaEntity = new QualityItemEntity();
        quaEntity.RMES_ID = rmes_id;
        quaEntity.COMPANY_CODE = theCompanyCode;
        quaEntity.PLINE_CODE = plineCode.SelectedItem.Value.ToString().Trim();
        quaEntity.SERIES_CODE = productSeries.SelectedItem.Value.ToString().Trim();
        quaEntity.QUALITY_ITEM_CODE = e.NewValues["QUALITY_ITEM_CODE"].ToString().Trim();
        quaEntity.QUALITY_ITEM_NAME = e.NewValues["QUALITY_ITEM_NAME"].ToString().Trim();
        quaEntity.QUALITY_ITEM_DESC = e.NewValues["QUALITY_ITEM_DESC"].ToString().Trim();

        db.Update(quaEntity);

        e.Cancel = true;
        ASPxGridView1.CancelEdit();
        setCondition();

    }
    protected void ASPxGridView1_HtmlEditFormCreated(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewEditFormEventArgs e)
    {
        userManager theUserManager = (userManager)Session["theUserManager"];
        theCompanyCode = theUserManager.getCompanyCode();
        ASPxComboBox plineCode = ASPxGridView1.FindEditFormTemplateControl("plineCode") as ASPxComboBox;
        ASPxComboBox productSeries = ASPxGridView1.FindEditFormTemplateControl("productSeries") as ASPxComboBox;
        string sql1 = "select pline_code,pline_name from code_product_line where company_code=" + theCompanyCode;
        DataTable dt = dc.GetTable(sql1);
        plineCode.DataSource = dt;
        plineCode.TextField = "pline_name";
        plineCode.ValueField = "pline_code";
        //plineCode.DataBind();

        string sql2 = "select distinct MODEL_CODE from data_xk_oldbom";
        DataTable dt1 = dc.GetTable(sql2);
        productSeries.DataSource = dt1;
        productSeries.TextField = "MODEL_CODE";
        productSeries.ValueField = "MODEL_CODE";
        //productSeries.DataBind();    前台使用了<%# Bind("") %>后台就不能使用DataBind()
        //创建Editform，对其中某些字段进行属性设置

        //if (!ASPxGridView1.IsNewRowEditing && ASPxGridView1.IsEditing)
        //{
        //    ///主键不可以修改
        //    (ASPxGridView1.FindEditFormTemplateControl("txtPlanCode") as ASPxTextBox).Enabled = false;
        //}
    }

    protected void ASPxGridView1_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
    {
        //数据提交前进行校验

        //if (thePubCs.IsNumeric(e.NewValues["PLAN_SEQUENCE"].ToString()) == false || thePubCs.IsNumeric(e.NewValues["PLAN_QTY"].ToString()) == false)
        //{
        //    e.RowError = "计划数量及执行序必须是正整数！";
        //}
    }
}