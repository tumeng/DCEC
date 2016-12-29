using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxGridView;

//以下引用，如果采用实体类，请全部复制到新页面
using PetaPoco;
using Rmes.DA.Base;
using Rmes.DA.Entity;
using Rmes.DA.Dal;
using Rmes.DA.Factory;
using Rmes.Pub.Data;
using Rmes.Web.Base;


/**
 * 功能概述：工序完成情况
 * 作    者：caoly
 * 创建时间：2014-03-18
 * 修改时间：2014-03-18
 */

public partial class Rmes_pms3200 : BasePage
{
    private dataConn dc = new dataConn();

    private static string theCompanyCode, theUserID;

    public Database db = DB.GetInstance();

    protected void Page_Load(object sender, EventArgs e)
    {
        userManager theUserManager = (userManager)Session["theUserManager"];
        theCompanyCode = theUserManager.getCompanyCode();
        theUserID = theUserManager.getUserId();
        DataBindGridview();
    }
    private void DataBindGridview()
    {
        //绑定表数据

        List<WorkShopEntity> workShop = WorkShopFactory.GetUserWorkShops(theUserID);

        string sql = "select * from vw_data_sn_process_union t where exists(select * from data_project a where t.PROJECT_CODE=a.project_code and a.workshop_id='" + workShop[0].RMES_ID + "')";

        DataTable dt = dc.GetTable(sql);

        ASPxGridView1.DataSource = dt;

        //ASPxGridView1.DataSource = db.Fetch<SNProcessEntity>("where company_code=@0 order by pline_code", theCompanyCode);

        GridViewDataComboBoxColumn col = ASPxGridView1.Columns["PLINE_CODE"] as GridViewDataComboBoxColumn;
        col.PropertiesComboBox.DataSource = ProductLineFactory.GetByWorkShopID(workShop[0].RMES_ID);
        col.PropertiesComboBox.ValueField = "RMES_ID";
        col.PropertiesComboBox.TextField = "PLINE_NAME";

        //GridViewDataComboBoxColumn col1 = ASPxGridView1.Columns["LOCATION_CODE"] as GridViewDataComboBoxColumn;
        //col1.PropertiesComboBox.DataSource = db.Fetch<LocationEntity>("");
        //col1.PropertiesComboBox.ValueField = "RMES_ID";
        //col1.PropertiesComboBox.TextField = "LOCATION_NAME";


        ASPxGridView1.DataBind();
    }

    protected string GetCellText(GridViewDataItemTemplateContainer container)
    {
        string cell_text = container.Text;
        string strField = container.Column.FieldName;
        string strret = cell_text;

        switch (strField)
        {
            case "COMPLETE_FLAG":
                switch (cell_text)
                {
                    case "Y":
                        strret = "已完成";
                        break;
                    case "N":
                        strret = "未完成";
                        break;
                    default:
                        break;
                }
                break;
        }

        return strret;
    }
}