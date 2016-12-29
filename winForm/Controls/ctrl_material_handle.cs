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

namespace Rmes.WinForm.Controls
{
    public partial class ctrl_material_handle : BaseControl
    {
        private string CompanyCode, PlineCode, PlineID, StationCode, stationname, TheSo, TheSn, ThePlancode;
        private string theLjdm = "", theGys = "", theAddTime = "", theGwmc="";
        private bool theDelFlag = false;
        //dataConn dc = new dataConn();
        ProductInfoEntity product;
        public ctrl_material_handle()
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
            Init_Data();
            Show_Data();
            //this.RMesDataChanged += new RMesEventHandler(ctrl_Tj_RMesDataChanged);
        }
        void ctrl_Tj_RMesDataChanged(object obj, Rmes.WinForm.Base.RMESEventArgs e)
        {
            RMESEventArgs arg = new RMESEventArgs();
            arg.MessageHead = "";
            arg.MessageBody = "";
            if (e.MessageHead == null) return;
        }
        private void Init_Data()
        {
            ComboGw.DataSource = null;
            string sql = "select distinct location_code from vw_rel_station_location where pline_code='" + LoginInfo.ProductLineInfo.PLINE_CODE + "' AND station_name='" + LoginInfo.StationInfo.STATION_NAME + "'  order by location_code";
            DataTable dt = dataConn.GetTable(sql);
            ComboGw.DataSource = dt;
            ComboGw.SelectedIndex = 0;
        }

        private void Show_Data()
        {
            string sql = "select location_code ,material_code ,gys_code ,material_num ,to_char(add_time,'YYYY-MM-DD HH24:MI:SS') YLSJ,zdmc ,ygmc ,reject_reason  from ms_discard_mt where gzdd='" + LoginInfo.ProductLineInfo.PLINE_CODE + "' and zdmc='" + LoginInfo.StationInfo.STATION_NAME + "' and handle_flag='N' order by location_code,material_code";
            DataTable dt = dataConn.GetTable(sql);
            dgvInsite.DataSource = dt;
            theDelFlag = false;
        }

        private void cmdAdd_Click(object sender, EventArgs e)
        {
            if (Check_Data())
            {
                string sql = "INSERT INTO MS_DISCARD_MT(LOCATION_CODE,MATERIAL_CODE,MATERIAL_NUM,HANDLE_FLAG,ZDMC,YGMC,BCMC,BZMC,ADD_TIME,REJECT_REASON,GZDD,GYS_CODE) "
                               + " VALUES('" + ComboGw.Text + "','" + theLjdm +"',"+txtNum.Text+",'N','"+LoginInfo.StationInfo.STATION_NAME+"','"+LoginInfo.UserInfo.USER_NAME+"','"+LoginInfo.ShiftInfo.SHIFT_NAME+"','"+LoginInfo.TeamInfo.TEAM_NAME+"',SYSDATE,'"+txtReason.Text+"','"+PlineCode+"','"+theGys+"')";
                dataConn.ExeSql(sql);
                Show_Data();
            }
        }
        private bool Check_Data()
        {
            try
            {
                if (ComboGw.Text == "")
                {
                    MessageBox.Show("请选择工位");
                    return false;
                }
                if (txtTm.Text.Trim() == "")
                {
                    MessageBox.Show("零件号不能为空");
                    GetFocused(txtTm);
                    return false;
                }
                txtTm.Text = txtTm.Text.Replace('|', '^').ToUpper();
                if (txtNum.Text.Trim() == "")
                {
                    MessageBox.Show("请输入数量");
                    GetFocused(txtNum);
                    return false;
                }
                if (!Microsoft.VisualBasic.Information.IsNumeric(txtNum.Text.Trim()))
                {
                    MessageBox.Show("数量不合法");
                    GetFocused(txtNum);
                    return false;
                }
                if (txtTm.Text.IndexOf('^') < 2)
                {
                    MessageBox.Show("零件格式非法");
                    GetFocused(txtTm);
                    return false;
                }
                string[] strAry = txtTm.Text.Trim().Split('^');

                for (int j = 0; j < strAry.Length; j++)
                {
                    if (j == 0)
                    {
                        theLjdm=strAry[0];
                    }
                    if (j == 1)
                    {
                        theGys=strAry[1];
                    }
                }
                string sql = "select ps_comp from copy_ps_mstr where ps_comp='" + theLjdm + "'";
                DataTable dt = dataConn.GetTable(sql);
                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("零件格式非法");
                    GetFocused(txtTm);
                    return false;
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        private void GetFocused(TextBox textBox)
        {
            textBox.Focus();
            textBox.Select(0, textBox.Text.Length);
        }

        private void cmdExit_Click(object sender, EventArgs e)
        {
            BaseForm tempForm1 = (BaseForm)this.ParentForm;
            PublicClass.ClearEvents(tempForm1);
            Application.Exit();
        }

        private void cmdDelete_Click(object sender, EventArgs e)
        {
            if (!theDelFlag)
            {
                MessageBox.Show("请选择删除项");
                return;
            }
            string sql = "     DELETE FROM MS_DISCARD_MT "
                        + " WHERE GZDD='" + PlineCode + "' AND ZDMC='" + stationname + "' AND LOCATION_CODE='" + theGwmc + "' AND MATERIAL_CODE='" + theLjdm + "' AND HANDLE_FLAG='0' "
                        +"  AND ADD_TIME=TO_DATE('"+theAddTime+"','YYYY-MM-DD HH24:MI:SS')";
            dataConn.ExeSql(sql);
            Show_Data();
        }

        private void txtTm_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 13) return;
            if (string.IsNullOrWhiteSpace(txtTm.Text.Trim())) return;
            if (txtTm.Text.Trim().Contains("EXIT"))
            {
                BaseForm tempForm1 = (BaseForm)this.ParentForm;
                PublicClass.ClearEvents(tempForm1);
                Application.Exit();
            }
        }

        private void dgvInsite_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            dgvInsite.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Yellow;

            theGwmc = dgvInsite.Rows[e.RowIndex].Cells[0].ToString();
            theLjdm = dgvInsite.Rows[e.RowIndex].Cells[1].ToString();
            theGys = dgvInsite.Rows[e.RowIndex].Cells[2].ToString();
            theAddTime = dgvInsite.Rows[e.RowIndex].Cells[4].ToString();
            theDelFlag = true;

        }

    }
}
