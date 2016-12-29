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
 * 功能概述：分装站点定义
 * 作    者：caoly
 * 创建时间：2013-12-08
 * 修改时间：2013-12-09
 */

public partial class Rmes_epd1600 : BasePage
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

        ASPxGridView1.DataSource = StationSubFactory.GetAll();
        ASPxGridView1.DataBind();
    }

    protected void ASPxGridView1_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        //判断当前记录是否可以删除

        string strCode = e.Values["STATION_CODE_SUB"].ToString();
        StationSubEntity entSub = StationSubFactory.GetByKey(strCode);

        db.Delete(entSub);

        setCondition();
        e.Cancel = true;
    }


    protected void ASPxGridView1_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        //插入新纪录，应先做检查是否合法

        StationSubEntity entSub = new StationSubEntity();

        entSub.COMPANY_CODE = theCompanyCode;
        entSub.PLINE_CODE = e.NewValues["PLINE_CODE"].ToString();
        entSub.STATION_CODE = e.NewValues["STATION_CODE"].ToString();
        entSub.STATION_CODE_SUB = e.NewValues["STATION_CODE_SUB"].ToString();
        entSub.STATION_NAME_SUB = e.NewValues["STATION_NAME_SUB"].ToString();

        db.Insert(entSub);

        e.Cancel = true;
        ASPxGridView1.CancelEdit();
        setCondition();

    }


    protected void ASPxGridView1_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        //修改记录

        string strCode = e.OldValues["STATION_CODE_SUB"].ToString();
        StationSubEntity entSub = StationSubFactory.GetByKey(strCode);

        entSub.PLINE_CODE = e.NewValues["PLINE_CODE"].ToString();
        entSub.STATION_CODE = e.NewValues["STATION_CODE"].ToString();
        entSub.STATION_CODE_SUB = e.NewValues["STATION_CODE_SUB"].ToString();
        entSub.STATION_NAME_SUB = e.NewValues["STATION_NAME_SUB"].ToString();

        db.Update(entSub);

        e.Cancel = true;
        ASPxGridView1.CancelEdit();
        setCondition();

    }
    protected void ASPxGridView1_HtmlEditFormCreated(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewEditFormEventArgs e)
    {
        //创建Editform，对其中某些字段进行属性设置

        ASPxComboBox uPcode = ASPxGridView1.FindEditFormTemplateControl("dropPlineCode") as ASPxComboBox;
        ASPxComboBox uScode = ASPxGridView1.FindEditFormTemplateControl("dropStationCode") as ASPxComboBox;

        string Sql2 = "select pline_code,pline_code||' '||pline_name as showtext from CODE_PRODUCT_LINE";
        DataTable dt2 = dc.GetTable(Sql2);

        uPcode.DataSource = dt2;
        uPcode.TextField = dt2.Columns[1].ToString();
        uPcode.ValueField = dt2.Columns[0].ToString();


        string Sql3 = "SELECT STATION_CODE,STATION_CODE||' '||STATION_NAME AS SHOWTEXT FROM CODE_STATION";
        DataTable dt3 = dc.GetTable(Sql3);

        uScode.DataSource = dt3;
        uScode.TextField = dt3.Columns[1].ToString();
        uScode.ValueField = dt3.Columns[0].ToString();

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