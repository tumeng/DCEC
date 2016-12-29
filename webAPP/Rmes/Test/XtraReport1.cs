using System;
using System.Linq;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
//以下引用，如果采用实体类，请全部复制到新页面
using PetaPoco;
using Rmes.DA.Base;
using Rmes.DA.Entity;
using Rmes.DA.Dal;
using Rmes.DA.Factory;
using Rmes.Pub.Data;
using Rmes.Web.Base;
using System.Collections.Generic;

namespace Rmes.WebApp.Rmes.Test
{
    public partial class XtraReport1 : DevExpress.XtraReports.UI.XtraReport
    {
        public XtraReport1(List<PlanBomEntity> ds)
        {
            InitializeComponent();
            SetDataBind(ds);
        }

        private void SetDataBind(List<PlanBomEntity> ds)//绑定数据源
        {

            DataSource = ds;

            this.xrTableCell4.DataBindings.Add("Text", DataSource, "ITEM_CODE");

            this.xrTableCell5.DataBindings.Add("Text", DataSource, "ITEM_NAME");

            this.xrTableCell6.DataBindings.Add("Text", DataSource, "ITEM_QTY");


            var temp = (from s in ds select new { PLAN_CODE=s.PLAN_CODE}).Max(m=>m.PLAN_CODE);
            xrBarCode1.DataBindings.Add("Text", temp, "PLAN_CODE");
            xrBarCode1.BinaryData = new byte[] {1,2,3,4 };
            
            xrBarCode1.Text = temp as string;
            xrBarCode1.ShowText = true;
           
        }
        /*;
        using System.Collections.Generic;
        using System.Drawing.Printing;
        using System.Windows.Forms;
        using DevExpress.XtraPrinting.BarCode;
        using DevExpress.XtraReports.UI;
        // ...

        public XRBarCode CreateCode128BarCode(string BarCodeText) {
            // Create a bar code control.
            XRBarCode barCode = new XRBarCode();

            // Set the bar code's type to Code 128.
            barCode.Symbology = new Code128Generator();

            // Adjust the bar code's main properties.
            barCode.Text = BarCodeText;
            barCode.Width = 400;
            barCode.Height = 100;

            // Adjust the properties specific to the bar code type.
            ((Code128Generator)barCode.Symbology).CharacterSet = Code128Charset.CharsetB;

            return barCode;
        }

 
        C#:Form1.cs 
        using System;
        using System.Collections.Generic;
        using System.Drawing.Printing;
        using System.Windows.Forms;
        using DevExpress.XtraPrinting.BarCode;
        using DevExpress.XtraReports.UI;
        // ...

        public XRBarCode CreateCode128BarCode(string BarCodeText) {
            // Create a bar code control.
            XRBarCode barCode = new XRBarCode();

            // Set the bar code's type to Code 128.
            barCode.Symbology = new Code128Generator();

            // Adjust the bar code's main properties.
            barCode.Text = BarCodeText;
            barCode.Width = 400;
            barCode.Height = 100;

            // Adjust the properties specific to the bar code type.
            ((Code128Generator)barCode.Symbology).CharacterSet = Code128Charset.CharsetB;

            return barCode;
        }

 

 

        To add the XRBarCode to a report band, handle the report's XRControl.BeforePrint event.

        C# VB  
 
        using System.Drawing.Printing;
        // ... 

        private void XtraReport1_BeforePrint(object sender, PrintEventArgs e) {
            this.Detail.Controls.Add(CreateCode128BarCode("012345678"));
        }
 
        */

    }
}

