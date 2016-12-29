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
using Rmes.DA.Base;
using Rmes.DA.Entity;
using Rmes.DA.Factory;
using System.IO;
using System.IO.Ports;
using Microsoft.Win32.SafeHandles;
using System.Runtime.InteropServices;
using System.Drawing.Printing;


namespace Rmes.WinForm.Controls
{
    public partial class ctrlprinting :BaseControl
    {
        [DllImport("kernel32.dll", SetLastError = true)]
        static extern SafeFileHandle CreateFile(string lpFileName, FileAccess dwDesiredAccess,
            uint dwShareMode, IntPtr lpSecurityAttributes, FileMode dwCreationDisposition,
            uint dwFlagsAndAttributes, IntPtr hTemplateFile);

        [DllImport("FNTHEX32.dll")]
        //[DllImport(@"D:\CHKMES\rmes.workstation\Lib\FNTHEX32.dll")]
        public static extern int GETFONTHEX(
                              string BarcodeText,
                              string FontName,
                              string FileName,
                              int Orient,
                              int Height,
                              int Width,
                              int IsBold,
                              int IsItalic,
                              StringBuilder ReturnBarcodeCMD); 
        public ctrlprinting()
        {
            InitializeComponent();
            dateTimePicker1.Value = DateTime.Now.AddDays(-7);
            dateTimePicker2.Value = DateTime.Now;
            InitPlineList();
            initSnEdit();
        }

        private void InitPlineList()
        {
            //List<ProductLineEntity> lst = ProductLineFactory.GetAll();
            //var s = (from p in lst where p.AUTO_TYPE == "A" orderby p.PLINE_NAME select p).ToList();

            //comboBox1.DisplayMember = "PLINE_NAME";
            //comboBox1.ValueMember = "PLINE_CODE";
            //comboBox1.DataSource = s;
        }

        private void initSnEdit()
        {
            GridPrint.Rows.Clear();

            List<PlanEntity> allPlans = PlanFactory.GetByCreatePeriod(dateTimePicker1.Value, dateTimePicker2.Value);
            if (comboBox1.SelectedIndex >= 0)
            {
                string pline_code=comboBox1.SelectedValue.ToString();
                allPlans=(from s in allPlans where s.PLINE_CODE==pline_code select s).ToList();
            }
            if (allPlans.Count > 0)
            {
                foreach (var p in allPlans)
                {
                    List<PlanSnEntity> snEntity = PlanSnFactory.WebGetByOrderCode(p.ORDER_CODE);
                    if (snEntity == null) continue;
                    foreach (var s in snEntity)
                    {
                        //int i = GridPrint.Rows.Add();
                        //GridPrint.Rows[i].Cells["colPrint"].Value = "打印";
                        //GridPrint.Rows[i].Cells["colProject"].Value = p.PROJECT_CODE;
                        //GridPrint.Rows[i].Cells["colOrderCode"].Value = p.ORDER_CODE;
                        //GridPrint.Rows[i].Cells["colModel"].Value = p.PLAN_SO;
                        //GridPrint.Rows[i].Cells["colSeries"].Value = p.PRODUCT_SERIES;
                        //GridPrint.Rows[i].Cells["colBarCode"].Value = p.DETECT_BARCODE;
                        //GridPrint.Rows[i].Cells["colSN"].Value = s.SN;
                    }

                }
            }
            if(statusStrip1.Items.Count==0)
                statusStrip1.Items.Add("记录数量：" + allPlans.Count.ToString());
            else
                statusStrip1.Items[0].Text = "记录数量：" + allPlans.Count.ToString();
                // GridEdit.Rows[i].ReadOnly = true;
                // bCell.Value = "不可打印";
            
        }

        private void GridPrint_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return;
            if (e.ColumnIndex == 0)
            {
                int i=e.RowIndex;
                //SnEditEntity en = SnEditFactory.GetBySN(GridPrinting.Rows[j].Cells["colSn3"].Value.ToString());
                if (string.IsNullOrWhiteSpace(GridPrint.Rows[e.RowIndex].Cells["colSn"].Value as string))
                    return;
                
                 string code = "";
                    string code1 = "";

                    string str1 = "订单号：" + GridPrint.Rows[i].Cells["colOrderCode"].Value as string + " 工程：" + GridPrint.Rows[i].Cells["colProject"].Value as string;
                    //string str2 = "   "+"间隔：" + en.TEMP1 + "   " + en.TEMP2;
                    string str3 = "产品型号：" + GridPrint.Rows[i].Cells["colModel"].Value as string + " 产品系列：" + GridPrint.Rows[i].Cells["colSeries"].Value as string;
                    //string str4 = "   "+"相序：" + en.TEMP3 + "   " + "备注一:" + en.TEMP4;
                   // string str5 = "   "+"班组：" + en.TEAM_NAME + "    " + "备注二：" + en.TEMP5;
                    string str6 = "SN："+GridPrint.Rows[i].Cells["colSN"].Value as string;
                    string str7 = "质检条码："+GridPrint.Rows[i].Cells["colBarCode"].Value as string;
                    
                    

                    StringBuilder project = new StringBuilder(10240);
                    int i1 = GETFONTHEX(str1, "宋体", "temp1", 0, 25, 15, 0, 0, project);
                    //StringBuilder jg = new StringBuilder(10240);
                    //int i2 = GETFONTHEX(str2, "宋体", "temp1", 10, 30, 20, 0, 0, jg);
                    StringBuilder SO = new StringBuilder(10240);
                    int i3 = GETFONTHEX(str3, "宋体", "temp1", 0, 25, 15, 0, 0, SO);
                    //StringBuilder xx = new StringBuilder(10240);
                    //int i4 = GETFONTHEX(str4, "宋体", "temp1", 10, 30, 20, 0, 0, xx);
                    //StringBuilder bz = new StringBuilder(10240);
                    //int i5 = GETFONTHEX(str5, "宋体", "temp1", 10, 30, 20, 0, 0, bz);
                    StringBuilder h = new StringBuilder(10240);
                    int i6 = GETFONTHEX(str6, "宋体", "temp1", 0, 25, 15, 0, 0, h);

                    StringBuilder bar = new StringBuilder(10240);
                    int i7 = GETFONTHEX(str7, "宋体", "temp1", 0, 25, 15, 0, 0, bar);


                    if (string.IsNullOrEmpty(GridPrint.Rows[i].Cells["colBarCode"].Value as string))
                    {
                        code1 = "^XA^FO200,65^BY2,2,50^BCN,100,N,N,N^FD" + GridPrint.Rows[i].Cells["colSN"].Value as string + "^FS" +
                                 h.ToString() + "^MD30^LH120,10^FO100,165^XGtemp1,1,1^FS" +
                                 project.ToString() + "^MD30^LH20,10^FO20,200^XGtemp1,1,1^FS" +
                                 SO.ToString() + "^MD30^LH20,10^FO20,225^XGtemp1,1,1^FS" +
                                 "^XZ";
                    }
                    else
                    {
                        //code1 = "^XA^FO80,25^BY2,2,50^BCN,100,N,N,N^FD" + GridPrint.Rows[i].Cells["colSN"].Value as string + "^FS" +
                        //         h.ToString() + "^MD30^LH120,10^FO80,125^XGtemp1,1,1^FS" +
                        //         project.ToString() + "^MD30^LH30,10^FO20,160^XGtemp1,1,1^FS" +
                        //         SO.ToString() + "^MD30^LH30,10^FO20,185^XGtemp1,1,1^FS" +
                        //         "^FO80,225^BY2,2,50^BCN,100,N,N,N^FD" + GridPrint.Rows[i].Cells["colBarCode"].Value as string + "^FS" +
                        //         bar.ToString() + "^MD30^LH120,10^FO20,325^XGtemp1,1,1^FS" +
                        //         "^XZ";
                        code1 = "^XA^FO200,65^BY2,2,50^BCN,100,N,N,N^FD" + GridPrint.Rows[i].Cells["colSN"].Value as string + "^FS" +
                                 h.ToString() + "^MD30^LH120,10^FO100,165^XGtemp1,1,1^FS" +
                                 project.ToString() + "^MD30^LH20,10^FO50,240^XGtemp1,1,1^FS" +
                                 SO.ToString() + "^MD30^LH20,10^FO50,265^XGtemp1,1,1^FS" +
                                 "^FO50,305^BY2,2,50^BCN,100,N,N,N^FD" + GridPrint.Rows[i].Cells["colBarCode"].Value as string + "^FS" +
                                 bar.ToString() + "^MD30^LH20,10^FO50,405^XGtemp1,1,1^FS" +
                                 "^XZ";
                    }

                    DB.GetInstance().BeginTransaction();
                    try
                    {
                        PrintDriver.SendStringToPrinter("ZDesigner ZM400 200 dpi (ZPL)", code1);
                        string orderCode = GridPrint.Rows[i].Cells["colOrderCode"].Value as string;
                        DB.GetInstance().Execute("update DATA_PLAN_SN set SN_FLAG='P' where ORDER_CODE=@0", orderCode);
                        DB.GetInstance().CompleteTransaction();
                    }
                    catch (Exception ex)
                    {
                        DB.GetInstance().AbortTransaction();
                        MessageBox.Show(ex.Message);
                    }
                    //initSnEdit();
                }
                
        }

        private void button1_Click(object sender, EventArgs e)
        {
            initSnEdit();
        }


        

        
        
    }
}
