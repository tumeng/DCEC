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
    public partial class FrmShowCompleteStatistics : Form
    {
        public FrmShowCompleteStatistics()
        {
            InitializeComponent();
            this.ctrlPlan = new Rmes.WinForm.Controls.ctrlStaComStatistics();
            ctrlPlan.Dock = DockStyle.Fill;
            //ctrlPlan.Width = 1400;
            //ctrlPlan.Height = 650;
            this.Controls.Add(ctrlPlan);
        }
    }
}
