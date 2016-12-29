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
    public partial class FrmShowProcessPic : Form
    {
        public FrmShowProcessPic()
        {
            InitializeComponent();
            this.ctrlPrcPic = new Rmes.WinForm.Controls.ctrlProcessPic();
            this.Controls.Add(ctrlPrcPic);

            int count = this.Controls.Count * 2 + 2;
            float[] factor = new float[count];
            int i = 0;
            factor[i++] = Size.Width;
            factor[i++] = Size.Height;
            foreach (Control ctrl in this.Controls)
            {
                factor[i++] = ctrl.Location.X / (float)Size.Width;
                factor[i++] = ctrl.Location.Y / (float)Size.Height;
                ctrl.Tag = ctrl.Size;
            }
            Tag = factor;
        }
        public FrmShowProcessPic(string type,string oldplinecode)
        {
            InitializeComponent();
            this.ctrlPrcPic = new Rmes.WinForm.Controls.ctrlProcessPic(type,oldplinecode);
            this.Controls.Add(ctrlPrcPic);

            int count = this.Controls.Count * 2 + 2;
            float[] factor = new float[count];
            int i = 0;
            factor[i++] = Size.Width;
            factor[i++] = Size.Height;
            foreach (Control ctrl in this.Controls)
            {
                factor[i++] = ctrl.Location.X / (float)Size.Width;
                factor[i++] = ctrl.Location.Y / (float)Size.Height;
                ctrl.Tag = ctrl.Size;
            }
            Tag = factor;
        }
        public FrmShowProcessPic(string type)
        {
            InitializeComponent();
            this.ctrlPrcPic = new Rmes.WinForm.Controls.ctrlProcessPic(type);
            this.Controls.Add(ctrlPrcPic);

            int count = this.Controls.Count * 2 + 2;
            float[] factor = new float[count];
            int i = 0;
            factor[i++] = Size.Width;
            factor[i++] = Size.Height;
            foreach (Control ctrl in this.Controls)
            {
                factor[i++] = ctrl.Location.X / (float)Size.Width;
                factor[i++] = ctrl.Location.Y / (float)Size.Height;
                ctrl.Tag = ctrl.Size;
            }
            Tag = factor;
        }
        private void Form1_Resize(object sender, EventArgs e)
        {
            try
            {
                float[] scale = (float[])Tag;
                int i = 2;

                foreach (Control ctrl in this.Controls)
                {
                    ctrl.Left = (int)(Size.Width * scale[i++]);
                    ctrl.Top = (int)(Size.Height * scale[i++]);
                    ctrl.Width = (int)(Size.Width / (float)scale[0] * ((Size)ctrl.Tag).Width);
                    ctrl.Height = (int)(Size.Height / (float)scale[1] * ((Size)ctrl.Tag).Height);

                    //每次使用的都是最初始的控件大小，保证准确无误。
                }
            }
            catch
            { }
        }
    }
}
