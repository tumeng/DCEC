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


//按站点、工序报完工，扫描vin后 触发记录，统计各个工序完成情况，并记录完成时间存入option_record表   按站点报完工所有工序完成才算该站点完成 否则下次仍要全部扫描  
//thl
namespace Rmes.WinForm.Controls
{
    public partial class ctrlOptionShow : BaseControl
    {
        private int oldrow;
        private string type;
        private int nums;
        private DateTime starttime ;
        private DateTime endtime ;
        private string sn;
        private int state;   //初始化判断当前sn是否报完工 未报完工则置1
        //区分按站点报完工还是按工序报完工
        public ctrlOptionShow()
        {
            InitializeComponent();
            this.RMesDataChanged += new Rmes.WinForm.Base.RMesEventHandler(ctrlOptionComplete_RmesDataChanged);
            type = DB.ReadConfigServer("OPTIONCOMPLETE");
            //type = "0";   //0  站点 1 工序  
        }

        public void initial()
        {
            oldrow = -1;
            nums = 0;
            state = 0;
            gridOptionList.DataSource = null;
            string pline_code = LoginInfo.ProductLineInfo.PLINE_CODE.ToString();
            string station_code = LoginInfo.StationInfo.STATION_CODE.ToString();  //站点代码
            gridOptionList.AutoGenerateColumns = false;
            gridOptionList.DataSource = OptionRecordFactory.GetOptionList(pline_code, station_code,sn);
        }

        public void initials()
        {
            oldrow = -1;
            nums = 0;
            state = 0;
            gridOptionList.DataSource = null;
            string pline_code = LoginInfo.ProductLineInfo.PLINE_CODE.ToString();
            string station_code = LoginInfo.StationInfo.STATION_CODE.ToString();  //站点代码
            gridOptionList.AutoGenerateColumns = false;
            gridOptionList.DataSource = OptionRecordFactory.GetOptionLists(pline_code, station_code, sn);
        }

       private void OptionShow_Load()
        {
            for (int j = 0; j < gridOptionList.Rows.Count; j++)
            {
                if (Convert.ToDateTime(gridOptionList.Rows[j].Cells["etime"].Value).ToString("yyyy-MM-dd HH:mm:ss")  == "0001-01-01 00:00:00" ||gridOptionList.Rows[j].Cells["etime"].Value.ToString() ==""|| gridOptionList.Rows[j].Cells["etime"].Value == null)
                {
                    DateTime defaulttime=Convert.ToDateTime("0000-00-00 00:00:00");
                    //gridOptionList.Rows[j].Cells["etime"].Value = defaulttime.ToString();
                    if (Convert.ToDateTime(gridOptionList.Rows[j].Cells["stime"].Value).ToString("yyyy-MM-dd HH:mm:ss") == "0001-01-01 00:00:00" || gridOptionList.Rows[j].Cells["etime"].Value.ToString() == "" || gridOptionList.Rows[j].Cells["stime"].Value == null)
                    { 
                        state = 1; 
                        //gridOptionList.Rows[j].Cells["stime"].Value = defaulttime.ToString(); 
                    }
                    else
                    {
                        for (int i = 0; i < gridOptionList.Columns.Count; i++)
                            gridOptionList.Rows[j].Cells[i].Style.BackColor = Color.Red;
                        state = 1;
                    }
                }
                else
                {
                    for (int i = 0; i < gridOptionList.Columns.Count; i++)
                         gridOptionList.Rows[j].Cells[i].Style.BackColor = Color.Green;
                    nums += 1;
                }
            }
        }


        protected void ctrlOptionComplete_RmesDataChanged(object obj, Rmes.WinForm.Base.RMESEventArgs e)
        {
            if (e.MessageHead == "VIN")
            {
                gridOptionList.Enabled = true;
                ProductInfoEntity product = (ProductInfoEntity)e.MessageBody;
                sn = product.SN;
                switch (type)
                {
                    case "0":   //站点
                        initials();
                        break;
                    case "1":   //工序
                        initial();
                        break;
                    default:
                        break;
                }
                OptionShow_Load();
                if (state == 0)  //报完工发送消息
                {
                    RMESEventArgs args = new RMESEventArgs();
                    args.MessageHead = "OPTIONCOMPLETES";
                    args.MessageBody = null;
                    UiFactory.CallDataChanged(this, args);
                }
            }
            
        }

        private void gridOptionList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {   //点击按钮是执行事件
            OptionRecordEntity option_record=new OptionRecordEntity();
            if (gridOptionList.Columns[e.ColumnIndex].Name=="operate")  //开始
            {
                if (oldrow == -1)
                {
                    if (gridOptionList.Rows[e.RowIndex].Cells["option_code"].Style.BackColor != Color.Green)  //未完成工序
                    {

                        if (type == "0")  //按站点
                        {
                            if (nums == 0)
                            {
                                starttime = DateTime.Now;
                                gridOptionList.Rows[e.RowIndex].Cells["stime"].Value = starttime.ToString();
                            }
                        }
                        else
                        {
                            starttime = DateTime.Now;
                            gridOptionList.Rows[e.RowIndex].Cells["stime"].Value = starttime.ToString();
                        }

                        
                        oldrow = e.RowIndex;
                        for (int j = 0; j < gridOptionList.Columns.Count; j++)
                            gridOptionList.Rows[oldrow].Cells[j].Style.BackColor = Color.Red;
                    }
                }
                else
                {
                    MessageBox.Show("前一序未完成！");
                }
                
            }
            if (gridOptionList.Columns[e.ColumnIndex].Name == "operates")  //结束
            {
                if (oldrow == -1)
                {
                    MessageBox.Show("尚无工序开始！");
                    return;
                }

                int newrow = e.RowIndex;
                if (oldrow == newrow)  //当前工序完成
                {
                    for (int j = 0; j < gridOptionList.Columns.Count; j++)
                        gridOptionList.Rows[oldrow].Cells[j].Style.BackColor = Color.Green;
                    
                    oldrow = -1;
                    nums += 1;
                    if(type=="1") //按工序
                    {
                        endtime = DateTime.Now;
                        gridOptionList.Rows[e.RowIndex].Cells["etime"].Value = endtime.ToString();
                        RMESEventArgs arg = new RMESEventArgs();
                        arg.MessageHead = "OPTIONCOMPLETE";
                        //option_record.RMES_ID = "";
                        option_record.COMPANY_CODE = LoginInfo.CompanyInfo.COMPANY_CODE;
                        option_record.PLINE_CODE = LoginInfo.ProductLineInfo.PLINE_CODE.ToString();
                        option_record.STATION_CODE = LoginInfo.StationInfo.STATION_CODE.ToString();
                        option_record.OPTION_CODE  = gridOptionList.Rows[e.RowIndex].Cells["option_code"].Value.ToString();
                        option_record.OPTION_NAME  = gridOptionList.Rows[e.RowIndex].Cells["option_name"].Value.ToString();
                        option_record.START_TIME = starttime;
                        option_record.END_TIME = endtime;
                        option_record.SN = sn;
                        arg.MessageBody = option_record;
                        UiFactory.CallDataChanged(this, arg);
                        if (nums == gridOptionList.RowCount) 
                        {
                            RMESEventArgs args = new RMESEventArgs();
                            args.MessageHead = "OPTIONCOMPLETES";
                            args.MessageBody = null;
                            UiFactory.CallDataChanged(this, args);
                        }
                    }
                    else
                    {
                        if (type == "0") //按站点
                        {
                            if (nums == gridOptionList.RowCount)
                            {
                                endtime = DateTime.Now;
                                gridOptionList.Rows[e.RowIndex].Cells["etime"].Value = endtime.ToString();
                                RMESEventArgs arg = new RMESEventArgs();
                                arg.MessageHead = "OPTIONCOMPLETE";
                                option_record.COMPANY_CODE = LoginInfo.CompanyInfo.COMPANY_CODE;
                                option_record.PLINE_CODE = LoginInfo.ProductLineInfo.PLINE_CODE.ToString();
                                option_record.STATION_CODE = LoginInfo.StationInfo.STATION_CODE.ToString();
                                option_record.START_TIME = starttime;
                                option_record.END_TIME = endtime;
                                option_record.SN = sn;
                                arg.MessageBody = option_record;
                                UiFactory.CallDataChanged(this, arg);
                                RMESEventArgs args = new RMESEventArgs();
                                args.MessageHead = "OPTIONCOMPLETES";
                                args.MessageBody = null;
                                UiFactory.CallDataChanged(this, args);
                            }
                        }
                    }

                }
                else
                {
                    MessageBox.Show("当前工序尚未开始，前一序未完成！");
                    return;
                }

            }

        }

    }
}
