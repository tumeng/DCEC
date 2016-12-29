using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;

namespace Rmes.WebApp.Rmes.Exp.Exp2300
{
    public partial class Report_Exp2300 : DevExpress.XtraReports.UI.XtraReport
    {
        public Report_Exp2300(DataTable dt)
        {
            InitializeComponent();
            xrPivotGrid1.DataSource = dt;
        }

    }
}
