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
    public partial class XtraReport2 : DevExpress.XtraReports.UI.XtraReport
    {
        public XtraReport2(List<IMESStore2LineEntity> ds,List<object> planSO)
        {
            InitializeComponent();
            SetDataBind(ds,planSO);
        }
        private void SetDataBind(List<IMESStore2LineEntity> ds, List<object> planSO)//绑定数据源
        {

            if (ds.Count <= 0) return;
            List<string> orders = ds.OrderBy(m=>m.AUFNR).Select(m => m.AUFNR).Distinct().ToList<string>();
            List<string> projn = ds.OrderBy(m => m.PROJN).Select(m => m.PROJN).Distinct().ToList<string>();

            string txt1 = "合同号：";
            foreach (string p in projn)
            {
                txt1 += p + ";  ";
            }
            xrRichText1.Text = txt1;
            
            //List<string> planSO = plans.Select(m=>m.PROJECT_CODE).Distinct().ToList<string>();
            //List<LineSideStoreEntity> stores = LinesideStoreFactory.GetAll();
            string txt2 = "成品编码：";
            foreach (string p in planSO)
            {
                txt2+=p +";  ";
            }
            xrRichText2.Text = txt2;

            string txt3 = "订单号：";
            foreach (string o in orders)
            {
                txt3 += o + ";  ";
            }
            xrRichText3.Text = txt3;

            //xrLabel3.Text = DateTime.Today.ToShortDateString();

            

            
            //for (int i = 0; i < ds.Count; i++)
            //{
            //    string ws = ds[i].WERKS;
            //    //string pr = ds[i].PRIND;
            //    string tl = ds[i].TLGORT;
            //    List<string> storeNames = new List<string>();
            //    storeNames = (from s in stores where s.STORE_CODE.Equals(tl) select s.STORE_NAME).ToList<string>();
            //    if (storeNames.Count > 0)
            //    {
            //        ds[i].TLGORT = storeNames[0];
            //    }
            //    if (ws == "8101")
            //        ds[i].WERKS = "园区";
            //    else
            //    {
            //        ds[i].WERKS = "基地";
            //    }
                
            //    //switch (pr)
            //    //{
            //    //    case "0":
            //    //        ds[i].PRIND = "初始状态";
            //    //        break;
            //    //    case "1":
            //    //        ds[i].PRIND = "传送中";
            //    //        break;
            //    //    case "2":
            //    //        ds[i].PRIND = "已传送";
            //    //        break;
            //    //}  
            //}

            var temp = from s in ds group s by new { s.TLGORT, s.SUBMAT, s.MATKT, s.SLGORT, s.PROJN } into r select new { r.Key.MATKT, r.Key.SLGORT, r.Key.TLGORT, r.Key.SUBMAT, MENGE = r.Sum(m => Convert.ToDecimal(m.MENGE)),  r.Key.PROJN };

            DataSource = temp;


            this.xrTableCell11.DataBindings.Add("Text", DataSource, "SUBMAT");
            this.xrTableCell2.DataBindings.Add("Text", DataSource, "MENGE");
            this.xrTableCell13.DataBindings.Add("Text", DataSource, "MATKT");
            this.xrTableCell14.DataBindings.Add("Text", DataSource, "SLGORT");
            this.xrTableCell15.DataBindings.Add("Text", DataSource, "TLGORT");
            //this.xrTableCell16.DataBindings.Add("Text", DataSource, "PROJN");
            //this.xrTableCell18.DataBindings.Add("Text", DataSource, "VORNR");
            //this.xrTableCell17.DataBindings.Add("Text", DataSource, "PRIND");


        }

    }
}
