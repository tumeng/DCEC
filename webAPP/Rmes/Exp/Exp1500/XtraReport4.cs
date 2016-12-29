using System;
using System.Linq;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.IO;
using System.Data;
//以下引用，如果采用实体类，请全部复制到新页面
using PetaPoco;
using Rmes.DA.Base;
using Rmes.DA.Entity;
using Rmes.DA.Dal;
using Rmes.DA.Factory;
using Rmes.Pub.Data;
using Rmes.Web.Base;
using System.Collections.Generic;
namespace Rmes.WebApp.Rmes.Exp
{
    public partial class XtraReport4 : DevExpress.XtraReports.UI.XtraReport
    {
        public XtraReport4(List<ItemLineSideStore2LineEntity>ds)
        {
            InitializeComponent();
            SetDataBind(ds);
        }
        private void SetDataBind(List<ItemLineSideStore2LineEntity> ds)//绑定数据源
        {

            DataSource = ds;
            List<LineSideStoreEntity> stores = LinesideStoreFactory.GetAll();
           
            for (int i = 0; i < ds.Count; i++)
            {
                string ws = ds[i].WORKSHOP;
                string tl = ds[i].T_LINESIDESTORE;
                List<string> storeNames = new List<string>();
                storeNames = (from s in stores where s.STORE_CODE.Equals(tl) select s.STORE_CODE).ToList<string>();
                if (storeNames.Count > 0)
                {
                    ds[i].T_LINESIDESTORE = storeNames[0];
                }
                if (ws == "8101")
                    ds[i].WORKSHOP = "园区";
                else
                {
                    ds[i].WORKSHOP = "基地";
                }
               
            }


            this.xrTableCell7.DataBindings.Add("Text", DataSource, "ITEM_CODE");
            this.xrTableCell8.DataBindings.Add("Text", DataSource, "ITEM_QTY");
            this.xrTableCell9.DataBindings.Add("Text", DataSource, "S_LINESIDESTORE");
            this.xrTableCell10.DataBindings.Add("Text", DataSource, "T_LINESIDESTORE");
            this.xrTableCell11.DataBindings.Add("Text", DataSource, "ITEM_NAME");
            this.xrTableCell12.DataBindings.Add("Text", DataSource, "CREATE_TIME");
            

        }
    }
}
