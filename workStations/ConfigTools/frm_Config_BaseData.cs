using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PetaPoco;

namespace Rmes.Workstation.ConfigTools
{
    public partial class frm_Config_BaseData : Form
    {
        public frm_Config_BaseData()
        {
            InitializeComponent();
            cmbTables.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbTables.SelectedValueChanged += new EventHandler(cmbTables_SelectedValueChanged);
        }

        void cmbTables_SelectedValueChanged(object sender, EventArgs e)
        {
            string tbName = cmbTables.SelectedValue.ToString();
            if (tbName.Equals(string.Empty)) return;
            Database db = Rmes.DA.Base.DB.GetInstance();
            List<string> columns = db.Fetch<string>("select column_name from user_tab_cols where table_name=@0",tbName);
            dataGridView1.DataSource = null;
            dataGridView1.Columns.Clear();
            dataGridView1.Rows.Clear();
            dataGridView1.MultiSelect = false;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            
            List<dynamic> rows = db.Fetch<dynamic>("select * from " + tbName);
            if (rows.Count > 0)
            {
                dataGridView1.DataSource = ToDataTable(rows);
                dataGridView1.AllowUserToAddRows = true;
                dataGridView1.AllowUserToDeleteRows = true;
            }
            else
            {
                foreach (string c in columns)
                {
                    dataGridView1.Columns.Add(c, c);
                }
            }
            //List<arr> = db.Fetch<typeof(Array)>("select * from @0",tbName);
            //Rmes.Public.ErrorHandle.EH.ERROR(colsnum.ToString());
            //throw new NotImplementedException();
        }

        private DataTable ToDataTable(IEnumerable<dynamic> items)
        {
            var data = items.ToArray();
            if (data.Count() == 0) return null;
            var dt = new DataTable();
            foreach (var key in ((IDictionary<string, object>)data[0]).Keys)
            {
                dt.Columns.Add(key);
            }
            foreach (var d in data)
            {
                dt.Rows.Add(((IDictionary<string, object>)d).Values.ToArray());
            }
            return dt;
        }
        private void frm_Config_BaseData_FormClosed(object sender, FormClosedEventArgs e)
        {
            MDIParent1 parent = this.MdiParent as MDIParent1;
            if (parent == null) return;
            parent.编辑基础数据表ToolStripMenuItem.Enabled = true;
        }

        private void frm_Config_BaseData_Load(object sender, EventArgs e)
        {
           Database db = Rmes.DA.Base.DB.GetInstance();
           cmbTables.DataSource = db.Fetch<string>("select TABLE_NAME from user_tables where dropped=@0 order by table_name","NO");

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            dataGridView1.EndEdit();
            Database db = Rmes.DA.Base.DB.GetInstance();
            foreach (DataGridViewRow r in dataGridView1.Rows)
            {
                if (!r.IsNewRow)
                {
                    dynamic a = new System.Dynamic.ExpandoObject();
                    string keyColName = "";
                    string keyColValue = "";
                    foreach (DataGridViewCell c in r.Cells)
                    {
                        string key = dataGridView1.Columns[c.ColumnIndex].Name;
                        string val = c.Value.ToString();
                        if (key.Equals("RMES_ID"))
                        {
                            keyColName = key;
                            keyColValue = val;
                        }
                        ((IDictionary<string, object>)a).Add(key, val);
                    }
                    if (keyColName.Equals(string.Empty)) return;
                    string aa=db.ExecuteScalar<string>("select RMES_ID from " + cmbTables.SelectedValue.ToString() + " where RMES_ID=@0", keyColValue);

                    if (aa==null || aa.Equals(string.Empty))
                    {
                        object b = db.Insert(cmbTables.SelectedValue.ToString(), keyColName, false, a);
                        MessageBox.Show(b.ToString(), "insert");
                    }
                    else
                    {
                        
                        int kk = db.Update(cmbTables.SelectedValue.ToString(), keyColName, a);
                        MessageBox.Show(kk.ToString(), "udate");
                    }
                }
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            dataGridView1.EndEdit();
            Database db = Rmes.DA.Base.DB.GetInstance();
            foreach (DataGridViewRow r in dataGridView1.SelectedRows)
            {
                if (!r.IsNewRow)
                {
                    dynamic a = new System.Dynamic.ExpandoObject();
                    string keyColName = "";
                    foreach (DataGridViewCell c in r.Cells)
                    {
                        string key = dataGridView1.Columns[c.ColumnIndex].Name;
                        string val = c.Value.ToString();
                        if (key.Equals("RMES_ID")) keyColName = key;
                        ((IDictionary<string, object>)a).Add(key, val);
                    }
                        int b = db.Delete(cmbTables.SelectedValue.ToString(), keyColName, a);
                        MessageBox.Show(b.ToString(), "Delete");
                    //Rmes.Public.ErrorHandle.EH.Info(r.Cells[0].Value.ToString());
                }
            }

        }
    }
}
