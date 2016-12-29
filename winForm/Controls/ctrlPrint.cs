using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Rmes.WinForm.Base;
using Rmes.Pub.Data1;
using System.IO;
using System.Diagnostics;
using Rmes.DA.Base;

namespace Rmes.WinForm.Controls
{
    public partial class ctrlPrint : BaseControl
    {
        //dataConn dc = new dataConn();
        string userid, username;
        public ctrlPrint()
        {
            InitializeComponent();
            string password = Microsoft.VisualBasic.Interaction.InputBox("请输入密码", "输入密码");
            if (password != "ATPU")
            {
                groupBox1.Visible = false;
                groupBox2.Visible = false;
                timer1.Enabled = false;
                //BaseForm tempForm1 = (BaseForm)this.ParentForm;
                //PublicClass.ClearEvents(tempForm1);
                //Application.Exit();
            }
            else
            {
                dateTimePicker1.Value = DateTime.Now;
                dataGridView1.AutoGenerateColumns = false;
                dataGridView1.RowHeadersVisible = false;
                dataGridView1.DataSource = null;
                show_unprint();
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 13) return;
            string sn = "";
            string sql = "";
            DataTable dt = new DataTable();
            sn = textBox1.Text;
            sql = "SELECT * FROM data_product where sn='" + sn + "' ";//atpuxxjc
            dt = dataConn.GetTable(sql);
            if (dt.Rows.Count < 1)
            {
                MessageBox.Show("该流水号不存在");
                return;
            }
            textBox2.Text=dt.Rows[0]["plan_so"] as string;
            textBox3.Text = dt.Rows[0]["PRODUCT_MODEL"] as string;
            try
            {
                textBox7.Text = dataConn.GetValue("select seq_head||seq from data_PRINT_SEQ where gzdd='" + dt.Rows[0]["PLINE_CODE"].ToString() + "' and stationcode='" + LoginInfo.StationInfo.STATION_NAME + "'");
            }
            catch
            {
                textBox7.Text = "";
            }
            dt = dataConn.GetTable("SELECT bz,pf FROM dmsopfb t WHERE t.so='" + textBox2.Text + "'");
            try
            {
                textBox4.Text = dt.Rows[0][0].ToString().Replace((char)13, ' ');
            }
            catch { textBox4.Text = ""; }
            try
            {
                textBox5.Text = dt.Rows[0][1].ToString().Replace((char)13, ' ');
            }
            catch { textBox5.Text = ""; }

            try
            {
                string sqlhz = "select count(1) from ATPUHGZ where lsh='" + sn + "' ";
                string count1 = dataConn.GetValue(sqlhz);
                if (count1 != "0")
                {
                    MessageBox.Show("此发动机条码已打印" + count1 + "次！", "提示");
                }
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.Message, "提示");
            }
            //textBox6.Text = dt.Rows[0]["PLINE_CODE"] as string;

            //string[] theString = new string[6] {"","","","","" ,""};
            //theString[0] = textBox1.Text;
            //theString[1] = textBox2.Text;
            //theString[2] = textBox3.Text;
            //theString[3] = textBox4.Text;
            //theString[4] = textBox5.Text;
            //theString[5] = textBox6.Text;

        }

        private void btn_print_Click(object sender, EventArgs e)
        {
            string[] theString = new string[6] {"","","","","" ,""};
            theString[0] = textBox1.Text;
            theString[1] = textBox2.Text;
            theString[2] = textBox3.Text;
            theString[3] = textBox4.Text;
            theString[4] = dateTimePicker1.Value.ToString("yyyy-MM-dd");//textBox5.Text;//textBox5报告号取消 改为检验日期
            theString[5] = textBox7.Text;
            string sql = "SELECT * FROM data_product where sn='" + textBox1.Text + "' ";//atpuxxjc
            DataTable dt = dataConn.GetTable(sql);
            string gzdd1 = "";
            try
            {
                gzdd1 = dt.Rows[0]["PLINE_CODE"].ToString();
            }
            catch 
            {
                gzdd1 = LoginInfo.ProductLineInfo.PLINE_CODE;
            }
                
            Print_hgz(theString,gzdd1);
            string SQLSTR = "INSERT INTO ATPUHGZ(LSH,SO,JX,PFDJ,PFBGH,RQ,JYY,XLH)VALUES('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + textBox4.Text + "','" + textBox5.Text + "',sysdate,'" + textBox6.Text.Trim() + "','" + textBox7.Text + "')";
            dataConn.ExeSql(SQLSTR);
            show_unprint();
        }

        private void btn_ok_select_Click(object sender, EventArgs e)
        {
            string sn = "";
            string sql = "";
            timer1.Enabled = false;
            DataTable dt = new DataTable();
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                string _selectValue = dataGridView1.Rows[i].Cells["col1"].EditedFormattedValue.ToString();

                if (_selectValue == "True")//如果被选中则进行操作
                {
                    string[] theString = new string[6] { "","","","","",""};
                    sn = dataGridView1.Rows[i].Cells["colsn"].Value as string;
                    theString[0] = sn;
                    sql = "SELECT * FROM data_product where sn='" + sn + "' ";
                    dt = dataConn.GetTable(sql);
                    if (dt.Rows.Count < 1)
                    {
                        continue;
                    }
                    theString[1] = dt.Rows[0]["PLAN_SO"] as string;
                    theString[2] = dt.Rows[0]["PRODUCT_MODEL"] as string;
                    try
                    {
                        theString[5] = dataConn.GetValue("select seq_head||seq from data_PRINT_SEQ where gzdd='" + dt.Rows[0]["PLINE_CODE"].ToString() + "' and stationcode='" + LoginInfo.StationInfo.STATION_NAME + "'");
                    }
                    catch
                    {
                        theString[5] = "";
                    }
                    string gzdd1 = "";
                    try
                    {
                        gzdd1 = dt.Rows[0]["PLINE_CODE"].ToString();
                    }
                    catch
                    {
                        gzdd1 = LoginInfo.ProductLineInfo.PLINE_CODE;
                    }
                    //dt = dataConn.GetTable("select P.PT_DESC2,P.PT_DESC1 from COPY_PT_MSTR P,VW_QAD_ATPUBOM A WHERE P.Pt_part=A.ABOM_PAR AND A.ABOM_COMP='" + textBox2.Text + "'");
                    dt = dataConn.GetTable("SELECT bz,pf FROM dmsopfb t WHERE t.so='" + theString[1] + "'");
                    try
                    {
                        theString[3] = dt.Rows[0][0].ToString().Replace((char)13, ' ');
                    }
                    catch { }
                    try
                    {
                        theString[4] = dateTimePicker1.Value.ToString("yyyy-MM-dd");// DateTime.Now.ToString("yyyy-MM-dd");//dt.Rows[0][1].ToString().Replace((char)13, ' ');
                    }
                    catch { }
                    //theString[5]=dataConn.GetValue("select seq from data_PRINT_SEQ");

                    //theString[3] = dt.Rows[0][0] as string;
                    //theString[4] = dt.Rows[0][1] as string;
                    //theString[5] = "";
                    Print_hgz(theString, gzdd1);
                    string SQLSTR = "INSERT INTO ATPUHGZ(LSH,SO,JX,PFDJ,PFBGH,RQ,JYY,XLH)VALUES('" + theString[0] + "','" + theString[1] + "','" + theString[2] + "','" + theString[3] + "','" + theString[4] + "',sysdate,'','" + theString[5] + "')";
                    dataConn.ExeSql(SQLSTR);
                }
            }
            timer1.Enabled = true;
            show_unprint();
        }
        public void Print_hgz(string[] theString,string gzdd1)
        {
            StreamWriter Ts1;
            string TheFileName = "C:\\tmdy.txt";
            if (File.Exists(TheFileName))
            {
                File.Delete(TheFileName);
            }
            //Ts1 = File.CreateText(TheFileName);
            Ts1 = new StreamWriter(TheFileName, false, Encoding.GetEncoding("gb2312"));
            //Ts1.WriteLine("合格证");
            //Ts1.WriteLine("'流水号：  " + theString[0] + "'");
            //Ts1.WriteLine("'订单号：" + theString[1] + "'");
            //Ts1.WriteLine("'机型：" + theString[2] + "'");
            //Ts1.WriteLine("'排放报告号：" + theString[4] + "'");
            //Ts1.WriteLine("'检验日期" + DateTime.Now.ToString() + "'");
            //Ts1.WriteLine("'检验员：" + username + "'");
            //Ts1.WriteLine("'排放等级" + theString[3] + "'");
            string YY = "", MM = "", DD = "";
            YY = ((char)(Convert.ToInt32(DateTime.Now.Year.ToString().Substring(3).ToString()) + 64)).ToString();
            MM = ((char)(Convert.ToInt32(DateTime.Now.Month.ToString()) + 64)).ToString();
            if (DateTime.Now.Day <= 26)
            {
                DD = ((char)(DateTime.Now.Day + 64)).ToString();
            }
            else
            {
                if (DateTime.Now.Day == 27)
                {
                    DD = "AA";
                }
                if (DateTime.Now.Day == 28)
                {
                    DD = "AB";
                }
                if (DateTime.Now.Day == 29)
                {
                    DD = "AC";
                }
                if (DateTime.Now.Day == 30)
                {
                    DD = "AD";
                }
                if (DateTime.Now.Day == 31)
                {
                    DD = "AE";
                }
            }
            Ts1.WriteLine("TM");
            Ts1.WriteLine(theString[0] + "," + theString[1] + "," + theString[2] + "," + theString[3] + ",," + theString[4].Substring(0, 4) + "-" + theString[4].Substring(5, 2) + "-" + theString[4].Substring(8, 2) + "," + YY + "-" + MM + "-" + DD + "A");
            Ts1.Close();
            if (!Directory.Exists("C:\\Program Files\\lmw32\\"))
            {
                Directory.CreateDirectory("C:\\Program Files\\lmw32\\");
            }
            string path1 = "C:\\Program Files\\lmw32";
            string run_exe = "LMWPRINT";
            string theExeStr1 = "/L=C:\\gttm.qdf ";

            System.Diagnostics.ProcessStartInfo p = null;
            System.Diagnostics.Process Proc;
            p = new ProcessStartInfo(run_exe, theExeStr1);
            p.WorkingDirectory = path1;//设置此外部程序所在windows目录
            p.WindowStyle = ProcessWindowStyle.Hidden;//在调用外部exe程序的时候，控制台窗口不弹出

            Proc = System.Diagnostics.Process.Start(p);//调用外部程序
            //string path1 = "C:\\Program Files\\lmw32";
            //string run_exe = "LMWPRINT";
            //string theExeStr1 = path1 + "LMWPRINT /L=C:\\gttm.QDF";
            //System.Diagnostics.ProcessStartInfo p = null;
            //System.Diagnostics.Process Proc;
            //p = new ProcessStartInfo(run_exe, theExeStr1);
            //p.WorkingDirectory = path1;//设置此外部程序所在windows目录
            //p.WindowStyle = ProcessWindowStyle.Hidden;//在调用外部exe程序的时候，控制台窗口不弹出

            //Proc = System.Diagnostics.Process.Start(p);//调用外部程序
            dataConn.ExeSql("UPDATE DATA_PRINT_SEQ SET SEQ=SEQ+1 where gzdd='" + gzdd1 + "' and stationcode='"+LoginInfo.StationInfo.STATION_NAME+"'  ");
        }
        public static int Asc(string character)
        {
            if (character.Length == 1)
            {
                System.Text.ASCIIEncoding asciiEncoding = new System.Text.ASCIIEncoding();
                int intAsciiCode = (int)asciiEncoding.GetBytes(character)[0];
                return (intAsciiCode);
            }
            else
            {
                throw new Exception("Character is not valid.");
            }

        }

        private void btn_readtext_Click(object sender, EventArgs e)
        {
            //string FilePath1 = "d:\\DT900\\UP\\ruku.txt";
            //if (File.Exists(FilePath1))
            //    return;
            //string[] filelist = File.ReadAllLines(FilePath1, Encoding.Default);
            //DataTable dt = new DataTable();
            //dt.Columns.Add("SN");
            //for (int i = filelist.Length - 1; i >= 0; i--) //倒序读取文本
            //{
            //    if (filelist.Length > 10)
            //    {
            //        string m_Ghtm = filelist[i].Substring(0, 8);
            //        dt.Rows.Add(m_Ghtm);
            //    }
            //}
            //dataGridView2.DataSource = dt;
            //string File.ReadAllText
        }

        private void btn_PTxt_Click(object sender, EventArgs e)
        {
            //string sn = "";
            //string sql = "";
            //DataTable dt = new DataTable();
            //for (int i = 0; i < dataGridView2.Rows.Count; i++)
            //{
            //    sn = dataGridView2.Rows[i].Cells[0].Value as string;
            //    sql = "SELECT * FROM data_product where sn='" + sn + "'";
            //    dt = dataConn.GetTable(sql);
            //    if (dt.Rows.Count < 1)
            //    {
            //         for (int j = 0; j < dataGridView2.Columns.Count; j++)
            //                dataGridView2.Rows[i].Cells[j].Style.BackColor = Color.Red;
            //        //MessageBox.Show("");   用颜色标识可以不提示
            //         continue;
            //    }
            //    textBox2.Text = dt.Rows[0]["plan_so"] as string;
            //    textBox3.Text = dt.Rows[0]["PRODUCT_MODEL"] as string;
            //    try
            //    {
            //        textBox7.Text = dataConn.GetValue("select seq_head||seq from data_PRINT_SEQ where gzdd='" + dt.Rows[0]["PLINE_CODE"].ToString() + "' ");
            //    }
            //    catch
            //    {
            //        textBox7.Text = "";
            //    }
            //    string gzdd1=dt.Rows[0]["PLINE_CODE"].ToString();
            //    dt = dataConn.GetTable("SELECT bz,pf FROM dmsopfb t WHERE t.so='" + textBox2.Text + "'");
            //    textBox4.Text = dt.Rows[0][0].ToString().Replace((char)13, ' ');
            //    textBox5.Text = dt.Rows[0][1].ToString().Replace((char)13, ' ');
            //    //textBox7.Text = dataConn.GetValue("select seq from data_PRINT_SEQ");

            //    //dt = dataConn.GetTable("select P.PT_DESC2,P.PT_DESC1 from COPY_PT_MSTR P,VW_QAD_ATPUBOM A WHERE P.Pt_part=A.ABOM_PAR AND A.ABOM_COMP='" + textBox2.Text + "'");
            //    //textBox4.Text = dt.Rows[0][0] as string;
            //    //textBox5.Text = dt.Rows[0][1] as string;

            //    string[] theString = new string[6] { "", "", "", "", "", "" };
            //    theString[0] = textBox1.Text;
            //    theString[1] = textBox2.Text;
            //    theString[2] = textBox3.Text;
            //    theString[3] = textBox4.Text;
            //    theString[4] = textBox5.Text;
            //    theString[5] = textBox7.Text;
            //    Print_hgz(theString,gzdd1);
            //}
            //show_unprint();
        }

        private void btn_printed_Click(object sender, EventArgs e)
        {
            //新增一个form  显示打印记录
            //select * from atpuhgz where lsh='" + txtLsh.Text + "'
            FrmShowPrinted Printed = new FrmShowPrinted();
            Printed.Show();
        }


        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            string sn = "";
            string sql = "";
            DataTable dt = new DataTable();
            if (checkBox1.Checked)
            {
                timer1.Enabled = false;
                timer1.Stop();
                timer2.Start();
                timer2.Enabled = true;
            }
            else
            {
                timer1.Start();
                timer1.Enabled = true;
                timer2.Stop();
                timer2.Enabled = false;
                show_unprint();
            }
        }
        void show_unprint()
        {

            string sql = "select lsh,rqsj,jhso,gzdd from atpuxxjc T where not exists (SELECT A.LSH FROM ATPUHGZ A  ) AND gzdd='" + LoginInfo.ProductLineInfo.PLINE_CODE + "' and jhso like '%" + textBox8.Text.Trim().ToUpper() + "%' and rqsj>sysdate-15 order by jhso,rqsj";
            dataGridView1.DataSource = dataConn.GetTable(sql);
        }

        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            string sn = dataGridView1.Rows[e.RowIndex].Cells["colsn"].Value.ToString();
            textBox1.Text=sn;
            textBox1_KeyPress(this,new KeyPressEventArgs((char)13));
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {
        
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string sn = "";
            string sql = "";
            DataTable dt = new DataTable();
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                string _selectValue = dataGridView1.Rows[i].Cells["col1"].EditedFormattedValue.ToString();

                if (_selectValue == "True")//如果被选中则进行操作
                {
                    string[] theString = new string[6] { "", "", "", "", "", "" };
                    sn = dataGridView1.Rows[i].Cells["colsn"].Value as string;
                    //sql = "SELECT * FROM data_product where sn='" + sn + "' ";
                    //dt = dataConn.GetTable(sql);
                    //if (dt.Rows.Count < 1)
                    //{
                    //    MessageBox.Show("");
                    //    return;
                    //}
                    //theString[1] = dt.Rows[0]["PLAN_SO"] as string;
                    //theString[2] = dt.Rows[0]["PRODUCT_MODEL"] as string;
                    ////dt = dataConn.GetTable("select P.PT_DESC2,P.PT_DESC1 from COPY_PT_MSTR P,VW_QAD_ATPUBOM A WHERE P.Pt_part=A.ABOM_PAR AND A.ABOM_COMP='" + textBox2.Text + "'");
                    //dt = dataConn.GetTable("SELECT bz,pf FROM dmsopfb t WHERE t.so='" + theString[1] + "'");
                    //theString[3] = dt.Rows[0][0].ToString().Replace((char)13, ' ');
                    //theString[4] = dt.Rows[0][1].ToString().Replace((char)13, ' ');
                    //theString[5] = dataConn.GetValue("select seq from data_PRINT_SEQ");
                    string SQLSTR = "INSERT INTO ATPUHGZ(LSH,SO,JX,PFDJ,PFBGH,RQ,JYY,XLH)VALUES('" + sn + "','','','','',sysdate,'删除','')";
                    dataConn.ExeSql(SQLSTR);
                }
            }
            show_unprint();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            show_unprint();
        }

        private void textBox8_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 13) return;
            string so1 = textBox8.Text.Trim().ToUpper();
            string sql = "select lsh,rqsj,jhso,gzdd from atpuxxjc T where T.LSH NOT IN(SELECT A.LSH FROM ATPUHGZ A  ) AND gzdd='" + LoginInfo.ProductLineInfo.PLINE_CODE + "' and jhso like '%" + so1 + "%' order by jhso,rqsj";
            dataGridView1.DataSource = dataConn.GetTable(sql);
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            //for (int i = 0; i < dataGridView1.Rows.Count; i++)
            int i = 0;
            {
                string[] theString = new string[6] { "", "", "", "", "", "" };
                string sn = dataGridView1.Rows[i].Cells["colsn"].Value as string;
                theString[0] = sn;
                string sql = "SELECT * FROM data_product where sn='" + sn + "' ";
                DataTable dt = dataConn.GetTable(sql);
                if (dt.Rows.Count < 1)
                {
                    string SQLSTR = "INSERT INTO ATPUHGZ(LSH,SO,JX,PFDJ,PFBGH,RQ,JYY,XLH)VALUES('" + sn + "','','','','',sysdate,'','')";
                    dataConn.ExeSql(SQLSTR);
                    return;
                }
                theString[1] = dt.Rows[0]["PLAN_SO"] as string;
                theString[2] = dt.Rows[0]["PRODUCT_MODEL"] as string;
                try
                {
                    theString[5] = dataConn.GetValue("select seq_head||seq from data_PRINT_SEQ where gzdd='" + dt.Rows[0]["PLINE_CODE"].ToString() + "' and stationcode='" + LoginInfo.StationInfo.STATION_NAME + "'");
                }
                catch
                {
                    theString[5] = "";
                }
                string gzdd1 = "";
                try
                {
                    gzdd1 = dt.Rows[0]["PLINE_CODE"].ToString();
                }
                catch
                {
                    gzdd1 = LoginInfo.ProductLineInfo.PLINE_CODE;
                }
                //dt = dataConn.GetTable("select P.PT_DESC2,P.PT_DESC1 from COPY_PT_MSTR P,VW_QAD_ATPUBOM A WHERE P.Pt_part=A.ABOM_PAR AND A.ABOM_COMP='" + textBox2.Text + "'");
                //theString[3] = dt.Rows[0][0] as string;
                //theString[4] = dt.Rows[0][1] as string;
                dt = dataConn.GetTable("SELECT bz,pf FROM dmsopfb t WHERE t.so='" + theString[1] + "'");
                try
                {
                    theString[3] = dt.Rows[0][0].ToString().Replace((char)13, ' ');
                }
                catch { }
                string bgh = "";
                try
                {
                    bgh=dt.Rows[0][1].ToString().Replace((char)13, ' ');
                }
                catch { }
                theString[4] = dateTimePicker1.Value.ToString("yyyy-MM-dd");// DateTime.Now.ToString("yyyy-MM-dd");
                Print_hgz(theString, gzdd1);
                string SQLSTR1 = "INSERT INTO ATPUHGZ(LSH,SO,JX,PFDJ,PFBGH,RQ,JYY,XLH)VALUES('" + theString[0] + "','" + theString[1] + "','" + theString[2] + "','" + theString[3] + "','" + bgh + "',sysdate,'','" + theString[5] + "')";
                dataConn.ExeSql(SQLSTR1);
                show_unprint();
            }

        }

    }
}
