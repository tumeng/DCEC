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
    public partial class FrmShowGZqd : Form
    {
        public FrmShowGZqd(string sn1,string plancode1)
        {
            InitializeComponent();
            this.ctrl_fxbomcom1 = new Rmes.WinForm.Controls.ctrl_fxbomcom(sn1,plancode1);
            ctrl_fxbomcom1.Dock = DockStyle.Fill;
            this.Controls.Add(ctrl_fxbomcom1);
        }
    }
}
