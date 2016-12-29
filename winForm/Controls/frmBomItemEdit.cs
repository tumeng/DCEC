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
    public partial class frmBomItemEdit : Form
    {
        public frmBomItemEdit(string RmesID)
        {
            InitializeComponent();
            this.ctrlBomItemEdit1 = new Rmes.WinForm.Controls.ctrlBomItemEdit(RmesID);
            ctrlBomItemEdit1.Dock = DockStyle.Fill;
            this.Controls.Add(ctrlBomItemEdit1);
        }
        //public frmBomItemEdit(string RmesID,string batchcode1)
        //{
        //    InitializeComponent();
        //    this.ctrlBomItemEdit1 = new Rmes.WinForm.Controls.ctrlBomItemEdit(RmesID, batchcode1);
        //    this.Controls.Add(ctrlBomItemEdit1);
        //}
        public frmBomItemEdit(string RmesID,string VendorCode,string BatchCode,string type,string barcode)
        {
            InitializeComponent();
            this.ctrlBomItemEdit1 = new Rmes.WinForm.Controls.ctrlBomItemEdit(RmesID, VendorCode, BatchCode,type,barcode);
            this.Controls.Add(ctrlBomItemEdit1);
        }
        public frmBomItemEdit()
        {
            InitializeComponent();
            this.ctrlBomItemEdit1 = new Rmes.WinForm.Controls.ctrlBomItemEdit();
            this.Controls.Add(ctrlBomItemEdit1);
        }
        private void frmBomItemEdit_Load(object sender, EventArgs e)
        {

        }
       
    }
}
