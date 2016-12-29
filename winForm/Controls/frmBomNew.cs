using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Rmes.WinForm.Controls
{
    public partial class frmBomNew : Form
    {
        public frmBomNew(string pPlanCode,string pSn,string pStationCode)
        {
            InitializeComponent();
            this.ctrlBomItemNew1 = new Rmes.WinForm.Controls.ctrlBomItemNew(pPlanCode,pSn,pStationCode);
            this.Controls.Add(ctrlBomItemNew1);
            this.Width = ctrlBomItemNew1.Width;
            this.Height = ctrlBomItemNew1.Height + 30;
        }

        private void frmBomNew_Load(object sender, EventArgs e)
        {

        }
    }
}
