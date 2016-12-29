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
    public partial class FrmShowProcessFile : Form
    {
        string _url = "";
        public FrmShowProcessFile(string url)
        {
            InitializeComponent();
            _url = url;
            webBrowser1.Url = new Uri(_url);
        }
    }
}
