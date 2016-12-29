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
namespace Rmes.WinForm.Controls
{
    /// <summary>
    /// 作者：thl
    /// 控件版本：v1.0
    /// 控件描述：
    /// 1、实时显示站点完成统计情况
    /// 
    /// </summary>
    public partial class ctrl_station_statistics : BaseControl
    {
        string CompanyCode, PlineCode, StationCode,ShiftCode,Team_Code, planCode, orderCode, WorkunitCode, StationName;
        string sn_flag;
        int PrevPlanNum = 0;
        string CurPlancode = "none", PrevPlancode = "none";
        //dataConn dc = new dataConn();
        public ctrl_station_statistics()
        {
            InitializeComponent();
            CompanyCode = LoginInfo.CompanyInfo.COMPANY_CODE;
            PlineCode = LoginInfo.ProductLineInfo.PLINE_CODE;
            StationCode = LoginInfo.StationInfo.STATION_CODE;
            StationName = LoginInfo.StationInfo.STATION_NAME;
            ShiftCode = LoginInfo.ShiftInfo.SHIFT_CODE;
            Team_Code = LoginInfo.TeamInfo.TEAM_CODE;
            WorkunitCode = "";
            this.RMesDataChanged += new RMesEventHandler(ctrStatis_RMesDataChanged);
            GridPlan.AutoGenerateColumns = false;
            GridPlan.RowHeadersVisible = false;
            GridPlan.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            GridPlan.ReadOnly = true;

            initPlan();
        }
        void ctrStatis_RMesDataChanged(object obj, RMESEventArgs e)
        {
            if (e.MessageHead == "SN" || e.MessageHead == "ONLINE" || e.MessageHead == "OFFLINE" || e.MessageHead == "REFRESHPLAN")
            {
                initPlan();
            }
        }
        private void initPlan()
        {
            GridPlan.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            GridPlan.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            GridPlan.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            GridPlan.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            GridPlan.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            string plineid = LoginInfo.ProductLineInfo.RMES_ID;
            GridPlan.DataSource = null;
            ///得到当前显示的计划集
            string sql = "select rmes_id,plan_code,plan_so,product_model,complete_qty,pline_code,station_code,team_code,shift_code,user_id,work_date,start_time,plan_qty,customer_name from data_complete_statistics t where work_date=to_date('"+LoginInfo.WorkDate+"','yyyy-mm-dd') and shift_code='"+ShiftCode+"' and team_code='"+Team_Code+"' and station_code='"+StationCode+"' order by plan_code desc ";
            DataTable dt = dataConn.GetTable(sql);
            if (dt.Rows.Count == 0) { GridPlan.DataSource = null; return; };
            GridPlan.DataSource = dt;
            GridPlan.ClearSelection();
            GridPlan.Rows[0].Selected = true;
        }

    }
}
