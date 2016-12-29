using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Rmes.DA.Entity;
using Rmes.DA.Base;
using System.Data;
using Rmes.Pub.Data;
using Rmes.DA.Factory;


namespace Rmes.WebApp.Rmes.Exp.Exp2400
{
    public partial class Report_Exp2400 : DevExpress.XtraReports.UI.XtraReport
    {
        public  Report_Exp2400(DateTime time,string dept)
        {
            InitializeComponent();
            dataConn theconn = new dataConn();
            string _b = time.AddDays(-time.Day + 1).ToShortDateString() + " 00:00:00";
            string _e = time.AddMonths(1).AddDays(-time.Day + 1).ToShortDateString() + " 00:00:00";
            string dept_name = theconn.GetValue("select dept_name from code_dept where dept_code='" + dept + "'");
            xrLabel2.Text = dept_name + time.Month + "月完工成品报表";
            xrLabel3.Text = DateTime.Now.ToShortDateString();

            string sql = string.Format("select t.*,a.period_qty from VW_DATA_DEPT_COMPLETE_1 t  left join (select project_code,product_series,temp_period_qty as period_qty from VW_DATA_DEPT_COMPLETE_1 where work_date between to_date('{0}','yyyy-mm-dd hh24:mi:ss') and to_date('{1}','yyyy-mm-dd hh24:mi:ss') ) a on a.project_code=t.project_code and a.product_series=t.product_series where t.dept_code='{2}' and t.work_date between to_date('{3}','yyyy-mm-dd hh24:mi:ss') and to_date('{4}','yyyy-mm-dd hh24:mi:ss')", _b, _e, dept, _b, _e);

            this.xrPivotGrid1.DataSource = theconn.GetTable(sql);

        }

    }
}
