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
    //显示返修记录及差异清单
    public partial class ctrl_fxbomcom : BaseControl
    {
        public string SN, So, productmodel;
        public string planCode, orderCode;
        public string companyCode, PlineCode, Plineid, stationCode, stationtype, WorkunitCode, LinesideStock, stationname, pline_type, Station_Code, stationcode_fx, stationname_fx;
        public bool IsABC = false;
        //dataConn dc = new dataConn();
        public ctrl_fxbomcom()
        {
            InitializeComponent();

        }
        public ctrl_fxbomcom(string sn1,string plancode1)
        {
            InitializeComponent();
            SN = sn1;
            planCode = plancode1;

            dgv1.DataSource = null;
            dgv2.DataSource = null;
            dgv3.DataSource = null;

            string usercode = LoginInfo.UserInfo.USER_CODE;
            dataConn.ExeSql("delete from rst_ghtm_bom_comp where yhdm='" + LoginInfo.UserInfo.USER_CODE + "'");

            string oldplancode1 = "", oldso1 = "", newplancode1 = "", newso1 = "",plinecode1="";
            string sql = "";
            sql = "select plan_code,plan_so,pline_code from data_product where sn='"+sn1+"' ";
            DataTable dt = dataConn.GetTable(sql);
            if (dt.Rows.Count > 0)
            {
                newplancode1 = dt.Rows[0][0].ToString();
                newso1 = dt.Rows[0][1].ToString();
                plinecode1 = dt.Rows[0][2].ToString();
            }
            sql = "select plan_code,plan_so from data_record where sn='" + sn1 + "' order by work_time desc ";
            dt = dataConn.GetTable(sql);
            if (dt.Rows.Count > 0)
            {
                oldplancode1 = dt.Rows[0][0].ToString();
                oldso1 = dt.Rows[0][1].ToString();
            }
            MW_COMPARE_GHTMBOM sp = new MW_COMPARE_GHTMBOM()
            {
                GHTM1 = sn1,
                GZDD1 = plinecode1,
                JHDM1 = oldplancode1,
                JHSO1 = oldso1,
                GZFLAG1 = "OLD",
                GZDD2 = plinecode1,
                JHDM2 = newplancode1,
                JHSO2 = newso1,
                GZFLAG2 = "NEW",
                YHDM1 = usercode
            };
            Procedure.run(sp);

            sql = "select GHTM ,ABOM_COMP ,ABOM_DESC ,ABOM_QTY ,ABOM_OP ,ABOM_WKCTR ,ABOM_JHDM ,ABOM_SO,GZDD ,ABOM_GYS ,case COMP_FLAG when '0' then '相同' when '1' then '原BOM有' when '2' then '新BOM有' end  COMP_FLAG FROM RST_GHTM_BOM_COMP where YHDM='" + usercode + "' order by comp_flag desc,abom_comp asc";
            DataTable dt1 = dataConn.GetTable(sql);
            sql = "select sn,plan_code,station_name,location_code,process_code,item_code,item_name,item_qty,item_vendor,item_sn,to_char(create_time,'yyyy-mm-dd hh24:mi:ss') create_time  from vw_data_sn_bom_temp where sn='" + sn1 + "' and plan_code='" + plancode1 + "'  order by location_code,process_code,item_code,create_time desc";
            DataTable dt2 = dataConn.GetTable(sql);
            sql = "select sn,plan_code,station_name,location_code,detect_name,detect_value,user_id,to_char(work_time,'yyyy-mm-dd hh24:mi:ss') work_time from data_sn_detect_data_temp where sn='" + sn1 + "' and plan_code='" + plancode1 + "'  order by work_time desc";
            DataTable dt3 = dataConn.GetTable(sql);

            dgv1.DataSource = dt1;
            dgv2.DataSource = dt2;
            dgv3.DataSource = dt3;
            ShowQualityList();
            timer1.Enabled = true;
        }

        private void ShowQualityList()
        {

            for (int j = 0; j < dgv1.Rows.Count; j++)
            {
                string detect_flag = GetGridValue(dgv1, j, "colReplaceFlag");
                dgv1.Rows[j].DefaultCellStyle.BackColor = Color.White;
                if (detect_flag == "原BOM有")
                    dgv1.Rows[j].DefaultCellStyle.BackColor = Color.Red;
                else if (detect_flag == "新BOM有")
                    dgv1.Rows[j].DefaultCellStyle.BackColor = Color.FromArgb(0, 255, 0);
                dgv1.Rows[j].DefaultCellStyle.ForeColor = Color.FromArgb(0, 0, 0);
            }
        }
        private string GetGridValue(DataGridView dgv, int rowid, string colname)
        {
            object cell_val = dgv.Rows[rowid].Cells[colname].Value;
            if (cell_val == null) return "";
            else return cell_val.ToString();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            ShowQualityList();
        }

    }
}
