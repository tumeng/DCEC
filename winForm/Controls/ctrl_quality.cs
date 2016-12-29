using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Rmes.WinForm.Base;
using Rmes.DA.Base;
using Rmes.DA.Entity;
using Rmes.DA.Factory;
using Rmes.Pub.Data1;

namespace Rmes.WinForm.Controls
{
    public partial class ctrl_quality : BaseControl
    {
        //质量问答界面 RSTBOMQATS
        //登录质量站点，判断质量点，弹出前道质量问答结果，可修改，记录修改内容；点击质量按钮，显示本站点质量问答问题，并记录回答内容
        private string CompanyCode, PlineCode, PlineID, StationCode, stationname, TheSo, TheSn="", ThePlancode,Thetype="";
        //dataConn dc = new dataConn();
        ProductInfoEntity product;
        public bool IsABC = false;
        public ctrl_quality()
        {
            InitializeComponent();
            CompanyCode = LoginInfo.CompanyInfo.COMPANY_CODE;
            PlineID = LoginInfo.ProductLineInfo.RMES_ID;
            PlineCode = LoginInfo.ProductLineInfo.PLINE_CODE;
            StationCode = LoginInfo.StationInfo.STATION_CODE;
            stationname = LoginInfo.StationInfo.STATION_NAME;
            GridQuality.AutoGenerateColumns = false;
            GridQuality.RowHeadersVisible = false;
            GridQuality.DataSource = null;
            this.RMesDataChanged += new RMesEventHandler(ctrl_Tj_RMesDataChanged);
        }
        public ctrl_quality(string type,string sn,string stationcode1)
        {
            InitializeComponent();
            CompanyCode = LoginInfo.CompanyInfo.COMPANY_CODE;
            PlineID = LoginInfo.ProductLineInfo.RMES_ID;
            PlineCode = LoginInfo.ProductLineInfo.PLINE_CODE;
            StationCode = LoginInfo.StationInfo.STATION_CODE;
            stationname = LoginInfo.StationInfo.STATION_NAME;
            GridQuality.AutoGenerateColumns = false;
            GridQuality.RowHeadersVisible = false;
            GridQuality.DataSource = null;
            Thetype = type;
            TheSn = sn;
            product = ProductInfoFactory.GetByCompanyCodeSNSingle(CompanyCode, TheSn);//获取sn信息
            if (product == null) return;
            //执行存储过程 获取RSTBOMQATS
            ProductDataFactory.PL_QUERY_QAZJTS(product.PLAN_SO, StationCode, PlineCode, "", product.PLAN_CODE, sn);
            string sql = "select count(1) from data_sn_qa where company_code='" + CompanyCode + "' and plan_code='" + product.PLAN_CODE + "' and sn='" + TheSn + "' and station_code='" + StationCode + "' and pline_code='" + PlineCode + "'  ";
            if (dataConn.GetValue(sql) == "0")
            {
                //插入data_sn_qa
                sql = " insert into data_sn_qa(rmes_id,sn,company_code,plan_code,pline_code,location_code,station_code,question,standard_answer,station_name)  "
                    + " select seq_rmes_id.nextval,'" + TheSn + "','" + CompanyCode + "','" + product.PLAN_CODE + "','" + PlineCode + "',gwdm,'" + StationCode + "',question,answer,'" + stationname + "'  from RSTBOMQATS where zddm='" + StationCode + "'    ";
                dataConn.ExeSql(sql);
            }
            ThePlancode = product.PLAN_CODE;
            GridQuality.Focus();
            IsABC = false;
            ShowData(type, TheSn, stationcode1);//A显示前道站点质量信息  B 显示当前站点质量信息
            try
            {
                if (GridQuality.Rows.Count > 0 && IsABC)
                {
                    PlanSnFactory.InitStationControl(CompanyCode, PlineID, LoginInfo.StationInfo.RMES_ID, product.PLAN_CODE, product.SN, "Rmes.WinForm.Controls.ctrl_quality");
                    dataConn.ExeSql("update data_sn_controls_complete set complete_flag='A' where station_code='" + LoginInfo.StationInfo.RMES_ID + "' and control_name='Rmes.WinForm.Controls.ctrl_quality' and sn='" + product.SN + "' and plan_code='" + product.PLAN_CODE + "' ");
                }
            }
            catch
            { }
                //this.RMesDataChanged += new RMesEventHandler(ctrl_Tj_RMesDataChanged);
        }
        void ctrl_Tj_RMesDataChanged(object obj, Rmes.WinForm.Base.RMESEventArgs e)
        {
            RMESEventArgs arg = new RMESEventArgs();
            arg.MessageHead = "";
            arg.MessageBody = "";
            if (e.MessageHead == null) return;
            if (e.MessageHead == "SN" || e.MessageHead == "RESN" || e.MessageHead == "RECHECK")
            { 
                TheSn = e.MessageBody.ToString();
                product = ProductInfoFactory.GetByCompanyCodeSNSingle(CompanyCode, TheSn);//获取sn信息
                if (product == null) return;
                ThePlancode = product.PLAN_CODE;
                //初始化质量数据
                //string sql = "select count(1) from data_sn_qa where company_code='"+CompanyCode+"' and plan_code='"+product.PLAN_CODE+"' and sn='"+TheSn+"' and station_code='"+StationCode+"' and pline_code='"+PlineCode+"'  ";
                //if (dataConn.GetValue(sql) == "0")
                //{
                //    //插入data_sn_qa
                //    sql = " insert into data_sn_qa(rmes_id,sn,company_code,plan_code,pline_code,location_code,station_code,question,standard_answer,station_name) values "
                //        + " select seq_rmes_id.nextval,'"+TheSn+"','" + CompanyCode + "','" + product.PLAN_CODE + "','" + PlineCode + "',gwdm,'" + StationCode + "',question,answer,'"+stationname+"'  from RSTBOMQATS where zddm='" + StationCode + "'    ";
                //    dataConn.ExeSql(sql);
                //}
                string type="B";
                Thetype = type;
                string sn = TheSn;
                product = ProductInfoFactory.GetByCompanyCodeSNSingle(CompanyCode, TheSn);//获取sn信息
                if (product == null) return;
                //执行存储过程 获取RSTBOMQATS
                ProductDataFactory.PL_QUERY_QAZJTS(product.PLAN_SO, StationCode, PlineCode, "", product.PLAN_CODE, sn);
                string sql = "select count(1) from data_sn_qa where company_code='" + CompanyCode + "' and plan_code='" + product.PLAN_CODE + "' and sn='" + TheSn + "' and station_code='" + StationCode + "' and pline_code='" + PlineCode + "'  ";
                if (dataConn.GetValue(sql) == "0")
                {
                    //插入data_sn_qa
                    sql = " insert into data_sn_qa(rmes_id,sn,company_code,plan_code,pline_code,location_code,station_code,question,standard_answer,station_name)  "
                        + " select seq_rmes_id.nextval,'" + TheSn + "','" + CompanyCode + "','" + product.PLAN_CODE + "','" + PlineCode + "',gwdm,'" + StationCode + "',question,answer,'" + stationname + "'  from RSTBOMQATS where zddm='" + StationCode + "'    ";
                    dataConn.ExeSql(sql);
                }
                ThePlancode = product.PLAN_CODE;
                GridQuality.Focus();
                IsABC = false;
                ShowData(type, TheSn, StationCode);//A显示前道站点质量信息  B 显示当前站点质量信息
                try
                {
                    if (GridQuality.Rows.Count > 0 && IsABC)
                    {
                        PlanSnFactory.InitStationControl(CompanyCode, PlineID, LoginInfo.StationInfo.RMES_ID, product.PLAN_CODE, product.SN, "Rmes.WinForm.Controls.ctrl_quality");
                        dataConn.ExeSql("update data_sn_controls_complete set complete_flag='A' where station_code='" + LoginInfo.StationInfo.RMES_ID + "' and control_name='Rmes.WinForm.Controls.ctrl_quality' and sn='" + product.SN + "' and plan_code='" + product.PLAN_CODE + "' ");
                    }
                }
                catch
                { }

                //
            }

        }

        private void ShowData(string type, string sn, string stationcode1)
        {
            if (type == "A")
            {
                //获取前质量站点
                string sql = "select rmes_id,sn,company_code,plan_code,pline_code,location_code,station_code,question,standard_answer,answer_value,user_id,work_time,remark,station_name,qa_flag from data_sn_qa where sn='" + TheSn + "' and plan_code='" + ThePlancode + "' and station_code in ( select t.prestation_code from REL_QA_STATION_PRESTATION t where t.pline_code='"+PlineCode+"' and t.station_code=  '" + stationcode1 + "' )";
                DataTable dt = dataConn.GetTable(sql);
                GridQuality.DataSource = dt;
                ShowQualityList1();
                if (GridQuality.Rows.Count > 0)
                {
                    GetFocus(0);
                }
            }
            else
            {
                string sql = "select rmes_id,sn,company_code,plan_code,pline_code,location_code,station_code,question,standard_answer,answer_value,user_id,work_time,remark,station_name,qa_flag from data_sn_qa where sn='" + TheSn + "' and plan_code='" + ThePlancode + "' and station_code='" + StationCode + "' ";
                DataTable dt = dataConn.GetTable(sql);
                GridQuality.DataSource = dt;
                ShowQualityList();
                if (GridQuality.Rows.Count > 0)
                {
                    GetFocus(0);
                }
            }
        }
        private void GetFocus(int rowid)
        {
            //this.GridQuality.Rows[rowid].Cells["colDetectValue"].Selected = true;
            //SendKeys.Send("+{Home}");
            try
            {
                this.GridQuality.CurrentCell = GridQuality.Rows[rowid].Cells["colAnswer"];
                GridQuality.Focus();
                GridQuality.BeginEdit(false);
            }
            catch
            { }
        }
        private void ShowQualityList()
        {
            for (int j = 0; j < GridQuality.Rows.Count; j++)
            {
                string detect_flag = GetGridValue(GridQuality, j, "colFlag");
                if (detect_flag != "Y")
                {
                    IsABC = true;
                    GridQuality.Rows[j].DefaultCellStyle.BackColor = Color.White;
                }
                else
                    GridQuality.Rows[j].DefaultCellStyle.BackColor = Color.FromArgb(0, 255, 0);
                GridQuality.Rows[j].DefaultCellStyle.ForeColor = Color.FromArgb(0, 0, 0);
            }
            if (!IsABC && TheSn!="")
            {
                RMESEventArgs args = new RMESEventArgs();
                args.MessageHead = "CCP";
                args.MessageBody = TheSn + "^Rmes.WinForm.Controls.ctrl_quality^B";
                UiFactory.CallDataChanged(this, args);
            }
           
        }
        private void ShowQualityList1()
        {
            //前站点质量问题 不记录
            for (int j = 0; j < GridQuality.Rows.Count; j++)
            {
                string detect_flag = GetGridValue(GridQuality, j, "colFlag"); //colStandard colAnswer
                string standardans = GetGridValue(GridQuality, j, "colStandard");
                string ans = GetGridValue(GridQuality, j, "colAnswer");
                if (standardans.ToUpper().Trim()!=ans.ToUpper().Trim())
                {
                    IsABC = false;
                    GridQuality.Rows[j].DefaultCellStyle.BackColor = Color.Red;
                }
                else
                    GridQuality.Rows[j].DefaultCellStyle.BackColor = Color.FromArgb(0, 255, 0);
                GridQuality.Rows[j].DefaultCellStyle.ForeColor = Color.FromArgb(0, 0, 0);
            }

        }
        private string GetGridValue(DataGridView dgv, int rowid, string colname)
        {
            object cell_val = dgv.Rows[rowid].Cells[colname].Value;
            if (cell_val == null) return "";
            else return cell_val.ToString();
        }

        private void GridQuality_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            //输入质量内容 保存
            int row = GridQuality.CurrentCell.RowIndex;
            int column = GridQuality.CurrentCell.ColumnIndex;
            if (GridQuality.Columns[e.ColumnIndex].Name == "colAnswer")
            {
                string rmesid = GetGridValue(GridQuality, row, "colRmesID");
                string answer_value = GridQuality.CurrentCell.EditedFormattedValue.ToString();
                string oldvalue = GetGridValue(GridQuality, row, "colAnswer");
                string standardAnswer = GetGridValue(GridQuality, row, "colStandard");
                string IsRight = "N";
                if (answer_value == "")
                {
                    e.Cancel = false;
                    return;
                }
                if (answer_value.ToUpper().Trim() == standardAnswer.ToUpper().Trim())
                {
                    IsRight = "Y";
                }
                if (oldvalue != answer_value)
                {
                    string time11=DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    if (oldvalue == "")//新插入的记录
                    {
                        string sql = "update data_sn_qa set answer_value='" + answer_value + "' ,user_id='" + LoginInfo.UserInfo.USER_CODE + "',work_time=to_date('" + time11 + "','yyyy-mm-dd hh24:mi:ss'),qa_flag='" + IsRight + "'  where rmes_id='" + rmesid + "'";
                        dataConn.ExeSql(sql);
                    }
                    else //修改记录
                    {
                        string sql = "insert into DATA_SN_QA_LOG select t.*,'" + answer_value + "','" + LoginInfo.UserInfo.USER_CODE + "',sysdate from DATA_SN_QA t where rmes_id='" + rmesid + "' ";
                        dataConn.ExeSql(sql);
                        sql = "update data_sn_qa set answer_value='" + answer_value + "' ,user_id='" + LoginInfo.UserInfo.USER_CODE + "',work_time=to_date('" + time11 + "','yyyy-mm-dd hh24:mi:ss'),qa_flag='" + IsRight + "'  where rmes_id='" + rmesid + "'";
                        dataConn.ExeSql(sql);
                    }
                    GridQuality.Rows[row].Cells["colFlag"].Value = IsRight;
                    GridQuality.Rows[row].Cells["colAnswer"].Value = answer_value;
                    GridQuality.Rows[row].Cells["colUserid"].Value = LoginInfo.UserInfo.USER_CODE;
                    GridQuality.Rows[row].Cells["coltime"].Value = time11;
                    GridQuality.Rows[row].DefaultCellStyle.BackColor = Color.Green;
                }
                if (LoginInfo.StationInfo.STATION_TYPE != "ST05")
                {
                    if (Thetype == "A")
                    {
                        ShowQualityList1();
                    }
                    else if (Thetype == "B")
                    {
                        ShowQualityList();
                    }
                }
                e.Cancel = false;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //timer1.Enabled = false;
            //ShowQualityList();
            //this.GridQuality.Focus();
            //SendKeys.Send("+{TAB}");
            //SendKeys.Send("{TAB}");
            //GetFocus(0);
        }





    }
}
