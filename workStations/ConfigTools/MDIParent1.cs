using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Rmes.DA.Base;
using System.Configuration;
using PetaPoco;

namespace Rmes.Workstation.ConfigTools
{
    public partial class MDIParent1 : Form
    {
        //private int childFormNumber = 0;
        
        public MDIParent1()
        {
            InitializeComponent();
            foreach (ConnectionStringSettings set1 in ConfigurationManager.ConnectionStrings)
            {
                ToolStripMenuItem item = new ToolStripMenuItem();
                item.Text = set1.Name;
                item.Name = set1.Name;
                item.ToolTipText = set1.ConnectionString;
                item.Click += new EventHandler(item_Click);
                item.CheckStateChanged += new EventHandler(item_CheckStateChanged);
                MenuItemDataBase.DropDownItems.Add(item);
            }
            DB.DataConnectionName = "";
        }

        void item_CheckStateChanged(object sender, EventArgs e)
        {
            ToolStripMenuItem item = (ToolStripMenuItem)sender;
            if (((ToolStripMenuItem)sender).CheckState.Equals(CheckState.Checked))
            {
                DB.DataConnectionName = item.Name;
                Database db =  DB.GetInstance();
                if (db == null) return;
                foreach (ToolStripMenuItem item1 in MenuItemDataBase.DropDownItems)
                {
                    item1.Enabled = false;
                }
            }
            //throw new NotImplementedException();
        }

        void item_Click(object sender, EventArgs e)
        {
            foreach (ToolStripMenuItem item in MenuItemDataBase.DropDownItems)
            {
                item.CheckState = CheckState.Unchecked;
            }
            ((ToolStripMenuItem)sender).CheckState = CheckState.Checked;
            //throw new NotImplementedException();
        }

        private void 编辑站点界面关系ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frm_Config_StationForm form1 = new frm_Config_StationForm();
            form1.MdiParent = this;
            form1.Show();
            ((ToolStripMenuItem)sender).Enabled = false;
        }

        private void 编辑基础数据表ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frm_Config_BaseData form2 = new frm_Config_BaseData();
            form2.MdiParent = this;
            form2.Show();
            ((ToolStripMenuItem)sender).Enabled = false;
        }

        private void 测试SpeechLibToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frm_Test_Speech formSpeech = new frm_Test_Speech();
            formSpeech.MdiParent = this;
            formSpeech.Show();
            ((ToolStripMenuItem)sender).Enabled = false;
        }

        private void 代码生成EntityToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frm_Gen_Entities form1 = new frm_Gen_Entities();
            form1.MdiParent = this;
            form1.Show();
        }
    }
}
