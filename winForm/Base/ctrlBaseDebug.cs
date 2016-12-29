using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Rmes.DA.Base;

namespace Rmes.WinForm.Base
{
    public partial class ctrlBaseDebug : BaseControl
    {
        bool debugmode = false;
        bool draging = false;
        bool sizing = false;
        int handlesize = 6;
        int gridsize = 4;
        Rectangle handle1;
        public ctrlBaseDebug()
        {
            InitializeComponent();
        }

        void ctrlBaseDebug_MouseClick(object sender, MouseEventArgs e)
        {
            if (!debugmode) return;
            this.Focus();
        }

        public object OBJ
        {
            get;
            set;
        }

        private Point mouse_offset;
        private void Common_MouseUp(object sender, MouseEventArgs e)
        {
            if (!debugmode) return;
            sizing = false;
            draging = false;
        }
        private void Common_MouseDown(object sender, MouseEventArgs e)
        {
            if (!debugmode) return;
            mouse_offset = new Point(-e.X, -e.Y);
            if (e.X > this.Width - handlesize && e.Y > this.Height - handlesize)
                sizing = true;
            else
                draging = true;
        }
        protected override void  OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (debugmode)
            {
                if(sizing)
                    Invalidate(handle1);
                handle1 = new Rectangle(this.Width - handlesize, this.Height - handlesize, handlesize, handlesize);
                Brush nb = Brushes.Yellow;
                e.Graphics.FillRectangle(nb, handle1);
            }
        }
        private void Common_MouseMove(object sender, MouseEventArgs e)
        {
            if (!debugmode) return;
            Control c = sender as Control;
            if (e.X > this.Width - handlesize && e.Y > this.Height - handlesize)
                c.Cursor = Cursors.SizeNWSE;
            else
                c.Cursor = Cursors.Hand;
            if (e.Button == MouseButtons.Left)
            {
                if (draging)
                {
                    Point mousePos = Control.MousePosition;
                    mousePos.Offset(mouse_offset.X, mouse_offset.Y);
                    mousePos.X = toGrid(mousePos.X);
                    mousePos.Y = toGrid(mousePos.Y);
                    c.Location = c.Parent.PointToClient(mousePos);
                    return;
                }
                if (sizing)
                {
                    c.SetBounds(this.Bounds.Left, this.Bounds.Top, toGrid(e.X), toGrid(e.Y));
                    return;
                }
            }
        }

        private int toGrid(int x)
        {
            int g = x % gridsize;
            if (g == 0) return x;
            if (g >= (gridsize / 2)) return x + gridsize - g;
            return x - g;
        }
        public void setText(string text)
        {
            label1.Text = text;
        }
        public void setDebug(bool md)
        {
            debugmode = md;
            if (debugmode)
            {
                this.MinimumSize = new Size(30, 30);
                this.MouseDown += new MouseEventHandler(this.Common_MouseDown);
                this.MouseUp += new MouseEventHandler(this.Common_MouseUp);
                this.MouseMove += new MouseEventHandler(this.Common_MouseMove);
                this.MouseClick += new MouseEventHandler(ctrlBaseDebug_MouseClick);
                this.Resize += new EventHandler(ctrlBaseDebug_Resize);
                this.Move += new EventHandler(ctrlBaseDebug_Move);
            }
        }

        void ctrlBaseDebug_Move(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (!debugmode) return;
            ConfigControlsEntity en = this.OBJ as ConfigControlsEntity;
            if (en == null) return;
            ((ConfigControlsEntity)this.OBJ).Top = this.Top<0?0:this.Top;
            ((ConfigControlsEntity)this.OBJ).Left = this.Left<0?0:this.Left;
        }

        void ctrlBaseDebug_Resize(object sender, EventArgs e)
        {
            if (!debugmode) return;
            ConfigControlsEntity en = this.OBJ as ConfigControlsEntity;
            if (en == null) return;
            ((ConfigControlsEntity)this.OBJ).Width = this.Width;
            ((ConfigControlsEntity)this.OBJ).Height = this.Height;
            //throw new NotImplementedException();
        }

        public void Save()
        {
            if (!debugmode) return;
            ConfigControlsEntity en = this.OBJ as ConfigControlsEntity;
            if (en != null)
            {
                BaseControlFactory.SaveFormControlsInfo(en);
            }
            base.Dispose();
        }
        

        private void label1_DoubleClick(object sender, EventArgs e)
        {
            if (!debugmode)
            {
                MessageBox.Show(label1.Text);
                return;
            }
        }        
    }
}
