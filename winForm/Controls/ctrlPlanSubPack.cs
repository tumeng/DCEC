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
using Rmes.Pub.Data1;

namespace Rmes.WinForm.Controls
{
    public partial class ctrlPlanSubPack : BaseControl
    {
        private string CompanyCode = LoginInfo.CompanyInfo.COMPANY_CODE;
        private string PLineID = LoginInfo.ProductLineInfo.RMES_ID;
        private string StationID = LoginInfo.StationInfo.RMES_ID;
        private string PLineCode= LoginInfo.ProductLineInfo.PLINE_CODE;
        private string StationCode = LoginInfo.StationInfo.STATION_CODE;
        //private dataConn dc = new dataConn();
        private RMESEventArgs arg = new RMESEventArgs();
        private string  PlineCode, PlineID, stationname, TheSo, TheSn = "", ThePlancode="";
        ProductInfoEntity product;

        public ctrlPlanSubPack()
        {
            InitializeComponent();
            CompanyCode = LoginInfo.CompanyInfo.COMPANY_CODE;
            StationCode = LoginInfo.StationInfo.STATION_CODE;
            initPlan();
            grdPlan.AutoGenerateColumns = false;
            grdPlan.RowHeadersVisible = false;

            //string sql = "select rmes_id,sn,plan_so,plan_code,pline_code,station_code,sub_zc,check_flag from DATA_SUBPACK where WORK_FLAG='N' and station_code='" + StationCode + "' order by start_time ";
            //DataTable dt = dataConn.GetTable(sql);
            //if (dt.Rows.Count > 0)
            //{
            //    string planmodel ="";
            //    try
            //    {
            //        planmodel = dataConn.GetValue("select product_model from data_plan where plan_code='" + dt.Rows[0]["PLAN_CODE"].ToString() + "' and rownum=1");
            //    }
            //    catch { }
            //    arg.MessageHead = "FILLSN";
            //    arg.MessageBody = dt.Rows[0]["SN"].ToString() + "^" + dt.Rows[0]["PLAN_SO"].ToString() + "^" + planmodel + "^" + dt.Rows[0]["PLAN_CODE"].ToString();
            //    SendDataChangeMessage(arg);
            //}
            this.RMesDataChanged += new RMesEventHandler(ctrl_Tj_RMesDataChanged);
        }
        void ctrl_Tj_RMesDataChanged(object obj, Rmes.WinForm.Base.RMESEventArgs e)
        {
            RMESEventArgs arg = new RMESEventArgs();
            arg.MessageHead = "";
            arg.MessageBody = "";
            if (e.MessageHead == null) return;
            if (e.MessageHead == "GETSUB")
            {
                //BOM扫描完成后刷新流水号
                TheSn = e.MessageBody.ToString();
                string sql = "select rmes_id,sn,plan_so,plan_code,pline_code,station_code,sub_zc,check_flag from DATA_SUBPACK where WORK_FLAG='N' and station_code='" + StationCode + "' and sn!='"+TheSn+"' order by start_time ";
                DataTable dt = dataConn.GetTable(sql);
                if (dt.Rows.Count > 0)
                {
                    string planmodel = "";
                    try
                    {
                        planmodel = dataConn.GetValue("select product_model from data_plan where plan_code='" + dt.Rows[0]["PLAN_CODE"].ToString() + "' and rownum=1");
                    }
                    catch { }
                    arg.MessageHead = "FILLSN";
                    arg.MessageBody = dt.Rows[0]["SN"].ToString() + "^" + dt.Rows[0]["PLAN_SO"].ToString() + "^" + planmodel + "^" + dt.Rows[0]["PLAN_CODE"].ToString();
                    SendDataChangeMessage(arg);
                }
                else
                {
                    arg.MessageHead = "FILLSN";
                    arg.MessageBody = "" + "^" + "" + "^" + "" + "^" + "";
                    SendDataChangeMessage(arg);
                }
            }

        }
        private void initPlan()
        {
            string sql = "select rmes_id,sn,plan_so,plan_code,pline_code,station_code,sub_zc,check_flag from DATA_SUBPACK where WORK_FLAG='N' and station_code='" + StationCode + "' order by start_time ";
            DataTable dt = dataConn.GetTable(sql);
            grdPlan.DataSource = dt;
        }
                

        private void grdPlan_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void grdPlan_MouseLeave(object sender, EventArgs e)
        {
            //timer1.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            initPlan();
        }

        //private void grdPlan_CellContentClick(object sender, DataGridViewCellEventArgs e)
        //{
        //    //if (e.RowIndex == -1) return;
        //    //if (grdPlan.Columns[e.ColumnIndex].Name == "colCmdConfirm")
        //    //{
        //    //    string sn = grdPlan.Rows[e.RowIndex].Cells["colSN"].Value.ToString();
        //    //    ProductEntity newProduct;
        //    //    newProduct = ProductFactory.GetByCompanyCodeSN(LoginInfo.CompanyInfo.COMPANY_CODE, sn);
        //    //    RMESEventArgs thisEventArg = new RMESEventArgs();
        //    //    thisEventArg.MessageHead = "STATIONCOMPLETED";
        //    //    thisEventArg.MessageBody = newProduct;
        //    //    SendDataChangeMessage(thisEventArg);
        //    //}

        //}


    }
}
