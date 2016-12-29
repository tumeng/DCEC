using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Rmes.DA.Base;

namespace Rmes.WinForm.Base
{
    public partial class BaseForm : Form
    {
        //RMESEventArgs arg = new RMESEventArgs();

        public BaseForm()
        {
            InitializeComponent();
        }
        public void LoadConfig(ConfigFormEntity FormConfig)
        {
            if (FormConfig != null)
            {
                this.MaximizeBox = false;
                this.MinimizeBox = false;

                if (FormConfig.FullScreen)
                {
                    this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                    this.WindowState = FormWindowState.Maximized;
                    this.Bounds = Screen.PrimaryScreen.Bounds;
                }
                else
                {
                    this.StartPosition = FormStartPosition.Manual;
                    this.Top = FormConfig.Top;
                    this.Left = FormConfig.Left;
                    this.Width = FormConfig.Width;
                    this.Height = FormConfig.Height;



                    if (FormConfig.Resizable)
                    {
                        this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
                        this.MaximizeBox = true;
                        this.MinimizeBox = true;
                    }
                    else
                    {
                        this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
                        this.MinimizeBox = true;
                    }
                }
            }
            else throw new Exception("Form Config is Null");
        }
    }
}
