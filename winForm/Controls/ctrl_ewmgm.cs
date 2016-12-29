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

namespace Rmes.WinForm.Controls
{
    public partial class ctrl_ewmgm : BaseControl
    {
        private string CompanyCode, PlineID, PlineCode1, StationID, len_sn, TheSo, TheSn, ThePlancode, StationName, Station_Code;
        private string ShiftCode, TeamCode, UserID;
        ProductInfoEntity product;
        //dataConn dc = new dataConn();
        //二维码改码打印
        public ctrl_ewmgm()
        {
            InitializeComponent();
            //this.RMesDataChanged += new Rmes.WinForm.Base.RMesEventHandler(ctrlProductScan_RmesDataChanged);

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
            txtscrq.Text = DateTime.Now.ToString("yyMMdd");
            string sql = "select model_no,model_name,flag from atpumodel where flag='Y'";
            DataTable dt = dataConn.GetTable(sql);
            model.DataSource = dt;
            model.DisplayMember = "MODEL_NAME";
            model.ValueMember = "MODEL_NO";
        }

        private void txtso_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 13) return;
            if (string.IsNullOrWhiteSpace(txtso.Text)) return;
            txtkhh.Text = "";
            txtjx.Text = "";
            //string sql = "select jx,nvl(khh,'" + txtso.Text.Trim() + "') from atpuplannameplate  WHERE bzso='" + txtso.Text.Trim() + "' and rownum=1 ";
            string sql = "select jx,nvl(khh,'" + txtso.Text.Trim() + "') from atpuplannameplate where bzso='" + txtso.Text.Trim() + "' and plan_code=(select plan_code from data_product where sn='" + txtlsh.Text.Trim() + "' and rownum=1) and rownum=1 ";
           
            DataTable dt = dataConn.GetTable(sql);
            if (dt.Rows.Count > 0)
            {
                txtjx.Text = dt.Rows[0][0].ToString();
                txtkhh.Text = dt.Rows[0][1].ToString();
                if (model.Text == "DFCP")
                    txtkhh.Text = "";
            }
            else
            {
                txtkhh.Text = "";
                txtjx.Text = "";
            }

        }

        private void Command1_Click(object sender, EventArgs e)
        {
            if (txtso.Text.Trim() == "")
            {
                MessageBox.Show("请输入SO","提示");
                return;
            }
            txtkhh.Text = "";
            txtjx.Text = "";
            //string sql = "select jx,nvl(khh,'" + txtso.Text.Trim() + "') from atpuplannameplate  WHERE bzso='" + txtso.Text.Trim() + "' and rownum=1 ";
            string sql = "select jx,nvl(khh,'" + txtso.Text.Trim() + "') from atpuplannameplate where bzso='" + txtso.Text.Trim() + "' and plan_code=(select plan_code from data_product where sn='"+txtlsh.Text.Trim()+"' and rownum=1) and rownum=1 ";
            DataTable dt = dataConn.GetTable(sql);
            if (dt.Rows.Count > 0)
            {
                txtjx.Text = dt.Rows[0][0].ToString();
                txtkhh.Text = dt.Rows[0][1].ToString();
            }
            else
            {
                txtkhh.Text = "";
                txtjx.Text = "";
            }
        }

        private void btnprint_Click(object sender, EventArgs e)
        {
            if (model.Text == "")
            {
                MessageBox.Show("请选择要打印的模板","提示");
                return;
            }
            if (txtlsh.Text.Trim() == "")
            {
                MessageBox.Show("请输入流水号", "提示");
                return;
            }
            if (txtso.Text.Trim() == "")
            {
                MessageBox.Show("请输入SO", "提示");
                return;
            }
            if (txtscrq.Text.Trim() == "")
            {
                MessageBox.Show("请输入生产日期", "提示");
                return;
            }
            switch (model.Text)
            {
                case "DFCV":
                    PublicClass.printDFCVE1(txtlsh.Text.Trim(),txtso.Text.Trim(),txtkhh.Text.Trim(),txtscrq.Text.Trim());
                    break;
                case "DFCP":
                    PublicClass.printDFCP1(txtlsh.Text.Trim(), txtso.Text.Trim(), txtkhh.Text.Trim(), txtscrq.Text.Trim());
                    break;
                case "DFAC":
                    PublicClass.printDFAC1(txtlsh.Text.Trim(), txtso.Text.Trim(), txtkhh.Text.Trim(), txtscrq.Text.Trim());
                    break;
            }


        }

        private void obprint_Click(object sender, EventArgs e)
        {
            string path1 = "C:\\Program Files\\lmw32";
            string run_exe = "LMWPRINT";
            string theExeStr1 = " /L=C:\\gttmisf.QDF";
            System.Diagnostics.ProcessStartInfo p = null;
            System.Diagnostics.Process Proc;
            p = new ProcessStartInfo(run_exe, theExeStr1);
            p.WorkingDirectory = path1;//设置此外部程序所在windows目录
            p.WindowStyle = ProcessWindowStyle.Hidden;//在调用外部exe程序的时候，控制台窗口不弹出

            Proc = System.Diagnostics.Process.Start(p);//调用外部程序
        }

        private void tbprint_Click(object sender, EventArgs e)
        {
            string path1 = "C:\\Program Files\\lmw8";
            string run_exe = "LMWPRINT";
            string theExeStr1 = " /L=C:\\gttmisf.QDF";
            System.Diagnostics.ProcessStartInfo p = null;
            System.Diagnostics.Process Proc;
            p = new ProcessStartInfo(run_exe, theExeStr1);
            p.WorkingDirectory = path1;//设置此外部程序所在windows目录
            p.WindowStyle = ProcessWindowStyle.Hidden;//在调用外部exe程序的时候，控制台窗口不弹出

            Proc = System.Diagnostics.Process.Start(p);//调用外部程序
        }




    }
}
