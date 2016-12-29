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
using System.Reflection;
using System.Diagnostics;
using Rmes.Pub.Data1;
using System.Runtime.InteropServices;

namespace Rmes.WinForm.Controls
{
    /// <summary>
    /// 条码扫描控件，开发者：倪晓光
    /// </summary>
    public partial class ctrlProductScan : BaseControl
    {
        //[DllImport(@"..\OCI\SPCclient.dll")]
        [DllImport(@"C:\SPCclient.dll")]
        //[DllImport(@"D:\CHKMES\rmes.workstation\Lib\FNTHEX32.dll")]
        //public static extern void sendcode(string stationcode, int len_station, string sn, int sn_len);
        public static extern void sendcode(string sn, int sn_len);

        private List<MessageEntity> MessageList;

        private string CompanyCode, PlineID, PlineCode1, StationID, Cl_plan = "", TheSmTm="", Cl_so = "",oldPlinecode="", Cl_pmodel = "", len_sn, TheSo, TheSn, ThePlancode, StationName, Station_Code;
        private string ShiftCode,TeamCode,UserID;
        private bool SkipFlag = false;
        ProductInfoEntity product;
        //dataConn dc = new dataConn();
        //dataConn dc;
        public ctrlProductScan()
        {
            InitializeComponent();
            this.RMesDataChanged += new Rmes.WinForm.Base.RMesEventHandler(ctrlProductScan_RmesDataChanged);
            MessageList = new List<MessageEntity>();
            CompanyCode = LoginInfo.CompanyInfo.COMPANY_CODE;
            PlineID = LoginInfo.ProductLineInfo.RMES_ID;
            PlineCode1 = LoginInfo.ProductLineInfo.PLINE_CODE;
            StationID = LoginInfo.StationInfo.RMES_ID;
            Station_Code = LoginInfo.StationInfo.STATION_CODE;
            StationName = LoginInfo.StationInfo.STATION_NAME;
            ShiftCode = LoginInfo.ShiftInfo.SHIFT_CODE;
            TeamCode = LoginInfo.TeamInfo.TEAM_CODE;
            UserID = LoginInfo.UserInfo.USER_ID;
            len_sn = LoginInfo.LEN_SN;
            TheSo = "";
            TheSn = "";
            ThePlancode = "";
            GetFocused(txtSN);
            timer1.Enabled = true;
            //txtSN.Text = "12345678901";
        }

        /// <summary>
        /// 通过消息头执行指定的功能
        /// </summary>
        /// <param name="MessageHead">消息头</param>

        protected void ctrlProductScan_RmesDataChanged(object obj, Rmes.WinForm.Base.RMESEventArgs e)
        {
                string sn,quality_status="A";
                RMESEventArgs arg = new RMESEventArgs();
                arg.MessageHead = "";
                arg.MessageBody = "";
                if (e.MessageHead == null) return;

                this.Visible = true;
                if (e.MessageHead == "INIT")//初始化控件
                {
                    GetFocused(txtSN);
                }
                if (e.MessageHead == "PRINTISDE")
                {
                    string[] txt = e.MessageBody.ToString().Split('|');
                    TheSmTm = txt[0];
                    ThePlancode = txt[1];
                }
                if (e.MessageHead == "EXIT")//退出系统
                {
                    dataConn.ExeSql("update data_complete set complete_time=sysdate where station_code='" + LoginInfo.StationInfo.STATION_CODE + "' and complete_time is null ");
                }
                
                if (e.MessageHead == "FILLSN")//初始化控件
                {
                    string[] txt = e.MessageBody.ToString().Split('^');
                    string sn1 = txt[0];
                    txtSN.Text = sn1;
                    ThePlancode = txt[3];
                    GetFocused(txtSN);
                }
                if (e.MessageHead == "FILLSN1")//初始化控件
                {
                    string txt = e.MessageBody.ToString();
                    txtSN.Text = txt;
                    GetFocused(txtSN);
                }
                if (e.MessageHead == "SHOWBOMLQ")
                {
                    string txt = e.MessageBody.ToString();
                    ProductInfoEntity product11 = ProductInfoFactory.GetByCompanyCodeSNSingle(LoginInfo.CompanyInfo.COMPANY_CODE, txt);
                    if (product11 == null) return;
                    string oldplancode = "";
                    oldplancode = dataConn.GetValue("select jhmcold from atpujhsn where jhmc='" + product11.PLAN_CODE + "'  and is_valid='N' and rownum=1");
                    if (oldplancode == "")
                        return;

                    ProductInfoEntity productold = ProductInfoFactory.GetByCompanyCodeSNSingleLQ(LoginInfo.CompanyInfo.COMPANY_CODE, txt, oldplancode);

                    oldPlinecode = productold.PLINE_CODE;

                }
                if (e.MessageHead == "CL_PLAN")//CL生产线得到计划
                {
                    string[] txt = e.MessageBody.ToString().Split('^');
                    Cl_plan = txt[0];
                    Cl_so = txt[1];
                    Cl_pmodel = txt[2];
                    GetFocused(txtSN);
                }
                if (e.MessageHead == "RECHECK")//重复扫描 
                { 
                    sn = e.MessageBody.ToString();
                    product = ProductInfoFactory.GetByCompanyCodeSNSingle(CompanyCode, sn);//获取sn信息
                    if (product != null)
                    {
                        TheSo = product.PLAN_SO;
                        TheSn = product.SN;
                        ThePlancode = product.PLAN_CODE;
                        //判断是否是质量站点 是 则显示前道站点质量信息
                        //string sql = "select station_code from vw_code_station where station_type='ST15' and pline_code='" + PlineCode1 + "' and station_seq <(select nvl(station_seq,0) from vw_code_station where station_code='" + Station_Code + "') order by station_seq desc ";
                        string sql = "select * from REL_QA_STATION_PRESTATION t left join code_station a on t.station_code=a.station_code where t.station_code='" + Station_Code + "'  and a.station_type='ST15'";
                        
                        DataTable dt = dataConn.GetTable(sql);
                        if (dt.Rows.Count > 0) //存在前道质量点
                        {
                            //string stacode1 = dt.Rows[0][0].ToString();
                            frmQa ps = new frmQa("A", sn, Station_Code);
                            ps.StartPosition = FormStartPosition.Manual;
                            ps.WindowState = FormWindowState.Maximized;
                            ps.Show(this);
                        }
                    }
                }
                if (e.MessageHead == "CHECKOK")//流水号检查完毕 确认无误
                {
                    sn = e.MessageBody.ToString();
                    PlanSnEntity ent = PlanSnFactory.GetBySnPline(sn, PlineCode1);//获取sn信息
                    if (ent != null)
                    {
                        product = ProductInfoFactory.GetByCompanyCodeSNSingle(CompanyCode, sn);//获取sn信息
                        TheSo = product.PLAN_SO;
                        TheSn = product.SN;
                        ThePlancode = product.PLAN_CODE;

                        //判断是否是质量站点 是 则显示前道站点质量信息
                        //string sql = "select station_code from vw_code_station where station_type='ST15' and pline_code='" + PlineCode1 + "' and station_seq <(select nvl(station_seq,0) from vw_code_station where station_code='" + Station_Code + "') order by station_seq desc ";
                        string sql = "select * from REL_QA_STATION_PRESTATION t left join code_station a on t.station_code=a.station_code where t.station_code='" + Station_Code + "'  and a.station_type='ST15'";
                        
                        DataTable dt = dataConn.GetTable(sql);
                        if (dt.Rows.Count > 0) //存在前道质量点
                        {
                            //string stacode1 = dt.Rows[0][0].ToString();
                            frmQa ps = new frmQa("A", sn, Station_Code);
                            ps.StartPosition = FormStartPosition.Manual;
                            ps.WindowState = FormWindowState.Maximized;
                            //ps.Show(this);
                            ps.ShowDialog();
                        }

                        if (ent.SN_FLAG == "P" || ent.SN_FLAG == "N")//未上线 初始为N 打印为P  上线未Y
                        {
                            if (LoginInfo.StationInfo.STATION_TYPE == "ST01")//当前站点是上线站点
                            {
                                arg.MessageHead = "ONLINE";
                                arg.MessageBody = sn;
                                SendDataChangeMessage(arg);
                                arg.MessageHead = "ONLINEONLINE";
                                arg.MessageBody = sn;
                                SendDataChangeMessage(arg);
                            } 
                            else
                            {
                                MessageBox.Show("此发动机流水号未上线");
                                return;
                            }
                        }
                        else //已上线
                        {
                            string str1 = dataConn.GetValue("select count(1) from data_complete where sn='"+sn+"' and plan_code='"+ent.PLAN_CODE+"' and station_code='"+Station_Code+"'");
                            if (str1 != "0") //根据完成表判断重复扫描
                            {
                                arg.MessageHead = "RESN";
                                arg.MessageBody = sn;
                                SendDataChangeMessage(arg);
                                return;
                            }
                            arg.MessageHead = "SN";
                            arg.MessageBody = sn;
                            SendDataChangeMessage(arg);
                        }
                    }
                }
                if (e.MessageHead == "SN")  //处理流水号站点信息
                {
                    sn = e.MessageBody.ToString();
                    product = ProductInfoFactory.GetByCompanyCodeSNSingle(CompanyCode, sn);//获取sn信息
                    if (product == null) return;
                    Cl_plan = "";
                    Cl_so = "";
                    Cl_pmodel = "";
                    SkipFlag = false;
                    //txtSN.Text = e.MessageBody.ToString();
                    TheSo = product.PLAN_SO;
                    TheSn = product.SN;
                    ThePlancode = product.PLAN_CODE;
                    //初始化站点控件信息  判断该流水号该站点是否全部操作完成
                    //PlanSnFactory.InitStationControl(CompanyCode, PlineID, StationID, product.PLAN_CODE, product.SN);
                    
                    string plan_code = product.PLAN_CODE;
                    //处理完成表 储备表 分装站点及触发站点
                    ProductDataFactory.Station_Start(plan_code, sn, quality_status);
                    arg.MessageHead = "REFRESHPLAN";
                    arg.MessageBody = sn;
                    SendDataChangeMessage(arg);
                    //保存记录到本地
                    string gcSaveData = TheSn + ";" + TheSo + ";" + DateTime.Now.ToString("yyyyMMddHHmmss") +";" 
                                   + LoginInfo.WorkDate + ";" + StationName + ";" + product.PRODUCT_MODEL + ";" 
                                   + LoginInfo.UserInfo.USER_CODE + ";" + ShiftCode + ";" + TeamCode + ";" + "0" + ";" 
                                   + PlineCode1 + ";" + ThePlancode;
                    PublicClass.Output_DayData(StationName,TheSn, "sjwcb", gcSaveData);
                    //调用与ATPU-SPC系统的条码接口
                    try
                    {
                        //调用第三方dll导致程序异常退出
                        //MessageBox.Show("SPC", "提示");
                        //sendcode(StationName, StationName.Length, TheSn.Trim(), TheSn.Length);
                        sendcode(TheSn.Trim(), TheSn.Length);
                    }
                    catch (Exception e1) { MessageBox.Show(e1.Message,"提示"); }
                    if (CsGlobalClass.NEEDVEPS)
                    {
                        PublicClass.Handle_Veps(TheSo,TheSn);
                    }
                    if (LoginInfo.StationInfo.STATION_TYPE == "ST03")//当前站点是下线站点
                    {
                        arg.MessageHead = "OFFLINE";
                        arg.MessageBody = sn;
                        SendDataChangeMessage(arg);
                    }
                    return;
                }
                if (e.MessageHead == "RESN")  //处理流水号站点信息
                {
                    sn = e.MessageBody.ToString();
                    product = ProductInfoFactory.GetByCompanyCodeSNSingle(CompanyCode, sn);//获取sn信息
                    if (product == null) return;
                    Cl_plan = "";
                    Cl_so = "";
                    Cl_pmodel = "";
                    SkipFlag = false;
                    //txtSN.Text = e.MessageBody.ToString();
                    TheSo = product.PLAN_SO;
                    TheSn = product.SN;
                    ThePlancode = product.PLAN_CODE;
                    //初始化站点控件信息  判断该流水号该站点是否全部操作完成
                    //PlanSnFactory.InitStationControl(CompanyCode, PlineID, StationID, product.PLAN_CODE, product.SN);

                    string plan_code = product.PLAN_CODE;
                    //处理完成表 储备表 分装站点及触发站点
                    ProductDataFactory.Station_Start(plan_code, sn, quality_status);
                    arg.MessageHead = "REFRESHPLAN";
                    arg.MessageBody = sn;
                    SendDataChangeMessage(arg);
                    //保存记录到本地
                    string gcSaveData = TheSn + ";" + TheSo + ";" + DateTime.Now.ToString("yyyyMMddHHmmss") + ";"
                                   + LoginInfo.WorkDate + ";" + StationName + ";" + product.PRODUCT_MODEL + ";"
                                   + LoginInfo.UserInfo.USER_CODE + ";" + ShiftCode + ";" + TeamCode + ";" + "0" + ";"
                                   + PlineCode1 + ";" + ThePlancode;
                    PublicClass.Output_DayData(StationName, TheSn, "sjwcb", gcSaveData);
                    //调用与ATPU-SPC系统的条码接口
                    try
                    {
                        //调用第三方dll导致程序异常退出
                        //MessageBox.Show("SPC", "提示");
                        //sendcode(StationName, StationName.Length, TheSn.Trim(), TheSn.Length);
                        sendcode(TheSn.Trim(), TheSn.Length);
                    }
                    catch (Exception e1) { MessageBox.Show(e1.Message, "提示"); }
                    //sendcode(TheSn.Trim(), TheSn.Length);

                    if (CsGlobalClass.NEEDVEPS)
                    {
                        PublicClass.Handle_Veps(TheSo, TheSn);
                    }
                    return;
                }
                //if (e.MessageHead == "PLAN")
                //{
                //    string plan_code = e.MessageBody.ToString();

                //    PlanEntity ent_plan = PlanFactory.GetByKey(plan_code);
                //    string plan_batch = ent_plan.PLAN_BATCH;
                //    txtSN.Text = e.MessageBody.ToString();
                //    PlanSnFactory.InitStationControl(CompanyCode, PlineID, StationID, plan_code, plan_batch);

                //    ProductDataFactory.Station_Start(plan_code, plan_batch, quality_status);
                //}
                if (e.MessageHead == "CHECK") 
                {
                    sn = e.MessageBody.ToString();
                    product = ProductInfoFactory.GetByCompanyCodeSNSingle(CompanyCode, sn);//获取sn信息
                    if (product == null) return;
                }
                if (e.MessageHead == "CCP")
                {//处理检测数据完成
                    string[] msgbody = e.MessageBody.ToString().Split('^');
                    sn = msgbody[0];
                    product = ProductInfoFactory.GetByCompanyCodeSNSingle(CompanyCode, sn);//获取sn信息

                    ProductControlFactory.HandleControlComplete(CompanyCode, PlineID, StationID, product.PLAN_CODE,
                                                 product.SN, msgbody[1], msgbody[2]);
                    if (msgbody[2] == "B")
                    {
                        if ((StationName == "Z100" || StationName == "Z110" || StationName == "Z120") && msgbody[1].Contains("ctrlShowBom"))
                        { }
                        else
                        {
                            GetFocused(txtSN);
                        }
                    }
                    //if (msgbody[1].Contains("ctrlShowBom") && msgbody[2] == "B")
                    //{
                    //    RMESEventArgs args = new RMESEventArgs();
                    //    args.MessageHead = "GETSUB";
                    //    args.MessageBody = sn;
                    //    UiFactory.CallDataChanged(this, args);
                    //}
                }

       }

        private bool TheProcessValid(string PlanCode)
        {
            List<PlanProcessEntity> lstProc = PlanProcessFactory.GetByPlan(PlanCode);
            //string theWorkUnit = StationFactory.GetByKey(StationCode).WORKUNIT_CODE;
            //string prevWorkUnit, prevStation;
            //for (int i = 0; i < lstProc.Count; i++)
            //{
            //    if (lstProc[i].WORKUNIT_CODE == theWorkUnit)
            //    {
            //        if (i == 0) return true;
            //        prevWorkUnit = lstProc[i - 1].WORKUNIT_CODE;
            //        prevStation = StationFactory.GetByWorkUnit(prevWorkUnit).STATION_CODE;
            //        List<ProductCompleteEntity> ent1 = ProductCompleteFactory.GetByPlanStation(PlanCode, prevStation);
            //        if (ent1.Count == 0) return false; else return true;
            //    }
            //}
            return true;
        }//校验计划工序完成情况


        private void txtSN_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (LoginInfo.StationInfo.STATION_TYPE == "ST04" && e.KeyChar == 13 && !txtSN.Text.Contains("$"))
            {
                //线边分装站点 所有扫描都完成后再生成新的条码
                RMESEventArgs arg1 = new RMESEventArgs();

                string sqlfz = "select count(1) from data_sn_controls_complete where sn='" + TheSn + "' and plan_code='" + ThePlancode + "' and station_code='" + Station_Code + "' and complete_flag='A'";
                if (dataConn.GetValue(sqlfz).ToString() == "0")//都扫描完成
                {
                    string sql = "";
                    if (TheSn == "")
                    {
                        //流水号为空则代表刚上线 先从control_complete里取 没有再从分装表取
                        try
                        {
                            string sqlfz1 = "select distinct sn from data_sn_controls_complete t where  pline_code='" + PlineID + "' and station_code='" + Station_Code + "' and complete_flag = 'A'  ";
                            DataTable dtfz1 = dataConn.GetTable(sqlfz1);
                            if (dtfz1.Rows.Count > 0)
                            {
                                sql = "select rmes_id,sn,plan_so,plan_code,pline_code,station_code,sub_zc,check_flag from DATA_SUBPACK where sn='" + dtfz1.Rows[0][0].ToString() + "' and station_code='" + Station_Code + "' order by start_time ";
                            }
                            else
                            {
                                sql = "select rmes_id,sn,plan_so,plan_code,pline_code,station_code,sub_zc,check_flag from DATA_SUBPACK where WORK_FLAG='N' and station_code='" + Station_Code + "' and sn is not null order by start_time ";
                            }
                        }
                        catch
                        {
                            sql = "select rmes_id,sn,plan_so,plan_code,pline_code,station_code,sub_zc,check_flag from DATA_SUBPACK where WORK_FLAG='N' and station_code='" + Station_Code + "' and sn is not null order by start_time ";
                        }
                    }
                    else
                    {
                        sql = "select rmes_id,sn,plan_so,plan_code,pline_code,station_code,sub_zc,check_flag from DATA_SUBPACK where WORK_FLAG='N' and station_code='" + Station_Code + "' and sn!='" + TheSn + "' order by start_time ";
                    }
                    //string sql = "select rmes_id,sn,plan_so,plan_code,pline_code,station_code,sub_zc,check_flag from DATA_SUBPACK where WORK_FLAG='N' and station_code='" + Station_Code + "' and sn!='" + TheSn + "' order by start_time ";
                    DataTable dt = dataConn.GetTable(sql);
                    if (dt.Rows.Count > 0)
                    {
                        string planmodel = "";
                        try
                        {
                            planmodel = dataConn.GetValue("select product_model from data_plan where plan_code='" + dt.Rows[0]["PLAN_CODE"].ToString() + "' and rownum=1");
                        }
                        catch { }
                        txtSN.Text = dt.Rows[0]["SN"].ToString();
                        arg1.MessageHead = "FILLSN";
                        arg1.MessageBody = dt.Rows[0]["SN"].ToString() + "^" + dt.Rows[0]["PLAN_SO"].ToString() + "^" + planmodel + "^" + dt.Rows[0]["PLAN_CODE"].ToString();
                        SendDataChangeMessage(arg1);
                    }
                    else
                    {
                        txtSN.Text = "";
                        arg1.MessageHead = "FILLSN";
                        arg1.MessageBody = "" + "^" + "" + "^" + "" + "^" + "";
                        SendDataChangeMessage(arg1);
                    }
                }
            }
            //大线上线站点 重新获取未上线流水号 做上线
            if (LoginInfo.StationInfo.STATION_TYPE == "ST01" && e.KeyChar == 13 && !txtSN.Text.Contains("$")  && LoginInfo.ProductLineInfo.PLINE_TYPE_CODE == "A" && LoginInfo.ProductLineInfo.PLINE_CODE != "L") //大线上线站点 重新获取未上线流水号 做上线
            {
                //string sql = "SELECT sn,plan_seq,product_model,plan_code,plan_so,plan_qty,online_qty,customer_name,begin_date,end_date,create_time,remark "
                //         + " FROM vw_data_plan_sn  Where run_flag='Y' and confirm_flag='Y' and plan_type!='C' and plan_type!='D' and pline_code='" + PlineCode1 + "' AND  plan_qty > online_qty AND to_char(end_date,'yyyymmdd')<=to_char(sysdate+30,'yyyymmdd')  AND to_char(begin_date,'yyyymmdd')>=to_char(sysdate-30,'yyyymmdd') and snflag='N'   and sn is not null order by begin_date,plan_seq,plan_code,sn ";
                string sqlfz = "select count(1) from data_sn_controls_complete where sn='" + TheSn + "' and plan_code='" + ThePlancode + "' and station_code='" + Station_Code + "' and complete_flag='A'";
                if (dataConn.GetValue(sqlfz).ToString() == "0")//都扫描完成
                {
                    RMESEventArgs arg1 = new RMESEventArgs();
                    string sql = "SELECT sn,plan_seq,product_model,plan_code,plan_so,plan_qty,online_qty,customer_name,begin_date,end_date,create_time,remark "
                             + " FROM vw_data_plan_sn  Where run_flag='Y' and confirm_flag='Y' and plan_type!='C' and plan_type!='D' and pline_code='" + PlineCode1 + "' AND  plan_qty > online_qty  and snflag='N'   and sn is not null order by begin_date,plan_seq,plan_code,sn ";
                    string adstatus = "A";
                    try
                    {
                        adstatus = dataConn.GetValue(" select increase_flag from code_sn where pline_code='" + PlineCode1 + "' ");
                    }
                    catch { }
                    if (adstatus != "A")
                        sql = sql + " desc ";
                    DataTable dt = dataConn.GetTable(sql);
                    if (dt.Rows.Count != 0)
                    {
                        string ThePlanso = "", ThePmodel = "", TheCustomer = "";
                        try
                        {
                            ThePlancode = dt.Rows[0]["plan_code"].ToString();
                            ThePlanso = dt.Rows[0]["plan_so"].ToString();
                            TheSn = dt.Rows[0]["sn"].ToString();
                            ThePmodel = dt.Rows[0]["product_model"].ToString();
                            TheCustomer = dt.Rows[0]["customer_name"].ToString();
                        }
                        catch
                        {
                            ThePlancode = "";
                            ThePlanso = "";
                            TheSn = "";
                            ThePmodel = "";
                            TheCustomer = "";
                        }
                        if (TheSn != "")
                        {
                            txtSN.Text = TheSn;
                            arg1.MessageHead = "FILLSN";
                            arg1.MessageBody = TheSn + "^" + ThePlanso + "^" + ThePmodel + "^" + ThePlancode;
                            SendDataChangeMessage(arg1);
                        }
                        else
                        {
                            txtSN.Text = "";
                            arg1.MessageHead = "FILLSN";
                            arg1.MessageBody = "" + "^" + "" + "^" + "" + "^" + "";
                            SendDataChangeMessage(arg1);
                        }
                        GetFocused(txtSN);
                    }
                }
            }

            if (e.KeyChar != 13) return;
            if (string.IsNullOrWhiteSpace(txtSN.Text)) return;
            CsGlobalClass.NEEDVEPS = false;
            GetFocused(txtSN);

            string txt = txtSN.Text.ToUpper();
            txtSN.Text = txt;
            RMESEventArgs arg = new RMESEventArgs();
            arg.MessageHead = "";
            arg.MessageBody = txt;

            if (txt.StartsWith("$")) //单独指令
            {
                switch (txt)
                {
                    case "$QUIT": //关闭计算机
                        //三漏数据处理 每次处理上一个流水号  退出时处理当前
                        if (PlineCode1 == "E" && (StationName == "Z690-1" || StationName == "Z690-2" || StationName == "Z690-3" || StationName == "Z690-4"))
                        {
                            PublicClass.InsertSLData(CsGlobalClass.NEWSN);
                        }
                        if (PlineCode1 == "W" && (StationName == "ATPU-A470" || StationName == "ATPU-A480"))
                        {
                            PublicClass.InsertSLData(CsGlobalClass.NEWSN);
                        }
                        if (CsGlobalClass.NEWSN != "")
                            dataConn.ExeSql("update data_complete set complete_time=sysdate where sn='" + CsGlobalClass.NEWSN + "' and plan_code='" + CsGlobalClass.NEWPLANCODE + "' and station_code='" + LoginInfo.StationInfo.STATION_CODE + "' and complete_time is null ");
                        
                        Process.Start("shutdown.exe", "-s -t 10");
                        break;
                    case "$DGSM"://返修站点顶岗扫描
                        CsGlobalClass.DGSM = true;
                        break;
                    case "$EXIT"://退出程序
                        //三漏数据处理 每次处理上一个流水号  退出时处理当前
                        if (PlineCode1 == "E" && (StationName == "Z690-1" || StationName == "Z690-2" || StationName == "Z690-3" || StationName == "Z690-4"))
                        {
                            PublicClass.InsertSLData(CsGlobalClass.NEWSN);
                        }
                        if (PlineCode1 == "W" && (StationName == "ATPU-A470" || StationName == "ATPU-A480"))
                        {
                            PublicClass.InsertSLData(CsGlobalClass.NEWSN);
                        }
                        if (CsGlobalClass.NEWSN != "")
                            dataConn.ExeSql("update data_complete set complete_time=sysdate where sn='" + CsGlobalClass.NEWSN + "' and plan_code='" + CsGlobalClass.NEWPLANCODE + "' and station_code='" + LoginInfo.StationInfo.STATION_CODE + "' and complete_time is null ");
                        
                          BaseForm tempForm1 = (BaseForm)this.ParentForm;
                          PublicClass.ClearEvents(tempForm1);
                          Application.Exit();
                        break;
                    case "$CANC"://重新登录
                        //三漏数据处理 每次处理上一个流水号  退出时处理当前
                        if (PlineCode1 == "E" && (StationName == "Z690-1" || StationName == "Z690-2" || StationName == "Z690-3" || StationName == "Z690-4"))
                        {
                            PublicClass.InsertSLData(CsGlobalClass.NEWSN);
                        }
                        if (PlineCode1 == "W" && (StationName == "ATPU-A470" || StationName == "ATPU-A480"))
                        {
                            PublicClass.InsertSLData(CsGlobalClass.NEWSN);
                        }
                        if (CsGlobalClass.NEWSN != "")
                            dataConn.ExeSql("update data_complete set complete_time=sysdate where sn='" + CsGlobalClass.NEWSN + "' and plan_code='" + CsGlobalClass.NEWPLANCODE + "' and station_code='" + LoginInfo.StationInfo.STATION_CODE + "' and complete_time is null ");
                        
                          BaseForm tempForm = (BaseForm)this.ParentForm;
                          ClearEvents(tempForm);
                          Application.Restart();
                        break;
                    case "$LJQD"://显示零件清单
                        Form form22 = new Form();
                        form22.Text = "现场待处理物料数据录入";
                        form22.WindowState = FormWindowState.Maximized;
                        ctrlShowBom print21 = new ctrlShowBom(TheSn);
                        print21.Width = 1000;
                        print21.Height = 500;
                        print21.Top = 6;
                        print21.Left = 6;
                        form22.Controls.Add(print21);
                        form22.Show(this);
                        break;
                    case "$CZTS"://显示装机提示
                        FrmShowProcessNote ps1 = new FrmShowProcessNote(ThePlancode,"");
                        ps1.StartPosition = FormStartPosition.Manual;
                        ps1.StartPosition = FormStartPosition.CenterScreen;
                        ps1.Show(this);
                        break;
                    case "$JHCX"://显示前后30天计划信息，双击查对应bom
                        FrmShowPlan ps = new FrmShowPlan();
                        ps.StartPosition = FormStartPosition.Manual;
                        ps.StartPosition = FormStartPosition.CenterScreen;
                        ps.Show(this);
                        break;
                    case "$LJDCL"://录入线边待处理零件信息frmMs003
                        Form form12 = new Form();
                        form12.Text = "现场待处理物料数据录入";
                        form12.WindowState = FormWindowState.Maximized;
                        ctrl_material_handle print1 = new ctrl_material_handle();
                        print1.Width = 1000;
                        print1.Height = 500;
                        print1.Top = 6;
                        print1.Left = 6;
                        form12.Controls.Add(print1);
                        form12.Show(this);
                        break;
                    case "$LJGYS"://录入零件供应商信息frmMs001
                        //线边暂不处理
                        break;
                    case "$LJQL"://录入缺料信息frmZlbj
                        //线边暂不处理
                        break;
                    case "$LSYL"://录入现场临时要料信息frmMs002
                        //线边暂不处理
                        break;
                    case "$QXJL"://显示返修质量记录frmQxInput
                        //sjzlb 未用
                        break;
                    case "$SBGZ"://录入设备故障frmZlbj
                        //未用
                        break;
                    case "$SKIP"://物料核减控制frmMs004
                        string password = Microsoft.VisualBasic.Interaction.InputBox("请输入密码", "输入密码");
                        string sql = "select yhmm from ms_skip_password where gzdd='" + PlineCode1 + "' and yhmm='" + password + "'";
                        DataTable dt = dataConn.GetTable(sql);
                        if (dt.Rows.Count == 0)
                        {
                            MessageBox.Show("密码错误", "提示");
                        }
                        else
                        {
                            SkipFlag = true;
                            dataConn.ExeSql("insert into ms_skip_log(gzdd,zdmc,ygmc,bcmc,bzmc,add_time) values('" + PlineCode1 + "','" + StationName + "','" + LoginInfo.UserInfo.USER_NAME + "','" + ShiftCode + "','" + TeamCode + "',sysdate)");
                        }
                        break;
                    case "$ZDTPTS"://查看站点图片？

                        break;
                    case "$ZLBG"://录入质量缺陷frmZlbj
                        //同上
                        break;
                    case "$ZPZYZDS"://查看工艺文件frmGywjSel
                        Form form13 = new Form();
                        form13.Text = "工艺文件查看";
                        form13.WindowState = FormWindowState.Maximized;
                        ctrlProcessFile print13 = new ctrlProcessFile(TheSn);
                        print13.Width = 1000;
                        print13.Height = 500;
                        print13.Top = 6;
                        print13.Left = 6;
                        form13.Controls.Add(print13);
                        form13.Show(this);
                        break;
                    case "$ZPJL"://显示、录入装配记录frmZpjlInput
                        Form form1 = new Form();
                        form1.Text = "检测数据录入";
                        form1.WindowState = FormWindowState.Maximized;
                        Label label1 = new Label();
                        label1.Text = "重要零件数据录入";
                        label1.Location = new Point(200,10);
                        Label label2 = new Label();
                        label2.AutoSize = true;
                        label2.Text = "流水号：" + TheSn;
                        label2.Font = new System.Drawing.Font("微软雅黑", 14);
                        label2.Location = new Point(10,20);
                        Label label3 = new Label();
                        label3.AutoSize = true;
                        label3.Text = "SO号：" + TheSo;
                        label3.Font = new System.Drawing.Font("微软雅黑", 14);
                        label3.Location = new Point(200, 20);
                        ctrlQualityDetect print = new ctrlQualityDetect(TheSn);
                        print.Width = 1000;
                        print.Height = 500;
                        print.Top = 50;
                        print.Left = 6;
                        //form1.Controls.Add(label1);
                        form1.Controls.Add(label2);
                        form1.Controls.Add(label3);
                        form1.Controls.Add(print);
                        form1.Show(this);
                        break;
                    case "$BHGP"://处理不合格品

                        break;
                    case "$CXGL"://VEPS重新灌录
                        CsGlobalClass.NEEDVEPS = true;
                        //arg.MessageHead = "VEPS";
                        //arg.MessageBody = "";
                        //SendDataChangeMessage(arg);
                        break;
                }
                if (txt.StartsWith("$GW"))//查看录入SO frmBomSo的BOM清单
                {

                }
                if (txt.StartsWith("$YG"))//查看录入SO frmBomSo的BOM清单
                {

                }
                arg.MessageHead = "";
                arg.MessageBody = txt;
                return;
            }
            else
            {
                ////方法① 通过正则表达式判断流水号和零件条码
                    //MessageHeadEntity mse = MessageHeadFactory.GetByString(txt);
                    //if (mse != null)
                    //{
                    //    arg.MessageHead = mse.HEAD_CODE;
                    //    arg.MessageBody = txtSN.Text;
                    //}
                ////方法② 通过程序判断流水号和零件条码
                int sn_length = -1;
                string msg1 = "";
                if (IsNumeric(len_sn))
                {
                    sn_length = Convert.ToInt32(len_sn);
                }
                //CL分装先得到计划，再绑定流水号 需要把录入的三段条码进行分解
                if (LoginInfo.ProductLineInfo.PLINE_TYPE_CODE == "B" && LoginInfo.ProductLineInfo.PLINE_CODE.Contains("CL"))
                {
                    if (LoginInfo.StationInfo.STATION_TYPE == "ST01") //上线站点
                    {
                        if (Cl_plan == "")
                        {
                            MessageBox.Show("当前没有计划，请先选择计划", "提示");
                            return;
                        }
                        txt = GetLsh(txt); //三段码取最后一段流水号作为sn
                        //txtSN.Text = txt;
                        if (txt == "")
                        {
                            MessageBox.Show("非法流水号", "提示");
                            GetFocused(txtSN);
                            return;
                        }
                        string sql = "select count(1) from data_product where sn='" + txt + "'";
                        if (dataConn.GetValue(sql) != "0")
                        {
                            MessageBox.Show("流水号已使用", "提示");
                            GetFocused(txtSN);
                            return;
                        }
                        string sqlisde = "select count(1) from data_plan_sn where sn='" + txt + "' and plan_code='" + Cl_plan + "' and is_valid='Y' ";
                        if (dataConn.GetValue(sqlisde) == "0")
                        {
                            sqlisde = " insert into data_plan_sn(rmes_id,company_code,pline_code,plan_code,sn,sn_flag,create_time,is_valid) values(seq_rmes_id.nextval,'" + CompanyCode + "','" + PlineCode1 + "','" + Cl_plan + "','" + txt + "','N',sysdate,'Y'  )  ";
                            dataConn.ExeSql(sqlisde);
                        }
                    }
                    else
                    {
                        txt = GetLsh(txt); //三段码取最后一段流水号作为sn
                        if (txt == "")
                        {
                            MessageBox.Show("非法流水号", "提示");
                            GetFocused(txtSN);
                            return;
                        }
                    }
                    arg.MessageHead = "CHECK";
                    arg.MessageBody = txt;
                    SendDataChangeMessage(arg);
                    return;
                }
                //分装判断输入的流水号 由于采用的是缸体二维码中的零件流水号 所以位数不定 含有字母 单独处理
                if (LoginInfo.ProductLineInfo.PLINE_TYPE_CODE == "B" && LoginInfo.ProductLineInfo.PLINE_CODE.Contains("ISDE"))
                {
                    if (LoginInfo.StationInfo.STATION_TYPE == "ST01")
                    {
                        txt = GetLsh(txt); //三段码取最后一段流水号作为sn
                        if (txt == "")
                        {
                            MessageBox.Show("非法流水号", "提示");
                            GetFocused(txtSN);
                            return;
                        }
                        string sqlisde = "select count(1) from data_plan_sn where sn='" + txt + "' and is_valid='Y'  ";
                        if (dataConn.GetValue(sqlisde) == "0")
                        {
                            string count2 = dataConn.GetValue("select plan_qty from data_plan where plan_code='" + ThePlancode + "'");
                            string count3 = dataConn.GetValue("select count(1) from data_plan_sn where plan_code='" + ThePlancode + "'");
                            int str3 = 0, str2 = 0;
                            try
                            {
                                str2 = Convert.ToInt32(count2);
                            }
                            catch { }
                            str3 = Convert.ToInt32(count3);
                            if (str2 <= str3)
                            {
                                txtSN.Text = "";
                                return;
                            }
                            sqlisde = " insert into data_plan_sn(rmes_id,company_code,pline_code,plan_code,sn,sn_flag,create_time,is_valid) values(seq_rmes_id.nextval,'" + CompanyCode + "','" + PlineCode1 + "','" + ThePlancode + "','" + txt + "','N',sysdate,'Y'  )  ";
                            dataConn.ExeSql(sqlisde);
                        }
                    }
                    else
                    {
                        txt = GetLsh(txt); //三段码取最后一段流水号作为sn
                        if (txt == "")
                        {
                            MessageBox.Show("非法流水号", "提示");
                            GetFocused(txtSN);
                            return;
                        }
                    }

                    arg.MessageHead = "CHECK";
                    arg.MessageBody = txt;
                    SendDataChangeMessage(arg);
                    return;
                }
                ////大线上线站点 重新获取未上线流水号 做上线
                //if (LoginInfo.StationInfo.STATION_TYPE == "ST01" && LoginInfo.ProductLineInfo.PLINE_TYPE_CODE == "A" && LoginInfo.ProductLineInfo.PLINE_CODE!="L") //大线上线站点 重新获取未上线流水号 做上线
                //{
                //    //string sql = "SELECT sn,plan_seq,product_model,plan_code,plan_so,plan_qty,online_qty,customer_name,begin_date,end_date,create_time,remark "
                //    //         + " FROM vw_data_plan_sn  Where run_flag='Y' and confirm_flag='Y' and plan_type!='C' and plan_type!='D' and pline_code='" + PlineCode1 + "' AND  plan_qty > online_qty AND to_char(end_date,'yyyymmdd')<=to_char(sysdate+30,'yyyymmdd')  AND to_char(begin_date,'yyyymmdd')>=to_char(sysdate-30,'yyyymmdd') and snflag='N'   and sn is not null order by begin_date,plan_seq,plan_code,sn ";
                    
                //    string sql = "SELECT sn,plan_seq,product_model,plan_code,plan_so,plan_qty,online_qty,customer_name,begin_date,end_date,create_time,remark "
                //             + " FROM vw_data_plan_sn  Where run_flag='Y' and confirm_flag='Y' and plan_type!='C' and plan_type!='D' and pline_code='" + PlineCode1 + "' AND  plan_qty > online_qty  and snflag='N'   and sn is not null order by begin_date,plan_seq,plan_code,sn ";
                //    DataTable dt = dataConn.GetTable(sql);
                //    if (dt.Rows.Count != 0)
                //    {
                //        string ThePlanso = "", ThePmodel = "", TheCustomer = "";
                //        try
                //        {
                //            ThePlancode = dt.Rows[0]["plan_code"].ToString();
                //            ThePlanso = dt.Rows[0]["plan_so"].ToString();
                //            TheSn = dt.Rows[0]["sn"].ToString();
                //            ThePmodel = dt.Rows[0]["product_model"].ToString();
                //            TheCustomer = dt.Rows[0]["customer_name"].ToString();
                //        }
                //        catch
                //        {
                //            ThePlancode = "";
                //            ThePlanso = "";
                //            TheSn = "";
                //            ThePmodel = "";
                //            TheCustomer = "";
                //        }
                //        if (TheSn != "")
                //        {
                //            arg.MessageHead = "FILLSN";
                //            arg.MessageBody = TheSn + "^" + ThePlanso + "^" + ThePmodel + "^" + ThePlancode;
                //            SendDataChangeMessage(arg);
                //        }
                //        else
                //        {
                //        }
                //        txtSN.Text = TheSn;
                //        txt = txtSN.Text.ToUpper();
                //        GetFocused(txtSN);
                //    }
                //}

                //if (LoginInfo.StationInfo.STATION_TYPE == "ST04")
                //{
                //    //线边分装站点 所有扫描都完成后再生成新的条码
                //    string sqlfz = "select count(1) from data_sn_controls_complete where sn='" + TheSn + "' and plan_code='" + ThePlancode + "' and station_code='" + Station_Code + "' and complete_flag='A'";
                //    if (dataConn.GetValue(sqlfz).ToString() == "0")//都扫描完成
                //    {
                //        string sql = "select rmes_id,sn,plan_so,plan_code,pline_code,station_code,sub_zc,check_flag from DATA_SUBPACK where WORK_FLAG='N' and station_code='" + Station_Code + "' and sn!='" + TheSn + "' order by start_time ";
                //        DataTable dt = dataConn.GetTable(sql);
                //        if (dt.Rows.Count > 0)
                //        {
                //            string planmodel = "";
                //            try
                //            {
                //                planmodel = dataConn.GetValue("select product_model from data_plan where plan_code='" + dt.Rows[0]["PLAN_CODE"].ToString() + "' and rownum=1");
                //            }
                //            catch { }
                //            arg.MessageHead = "FILLSN";
                //            arg.MessageBody = dt.Rows[0]["SN"].ToString() + "^" + dt.Rows[0]["PLAN_SO"].ToString() + "^" + planmodel + "^" + dt.Rows[0]["PLAN_CODE"].ToString();
                //            SendDataChangeMessage(arg);
                //        }
                //        else
                //        {
                //            arg.MessageHead = "FILLSN";
                //            arg.MessageBody = "" + "^" + "" + "^" + "" + "^" + "";
                //            SendDataChangeMessage(arg);
                //        }
                //    }
                //}
                if (IsNumeric(txt) && txt.Length == sn_length) //判断是否SN 
                {
                    //区分返修站点 
                    if (LoginInfo.StationInfo.STATION_TYPE == "ST05")
                    {
                        //返修站点分为两种 一种线上返修的流水号 对应的计划是大线计划，需要登录大线站点操作，另一种是线下返修计划的流水号 
                        PlanSnEntity ent = PlanSnFactory.GetBySnPline(txt, PlineCode1);//获取sn信息
                        if (ent != null)
                        {
                            if (CheckSN_FX(txt))
                            {
                                arg.MessageHead = "CHECKFX";
                                arg.MessageBody = txt;
                                SendDataChangeMessage(arg);
                            }
                        }
                        else
                        {
                            MessageBox.Show("该流水号" + txt + "不存在", "警告", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            txtSN.Text = "";
                        }
                    }
                    else
                    {
                        //判断是否SN  分装生产线线上绑定流水号
                        PlanSnEntity ent = PlanSnFactory.GetBySnPline(txt, PlineCode1);//获取sn信息
                        if (ent != null)
                        {
                            if (CheckSN(txt))
                            {
                                arg.MessageHead = "CHECK";
                                arg.MessageBody = txt;
                                SendDataChangeMessage(arg);
                            }
                            else
                            {
                                //MessageBox.Show("该流水号" + txt + "不合法", "警告", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                txtSN.Text = "";
                            }
                        }
                        else
                        {
                            MessageBox.Show("该流水号" + txt + "不存在", "警告", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            txtSN.Text = "";
                        }
                    }
                }
                else
                {
                    //判断是否是零件
                    if (CheckItem(txt, TheSn, TheSo, PlineCode1, ThePlancode, out msg1))
                    {
                        GetFocused(txtSN);
                        arg.MessageHead = "ITEM";
                        arg.MessageBody = msg1 + "^" + txt;
                        SendDataChangeMessage(arg);
                        //GetFocused(txtSN);
                    }
                    else
                    {
                        //MessageBox.Show("零件条码不合法", "警告", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        arg.MessageHead = "FAULTITEM";
                        arg.MessageBody = msg1;
                        SendDataChangeMessage(arg);
                        GetFocused(txtSN);
                        return;
                    }
                }
            } 
        }
        /// <summary>
        /// 验证当前SN是否合法，计划状态、上一流水号完成状态
        /// </summary>
        /// <param name="arg"></param>
        private bool CheckSN(string SnInfo)
        {
            string sn = SnInfo;
            string cur_sn = "";

            List<ProductInfoEntity> newProduct = new List<ProductInfoEntity>();
            try
            {
                //获取sn基础信息 data_plan_sn 和data_plan
                newProduct = ProductInfoFactory.GetByCompanyCodeSNPline(LoginInfo.CompanyInfo.COMPANY_CODE, sn, LoginInfo.ProductLineInfo.PLINE_CODE);
                if (newProduct.Count == 0)
                {
                    MessageBox.Show("【" + sn + "】不存在！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                //改制返修站点不允许扫描大线流水号
                if (LoginInfo.StationInfo.STATION_TYPE == "ST05")
                {
                    if (newProduct.First<ProductInfoEntity>().PLAN_TYPE != "C" && newProduct.First<ProductInfoEntity>().PLAN_TYPE != "D")
                    {
                        MessageBox.Show("当前计划非执返修计划,不允许在返修点扫描", "警告", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
                string run_flag = newProduct.First<ProductInfoEntity>().RUN_FLAG;
                //if (run_flag != "Y")  //上线实时获取流水号，中间站点对于已经上线的发动机允许继续生产
                //{
                //    MessageBox.Show("当前计划处于非执行状态", "警告", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //    return false;
                //}
                //发动机下线判断 由于现场下线后还允许扫描 所以将下线操作放到入库点进行，此处改为入库后不允许扫描，modify by thl 20161213
                string sql1 = "select count(1) from dp_rckwcb  where ghtm='" + newProduct.First<ProductInfoEntity>().SN + "' and jhdm='" + newProduct.First<ProductInfoEntity>().PLAN_CODE + "' and rc='入库' ";//and station_code in (select station_code from code_station where station_type='ST03' )
                string isoffline = dataConn.GetValue(sql1);
                if (isoffline != "0" && LoginInfo.StationInfo.STATION_TYPE != "ST11")
                {
                    MessageBox.Show("该发动机已下线！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                //终检站点判断返修发动机是否已全部确认
                if (LoginInfo.StationInfo.STATION_TYPE == "ST03") //下线站点
                {
                    if (newProduct.First<ProductInfoEntity>().PLAN_TYPE == "C" || newProduct.First<ProductInfoEntity>().PLAN_TYPE == "D")
                    {
                        if (dataConn.GetValue("select count(1) from data_sn_detect_query t where sn='" + newProduct.First<ProductInfoEntity>().SN + "' and plan_code='" + newProduct.First<ProductInfoEntity>().PLAN_CODE + "' and (detect_flag='N' or scan_flag='N')  ") != "0")
                        {
                            MessageBox.Show("该返修发动机尚未确认，请联系管理人员确认！", "提示");
                            return false;
                        }
                        //判断改制发动机是否管理工作站确认通过管理工作站确认表判断20161123
                        string sql = "select count(1) from data_sn_bom_temp where sn='" + newProduct.First<ProductInfoEntity>().SN + "' and plan_code='" + newProduct.First<ProductInfoEntity>().PLAN_CODE + "' and confirm_flag!='Y' and item_type!='C'  ";
                        if (dataConn.GetValue(sql) != "0")
                        {
                            MessageBox.Show("该返修发动机装配重要零件尚未全部扫描完成，请联系管理人员确认！", "提示");
                            return false;
                        }
                        sql = "select count(1) from data_sn_detect_data_temp where sn='" + newProduct.First<ProductInfoEntity>().SN + "' and plan_code='" + newProduct.First<ProductInfoEntity>().PLAN_CODE + "' and detect_flag!='Y' ";
                        if (dataConn.GetValue(sql) != "0")
                        {
                            MessageBox.Show("该返修发动机检测数据尚未全部录入完成，请联系管理人员确认！", "提示");
                            return false;
                        }

                        sql = "select count(1) from data_sn_qa where sn='" + newProduct.First<ProductInfoEntity>().SN + "' and plan_code='" + newProduct.First<ProductInfoEntity>().PLAN_CODE + "' and qa_flag!='Y'";
                        if (dataConn.GetValue(sql) != "0")
                        {
                            MessageBox.Show("该返修发动机终检未全部确认", "提示");
                            return false;
                        }
                    }
                    else
                    {
                        string sql = "select count(1) from data_sn_bom_temp where sn='" + newProduct.First<ProductInfoEntity>().SN + "' and plan_code='" + newProduct.First<ProductInfoEntity>().PLAN_CODE + "' and confirm_flag!='Y' and item_type!='C'  ";
                        if (dataConn.GetValue(sql) != "0")
                        {
                            String sta1 = dataConn.GetValue("select FUNC_GET_UNSCANSTATION('" + newProduct.First<ProductInfoEntity>().SN + "','" + newProduct.First<ProductInfoEntity>().PLAN_CODE + "','BOM') from dual");
                            MessageBox.Show("该发动机装配" + sta1 + "重要零件尚未全部扫描完成，请重新扫描！", "提示");
                            return false;
                        }
                        sql = "select count(1) from data_sn_detect_data_temp where sn='" + newProduct.First<ProductInfoEntity>().SN + "' and plan_code='" + newProduct.First<ProductInfoEntity>().PLAN_CODE + "' and detect_flag!='Y' ";
                        if (dataConn.GetValue(sql) != "0")
                        {
                            String sta1 = dataConn.GetValue("select FUNC_GET_UNSCANSTATION('" + newProduct.First<ProductInfoEntity>().SN + "','" + newProduct.First<ProductInfoEntity>().PLAN_CODE + "','DETECT') from dual");
                            MessageBox.Show("该发动机检测数据尚未全部录入完成，请重新扫描！", "提示");
                            return false;
                        }

                        sql = "select count(1) from data_sn_qa where sn='" + newProduct.First<ProductInfoEntity>().SN + "' and plan_code='" + newProduct.First<ProductInfoEntity>().PLAN_CODE + "' and qa_flag!='Y'";
                        if (dataConn.GetValue(sql) != "0")
                        {
                            String sta1 = dataConn.GetValue("select FUNC_GET_UNSCANSTATION('" + newProduct.First<ProductInfoEntity>().SN + "','" + newProduct.First<ProductInfoEntity>().PLAN_CODE + "','QA') from dual");
                            MessageBox.Show("该发动机终检未全部扫描完成，请重新扫描！", "提示");
                            return false;
                        }
                    }
                }

                //List<ProductSnEntity> sttList = ProductSnFactory.GetCurrentSN(LoginInfo.CompanyInfo.COMPANY_CODE,
                //                                                            LoginInfo.ProductLineInfo.RMES_ID,
                //                                                            LoginInfo.StationInfo.RMES_ID);
                //if (sttList.Count > 1)
                //{
                //    MessageBox.Show("当前站点有多个未完成任务", "警告", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //    return false;
                //}
                //if (sttList.Count == 1)
                //{
                //    cur_sn = sttList.First<ProductSnEntity>().SN;
                //    if (cur_sn != sn)
                //    {
                //        MessageBox.Show("前一流水号发动机未完成！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //        return false;
                //    }
                //}

                if (!SkipFlag && !CsGlobalClass.DGSM)
                {
                    string sql = "select count(1) from data_sn_controls_complete where station_code='" + LoginInfo.StationInfo.RMES_ID + "' and sn<>'" + sn + "' and pline_code='" + LoginInfo.ProductLineInfo.RMES_ID + "' and complete_flag='A'";
                    if (dataConn.GetValue(sql) != "0")
                    {
                        string str1 = "select control_script,sn from data_sn_controls_complete where station_code='" + LoginInfo.StationInfo.RMES_ID + "' and sn<>'" + sn + "' and pline_code='" + LoginInfo.ProductLineInfo.RMES_ID + "' and complete_flag='A'";
                        DataTable dt112 = dataConn.GetTable(str1);
                        string str2 = "";
                        for (int i = 0; i < dt112.Rows.Count; i++)
                        {
                            str2 += dt112.Rows[i][1].ToString() + "-" + dt112.Rows[i][0].ToString() + ";";
                        }
                        if (str2.EndsWith(";"))
                            str2 = str2.Substring(0, str2.Length - 1);
                        MessageBox.Show("前一流水号发动机"+str2+"未完成！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
                if (SkipFlag)//skip跳过 则将前一流水号data_sn_controls_complete置为完成
                {
                    try
                    {
                        dataConn.ExeSql("update data_sn_controls_complete  set  complete_flag='B' where  station_code='" + LoginInfo.StationInfo.RMES_ID + "' and pline_code='" + LoginInfo.ProductLineInfo.RMES_ID + "'   and sn<>'" + sn + "'     and complete_flag='A' ");
                    }
                    catch
                    { }
                }
                PlanSnEntity ent = PlanSnFactory.GetBySnPline(sn, LoginInfo.ProductLineInfo.PLINE_CODE);//获取sn信息
                if (ent != null)
                {
                    if (ent.SN_FLAG == "P" || ent.SN_FLAG == "N")//未上线 初始为N 打印为P  上线未Y
                    {
                        if (LoginInfo.StationInfo.STATION_TYPE != "ST01")//当前站点是上线站点
                        {
                            MessageBox.Show("此发动机流水号未上线");
                            return false;
                        }
                    }

                    //string plan_code = ent.PLAN_CODE;
                    //string quality_status = "A", complete_flag = "0";

                    //if (ent.SN_FLAG == "Y")//如果是已经上线，则判断质量状态
                    //{
                    //    //获取完成表data_complete信息
                    //    List<ProductCompleteEntity> prc = ProductCompleteFactory.GetByPlanSn(CompanyCode, PlineCode1, plan_code, sn, Station_Code);
                    //    if (prc.Count > 0) complete_flag = prc.First<ProductCompleteEntity>().COMPLETE_FLAG;
                    //    //if (complete_flag == "Y")
                    //    //{
                    //    //    MessageBox.Show("此发动机在该站点已经完成下线", "警告", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //    //    return false;
                    //    //}
                    //}
                }
                else
                {
                    MessageBox.Show("【" + sn + "】不存在！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "警告", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
        /// <summary>
        /// 验证当前SN是否合法，计划状态、上一流水号完成状态
        /// </summary>
        /// <param name="arg"></param>
        private bool CheckSN_FX(string SnInfo)
        {
            string sn = SnInfo;
            string cur_sn = "";

            List<ProductInfoEntity> newProduct = new List<ProductInfoEntity>();
            try
            {
                //获取sn基础信息 data_plan_sn 和data_plan
                newProduct = ProductInfoFactory.GetByCompanyCodeSNPline(LoginInfo.CompanyInfo.COMPANY_CODE, sn, LoginInfo.ProductLineInfo.PLINE_CODE);
                //newProduct = ProductInfoFactory.GetByCompanyCodeSNPline1(LoginInfo.CompanyInfo.COMPANY_CODE, sn, LoginInfo.ProductLineInfo.PLINE_CODE);
                
                if (newProduct.Count == 0)
                {
                    MessageBox.Show("【" + sn + "】不存在！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                //改制返修站点不允许扫描大线流水号
                if (LoginInfo.StationInfo.STATION_TYPE == "ST05")
                {
                    if (newProduct.First<ProductInfoEntity>().PLAN_TYPE != "C" && newProduct.First<ProductInfoEntity>().PLAN_TYPE != "D")
                    {
                        MessageBox.Show("当前计划非执返修计划,不允许在返修点扫描", "警告", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
                string run_flag = newProduct.First<ProductInfoEntity>().RUN_FLAG;
                if (run_flag != "Y")
                {
                    MessageBox.Show("当前计划处于非执行状态", "警告", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                //List<ProductSnEntity> sttList = ProductSnFactory.GetCurrentSN(LoginInfo.CompanyInfo.COMPANY_CODE,
                //                                                            LoginInfo.ProductLineInfo.RMES_ID,
                //                                                            LoginInfo.StationInfo.RMES_ID);
                //if (sttList.Count > 1)
                //{
                //    MessageBox.Show("当前站点有多个未完成任务", "警告", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //    return false;
                //}
                //if (sttList.Count == 1)
                //{
                //    cur_sn = sttList.First<ProductSnEntity>().SN;
                //    if (cur_sn != sn)
                //    {
                //        MessageBox.Show("前一流水号发动机未完成！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //        return false;
                //    }
                //}
                //string sql = "select count(1) from data_sn_controls_complete where station_code='" + LoginInfo.StationInfo.RMES_ID + "' and sn<>'" + sn + "' and pline_code='" + LoginInfo.ProductLineInfo.RMES_ID + "' and complete_flag='A'";
                //if (dataConn.GetValue(sql) != "0")
                //{
                //    MessageBox.Show("前一流水号发动机未完成！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //    return false;
                //}
                PlanSnEntity ent = PlanSnFactory.GetBySnPline(sn, LoginInfo.ProductLineInfo.PLINE_CODE);//获取sn信息
                if (ent != null)
                {
                    if (ent.SN_FLAG == "P" || ent.SN_FLAG == "N")//未上线 初始为N 打印为P  上线未Y
                    {
                        //if (LoginInfo.StationInfo.STATION_TYPE != "ST01")//当前站点是上线站点
                        //{
                        //    MessageBox.Show("此发动机流水号未上线");
                        //    return false;
                        //}
                    }

                    string plan_code = ent.PLAN_CODE;
                    string quality_status = "A", complete_flag = "0";

                    if (ent.SN_FLAG == "Y")//如果是已经上线，则判断质量状态
                    {
                        //获取完成表data_complete信息
                        List<ProductCompleteEntity> prc = ProductCompleteFactory.GetByPlanSn(CompanyCode, PlineCode1, plan_code, sn, Station_Code);
                        if (prc.Count > 0) complete_flag = prc.First<ProductCompleteEntity>().COMPLETE_FLAG;
                        if (complete_flag == "Y")
                        {
                            //MessageBox.Show("此发动机在该站点已经完成下线", "警告", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            //return false;
                        }
                    }
                }
                else
                {
                    MessageBox.Show("【" + sn + "】不存在！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "警告", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
        /// <summary>
        /// 验证当前零件号是否合法
        /// </summary>
        /// <param name="arg"></param>
        private bool CheckItem(string ItemInfo1,string sn,string so,string plinecode,string plancode, out string MsgBody)
        {
            string ItemInfo = ItemInfo1.Replace("|","^").ToUpper();
            string[] cmd_info = ItemInfo.Split('^');
            string itemcode = "";//零件代码
            string suppliercode = "";//供应商代码
            string batchcode = "";//批次号
            string barcode = "";
            bool isItem = false;
            if (cmd_info.Length > 1)
            {//一维码
                if (cmd_info.Length == 2)
                {
                    //itemcode = cmd_info[0];
                    //suppliercode = cmd_info[1];
                    //batchcode = "";
                    //barcode = ItemInfo;
                    //isItem = true;
                    MsgBody = "";
                    isItem = false;
                    return false;
                }
                else if (cmd_info.Length == 3)
                {
                    itemcode = cmd_info[0];
                    suppliercode = cmd_info[1].Trim();
                    batchcode = cmd_info[2].Trim();
                    if (batchcode == "" || suppliercode == "")
                    {
                        MsgBody = "";
                        isItem = false;
                        return false;
                    }
                    barcode = ItemInfo;// itemcode + "^" + suppliercode + "^" + batchcode;
                    isItem = true;
                }
                else
                {
                    //MessageBox.Show("零件条码不合法", "警告", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    MsgBody = "";
                    isItem = false;
                    //MessageBox.Show("零件条码错误！", "警告");
                    return false;
                }
            }
            else
            {
                //二维码

                if (ItemInfo.IndexOf("D") >= 0 && ItemInfo.IndexOf("S") > 0 && ItemInfo.IndexOf("1P") > 0 && ItemInfo.IndexOf("11Z") > 0)
                {
                    itemcode = "C" + ItemInfo.ToUpper().Substring(ItemInfo.IndexOf("1P") + 2, 7);
                    suppliercode = ItemInfo.Substring(ItemInfo.IndexOf("11Z") + 3, ItemInfo.Replace("12Z", "14Z").IndexOf("14Z") - ItemInfo.IndexOf("11Z") - 3);
                    batchcode = ItemInfo.Substring(ItemInfo.IndexOf("S") + 1, ItemInfo.IndexOf("1P") - ItemInfo.IndexOf("S") - 1);
                    isItem = true;
                }
                else if (ItemInfo.IndexOf("P")==0 && ItemInfo.IndexOf("V")>0 && ItemInfo.IndexOf("S")>0)
                {
                    itemcode = ItemInfo.Substring(ItemInfo.IndexOf("P") + 1, ItemInfo.IndexOf("V") - ItemInfo.IndexOf("P")-1);
                    if (itemcode.Contains('C'))
                    {
                    }
                    else
                    {
                        itemcode = "C" + itemcode;
                    }
                    suppliercode = ItemInfo.Substring(ItemInfo.IndexOf("V") + 1, 7);
                    batchcode = ItemInfo.Substring(ItemInfo.IndexOf("S") + 1);
                    if (batchcode == "" || suppliercode == "")
                    {
                        MsgBody = "";
                        isItem = false;
                        return false;
                    }
                    barcode = ItemInfo;
                    isItem = true;
                }
                else
                {
                    //if (StationName == "Z490")
                    //{
                    //    string sql = "select item_code  from data_plan_standard_bom where plan_code='" + plancode + "' and location_code='Z490'  and item_name ='喷油器' and rownum=1 ";
                    //    string ljdm = dataConn.GetValue(sql);
                    //    if (ItemInfo.Length == 49 && ljdm.Contains(ItemInfo.Substring(5, 5)))
                    //    {
                    //        string trimGs = "";
                    //        trimGs = dataConn.GetValue("select gs from copy_engine_property WHERE so='" + so + "'");
                    //        if (trimGs == "")
                    //        {
                    //            MessageBox.Show("缸数为空，请反馈给BOM维护员", "提示");
                    //            MsgBody = "";
                    //            isItem = false;
                    //            return false;
                    //        }
                    //        try
                    //        {
                    //            //PublicClass.Output_Trim_File(sn, trimGs, "1", ItemInfo.Substring(20).ToUpper());
                    //        }
                    //        catch { }
                    //        itemcode = ljdm;
                    //        suppliercode = "DENSO";
                    //        batchcode = ItemInfo.Substring(10,9);
                    //        isItem = true;
                    //    }
                    //    else
                    //    {
                    //        MsgBody = "";
                    //        isItem = false;
                    //        //MessageBox.Show("零件条码错误！", "警告");
                    //        return false;
                    //    }

                    //}
                    //else
                    {
                        MsgBody = "";
                        isItem = false;
                        //MessageBox.Show("零件条码错误！", "警告");
                        return false;
                    }
                }
            }
            if (suppliercode.Length < 2)
            {
                MsgBody = "";
                isItem = false;
                //MessageBox.Show("零件条码错误！", "警告");
                return false;
            }
            if (suppliercode == "SUB") //分装总成零件
            {
                MsgBody = itemcode + "^" + suppliercode + "^" + batchcode;
                return true;
            }
            int count1 = Convert.ToInt32(dataConn.GetValue("select count(1) from copy_pt_mstr where upper(pt_part)='" + itemcode + "'  ")); //and (upper(pt_status)='P' or upper(pt_status)='S')
            if (count1 == 0)
            {
                isItem = false;
                //MessageBox.Show("零件条码不存在！", "警告");
                MsgBody = "";
                return false;
            }
            if (!isItem)
            {
                MsgBody = "";
                //MessageBox.Show("零件条码错误！", "警告");
                return false;
            }
            if (so == "" || sn == "" || plancode == "")
            {
                MsgBody = "";
                //MessageBox.Show("请扫描发动机流水号！", "警告");
                return false;
            }
            //判断零件是否是BOM中的零件

            //if (dataConn.GetValue("select count(1) from rstbomqd where zddm='" + LoginInfo.StationInfo.STATION_CODE + "' and (comp='" + itemcode + "' or comp='#" + itemcode + "' )    ") == "0")
            try
            {
                if (dataConn.GetValue("select count(1) from data_sn_bom_temp where station_code='" + LoginInfo.StationInfo.STATION_CODE + "' and sn='" + sn + "' and plan_code='" + plancode + "' and (item_code='" + itemcode + "' or item_code='#" + itemcode + "' )    ") == "0")
                {
                    MsgBody = "";
                    //MessageBox.Show("请扫描发动机流水号！", "警告");
                    return false;
                }
            }
            catch
            {
                if (dataConn.GetValue("select count(1) from rstbomqd where zddm='" + LoginInfo.StationInfo.STATION_CODE + "' and (comp='" + itemcode + "' or comp='#" + itemcode + "' )    ") == "0")
                {
                    MsgBody = "";
                    //MessageBox.Show("请扫描发动机流水号！", "警告");
                    return false;
                }
            }
            string gys = dataConn.GetValue("select PL_IS_GYS('" + so + "','" + itemcode + "','" + plinecode + "','" + suppliercode + "','"+plancode+"') from dual");
            string smcf = dataConn.GetValue("select is_LJSMCF('" + sn + "','" + itemcode + "','" + suppliercode + "','" + batchcode + "','" + plinecode + "','" + barcode + "','"+LoginInfo.StationInfo.STATION_NAME+"') from dual ");
            if (gys != "OK")
            {
                MsgBody = "";
                //MessageBox.Show("零件条码与供应商不匹配！","警告");
                //RMESEventArgs arg = new RMESEventArgs();
                //arg.MessageHead = "FAILITEM";
                //arg.MessageBody = "";
                //SendDataChangeMessage(arg);
                return false;
            }
            if (smcf == "N")
            {
                MsgBody = "";
                //MessageBox.Show("扫描的零件信息重复！", "警告");
                //RMESEventArgs arg = new RMESEventArgs();
                //arg.MessageHead = "FAILITEM";
                //arg.MessageBody = "";
                //SendDataChangeMessage(arg);
                return false;
            }
            MsgBody = itemcode + "^" + suppliercode + "^" + batchcode;
            return true;
        }

        /// <summary>
        /// 获得光标焦点
        /// </summary>
        /// <param name="textBox"></param>
        private void GetFocused(TextBox textBox)
        {
            textBox.Focus();
            textBox.Select(0, textBox.Text.Length);
        }

        private bool IsNumeric(string sValue)
        {
            try
            {
                int i = int.Parse(sValue);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private void txtSN_Validated(object sender, EventArgs e)
        {

        }

        private void txtSN_Click(object sender, EventArgs e)
        {
            GetFocused(txtSN);
        }

        private void txtSN_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F1:
                    //三漏数据处理 每次处理上一个流水号  退出时处理当前
                    if (PlineCode1 == "E" && (StationName == "Z690-1" || StationName == "Z690-2" || StationName == "Z690-3" || StationName == "Z690-4"))
                    {
                        PublicClass.InsertSLData(CsGlobalClass.NEWSN);
                    }
                    if (PlineCode1 == "W" && (StationName == "ATPU-A470" || StationName == "ATPU-A480"))
                    {
                        PublicClass.InsertSLData(CsGlobalClass.NEWSN);
                    }
                    if (CsGlobalClass.NEWSN != "")
                        dataConn.ExeSql("update data_complete set complete_time=sysdate where sn='" + CsGlobalClass.NEWSN + "' and plan_code='" + CsGlobalClass.NEWPLANCODE + "' and station_code='" + LoginInfo.StationInfo.STATION_CODE + "' and complete_time is null ");

                    BaseForm tempForm = (BaseForm)this.ParentForm;
                    ClearEvents(tempForm);
                    Application.Restart();
                    break;
                case Keys.F11:
                    //显示改制BOM对比清单 及 改制BOM扫描结果 及检测数据扫描结果
                    if (TheSn == "")
                    {
                        MessageBox.Show("请扫描发动机流水号", "提示");
                        return;
                    }
                    FrmShowGZqd psgz = new FrmShowGZqd(TheSn, ThePlancode);
                    psgz.StartPosition = FormStartPosition.Manual;
                    psgz.StartPosition = FormStartPosition.CenterScreen;
                    psgz.Show(this);
                    break;
                case Keys.F2:
                    //分装CL报废处理
                    if (LoginInfo.ProductLineInfo.PLINE_CODE != "CL")
                    {
                        break;
                    }
                    DialogResult drt = MessageBox.Show("执行该功能将对缸盖进行报废处理，要继续吗？", "提示", MessageBoxButtons.YesNo);
                    if (drt == DialogResult.No) return;

                    string ghtm = Microsoft.VisualBasic.Interaction.InputBox("请输入缸盖流水号", "输入");
                    if (ghtm.Trim() == "")
                    {
                        return;
                    }
                    string outstr = "";
                    ProductDataFactory.WGG_MODIFY_JHB(ghtm, LoginInfo.ProductLineInfo.PLINE_CODE, out outstr);
                    if (outstr != "GOOD")
                    {
                        MessageBox.Show(outstr, "提示");
                    }
                    //刷新计划
                    RMESEventArgs arg = new RMESEventArgs();
                    arg.MessageHead = "REFRESHCLPLAN";
                    arg.MessageBody = "";
                    SendDataChangeMessage(arg);
                    break;
                case Keys.F3:
                    FrmShowCompleteStatistics ps31 = new FrmShowCompleteStatistics();
                    ps31.StartPosition = FormStartPosition.Manual;
                    ps31.StartPosition = FormStartPosition.CenterScreen;
                    ps31.Show(this);
                        //DataTable dt = new DataTable();
                        //Form frm_complete = new Form();
                        //frm_complete.Text = "完工情况查看";
                        //frm_complete.WindowState = FormWindowState.Maximized;
                        //DataGridView gvc = new DataGridView();
                        //gvc.Columns.Add("colplan_code", "计划号");
                        //gvc.Columns.Add("colsn", "SN");
                        //gvc.Columns.Add("colso", "SO");
                        //gvc.Columns.Add("colpmodel", "机型");
                        //gvc.Columns.Add("colteam", "班组");
                        //gvc.Columns.Add("colstime", "开始时间");
                        //gvc.Columns.Add("coletime", "结束时间");
                        //gvc.Columns.Add("colempty", "");
                        ////gvc.Columns[0].DataPropertyName = "PLAN_CODE";
                        ////gvc.Columns[1].DataPropertyName = "SN";
                        ////gvc.Columns[2].DataPropertyName = "PLAN_SO";
                        ////gvc.Columns[3].DataPropertyName = "PRODUCT_MODEL";
                        ////gvc.Columns[4].DataPropertyName = "TEAM_CODE";
                        ////gvc.Columns[5].DataPropertyName = "START_TIME";
                        ////gvc.Columns[6].DataPropertyName = "COMPLETE_TIME";
                        //gvc.Columns[7].Width = Screen.PrimaryScreen.Bounds.Width - 613;
                        //gvc.Columns[0].Width = 90;
                        //gvc.Columns[1].Width = 90;
                        //gvc.Columns[2].Width = 90;
                        //gvc.Columns[4].Width = 90;
                        //gvc.Columns[5].Width = 120;
                        //gvc.Columns[6].Width = 120;
                        //gvc.Columns[4].Visible = false;
                        //string sql = string.Format("select * FROM data_complete t where t.SHIFT_CODE='{0}' "
                        //                            + "and t.team_code='{1}' and t.station_code='{2}' and "
                        //                            + "T.START_TIME>TO_DATE(to_char(SYSDATE,'yyyy/mm/dd')||'00:00:00','yyyy/mm/dd hh24:mi:ss')",
                        //                            LoginInfo.ShiftInfo.SHIFT_CODE,
                        //                            LoginInfo.TeamInfo.TEAM_CODE, LoginInfo.StationInfo.RMES_ID);
                        //dt = dataConn.GetTable(sql);
                        //gvc.Height = 500;
                        //gvc.Width = 1000;
                        //gvc.RowHeadersVisible = false;
                        //gvc.Dock = System.Windows.Forms.DockStyle.Fill;
                        //gvc.AllowUserToAddRows = false;
                        //gvc.AutoGenerateColumns = false;
                        //gvc.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
                        //DataRow dr = dt.NewRow();
                        //dr[1] = "合计："+dt.Rows.Count.ToString();
                        //dt.Rows.Add(dr);
                        //for (int i = 0; i < dt.Rows.Count; i++)
                        //{
                        //    gvc.Rows.Add();
                        //    Color color = Color.White;
                        //    if (i % 2 == 0) //奇数行显示浅蓝色
                        //    {
                        //        color = Color.FromArgb(208, 247, 252);//Color.FromArgb(144,220,238)  Color.FromArgb(144, 220, 238)
                        //    }
                        //    for (int j = 0; j < gvc.Columns.Count; j++)
                        //    {
                        //        gvc.Rows[i].Cells[j].Style.BackColor = color;

                        //    }
                        //    gvc.Rows[i].Cells[0].Value = dt.Rows[i]["PLAN_CODE"];
                        //    gvc.Rows[i].Cells[1].Value = dt.Rows[i]["SN"];
                        //    gvc.Rows[i].Cells[2].Value = dt.Rows[i]["PLAN_SO"];
                        //    gvc.Rows[i].Cells[3].Value = dt.Rows[i]["PRODUCT_MODEL"];
                        //    gvc.Rows[i].Cells[4].Value = dt.Rows[i]["TEAM_CODE"];
                        //    gvc.Rows[i].Cells[5].Value = dt.Rows[i]["START_TIME"];
                        //    gvc.Rows[i].Cells[6].Value = dt.Rows[i]["COMPLETE_TIME"];
                        //}
                        //frm_complete.Width = 1000;
                        //frm_complete.Height = 500;
                        //frm_complete.Top = 6;
                        //frm_complete.Left = 6;
                        //frm_complete.Controls.Add(gvc);
                        //frm_complete.Show(this);
                        //gvc.ClearSelection();
                    break;
                case Keys.F7:
                    if (LoginInfo.ProductLineInfo.PLINE_CODE == "L")
                    {
                        FrmShowProcessPic ps = new FrmShowProcessPic("", oldPlinecode);
                        ps.StartPosition = FormStartPosition.Manual;
                        ps.Location = new Point(0, 400);
                        ps.Show();
                    }
                    else
                    {
                        //显示装机图片
                        FrmShowProcessPic ps = new FrmShowProcessPic("A");
                        ps.StartPosition = FormStartPosition.Manual;
                        ps.StartPosition = FormStartPosition.CenterScreen;
                        ps.Show(this);
                    }
                    ////显示装机图片
                    //FrmShowProcessPic ps = new FrmShowProcessPic("A");
                    //ps.StartPosition = FormStartPosition.Manual;
                    //ps.StartPosition = FormStartPosition.CenterScreen;
                    //ps.Show(this);
                    break;
                case Keys.F8:
                    if (TheSn == "")
                    {
                        MessageBox.Show("请输入发动机流水号", "提示");
                        break;
                    }
                    //查看工艺文件
                    frmProcessFile ps1 = new frmProcessFile(TheSn);
                    ps1.StartPosition = FormStartPosition.Manual;
                    ps1.StartPosition = FormStartPosition.CenterScreen;
                    ps1.Show(this);
                    break;
                case Keys.F9:
                    //打印条码
                    if (LoginInfo.ProductLineInfo.PLINE_TYPE_CODE == "B") //分装
                    {
                        if (TheSn == "")
                        {
                            MessageBox.Show("请输入发动机流水号", "提示");
                            break;
                        }
                        frmPrintTm ps2 = new frmPrintTm(TheSmTm, ThePlancode);
                        ps2.StartPosition = FormStartPosition.Manual;
                        ps2.StartPosition = FormStartPosition.CenterScreen;
                        ps2.Show(this);
                    }
                    else
                    {
                        frmPrintTm ps3 = new frmPrintTm();
                        ps3.StartPosition = FormStartPosition.Manual;
                        ps3.StartPosition = FormStartPosition.CenterScreen;
                        ps3.Show(this);
                    }
                    break;
                case Keys.F10:
                    //质量检验 质检站点
                    if (TheSn == "" || Station_Code == "")
                    {
                        MessageBox.Show("请输入发动机流水号", "提示");
                        return;
                    }
                    frmQa ps4 = new frmQa("B", TheSn, Station_Code);
                    ps4.StartPosition = FormStartPosition.Manual;
                    ps4.WindowState = FormWindowState.Maximized;
                    //ps.Show(this);
                    ps4.ShowDialog();

                    break;
                case Keys.F4:
                    txtSN.Text = "";
                    //打印分装附装站点装配清单
                    //MessageBox.Show("打印分装清单");
                    if (TheSn == "" || TheSo == "")
                    {
                        MessageBox.Show("请扫描流水号");
                        return;
                    }
                    string sql1 = "select gwmc,comp,udesc,qty,gxmc,gysmc from rstbomqd where zdDM='" + StationName + "' order by gwmc,gxmc";
                    PublicClass.PrintRstTable("分装装机清单",sql1,1,TheSn,TheSo);
                    
                    break;
                case Keys.F5:
                    txtSN.Text = "";
                    //零件录入
                    //MessageBox.Show("检测数据录入");
                    //Form form1 = new Form();
                    //form1.Text = "检测数据录入";
                    //form1.WindowState = FormWindowState.Maximized;
                    //Label label1 = new Label();
                    //label1.Text = "重要零件数据录入";
                    //label1.Location = new Point(200,10);
                    //Label label2 = new Label();
                    //label2.AutoSize = true;
                    //label2.Text = "流水号：" + TheSn;
                    //label2.Font = new System.Drawing.Font("微软雅黑", 14);
                    //label2.Location = new Point(10,20);
                    //Label label3 = new Label();
                    //label3.AutoSize = true;
                    //label3.Text = "SO号：" + TheSo;
                    //label3.Font = new System.Drawing.Font("微软雅黑", 14);
                    //label3.Location = new Point(200, 20);
                    //ctrlQualityDetect print = new ctrlQualityDetect(TheSn);
                    //print.Width = 1000;
                    //print.Height = 500;
                    //print.Top = 50;
                    //print.Left = 6;
                    ////form1.Controls.Add(label1);
                    //form1.Controls.Add(label2);
                    //form1.Controls.Add(label3);
                    //form1.Controls.Add(print);
                    //form1.Show(this);
                    break;
                case Keys.Multiply:
                    txtSN.Text = "";
                    //*号键零件录入
                    //MessageBox.Show("检测数据录入");
                    Form form12 = new Form();
                    form12.Text = "检测数据录入";
                    form12.WindowState = FormWindowState.Maximized;
                    Label label11 = new Label();
                    label11.Text = "重要零件数据录入";
                    label11.Location = new Point(200,10);
                    Label label21 = new Label();
                    label21.AutoSize = true;
                    label21.Text = "流水号：" + TheSn;
                    label21.Font = new System.Drawing.Font("微软雅黑", 14);
                    label21.Location = new Point(10,20);
                    Label label31 = new Label();
                    label31.AutoSize = true;
                    label31.Text = "SO号：" + TheSo;
                    label31.Font = new System.Drawing.Font("微软雅黑", 14);
                    label31.Location = new Point(200, 20);
                    ctrlQualityDetect print1 = new ctrlQualityDetect(TheSn);
                    print1.Width = 1000;
                    print1.Height = 500;
                    print1.Top = 50;
                    print1.Left = 6;
                    //form1.Controls.Add(label1);
                    form12.Controls.Add(label21);
                    form12.Controls.Add(label31);
                    form12.Controls.Add(print1);
                    form12.Show(this);
                    break;
                case Keys.F6:
                    txtSN.Text = "";
                    //显示前后三十天计划信息
                    //MessageBox.Show("计划信息显示");
                    FrmShowPlan ps6 = new FrmShowPlan();
                    ps6.StartPosition = FormStartPosition.Manual;
                    ps6.StartPosition = FormStartPosition.CenterScreen;
                    ps6.Show(this);
                    break;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            GetFocused(txtSN);
            timer1.Enabled = false;
        }
        /// <summary>
        /// 清除一个对象的某个事件所挂钩的delegate
        /// </summary>
        /// <param name="ctrl">控件对象</param>
        /// <param name="eventName">事件名称，默认的</param>
        public void ClearEvents(object ctrl, string eventName = "_EventAll")
        {
            if (ctrl == null) return;
            BindingFlags bindingFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.IgnoreCase | BindingFlags.Static;
            EventInfo[] events = ctrl.GetType().GetEvents(bindingFlags);
            if (events == null || events.Length < 1) return;

            for (int i = 0; i < events.Length; i++)
            {
                try
                {
                    EventInfo ei = events[i];
                    //只删除指定的方法，默认是_EventAll，前面加_是为了和系统的区分，防以后雷同
                    if (eventName != "_EventAll" && ei.Name != eventName) continue;

                    /********************************************************
                     * class的每个event都对应了一个同名(变了，前面加了Event前缀)的private的delegate类
                     * 型成员变量（这点可以用Reflector证实）。因为private成
                     * 员变量无法在基类中进行修改，所以为了能够拿到base 
                     * class中声明的事件，要从EventInfo的DeclaringType来获取
                     * event对应的成员变量的FieldInfo并进行修改
                     ********************************************************/
                    FieldInfo[] fis = ei.DeclaringType.GetFields(bindingFlags);
                    FieldInfo fi = ei.DeclaringType.GetField(("EVENT_" + ei.Name).ToUpper(), bindingFlags);
                    if (fi != null)
                    {
                        // 将event对应的字段设置成null即可清除所有挂钩在该event上的delegate
                        fi.SetValue(ctrl, null);
                    }
                }
                catch { }
            }
        }
        private string GetLsh(string gtm)
        {
            try
            {
                int i = gtm.IndexOf('^');
                if (i <= 1)
                {
                    return "";
                }
                string[] strAry = gtm.Split('^');
                string strLjdm = "", strGys = "", strlsh = "";
                if (strAry.Length == 2)
                {
                    strLjdm = strAry[0];
                    strGys = strAry[1];
                }
                else if (strAry.Length == 3)
                {
                    strLjdm = strAry[0];
                    strGys = strAry[1];
                    strlsh = strAry[2];
                }

                return strlsh;
            }
            catch
            {
                return "";
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (StationName == "ATPU-U800")
            {
                timer2.Enabled = false;
                string sqlstr = "select  nvl(lsh,'') from atpuxqyqdyb  order by ptime";//where zdmc='ATPU-T560' 
                DataTable dtsql = dataConn.GetTable(sqlstr);
                if (dtsql.Rows.Count > 0)
                {
                    string lsh1 = dtsql.Rows[0][0].ToString();
                    if (lsh1 != "")
                    {
                        ProductInfoEntity product1 = ProductInfoFactory.GetByCompanyCodeSNSingle(CompanyCode, lsh1);
                        DataTable dtsql2 = dataConn.GetTable("select station_name,plan_so from vw_data_complete where sn='" + lsh1 + "' and plan_code='" + product1.PLAN_CODE + "' and pline_code='" + product1.PLINE_CODE + "' and station_code in (select station_code from vw_code_station where station_type='ST01' and pline_code='" + product1.PLINE_CODE + "')");
                        string yg = "";
                        try
                        {
                            yg=dtsql2.Rows[0][0].ToString();
                        }
                        catch { }
                        string thisso1 = "";
                        try
                        {
                            thisso1=dtsql2.Rows[0][1].ToString();
                        }
                        catch { }
                        string thisYh = "",thisJx="",thisYhdm="";
                        try
                        {
                            thisYh = dataConn.GetValue("select customer_name from data_plan where plan_code ='" + product1.PLAN_CODE + "' and rownum=1");
                        }
                        catch { }
                        try
                        {
                            thisJx = dataConn.GetValue("select product_model from data_plan where plan_code ='" + product1.PLAN_CODE + "' and rownum=1");
                        }
                        catch { }
                        try
                        {
                            thisYhdm = dataConn.GetValue("select nvl(KHH,'') from atpuplannameplate where plan_code='" + product1.PLAN_CODE + "'");
                        }
                        catch { }
                        //PublicClass.PrintTmFz1(lsh1,thisso1,thisYh,thisYhdm,thisJx,2);
                        PublicClass.PrintWTmFz(lsh1, thisso1, thisYh, thisYhdm, thisJx, 2, product1.PLAN_CODE);
                        PublicClass.PrintTmFzGb(lsh1,thisso1,thisYh,thisYhdm,thisJx,1,product1.REMARK,product1.PLAN_CODE,product1.PLINE_CODE);
                        

                        dataConn.ExeSql("delete from atpuxqyqdyb where lsh='" + lsh1 + "'");
                    }
                    try
                    {
                        //20161216增加环保要求的条码打印 判断FR、SC 获取相应信息
                        string fr1 = dataConn.GetValue("select get_planfr('" + lsh1 + "','FR') from dual");
                        string sc1 = dataConn.GetValue("select get_planfr('" + lsh1 + "','SC') from dual");
                        DataTable dthb = dataConn.GetTable("select xzmc,d20,d21,d22,d24,d25,d26,d27,d28 from  where COPY_PDM_HBGKXX where fr='" + fr1 + "' and sc='" + sc1 + "'");
                        if (dthb.Rows.Count > 0)
                        {
                            ProductInfoEntity product12 = ProductInfoFactory.GetByCompanyCodeSNSingle(CompanyCode, lsh1);
                            string so1 = product12.PLAN_SO;
                            string xzmc = "", d20 = "", d21 = "", d22 = "", d24 = "", d25 = "", d26 = "", d27 = "", d28 = "";
                            try
                            {
                                xzmc = dthb.Rows[0][0].ToString() + (char)13 + (char)10;
                            }
                            catch { xzmc = "" + (char)13 + (char)10; }
                            try
                            {
                                d20 = dthb.Rows[0][1].ToString() + (char)13 + (char)10;
                            }
                            catch { d20 = "" + (char)13 + (char)10; }
                            try
                            {
                                d21 = dthb.Rows[0][2].ToString() + (char)13 + (char)10;
                            }
                            catch { d21 = "" + (char)13 + (char)10; }
                            try
                            {
                                d22 = dthb.Rows[0][3].ToString() + (char)13 + (char)10;
                            }
                            catch { d22 = "" + (char)13 + (char)10; }
                            try
                            {
                                d24 = dthb.Rows[0][4].ToString() + (char)13 + (char)10;
                            }
                            catch { d24 = "" + (char)13 + (char)10; }
                            try
                            {
                                d25 = dthb.Rows[0][5].ToString() + (char)13 + (char)10;
                            }
                            catch { d25 = "" + (char)13 + (char)10; }
                            try
                            {
                                d26 = dthb.Rows[0][6].ToString() + (char)13 + (char)10;
                            }
                            catch { d26 = "" + (char)13 + (char)10; }
                            try
                            {
                                d27 = dthb.Rows[0][7].ToString() + (char)13 + (char)10;
                            }
                            catch { d27 = "" + (char)13 + (char)10; }
                            try
                            {
                                d28 = dthb.Rows[0][8].ToString();
                            }
                            catch { d28 = ""; }

                            PublicClass.PrintHbgkxx(xzmc, d20, d21, d22, d24, d25, d26, d27, d28, so1);
                        }
                    }
                    catch
                    { }
                }

                timer2.Enabled = true;
            }
            if (StationName == "XU300")
            {
                timer2.Enabled = false;
                string sqlstr = "select  nvl(lsh,'') from ATPUZYQDYB order by ptime";
                DataTable dtsql = dataConn.GetTable(sqlstr);
                if (dtsql.Rows.Count > 0)
                {
                    string lsh1 = dtsql.Rows[0][0].ToString();
                    if (lsh1 != "")
                    {
                        ProductInfoEntity product1 = ProductInfoFactory.GetByCompanyCodeSNSingle(CompanyCode, lsh1);
                        DataTable dtsql2 = dataConn.GetTable("select station_name,plan_so from vw_data_complete where sn='" + lsh1 + "' and plan_code='" + product1.PLAN_CODE + "' and pline_code='" + product1.PLINE_CODE + "' and station_code in (select station_code from vw_code_station where station_type='ST01' and pline_code='" + product1.PLINE_CODE + "')");
                        string yg = "";
                        try
                        {
                            yg = dtsql2.Rows[0][0].ToString();
                        }
                        catch { }
                        string thisso1 = "";
                        try
                        {
                            thisso1 = dtsql2.Rows[0][1].ToString();
                        }
                        catch { }
                        string thisYh = "", thisJx = "", thisYhdm = "";
                        try
                        {
                            thisYh = dataConn.GetValue("select customer_name from data_plan where plan_code ='" + product1.PLAN_CODE + "' and rownum=1");
                        }
                        catch { }
                        try
                        {
                            thisJx = dataConn.GetValue("select product_model from data_plan where plan_code ='" + product1.PLAN_CODE + "' and rownum=1");
                        }
                        catch { }
                        try
                        {
                            thisYhdm = dataConn.GetValue("select nvl(KHH,'') from atpuplannameplate where plan_code='" + product1.PLAN_CODE + "'");
                            //thisYhdm = dataConn.GetValue("select khh from nameplate_so where plan_code='" + product1.PLAN_CODE + "'");
                        }
                        catch { }
                        PublicClass.PrintTmFz1(lsh1, thisso1, thisYh, thisYhdm, thisJx, 2);
                        dataConn.ExeSql("delete from ATPUZYQDYB where lsh='" + lsh1 + "'");
                    }
                }

                timer2.Enabled = true;
            }
        }

    }
}
