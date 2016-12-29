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
    public partial class ctrlProductRetirement :BaseControl
    {
        private string VIN="";
        private Boolean _quality;
        private Boolean Quality
        {
            get { return _quality; }
            set { _quality = value; SetButtonText(); }
        }
        public ctrlProductRetirement()
        {
            InitializeComponent();
            this.RMesDataChanged += new RMesEventHandler(ctrlProductRetirement_RMesDataChanged);
            Quality = true;
        }
        void ctrlProductRetirement_RMesDataChanged(object obj, Rmes.WinForm.Base.RMESEventArgs e)
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
            VIN = e.MessageBody.ToString() ;
        }
        protected void GetQualityStatus(object obj, Rmes.WinForm.Base.RMESEventArgs e)
        {
           
        }

        protected void SetButtonText()
        {
            if (_quality)
            {
                btnRetirement.Text = "正常(&N)";
                btnRetirement.BackColor = System.Drawing.Color.LightGreen;
            }
            else
            {
                btnRetirement.Text = "报废(&R)";
                btnRetirement.BackColor = System.Drawing.Color.HotPink;
            }
        }
        private void btnQuality_Click(object sender, EventArgs e)
        {
            Quality = !Quality;
          
        }
    }
}
