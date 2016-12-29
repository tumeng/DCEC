using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Rmes.Pub.Data;
using System.Data;

namespace Rmes.WebApp.Rmes.Exp.Exp2000
{
    public class Report_Exp2000_1 : DevExpress.XtraReports.UI.XtraReport
    {
        private DevExpress.XtraReports.UI.TopMarginBand topMarginBand1;
        private DevExpress.XtraReports.UI.DetailBand detailBand1;
        private DevExpress.XtraReports.UI.XRPivotGrid xrPivotGrid1;
        private DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField xrPivotGridField1;
        private DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField xrPivotGridField2;
        private DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField xrPivotGridField3;
        private DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField xrPivotGridField4;
        private DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField xrPivotGridField5;
        private DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField xrPivotGridField6;
        private DevExpress.XtraReports.UI.XRLabel xrLabel2;
        private DevExpress.XtraReports.UI.XRLabel xrLabel1;
        private DevExpress.XtraReports.UI.XRLabel xrLabel3;
        private DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField xrPivotGridField7;
        private DevExpress.XtraReports.UI.ReportFooterBand ReportFooter;
        private DevExpress.XtraReports.UI.XRTable xrTable1;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow1;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell1;
        private DevExpress.XtraReports.UI.BottomMarginBand bottomMarginBand1;

        private void InitializeComponent()
        {
            this.topMarginBand1 = new DevExpress.XtraReports.UI.TopMarginBand();
            this.xrLabel3 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel2 = new DevExpress.XtraReports.UI.XRLabel();
            this.detailBand1 = new DevExpress.XtraReports.UI.DetailBand();
            this.xrPivotGrid1 = new DevExpress.XtraReports.UI.XRPivotGrid();
            this.xrPivotGridField6 = new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField();
            this.xrPivotGridField1 = new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField();
            this.xrPivotGridField2 = new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField();
            this.xrPivotGridField3 = new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField();
            this.xrPivotGridField4 = new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField();
            this.xrPivotGridField5 = new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField();
            this.xrPivotGridField7 = new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField();
            this.bottomMarginBand1 = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.ReportFooter = new DevExpress.XtraReports.UI.ReportFooterBand();
            this.xrTable1 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell1 = new DevExpress.XtraReports.UI.XRTableCell();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // topMarginBand1
            // 
            this.topMarginBand1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel3,
            this.xrLabel1,
            this.xrLabel2});
            this.topMarginBand1.Name = "topMarginBand1";
            // 
            // xrLabel3
            // 
            this.xrLabel3.LocationFloat = new DevExpress.Utils.PointFloat(628.0835F, 77F);
            this.xrLabel3.Name = "xrLabel3";
            this.xrLabel3.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel3.SizeF = new System.Drawing.SizeF(100F, 23F);
            this.xrLabel3.StylePriority.UseTextAlignment = false;
            this.xrLabel3.Text = "2015/12/1";
            this.xrLabel3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabel1
            // 
            this.xrLabel1.Font = new System.Drawing.Font("宋体", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrLabel1.Name = "xrLabel1";
            this.xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel1.SizeF = new System.Drawing.SizeF(86.45834F, 14.66667F);
            this.xrLabel1.StylePriority.UseFont = false;
            this.xrLabel1.Text = "报表编号";
            // 
            // xrLabel2
            // 
            this.xrLabel2.AutoWidth = true;
            this.xrLabel2.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel2.LocationFloat = new DevExpress.Utils.PointFloat(263.5417F, 39.58333F);
            this.xrLabel2.Name = "xrLabel2";
            this.xrLabel2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel2.SizeF = new System.Drawing.SizeF(197.9168F, 23F);
            this.xrLabel2.StylePriority.UseFont = false;
            this.xrLabel2.StylePriority.UseTextAlignment = false;
            this.xrLabel2.Text = "制造单元完工日报表";
            this.xrLabel2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrLabel2.WordWrap = false;
            // 
            // detailBand1
            // 
            this.detailBand1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPivotGrid1});
            this.detailBand1.Name = "detailBand1";
            // 
            // xrPivotGrid1
            // 
            this.xrPivotGrid1.Appearance.Cell.BackColor = System.Drawing.Color.White;
            this.xrPivotGrid1.Appearance.Cell.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.xrPivotGrid1.Appearance.Cell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrPivotGrid1.Appearance.CustomTotalCell.BackColor = System.Drawing.Color.White;
            this.xrPivotGrid1.Appearance.CustomTotalCell.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.xrPivotGrid1.Appearance.CustomTotalCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrPivotGrid1.Appearance.FieldHeader.BackColor = System.Drawing.Color.White;
            this.xrPivotGrid1.Appearance.FieldHeader.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.xrPivotGrid1.Appearance.FieldHeader.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrPivotGrid1.Appearance.FieldValue.BackColor = System.Drawing.Color.White;
            this.xrPivotGrid1.Appearance.FieldValue.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.xrPivotGrid1.Appearance.FieldValue.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrPivotGrid1.Appearance.FieldValueGrandTotal.BackColor = System.Drawing.Color.White;
            this.xrPivotGrid1.Appearance.FieldValueGrandTotal.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.xrPivotGrid1.Appearance.FieldValueGrandTotal.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrPivotGrid1.Appearance.FieldValueTotal.BackColor = System.Drawing.Color.White;
            this.xrPivotGrid1.Appearance.FieldValueTotal.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.xrPivotGrid1.Appearance.FieldValueTotal.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrPivotGrid1.Appearance.FilterSeparator.BackColor = System.Drawing.Color.White;
            this.xrPivotGrid1.Appearance.FilterSeparator.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.xrPivotGrid1.Appearance.FilterSeparator.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrPivotGrid1.Appearance.GrandTotalCell.BackColor = System.Drawing.Color.White;
            this.xrPivotGrid1.Appearance.GrandTotalCell.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.xrPivotGrid1.Appearance.GrandTotalCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrPivotGrid1.Appearance.HeaderGroupLine.BackColor = System.Drawing.Color.White;
            this.xrPivotGrid1.Appearance.HeaderGroupLine.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.xrPivotGrid1.Appearance.HeaderGroupLine.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrPivotGrid1.Appearance.Lines.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.xrPivotGrid1.Appearance.Lines.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrPivotGrid1.Appearance.TotalCell.BackColor = System.Drawing.Color.White;
            this.xrPivotGrid1.Appearance.TotalCell.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.xrPivotGrid1.Appearance.TotalCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrPivotGrid1.Fields.AddRange(new DevExpress.XtraPivotGrid.PivotGridField[] {
            this.xrPivotGridField6,
            this.xrPivotGridField1,
            this.xrPivotGridField2,
            this.xrPivotGridField3,
            this.xrPivotGridField4,
            this.xrPivotGridField5,
            this.xrPivotGridField7});
            this.xrPivotGrid1.LocationFloat = new DevExpress.Utils.PointFloat(3.178914E-05F, 0F);
            this.xrPivotGrid1.Name = "xrPivotGrid1";
            this.xrPivotGrid1.OptionsChartDataSource.UpdateDelay = 300;
            this.xrPivotGrid1.OptionsDataField.AreaIndex = 2;
            this.xrPivotGrid1.OptionsView.RowTotalsLocation = DevExpress.XtraPivotGrid.PivotRowTotalsLocation.Near;
            this.xrPivotGrid1.OptionsView.ShowColumnGrandTotalHeader = false;
            this.xrPivotGrid1.OptionsView.ShowColumnHeaders = false;
            this.xrPivotGrid1.OptionsView.ShowColumnTotals = false;
            this.xrPivotGrid1.OptionsView.ShowDataHeaders = false;
            this.xrPivotGrid1.OptionsView.ShowFilterHeaders = false;
            this.xrPivotGrid1.OptionsView.ShowFilterSeparatorBar = false;
            this.xrPivotGrid1.OptionsView.ShowHorzLines = false;
            this.xrPivotGrid1.OptionsView.ShowRowGrandTotalHeader = false;
            this.xrPivotGrid1.OptionsView.ShowRowGrandTotals = false;
            this.xrPivotGrid1.OptionsView.ShowRowTotals = false;
            this.xrPivotGrid1.OptionsView.ShowVertLines = false;
            this.xrPivotGrid1.SizeF = new System.Drawing.SizeF(728.0834F, 100F);
            // 
            // xrPivotGridField6
            // 
            this.xrPivotGridField6.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.xrPivotGridField6.AreaIndex = 0;
            this.xrPivotGridField6.Caption = "生产部门";
            this.xrPivotGridField6.FieldName = "DEPT";
            this.xrPivotGridField6.Name = "xrPivotGridField6";
            this.xrPivotGridField6.Width = 80;
            // 
            // xrPivotGridField1
            // 
            this.xrPivotGridField1.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.xrPivotGridField1.AreaIndex = 1;
            this.xrPivotGridField1.Caption = "生产线";
            this.xrPivotGridField1.FieldName = "PLINE_NAME";
            this.xrPivotGridField1.Name = "xrPivotGridField1";
            this.xrPivotGridField1.Width = 260;
            // 
            // xrPivotGridField2
            // 
            this.xrPivotGridField2.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.xrPivotGridField2.AreaIndex = 2;
            this.xrPivotGridField2.Caption = "合同号";
            this.xrPivotGridField2.FieldName = "PROJECT_CODE";
            this.xrPivotGridField2.Name = "xrPivotGridField2";
            this.xrPivotGridField2.TotalsVisibility = DevExpress.XtraPivotGrid.PivotTotalsVisibility.None;
            this.xrPivotGridField2.UnboundFieldName = "xrPivotGridField2";
            this.xrPivotGridField2.Width = 80;
            // 
            // xrPivotGridField3
            // 
            this.xrPivotGridField3.AllowedAreas = DevExpress.XtraPivotGrid.PivotGridAllowedAreas.RowArea;
            this.xrPivotGridField3.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.xrPivotGridField3.AreaIndex = 4;
            this.xrPivotGridField3.Caption = "成品号";
            this.xrPivotGridField3.FieldName = "PRODUCT_CODE";
            this.xrPivotGridField3.Name = "xrPivotGridField3";
            this.xrPivotGridField3.SummaryType = DevExpress.Data.PivotGrid.PivotSummaryType.Custom;
            // 
            // xrPivotGridField4
            // 
            this.xrPivotGridField4.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.xrPivotGridField4.AreaIndex = 5;
            this.xrPivotGridField4.Caption = "入库地点";
            this.xrPivotGridField4.FieldName = "STORE_CODE";
            this.xrPivotGridField4.Name = "xrPivotGridField4";
            this.xrPivotGridField4.SummaryType = DevExpress.Data.PivotGrid.PivotSummaryType.Var;
            this.xrPivotGridField4.Width = 80;
            // 
            // xrPivotGridField5
            // 
            this.xrPivotGridField5.Area = DevExpress.XtraPivotGrid.PivotArea.ColumnArea;
            this.xrPivotGridField5.AreaIndex = 0;
            this.xrPivotGridField5.Caption = "备注";
            this.xrPivotGridField5.EmptyCellText = "   ";
            this.xrPivotGridField5.EmptyValueText = "   ";
            this.xrPivotGridField5.GrandTotalText = "   ";
            this.xrPivotGridField5.Name = "xrPivotGridField5";
            this.xrPivotGridField5.Width = 23;
            // 
            // xrPivotGridField7
            // 
            this.xrPivotGridField7.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.xrPivotGridField7.AreaIndex = 3;
            this.xrPivotGridField7.Caption = "订单数量";
            this.xrPivotGridField7.FieldName = "ORDER_NUM";
            this.xrPivotGridField7.Name = "xrPivotGridField7";
            this.xrPivotGridField7.Width = 80;
            // 
            // bottomMarginBand1
            // 
            this.bottomMarginBand1.HeightF = 30F;
            this.bottomMarginBand1.Name = "bottomMarginBand1";
            // 
            // ReportFooter
            // 
            this.ReportFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable1});
            this.ReportFooter.HeightF = 26.04167F;
            this.ReportFooter.Name = "ReportFooter";
            // 
            // xrTable1
            // 
            this.xrTable1.LocationFloat = new DevExpress.Utils.PointFloat(3.178914E-05F, 0F);
            this.xrTable1.Name = "xrTable1";
            this.xrTable1.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow1});
            this.xrTable1.SizeF = new System.Drawing.SizeF(728.0834F, 25F);
            // 
            // xrTableRow1
            // 
            this.xrTableRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell1});
            this.xrTableRow1.Name = "xrTableRow1";
            this.xrTableRow1.Weight = 1D;
            // 
            // xrTableCell1
            // 
            this.xrTableCell1.Name = "xrTableCell1";
            this.xrTableCell1.Text = "总计";
            this.xrTableCell1.Weight = 3D;
            // 
            // Report_Exp2000_1
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.topMarginBand1,
            this.detailBand1,
            this.bottomMarginBand1,
            this.ReportFooter});
            this.Margins = new System.Drawing.Printing.Margins(53, 60, 100, 30);
            this.Version = "11.1";
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        public Report_Exp2000_1(string pline_code,string dept_code,DateTime date)
        {
            InitializeComponent();
            xrLabel3.Text = date.ToShortDateString();
            string _b = date.ToShortDateString() + " 00:00:00";
            string _e = date.ToShortDateString() + " 23:59:59";
            
            dataConn theconn = new dataConn();
            DataTable dt = new DataTable();
            
            
            string dept_name = theconn.GetValue("select dept_name from code_dept where dept_code='"+dept_code+"'");
            //DataTable plines = theconn.GetTable("select pline_code from REL_DEPT_PLINE where dept_code='" + dept_code + "'");

            string sql = string.Format("select * from VW_DATA_COMPLETE_PER_DAY t left join (select count(instore_qty) as order_num,d.project_code from VW_DATA_COMPLETE_PER_DAY d where d.work_date between to_date('{0}','yyyy-mm-dd hh24:mi:ss') and to_date('{1}','yyyy-mm-dd hh24:mi:ss') and d.pline_code='{2}' group by d.project_code) a on a.project_code = t.project_code where t.work_date between to_date('{3}','yyyy-mm-dd hh24:mi:ss') and to_date('{4}','yyyy-mm-dd hh24:mi:ss') and t.pline_code='{5}'", _b, _e, pline_code, _b, _e, pline_code);
            dt.Merge(theconn.GetTable(sql));

            dt.Columns.Add("DEPT");
            int num = 0;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dt.Rows[i]["DEPT"] = dept_name;
                num += Convert.ToInt32(dt.Rows[i]["INSTORE_QTY"]);
            }
                //this.DataSource = dt;
                this.xrPivotGrid1.DataSource = dt;


                xrTableCell1.Text = "总计" + num + "台";
        }
    }
}