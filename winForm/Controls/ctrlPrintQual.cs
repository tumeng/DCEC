using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using Rmes.Public;
using System.Windows.Forms;
using Rmes.WinForm.Base;
using System.IO;
using System.IO.Ports;
using Microsoft.Win32.SafeHandles;
using System.Runtime.InteropServices;

using Rmes.DA.Base;
using Rmes.DA.Entity;
using Rmes.DA.Factory;

namespace Rmes.WinForm.Controls
{
    public partial class ctrlPrintQual : BaseControl
    {
        private string theCompanyCode;
        
        public ctrlPrintQual()
        {
            InitializeComponent();
            theCompanyCode = LoginInfo.CompanyInfo.COMPANY_CODE;
            GridSn2Print.AutoGenerateColumns = false;
            dateTimePicker1.Value = DateTime.Today.AddDays(-3);
            dateTimePicker1.Value = DateTime.Today;
            InitPlineList();
            InitSoList();
            //InitSn2Print();
        }

        private void InitPlineList()
        {
            List<string> lst_pline = new List<string> { "M010", "M020", "M050" };
            List<ProductLineEntity> lst = ProductLineFactory.GetAll();
            //var s = (from p in lst from q in lst_pline where p.AUTO_TYPE == "A" && p.PLINE_CODE==q orderby p.PLINE_NAME select p).ToList();
            BarCodeEntity s = null;
            comboBox1.DisplayMember = "PLINE_NAME";
            comboBox1.ValueMember = "PLINE_CODE";
            comboBox1.DataSource = s;
        }
        private void InitSoList()
        {
            string proCode = textBox1.Text.ToUpper();
            string pline = comboBox1.SelectedValue.ToString();
            if (!string.IsNullOrEmpty(proCode))
            {
                //List<PlanEntity> plan = PlanFactory.GetAll();
                //var a = (from s in plan where s.PROJECT_CODE.Equals(proCode) && s.PLINE_CODE.Equals(pline) select s.PLAN_SO).Distinct().ToList();
                //listBox1.ValueMember= "PLAN_SO";
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
            //List<DetectDataEntity> lst_rpt = new List<DetectDataEntity>();
            if (textBox1.Text.ToString() != "")
            {
                
                if (listBox1.SelectedItems.Count == 0)
                {
                    MessageBox.Show("成品号不能为空");
                    return;
                }
                string sn = listBox1.SelectedValue.ToString().Trim();
                List<DetectReportEntity> lst_rpt_1 = DetectReportFactory.GetAll();
                lst_rpt_1 = (from s in lst_rpt_1 where s.SN.Equals(sn) select s).ToList();
                GridSn2Print.DataSource = lst_rpt_1;
            }
            else
            {

                List<DetectReportEntity> lst_rpt = DetectReportFactory.GetByPlineAndDate(theCompanyCode, pline_code, date1, date2);
                if (lst_rpt == null)
                {
                    MessageBox.Show("时间选择错误，请重新选择时间或选择成品号");
                    return;

                }
                GridSn2Print.DataSource = lst_rpt;
            }           
        }

      
        private void button1_Click(object sender, EventArgs e)
        {
            
            InitSn2Print();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            (Parent as frmPrintQual).Close();
        }

        
        private void GridSn2Print_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (GridSn2Print.Rows.Count == 0) return;

            if (e.RowIndex == -1) return;
            
            if (e.ColumnIndex == 0)
            {
                string rmes_id=GridSn2Print.Rows[e.RowIndex].Cells["colRmesID"].Value.ToString();
                string sn = GridSn2Print.Rows[e.RowIndex].Cells["colSn"].Value.ToString();
                string file_path = @"\\10.212.202.20\d$\SyncFiles";
                string file_name = GridSn2Print.Rows[e.RowIndex].Cells["colFileName"].Value.ToString();
                file_name = file_path + "\\" + file_name;
                if(!File.Exists(file_name))
                {
                    MessageBox.Show("相应的质检报告文件没有找到");
                    return;

                }
                //File.Open(file_name,FileMode.Open);

                System.Diagnostics.Process pExecuteEXE = new System.Diagnostics.Process();
                pExecuteEXE.StartInfo.FileName = @"C:\Program Files\Microsoft Office\Office15\excel.exe";
                pExecuteEXE.StartInfo.FileName = "excel.exe";
                pExecuteEXE.StartInfo.Arguments = file_name;
                pExecuteEXE.Start();
                pExecuteEXE.WaitForExit();//

                DetectReportEntity ent = DetectReportFactory.GetByKey(rmes_id);
                ent.PRINT_DATE = DateTime.Now;
                ent.PRINT_TIMES = ent.PRINT_TIMES + 1;
                DetectReportFactory.UpdateInsertRecord(ent);

                //InitSn2Print();
            }
            
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            InitSoList();
        }

        

    }
}
