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


namespace Rmes.WinForm.Base
{
    public partial class frmLogin1 : Form
    {
        bool initing = true;
        public frmLogin1()
        {
            InitializeComponent();

            comboBox1.DisplayMember = "COMPANY_NAME";
            comboBox1.ValueMember = "COMPANY_CODE";

            comboBox2.DisplayMember = "PLINE_NAME";
            comboBox2.ValueMember = "RMES_ID";

            comboBox3.DisplayMember = "STATION_NAME";
            comboBox3.ValueMember = "STATION_CODE";

             comboBox4.DisplayMember = "SHIFT_NAME";
            comboBox4.ValueMember = "RMES_ID";

            comboBox5.DisplayMember = "TEAM_NAME";
            comboBox5.ValueMember = "RMES_ID";

            comboBox6.DisplayMember = "WORKSHOP_NAME";
            comboBox6.ValueMember = "WORKSHOP_CODE";
            
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
            string companycode = comboBox1.SelectedIndex>-1?comboBox1.SelectedValue.ToString():"";
            string workshopcode = comboBox6.SelectedIndex > -1 ? comboBox6.SelectedValue.ToString() : "";
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
      
            if (comboBox3.SelectedIndex >= 0)
            {
                string stationcode = cb.SelectedValue.ToString();
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
                    comboBox6.SelectedValue = DB.ReadConfigLocal("WORKSHOP_CODE");
                    comboBox6.DataSource = WorkShopFactory.GetAll();
                    setComboxBoxCurrent(comboBox6);

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

        private void comboBox6_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (initing) return;
            //string workshop_code = comboBox6.SelectedValue.ToString();
            //DB.WriteConfigLocal("WORKSHOP_CODE", workshop_code);
            //List<ProductLineEntity> plines = ProductLineFactory.GetAll();
            //var ss = from s in plines orderby s.WORKSHOP_CODE
            //         where s.WORKSHOP_CODE == workshop_code && s.COMPANY_CODE == comboBox1.SelectedValue.ToString()
            //         select s;
            //comboBox2.DataSource = ss.ToList<ProductLineEntity>();
            //LoginInfo.WorkShopInfo = WorkShopFactory.GetByWorkShopID(workshop_code).First();
        }

    }
}
