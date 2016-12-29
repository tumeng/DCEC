using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Rmes.DA.Base;
using Rmes.DA.Entity;
using Rmes.DA.Dal;
using Rmes.DA.Factory;
using Rmes.WinForm.Base;
using Rmes.Pub.Data1;
using System.Collections;
using System.IO;
using System.Diagnostics;

namespace Rmes.WinForm.Controls
{
    //质量警示模块 thl 20160823
    public partial class ctrlProcessQuaNote : BaseControl
    {
        string company_code, pline_code, plan_code, process_code, stationcode;
        //dataConn dc = new dataConn();
        private string wjlj = "";
        private ArrayList wjpath = new ArrayList();
        public ctrlProcessQuaNote()
        {
            InitializeComponent();
            company_code = LoginInfo.CompanyInfo.COMPANY_CODE;
            pline_code = LoginInfo.ProductLineInfo.PLINE_CODE;
            stationcode = LoginInfo.StationInfo.STATION_CODE;
            //InitProcessNote();
            wjlj = dataConn.GetValue("select INTERNAL_VALUE FROM CODE_INTERNAL WHERE COMPANY_CODE='" + company_code
                        + "' AND INTERNAL_TYPE_CODE='PATH' AND INTERNAL_CODE='QUALITYALERTPATH'");
            this.RMesDataChanged += new Rmes.WinForm.Base.RMesEventHandler(ctrlShowQuaZjts_RmesDataChanged);
        }
        void ctrlShowQuaZjts_RmesDataChanged(object obj, Rmes.WinForm.Base.RMESEventArgs e)
        {
            if (e.MessageHead == null) return;
            if (e.MessageHead == "SHOWQUATS") //质量警示
            {
                richTextBox1.Text = "";
                string sql = "select gwdm,czts,nvl(note_color,''),nvl(note_font,''),wjpath from rstbomquats where zddm='" + stationcode + "' order by gwdm";
                DataTable dt = dataConn.GetTable(sql);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    try
                    {
                        string note = dt.Rows[i][1].ToString();
                        string notecolor = dt.Rows[i][2].ToString();
                        string wjpath1 = dt.Rows[i][4].ToString();
                        string[] s = wjpath1.Split('$');
                        for (int j = 0; j < s.Length; j++)
                        {
                            string path1 = wjlj + "\\" + pline_code + "\\" + s[j];
                            if (File.Exists(path1))
                            {
                                if (!wjpath.Contains(path1))
                                {
                                    wjpath.Add(path1);
                                }
                            }
                        }

                        richTextBox1.AppendText(note + (char)13 + (char)10);

                        if (notecolor != "")
                        {
                            Color fontColor = System.Drawing.ColorTranslator.FromHtml(notecolor);
                            int p1 = richTextBox1.Text.IndexOf(note);
                            int p2 = note.Length;
                            //richTextBox1.ForeColor = fontColor;
                            richTextBox1.Select(richTextBox1.Text.Length - p2 - 1, p2);
                            richTextBox1.SelectionColor = fontColor;
                        }

                        string notefont = dt.Rows[i][3].ToString();
                        if (notefont != "")
                        {
                            FontFamily myFontFamily = new FontFamily("微软雅黑"); //采用哪种字体
                            int font1 = Convert.ToInt32(notefont);
                            Font myFont = new Font(myFontFamily, font1, FontStyle.Regular); //字是那种字体（幼圆），显示的风格（粗体）
                            int p1 = richTextBox1.Text.IndexOf(note);
                            int p2 = note.Length;
                            richTextBox1.Select(richTextBox1.Text.Length - p2 - 1, p2);
                            richTextBox1.SelectionFont = myFont;
                        }
                        //for (int k = 0; k < wjpath.Count; k++)
                        //{
                        //    LinkLabel link1 = new LinkLabel();
                        //    link1.Text = wjpath[k].ToString();
                        //    link1.LinkClicked+= new LinkLabelLinkClickedEventHandler(link1_LinkClicked);
                        //    this.richTextBox1.Controls.Add(link1);
                        //}
                        for (int k = 0; k < wjpath.Count; k++)
                        {
                            int w = richTextBox1.Text.Length;
                            richTextBox1.AppendText(wjpath[k].ToString());
                            this.richTextBox1.Select(w, wjpath[k].ToString().Length);
                            richTextBox1.InsertLink(wjpath[k].ToString());
                            richTextBox1.SelectedText = " ";//+(char)13
                            if (k + 1 == wjpath.Count)
                            {
                                richTextBox1.SelectedText = "" + (char)13;
                            }
                        }
                        //richTextBox1.Text = richTextBox1.Text + note + (char)13 + (char)10;
                    }
                    catch
                    {
                        string note = dt.Rows[i][1].ToString();
                        richTextBox1.AppendText(note + (char)13 + (char)10);
                    }
                }
                richTextBox1.Select(0,0);
                richTextBox1.ScrollToCaret();
            }
            if (e.MessageHead == "ZJTSADD")
            {
                richTextBox1.Text = e.MessageBody.ToString() + (char)13 + (char)10 + richTextBox1.Text;
            }
        }
        private void richTextBox1_LinkClicked(object sender, System.Windows.Forms.LinkClickedEventArgs e)
        {
            //MessageBox.Show("A link has been clicked.\nThe link text is '" + e.LinkText + "'");
            try
            {
                Process.Start(e.LinkText.Replace("\r",""));
            }
            catch(Exception e1)
            { }
        }
        private void link1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(((LinkLabel)sender).Text);
        }

        private void InitProcessNote()
        {
            //string process_note = "", plan_process_note="";
            //PlanEntity ent=PlanFactory.GetByKey(plan_code);
            //if (ent.PRODUCT_SERIES == null)
            //{
            //    MessageBox.Show("此计划工序没有定义相应的工序指导文件");
            //    return;
            //}
            //string product_series=PlanFactory.GetByKey(plan_code).PRODUCT_SERIES;
            //List<ProcessNoteEntity> lst1 = ProcessNoteFactory.GetByProcessProduct(LoginInfo.CompanyInfo.COMPANY_CODE,
            //                                                        LoginInfo.ProductLineInfo.RMES_ID,process_code, product_series);
            //List<PlanProcessNoteEntity> lst2 = PlanProcessNoteFactory.GetByProcessProduct
            //                                                        (LoginInfo.CompanyInfo.COMPANY_CODE,
            //                                                        LoginInfo.ProductLineInfo.RMES_ID,
            //                                                        process_code, product_series, plan_code);


            //if (lst1!=null && lst1.Count > 0)
            //    process_note = lst1.First<ProcessNoteEntity>().PROCESS_NOTE+"\n";
            //if (lst2 != null && lst2.Count > 0) 
            //    plan_process_note = lst2.First<PlanProcessNoteEntity>().PROCESS_NOTE;
            //string note = process_note + plan_process_note;

            //richTextBox1.Text = note;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //(Parent as FrmShowProcessNote).Close();
        }

    }
}