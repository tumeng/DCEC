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
using System.Data;
using DevExpress.Web.ASPxEditors;
using DevExpress.Web.ASPxClasses;
using DevExpress.Web.ASPxGridView;


/**
 * 功能概述：定时拉料基础数据定义
 * 作    者：TuMeng
 * 
 * 
 */


namespace Rmes.WebApp.Rmes.Epd.epd4100
{
    public partial class epd4100 : BasePage
    {
        private static string theCompanyCode, theUserId;
        protected void Page_Load(object sender, EventArgs e)
        {
            userManager theUserManager = (userManager)Session["theUserManager"];
            theCompanyCode = theUserManager.getCompanyCode();
            theUserId = theUserManager.getUserId();
            BindData();
        }

        public void BindData()
        {
            List<ItemLineSideEntity> allEntity = ItemLineSideFactory.GetAll();
            ASPxGridView1.DataSource = allEntity;

            GridViewDataComboBoxColumn comStock = ASPxGridView1.Columns["LINESIDE_STORE_CODE"] as GridViewDataComboBoxColumn;
            List<LineSideStoreEntity> all = LinesideStoreFactory.GetLineSideStore();
            comStock.PropertiesComboBox.DataSource = all;
            comStock.PropertiesComboBox.TextField = "STORE_NAME";
            comStock.PropertiesComboBox.ValueField = "STORE_CODE";

            GridViewDataComboBoxColumn comResourceStore = ASPxGridView1.Columns["RESOURCE_STORE"] as GridViewDataComboBoxColumn;
            List<LineSideStoreEntity> _all = LinesideStoreFactory.GetMaterialStore();
            comResourceStore.PropertiesComboBox.DataSource = _all;
            comResourceStore.PropertiesComboBox.TextField = "STORE_NAME";
            comResourceStore.PropertiesComboBox.ValueField = "STORE_CODE";

            GridViewDataComboBoxColumn comType = ASPxGridView1.Columns["BATCH_TYPE"] as GridViewDataComboBoxColumn;

            DataTable dt = new DataTable();
            dt.Columns.Add("text");
            dt.Columns.Add("value");
            dt.Rows.Add("天",0);
            dt.Rows.Add("周",1);
            dt.Rows.Add("旬", 2);
            dt.Rows.Add("月", 3);
            dt.Rows.Add("季度", 4);

            comType.PropertiesComboBox.DataSource = dt;
            comType.PropertiesComboBox.TextField = "text";
            comType.PropertiesComboBox.ValueField = "value";

            ASPxGridView1.DataBind();

        }

        public void ASPxGridView1_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            string id = e.Values["RMES_ID"].ToString();
            ItemLineSideEntity entity = ItemLineSideFactory.GetByID(id);
            ItemLineSideFactory.Delete(entity);
            e.Cancel = true;
            ASPxGridView1.CancelEdit();
            BindData();
        }

        public void ASPxGridView1_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            if (ASPxGridView1.IsEditing)
            {
                string id = e.OldValues["RMES_ID"].ToString();
                ItemLineSideEntity entity = ItemLineSideFactory.GetByID(id);
                entity.COMPANY_CODE = theCompanyCode;
                entity.ITEM_CODE = e.NewValues["ITEM_CODE"].ToString();
                entity.MIN_STOCK_QTY=Convert.ToInt32(e.NewValues["MIN_STOCK_QTY"]);
                entity.STAND_QTY = Convert.ToInt32(e.NewValues["STAND_QTY"]);
                ASPxComboBox tempComboBox1 = ASPxGridView1.FindEditFormTemplateControl("comLineSideStock") as ASPxComboBox;
                ASPxComboBox tempComboBox2 = ASPxGridView1.FindEditFormTemplateControl("comBatchType") as ASPxComboBox;
                ASPxComboBox tempComboBox3 = ASPxGridView1.FindEditFormTemplateControl("comResourceStore") as ASPxComboBox;
                entity.RESOURCE_STORE = tempComboBox3.SelectedItem.Value.ToString();
                entity.LINESIDE_STORE_CODE = tempComboBox1.SelectedItem.Value.ToString();
                entity.BATCH_TYPE = tempComboBox2.SelectedItem.Value.ToString();
                entity.ITEM_NAME = e.NewValues["ITEM_NAME"].ToString();
                entity.UNIT_CODE = e.NewValues["UNIT_CODE"].ToString(); 
                ItemLineSideFactory.Update(entity);
                e.Cancel = true;
                ASPxGridView1.CancelEdit();
                BindData();
            }
        }

        public void ASPxGridView1_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            if (ASPxGridView1.IsNewRowEditing)
            {
                
                ItemLineSideEntity entity = new ItemLineSideEntity();
                entity.COMPANY_CODE = theCompanyCode;
                entity.ITEM_CODE = e.NewValues["ITEM_CODE"].ToString();
                entity.MIN_STOCK_QTY = Convert.ToInt32(e.NewValues["MIN_STOCK_QTY"]);
                entity.STAND_QTY = Convert.ToInt32(e.NewValues["STAND_QTY"]);
                ASPxComboBox tempComboBox1 = ASPxGridView1.FindEditFormTemplateControl("comLineSideStock") as ASPxComboBox;
                ASPxComboBox tempComboBox2 = ASPxGridView1.FindEditFormTemplateControl("comBatchType") as ASPxComboBox;
                ASPxComboBox tempComboBox3 = ASPxGridView1.FindEditFormTemplateControl("comResourceStore") as ASPxComboBox;
                entity.RESOURCE_STORE = tempComboBox3.SelectedItem.Value.ToString();
                entity.LINESIDE_STORE_CODE = tempComboBox1.SelectedItem.Value.ToString();
                entity.BATCH_TYPE = tempComboBox2.SelectedItem.Value.ToString();
                entity.ITEM_NAME = e.NewValues["ITEM_NAME"].ToString();
                entity.UNIT_CODE = e.NewValues["UNIT_CODE"].ToString(); 
                ItemLineSideFactory.Insert(entity);
                e.Cancel = true;
                ASPxGridView1.CancelEdit();
                BindData();
            }
        }

        public void comLineSideStock_DataBinding(object sender, EventArgs e)
        {
            ASPxComboBox comStock = sender as ASPxComboBox;
            List<LineSideStoreEntity> all = LinesideStoreFactory.GetLineSideStore();
            comStock.DataSource = all;
            comStock.TextField = "STORE_NAME";
            comStock.ValueField = "STORE_CODE";
        }

        public void comResourceStore_DataBinding(object sender, EventArgs e)
        {
            ASPxComboBox comStock = sender as ASPxComboBox;
            List<LineSideStoreEntity> all = LinesideStoreFactory.GetMaterialStore();
            comStock.DataSource = all;
            comStock.TextField = "STORE_NAME";
            comStock.ValueField = "STORE_CODE";
        }


        protected void ASPxCbSubmit_Callback(object source, DevExpress.Web.ASPxCallback.CallbackEventArgs e)
        {

            string itemCode = e.Parameter;
            ItemEntity item = ItemFactory.GetByItem("01",itemCode);
            if (item == null)
            {
                e.Result = "error";
            }
            else
            {
                e.Result = item.ITEM_NAME+","+item.UNIT_CODE;
            }
        }
    }
}