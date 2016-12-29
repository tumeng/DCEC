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
using System.Collections.Generic;

/**
 * 功能概述：线边库存查询
 * 作    者：caoly
 * 创建时间：2014-01-11
 * 修改时间：2014-01-11
 */

public partial class Rmes_inv3100 : BasePage
{
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
        userManager theUserManager = (userManager)Session["theUserManager"];
        theCompanyCode = theUserManager.getCompanyCode();
        string theUserId = theUserManager.getUserId();

        List<WorkShopEntity> workShop = WorkShopFactory.GetUserWorkShops(theUserId);
        ASPxGridView1.DataSource = LineSideStockFactory.GetByWorkShopID(workShop[0].RMES_ID);

        GridViewDataComboBoxColumn col = ASPxGridView1.Columns["PLINE_CODE"] as GridViewDataComboBoxColumn;
        col.PropertiesComboBox.DataSource = ProductLineFactory.GetAll();
        col.PropertiesComboBox.ValueField = "RMES_ID";
        col.PropertiesComboBox.TextField = "PLINE_NAME";

        GridViewDataComboBoxColumn col1 = ASPxGridView1.Columns["LOCATION_CODE"] as GridViewDataComboBoxColumn;
        col1.PropertiesComboBox.DataSource = db.Fetch<LocationEntity>("");
        col1.PropertiesComboBox.ValueField = "RMES_ID";
        col1.PropertiesComboBox.TextField = "LOCATION_NAME";

        ASPxGridView1.DataBind();

    }

}