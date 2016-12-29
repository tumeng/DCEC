using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Rmes.WinForm.Base;
using Rmes.DA.Base;
using Rmes.DA.Entity;
using Rmes.DA.Factory;
using Rmes.Pub.Data1;
using System.IO;
using System.Diagnostics;
using System.Data.OleDb;
using System.Reflection;

namespace Rmes.WinForm.Controls
{
    public partial class ctrl_tlz : BaseControl
    {
        //凸轮轴条形码打印
        private string CompanyCode, PlineCode, PlineID, StationCode, stationname, TheSo, TheSn, ThePlancode,TheZdmc="",TheGys="";
        //dataConn dc = new dataConn();
        ProductInfoEntity product;
        private bool ifDone = true;
        private string thelj = "";
        private string thePch = "";
        private string theGys = "";
        private OleDbConnection _oleDbConn;
        private OleDbDataAdapter _oleDbAda;
        //public readonly static string _strdata = string.Format("{0}{1}{2}", "provider=microsoft.jet.oledb.4.0; Data Source=", System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"\db\StuManData.mdb");
        public readonly static string _strConn = @"provider=microsoft.jet.oledb.4.0; Data Source=C:\tmdy_log.mdb";
        public ctrl_tlz()
        {
            InitializeComponent();
            CompanyCode = LoginInfo.CompanyInfo.COMPANY_CODE;
            PlineID = LoginInfo.ProductLineInfo.RMES_ID;
            PlineCode = LoginInfo.ProductLineInfo.PLINE_CODE;
            StationCode = LoginInfo.StationInfo.RMES_ID;
            stationname = LoginInfo.StationInfo.STATION_NAME;
            txtPch.Text = "";
            TxtGys.Text = "";
            string theFile = "C:\\lgljxxb.txt";

            if (!File.Exists(theFile))
            {
                File.CreateText(theFile);
            }
            string filelist = File.ReadAllText(theFile, Encoding.Default);
            string ljb = filelist.Substring(filelist.IndexOf(':')+1);
            string[] ljh = ljb.Split(';');
            ComBoLj.Items.Clear();
            DataTable dt = new DataTable();
            dt.Columns.Add("name");
            dt.Columns.Add("value");
            for (int i = 0; i < ljh.Length; i++)
            {
                if (ljh[i] != "")
                {
                    DataRow dr = dt.NewRow();
                    dr[0] = ljh[i];
                    dr[1] = i.ToString();
                    dt.Rows.Add(dr);
                }
            }
            ComBoLj.DataSource = dt;
            ComBoLj.DisplayMember = "name";
            ComBoLj.ValueMember = "value";

            theFile = "C:\\xgxxb.txt";
            if (!File.Exists(theFile))
            {
                File.CreateText(theFile);
            }
            filelist = File.ReadAllText(theFile, Encoding.Default);
            TheZdmc = filelist;
            TheGys = "DCEC";
            TxtGys.Text = TheGys;
            label1.Text = TheZdmc + "条码打印";
            ifDone = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            BaseForm tempForm1 = (BaseForm)this.ParentForm;
            PublicClass.ClearEvents(tempForm1);
            Application.Exit();
        }

        private void ComBoLj_Click(object sender, EventArgs e)
        {
            //GetFocused(txtPch);
        }
        private void GetFocused(TextBox textBox)
        {
            textBox.Focus();
            textBox.Select(0, textBox.Text.Length);
        }

        private void txtPch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 13) return;
            if (string.IsNullOrWhiteSpace(txtPch.Text)) return;
            thelj = ComBoLj.Text.Trim();
            thePch = txtPch.Text.ToUpper().Trim();
            txtPch.Text = thePch;
            theGys = TxtGys.Text.Trim();
            if (thelj == "" || thePch == "" || theGys == "")
            {
                MessageBox.Show("各项内容不能为空","提示");
                return;
            }
            if (!ifDone)
            {
                MessageBox.Show("打印忙，请稍后再试","提示");
                return;
            }
            ifDone = false;
            Print_Tm();
            ifDone = true;
            GetFocused(txtPch);
        }
        private void Print_Tm()
        {
            try
            {
                string TheTmlb = "凸轮轴";
                //access数据库数据检查录入
                _oleDbConn = new OleDbConnection(_strConn);//根据链接信息实例化链接对象
                _oleDbConn.Open();//打开连接;
                string strsql = "select * from tmdy_log where pch='" + thePch + "' and ljh='" + thelj + "'";
                OleDbCommand mycom = new OleDbCommand(strsql, _oleDbConn);
                OleDbDataReader myReader = null;
                myReader = mycom.ExecuteReader();
                if (myReader.HasRows)
                {
                    DialogResult drt = MessageBox.Show("该条码已经打印，是否重新打印!", "提示", MessageBoxButtons.YesNo);
                    if (drt == DialogResult.No) return;
                }
                else
                {
                    strsql = "insert into tmdy_log(pch,gys,bc,ljh,gzrq,zdmc,tmlb)values('" + thePch + "','" + TheGys + "','" + LoginInfo.ShiftInfo.SHIFT_CODE + "','" + thelj + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + TheZdmc + "','" + TheTmlb + "')";
                    mycom = new OleDbCommand(strsql, _oleDbConn);
                    mycom.ExecuteNonQuery();
                }

                //_oleDbAda = new OleDbDataAdapter(strsql, _oleDbConn);//strsql sql语句；

                //DataSet myds = new DataSet();
                //_oleDbAda.Fill(myds, "tmdy_log");

                myReader.Close();
                _oleDbConn.Close();
                StreamWriter Ts1;
                string TheFileName = "c:\\tmdy.txt";
                if (File.Exists(TheFileName))
                {
                    File.Delete(TheFileName);
                }
                Ts1 = File.CreateText(TheFileName);
                Ts1.WriteLine("TM");
                Ts1.WriteLine(TheZdmc + ",'" + thePch + "','" + thelj + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + thelj + "^" + theGys+"^"+thePch+"','");
                Ts1.Close();
                if (!Directory.Exists("C:\\Program Files\\lmw32\\"))
                {
                    Directory.CreateDirectory("C:\\Program Files\\lmw32\\");
                }
                string path1 = "C:\\Program Files\\lmw32";
                string run_exe = "LMWPRINT";
                string theExeStr1 = " /L=C:\\gttm.QDF";
                System.Diagnostics.ProcessStartInfo p = null;
                System.Diagnostics.Process Proc;
                p = new ProcessStartInfo(run_exe, theExeStr1);
                p.WorkingDirectory = path1;//设置此外部程序所在windows目录
                p.WindowStyle = ProcessWindowStyle.Hidden;//在调用外部exe程序的时候，控制台窗口不弹出

                Proc = System.Diagnostics.Process.Start(p);//调用外部程序
                txtPch.Text = "";
            }
            catch (Exception e1)
            {
                MessageBox.Show("请确认Label Matrix的目录,数量是否正确!", "错误提示");
            }
        }


    }
}
