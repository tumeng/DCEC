using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Rmes.Public;
using Rmes.WinForm.Base;
using System.IO;
using System.IO.Ports;
using Microsoft.Win32.SafeHandles;
using System.Runtime.InteropServices;
using System.Drawing.Printing;
using Microsoft.Office.Interop.Excel;


using Rmes.DA.Base;
using Rmes.DA.Entity;
using Rmes.DA.Factory;

namespace Rmes.WinForm.Controls
{
    public partial class ctrlPrintQual_1 : UserControl
    {
        public string file_template="\\Template\\KGG.xlsx";
        public List<OPCTagsEntity> OPC_TAGS;
        private string theCompanyCode;
        public ctrlPrintQual_1()
        {
            InitializeComponent();
            theCompanyCode = LoginInfo.CompanyInfo.COMPANY_CODE;
            GridSn2Print.AutoGenerateColumns = false;
            dateTimePicker1.Value = DateTime.Today.AddDays(-3);
            dateTimePicker1.Value = DateTime.Today;
            InitPlineList();
            
            //InitSn2Print();
        }

        private void InitPlineList()
        {
            //List<string> lst_pline = new List<string>{"M030", "M040", "M060"};

            //List<ProductLineEntity> lst = ProductLineFactory.GetAll();
            //var s = (from p in lst from q in lst_pline where p.AUTO_TYPE == "A" && p.PLINE_CODE==q  orderby p.PLINE_NAME select p).ToList();

            //comboBox1.DisplayMember = "PLINE_NAME";
            //comboBox1.ValueMember = "PLINE_CODE";
            //comboBox1.DataSource = s;
        }
        private void InitSoList()
        {
            string proCode = textBox1.Text.ToUpper();
            if (!string.IsNullOrEmpty(proCode))
            {
                //List<PlanEntity> plan = PlanFactory.GetAll();
                //string pline = comboBox1.SelectedValue.ToString();
                //var a = (from s in plan where s.PROJECT_CODE.Equals(proCode) && s.PLINE_CODE.Equals(pline) select s.PLAN_SO).Distinct().ToList();
                //listBox1.ValueMember = "PLAN_SO";
                //listBox1.DataSource = a;


            }
        }
        private void InitSn2Print()
        {
            string StationCode = LoginInfo.StationInfo.RMES_ID;
            string Pline = LoginInfo.StationInfo.PLINE_CODE;
            string pline_code = comboBox1.SelectedValue.ToString();
            string date1 = this.dateTimePicker1.Value.ToString("yyyyMMdd");
            string date2 = this.dateTimePicker2.Value.AddDays(1).ToString("yyyyMMdd");
            if (textBox1.Text.ToString() != "")
            {
                if (listBox1.SelectedItems.Count == 0)
                {
                    MessageBox.Show("成品号不能为空");
                    return;
                }
                string sn = listBox1.SelectedValue.ToString().Trim();
                List<SNDetectdDataEntity> lst_detect_1 = SNDetectFactory.GetBySn(sn);
                var h = (from k in lst_detect_1 select new { SN = k.SN, PLINE_CODE = k.PLINE_CODE, WORK_TIME = k.WORK_TIME.ToShortDateString() }).ToList();
                GridSn2Print.DataSource = h;
            }
            //List<DetectDataEntity> lst_rpt = new List<DetectDataEntity>();
            else
            {
                List<SNDetectdDataEntity> lst_detect = SNDetectFactory.GetDetectDataAuto(theCompanyCode, pline_code, date1, date2);
                if (lst_detect.Count == 0)
                {
                    MessageBox.Show("时间选择错误，请重新选择时间或选择成品号");
                    return;
                }

                var s = (from p in lst_detect select new { SN = p.SN, PLINE_CODE = p.PLINE_CODE, WORK_TIME = p.WORK_TIME.ToShortDateString() }).ToList();
                //int a = s.Count();

                GridSn2Print.DataSource = s;
            }

        }


        private void SetQualFiles(string pSn,string pPlineCode)
        {
            Microsoft.Office.Interop.Excel.Application apps=new Microsoft.Office.Interop.Excel.Application();
           
           
            //string filename = System.Windows.Forms.Application.StartupPath+"\\..\\.." + file_template;
            string filename = System.Windows.Forms.Application.StartupPath + file_template;

            object oMissing = System.Reflection.Missing.Value;
            
            Workbook _wkbook = apps.Workbooks.Open(filename,0, true, 5, oMissing, oMissing, true, 1, oMissing, false, false, oMissing, false, oMissing, oMissing); 
            Worksheet _wksheet = _wkbook.Sheets["sheet1"];

            
            //OPCStationEntity aa=OPCStationFactory.GetByPlineCode(
            //List<OPCTagsEntity> qual_list=OPCTagsFactory.GetQualityItems(
            //_wksheet.Cells[1,7]="123";
            //_wksheet.Range["G1"].Value = "123";
            List<SNDetectdDataEntity> lst_detect = SNDetectFactory.GetByPlineCodeSn(theCompanyCode, pPlineCode,pSn);

            foreach (OPCTagsEntity s in OPC_TAGS)
            {
                SNDetectdDataEntity detect_data=(from p in lst_detect where p.DETECT_DATA_CODE==s.OPC_TAG_NAME select p).First();

                //string ssi = _wksheet.Range[s.OPC_TAG_NAME].Value;
                string detect_value = detect_data.DETECT_DATA_VALUE.ToUpper();
                if (detect_value == "TRUE") detect_value = "合格";
                else if (detect_value == "FALSE") detect_value = "不合格";

                _wksheet.Range[s.OPC_TAG_NAME].Value = detect_value;
                
            }
            List<PlanEntity> plan_data = PlanFactory.GetByPlanSO(pSn);
            //if (plan_data.Count != 0)
            //{
            //    _wksheet.Cells[3, 4] = plan_data.First().PRODUCT_SERIES.ToString();
            //    _wksheet.Cells[4, 6] = plan_data.First().PROJECT_CODE.ToString();
            //}
            _wksheet.Cells[4, 4] = pSn;
            //_wkbook.Save();
            apps.Visible = true;
            _wksheet.PrintPreview(null);
            //apps.Visible = false;
            _wkbook.Close(0, oMissing, oMissing);
            apps.Quit();
            GC.Collect();
            

        }

        private void button1_Click(object sender, EventArgs e)
        {
            InitSn2Print();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            (Parent as frmPrintQual_1).Close();
        }
       
       

        private void GridSn2Print_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (GridSn2Print.Rows.Count == 0) return;

            if (e.RowIndex == -1) return;

            if (e.ColumnIndex == 0)
            {
                string sn = GridSn2Print.Rows[e.RowIndex].Cells["colSn"].Value.ToString();
                string pline_code = GridSn2Print.Rows[e.RowIndex].Cells["colPlineCode"].Value.ToString();
                OPCStationEntity opc_station = OPCStationFactory.GetByPlineCode(pline_code);

                string rmes_id = opc_station.RMES_ID;
                OPC_TAGS = OPCTagsFactory.GetQualityItems(rmes_id, "3");
                SetQualFiles(sn, pline_code);

                
                //InitSn2Print();
            }


        }

        private void button3_Click(object sender, EventArgs e)
        {
            InitSoList();
        }

        
    }
}
