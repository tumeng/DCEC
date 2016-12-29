using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Rmes.DA.Base;
using Rmes.DA.Entity;
using Rmes.DA.Factory;
using Rmes.WinForm.Base;
using Rmes.Pub.Data1;
using System.IO;
using System.Collections;

namespace Rmes.WinForm.Controls
{
    public partial class ctrlProcessPic : BaseControl
    {
        //private string wjlj = "\\\\192.168.112.82\\pic";
        private string wjlj = "";
        public string SN, So, productmodel;
        public string planCode, orderCode;
        public string companyCode, PlineCode, Plineid, stationID, WorkunitCode, LinesideStock, stationname, pline_type, Station_Code;
        private string oldVendorCode;
        //public string[] wjpath;
        private ArrayList wjpath = new ArrayList();
        private int count1 = 0, curcount1 = 0;
        //dataConn dc = new dataConn();
        public ctrlProcessPic()
        {
            InitializeComponent();
            stationID= LoginInfo.StationInfo.RMES_ID;
            Station_Code = LoginInfo.StationInfo.STATION_CODE;
            PlineCode = LoginInfo.ProductLineInfo.PLINE_CODE;
            Plineid = LoginInfo.ProductLineInfo.RMES_ID;
            companyCode = LoginInfo.CompanyInfo.COMPANY_CODE;
            stationname = LoginInfo.StationInfo.STATION_NAME;
            pline_type = LoginInfo.ProductLineInfo.PLINE_TYPE_CODE;
            planCode = "";
            wjlj = dataConn.GetValue("select INTERNAL_VALUE FROM CODE_INTERNAL WHERE COMPANY_CODE='" + companyCode
            + "' AND INTERNAL_TYPE_CODE='PATH' AND INTERNAL_CODE='PROCESSNOTEPATH'");
            timer1.Enabled = false;
            Show();
            //this.RMesDataChanged += new Rmes.WinForm.Base.RMesEventHandler(ctrlShowPic_RmesDataChanged);
        }
        public ctrlProcessPic(string type)
        {
            InitializeComponent();
            stationID = LoginInfo.StationInfo.RMES_ID;
            Station_Code = LoginInfo.StationInfo.STATION_CODE;
            PlineCode = LoginInfo.ProductLineInfo.PLINE_CODE;
            Plineid = LoginInfo.ProductLineInfo.RMES_ID;
            companyCode = LoginInfo.CompanyInfo.COMPANY_CODE;
            stationname = LoginInfo.StationInfo.STATION_NAME;
            pline_type = LoginInfo.ProductLineInfo.PLINE_TYPE_CODE;
            planCode = "";
            wjlj = dataConn.GetValue("select INTERNAL_VALUE FROM CODE_INTERNAL WHERE COMPANY_CODE='" + companyCode
            + "' AND INTERNAL_TYPE_CODE='PATH' AND INTERNAL_CODE='PROCESSNOTEPATH'");
            timer1.Enabled = false;
            Show1();
            //this.RMesDataChanged += new Rmes.WinForm.Base.RMesEventHandler(ctrlShowPic_RmesDataChanged);
        }
        public ctrlProcessPic(string type,string plinecode1)
        {
            InitializeComponent();
            stationID = LoginInfo.StationInfo.RMES_ID;
            Station_Code = LoginInfo.StationInfo.STATION_CODE;
            PlineCode = plinecode1;
            Plineid = LoginInfo.ProductLineInfo.RMES_ID;
            companyCode = LoginInfo.CompanyInfo.COMPANY_CODE;
            stationname = LoginInfo.StationInfo.STATION_NAME;
            pline_type = LoginInfo.ProductLineInfo.PLINE_TYPE_CODE;
            planCode = "";
            wjlj = dataConn.GetValue("select INTERNAL_VALUE FROM CODE_INTERNAL WHERE COMPANY_CODE='" + companyCode
            + "' AND INTERNAL_TYPE_CODE='PATH' AND INTERNAL_CODE='PROCESSNOTEPATH'");
            timer1.Enabled = false;
            Show();
            //this.RMesDataChanged += new Rmes.WinForm.Base.RMesEventHandler(ctrlShowPic_RmesDataChanged);
        }
        private bool Show()
        {
            try
            {
                //this.Visible = false;
                string sql = "select wjpath from rstbomts where zddm='" + Station_Code + "' and wjpath is not null ";
                DataTable dt = dataConn.GetTable(sql);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string[] s = dt.Rows[i][0].ToString().Split('$');
                    for (int j = 0; j < s.Length; j++)
                    {
                        string path1 = wjlj + "\\" + PlineCode + "\\" + s[j];
                        if (File.Exists(path1))
                        {
                            if (!wjpath.Contains(path1))
                            {
                                wjpath.Add(path1);
                            }
                        }
                    }
                }
                curcount1 = 0;
                count1 = wjpath.Count;
                label1.Text = "显示" + count1 + "张图片";
                if (count1 > 0)
                {
                    this.Visible = true;
                    //FrmShowProcessPic form1 = new FrmShowProcessPic();
                    //form1.ShowDialog();
                    pictureBox1.Image = Image.FromFile(wjpath[0].ToString());
                    timer1.Enabled = true;
                    return true;
                }
                else
                {
                    //timer1.Enabled = true;

                    //Form tempForm = (Form)this.ParentForm;
                    //tempForm.Hide();
                    //(Parent as FrmShowProcessPic).Hide();
                    return false;
                    
                    //(Parent as FrmShowProcessPic).Close();
                    // (Parent as FrmShowProcessPic).Hide();
                }
            }
            catch (Exception e1)
            {
                MessageBox.Show("装机图片" + e1.Message, "提示");
                return false;
            }
        }
        private bool Show1()
        {
            try
            {
                //this.Visible = false;
                string sql = "select wjpath from rstbomts where zddm='" + Station_Code + "' and wjpath is not null ";
                DataTable dt = dataConn.GetTable(sql);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string[] s = dt.Rows[i][0].ToString().Split('$');
                    for (int j = 0; j < s.Length; j++)
                    {
                        string path1 = wjlj + "\\" + PlineCode + "\\" + s[j];
                        if (File.Exists(path1))
                        {
                            if (!wjpath.Contains(path1))
                            {
                                wjpath.Add(path1);
                            }
                        }
                    }
                }
                curcount1 = 0;
                count1 = wjpath.Count;
                label1.Text = "显示" + count1 + "张图片";
                if (count1 > 0)
                {
                    this.Visible = true;
                    pictureBox1.Image = Image.FromFile(wjpath[0].ToString());
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e1)
            {
                MessageBox.Show("装机图片" + e1.Message, "提示");
                return false;
            }
        }
        private void ctrlShowPic_RmesDataChanged(object obj, Rmes.WinForm.Base.RMESEventArgs e)
        {
            if (e.MessageHead == null) return;
            if (e.MessageHead == "SHOWPICTURE")
            {
                this.Visible = false;
                string sql = "select wjpath from rstbomts where zddm='"+Station_Code+"' and wjpath is not null ";
                DataTable dt = dataConn.GetTable(sql);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string[] s=dt.Rows[i][0].ToString().Split('$');
                    for (int j = 0; j < s.Length; j++)
                    {
                        string path1 = wjlj + "\\" + PlineCode + "\\" + s[j];
                        if (File.Exists(path1))
                        {
                            if (!wjpath.Contains(path1))
                            {
                                wjpath.Add(path1);
                            }
                        }
                    }
                }
                curcount1 = 0;
                count1 = wjpath.Count;
                label1.Text = "显示" + count1 + "张图片";
                if (count1 > 0)
                {
                    this.Visible = true;
                    //FrmShowProcessPic form1 = new FrmShowProcessPic();
                    //form1.ShowDialog();
                    pictureBox1.Image = Image.FromFile(wjpath[0].ToString());
                    timer1.Enabled = true;
                }
                else
                {
                    (Parent as FrmShowProcessPic).Hide();
                }
                return;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                //下一张
                curcount1 = curcount1 + 1;
                if (curcount1 == count1)
                {
                    curcount1 = 0;
                }

                pictureBox1.Image = Image.FromFile(wjpath[curcount1].ToString());
            }
            catch
            { }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                //上一张
                curcount1 = curcount1 - 1;
                if (curcount1 == -1)
                {
                    curcount1 = count1 - 1;
                }
                pictureBox1.Image = Image.FromFile(wjpath[curcount1].ToString());
            }
            catch
            { }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                timer1.Enabled = false;
                //this.Visible = false;
                //(Parent as FrmShowProcessPic).Close();

                //BaseForm form;
                //BaseForm tempForm = (BaseForm)this.ParentForm;
                ////唐海林 暂时注释
                //if (tempForm.Text.Equals(LoginInfo.CompanyInfo.COMPANY_NAME_BRIEF + "MES系统"))
                //{
                //    form = tempForm;
                //}
                //else
                //{
                //    form = (BaseForm)tempForm.Owner;
                //    tempForm.Dispose();
                //    form.Hide();
                //}
                Form tempForm = (Form)this.ParentForm;
                tempForm.Hide();

            }
            catch(Exception e1)
            {
                MessageBox.Show("装机图片"+e1.Message,"提示");
            }

            //if (tempForm.Text.Equals("条码打印")) return;
            //FrmBarCode checkForm = new FrmBarCode(form);

            //checkForm.Show(form);
            //form.Hide();
        }

    }
}
