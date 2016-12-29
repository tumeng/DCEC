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
    public partial class XtraReport3 : DevExpress.XtraReports.UI.XtraReport
    {
       
        public XtraReport3(List<PlanEntity> ds)
        {
            InitializeComponent();
            //SetDataBind(ds);
        }
        //private void SetDataBind(List<PlanEntity> ds)//绑定数据源
        //{

        //    DataSource = ds;
            
            
        //    for (int i = 0; i < ds.Count; i++)
        //    {
        //        string ws = ds[i].WORKSHOP_CODE;
        //        string pt = ds[i].PLAN_TYPE_CODE;
        //        string itf = ds[i].ITEM_FLAG;
        //        string ruf = ds[i].RUN_FLAG;
        //        string pl = ds[i].PLINE_CODE;
                
                
        //        if (ws == "8101")
        //            ds[i].WORKSHOP_CODE = "园区";
        //        else
        //        {
        //            ds[i].WORKSHOP_CODE = "基地";
        //        }
        //        switch (pt)
        //        {
        //            case "ZP01":
        //                ds[i].PLAN_TYPE_CODE = "标准";
        //                break;
        //            case "ZP02":
        //                ds[i].PLAN_TYPE_CODE = "返工";
        //                break;
        //            case "ZP03":
        //                ds[i].PLAN_TYPE_CODE = "拆解";
        //                break;

        //        }
        //        switch (itf)
        //        {
        //            case "N":
        //                ds[i].ITEM_FLAG = "初始状态";
        //                break;
        //            case "B":
        //                ds[i].ITEM_FLAG = "已生成领料单";
        //                break;
        //            case "Y":
        //                ds[i].ITEM_FLAG = "线边已收料";
        //                break;
        //        }
        //        switch (ruf)
        //        {
        //            case "Y":
        //                ds[i].RUN_FLAG = "执行状态";
        //                break;
        //            case "N":
        //                ds[i].RUN_FLAG = "初始状态";
        //                break;
        //            case "P":
        //                ds[i].RUN_FLAG = "暂停";
        //                break;
        //            case "F":
        //                ds[i].RUN_FLAG = "完成";
        //                break;
        //            case "S":
        //                ds[i].RUN_FLAG = "停止";
        //                break;
        //        }

        //        switch (pl)
        //        {
        //            case "M007":
        //                ds[i].PLINE_CODE = "随机附件";
        //                break;
        //            case "M075":
        //                ds[i].PLINE_CODE = "i-AY6-12开关柜虚拟生产线";
        //                break;
        //            case "M006":
        //                ds[i].PLINE_CODE = "备品备件";
        //                break;
        //            case "M100":
        //                ds[i].PLINE_CODE = "钣金自动/人工";
        //                break;
        //            case "M076":
        //                ds[i].PLINE_CODE = "i-AY2-18开关柜虚拟生产线";
        //                break;
        //            case "M072":
        //                ds[i].PLINE_CODE = "开关柜虚拟生产线 - KYN18C-12'\'KYN18C-7.2（F-C）开关柜";
        //                break;
        //            case "M050":
        //                ds[i].PLINE_CODE = "40.5kV断路器生产线";
        //                break;
        //            case "M073":
        //                ds[i].PLINE_CODE = "12-24kV开关柜虚拟线（KYN28A'\'i-AY1A-12'\'KYN79）";
        //                break;
        //            case "M081":
        //                ds[i].PLINE_CODE = "断路器虚拟生产线ZN65A装配";
        //                break;
        //            case "M082":
        //                ds[i].PLINE_CODE = "断路器虚拟生产线-接触器装配";
        //                break;
        //            case "M083":
        //                ds[i].PLINE_CODE = "断路器虚拟生产线--辅助车装配（12-40.5kV）（含避雷器车、变压器车、计量车、验电车、弯板车、互感器车等）（不上线）";
        //                break;
        //            case "M084":
        //                ds[i].PLINE_CODE = "KYN18C-7.2(F-C）手车装配";
        //                break;
        //            case "M085":
        //                ds[i].PLINE_CODE = "12~24kV断路器虚拟（ZN63A'\'EVH1）生产线";
        //                break;
        //            case "M086":
        //                ds[i].PLINE_CODE = "辅助车装配";
        //                break;
        //            case "M090":
        //                ds[i].PLINE_CODE = "极柱灌封";
        //                break;
        //            case "M110":
        //                ds[i].PLINE_CODE = "母线制造单元";
        //                break;
        //            case "M120":
        //                ds[i].PLINE_CODE = "二次线束制造单元";
        //                break;
        //            case "M087":
        //                ds[i].PLINE_CODE = "EVH6-31.5断路器虚拟生产线";
        //                break;
        //            case "M088":
        //                ds[i].PLINE_CODE = "EVH9-12断路器虚拟生产线";
        //                break;
        //            case "M089":
        //                ds[i].PLINE_CODE = "EVH4-12/15/18断路器虚拟(3150-50及以下规格)生产线";
        //                break;
        //            case "M080":
        //                ds[i].PLINE_CODE = "EVH4-12/15/18断路器虚拟(3150-50以上规格)生产线";
        //                break;
        //            case "M074":
        //                ds[i].PLINE_CODE = "XZ1-31.5开关柜虚拟生产线";
        //                break;
        //            case "M051":
        //                ds[i].PLINE_CODE = "40.5kV断路器虚拟生产线";
        //                break;
        //            case "M061":
        //                ds[i].PLINE_CODE = "40.5kV开关柜虚拟生产线";
        //                break;
        //            case "M005":
        //                ds[i].PLINE_CODE = "外包装配";
        //                break;
        //            case "M040":
        //                ds[i].PLINE_CODE = "12~24kV开关柜生产线二";
        //                break;
        //            case "M060":
        //                ds[i].PLINE_CODE = "40.5kV开关柜生产线";
        //                break;

        //        }
                
                
        //    }


        //    this.xrTableCell12.DataBindings.Add("Text", DataSource, "ORDER_CODE");
        //    this.xrTableCell13.DataBindings.Add("Text", DataSource, "PLAN_CODE");


        //    this.xrTableCell14.DataBindings.Add("Text", DataSource, "PLAN_SO");
        //    this.xrTableCell15.DataBindings.Add("Text", DataSource, "PLINE_CODE");
        //    this.xrTableCell16.DataBindings.Add("Text", DataSource, "WORKSHOP_CODE");
        //    this.xrTableCell17.DataBindings.Add("Text", DataSource, "PLAN_TYPE_CODE");
        //    this.xrTableCell18.DataBindings.Add("Text", DataSource, "CREATE_TIME");
        //    this.xrTableCell19.DataBindings.Add("Text", DataSource, "BEGIN_TIME");
        //    this.xrTableCell20.DataBindings.Add("Text", DataSource, "END_TIME");
        //    this.xrTableCell21.DataBindings.Add("Text", DataSource, "ITEM_FLAG");
        //    this.xrTableCell22.DataBindings.Add("Text", DataSource, "RUN_FLAG");


        //}
    }
}
