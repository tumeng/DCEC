using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Rmes.DA.Base;
using Rmes.DA.Entity;
using Rmes.DA.Factory;
using Rmes.WinForm.Base;


namespace Rmes.WinForm.Controls
{
    public partial class frmBomItemMng : Form
    {
        public frmBomItemMng(string pPlanCode,string pSn,List<SNBomTempEntity> pDs)
        {
            InitializeComponent();
            this.ctrlBomItemMng1 = new Rmes.WinForm.Controls.ctrlBomItemMng(pPlanCode,pSn,pDs);
            this.Controls.Add(ctrlBomItemMng1);
            this.Width = ctrlBomItemMng1.Width;
            this.Height = ctrlBomItemMng1.Height + 30;

        }

        private void frmBomItemMng_Load(object sender, EventArgs e)
        {

        }
    }
}
