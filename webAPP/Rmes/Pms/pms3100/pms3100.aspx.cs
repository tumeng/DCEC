﻿using System;
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
 * 功能概述：物料消耗记录查询
 * 作    者：李蒙蒙
 * 创建时间：2014-1-21
 * 修改时间：2014-2-04
 */

public partial class Rmes_pms3100 : BasePage
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

        string sql = "select * from VW_DATA_SN_BOM_UNION t where exists(select * from data_project a where t.PROJECT_CODE=a.project_code and a.workshop_id='" + workShop[0].RMES_ID + "')";

        DataTable dt = dc.GetTable(sql);

        ASPxGridView1.DataSource = dt;

        //ASPxGridView1.DataSource = db.Fetch<SNBomEntity>("where company_code=@0 order by pline_code", theCompanyCode);

        GridViewDataComboBoxColumn col = ASPxGridView1.Columns["PLINE_CODE"] as GridViewDataComboBoxColumn;
        col.PropertiesComboBox.DataSource = ProductLineFactory.GetByWorkShopID(workShop[0].RMES_ID);
        col.PropertiesComboBox.ValueField = "RMES_ID";
        col.PropertiesComboBox.TextField = "PLINE_NAME";

        //GridViewDataComboBoxColumn col1 = ASPxGridView1.Columns["LOCATION_CODE"] as GridViewDataComboBoxColumn;
        //col1.PropertiesComboBox.DataSource = LocationFactory.GetAll();
        //col1.PropertiesComboBox.ValueField = "RMES_ID";
        //col1.PropertiesComboBox.TextField = "LOCATION_NAME";

        //GridViewDataComboBoxColumn col2 = ASPxGridView1.Columns["STATION_CODE"] as GridViewDataComboBoxColumn;
        //col2.PropertiesComboBox.DataSource = StationFactory.GetAll();
        //col2.PropertiesComboBox.ValueField = "RMES_ID";
        //col2.PropertiesComboBox.TextField = "STATION_NAME";

        ASPxGridView1.DataBind();
    }
}