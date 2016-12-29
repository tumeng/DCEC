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
using Rmes.DA.Factory;
using Rmes.DA.Dal;
using System.Net;
using System.Net.Sockets;
using System.Diagnostics;
using System.IO;

namespace Rmes.WinForm.Base
{
    public partial class frmLogin : Form
    {
        bool initing = true;
        public frmLogin()
        {
            InitializeComponent();
            LoginInfo.LEN_SN = "8";
            CsGlobalClass.OLDSN = "";
            CsGlobalClass.OLDSO = "";
            CsGlobalClass.OLDJX = "";
            CsGlobalClass.NEWSN = "";
            CsGlobalClass.NEWPLANCODE = "";
            CsGlobalClass.FDJXL = "";
            CsGlobalClass.NEEDVEPS = false;
            CsGlobalClass.DGSM = false;
            //CsGlobalClass.WJLJ = "\\\\192.168.112.82\\pic";
            CsGlobalClass.WJLJ = "D:\\tv";
            //20161221 现场报缺少根元素 感觉跟这个有问题 暂时注释
            //string _companycode = DB.ReadConfigLocal("COMPANY_CODE");
            //if (!_companycode.Equals(string.Empty))
            //{
            //    CompanyEntity company = CompanyFactory.GetByKey(_companycode);
            //    if (company != null)
            //    {
            //        LoginInfo.CompanyInfo = company;
            //    }
            //}
            initing = false;
            try
            {
                //File.Delete(@"C:\\Spc.ini");  //string file_template="\\Template\\KGG.xlsx";
                //File.Copy(@"..\Template\KGG.xlsx", @"D:\\KGG.xlsx", true);
                if (File.Exists(@"\\192.168.112.144\MES共享\模板\Spc.ini"))
                {
                    File.Copy(@"\\192.168.112.144\MES共享\模板\Spc.ini", @"C:\\Spc.ini", true);
                }
            }
            catch(Exception e1) 
            {
                //MessageBox.Show(e1.Message);
            }
            try
            {
                //File.Delete(@"C:\\Spc.ini");  //string file_template="\\Template\\KGG.xlsx";
                //File.Copy(@"..\Template\KGG.xlsx", @"D:\\KGG.xlsx", true);
                if (File.Exists(@"\\192.168.112.144\MES共享\模板\SPCclient.dll"))
                {
                    File.Copy(@"\\192.168.112.144\MES共享\模板\SPCclient.dll", @"C:\\SPCclient.dll", true);
                }
            }
            catch (Exception e1)
            {
                //MessageBox.Show(e1.Message);
            }
        }

        private void setComboxBoxCurrent(ComboBox cbox)
        {
            if (cbox.SelectedIndex < 0 && cbox.Items.Count > 0)
                cbox.SelectedIndex = 0;
        }

        private void txtBc_Click(object sender, EventArgs e)
        {
            txtBc.SelectionStart = 0;
            txtBc.SelectionLength = txtBc.Text.Length;
            txtBc.Focus();
        }

        private void txtBc_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar.ToString().Equals("\r"))
            {
                try
                {
                    if (txtBc.Text == "")
                    {
                        txtBc.Focus(); return;
                    }

                    string shift = txtBc.Text.Trim().ToUpper();
                    switch (shift)
                    {
                        case "$QUIT":
                            Process.Start("shutdown.exe", "-s -t 10");
                            break;
                        case "$EXIT":
                            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
                            this.Close();
                            break;
                        case "$CANC":
                            txtBc.Focus();
                            break;
                        default:
                            break;
                    }

                    DB.WriteConfigLocal("SHIFT_CODE", shift);
                    LoginInfo.ShiftInfo = ShiftFactory.GetBySCode(shift);
                    List<ProductLineEntity> plines = ProductLineFactory.GetAll();
                    DB.WriteConfigLocal("COMPANY_CODE", LoginInfo.ShiftInfo.COMPANY_CODE);
                    LoginInfo.CompanyInfo = CompanyFactory.GetByKey(LoginInfo.ShiftInfo.COMPANY_CODE);
                    txtBc.Text = "";
                    txtBc.Text = LoginInfo.ShiftInfo.SHIFT_NAME;
                    e.Handled = true;
                    //if (txtBz.Text == "")
                    {
                        txtBz.Focus(); return;
                    }

                }
                catch
                {
                    txtBc.Text = "";
                    txtBc.SelectionStart = 0;
                    txtBc.SelectionLength = txtBc.Text.Length;
                    txtBc.Focus();
                }
                //this.button1_Click(txtBz, null);
            }
        }

        private void txtBz_Click(object sender, EventArgs e)
        {
            txtBz.SelectionStart = 0;
            txtBz.SelectionLength = txtBz.Text.Length;
            txtBz.Focus();
        }

        private void txtBz_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar.ToString().Equals("\r"))
            {
                try
                {
                    if (txtBz.Text == "")
                    {
                        txtBz.Focus(); return;
                    }

                    string team = txtBz.Text.Trim().ToUpper();
                    switch (team)
                    {
                        case "QUIT":
                            Process.Start("shutdown.exe", "-s -t 10");
                            break;
                        case "EXIT":
                            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
                            this.Close();
                            break;
                        case "CANC":
                            txtBc.Focus();
                            txtBz.Text = "";
                            break;
                        default:
                            break;
                    }

                    DB.WriteConfigLocal("TEAM_CODE", team);
                    LoginInfo.TeamInfo = TeamFactory.GetByTeamCode(team);
                    txtBz.Text = "";
                    txtBz.Text = LoginInfo.TeamInfo.TEAM_NAME;
                    e.Handled = true;
                    //if (txtUser.Text == "")
                    {
                        txtUser.Focus(); return;
                    }

                }
                catch
                {
                    txtBz.Text = "";
                    txtBz.SelectionStart = 0;
                    txtBz.SelectionLength = txtBc.Text.Length;
                    txtBz.Focus();
                }
            }
        }

        private void txtUser_Click(object sender, EventArgs e)
        {
            txtUser.SelectionStart = 0;
            txtUser.SelectionLength = txtUser.Text.Length;
            txtUser.Focus();
        }

        private void txtUser_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar.ToString().Equals("\r"))
            {
                try
                {
                    if (txtUser.Text == "")
                    {
                        txtUser.Focus(); return;
                    }

                    string usercode = txtUser.Text.Trim().ToUpper();
                    switch (usercode)
                    {
                        case "QUIT":
                            Process.Start("shutdown.exe", "-s -t 10");
                            break;
                        case "EXIT":
                            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
                            this.Close();
                            break;
                        case "CANC":
                            txtBc.Focus();
                            txtBz.Text = "";
                            break;
                        default:
                            break;
                    }

                    DB.WriteConfigLocal("USER_CODE", usercode);
                    LoginInfo.UserInfo = UserFactory.GetByUserCode(usercode);
                    txtUser.Text = "";
                    txtUser.PasswordChar = new char();
                    txtUser.Text = LoginInfo.UserInfo.USER_NAME;
                    e.Handled = true;
                    //if (txtStation.Text == "")
                    {
                        txtStation.Focus(); return;
                    }
                }
                catch
                {
                    txtUser.Text = "";
                    txtUser.SelectionStart = 0;
                    txtUser.SelectionLength = txtBc.Text.Length;
                    txtUser.Focus();
                }
            }
        }

        private void txtStation_Click(object sender, EventArgs e)
        {
            txtStation.SelectionStart = 0;
            txtStation.SelectionLength = txtStation.Text.Length;
            txtStation.Focus();
        }

        private void txtStation_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar.ToString().Equals("\r"))
            {
                try
                {
                    if (txtStation.Text == "")
                    {
                        txtStation.Focus(); return;
                    }

                    string station = txtStation.Text.Trim().ToUpper();
                    switch (station)
                    {
                        case "QUIT":
                            Process.Start("shutdown.exe", "-s -t 10");
                            break;
                        case "EXIT":
                            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
                            this.Close();
                            break;
                        case "CANC":
                            txtBc.Focus();
                            txtBz.Text = "";
                            break;
                        default:
                            break;
                    }

                    DB.WriteConfigLocal("STATION_NAME", station);
                    LoginInfo.StationInfo = StationFactory.GetBySTATIONNAME(station);
                    txtStation.Text = "";
                    txtStation.PasswordChar = new char();
                    txtStation.Text = LoginInfo.StationInfo.STATION_NAME;

                    if (txtBc.Text.Trim() == "" || txtBz.Text.Trim() == "" || txtUser.Text.Trim() == "" || txtStation.Text.Trim() == "")
                    {
                        return;
                    }
                    if (LoginInfo.StationInfo.STATION_TYPE != "ST05" && LoginInfo.TeamInfo.TEAM_NAME.Contains("修理班"))//非返修站点 但是返修班组 则认为顶岗扫描
                    {
                        CsGlobalClass.DGSM = true;
                    }
                    LoginInfo.ProductLineInfo = ProductLineFactory.GetByStationCode(LoginInfo.StationInfo.STATION_CODE);
                    string companycode = LoginInfo.CompanyInfo.COMPANY_CODE;
                    string workshopcode = "";
                    string usercode = LoginInfo.UserInfo.USER_CODE;
                    string plinecode = LoginInfo.ProductLineInfo.RMES_ID;
                    string stationcode = LoginInfo.StationInfo.STATION_CODE;
                    string shiftcode = LoginInfo.ShiftInfo.SHIFT_CODE;
                    string groupcode = LoginInfo.TeamInfo.TEAM_CODE;
                    string othercode = "";
                    //string ip = System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName()).AddressList.GetValue(0).ToString();
                    string ip = GetLocalIP();
                    bool logined = UserFactory.GetByFormAuth(usercode, "",
                        companycode, workshopcode, plinecode, stationcode, shiftcode, groupcode, othercode, ip, DB.GetServerTime().ToString());
                    if (logined)
                    {
                        this.DialogResult = System.Windows.Forms.DialogResult.OK;
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show(Rmes.Public.ErrorHandle.EH.LASTMSG, "验证失败！");
                    }
                }
                catch
                {
                    txtStation.Text = "";
                    txtStation.SelectionStart = 0;
                    txtStation.SelectionLength = txtBc.Text.Length;
                    txtStation.Focus();
                }
            }
        }

        public static string GetLocalIP()
        {
            try
            {
                string HostName = Dns.GetHostName(); //得到主机名
                IPHostEntry IpEntry = Dns.GetHostEntry(HostName);
                for (int i = 0; i < IpEntry.AddressList.Length; i++)
                {
                    //从IP地址列表中筛选出IPv4类型的IP地址
                    //AddressFamily.InterNetwork表示此IP为IPv4,
                    //AddressFamily.InterNetworkV6表示此地址为IPv6类型
                    if (IpEntry.AddressList[i].AddressFamily == AddressFamily.InterNetwork)
                    {
                        return IpEntry.AddressList[i].ToString();
                    }
                }
                return "";
            }
            catch (Exception ex)
            {
                MessageBox.Show("获取本机IP出错:" + ex.Message);
                return "";
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }
    }
}
