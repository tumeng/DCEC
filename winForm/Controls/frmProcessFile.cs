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
    public partial class frmProcessFile : Form
    {
        public frmProcessFile(string sn)
        {
            InitializeComponent();
            InitializeComponent();
            this.ctrlPfile = new Rmes.WinForm.Controls.ctrlProcessFile(sn);
            this.Controls.Add(ctrlPfile);
        }
    }
}
