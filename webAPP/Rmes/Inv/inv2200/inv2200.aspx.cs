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
using System.Linq;
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
 * 功能概述：线边收料单
 * 作    者：涂猛
 * 创建时间：2014-01-11
 */

public partial class Rmes_inv2200 : BasePage
{
    private dataConn dc = new dataConn();

    public string theCompanyCode;

    public Database db = DB.GetInstance();
    
    protected void Page_Load(object sender, EventArgs e)
    {
        userManager theUserManager = (userManager)Session["theUserManager"];
        theCompanyCode = theUserManager.getCompanyCode();

        if (!IsPostBack)
        {

            



            List<ProductLineEntity> plineEntities = ProductLineFactory.GetAll();
            List<LineSideStoreEntity> stores = LinesideStoreFactory.GetAll();

            foreach (var s in stores)
            {
                storeCode.Items.Add(s.STORE_NAME,s.STORE_CODE);
            }

            foreach (var p in plineEntities)
            {
                plineCode.Items.Add(p.PLINE_NAME, p.PLINE_CODE);
            }

            
        }

        setCondition();
    }

    public void storeCode_Callback(object sender, CallbackEventArgsBase e)
    {
        ASPxComboBox cBox = sender as ASPxComboBox;
        cBox.Items.Clear();
        cBox.Items.Add("全部","All");
        
        string pline = plineCode.SelectedItem.Value.ToString();
        if (pline.Equals("All"))
        {
            List<LineSideStoreEntity> stores = LinesideStoreFactory.GetAll();

            foreach (var s in stores)
            {
                cBox.Items.Add(s.STORE_NAME, s.STORE_CODE);
            }
        }
        else
        {
            List<LineSideStoreEntity> stores = LinesideStoreFactory.GetByPline(pline);

            foreach (var s in stores)
            {
                cBox.Items.Add(s.STORE_NAME, s.STORE_CODE);
            }
        }

        cBox.SelectedIndex = 0;
    }

    private void setCondition()
    {
        //绑定表数据
        userManager theUserManager = (userManager)Session["theUserManager"];
        theCompanyCode = theUserManager.getCompanyCode();
        string theUserId = theUserManager.getUserId();

        List<LineSideStockEntity> dataSource = LineSideStockFactory.GetAll();

        if (!plineCode.SelectedItem.Value.ToString().Equals("All"))
        {
            dataSource = (from s in dataSource where s.PLINE_CODE==(plineCode.SelectedItem.Value.ToString()) select s).ToList<LineSideStockEntity>();
        }
        if (storeCode.SelectedItem!=null&&!storeCode.SelectedItem.Value.ToString().Equals("All"))
        {
            dataSource = (from s in dataSource where s.STORE_CODE==(storeCode.SelectedItem.Value.ToString()) select s).ToList<LineSideStockEntity>();
        }


        ASPxGridView1.DataSource = dataSource;

        GridViewDataComboBoxColumn comFactory = ASPxGridView1.Columns["FACTORY_CODE"] as GridViewDataComboBoxColumn;
        DataTable dt_factory = new DataTable();
        dt_factory.Columns.Add("text");
        dt_factory.Columns.Add("value");
        dt_factory.Rows.Add("园区", "8101");
        dt_factory.Rows.Add("基地","8102");
        comFactory.PropertiesComboBox.TextField = "text";
        comFactory.PropertiesComboBox.ValueField = "value";

        GridViewDataComboBoxColumn comStore = ASPxGridView1.Columns["STORE_CODE"] as GridViewDataComboBoxColumn;
        List<LineSideStoreEntity> stores = LinesideStoreFactory.GetAll();
        comStore.PropertiesComboBox.DataSource = stores;
        comStore.PropertiesComboBox.TextField = "STORE_NAME";
        comStore.PropertiesComboBox.ValueField = "STORE_CODE";

        GridViewDataComboBoxColumn comPline = ASPxGridView1.Columns["PLINE_CODE"] as GridViewDataComboBoxColumn;
        List<ProductLineEntity> plines = ProductLineFactory.GetAll();
        comPline.PropertiesComboBox.DataSource = plines;
        comPline.PropertiesComboBox.TextField = "PLINE_NAME";
        comPline.PropertiesComboBox.ValueField = "RMES_ID";


        GridViewDataComboBoxColumn comLocation = ASPxGridView1.Columns["LOCATION_CODE"] as GridViewDataComboBoxColumn;

        ASPxGridView1.DataBind();
    }


    public void ASPxButton1_Click(object sender, EventArgs e)
    {
        setCondition();
    }

    protected void btnXlsExport_Click(object sender, EventArgs e)
    {
        ASPxGridViewExporter1.WriteXlsToResponse("线边库存");
    }

}