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

namespace Rmes.WinForm.Controls
{
    public partial class ctrlProcessNote : BaseControl
    {
        //装机提示
        string company_code, pline_code, plan_code, process_code,stationcode;
        //dataConn dc = new dataConn();
        public ctrlProcessNote(string PlanCode,string ProcessCode)
        {
            InitializeComponent();
            company_code = LoginInfo.CompanyInfo.COMPANY_CODE;
            pline_code = LoginInfo.ProductLineInfo.PLINE_CODE;
            stationcode = LoginInfo.StationInfo.STATION_CODE;
            plan_code = PlanCode;
            process_code = ProcessCode;
            string so1 = "",fdjxl1="";
            try
            {
                so1 = dataConn.GetValue("select plan_so from data_plan where plan_code='" + plan_code + "' and rownum=1 ");
            }
            catch { }
            {
                SNBomTempFactory.ShowBomZJTS(company_code, plan_code, pline_code, stationcode, "", fdjxl1, so1, stationcode);

                richTextBox1.Text = "";
                string sql = "select gwdm,czts,nvl(note_color,''),nvl(note_font,'') from rstbomts where zddm='" + stationcode + "' order by gwdm,czts";
                DataTable dt = dataConn.GetTable(sql);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    try
                    {
                        string note = "*" + dt.Rows[i][1].ToString().Replace((char)13, ' ').Trim();
                        string notecolor = dt.Rows[i][2].ToString();
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
                        //richTextBox1.Text = richTextBox1.Text + note + (char)13 + (char)10;
                    }
                    catch
                    {
                        string note = dt.Rows[i][1].ToString();
                        richTextBox1.AppendText("*" + note + (char)13 + (char)10);
                    }
                }
            }
            //InitProcessNote();
        }
        public ctrlProcessNote()
        {
            InitializeComponent();
            company_code = LoginInfo.CompanyInfo.COMPANY_CODE;
            pline_code = LoginInfo.ProductLineInfo.PLINE_CODE;
            stationcode = LoginInfo.StationInfo.STATION_CODE;
            //InitProcessNote();
            this.RMesDataChanged += new Rmes.WinForm.Base.RMesEventHandler(ctrlShowZjts_RmesDataChanged);
        }
        void ctrlShowZjts_RmesDataChanged(object obj, Rmes.WinForm.Base.RMESEventArgs e)
        {
            if (e.MessageHead == null) return;
            if (e.MessageHead == "SHOWZJTS")
            {
                string item_info = e.MessageBody.ToString();//消息体是sn^stationcode^stationname
                string[] cmd_info = item_info.Split('^');
                string SN1 = cmd_info[0];
                string stationcode_fx = cmd_info[1];
                string stationname_fx = cmd_info[2];

                richTextBox1.Text = "";
                string sql = "select gwdm,czts,nvl(note_color,''),nvl(note_font,'') from rstbomts where zddm='" + stationcode_fx + "' order by gwdm,czts";
                DataTable dt=dataConn.GetTable(sql);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    try
                    {
                        string note = "*" + dt.Rows[i][1].ToString().Replace((char)13, ' ').Trim(); 
                        string notecolor = dt.Rows[i][2].ToString();
                        richTextBox1.AppendText(note + (char)13+(char)10);

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
                        //richTextBox1.Text = richTextBox1.Text + note + (char)13 + (char)10;
                    }
                    catch
                    {
                        string note = dt.Rows[i][1].ToString();
                        richTextBox1.AppendText("*" + note + (char)13 + (char)10);
                    }
                }
            }
            if (e.MessageHead == "ZJTSADD")
            {
                richTextBox1.Text = e.MessageBody.ToString() + (char)13 + (char)10 + richTextBox1.Text;
            }
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
