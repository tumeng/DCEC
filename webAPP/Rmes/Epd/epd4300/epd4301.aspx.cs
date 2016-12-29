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

namespace Rmes.WebApp.Rmes.Epd.epd4300
{
    public partial class epd4301 : BasePage
    {
        public Database db = DB.GetInstance();

        public dataConn conn = new dataConn();
        protected void Page_Load(object sender, EventArgs e)
        {
            BindData();
        }

        public void BindData()
        {
            List<LineSideStoreEntity> allEntity = LinesideStoreFactory.GetAll();
            ASPxGridView1.DataSource = allEntity;

            GridViewDataComboBoxColumn comPline = ASPxGridView1.Columns["PLINE_CODE"] as GridViewDataComboBoxColumn;
            List<ProductLineEntity> allPline = ProductLineFactory.GetAll();
            comPline.PropertiesComboBox.DataSource = allPline;
            comPline.PropertiesComboBox.TextField = "PLINE_NAME";
            comPline.PropertiesComboBox.ValueField = "PLINE_CODE";

            GridViewDataComboBoxColumn comWorkUnit = ASPxGridView1.Columns["WORKUNIT_CODE"] as GridViewDataComboBoxColumn;
            List<StationEntity> allStation = StationFactory.GetAll();
            comWorkUnit.PropertiesComboBox.DataSource = allStation;
            comWorkUnit.PropertiesComboBox.TextField = "STATION_NAME";
            comWorkUnit.PropertiesComboBox.ValueField = "WORKUNIT_CODE";


            GridViewDataComboBoxColumn comWorkShop = ASPxGridView1.Columns["WORKSHOP_CODE"] as GridViewDataComboBoxColumn;
            DataTable dt = new DataTable();
            dt.Columns.Add("text");
            dt.Columns.Add("value");
            dt.Rows.Add("园区", 8101);
            dt.Rows.Add("基地", 8102);


            comWorkShop.PropertiesComboBox.DataSource = dt;
            comWorkShop.PropertiesComboBox.TextField = "text";
            comWorkShop.PropertiesComboBox.ValueField = "value";

            ASPxGridView1.DataBind();

        }

        public void ASPxGridView1_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            string id = e.Values["RMES_ID"].ToString();
            LineSideStoreEntity entity = db.First<LineSideStoreEntity>("where rmes_id=@0", id);
            db.Delete(entity);
            e.Cancel = true;
            ASPxGridView1.CancelEdit();
            BindData();
        }

        public void ASPxGridView1_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            if (ASPxGridView1.IsEditing)
            {
                string id = e.OldValues["RMES_ID"].ToString();
                LineSideStoreEntity entity = db.First<LineSideStoreEntity>("where rmes_id=@0", id);
                entity.PLINE_CODE = e.NewValues["PLINE_CODE"].ToString();
                entity.WORKUNIT_CODE = e.NewValues["WORKUNIT_CODE"].ToString();
                entity.WORKSHOP_CODE = e.NewValues["WORKSHOP_CODE"].ToString();
                entity.STORE_CODE = e.NewValues["STORE_CODE"].ToString();
                entity.STORE_NAME = e.NewValues["STORE_NAME"].ToString();
                db.Update(entity);
                e.Cancel = true;
                ASPxGridView1.CancelEdit();
                BindData();
            }
        }

        public void ASPxGridView1_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            if (ASPxGridView1.IsNewRowEditing)
            {

                LineSideStoreEntity entity = new LineSideStoreEntity();
                entity.WORKUNIT_CODE = e.NewValues["WORKUNIT_CODE"].ToString();
                entity.WORKSHOP_CODE = e.NewValues["WORKUNIT_CODE"].ToString();
                entity.STORE_CODE = e.NewValues["STORE_CODE"].ToString();
                entity.STORE_NAME = e.NewValues["STORE_NAME"].ToString();
                entity.COMPANY_CODE = LoginInfo.CompanyInfo.COMPANY_CODE;
                db.Insert(entity);
                e.Cancel = true;
                ASPxGridView1.CancelEdit();
                BindData();
            }
        }

        protected void ASPxGridView1_HtmlEditFormCreated(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewEditFormEventArgs e)
        {


            ASPxComboBox uPcode = ASPxGridView1.FindEditFormTemplateControl("ASPxComPline") as ASPxComboBox;
            //ASPxComboBox uScode = ASPxGridView1.FindEditFormTemplateControl("dropStaionArea") as ASPxComboBox;

            List<ProductLineEntity> all = ProductLineFactory.GetAll();
            uPcode.DataSource = all;
            uPcode.TextField = "PLINE_NAME";
            uPcode.ValueField = "PLINE_CODE";




            if (!ASPxGridView1.IsNewRowEditing && ASPxGridView1.IsEditing)
            {

            }
            else
            {
                //uTcode.SelectedIndex = 0;
                //uPcode.SelectedIndex = 0;
                //uScode.SelectedIndex = 0;
            }
        }


        //public void ASPxComPline_DataBinding(object sender, EventArgs e)
        //{
        //    ASPxComboBox comPline = sender as ASPxComboBox;
        //    List<ProductLineEntity> all = ProductLineFactory.GetAll();
        //    comPline.DataSource = all;
        //    comPline.TextField = "PLINE_NAME";
        //    comPline.ValueField = "PLINE_CODE";
        //}

        protected void WorkUnit_Callback(object sender, CallbackEventArgsBase e)
        {

            string pline = e.Parameter;
            ASPxComboBox location = (ASPxComboBox)sender;
            List<StationEntity> all = StationFactory.GetByProductLine(pline);
            location.DataSource = all;
            location.TextField = "LOCATION_NAME";
            location.ValueField = "WORKUNIT_CODE";
            location.DataBind();

        }
    }
}
