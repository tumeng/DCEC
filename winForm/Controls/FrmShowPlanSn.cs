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
    public partial class FrmShowPlanSn : Form
    {
        public FrmShowPlanSn(string planID,string batchCode)
        {
            InitializeComponent();

        this.ctrPlanSn1 = new Rmes.WinForm.Controls.ctrPlanSn(planID, batchCode);
        this.ctrPlanSn1.Dock = DockStyle.Fill;
        this.Controls.Add(ctrPlanSn1);
        }
    }
}
