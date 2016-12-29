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
using PetaPoco;
using Rmes.WinForm.Controls;

namespace Rmes.WinForm.Base
{
    public partial class frmReLogin : Form
    {
       
        bool initing = true;
        public frmReLogin()
        {
            InitializeComponent();
            this.FormClosing += new FormClosingEventHandler(frmReLogin_FormClosing);
            comboBox1.DisplayMember = "COMPANY_NAME";
            comboBox1.ValueMember = "COMPANY_CODE";

            comboBox2.DisplayMember = "PLINE_NAME";
            comboBox2.ValueMember = "RMES_ID";

            comboBox3.DisplayMember = "STATION_NAME";
            comboBox3.ValueMember = "RMES_ID";

             comboBox4.DisplayMember = "SHIFT_NAME";
            comboBox4.ValueMember = "RMES_ID";

            comboBox5.DisplayMember = "TEAM_NAME";
            comboBox5.ValueMember = "RMES_ID";
            
            comboBox1.DataSource = CompanyFactory.GetAll();
            string _companycode = DB.ReadConfigLocal("COMPANY_CODE");
            if (!_companycode.Equals(string.Empty))
            {
                comboBox1.SelectedValue = _companycode;
                CompanyEntity company = CompanyFactory.GetByKey(_companycode);
                if (company != null)
                {
                    LoginInfo.CompanyInfo = company;
                    initComboBoxByCompany();
                }
            }
            initing = false;
        }

        void frmReLogin_FormClosing(object sender, FormClosingEventArgs e)
        {

            if (((Form)sender).DialogResult != DialogResult.OK)
            {
                this.Owner.Show();
                this.Dispose();
            }
            else
            {
                this.Owner.Dispose();
                try
                {
                    DB.DataConnectionName = "oracle";
                    Database db = DB.GetInstance();
                    List<ConfigFormEntity> formconfig = BaseControlFactory.GetFormConfigFromStationCode(LoginInfo.StationInfo.RMES_ID);
                    if (formconfig.Count < 1)
                    {
                        MessageBox.Show("该站点没有绑定任何执行窗体，请确认本地设置是否正确。");
                        e.Cancel = true;
                    }
                    else
                    {
                        
                        List<ConfigControlsEntity> configs = BaseControlFactory.GetFormControlConfigByFormID(formconfig.First().FORMID);
                        BaseForm _mainform = Rmes.WinForm.Base.UiFactory.ReLoad(formconfig.First(), configs.ToList<ConfigControlsEntity>());
                        _mainform.Text = LoginInfo.CompanyInfo.COMPANY_NAME_BRIEF + "MES系统";
                        //_mainform.FormClosed += new FormClosedEventHandler(_mainform_FormClosed);
                        //_mainform.FormClosing += new FormClosingEventHandler(_mainform_FormClosing);
                        _mainform.Show();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "系统初始化产生问题！");
                    Application.ExitThread();
                }
            }
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            panelDetail.Visible = !panelDetail.Visible;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Dictionary<string,object> dict = LoginInfo.Dict;
            //dict.Clear();
            //dict.Add("user", LoginInfo.UserInfo);
            //dict.Add("company", LoginInfo.CompanyInfo);
            //dict.Add("team", LoginInfo.TeamInfo);
            //dict.Add("station", LoginInfo.StationInfo);
            //dict.Add("pline", LoginInfo.ProductLineInfo);
            //dict.Add("shift", LoginInfo.ShiftInfo);
            string companycode = comboBox1.SelectedIndex>-1?comboBox1.SelectedValue.ToString():"";
            string workshopcode = comboBox6.SelectedIndex >= 0 ? comboBox6.SelectedValue.ToString() : "";
            string plinecode = comboBox2.SelectedIndex>-1?comboBox2.SelectedValue.ToString():"";
            string stationcode = comboBox3.SelectedIndex>-1?comboBox3.SelectedValue.ToString():"";
            string shiftcode = comboBox4.SelectedIndex>-1?comboBox4.SelectedValue.ToString():"";
            string groupcode = comboBox5.SelectedIndex>=0?comboBox5.SelectedValue.ToString():"";
            string othercode = comboBox6.SelectedIndex>=0?comboBox6.SelectedValue.ToString():"";
            string ip = System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName()).AddressList.GetValue(0).ToString();
            bool logined = UserFactory.GetByFormAuth(txtUcode.Text, txtUpass.Text,
                companycode, workshopcode,plinecode, stationcode, shiftcode, groupcode, othercode, ip, DB.GetServerTime().ToString());
           
            if (logined)
            {
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show(Rmes.Public.ErrorHandle.EH.LASTMSG,"验证失败！");
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (initing) return;
            object val = comboBox1.SelectedValue;
            if (val != null)
            {
                DB.WriteConfigLocal("COMPANY_CODE", val.ToString());
                LoginInfo.CompanyInfo = CompanyFactory.GetByKey(val.ToString());
                initComboBoxByCompany();
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (initing) return;
            string plinermesid = comboBox2.SelectedValue.ToString();
            DB.WriteConfigLocal("PRODUCTLINE_CODE", plinermesid);
            List<StationEntity> stations = StationFactory.GetAll();
            var ss = from s in stations
                     where s.PLINE_CODE == plinermesid && s.COMPANY_CODE == comboBox1.SelectedValue.ToString()
                     select s;
            comboBox3.DataSource = ss.ToList<StationEntity>();
            List<TeamEntity> teams = TeamFactory.GetByPlineCode(plinermesid);
            comboBox5.DataSource = teams;
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (initing) return;
            ComboBox cb = (ComboBox)sender;
            if (cb.SelectedIndex >= 0)
            {
                string stationcode = ((ComboBox)sender).SelectedValue.ToString();
                DB.WriteConfigLocal("STATION_CODE", stationcode);
            }
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (initing) return;
            string val = comboBox4.SelectedValue.ToString();
            DB.WriteConfigLocal("SHIFT_CODE", val);
        }

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (initing) return;
            string val = comboBox5.SelectedValue.ToString();
            DB.WriteConfigLocal("TEAM_CODE", val);
        }


        private void initComboBoxByCompany()
        {
            initing = true;
            object val = null;

            val = comboBox1.SelectedValue;
            try
            {
                if (val != null)
                {
                    comboBox2.DataSource = ProductLineFactory.GetByCompanyCode(val.ToString());
                    comboBox2.SelectedValue = DB.ReadConfigLocal("PRODUCTLINE_CODE");
                    setComboxBoxCurrent(comboBox2);

                    val = comboBox2.SelectedValue;
                    if (val != null)
                        comboBox3.DataSource = StationFactory.GetByProductLine(val.ToString());
                    else
                    {
                        List<StationEntity> stas = StationFactory.GetAll();
                        var sta1 = from ss in stas
                                   where ss.COMPANY_CODE == LoginInfo.CompanyInfo.COMPANY_CODE
                                   select ss;
                        comboBox3.DataSource = sta1.ToList<StationEntity>();
                    }
                    comboBox3.SelectedValue = DB.ReadConfigLocal("STATION_CODE");
                    setComboxBoxCurrent(comboBox3);

                    comboBox4.DataSource = ShiftFactory.GetAll();
                    comboBox4.SelectedValue = DB.ReadConfigLocal("SHIFT_CODE");
                    setComboxBoxCurrent(comboBox4);

                    comboBox5.DataSource = TeamFactory.GetByPlineCode(val.ToString());
                    comboBox5.SelectedValue = DB.ReadConfigLocal("TEAM_CODE");
                    setComboxBoxCurrent(comboBox5);
                }
            }
            catch
            {

            }
            initing = false;
        }

        private void setComboxBoxCurrent(ComboBox cbox)
        {
            if (cbox.SelectedIndex < 0 && cbox.Items.Count > 0)
                cbox.SelectedIndex = 0;
        }

        private void txtUpass_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar.ToString().Equals("\r"))
            {
                if (txtUpass.Text == "")
                { 
                    txtUpass.Focus(); return; 
                }
                if (txtUcode.Text == "")
                {
                    txtUcode.Focus(); return;
                }
                this.button1_Click(txtUpass, null);
            }
        }
    }
}
