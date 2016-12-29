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
    public partial class ctrl_yqxxtsdy : BaseControl
    {
        private string CompanyCode, PlineID, PlineCode1, StationID, len_sn, TheSo, TheSn, ThePlancode, StationName, Station_Code;
        private string ShiftCode, TeamCode, UserID;
        private string mySo = "", myJx = "", myJh = "", myYhdm = "", myYh = "";
        ProductInfoEntity product;
        //dataConn dc = new dataConn();
        //东区油漆下线条码打印特殊打印
        public ctrl_yqxxtsdy()
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

            GetFocused(TxtTstm);
        }

        private void CmdTc_Click(object sender, EventArgs e)
        {
            this.ParentForm.Close();
        }

        private void CmdQx_Click(object sender, EventArgs e)
        {
            TxtTstm.Text = "";
            txtso.Text = "";
            txtjx.Text = "";
            txtyh.Text = "";
            GetFocused(TxtTstm);
        }
        private void GetFocused(TextBox textBox)
        {
            textBox.Focus();
            textBox.Select(0, textBox.Text.Length);
        }

        private void TxtTstm_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 13) return;
            if (string.IsNullOrWhiteSpace(TxtTstm.Text)) return;
            string sn = TxtTstm.Text.Trim();
            if (sn == "")
                return;
            
            string sql = "select * from data_product where sn='" + TxtTstm.Text + "'";
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
            }
            sql = "select * from nameplate_so where bzso='" + mySo + "'";
            dt = dataConn.GetTable(sql);
            if (dt.Rows.Count > 0)
            {
                myYhdm = dt.Rows[0]["KHH"].ToString();
            }
            txtyh.Text = myYh;
            txtso.Text = mySo;
            txtjx.Text = myJx;
            CmdDy.Focus();
        }

        private void TxtTstm_Leave(object sender, EventArgs e)
        {
            TxtTstm_KeyPress(TxtTstm,new KeyPressEventArgs((char)13));
        }

        private void cmdDyms_Click(object sender, EventArgs e)
        {
            txtSl.Visible = true;
        }

        private void CmdDy_Click(object sender, EventArgs e)
        {
            if (TxtTstm.Text.Trim() == "" || txtso.Text.Trim() == "" || txtjx.Text.Trim() == "" || txtyh.Text.Trim() == "")
            {
                MessageBox.Show("待打印内容不能为空","提示");
                GetFocused(TxtTstm);
                return;
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
            PrintTmFz(TxtTstm.Text.Trim(), txtso.Text.Trim(), txtyh.Text.Trim(), myYhdm, txtjx.Text, sl);
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

    }
}
