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
using System.Diagnostics;
using System.IO;

namespace Rmes.WinForm.Controls
{
    public partial class ctrl_Manta : BaseControl
    {
        private string CompanyCode, PlineCode, PlineID, StationCode, stationname, TheSo, TheSn, ThePlancode, TheFdjxl = "", TheJx = "", thePlinecode = "", theFrFlag="0";
        private string thisQueryStr = "";
        //dataConn dc = new dataConn();
        ProductInfoEntity product;
        public ctrl_Manta()
        {
            InitializeComponent();
            CompanyCode = LoginInfo.CompanyInfo.COMPANY_CODE;
            PlineID = LoginInfo.ProductLineInfo.RMES_ID;
            PlineCode = LoginInfo.ProductLineInfo.PLINE_CODE;
            StationCode = LoginInfo.StationInfo.RMES_ID;
            stationname = LoginInfo.StationInfo.STATION_NAME;
            dgvInsite.AutoGenerateColumns = false;
            dgvInsite.RowHeadersVisible = false;
            dgvInsite.DataSource = null;
            if (LoginInfo.StationInfo.STATION_TYPE == "ST14") //电控站点
            {
                thisQueryStr = "IS%";
            }
            else
            {
                thisQueryStr = "UP%";
            }
            timer1.Enabled = true;
            timer1.Start();
            timer1_Tick(timer1, new EventArgs());
            //refreash();
            this.RMesDataChanged += new RMesEventHandler(ctrl_Tj_RMesDataChanged);
        }
        void ctrl_Tj_RMesDataChanged(object obj, Rmes.WinForm.Base.RMESEventArgs e)
        {
            RMESEventArgs arg = new RMESEventArgs();
            arg.MessageHead = "";
            arg.MessageBody = "";
            if (e.MessageHead == null) return;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            string sql = "";
            if (check1.Checked)
            {
                sql = "SELECT t.sn,t.plan_so,t.work_time,nvl(a.prt1,'0') tm,nvl(a.prt2,'0') mp from data_product t left join atpuecmlsh a on t.sn=a.ghtm where t.work_time>sysdate-10 and (nvl(a.prt1,'0')='0' or nvl(a.prt2,'0')='0') AND (select sfdk from copy_engine_property where t.plan_so=copy_engine_property.so)= '是' and t.pline_code='" + PlineCode + "'  order by t.work_time desc";
            }
            else
            {
                sql = "SELECT t.sn,t.plan_so,t.work_time,nvl(a.prt1,'0') tm,nvl(a.prt2,'0') mp from data_product t left join atpuecmlsh a on t.sn=a.ghtm where t.work_time>sysdate-10 AND (select sfdk from copy_engine_property where t.plan_so=copy_engine_property.so)= '是' and t.pline_code='" + PlineCode + "' order by t.work_time desc";
            }
            DataTable dt = dataConn.GetTable(sql);
            dgvInsite.ClearSelection();
            dgvInsite.DataSource = dt;
            initgrid();
        }

        private void initgrid()
        {
            int width = 0;//定义一个局部变量，用于存储自动调整列宽以后整个DtaGridView的宽度
            for (int i = 0; i < dgvInsite.Rows.Count; i++)
            {

                if (dgvInsite.Rows[i].Cells[3].ToString() == "1" || dgvInsite.Rows[i].Cells[4].ToString() == "1")
                {
                    dgvInsite.Rows[i].DefaultCellStyle.BackColor = Color.FromArgb(255,255,0);
                }
            }
            for (int i = 0; i < this.dgvInsite.Columns.Count; i++)//对于DataGridView的每一个列都调整
            {
                this.dgvInsite.AutoResizeColumn(i, DataGridViewAutoSizeColumnMode.AllCells);//将每一列都调整为自动适应模式
                width += this.dgvInsite.Columns[i].Width;//记录整个DataGridView的宽度
            }
            if (width > this.dgvInsite.Size.Width)//判断调整后的宽度与原来设定的宽度的关系，如果是调整后的宽度大于原来设定的宽度，则将DataGridView的列自动调整模式设置为显示的列即可，如果是小于原来设定的宽度，将模式改为填充。
            {
                this.dgvInsite.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            }
            else
            {
                this.dgvInsite.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //重新登录
            BaseForm tempForm = (BaseForm)this.ParentForm;
            PublicClass.ClearEvents(tempForm);
            Application.Restart();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //刷新
            timer1.Start();
            timer1_Tick(timer1, new EventArgs());
        }

        private void cmdClear_Click(object sender, EventArgs e)
        {
            txtLsh.Text = "";
            txtSo.Text = "";
            txtJx.Text = "";
            txtUnitNo.Text = "";
            txtEcm.Text = "";
        }

        private void cmdClear2_Click(object sender, EventArgs e)
        {
            txtPn.Text = "";
            txtSn.Text = "";
            txtEc.Text = "";
            txtDc.Text = "";
            txtLsh2.Text = "";
        }

        private void txtLsh_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 13) return;
            if (string.IsNullOrWhiteSpace(txtLsh.Text)) return;
            GetFocused(txtLsh);
            string ThisInput = txtLsh.Text.Trim().ToUpper();
            if (ThisInput.StartsWith("$")) //单独指令
            {
                switch (ThisInput)
                {
                    case "$QUIT": //关闭计算机
                        Process.Start("shutdown.exe", "-s -t 10");
                        break;
                    case "$EXIT"://退出程序
                        BaseForm tempForm1 = (BaseForm)this.ParentForm;
                        PublicClass.ClearEvents(tempForm1);
                        Application.Exit();
                        break;
                    case "$CANC"://重新登录
                        //三漏数据处理 每次处理上一个流水号  退出时处理当前
                        BaseForm tempForm = (BaseForm)this.ParentForm;
                        PublicClass.ClearEvents(tempForm);
                        Application.Restart();
                        break;
                }
            }
            TheSn = ThisInput;
            product = ProductInfoFactory.GetByCompanyCodeSNSingle(CompanyCode, TheSn);//获取sn信息
            if (product == null)
            {
                MessageBox.Show("非法流水号","提示");
                txtLsh.Text = "";
                GetFocused(txtLsh);
                return;
            }
            txtSo.Text = product.PLAN_SO;
            txtJx.Text = product.PRODUCT_MODEL;
            txtUnitNo.Text = product.UNITNO;
            TheFdjxl = product.PRODUCT_SERIES;
            ThePlancode = product.PLAN_CODE;
            TheSo = txtSo.Text;
            TheJx = txtJx.Text;
            thePlinecode = product.PLINE_CODE;
            string sql = "select item_code from data_plan_standard_bom where plan_code='"+ThePlancode+"' and item_name='电控模块' and process_code!='90015'  and rownum=1 ";
            DataTable dt = dataConn.GetTable(sql);
            if (dt.Rows.Count > 0)
            {
                txtEcm.Text=dt.Rows[0][0].ToString();
            }
            else
            {
                txtEcm.Text = "*";
            }
            GetFocused(txtLsh);
        }

        private void txtLsh_Click(object sender, EventArgs e)
        {
            GetFocused(txtLsh);
        }
        private void GetFocused(TextBox textBox)
        {
            textBox.Focus();
            textBox.Select(0, textBox.Text.Length);
        }

        private void cmdPrint_Click(object sender, EventArgs e)
        {
            txtLsh_KeyPress(txtLsh,new KeyPressEventArgs((char)13));
            if (TheSn != "")
            {
                theFrFlag = PublicClass.IsFr(ThePlancode,TheSo,TheSn,thePlinecode);
                PublicClass.PrintDpTm(txtLsh.Text,txtSo.Text,txtJx.Text,txtUnitNo.Text,txtEcm.Text,ThePlancode,theFrFlag);
            }
        }

        private void cmdPrint2_Click(object sender, EventArgs e)
        {
            PublicClass.PrintDPNP(txtPn.Text, txtSn.Text, txtEc.Text, txtDc.Text, txtLsh2.Text, ThePlancode);
        }

        private void cmdAutoNp_Click(object sender, EventArgs e)
        {
            if (timer3.Enabled == false)
            {
                timer3.Enabled = true;
                lblAtuoNp.BackColor = Color.Green;
                lblAtuoNp.Text = "AUTO PRINT NAMEPLATE...";
            }
            else
            {
                timer3.Enabled = false;
                lblAtuoNp.BackColor = Color.Red;
                lblAtuoNp.Text = "PAUSE...";
            }
            if (cmdAutoNp.Text == "AUTO PRINT")
            {
                cmdAutoNp.Text = "PAUSE";
            }
            else
            {
                cmdAutoNp.Text = "AUTO PRINT";
            }

        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            try
            {
                string TheFileName = "C:\\Data Tag Log File\\ecmdata.txt";
                if (File.Exists(TheFileName))
                {
                    string s2 = "TM";
                    string s1 = File.ReadAllText("c:\\Data Tag Log File\\ecmdata.txt");
                    string[] filelist = File.ReadAllLines("c:\\Data Tag Log File\\ecmdata.txt", Encoding.Default);
                    for (int i = 0; i < filelist.Length; i++) //倒序读取文本
                    {
                        string line = filelist[i];
                        if (line != "")
                        {
                            line = line.Substring(0, 92);
                            s2 = s2 + (char)13 + (char)10 + line;
                        }
                    }
                    StreamWriter Ts1;
                    string TheFileName1 = "C:\\Data Tag Log File\\ecm.txt";
                    if (File.Exists(TheFileName1))
                    {
                        File.Delete(TheFileName1);
                    }
                    Ts1 = File.CreateText(TheFileName1);
                    Ts1.Write(s2);
                    Ts1.Close();
                    PublicClass.Handle_Ecm(TheFileName1);
                    File.Delete(TheFileName);

                    if (!Directory.Exists("C:\\Program Files\\lmw32\\"))
                    {
                        Directory.CreateDirectory("C:\\Program Files\\lmw32\\");
                    }
                    string path1 = "C:\\Program Files\\lmw32";
                    string run_exe = "LMWPRINT";
                    string theExeStr1 = " /L=C:\\Data Tag Log File\\ecm.QDF";
                    System.Diagnostics.ProcessStartInfo p = null;
                    System.Diagnostics.Process Proc;
                    p = new ProcessStartInfo(run_exe, theExeStr1);
                    p.WorkingDirectory = path1;//设置此外部程序所在windows目录
                    p.WindowStyle = ProcessWindowStyle.Hidden;//在调用外部exe程序的时候，控制台窗口不弹出

                    Proc = System.Diagnostics.Process.Start(p);//调用外部程序

                }
            }
            catch
            {
                MessageBox.Show("请确认Label Matrix的目录,数量是否正确!", "错误提示");
            }
        }

        private void cmdAuto_Click(object sender, EventArgs e)
        {
            if (timer2.Enabled == false)
            {
                timer2.Enabled = true;
                lblAuto.BackColor = Color.Green;
                lblAuto.Text = "AUTO PRINT BARCODE...";
            }
            else
            {
                timer2.Enabled = false;
                lblAuto.BackColor = Color.Red;
                lblAuto.Text = "PAUSE...";
            }
            if (cmdAuto.Text == "AUTO PRINT")
            {
                cmdAuto.Text = "PAUSE";
            }
            else
            {
                cmdAuto.Text = "AUTO PRINT";
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            string sql = "select ghtm from sjecmgztm where gzdd='" + PlineCode + "' and handle_flag='N'  and get_sfdk(so)='是' and rownum=1";
            DataTable dt = dataConn.GetTable(sql);
            if (dt.Rows.Count > 0)
            {
                txtLsh.Text=dt.Rows[0][0].ToString();
            }
            else
            {
                sql = "SELECT t.sn,t.plan_so,t.work_time,nvl(a.prt1,'0') tm,nvl(a.prt2,'0') mp from data_product t left join atpuecmlsh a on t.sn=a.ghtm where t.work_time>sysdate-10 and nvl(a.prt1,'0')='0' AND (select sfdk from copy_engine_property where t.plan_so=copy_engine_property.so)= '是' and t.pline_code='" + PlineCode + "'  order by t.work_time";
                DataTable dt2 = dataConn.GetTable(sql);
                if (dt2.Rows.Count > 0)
                {
                    txtLsh.Text = dt2.Rows[0][0].ToString();
                }
                else
                    return;
            }
            cmdPrint_Click(cmdPrint,new EventArgs());
            button2_Click(button2,new EventArgs());
        }

        private void dgvInsite_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            txtLsh.Text=dgvInsite.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtLsh_KeyPress(txtLsh,new KeyPressEventArgs((char)13));
            GetFocused(txtLsh);
        }

        private void txtDc_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 13) return;
            GetFocused(txtLsh2);
        }

        private void txtEc_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 13) return;
            GetFocused(txtDc);
        }

        private void txtPn_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 13) return;
            GetFocused(txtSn);
        }

        private void txtSn_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 13) return;
            GetFocused(txtEc);
        }

        private void cmdQuery_Click(object sender, EventArgs e)
        {
            frmEcmQuery ps = new frmEcmQuery();
            ps.StartPosition = FormStartPosition.Manual;
            ps.Show();
        }



    }
}
