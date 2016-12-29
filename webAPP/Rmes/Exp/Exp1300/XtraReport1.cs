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
    public partial class XtraReport1 : DevExpress.XtraReports.UI.XtraReport
    {
       public XtraReport1(List<IMESCompleteInstoreEntity> ds,string userID,string procode)
        {
            InitializeComponent();
            SetDataBind(ds,userID,procode);
        }
       private void SetDataBind(List<IMESCompleteInstoreEntity> ds,string userID,string procode)//绑定数据源
       {

           DataSource = ds;
           for (int i = 0; i < ds.Count; i++)
           {
               string ws = ds[i].WERKS;
               string gp = ds[i].GRTYP;
               string pr = ds[i].PRIND;
               if (ws == "8101")
                   ds[i].WERKS = "园区";
               else
               {
                   ds[i].WERKS = "基地";
               }
               switch (gp)
               {
                   case "1":
                       ds[i].GRTYP = "成品收货";
                       break;
                   case "2":
                       ds[i].GRTYP = "拆解收货";
                       break;
                   case "3":
                       ds[i].GRTYP = "停线待处理入库";
                       break;

               }
               switch (pr)
               {
                   case "0":
                       ds[i].PRIND = "初始状态";
                       break;
                   case "1":
                       ds[i].PRIND = "传送中";
                       break;
                   case "2":
                       ds[i].PRIND = "已传送";
                       break;
               }
           }
           UserEntity entity = UserFactory.GetByID(userID);
           xrLabel3.Text = entity.USER_NAME.ToString();
           xrLabel5.Text = System.DateTime.Now.ToString();
           xrLabel9.Text = procode;
           xrLabel11.Text = ds.Count.ToString();

           this.xrTableCell10.DataBindings.Add("Text", DataSource, "AUFNR");
           this.xrTableCell13.DataBindings.Add("Text", DataSource, "WERKS");


           this.xrTableCell12.DataBindings.Add("Text", DataSource, "MATNR");
           this.xrTableCell14.DataBindings.Add("Text", DataSource, "GAMNG");
           this.xrTableCell16.DataBindings.Add("Text", DataSource, "LGORT");
           this.xrTableCell15.DataBindings.Add("Text", DataSource, "GRTYP");
           this.xrTableCell17.DataBindings.Add("Text", DataSource, "HSDAT");
           this.xrTableCell11.DataBindings.Add("Text", DataSource, "BATCH");
           this.xrTableCell18.DataBindings.Add("Text", DataSource, "PRIND");


       }
    }
}
