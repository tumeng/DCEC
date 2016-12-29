using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Rmes.WinForm.Base;
using Rmes.DA.Base;
namespace Rmes.WinForm.Controls
{
    public partial class ctrlProductQuality :BaseControl
    {
        private Boolean _quality = true;
        private Boolean Quality
        { get { return _quality;  } set { _quality = value; SetButtonText(); } }
        public ctrlProductQuality()
        {
            InitializeComponent();
            Quality = true;
            this.RMesDataChanged += new RMesEventHandler(ctrlProductQuality_RMesDataChanged);
        }
        void ctrlProductQuality_RMesDataChanged(object obj, Rmes.WinForm.Base.RMESEventArgs e)
        {
            switch (e.MessageHead)
            {
                case "VIN":
                    VINScanned(obj, e);
                    break;
                default:
                    break;
            }
        }
        protected void VINScanned(object obj, Rmes.WinForm.Base.RMESEventArgs e)
        {
            Quality = true;
        }
        protected void GetQualityStatus(object obj, Rmes.WinForm.Base.RMESEventArgs e)
        {
           
        }
        protected void SetButtonText()
        {
            if (_quality)
            {
                btnQuality.Text = "合格(&G)";
                btnQuality.BackColor = System.Drawing.Color.LightGreen;
            }
            else
            {
                btnQuality.Text = "不合格(&B)";
                btnQuality.BackColor = System.Drawing.Color.Yellow;
            }
        }
        private void btnQuality_Click(object sender, EventArgs e)
        {
            Quality = !Quality;
          
        }
    }
}
