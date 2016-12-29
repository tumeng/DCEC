using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Rmes.Pub.Data1;

namespace Rmes.WinForm.Controls
{
    public partial class FrmSkip : Rmes.WinForm.Base.BaseForm
    {
        string plinecode = "";
        public FrmSkip(string plinecode1)
        {
            InitializeComponent();
            plinecode = plinecode1;
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 13) return;
            if (string.IsNullOrWhiteSpace(textBox1.Text)) return;




        }
    }
}
