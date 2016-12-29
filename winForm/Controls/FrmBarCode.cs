using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Rmes.WinForm.Controls
{
    public partial class FrmBarCode : Rmes.WinForm.Base.BaseForm
    {
        public FrmBarCode(Form owner)
        {
            InitializeComponent();
            this.FormClosing += new FormClosingEventHandler(issueForm_FormClosing);
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
            this.Text = "条码打印";
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
            ctrlprinting print = new ctrlprinting();
            print.Width = 1000;
            print.Height = 650;
            print.Top = 46;
            print.Left = 6;
            this.Controls.Add(print);
            this.Controls.Add(basicInfo);
            this.Controls.Add(navigation);
        }

        void issueForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.ExitThread();
        }

        void issueForm_FormClosing(object sender, FormClosingEventArgs e)
        {

        }
    }
}
