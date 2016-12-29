using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Rmes.WinForm.Base
{
    public partial class BaseControl : UserControl
    {
        public BaseControl()
        {
            InitializeComponent();
        }

        public event RMesEventHandler RMesDataChanged;
        public virtual void OnRMesDataChanged(RMESEventArgs e)
        {
            if (RMesDataChanged != null)
            {
                RMesDataChanged(this, e);
            }
        }
        public void SendDataChangeMessage(RMESEventArgs e)
        {
            UiFactory.CallDataChanged(this, e);
        }
    }

}
