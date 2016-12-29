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

namespace Rmes.WinForm.Controls
{
/// <summary>
/// 作者：徐莹
/// 控件版本：v1.0
/// 控件描述：
/// 1、实时扫描数据库当前站点可执行计划，如有变动则刷新界面
/// 2、鼠标左键单击计划，弹出新form，显示选中计划包含的流水号
/// 
/// </summary>
    public partial class ctrQualityPlan : BaseControl
    {
        public ctrQualityPlan()
        {
            InitializeComponent();
            this.RMesDataChanged += new RMesEventHandler(ctrPlan_RMesDataChanged);
            GridPlan.AutoGenerateColumns = false;
            GridPlan.RowHeadersVisible = false;
            GridPlan.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            GridPlan.ReadOnly = true;
        }

        void ctrPlan_RMesDataChanged(object obj, RMESEventArgs e)
        {
            if (e.MessageHead == "MESLL")
            {
                this.Visible = false;
                return;
            }
            if (e.MessageHead.ToString() == "WORK")
            {
                this.Visible = true;
                return;
            }
        }
        /// <summary>
        /// 此函数负责对比当前显示计划与计划表的差异，对比结果通知主线程
        /// </summary>
        private void initPlan()
        {
            GridPlan.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            GridPlan.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            GridPlan.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            GridPlan.Columns[8].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            
            string plineid = LoginInfo.ProductLineInfo.RMES_ID;

            ///得到当前显示的计划集
            List<PlanEntity> currentPlanSet = GridPlan.DataSource as List<PlanEntity>;

            ///如果当前计划集与数据库计划集不同则刷新计划，如果相同则跳过

            List<PlanEntity> oraclePlanSet = PlanFactory.GetCurrentRunByPline(plineid);
            if (currentPlanSet != null)
            {
                if (!currentPlanSet.Equals(oraclePlanSet))
                    getPlan(plineid);
            }
            else { getPlan(plineid); }
        }
        private void getPlan(string plineid)
        {
            ////按序显示当前站点的可执行计划
            ////List<PlanEntity> ds1 = PlanFactory.GetByPlineID(plineid);
            ////修改为按生产线和班组取当日计划并排序：
            //List<Workshop_PlineEntity> wp = Workshop_PlineFactory.GetByPlineCode(plineid);
            //List<PlanEntity> ds2 = PlanFactory.GetByWorkshop1(wp[0].WORKSHOP_CODE);
            //if (ds2!=null && ds2.Count < 1) return;
            //List<PlanEntity> ds1 = (from p in ds2
            //                        orderby p.PLAN_SEQUENCE ascending, p.BEGIN_DATE ascending
            //                        select p).ToList<PlanEntity>();

            ////if (ds1.Count < 1) return;

            //int rowidx = 0;
            //if (GridPlan.SelectedRows.Count > 0)
            //    rowidx = GridPlan.SelectedRows[0].Index;
            //GridPlan.DataSource = ds1;
            //GridPlan.ClearSelection();
            //GridPlan.Rows[rowidx].Selected = true;

            //PlanEntity en1 = ds1[rowidx];
            //string label = string.Format("计划：{0}\r\n工程：{1}\r\n型号：{2}\r\n组件：{3}\r\n图号：{4}\r\n数量：{5}/{6}/{7}\r\n进度：{8}天内完成", en1.PLAN_CODE, en1.PROJECT_CODE, en1.PRODUCT_SERIES, en1.PRODUCT_MODEL, en1.PLAN_SO, en1.ONLINE_QTY, en1.OFFLINE_QTY, en1.PLAN_QTY, (int)(Convert.ToDateTime(en1.END_DATE) - DateTime.Now).TotalDays + 1);
            //label1.Text = label;

            ////if (LoginInfo.ProductLineInfo.PLINE_TYPE_CODE == "A")
            ////{
            ////    GridPlan.Columns["colSN"].Visible = false;
            ////    GridPlan.Columns["colPlanBatch"].Visible = true;
            ////}
            ////else
            ////{
            ////    GridPlan.Columns["colSN"].Visible = true;
            ////    GridPlan.Columns["colPlanBatch"].Visible = false;
            ////}

            //    for (int i = 0; i < GridPlan.Rows.Count; i++)
            //    {
            //        //如果完工标识为Y则黄色
                    
            //        string runFlag = GridPlan.Rows[i].Cells["ColRunFlag"].Value.ToString();

            //        if (runFlag == "F")
            //        {
            //            for (int j = 0; j < GridPlan.Columns.Count; j++)
            //                GridPlan.Rows[i].Cells[j].Style.BackColor = Color.Yellow;
            //        }
            //        //如果完工标识为N，实际上线数量>0则绿色
            //        int realOnlineQuantity = Convert.ToInt32(GridPlan.Rows[i].Cells["ColRealOnlineQuantity"].Value.ToString());
            //        if (realOnlineQuantity > 0 && runFlag == "Y")
            //        {
            //            for (int j = 0; j < GridPlan.Columns.Count; j++)
            //                GridPlan.Rows[i].Cells[j].Style.BackColor = Color.Green;
            //        }
            //        //挂起
            //        if (runFlag == "G")
            //        {
            //            for (int j = 0; j < GridPlan.Columns.Count; j++)
            //                GridPlan.Rows[i].Cells[j].Style.BackColor = Color.Red;
            //        }
            //    }
            
            ////将当前正在执行的计划通过消息发送给其它控件
            ////string currentPlanCode = PlanFactory.GetCurrentPlan(plinecode);
            ////RMESEventArgs thisEventArg = new RMESEventArgs();
            ////thisEventArg.MessageHead = "CURPLANCODE";
            ////thisEventArg.MessageBody = currentPlanCode;
            ////SendDataChangeMessage(thisEventArg);
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
            timer1.Enabled = false;
        }

        private void GridPlan_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
                        //得到计划号
            if (e.RowIndex < 0) return;
            timer1.Enabled = false;
            if (string.IsNullOrWhiteSpace(GridPlan.Rows[e.RowIndex].Cells["ColPlanCode"].Value as string)) return;
            string plan_code = GridPlan.Rows[e.RowIndex].Cells["ColPlanCode"].Value.ToString();
            string item_code = GridPlan.Rows[e.RowIndex].Cells["colPlanSo"].Value.ToString();
            string batch_code = GridPlan.Rows[e.RowIndex].Cells["colPlanBatch"].Value.ToString();
     
            string sn_flag=GridPlan.Rows[e.RowIndex].Cells["colSnFlag"].Value.ToString();
            
           
            ItemEntity iItem = ItemFactory.GetByItem(LoginInfo.CompanyInfo.COMPANY_CODE, item_code);
            if (iItem != null)
            {
                if (iItem.MANAGE_FLAG == "A")
                {
                    FrmShowPlanSn ps = new FrmShowPlanSn(plan_code, batch_code);
                    ps.Show();
                }
                else if (iItem.MANAGE_FLAG == "A" && sn_flag == "N")
                {
                    List<ProductInfoEntity> newProduct = ProductInfoFactory.GetByCompanyCodeSN(LoginInfo.CompanyInfo.COMPANY_CODE, batch_code);
                    RMESEventArgs thisEventArg = new RMESEventArgs();
                    if (newProduct.Count != 0)
                    {
                        thisEventArg.MessageHead = "SN";
                        thisEventArg.MessageBody = newProduct.First<ProductInfoEntity>();
                    }
                }
                else
                {
                    List<ProductInfoEntity> newProduct = ProductInfoFactory.GetByCompanyCodeBatch(LoginInfo.CompanyInfo.COMPANY_CODE, batch_code);
                    RMESEventArgs thisEventArg = new RMESEventArgs();
                    if (newProduct.Count != 0)
                    {
                        thisEventArg.MessageHead = "BATCH";
                        thisEventArg.MessageBody = newProduct.First<ProductInfoEntity>();
                    }
                    else
                    {
                        thisEventArg.MessageHead = null;
                        thisEventArg.MessageBody = null;
                    }
                    SendDataChangeMessage(thisEventArg);
                }
            }
        }

        private void GridPlan_MouseLeave(object sender, EventArgs e)
        {
            timer1.Enabled = true;
        }

        
    }
}
