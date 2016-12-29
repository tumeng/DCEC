using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PetaPoco;
using Rmes.DA.Base;
using Rmes.DA.Entity;
using Rmes.DA.Dal;
using Rmes.DA.Factory;
using Rmes.Pub.Data;
using Rmes.Web.Base;
using System.Data;
using DevExpress.Web.ASPxGridView;
using Rmes.DA.Procedures;
/**
 * 功能概述：铭牌及VEPS参数查询
 * 作者：游晓航
 * 创建时间：2016-06-21
 */
namespace Rmes.WebApp.Rmes.Atpu.atpu1400
{
    public partial class atpu1400 : BasePage
    {
      
        private dataConn dc = new dataConn();
        PubCs thePubCs = new PubCs();
        public string theCompanyCode, theUserId;
        public string theProgramCode;
        protected void Page_Load(object sender, EventArgs e)
        {
            userManager theUserManager = (userManager)Session["theUserManager"];
            theCompanyCode = theUserManager.getCompanyCode();
            theUserId = theUserManager.getUserId();
            theProgramCode = "atpu1400";
            initPlineCode();
            //string sql="SELECT PLAN_CODE,PLINE_CODE,BZSO,JX,GL,ZS,QFJQ 气阀进气,QFPQ 气阀排气,PL 排量,DS 怠速,XL 系列,FHCX 发火次序, "
            //    +"XNBH 性能表号,DYJX 打印机型,KHH 客户号,GD 固定日期,JZL 净重量,PYZS 喷油正时,EDGYL 额定供油率,HB 海拔, "
            //    +"ENYN 是否英文,PRSG 是否客户号代替总成号,GL1 额定功率二,ZS1 额定转速二,BYGL1 备用功率一,BYZS1 备用转速一, "
            //    +"BYGL2 备用功率二,BYZS2 备用转速二,NOX,PM ,KHGG 客户规格,SCXKZ 生产许可证,EPA,PFJD 排放阶段,PFJDHZH 排放阶段核准号, "
            //    +"FR,MPLJH 铭牌零件号,XZJD 限值阶段,DYGLD 对应功率段,ZDJGL 最大净功率,ZXBZ 执行标准,XZMC 系族名称, "
            //    +"XSHZHHMLY 形式核准号豁免理由,HCLZZLX 后处理装置类型 FROM atpuplannameplate";
            string sql = "select * from atpuplannameplate where pline_code in (select pline_code from vw_user_role_program where user_id='" + theUserId + "' and program_code='" + theProgramCode + "' and company_code='" + theCompanyCode + "')  and plan_code='"+txtPlan.Text.ToUpper()+"'";

            DataTable dt = dc.GetTable(sql);
            ASPxGridView1.DataSource = dt;
            ASPxGridView1.DataBind();


            string sql2 = "SELECT * FROM COPY_VEPS_DATA where so='" + txtSoQry.Text.Trim().ToUpper() + "'";

            DataTable dt2 = dc.GetTable(sql2);
            ASPxGridView2.DataSource = dt2;
            ASPxGridView2.DataBind();

        }
        //初始化生产线代码列表
        private void initPlineCode()
        {
            string sql = "select distinct a.pline_code,b.pline_name from vw_user_role_program a left join code_product_line b on a.pline_code=b.pline_code  where user_id='" + theUserId + "'  and a.program_code='" + theProgramCode + "' and a.company_code='" + theCompanyCode + "' ";
            boxpline.SelectCommand = sql;
            boxpline.DataBind();
        }
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                VEPS_SDGX sp = new VEPS_SDGX()
                {

                    SO1 = txtSoQry.Text.Trim().ToUpper()
                };

                Procedure.run(sp);

                Response.Write("<script>alert('VEPS更新成功!')</script>");

            }
            catch (Exception e1)
            {
                //MessageBox.Show("VEPS更新失败"+e1.Message+"!", "提示");
                //Show(this, "VEPS更新失败" + e1.Message + "!");
                Response.Write("<script>alert('VEPS更新失败!')</script>");
            }
        }

        protected void btnCx1_Click(object sender, EventArgs e)
        {

        }

        protected void btnCx_Click(object sender, EventArgs e)
        {

        } 
    }
}