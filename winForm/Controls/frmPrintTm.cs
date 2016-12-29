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
    //通用条码打印窗体
    public partial class frmPrintTm : Form
    {
        public frmPrintTm()
        {
            InitializeComponent();
            this.ctrlPrint = new Rmes.WinForm.Controls.ctrl_printTm();
            this.Controls.Add(ctrlPrint);
        }
        public frmPrintTm(string thesn,string theplan)
        {
            InitializeComponent();
            this.ctrlPrint1 = new Rmes.WinForm.Controls.ctrl_printTmISDE(thesn,theplan);
            this.Controls.Add(ctrlPrint1);
        }
    }
}
