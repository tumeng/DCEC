using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Rmes.DA.Base;
using Rmes.DA.Entity;
using Rmes.DA.Factory;
using Rmes.WinForm.Base;


//按站点、工序报完工，扫描vin后 触发记录，统计各个工序完成情况，并记录完成时间存入Process_record表   按站点报完工所有工序完成才算该站点完成 否则下次仍要全部扫描  
//thl
namespace Rmes.WinForm.Controls
{
    public partial class ctrlProcessShow : BaseControl
    {
        private string company_code = "", pline_code = "", station_code = "";
        private string type;

        private string plancode,ordercode,sn;

        public ctrlProcessShow()
        {
            InitializeComponent(); 
            this.RMesDataChanged += new Rmes.WinForm.Base.RMesEventHandler(ctrlProcessComplete_RmesDataChanged);
            type = DB.ReadConfigServer("ProcessCOMPLETE");

        }
        public void InitProcessList()
        {

            gridProcessList.DataSource = null;
            company_code = LoginInfo.CompanyInfo.COMPANY_CODE.ToString();
            pline_code = LoginInfo.ProductLineInfo.RMES_ID.ToString();
            station_code = LoginInfo.StationInfo.RMES_ID.ToString();  //站点代码
            gridProcessList.AutoGenerateColumns = false;

            //为了演示需要，暂时取所有的工艺工序进行显示
            //gridProcessList.DataSource = SNProcessFactory.GetAll();
            gridProcessList.DataSource = SNProcessTempFactory.GetProcessList(company_code, pline_code, station_code, plancode, sn);
        }

        private void ShowProcessList()
        {
            string complete_flag="", routing_code="",parent_routing_code="",process_code="";
            int routing_level=1;
            object obj_routing_code,obj_parent_routing,obj_process_code,obj_routing_level;
            for (int j = 0; j < gridProcessList.Rows.Count; j++)
            {
                complete_flag = gridProcessList.Rows[j].Cells["colCompleteFlag"].Value.ToString();
                obj_routing_code=gridProcessList.Rows[j].Cells["colRoutingCode"].Value;
                if (obj_routing_code != null) routing_code = obj_routing_code.ToString();

                obj_parent_routing = gridProcessList.Rows[j].Cells["colParentRoutingCode"].Value;
                if (obj_parent_routing != null) parent_routing_code = obj_parent_routing.ToString();

                obj_process_code = gridProcessList.Rows[j].Cells["colProcessCode"].Value;
                if(obj_process_code !=null) process_code=obj_process_code.ToString();

                obj_routing_level = gridProcessList.Rows[j].Cells["colRoutingLevel"].Value;
                if (obj_routing_level != null) routing_level = int.Parse(obj_routing_level.ToString());

                //process_code= gridProcessList.Rows[j].Cells["colProcessCode"].Value.ToString();
                DataGridViewButtonCell vCell = gridProcessList.Rows[j].Cells["colCmdStart"] as DataGridViewButtonCell;
                DataGridViewButtonCell cell_pause = gridProcessList.Rows[j].Cells["colCmdPause"] as DataGridViewButtonCell;
                vCell.UseColumnTextForButtonValue = false;
                string sss = "";
                for (int i = 0; i < routing_level-1; i++)
                {
                    sss = sss + "--";
                }
                cell_pause.Value = "暂停";
                gridProcessList.Rows[j].Cells["colRoutingCode"].Value = sss+ routing_code;
                if (obj_parent_routing != null)
                {
                    //gridProcessList.Rows[j].Cells["colRoutingCode"].Value = "--" + routing_code;
                    vCell.Value = "";
                }
                else if (obj_process_code != null)
                {
                    //gridProcessList.Rows[j].Cells["colRoutingCode"].Value = "----" + routing_code;
                    if (complete_flag == "N")
                    {
                        SetRowColor(j, Color.Gray);
                        vCell.Value = "开始";
                    }
                    else if (complete_flag == "R")
                    {
                        SetRowColor(j, Color.Yellow);
                        vCell.Value = "完成";
                    }
                    else if (complete_flag == "Y")
                    {
                        SetRowColor(j, Color.FromArgb(255, 0, 255, 0));
                        vCell.Value = "结束";
                    }
                    else if (complete_flag == "P")
                    {
                        SetRowColor(j, Color.Blue);
                        vCell.Value = "完成";
                        cell_pause.Value = "继续";
                    }
                }
                else vCell.Value = "";
                
                

            }
        }

        private void SetRowColor(int pRowID, Color pRowColor)
        {
            for (int i = 0; i < gridProcessList.Columns.Count; i++)
            {
                gridProcessList.Rows[pRowID].Cells[i].Style.BackColor = pRowColor;
                gridProcessList.Rows[pRowID].Cells[i].Style.ForeColor = Color.Black;
            }
        }


        protected void ctrlProcessComplete_RmesDataChanged(object obj, Rmes.WinForm.Base.RMESEventArgs e)
        {
            
            if (e.MessageHead == "SN")
            {
                //gridProcessList.Enabled = true;
                //sn = e.MessageBody as string;
                //PlanSnEntity ent_sn = PlanSnFactory.GetBySn(sn);
                //ordercode=ent_sn.ORDER_CODE;
                //if (ent_sn.PLAN_CODE != null) plancode = ent_sn.PLAN_CODE;
                //else plancode = PlanFactory.GetByOrder(ent_sn.ORDER_CODE).PLAN_CODE;
                
                InitProcessList();
                ShowProcessList();
                
                return;
            }
            if (e.MessageHead == "PLAN")
            {
                gridProcessList.Enabled = true;
                plancode = e.MessageBody as string;
                PlanEntity plan = PlanFactory.GetByKey(plancode);
                if (plan != null)
                {
                    ordercode = plan.ORDER_CODE;
                    InitProcessList();
                    ShowProcessList();
                }
            }
            else if (e.MessageHead == "PRCS")
            {
                //string[] msgbdy = (e.MessageBody as string).Split('^');
                //string process_code = msgbdy[0];
                //string complete_flag = msgbdy[1];
                //DealProcessComplete(process_code, complete_flag);
            }
            else if( e.MessageHead=="PAUSE")
            {
                this.gridProcessList.DataSource = null;
                return;
            }
            else if (e.MessageHead == "SCP" || e.MessageHead == "OFFLINE" )
            {
                this.gridProcessList.DataSource = null;
                ProductDataFactory.ProcessControlComplete(company_code, pline_code, station_code, plancode,sn);
            }
            else if (e.MessageHead == "MESLL")
            {
                this.Visible = false;
                return;
            }
            if (e.MessageHead.ToString() == "WORK" || e.MessageHead.ToString() == "QUA")
            {
                this.Visible = true;
                return;
            }
            
        }
       
        private void gridProcessList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //点击按钮是执行事件

            string process_code,cmd_caption,RMES_ID;
            RMESEventArgs args = new RMESEventArgs();
            DataGridViewButtonCell vCell, cell_pause;
            if (e.RowIndex < 0) return;


            if (e.ColumnIndex == 4)//根据工序触发processfile的变化
            {
                
               // this.RMesDataChanged += new RMesEventHandler(ProcessFlieChange);
                RMESEventArgs arg = new RMESEventArgs();
                if (gridProcessList.Rows[e.RowIndex].Cells["colProcessCode"].Value == null) return;
                process_code = gridProcessList.Rows[e.RowIndex].Cells["colProcessCode"].Value.ToString();
                arg.MessageHead = "PRCS";
                arg.MessageBody = process_code;

                SendDataChangeMessage(arg);
                return;
            }

            if (gridProcessList.Rows[e.RowIndex].Cells["colProcessCode"].Value == null) return;
            process_code = gridProcessList.Rows[e.RowIndex].Cells["colProcessCode"].Value.ToString();
            RMES_ID = gridProcessList.Rows[e.RowIndex].Cells["colRmesID"].Value.ToString();

            if (gridProcessList.Columns[e.ColumnIndex].Name == "colCmdStart")  //开始
            {
                vCell=gridProcessList.Rows[e.RowIndex].Cells["colCmdStart"] as DataGridViewButtonCell;
                
                cmd_caption = vCell.Value.ToString();
                if (cmd_caption == "开始")
                    DealProcessComplete(process_code, "0", RMES_ID);
                else if (cmd_caption == "完成")
                    DealProcessComplete(process_code, "1",RMES_ID);
                else
                    MessageBox.Show("此项工序已完成！");
            }

            if (gridProcessList.Columns[e.ColumnIndex].Name == "colCmdPause")  //开始
            {
                vCell = gridProcessList.Rows[e.RowIndex].Cells["colCmdPause"] as DataGridViewButtonCell;
                cmd_caption = vCell.Value.ToString();
                if (cmd_caption == "暂停")
                    DealProcessComplete(process_code, "2", RMES_ID);
                else if (cmd_caption == "继续")
                    DealProcessComplete(process_code, "3",RMES_ID);
                
            }

            
        }

        private void gridProcessList_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (gridProcessList.Rows[e.RowIndex].Cells["colProcessCode"].Value == null) return;
            string process_code = gridProcessList.Rows[e.RowIndex].Cells["colProcessCode"].Value.ToString().Trim();

            PlanEntity ent = PlanFactory.GetByKey(this.plancode);
            if (ent.PRODUCT_SERIES == null)
            {
                MessageBox.Show("此计划工序没有定义相应的工序指导文件");
                return;
            }

            RMESEventArgs args = new RMESEventArgs();
            args.MessageHead = "INFO";
            args.MessageBody = process_code;
            args.MessageBody += "^" + plancode;
            SendDataChangeMessage(args);

            FrmShowProcessNote spn = new FrmShowProcessNote(plancode, process_code);
            spn.Show();
        }

        private void DealProcessComplete(string ProcessCode, string CompleteFlag)
        {
            //点击按钮是执行事件
            string complete_flag = "";
            RMESEventArgs args = new RMESEventArgs();
            bool completed = false;
            int selected_row = -1;

            string grid_process_code, gird_rmes_id;
            object obj_process_code;

            for (int i = 0; i < gridProcessList.Rows.Count; i++)
            {
                if (gridProcessList.Rows[i].Cells["colProcessCode"].Value != null)
                {
                    grid_process_code = gridProcessList.Rows[i].Cells["colProcessCode"].Value.ToString();
                    if (grid_process_code == ProcessCode) { selected_row = i; break; }
                }
            }
            if (selected_row < 0) return;
            if (gridProcessList.Rows[selected_row].Cells["colProcessCode"].Value == null)
            {
                MessageBox.Show("只能对工序进行操作");
                return;
            }

            if (selected_row > 0 && CompleteFlag == "0")
            {
                //complete_flag = gridProcessList.Rows[selected_row - 1].Cells["colCompleteFlag"].Value.ToString();
                //obj_process_code = gridProcessList.Rows[selected_row - 1].Cells["colProcessCode"].Value;
                //if (obj_process_code != null && complete_flag == "R") //complete_flag != "Y"
                //{
                //    MessageBox.Show("前一序未完成！");
                //    return;
                //}


                //caoly 20140425 改成只要有未完成工序，就不能开始新的工序；可以挑着工序开始
                List<SNProcessTempEntity> temp = DB.GetInstance().Fetch<SNProcessTempEntity>("where sn=@0 and complete_flag='R'", sn);
                if (temp.Count > 0)
                {
                    MessageBox.Show("有未完成工序，请先完成该工序！");
                    return;
                }
            }
            else if (CompleteFlag == "1")
            {
                complete_flag = gridProcessList.Rows[selected_row].Cells["colCompleteFlag"].Value.ToString();
                if (complete_flag != "R")
                {
                    MessageBox.Show("此工序尚未开始！");
                    return;
                }
            }

            gird_rmes_id = gridProcessList.Rows[selected_row].Cells["colRmesID"].Value.ToString();
            SNProcessTempFactory.HandleProcessComplete(gird_rmes_id, LoginInfo.StationInfo.RMES_ID, CompleteFlag);
            gridProcessList.Rows[selected_row].Cells["colCompleteFlag"].Value = CompleteFlag == "0" ? "R" : "Y";
            DataGridViewButtonCell vCell = gridProcessList.Rows[selected_row].Cells["colCmdStart"] as DataGridViewButtonCell;

            for (int j = 0; j < gridProcessList.Columns.Count; j++)
                gridProcessList.Rows[selected_row].Cells[j].Style.BackColor = CompleteFlag == "0" ? Color.Yellow : Color.FromArgb(255, 0, 255, 0);
            vCell.Value = CompleteFlag == "0" ? "完成" : "结束";
            if (CompleteFlag == "0")
                gridProcessList.Rows[selected_row].Cells["colStartTime"].Value = System.DateTime.Now.ToString();
            else
                gridProcessList.Rows[selected_row].Cells["colEndTime"].Value = System.DateTime.Now.ToString();

            completed = selected_row+1 == gridProcessList.Rows.Count - 1 ? true : false;
            if (completed)
            {
                args.MessageHead = "CCP";
                args.MessageBody = "Rmes.WinForm.ctrlProcessShow^B";
                UiFactory.CallDataChanged(this, args);
            }
            
        }
        private void DealProcessComplete(string ProcessCode, string CompleteFlag, string RMES_ID)
        {
            //点击按钮是执行事件
            string complete_flag = "";
            RMESEventArgs args = new RMESEventArgs();
            bool completed = false;
            int selected_row = -1;
            Color rowColor=Color.Gray;
            string  gird_rmes_id;

            

            for (int i = 0; i < gridProcessList.Rows.Count; i++)
            {
                if (gridProcessList.Rows[i].Cells["colRmesID"].Value != null)
                {
                    gird_rmes_id = gridProcessList.Rows[i].Cells["colRmesID"].Value.ToString();
                    if (gird_rmes_id == RMES_ID) { selected_row = i; break; }
                }
            }
            if (selected_row < 0) return;
            complete_flag = gridProcessList.Rows[selected_row].Cells["colCompleteFlag"].Value.ToString();
            if (gridProcessList.Rows[selected_row].Cells["colProcessCode"].Value == null)
            {
                MessageBox.Show("只能对工序进行操作");
                return;
            }

            if (selected_row > 0 && CompleteFlag == "0")//工序执行开始
            {
                //complete_flag = gridProcessList.Rows[selected_row - 1].Cells["colCompleteFlag"].Value.ToString();
                //obj_process_code = gridProcessList.Rows[selected_row - 1].Cells["colProcessCode"].Value;
                //if (obj_process_code != null && complete_flag == "R") //complete_flag != "Y"
                //{
                //    MessageBox.Show("前一序未完成！");
                //    return;
                //}

                //caoly 20140425 改成只要有未完成工序，就不能开始新的工序；可以挑着工序开始
                List<SNProcessTempEntity> temp = DB.GetInstance().Fetch<SNProcessTempEntity>("where sn=@0 and complete_flag='R'",sn);
                if (temp.Count > 0)
                {
                    MessageBox.Show("有未完成工序，请先完成该工序！");
                    return;
                }
            }
            else if (CompleteFlag == "1" || CompleteFlag=="2")
            {
                complete_flag = gridProcessList.Rows[selected_row].Cells["colCompleteFlag"].Value.ToString();
                if (complete_flag != "R")
                {
                    MessageBox.Show("此工序尚未开始！");
                    return;
                }               
            }
            else if (CompleteFlag == "3")
            {
                if (complete_flag != "P")
                {
                    MessageBox.Show("此工序未进行暂停操作！");
                    return;
                }

            }
            
            //处理工序状态操作
            gird_rmes_id = gridProcessList.Rows[selected_row].Cells["colRmesID"].Value.ToString();
            SNProcessTempFactory.HandleProcessComplete(gird_rmes_id, LoginInfo.StationInfo.RMES_ID, CompleteFlag);

            //处理按钮标题显示
            gridProcessList.Rows[selected_row].Cells["colCompleteFlag"].Value = CompleteFlag == "0" ? "R" : "Y";
            if (CompleteFlag == "0" || CompleteFlag == "3")
                gridProcessList.Rows[selected_row].Cells["colCompleteFlag"].Value = "R";
            else if (CompleteFlag == "1")
                gridProcessList.Rows[selected_row].Cells["colCompleteFlag"].Value = "Y";
            else if (CompleteFlag == "2")
            {
                gridProcessList.Rows[selected_row].Cells["colCompleteFlag"].Value = "P";
                DateTime dt1 = DateTime.Parse(gridProcessList.Rows[selected_row].Cells["colStartTime"].Value.ToString());
                DateTime dt2 = System.DateTime.Now;
                string span_dt = (dt2 - dt1).TotalMinutes.ToString("00.00");
                gridProcessList.Rows[selected_row].Cells["colWorkTime"].Value = span_dt;
                gridProcessList.Rows[selected_row].Cells["colEndTime"].Value = dt2;

            }
            



            DataGridViewButtonCell vCell = gridProcessList.Rows[selected_row].Cells["colCmdStart"] as DataGridViewButtonCell;
            DataGridViewButtonCell cell_pause = gridProcessList.Rows[selected_row].Cells["colCmdPause"] as DataGridViewButtonCell;

            //处理行显示颜色
            if (CompleteFlag == "0") rowColor = Color.Yellow;
            else if (CompleteFlag == "1") rowColor = Color.FromArgb(255, 0, 255, 0);
            else if (CompleteFlag == "2") rowColor = Color.Blue;
            for (int j = 0; j < gridProcessList.Columns.Count; j++)
            {
                gridProcessList.Rows[selected_row].Cells[j].Style.BackColor = rowColor;
            }
            if (CompleteFlag == "0")
                vCell.Value = "完成";
            else if (CompleteFlag == "1")
                vCell.Value = "结束";
            else if (CompleteFlag == "2")
                cell_pause.Value = "继续";
            else if (CompleteFlag == "3")
                cell_pause.Value = "暂停";


            if (CompleteFlag == "0")
                gridProcessList.Rows[selected_row].Cells["colStartTime"].Value = System.DateTime.Now.ToString();
            else
                gridProcessList.Rows[selected_row].Cells["colEndTime"].Value = System.DateTime.Now.ToString();

            completed = selected_row + 1 == gridProcessList.Rows.Count - 1 ? true : false;
            if (completed)
            {
                args.MessageHead = "CCP";
                args.MessageBody = "Rmes.WinForm.ctrlProcessShow^B";
                UiFactory.CallDataChanged(this, args);
            }
        }
    }
    
}

