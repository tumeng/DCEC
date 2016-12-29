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
using System.Collections.Generic;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxEditors;

/**
 * 功能概述：取点站点和分装站点的对应关系维护
 * 作    者：TuMeng
 * 创建时间：2014-06-09
 */

namespace Rmes.WebApp.Rmes.Test
{
    public partial class sts : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            setCondition();
        }

        private void setCondition()
        {
            //绑定表数据
            List<StationSubEntity> allEntity = StationSubFactory.GetAll();

            List<string> plines = new List<string>();
            foreach (var p in allEntity)
            {
                if ((!plines.Contains(p.PLINE_CODE)) && (!string.IsNullOrWhiteSpace(p.PLINE_CODE)))
                    plines.Add(p.PLINE_CODE);            
            }

            ASPxGridView1.DataSource = allEntity;
            GridViewDataComboBoxColumn colPline = ASPxGridView1.Columns["PLINE_CODE"] as GridViewDataComboBoxColumn;
            List<ProductLineEntity> plineEntities = ProductLineFactory.GetByIDs(plines.ToArray());
            colPline.PropertiesComboBox.DataSource = plineEntities;
            colPline.PropertiesComboBox.ValueField = "RMES_ID";
            colPline.PropertiesComboBox.TextField = "PLINE_NAME";

            ASPxGridView1.DataBind();
        }

        protected void ASPxGridView1_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            if (ASPxGridView1.IsEditing)
            {
                string id = e.NewValues["RMES_ID"] as string;
                string plineCode = e.NewValues["PLINE_CODE"] as string;
                string station = e.NewValues["STATION_CODE"] as string;
                string stationSub = e.NewValues["STATION_CODE_SUB"] as string;
                StationSubEntity entity = StationSubFactory.GetByKey(id);
                entity.PLINE_CODE = plineCode;
                entity.STATION_CODE = station;
                entity.STATION_CODE_SUB = stationSub;
                StationSubFactory.Update(entity);

                e.Cancel = true;
                ASPxGridView1.CancelEdit();
                setCondition();
            }
        }

        protected void ASPxGridView1_HtmlEditFormCreated(object sender, ASPxGridViewEditFormEventArgs e)
        {
            
        }

        protected void ASPxGridView1_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            if (ASPxGridView1.IsNewRowEditing)
            {
                string plineCode = e.NewValues["PLINE_CODE"] as string;
                string station = e.NewValues["STATION_CODE"] as string;
                string stationSub = e.NewValues["STATION_CODE_SUB"] as string;
                StationSubEntity entity = new StationSubEntity
                {
                    COMPANY_CODE="01",
                    PLINE_CODE=plineCode,
                    STATION_CODE=station,
                    STATION_CODE_SUB=stationSub
                };
                StationSubFactory.Insert(entity);

                e.Cancel = true;
                ASPxGridView1.CancelEdit();
                setCondition();
            }
        }

        protected void ASPxGridView1_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            string id = e.Values["RMES_ID"] as string;
            StationSubEntity entity = StationSubFactory.GetByKey(id);
            StationSubFactory.Delete(entity);
        }

        protected void ASPxComboBoxStation_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            string plineCode = e.Parameter;
            List<StationEntity> stationEntities = StationFactory.GetByProductLine(plineCode);
            ASPxComboBox station = sender as ASPxComboBox;
            station.DataSource = stationEntities;
            station.ValueField = "RMES_ID";
            station.TextField = "STATION_NAME";
            station.DataBind();
        }

        protected void ASPxComboBoxStationSub_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            string plineCode = e.Parameter;
            List<StationEntity> stationEntities = StationFactory.GetByProductLine(plineCode);
            ASPxComboBox station = sender as ASPxComboBox;
            station.DataSource = stationEntities;
            station.ValueField = "RMES_ID";
            station.TextField = "STATION_NAME";
            station.DataBind();
        }

        protected void ComboBoxPline_DataBinding(object sender, EventArgs e)
        {
            ASPxComboBox colPline = sender as ASPxComboBox;
            List<ProductLineEntity> plineEntities = ProductLineFactory.GetAll();
            colPline.DataSource = plineEntities;
            colPline.ValueField = "RMES_ID";
            colPline.TextField = "PLINE_NAME";
        }
    }
}