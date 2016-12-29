using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

//以下引用，如果采用实体类，请全部复制到新页面
using PetaPoco;
using Rmes.DA.Base;
using Rmes.DA.Entity;
using Rmes.DA.Dal;
using Rmes.DA.Factory;
using Rmes.Pub.Data;
using Rmes.Web.Base;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxEditors;
using System.Data;


/**
 * 功能概述：生产线和所属车间关系维护
 * 作    者：涂猛
 * 创建时间：2013-12-27
 * 修改时间：
 */
public partial class Rmes_epd3300 : Rmes.Web.Base.BasePage
{

    private dataConn dc = new dataConn();
    PubCs thePubCs = new PubCs();

    public string theCompanyCode;
    private string theUserId;
    public string theProgramCode;
    public DateTime OracleServerTime;  //获取服务器时间，示例

    public Database db = DB.GetInstance();

    protected void Page_Load(object sender, EventArgs e)
    {
        userManager theUserManager = (userManager)Session["theUserManager"];
        theCompanyCode = theUserManager.getCompanyCode();
        theUserId = theUserManager.getUserId();
        theProgramCode = "epd3300";
        setCondition();
    }

    private void setCondition()
    {
        //绑定表数据
        string sql = "select * from REL_WORKSHOP_PLINE where pline_code in (select pline_code from vw_user_role_program where user_id='" + theUserId + "' and program_code='" + theProgramCode + "' and company_code='" + theCompanyCode + "') order by workshop_code";
        DataTable dt = dc.GetTable(sql);
        ASPxGridView1.DataSource = dt;

        GridViewDataComboBoxColumn col = ASPxGridView1.Columns["PLINE_CODE"] as GridViewDataComboBoxColumn;
        col.PropertiesComboBox.DataSource = ProductLineFactory.GetByUserID(theUserId, theProgramCode);
        col.PropertiesComboBox.ValueField = "PLINE_CODE";
        col.PropertiesComboBox.TextField = "PLINE_NAME";
        ASPxComboBox plinecode = new ASPxComboBox();

        GridViewDataComboBoxColumn col1 = ASPxGridView1.Columns["WORKSHOP_CODE"] as GridViewDataComboBoxColumn;
        col1.PropertiesComboBox.DataSource = WorkShopFactory.GetAll();
        col1.PropertiesComboBox.ValueField = "WORKSHOP_CODE";
        col1.PropertiesComboBox.TextField = "WORKSHOP_NAME";

        ASPxGridView1.DataBind();

    }

    protected void PlineCode_DataBinding(object sender, EventArgs e)
    {
        ASPxComboBox pline = (ASPxComboBox)sender;
        pline.DataSource = ProductLineFactory.GetByUserID(theUserId, theProgramCode);
        pline.ValueField = "PLINE_CODE";
        pline.TextField = "PLINE_NAME";
    }

    protected void WorkshopCode_DataBinding(object sender, EventArgs e)
    {
        ASPxComboBox workshop = (ASPxComboBox)sender;
        workshop.DataSource = WorkShopFactory.GetAll();
        workshop.ValueField = "WORKSHOP_CODE";
        workshop.TextField = "WORKSHOP_NAME";
    }

    protected void ASPxGridView1_HtmlEditFormCreated(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewEditFormEventArgs e)
    {

    }

    protected void ASPxGridView1_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        Workshop_PlineEntity wpe = new Workshop_PlineEntity();
        wpe.COMPANY_CODE = theCompanyCode;
        wpe.WORKSHOP_CODE = e.NewValues["WORKSHOP_CODE"] as string;
        wpe.PLINE_CODE = e.NewValues["PLINE_CODE"] as string;
        wpe.TEMP01 = e.NewValues["TEMP01"] as string;
        string sql = "update REL_WORKSHOP_PLINE set WORKSHOP_CODE='" + wpe.WORKSHOP_CODE + "', PLINE_CODE='" + wpe.PLINE_CODE + "',TEMP01='" + wpe.TEMP01
            + "' where WORKSHOP_CODE='" + (string)e.OldValues["WORKSHOP_CODE"] + "' and PLINE_CODE='" + (string)e.OldValues["PLINE_CODE"] + "'";
        dc.ExeSql(sql);

        e.Cancel = true;
        ASPxGridView1.CancelEdit();
        setCondition();
    }

    protected void ASPxGridView1_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        Workshop_PlineEntity wpe = new Workshop_PlineEntity();
        wpe.COMPANY_CODE = theCompanyCode;
        wpe.WORKSHOP_CODE = e.NewValues["WORKSHOP_CODE"] as string;
        wpe.PLINE_CODE = e.NewValues["PLINE_CODE"] as string;
        wpe.TEMP01 = e.NewValues["TEMP01"] as string;
        string sql = "insert into REL_WORKSHOP_PLINE(WORKSHOP_CODE,PLINE_CODE,TEMP01) values('" + wpe.WORKSHOP_CODE + "','" + wpe.PLINE_CODE + "','" + wpe.TEMP01 + "')";
        db.Insert(wpe);

        e.Cancel = true;
        ASPxGridView1.CancelEdit();
        setCondition();
    }

    protected void ASPxGridView1_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        string pline = e.Values["PLINE_CODE"] as string;
        string workshop = e.Values["WORKSHOP_CODE"] as string;
        string sql = "delete from REL_WORKSHOP_PLINE where pline_code='" + pline + "' and WORKSHOP_CODE='" + workshop + "'";
        dc.ExeSql(sql);
        //Workshop_PlineEntity wpe = new Workshop_PlineEntity();
        //try
        //{
        //    wpe = db.First<Workshop_PlineEntity>("where PLINE_CODE=@0 and WORKSHOP_CODE=@1", pline,workshop);
        //}
        //catch (Exception ex)
        //{
        //    e.Cancel = false;
        //    return;
        //}
        //if (wpe == null)
        //    return;
        //db.Delete(wpe);

        setCondition();
        e.Cancel = true;
    }

    protected void ASPxGridView1_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
    {
        if (ASPxGridView1.IsNewRowEditing)
        {
            string pline = e.NewValues["PLINE_CODE"] as string;
            string workshop = e.NewValues["WORKSHOP_CODE"] as string;
            List<Workshop_PlineEntity> temp = db.Fetch<Workshop_PlineEntity>("where pline_code=@0 and workshop_code=@1", pline, workshop);
            if (temp.Count > 0)
            {
                e.RowError = "对应关系已存在";
            }
            List<Workshop_PlineEntity> temp1 = db.Fetch<Workshop_PlineEntity>("where pline_code=@0", pline);
            if (temp1.Count > 0)
            {
                e.RowError = "一条生产线只能有一个车间";
            }
        }
    }

    protected void ASPxGridView1_RowInserting1(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        Workshop_PlineEntity wpe = new Workshop_PlineEntity();
        wpe.COMPANY_CODE = theCompanyCode;
        wpe.WORKSHOP_CODE = e.NewValues["WORKSHOP_CODE"] as string;
        wpe.PLINE_CODE = e.NewValues["PLINE_CODE"] as string;
        wpe.TEMP01 = e.NewValues["TEMP01"] as string;
        string sql = "insert into REL_WORKSHOP_PLINE(WORKSHOP_CODE,PLINE_CODE,TEMP01) values('" + wpe.WORKSHOP_CODE + "','" + wpe.PLINE_CODE + "','" + wpe.TEMP01 + "')";
        db.Insert(wpe);

        e.Cancel = true;
        ASPxGridView1.CancelEdit();
        setCondition();
    }
}
