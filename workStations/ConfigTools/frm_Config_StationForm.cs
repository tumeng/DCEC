using System;
using System.Reflection;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.IO;

namespace Rmes.Workstation.ConfigTools
{
    public partial class frm_Config_StationForm : Form
    {
        private Rmes.DA.Base.ConfigFormEntity stationform = null;
        public frm_Config_StationForm()
        {
            InitializeComponent();
            dgvStations.ReadOnly = true;
            dgvStations.RowHeadersVisible = false;
            this.FormClosed += new FormClosedEventHandler(frm_Config_StationForm_FormClosed);

            cbCompany.DisplayMember = "COMPANY_NAME";
            cbCompany.ValueMember = "COMPANY_CODE";
            cbCompany.SelectedIndexChanged += new EventHandler(cbCompany_SelectedIndexChanged);
            cbCompany.DataSource = Rmes.DA.Factory.CompanyFactory.GetAll();

            cbProductLine.DisplayMember = "PLINE_NAME";
            cbProductLine.ValueMember = "RMES_ID";
            cbProductLine.SelectedIndexChanged += new EventHandler(cbProductLine_SelectedIndexChanged);

            dgvStations.SelectionChanged += new EventHandler(dgvStations_SelectionChanged);

            
            DirectoryInfo dinfo = new DirectoryInfo(Application.StartupPath);
            FileInfo[] files = dinfo.GetFiles("*.dll");
            if (files.Count() > 0)
            {
                comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
                foreach (FileInfo f in files)
                {
                    comboBox1.Items.Add(f.Name);
                }
                comboBox1.SelectedIndexChanged += new EventHandler(comboBox1_SelectedIndexChanged);
            }

        }

        void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox2.Text = "";
            if (comboBox1.Text.Equals(string.Empty)) return;
            string file = Application.StartupPath + "\\" + comboBox1.Text;
            Type[] modules = Assembly.LoadFile(file).GetExportedTypes();
            comboBox2.Items.Clear();

            foreach (Type m in modules)
            {
                if (m.BaseType != null && (m.Equals(typeof(Rmes.WinForm.Base.BaseForm)) || m.BaseType.Equals(typeof(Rmes.WinForm.Base.BaseForm))))
                comboBox2.Items.Add(m.Namespace+"."+m.Name);
            }
            //throw new NotImplementedException();
        }

        void dgvStations_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvStations.SelectedRows.Count != 1) return;
            string stationid = dgvStations.SelectedRows[0].Cells["RMES_ID"].Value.ToString();
            List<Rmes.DA.Base.ConfigFormEntity> config = Rmes.DA.Base.BaseControlFactory.GetFormConfigFromStationCode(stationid);
            if (config.Count > 0)
            {
                stationform = config.First();
                textBox1.Text = stationform.FORMID;
                textBox1.Enabled = false;
                textBox2.Text = stationform.Left.ToString();
                textBox3.Text = stationform.Top.ToString();
                textBox5.Text = stationform.Width.ToString();
                textBox4.Text = stationform.Height.ToString();
                comboBox1.Text = stationform.AssembleFile;
                comboBox2.Text = stationform.AssembleString;
                checkBox1.Checked = stationform.FullScreen;
                checkBox2.Checked = stationform.Resizable;
            }
            else
            {
                stationform = null;
                textBox1.Text = "";
                textBox1.Enabled = true;
                textBox2.Text = "";
                textBox3.Text = "";
                textBox5.Text = "";
                textBox4.Text = "";
                comboBox1.SelectedIndex = -1;
                comboBox2.SelectedIndex = -1;
                checkBox1.Checked = false;
                checkBox2.Checked = false;
            }
            //throw new NotImplementedException();
        }

        void cbProductLine_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbCompany.SelectedValue != null && cbProductLine.SelectedValue != null)
            {
                string companycode = cbCompany.SelectedValue.ToString();
                string plinecode = cbProductLine.SelectedValue.ToString();
                dgvStations.DataSource = Rmes.DA.Factory.StationFactory.GetByProductLine(plinecode);
                this.Text = companycode + "," + plinecode;
                dgvStations.Columns[0].Visible = false;
                dgvStations.Columns[1].Visible = false;
                dgvStations.Columns[4].Visible = false;
                dgvStations.Columns[5].Visible = false;
                dgvStations.Columns[6].Visible = false;
            }
            //throw new NotImplementedException();
        }

        void cbCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cb = sender as ComboBox;
            if (cb == null) return;
            string companycode = cb.SelectedValue==null?"":cb.SelectedValue.ToString();
            if (companycode.Equals(string.Empty)) return;
            cbProductLine.DataSource = Rmes.DA.Factory.ProductLineFactory.GetByCompanyCode(companycode);
        }

        void frm_Config_StationForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            MDIParent1 parent = this.MdiParent as MDIParent1;
            if (parent == null) return;
            parent.编辑站点界面关系ToolStripMenuItem.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Equals(string.Empty)) return;
            if (stationform == null) stationform = new Rmes.DA.Base.ConfigFormEntity();
            try
            {
                stationform.FORMID = textBox1.Text; 
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
