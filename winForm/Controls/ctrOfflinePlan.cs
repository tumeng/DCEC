﻿using System;
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
    public partial class ctrOfflinePlan : BaseControl
    {
        string CompanyCode, PlineCode, StationCode, planCode, orderCode, WorkunitCode, StationName;
        string sn_flag;
        int PrevPlanNum = 0;
        string CurPlancode = "none", PrevPlancode = "none";
        //dataConn dc = new dataConn();
        public ctrOfflinePlan()
        {
            InitializeComponent();
            CompanyCode = LoginInfo.CompanyInfo.COMPANY_CODE;
            PlineCode = LoginInfo.ProductLineInfo.PLINE_CODE;
            StationCode = LoginInfo.StationInfo.STATION_CODE;
            StationName = LoginInfo.StationInfo.STATION_NAME;
            WorkunitCode = "";
            this.RMesDataChanged += new RMesEventHandler(ctrPlan_RMesDataChanged);
            GridPlan.AutoGenerateColumns = false;
            GridPlan.RowHeadersVisible = false;
            GridPlan.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            GridPlan.ReadOnly = true;

            initPlan();
        }
        void ctrPlan_RMesDataChanged(object obj, RMESEventArgs e)
        {
            if (e.MessageHead == null) return;
            if (e.MessageHead == "SN")
            {
                string sn = e.MessageBody.ToString();
                ProductInfoEntity ent = ProductInfoFactory.GetByCompanyCodeSNSingle(CompanyCode, sn);
                if (ent != null)
                {
                    planCode = ent.PLAN_CODE;
                    initPlan();
                    InitPlan(planCode);
                    return;
                }
            }
        }
        /// <summary>
        /// 此函数负责对比当前显示计划与计划表的差异，对比结果通知主线程
        /// </summary>
        private void initPlan()
        {
            //GridPlan.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //GridPlan.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //GridPlan.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //GridPlan.Columns[8].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            string plineid = LoginInfo.ProductLineInfo.RMES_ID;

            ///得到当前显示的计划集
            string sql = "SELECT plan_seq,product_model,plan_code,plan_so,plan_qty,online_qty,offline_qty,customer_name,begin_date,end_date,create_time,remark "
                 + " FROM data_plan  Where run_flag='Y' and confirm_flag='Y' and pline_code='" + PlineCode + "' AND to_char(end_date,'yyyymmdd')>=to_char(to_date('" + LoginInfo.WorkDate + "','yyyy-mm-dd'),'yyyymmdd')  AND to_char(begin_date,'yyyymmdd')<=to_char(to_date('" + LoginInfo.WorkDate + "','yyyy-mm-dd'),'yyyymmdd') order by begin_date,plan_seq ";
            DataTable dt = dataConn.GetTable(sql);

            GridPlan.DataSource = dt;
            GridPlan.ClearSelection();

            //List<PlanEntity> currentPlanSet = GridPlan.DataSource as List<PlanEntity>;
            //List<PlanEntity> oraclePlanSet = PlanFactory.GetByPlineCode(PlineCode);

            ////如果当前计划集与数据库计划集不同则刷新计划，如果相同则跳过
            //if (currentPlanSet != null)
            //{
            //    if (!currentPlanSet.Equals(oraclePlanSet))
            //        getPlan(PlineCode);
            //}
            //else { getPlan(PlineCode); }
        }
        private void getPlan(string plinecode)
        {
            //按序显示当前站点的可执行计划
            //List<PlanEntity> ds1 = PlanFactory.GetByPlineID(plineid);
            //修改为按生产线和班组取当日计划并排序：
            List<PlanEntity> ds2 = PlanFactory.GetByPlineCode(plinecode);

            if (ds2 != null && ds2.Count < 1) return;
            List<PlanEntity> ds1 = (from p in ds2
                                    where p.RUN_FLAG == "Y"
                                    orderby p.BEGIN_DATE ascending, p.PLAN_SEQ ascending
                                    select p).ToList<PlanEntity>();


            if (ds1.Count == 0) { GridPlan.DataSource = null; return; }
            List<PlanEntity> ds3 = ds1.GetRange(0, ds1.Count);
            foreach (PlanEntity s in ds3)
            {
                int complete_qty = ProductCompleteFactory.GetCompleteQtyByPlanStation(CompanyCode, PlineCode, s.PLAN_CODE, StationCode);
                if (complete_qty >= s.PLAN_QTY) ds1.Remove(s);
                //if (!TheProcessValid(s.PLAN_CODE)) ds1.Remove(s);
            }

            if (ds1.Count == 0) { GridPlan.DataSource = null; return; };
            PrevPlanNum = ds1.Count;

            GridPlan.DataSource = ds1;
            GridPlan.ClearSelection();
            GridPlan.Rows[0].Selected = true;

            PlanEntity en1 = ds1[0];
            string plan_code = en1.PLAN_CODE;
            InitPlan(plan_code);

            for (int i = 0; i < GridPlan.Rows.Count; i++)
            {
                //如果完工标识为Y则黄色
                string runFlag = GridPlan.Rows[i].Cells["colrunflag"].Value.ToString();

                //if (runFlag == "F")
                //{
                //    for (int j = 0; j < GridPlan.Columns.Count; j++)
                //        GridPlan.Rows[i].Cells[j].Style.BackColor = Color.Yellow;
                //}
                //如果完工标识为Y，实际上线数量>0则绿色
                int realOnlineQuantity = Convert.ToInt32(GridPlan.Rows[i].Cells["ColRealOnlineQuantity"].Value.ToString());
                if (realOnlineQuantity > 0 && runFlag == "Y")
                {
                    for (int j = 0; j < GridPlan.Columns.Count; j++)
                        GridPlan.Rows[i].Cells[j].Style.BackColor = Color.Green;
                }
                //挂起
                if (runFlag == "G")
                {
                    for (int j = 0; j < GridPlan.Columns.Count; j++)
                        GridPlan.Rows[i].Cells[j].Style.BackColor = Color.Red;
                }
            }

        }


        /// <summary>
        /// 实时监视站点计划任务的改变
        /// 如果有变化则刷新数据
        /// 否则不刷新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            initPlan();
        }
        /// <summary>
        /// 单击弹出新form，显示选中计划的流水号列表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GridPlan_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(GridPlan.Rows[e.RowIndex].Cells["ColPlanCode"].Value as string)) return;
            string plan_code = GridPlan.Rows[e.RowIndex].Cells["ColPlanCode"].Value.ToString();
            InitPlan(plan_code);
            //timer1.Enabled = false;
        }

        private void GridPlan_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //得到计划号
            if (e.RowIndex < 0) return;

            //timer1.Enabled = false;
            //if (string.IsNullOrWhiteSpace(GridPlan.Rows[e.RowIndex].Cells["ColPlanCode"].Value as string)) return;
            //string plan_code = GridPlan.Rows[e.RowIndex].Cells["ColPlanCode"].Value.ToString();
            //string item_code = GridPlan.Rows[e.RowIndex].Cells["colPlanSo"].Value.ToString();
            //string batch_code = GridPlan.Rows[e.RowIndex].Cells["colPlanBatch"].Value.ToString();
            //string order_code = GridPlan.Rows[e.RowIndex].Cells["colOrderCode"].Value.ToString();
            //string sn_flag=GridPlan.Rows[e.RowIndex].Cells["colSnFlag"].Value.ToString();

            //if (!TheProcessValid(plan_code))
            //{
            //    MessageBox.Show("前一工序没有完成！", "工序错误提示");
            //    return;
            //}

            //List<PlanSnEntity> lstsn = PlanSnFactory.GetSnByPlanCode(plan_code);
            //if (lstsn.Count > 0)//如果已经有SN在data_plan_sn表中
            //{
            //    FrmShowPlanSn ps = new FrmShowPlanSn(plan_code, batch_code);
            //    ps.Show();
            //}
            //else//这是另一种逻辑，没有预分配SN，则不按序列号管理。
            //{

            //    RMESEventArgs thisEventArg = new RMESEventArgs();
            //    thisEventArg.MessageHead = "PLAN";
            //    thisEventArg.MessageBody = plan_code;
            //   SendDataChangeMessage(thisEventArg);

            //}
        }

        private void InitPlan(string PlanCode)
        {
            PlanEntity en1 = PlanFactory.GetByKey(PlanCode);
            planCode = PlanCode;
            sn_flag = en1.SN_FLAG;
            CurPlancode = PlanCode;
        }

    }
}
