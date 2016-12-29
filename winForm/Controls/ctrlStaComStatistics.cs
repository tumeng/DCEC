using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Rmes.WinForm.Controls;
using Rmes.WinForm.Base;
using Rmes.DA.Base;
using Rmes.DA.Entity;
using Rmes.DA.Factory;
using System.Reflection;
using Microsoft.VisualBasic;
using Rmes.Pub.Data1;
using Rmes.DA.Procedures;

namespace Rmes.WinForm.Controls
{
    //显示站点完成情况及统计
    public partial class ctrlStaComStatistics : BaseControl
    {
        public string SN, So, productmodel;
        public string planCode, orderCode;
        public string companyCode, PlineCode, Plineid, stationCode, stationtype, WorkunitCode, LinesideStock, stationname, pline_type, Station_Code, stationcode_fx, stationname_fx;
        public bool IsABC = false;
        //dataConn dc = new dataConn();
        public ctrlStaComStatistics()
        {
            InitializeComponent();
            showData();
        }
        private void showData()
        {
            dgv1.DataSource = null;
            dgv2.DataSource = null;
            dgv3.DataSource = null;
            dgv1.AutoGenerateColumns = false;
            dgv1.RowHeadersVisible = false;
            dgv2.AutoGenerateColumns = false;
            dgv2.RowHeadersVisible = false;
            dgv3.AutoGenerateColumns = false;
            dgv3.RowHeadersVisible = false;

            string sql = string.Format("select * FROM vw_data_complete t where t.SHIFT_CODE='{0}' "
                                        + "and t.team_code='{1}' and t.station_code='{2}' and "
                                        + "T.START_TIME>TO_DATE(to_char(SYSDATE,'yyyy/mm/dd')||'00:00:00','yyyy/mm/dd hh24:mi:ss')",
                                        LoginInfo.ShiftInfo.SHIFT_CODE,
                                        LoginInfo.TeamInfo.TEAM_CODE, LoginInfo.StationInfo.STATION_CODE);
            DataTable dt = dataConn.GetTable(sql);

            sql=string.Format(" select t.plan_code,a.plan_qty,count(distinct sn) complete_qty from data_complete t "
              +"  left join data_plan a on t.plan_code=a.plan_code "
                                    + "  where station_code='{2}' and t.plan_code in  (select distinct plan_code FROM vw_data_complete t where t.SHIFT_CODE='{0}' "
                                        + "and t.team_code='{1}' and t.station_code='{2}' and "
                                        + "T.START_TIME>TO_DATE(to_char(SYSDATE,'yyyy/mm/dd')||'00:00:00','yyyy/mm/dd hh24:mi:ss')) group by t.plan_code,a.plan_qty ",
                                        LoginInfo.ShiftInfo.SHIFT_CODE,
                                        LoginInfo.TeamInfo.TEAM_CODE, LoginInfo.StationInfo.STATION_CODE);
            DataTable dt1 = dataConn.GetTable(sql);

            sql = string.Format(" select t.plan_code,a.plan_qty,count(distinct sn) complete_qty from data_complete t "
              + "  left join data_plan a on t.plan_code=a.plan_code "
                                    + "  where  t.SHIFT_CODE='{0}' "
                                        + "and t.team_code='{1}' and t.station_code='{2}' and "
                                        + "T.START_TIME>TO_DATE(to_char(SYSDATE,'yyyy/mm/dd')||'00:00:00','yyyy/mm/dd hh24:mi:ss') group by t.plan_code,a.plan_qty ",
                                        LoginInfo.ShiftInfo.SHIFT_CODE,
                                        LoginInfo.TeamInfo.TEAM_CODE, LoginInfo.StationInfo.STATION_CODE);
            DataTable dt2 = dataConn.GetTable(sql);

            dgv1.DataSource = dt2;
            dgv2.DataSource = dt;
            dgv3.DataSource = dt1;
            dgv1.ClearSelection();
            dgv2.ClearSelection();
            dgv3.ClearSelection();
        }

    }
}
