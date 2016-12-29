using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Rmes.WinForm.Controls
{
    public partial class FrmDetect : Rmes.WinForm.Base.BaseForm
    {
        public FrmDetect(Form owner)
        {
            InitializeComponent();
            this.FormClosing += new FormClosingEventHandler(frmQuality_FormClosing);
            this.FormClosed += new FormClosedEventHandler(issueForm_FormClosed);
            this.Width = owner.Width;
            this.Height = owner.Height;
            this.Top = owner.Top;
            this.Left = owner.Left;
            this.FormBorderStyle = owner.FormBorderStyle;
            this.WindowState = owner.WindowState;
            this.Bounds = owner.Bounds;
            this.StartPosition = owner.StartPosition;
            this.FormBorderStyle = owner.FormBorderStyle;
            this.MaximizeBox = owner.MaximizeBox;
            this.MinimizeBox = owner.MinimizeBox;
            this.Text = "质检";
            ctrlNavigation navigation = new ctrlNavigation();
            navigation.Width = 88;
            navigation.Height = 36;
            navigation.Top = 0;
            navigation.Left = 922;
            ctrlBasicInfo basicInfo = new ctrlBasicInfo();
            basicInfo.Width = 910;
            basicInfo.Height = 36;
            basicInfo.Top = 0;
            basicInfo.Left = 0;
            ctrlProductScan scan = new ctrlProductScan();
            scan.Width = 464;
            scan.Height = 36;
            scan.Top = 42;
            scan.Left = 0;


            this.Controls.Add(scan);
            this.Controls.Add(basicInfo);
            this.Controls.Add(navigation);
        }

        void issueForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.ExitThread();
        }

        void frmQuality_FormClosing(object sender, FormClosingEventArgs e)
        {

        }
    }
}
