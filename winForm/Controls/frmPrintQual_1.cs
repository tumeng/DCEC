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
    public partial class frmPrintQual_1 : Rmes.WinForm.Base.BaseForm
    {
        public frmPrintQual_1(Form owner)
        {
            InitializeComponent();
            this.FormClosed += new FormClosedEventHandler(frmPrintQual_1_FormClosed);
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
            this.Text = "开关柜质检报告打印";
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
            ctrlPrintQual_1 print = new ctrlPrintQual_1();
            print.Width = owner.Width;
            print.Height = owner.Height;
            print.Top = 46;
            print.Left = 6;
            this.Controls.Add(print);
            this.Controls.Add(basicInfo);
            this.Controls.Add(navigation);

        }

        void frmPrintQual_1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.ExitThread();
        }
    }
}
