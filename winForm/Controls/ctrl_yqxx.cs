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
using System.Reflection;
using System.Diagnostics;
using Rmes.Pub.Data1;
using System.IO;
namespace Rmes.WinForm.Controls
{
    public partial class ctrl_yqxx : BaseControl
    {
        private string CompanyCode, PlineID, PlineCode1, StationID, len_sn, TheSo, TheSn, ThePlancode, StationName, Station_Code;
        private string ShiftCode, TeamCode, UserID;
        ProductInfoEntity product;
        //dataConn dc = new dataConn();
        //东区油漆下线条码打印
        public ctrl_yqxx()
        {
            InitializeComponent();
            CompanyCode = LoginInfo.CompanyInfo.COMPANY_CODE;
            PlineID = LoginInfo.ProductLineInfo.RMES_ID;
            PlineCode1 = LoginInfo.ProductLineInfo.PLINE_CODE;
            StationID = LoginInfo.StationInfo.RMES_ID;
            Station_Code = LoginInfo.StationInfo.STATION_CODE;
            StationName = LoginInfo.StationInfo.STATION_NAME;
            ShiftCode = LoginInfo.ShiftInfo.SHIFT_CODE;
            TeamCode = LoginInfo.TeamInfo.TEAM_CODE;
            UserID = LoginInfo.UserInfo.USER_ID;
            len_sn = LoginInfo.LEN_SN;
            TheSo = "";
            TheSn = "";
            ThePlancode = "";
            TxtLsh.Text = "";
            TxtLsh.Enabled = false;
            ListJjdy.Items.Clear();
            ListYjdy.Items.Clear();
        }

        private void cmdDyms_Click(object sender, EventArgs e)
        {
            txtSl.Visible = true;
        }

        private void CmdQkDy_Click(object sender, EventArgs e)
        {
            ListYjdy.DataSource = null;
        }

        private void CmdTc_Click(object sender, EventArgs e)
        {
            BaseForm tempForm1 = (BaseForm)this.ParentForm;
            PublicClass.ClearEvents(tempForm1);
            Application.Exit();
        }

        private void CmdShx_Click(object sender, EventArgs e)
        {
            //刷新
            Init_ListJjdy();
            Init_ListYjdy();
        }
        private void Init_ListJjdy()
        {
            try
            {
                string sql = "select lsh||'--'||plan_so SHOW from atpudqyqdyb,data_product where pflag='0' and lsh=sn order by ptime";
                DataTable dt = dataConn.GetTable(sql);
                if (dt.Rows.Count == 0)
                {
                    //MessageBox.Show("当前没有可供打印的条码","提示");
                    TxtLsh.Text = "";
                    ListJjdy.DataSource=null;
                    return;
                }
                //ListJjdy.Items.Clear();
                ListJjdy.DataSource = dt;
                ListJjdy.ValueMember = "SHOW";
                ListJjdy.DisplayMember = "SHOW";
                string str = dt.Rows[0][0].ToString();
                TxtLsh.Text = str.Substring(0, str.IndexOf("--"));
            }
            catch { }
        }
        private void Init_ListYjdy()
        {
            try
            {
                string sql = "select lsh||'--'||plan_so SHOW from atpudqyqdyb,data_product where pflag='1' and to_char(ptime,'yyyymmdd')=to_char(sysdate,'yyyymmdd') and lsh=sn order by ptime ";
                DataTable dt = dataConn.GetTable(sql);
                //ListYjdy.Items.Clear();
                ListYjdy.DataSource = dt;
                ListYjdy.ValueMember = "SHOW";
                ListYjdy.DisplayMember = "SHOW";
            }
            catch
            { }
        }

        private void CmdSxdy_Click(object sender, EventArgs e)
        {
            string Lsh = TxtLsh.Text.Trim().ToUpper();
            TxtLsh.Text = Lsh;
            if (Lsh.Length == 0)
            {
                MessageBox.Show("没有条码可供打印","提示");
                TxtLsh.Text = "";
                ListJjdy.DataSource = null;
                return;
            }
            string mySo = "", myJx = "", myJh = "", myYh = "", myBz = "",myYhdm="";
            string sql = "select * from data_product where sn='"+Lsh+"'";
            DataTable dt = dataConn.GetTable(sql);
            if (dt.Rows.Count > 0)
            {
                mySo = dt.Rows[0]["PLAN_SO"].ToString();
                myJx = dt.Rows[0]["PRODUCT_MODEL"].ToString();
                myJh = dt.Rows[0]["PLAN_CODE"].ToString();
            }
            sql = "select * from data_plan where plan_code='" + myJh + "' and plan_so='" + mySo + "' ";
            dt = dataConn.GetTable(sql);
            if (dt.Rows.Count > 0)
            {
                myYh = dt.Rows[0]["CUSTOMER_NAME"].ToString();
                myBz = dt.Rows[0]["REMARK"].ToString();
            }
            sql = "select * from nameplate_so where bzso='" + mySo + "'";
            dt = dataConn.GetTable(sql);
            if (dt.Rows.Count > 0)
            {
                myYhdm = dt.Rows[0]["KHH"].ToString();
            }
            int sl;
            try
            { sl = Convert.ToInt32(txtSl.Text); }
            catch
            {
                MessageBox.Show("数量不合法","提示");
                sl = 0;
                return;
            }
            PrintTmFz(Lsh, mySo, myYh, myYhdm, myJx, Convert.ToInt32(txtSl.Text));
            sql = "update atpudqyqdyb set pflag='1',ptime=sysdate where lsh='" + Lsh + "'";
            dataConn.ExeSql(sql);
            Init_ListJjdy();
            Init_ListYjdy();

        }

        private void PrintTmFz(string Thislsh, string ThisSo, string ThisYh, string ThisYhdm, string ThisJx, int ThisSl)
        {
            try
            {
                StreamWriter Ts1;
                string TheFileName = "d:\\dymb.txt";
                if (File.Exists(TheFileName))
                {
                    File.Delete(TheFileName);
                }
                Ts1 = new StreamWriter(TheFileName, false, Encoding.GetEncoding("gb2312"));
                //Ts1 = File.CreateText(TheFileName);
                Ts1.WriteLine("TM");
                for (int i = 0; i < ThisSl; i++)
                {
                    Ts1.WriteLine(Thislsh + "," + ThisSo + "," + ThisYh + "," + ThisYhdm + "," + ThisJx);
                }
                Ts1.Close();
                if (!Directory.Exists("C:\\Program Files\\lmw32\\"))
                {
                    Directory.CreateDirectory("C:\\Program Files\\lmw32\\");
                }
                string path1 = "C:\\Program Files\\lmw32";
                string run_exe = "LMWPRINT";
                string theExeStr1 = " /L=D:\\tm.QDF";
                System.Diagnostics.ProcessStartInfo p = null;
                System.Diagnostics.Process Proc;
                p = new ProcessStartInfo(run_exe, theExeStr1);
                p.WorkingDirectory = path1;//设置此外部程序所在windows目录
                p.WindowStyle = ProcessWindowStyle.Hidden;//在调用外部exe程序的时候，控制台窗口不弹出

                Proc = System.Diagnostics.Process.Start(p);//调用外部程序
            }
            catch (Exception e1)
            {
                MessageBox.Show("请确认Label Matrix的目录,数量是否正确!", "错误提示");
            }
        }

        private void ListJjdy_DoubleClick(object sender, EventArgs e)
        {
            if (ListJjdy.SelectedIndex < 0) return;
            string str = ListJjdy.SelectedValue.ToString();
            //ListJjdy.Items.Remove(ListJjdy.SelectedItem);
            TxtLsh.Text = str.Substring(0, str.IndexOf("--"));
        }

        private void ListYjdy_DoubleClick(object sender, EventArgs e)
        {
            if (ListYjdy.SelectedIndex < 0) return;
            string str = ListYjdy.SelectedValue.ToString();
            TxtLsh.Text = str.Substring(0, str.IndexOf("--"));
            CmdSxdy_Click(CmdSxdy,new EventArgs());
        }

        private void Command1_Click(object sender, EventArgs e)
        {
            //特殊打印
            Form form13 = new Form();
            form13.Text = "特殊条码打印";
            form13.Width = 400;
            form13.Height = 600;
            form13.WindowState = FormWindowState.Normal;
            form13.StartPosition = FormStartPosition.CenterScreen;
            ctrl_yqxxtsdy print13 = new ctrl_yqxxtsdy();
            print13.Width = 600;
            print13.Height = 800;
            print13.Top = 6;
            print13.Left = 6;
            form13.Controls.Add(print13);
            form13.Show(this);
        }
    }
}
