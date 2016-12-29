using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Windows.Forms;
using DevExpress.Web.ASPxEditors;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxClasses;
using Rmes.Pub.Data;
using Rmes.DA.Base;
using Rmes.Web.Base;

/**
 * 功能概述：总成零件属性维护
 * 作者：杨少霞
 * 创建时间：2016-08-09
**/


namespace Rmes.WebApp.Rmes.Atpu.atpu1D00
{
    public partial class atpu1D00 : BasePage
    {
        private dataConn dc = new dataConn();
        private PubCs pc = new PubCs();
        public string theCompanyCode, theUserId, theUserName;
        public string theProgramCode;
        public DateTime theBeginDate, theEndDate;

        protected void Page_Load(object sender, EventArgs e)
        {
            userManager theUserManager = (userManager)Session["theUserManager"];
            theCompanyCode = theUserManager.getCompanyCode();
            theUserId = theUserManager.getUserId();
            theUserName = theUserManager.getUserName();
            theProgramCode = "atpu1C00";

            string Sql3 = "SELECT distinct a.PT_PART FROM COPY_PT_MSTR a left join copy_ptp_det b on a.PT_PART=b.PTP_PART "
                        + "where b.ptp_phantom=0 and upper(a.pt_group)='M' and upper(b.ptp_site) in('DCEC-B','DCEC-C') order by PT_PART ";
            SqlDataSource3.SelectCommand = Sql3;
            SqlDataSource3.DataBind();

            setCondition1();
            setCondition2();
            
        }

        //初始化总成零件维护
        private void setCondition1()
        {
            string sql = "SELECT A.ABOM_COMP,TO_CHAR(A.ZDRQ,'YYYY-MM-DD') ZDRQ,TO_CHAR(A.RQBEGIN,'YYYY-MM-DD') RQBEGIN,TO_CHAR(A.RQEND,'YYYY-MM-DD') RQEND,"
                       + " B.PT_DESC2,A.LJMC,a.SFXS,a.SFHC,a.SFZC "
                       + " FROM ATPUBOMKZB A LEFT JOIN COPY_PT_MSTR B ON A.ABOM_COMP=B.PT_PART "
                       + " ORDER BY  A.INPUT_TIME desc nulls last, A.ABOM_COMP ";
            DataTable dt = dc.GetTable(sql);

            ASPxGridView1.DataSource = dt;
            ASPxGridView1.DataBind();
        }
        
        //新增
        protected void ASPxGridView1_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            ASPxComboBox ljdm = ASPxGridView1.FindEditFormTemplateControl("comboLJDM") as ASPxComboBox;
            ASPxDateEdit begindate1 = ASPxGridView1.FindEditFormTemplateControl("dateStart") as ASPxDateEdit;
            ASPxDateEdit enddate1 = ASPxGridView1.FindEditFormTemplateControl("dateEnd") as ASPxDateEdit;
            
            string ljmc = "";
            string sqlC = "select PT_DESC2 from COPY_PT_MSTR where pt_part='"+ljdm.Value.ToString()+"'";
            DataTable dt = dc.GetTable(sqlC);
            if (dt.Rows.Count > 0)
            {
                ljmc = dt.Rows[0]["PT_DESC2"].ToString();
            }

            ASPxCheckBox chZCXSFlag = ASPxGridView1.FindEditFormTemplateControl("chZCFlag") as ASPxCheckBox;
            string xsFlag = "";
            if (chZCXSFlag.Checked == true)
            {
                xsFlag = "1";
            }
            else
            {
                xsFlag = "0";
            }
            //string Sql = "UPDATE ATPUBOMKZB SET SFXS='" + xsFlag + "' "
            //     + " WHERE abom_comp = '" + ljdm.Value.ToString() + "'";
            //dc.ExeSql(Sql);

            ////记录操作start
            //string logSql = "insert into atpubomkzb_log(abom_comp,zdrq,rqbegin,rqend,rqsj,yhmc,czms) "
            //              + " select abom_comp,zdrq,rqbegin,rqend,sysdate,'" + theUserName + "','修改了该零件的显示属性' "
            //              + " from  atpubomkzb where abom_comp='" + ljdm.Value.ToString() + "' ";
            //dc.ExeSql(logSql);
            ////记录操作end
            ASPxCheckBox chHCXSFlag = ASPxGridView1.FindEditFormTemplateControl("chHCFlag") as ASPxCheckBox;
            string hcFlag = "";
            if (chHCXSFlag.Checked == true)
            {
                hcFlag = "1";
            }
            else
            {
                hcFlag = "0";
            }
            ASPxCheckBox chHCZCFlag = ASPxGridView1.FindEditFormTemplateControl("chHCZCFlag") as ASPxCheckBox;
            string hcZCFlag = "";
            if (chHCZCFlag.Checked == true && chHCXSFlag.Checked == true)
            {
                hcZCFlag = "1";
            }
            else
            {
                hcZCFlag = "0";
            }
            //string Sql = "UPDATE ATPUBOMKZB SET SFHC='" + hcFlag + "', SFZC='" + hcZCFlag + "' "
            //     + " WHERE abom_comp = '" + ljdm.Value.ToString() + "'";
            //dc.ExeSql(Sql);

            ////记录操作start
            //string logSql = "insert into atpubomkzb_log(abom_comp,zdrq,rqbegin,rqend,rqsj,yhmc,czms) "
            //              + " select abom_comp,zdrq,rqbegin,rqend,sysdate,'" + theUserName + "','修改了该零件的回冲属性' "
            //              + " from  atpubomkzb where abom_comp='" + ljdm.Value.ToString() + "' ";
            //dc.ExeSql(logSql);
            ////记录操作end

            string Sql = "INSERT INTO ATPUBOMKZB (ABOM_COMP,ZDRQ,RQBEGIN,RQEND,LJMC,SFXS,SFHC,SFZC,INPUT_PERSON,INPUT_TIME) "
                   + " VALUES ( '" + ljdm.Value.ToString() + "',sysdate,to_date('" + begindate1.Text.Trim() + "','yyyy-MM-dd'),to_date('" + enddate1.Text.Trim() + "','yyyy-MM-dd'),'" + ljmc + "','" + xsFlag + "' ,'" + hcFlag + "','" + hcZCFlag + "','"+theUserId+"',SYSDATE)";
            dc.ExeSql(Sql);

            //记录操作start
            string logSql = "insert into atpubomkzb_log(abom_comp,zdrq,rqbegin,rqend,SFXS,SFHC,SFZC,rqsj,yhmc,czms)"
                          + " select abom_comp,zdrq,rqbegin,rqend,SFXS,SFHC,SFZC,sysdate,'" + theUserName + "','增加了新的零件' "
                          + " from  atpubomkzb where abom_comp='" + ljdm.Value.ToString() + "'";
            dc.ExeSql(logSql);
            //记录操作end


            e.Cancel = true;
            ASPxGridView1.CancelEdit();
            setCondition1();
        }

        //删除
        protected void ASPxGridView1_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            //判断当前记录是否可以删除
            string strD = e.Values["ABOM_COMP"].ToString();

            string strTableName = "ATPUBOMKZB";

            dataConn theDataConn = new dataConn("select func_check_delete_data('" + strTableName + "','MES','MES','MES','MES','" + strD + "') from dual");

            theDataConn.OpenConn();
            string theRet = theDataConn.GetValue();
            if (theRet != "Y")
            {
                ASPxGridView1.JSProperties.Add("cpCallbackName", "Delete");
                ASPxGridView1.JSProperties.Add("cpCallbackRet", theRet);
                theDataConn.CloseConn();
            }
            else
            {
                //记录操作start
                string logSql = "insert into atpubomkzb_log(abom_comp,zdrq,rqbegin,rqend,SFXS,SFHC,SFZC,rqsj,yhmc,czms) "
                              + " select abom_comp,zdrq,rqbegin,rqend,SFXS,SFHC,SFZC,sysdate,'" + theUserName + "','删除了该零件' "
                              + " from  atpubomkzb where abom_comp='" + strD + "'";
                dc.ExeSql(logSql);
                //记录操作end

                //确认删除
                string Sql = "delete from ATPUBOMKZB WHERE  ABOM_COMP =  '" + strD + "' ";
                dc.ExeSql(Sql);
            }
            e.Cancel = true;
            setCondition1();
        }

        //修改
        protected void ASPxGridView1_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            ASPxComboBox ljdm = ASPxGridView1.FindEditFormTemplateControl("comboLJDM") as ASPxComboBox;
            ASPxDateEdit begindate1 = ASPxGridView1.FindEditFormTemplateControl("dateStart") as ASPxDateEdit;
            ASPxDateEdit enddate1 = ASPxGridView1.FindEditFormTemplateControl("dateEnd") as ASPxDateEdit;
            ASPxCheckBox chZCXSFlag = ASPxGridView1.FindEditFormTemplateControl("chZCFlag") as ASPxCheckBox;
            string xsFlag = "";
            if (chZCXSFlag.Checked == true)
            {
                xsFlag = "1";
            }
            else
            {
                xsFlag = "0";
            }
             
            ASPxCheckBox chHCXSFlag = ASPxGridView1.FindEditFormTemplateControl("chHCFlag") as ASPxCheckBox;
            string hcFlag = "";
            if (chHCXSFlag.Checked == true)
            {
                hcFlag = "1";
            }
            else
            {
                hcFlag = "0";
            }
            ASPxCheckBox chHCZCFlag = ASPxGridView1.FindEditFormTemplateControl("chHCZCFlag") as ASPxCheckBox;
            string hcZCFlag = "";
            if (chHCZCFlag.Checked == true && chHCXSFlag.Checked == true)
            {
                hcZCFlag = "1";
            }
            else
            {
                hcZCFlag = "0";
            }

            string Sql = "UPDATE ATPUBOMKZB SET rqbegin=to_date('" + begindate1.Text.Trim() + "','yyyy-MM-dd'),rqend=to_date('" + enddate1.Text.Trim() + "','yyyy-MM-dd'), "
                + "SFXS='" + xsFlag + "',SFHC='" + hcFlag + "',SFZC='" + hcZCFlag + "' WHERE abom_comp = '" + ljdm.Value.ToString() + "'";
               
            dc.ExeSql(Sql);

            //记录操作start
            string logSql = "insert into atpubomkzb_log(abom_comp,zdrq,rqbegin,rqend,SFXS,SFHC,SFZC,rqsj,yhmc,czms) "
                          + " select abom_comp,zdrq,rqbegin,rqend,SFXS,SFHC,SFZC,sysdate,'" + theUserName + "','修改了该零件' "
                          + " from  atpubomkzb where abom_comp='" + ljdm.Value.ToString() + "' ";
            dc.ExeSql(logSql);
            //记录操作end

            e.Cancel = true;
            ASPxGridView1.CancelEdit();
            setCondition1();
        }
        //创建EDITFORM前
        protected void ASPxGridView1_HtmlEditFormCreated(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewEditFormEventArgs e)
        {
            if (!ASPxGridView1.IsNewRowEditing && ASPxGridView1.IsEditing)
            {
                ///主键不可以修改
                (ASPxGridView1.FindEditFormTemplateControl("comboLJDM") as ASPxComboBox).Enabled = false;

                //处理ASPxCheckBox
                string a = ASPxGridView1.GetRowValues(ASPxGridView1.FocusedRowIndex, "RQEND").ToString();
                if (ASPxGridView1.GetRowValues(ASPxGridView1.FocusedRowIndex, "RQEND").ToString() != "")
                {
                    (ASPxGridView1.FindEditFormTemplateControl("chValidFlag") as ASPxCheckBox).Checked = true;
                }
                else
                {
                    (ASPxGridView1.FindEditFormTemplateControl("chValidFlag") as ASPxCheckBox).Checked = false;
                }

               
                if (ASPxGridView1.GetRowValues(ASPxGridView1.FocusedRowIndex, "RQBEGIN").ToString() != "")
                {
                    theBeginDate = Convert.ToDateTime(ASPxGridView1.GetRowValues(ASPxGridView1.FocusedRowIndex, "RQBEGIN").ToString());
                }
                if (ASPxGridView1.GetRowValues(ASPxGridView1.FocusedRowIndex, "RQEND").ToString() != "")
                {
                    theEndDate = Convert.ToDateTime(ASPxGridView1.GetRowValues(ASPxGridView1.FocusedRowIndex, "RQEND").ToString());
                }
                //处理ASPxCheckBox
                if (ASPxGridView1.GetRowValues(ASPxGridView1.FocusedRowIndex, "SFXS").ToString() == "1")
                {
                    (ASPxGridView1.FindEditFormTemplateControl("chZCFlag") as ASPxCheckBox).Checked = true;
                }
                else
                {
                    (ASPxGridView1.FindEditFormTemplateControl("chZCFlag") as ASPxCheckBox).Checked = false;
                }
                //处理ASPxCheckBox
                if (ASPxGridView1.GetRowValues(ASPxGridView1.FocusedRowIndex, "SFHC").ToString() == "1")
                {
                    (ASPxGridView1.FindEditFormTemplateControl("chHCFlag") as ASPxCheckBox).Checked = true;
                }
                else
                {
                    (ASPxGridView1.FindEditFormTemplateControl("chHCFlag") as ASPxCheckBox).Checked = false;
                }
                //处理ASPxCheckBox
                if (ASPxGridView1.GetRowValues(ASPxGridView1.FocusedRowIndex, "SFZC").ToString() == "1")
                {
                    (ASPxGridView1.FindEditFormTemplateControl("chHCZCFlag") as ASPxCheckBox).Checked = true;
                }
                else
                {
                    (ASPxGridView1.FindEditFormTemplateControl("chHCZCFlag") as ASPxCheckBox).Checked = false;
                }
               
            }
        }

        //修改数据校验
        protected void ASPxGridView1_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
        {
            //判断为空
            if (e.NewValues["ABOM_COMP"].ToString() == "" || e.NewValues["ABOM_COMP"].ToString() == null)
            {
                e.RowError = "零件代码不能为空！";
            }

            string ljdm = e.NewValues["ABOM_COMP"].ToString().Trim();
            
            //判断超长
            if (ljdm.Length > 30)
            {
                e.RowError = "初始值字节长度不能超过30！";
            }

            //日期判断
            ASPxDateEdit begindate = ASPxGridView1.FindEditFormTemplateControl("dateStart") as ASPxDateEdit;
            ASPxDateEdit enddate = ASPxGridView1.FindEditFormTemplateControl("dateEnd") as ASPxDateEdit;
            string begindate1 = begindate.Text.ToString().Trim();
            string enddate1 = enddate.Text.ToString().Trim();
            string nowtime1 = DateTime.Now.ToString("yyyy-MM-dd");

            if (nowtime1.CompareTo(begindate1) > 0)
            {
                e.RowError = "生效日期不能小于当前日期！";
                return;
            }
            if (begindate1.CompareTo(enddate1) > 0 && enddate1 != "")
            {
                e.RowError = "失效日期不能小于生效日期！";
                return;
            }

            ASPxCheckBox chVFlag = ASPxGridView1.FindEditFormTemplateControl("chValidFlag") as ASPxCheckBox;
            string vFlag = "";
            if (chVFlag.Checked == true)
            {
                vFlag = "1";
            }
            else
            {
                vFlag = "0";
            }
            if (vFlag == "0" && enddate1 != "")
            {
                e.RowError = "没有勾选是否失效时，请勿录入失效日期！";
                return;
            }
            if (vFlag == "1" && enddate1 == "")
            {
                e.RowError = "勾选了是否失效时，请录入失效日期！";
                return;
            }
            //判断是否重复
            if (ASPxGridView1.IsNewRowEditing)
            {
                string chSql = "select * from atpubomkzb where abom_comp='" + ljdm + "'";
                DataTable dt = dc.GetTable(chSql);
                if (dt.Rows.Count > 0)
                {
                    e.RowError = "已存在相同的记录！";
                }
            }

        }

        //初始化分装总成维护
        private void setCondition2()
        {
            string sql = "SELECT * FROM ATPUFZZCB ORDER BY INPUT_TIME desc nulls last, SCXDM";
            DataTable dt = dc.GetTable(sql);

            ASPxGridView2.DataSource = dt;
            ASPxGridView2.DataBind();
        }

        //新增
        protected void ASPxGridView2_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            ASPxTextBox zcdm = ASPxGridView2.FindEditFormTemplateControl("txtZCDM") as ASPxTextBox;
            ASPxComboBox scxdm = ASPxGridView2.FindEditFormTemplateControl("comboSCXDM") as ASPxComboBox;
            ASPxTextBox fdjxl = ASPxGridView2.FindEditFormTemplateControl("txtFDJXL") as ASPxTextBox;

            string Sql = "INSERT INTO ATPUFZZCB (ZCDM,SCXDM,FDJXL,INPUT_PERSON,INPUT_TIME) "
                   + " VALUES ( UPPER('" + zcdm.Value.ToString() + "'),UPPER('" + scxdm.Value.ToString() + "'),UPPER('" + fdjxl.Value.ToString() + "'),'"+theUserId+"',SYSDATE) ";
            dc.ExeSql(Sql);

            //插入日志表161103
            string sql2 = "insert into ATPUFZZCB_LOG(ZCDM,SCXDM,FDJXL,USER_CODE,FLAG_LOG,RQSJ) "
                       + "VALUES(UPPER('" + zcdm.Value.ToString() + "'),UPPER('" + scxdm.Value.ToString() + "'),UPPER('" + fdjxl.Value.ToString() + "'),"
                       + "'" + theUserId + "','ADD',SYSDATE)";
            dc.ExeSql(sql2);

            e.Cancel = true;
            ASPxGridView2.CancelEdit();
            setCondition2();
        }

        //删除
        protected void ASPxGridView2_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            //判断当前记录是否可以删除
            string zcdm = e.Values["ZCDM"].ToString();

            //插入日志表161103
            string sql2 = "insert into ATPUFZZCB_LOG(ZCDM,SCXDM,FDJXL,USER_CODE,FLAG_LOG,RQSJ) "
                       + "SELECT ZCDM,SCXDM,FDJXL,'" + theUserId + "','DEL',SYSDATE FROM ATPUFZZCB WHERE ZCDM =  '" + zcdm + "'"
                       + "";
            dc.ExeSql(sql2);
            //确认删除
            string sql = "delete from ATPUFZZCB WHERE  ZCDM =  '" + zcdm + "' ";
            dc.ExeSql(sql);

            e.Cancel = true;
            setCondition2();
        }

        //数据校验
        protected void ASPxGridView2_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
        {
            //判断为空
            if (e.NewValues["ZCDM"].ToString() == "" || e.NewValues["ZCDM"].ToString() == null)
            {
                e.RowError = "总成零件代码不能为空！";
            }

            string zcdm = e.NewValues["ZCDM"].ToString().Trim();

            //判断ZCDM是否在COPY_PS_MSTR中
            string sql = " SELECT * FROM COPY_PS_MSTR WHERE PS_PAR = UPPER('" + zcdm + "') ";
            DataTable dt1 = dc.GetTable(sql);
            if (dt1.Rows.Count < 1)
            {
                e.RowError = "总成零件代码不存在！";
            }

            //判断是否重复
            if (ASPxGridView2.IsNewRowEditing)
            {
                string chSql = "select * from ATPUFZZCB where ZCDM = UPPER('" + zcdm + "') ";
                DataTable dt = dc.GetTable(chSql);
                if (dt.Rows.Count > 0)
                {
                    e.RowError = "已存在相同的记录！";
                }
            }

        }
    
        
    }
}