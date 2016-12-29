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
using Rmes.DA.Procedures;
namespace Rmes.WinForm.Controls
{
    public partial class ctrl_check : BaseControl
    {
        private string CompanyCode, PlineCode, PlineCode1, StationCode, stationname;
        //dataConn dc = new dataConn();
        public ctrl_check()
        {
            InitializeComponent();
            //CsGlobalClass.NEEDVEPS = false;
            CompanyCode = LoginInfo.CompanyInfo.COMPANY_CODE;
            PlineCode = LoginInfo.ProductLineInfo.RMES_ID;
            PlineCode1 = LoginInfo.ProductLineInfo.PLINE_CODE;
            StationCode = LoginInfo.StationInfo.RMES_ID;
            stationname = LoginInfo.StationInfo.STATION_NAME;
            this.RMesDataChanged += new RMesEventHandler(ctrl_check_RMesDataChanged);
        }
        void ctrl_check_RMesDataChanged(object obj, Rmes.WinForm.Base.RMESEventArgs e)
        {
            try
            {
                RMESEventArgs arg = new RMESEventArgs();
                arg.MessageHead = "";
                arg.MessageBody = "";
                if (e.MessageHead == null) return;
                if (e.MessageHead == "CHECK")
                {
                    string sn = e.MessageBody as string;
                    

                    ProductInfoEntity product = ProductInfoFactory.GetByCompanyCodeSNSingle(LoginInfo.CompanyInfo.COMPANY_CODE, sn);
                    if (product == null)
                    {
                        MessageBox.Show("该流水号不合法", "警告", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    if (product.LQ_FLAG == "Y" && LoginInfo.StationInfo.STATION_AREA == "U")
                    {
                        arg.MessageHead = "LQ";
                        arg.MessageBody = sn;
                        SendDataChangeMessage(arg);
                        MessageBox.Show("该计划为柳汽计划", "警告", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    string plancode = product.PLAN_CODE;
                    string plinecode = LoginInfo.ProductLineInfo.PLINE_CODE;
                    //ZS5-1跳序控制
                    AtpuEntity tx = AtpuFactory.GetByStationCode(LoginInfo.StationInfo.STATION_NAME,LoginInfo.ProductLineInfo.PLINE_CODE);
                    if (tx != null)
                    {

                        if (tx.ZDMC == stationname && tx.GZDD == LoginInfo.ProductLineInfo.PLINE_CODE)
                        {
                            string jhmc = "";
                            string sql = "select jhmc from atpujhb_tmp where zdmc='" + stationname + "' and wcsl<sl  and jhmc<>'" + plancode + "' and flag='Y' ";
                            DataTable dt = dataConn.GetTable(sql);
                            if (dt.Rows.Count > 0)
                            {
                                jhmc = dt.Rows[0][0].ToString();
                            }
                                sql = "select flag from atpujhb_tmp where zdmc='" + stationname + "'   and jhmc='" + plancode + "'";
                                DataTable dt1 = dataConn.GetTable(sql);
                                if (dt1.Rows.Count == 0)
                                {
                                    sql = "insert into atpujhb_tmp(jhdm,jhmc,jhso,ggxhmc,sl,yhmc,rqbegin,rqend,zdrq,sxsl,wcsl,bz,xh,fdjxl,gzdd,ghsl,havebom,run,qr,kffl,jzrq,unitno,gzqy,zdmc) ";
                                    sql = sql + " select plan_seq,plan_code,plan_so,product_model,plan_qty,customer_name,begin_date,end_date,create_time,online_qty,(select count(distinct ghtm) from sjwcb_tmp where jhdm='" + plancode + "' and zdmc='" + stationname + "'),remark,'','',rounting_site,'',bom_flag,run_flag,confirm_flag,item_flag,account_date,lq_flag,pline_code,'" + stationname + "' ";
                                    sql = sql + " from data_plan where plan_code='" + plancode + "' ";
                                    dataConn.ExeSql(sql);
                                    if (jhmc != "")
                                    {
                                        MessageBox.Show("计划" + jhmc + "尚未完成，请联系班组长解锁", "警告", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        arg.MessageHead = "INIT";
                                        arg.MessageBody = sn;
                                        SendDataChangeMessage(arg);
                                        return;
                                    }
                                }
                                else
                                {
                                    if (jhmc != "" && dt1.Rows[0][0].ToString() != "Y")
                                    {
                                        MessageBox.Show("计划" + jhmc + "尚未完成，请联系班组长解锁", "警告", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        arg.MessageHead = "INIT";
                                        arg.MessageBody = sn;
                                        SendDataChangeMessage(arg);
                                        return;
                                    }
                                }
                            
                        }
                    }
                    //ZF180VEPS灌录
                    if (stationname == "ZF180" || stationname == "ATPU-U860")
                    {
                        string veps = "";
                        //ProductDataFactory.VEPS_CHECK_SO(LoginInfo.ProductLineInfo.PLINE_CODE, product.PLAN_SO, out veps);
                        VEPS_CHECK_SO sp = new VEPS_CHECK_SO()
                        {
                            GZDD1 = LoginInfo.ProductLineInfo.PLINE_CODE,
                            SO1 = product.PLAN_SO,
                            RETSTR1 = ""
                        };
                        Procedure.run(sp);
                        veps = sp.RETSTR1;
                        if (veps == "0")
                        {
                            CsGlobalClass.NEEDVEPS = false;
                        }
                        else if (veps == "1")
                        {
                            MessageBox.Show("VEPS数据维护不全，请联系管理人员！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            arg.MessageHead = "INIT";
                            arg.MessageBody = sn;
                            SendDataChangeMessage(arg);
                            CsGlobalClass.NEEDVEPS = false;
                            return;
                        }
                        else if (veps == "2")
                        {
                            CsGlobalClass.NEEDVEPS = true;
                            //arg.MessageHead = "VEPS";
                            //arg.MessageBody = sn;
                            //SendDataChangeMessage(arg);
                        }
                    }
                    //机型 SO变换提示 机型取消
                    if (stationname != "ZF200-1" && stationname != "ZF210" && stationname != "ZF245")
                    {
                        string tishi = PublicClass.checktishi(stationname, product.PLAN_SO, sn);
                        if (tishi != "")
                        {
                            MessageBox.Show(tishi, "警告", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            arg.MessageHead = "INIT";
                            arg.MessageBody = sn;
                            SendDataChangeMessage(arg);
                            if (tishi.Contains("漏扫"))
                                return;
                        }
                    }
                    try
                    {
                        //宇通判断 SO框变绿
                        if (product.REMARK.Contains("宇通"))
                        {
                            arg.MessageHead = "YT";
                            arg.MessageBody = sn;
                            SendDataChangeMessage(arg);
                        }
                        else
                        {
                            arg.MessageHead = "FYT";
                            arg.MessageBody = sn;
                            SendDataChangeMessage(arg);
                        }
                        //军车判断 SO框变红
                        if (product.REMARK.Contains("JC"))
                        {
                            arg.MessageHead = "JC";
                            arg.MessageBody = sn;
                            SendDataChangeMessage(arg);
                        }
                        else
                        {
                            if (arg.MessageHead != "YT")
                            {
                                arg.MessageHead = "FJC";
                                arg.MessageBody = sn;
                                SendDataChangeMessage(arg);
                            }
                        }
                    }
                    catch { }
                    CsGlobalClass.OLDSO = product.PLAN_SO;
                    CsGlobalClass.OLDJX = product.PRODUCT_MODEL;
                    try
                    {
                        CsGlobalClass.FDJXL = dataConn.GetValue("select nvl(upper(xl),'') from copy_engine_property where upper(so)='" + product.PLAN_SO + "'");
                    }
                    catch
                    {
                        CsGlobalClass.FDJXL = "";
                    }
                    if (LoginInfo.ProductLineInfo.PLINE_CODE == "L")
                    {
                        //显示BOM 显示装机提示 
                        arg.MessageHead = "SHOWBOMLQ";
                        arg.MessageBody = sn;
                        SendDataChangeMessage(arg);
                    }
                    else
                    {
                        //显示BOM 显示装机提示 
                        arg.MessageHead = "SHOWBOM";
                        arg.MessageBody = sn;
                        SendDataChangeMessage(arg);
                    }
                    //显示检测数据
                    arg.MessageHead = "SHOWDETECT";
                    arg.MessageBody = sn;
                    SendDataChangeMessage(arg);
                    ////装机图片显示 转到showbom执行
                    //if (stationname != "ZF245")
                    //{
                    //    arg.MessageHead = "SHOWPICTURE";
                    //    arg.MessageBody = sn;
                    //    SendDataChangeMessage(arg);
                    //}
                    //判断改制 窗体变红
                    if (product.PLAN_TYPE == "C" || product.PLAN_TYPE == "D")
                    {
                        arg.MessageHead = "GZ";
                        arg.MessageBody = sn;
                        SendDataChangeMessage(arg);
                    }
                    //试验程序号
                    if (stationname == "Z460" || stationname == "ATPU-A30" || stationname == "XA120" || stationname == "RA020")
                    {
                        PublicClass.TestDataCreate(sn, product.PLINE_CODE,product.PLAN_CODE);////未完成 取计划对应的FR
                    }
                    ////特殊站点装机提示 转到showbom执行
                    //if (stationname == "ZF130" && sn.Length >= 8)
                    //{
                    //    string zjts = "";
                    //    zjts = dataConn.GetValue("select PL_QRY_QZDSJ('" + sn + "','Z635','ZF130') from dual");
                    //    arg.MessageHead = "ZJTSADD";
                    //    arg.MessageBody = zjts;
                    //    SendDataChangeMessage(arg);
                    //}
                    //重复扫描
                    if (sn == CsGlobalClass.NEWSN)
                    {
                        if (CsGlobalClass.NEEDVEPS)
                        {
                            //重复扫描 灌录VEPS
                            try
                            {
                                PublicClass.Handle_Veps(product.PLAN_SO, sn);
                                CsGlobalClass.NEEDVEPS = false;
                            }
                            catch
                            {
                                CsGlobalClass.NEEDVEPS = false;
                            }
                        }
                        arg.MessageHead = "RECHECK";
                        arg.MessageBody = sn;
                        SendDataChangeMessage(arg);
                        return;//重复扫描返回
                    }
                    else
                    {
                        //三漏数据处理 每次处理上一个流水号  退出时处理当前
                        if (stationname == "Z690-1" || stationname == "Z690-2" || stationname == "Z690-3" || stationname == "Z690-4" || stationname == "ATPU-A470" || stationname == "ATPU-A480")
                        {
                            if (CsGlobalClass.NEWSN != "")
                                PublicClass.InsertSLData(CsGlobalClass.NEWSN);
                        }
                        if (CsGlobalClass.NEWSN != "")
                            dataConn.ExeSql("update data_complete set complete_time=sysdate where sn='" + CsGlobalClass.NEWSN + "' and plan_code='" + CsGlobalClass.NEWPLANCODE + "' and station_code='" + LoginInfo.StationInfo.STATION_CODE + "' and complete_time is null ");
                        CsGlobalClass.NEWSN = sn;
                        CsGlobalClass.NEWPLANCODE = product.PLAN_CODE;
                        //CsGlobalClass.DGSM = false;
                    }

                    //
                    if (stationname == "Z750-1")
                    {
                        string count1 = dataConn.GetValue("select count(1) from data_plan where plan_code='" + product.PLAN_CODE + "' and plan_so ='" + product.PLAN_SO + "' and remark like '%缸体端面不打印机型号%'");
                        if (count1 != "0")
                        {
                            MessageBox.Show("缸体端面未打印机型号、流水号，请注意!");
                        }
                    }
                    //量检具辅料周期更换提醒
                    string sqllj = "select t.ljj,t.cycle,t.sl,t.xh from atpuflgjthzqb t where location_name='" + stationname + "'";
                    DataTable dtlj = dataConn.GetTable(sqllj);
                    for (int i = 0; i < dtlj.Rows.Count; i++)
                    {
                        string LJJName = dtlj.Rows[i][0].ToString();
                        int LJJCycle = Convert.ToInt32(dtlj.Rows[i][1].ToString());
                        int LJJSL = Convert.ToInt32(dtlj.Rows[i][2].ToString());
                        string LJJXh = dtlj.Rows[i][3].ToString();

                        dataConn.ExeSql("update atpuflgjthzqb set sl=sl+1 WHERE xh='" + LJJXh + "'");

                        if ((LJJSL + 1) % LJJCycle == 0)
                        {
                            MessageBox.Show("第" + LJJCycle + "台,请更换" + LJJName);
                            dataConn.ExeSql(" update atpuflgjthzqb set sl=0 WHERE xh='" + LJJXh + "'");
                        }
                    }
                    //完成记录 零件扫描 检测数据录入

                    //打印
                    Print(product);


                    arg.MessageHead = "CHECKOK";
                    arg.MessageBody = sn;
                    SendDataChangeMessage(arg);
                }
            }
            catch(Exception e1)
            {
                if (!e1.Message.Contains("由于程序无法提交或取消单元格值更改，操作失败。"))
                    MessageBox.Show(e1.Message);
            }
            //SendDataChangeMessage(arg);
        }
        private void Print(ProductInfoEntity product)
        {
            try
            {
                if (stationname == "ZF75")
                {
                    //宇通条码
                    string sql = "select * from atpukhtm where so='" + product.PLAN_SO+ "' and khmc='宇通'";
                    DataTable dt = dataConn.GetTable(sql);
                    if (dt.Rows.Count > 0)
                    {
                        //sql = "select ptime,to_char(ptime,'yymmdd') ptime1,to_char(ptime,'yyyy-mm-dd') ptime2 from atpudqmpb where lsh='" + product.SN + "'";
                        //DataTable dt1 = dataConn.GetTable(sql);

                        string wlh = dataConn.GetValue("select GET_KHTM('WLH','宇通','"+product.PLAN_SO+"','') from dual ");
                        string gysdm = dataConn.GetValue("select GET_KHTM('GYSDM','宇通','" + product.PLAN_SO + "','') from dual ");
                        string rqpc = dataConn.GetValue("select GET_KHTM('RQPC','宇通','" + product.PLAN_SO + "','" + DateTime.Now.ToString("yyMMdd") + "') from dual ");
                        string rqpc1 = dataConn.GetValue("select GET_KHTM('RQPC','宇通','" + product.PLAN_SO + "','" + DateTime.Now.ToString("yyMMdd") + "') from dual ");
                        string wlh2 = dataConn.GetValue("select GET_KHTM('WLH2','宇通','" + product.PLAN_SO + "','') from dual ");
                        string remark = product.REMARK;
                        //if (dt1.Rows.Count > 0)
                        {
                            PublicClass.PrintTmZF200(wlh + gysdm + rqpc, wlh2, DateTime.Now.ToString("yyyy-MM-dd"), gysdm);
                        }
                        //else
                        //{
                        //    //MessageBox.Show("该发动机没有打印铭牌，条码时间按照当前时间","提示");
                        //    PublicClass.PrintTmZF200(wlh + remark.Substring(remark.Length - 1) + gysdm + rqpc1, wlh2 + remark.Substring(remark.Length - 1), DateTime.Now.ToString("yyyy-MM-dd"), gysdm);
                        //}
                        return;
                    }
                    //江淮条码
                    if (product.CUSTOMER_NAME.Contains("江淮"))
                    {
                        PublicClass.printJh(product.SN + "001004" + product.PRODUCT_MODEL);
                        return;
                    }
                    //厦工
                    sql = "select wlh from atpukhtm where khmc='厦工' and SO = '" + product.PLAN_SO + "' ";
                    dt = dataConn.GetTable(sql);
                    if (dt.Rows.Count > 0)
                    {
                        PublicClass.printXiag(dt.Rows[0][0].ToString() + "A10001215" + product.SN);
                        return;
                    }
                    //福田
                    sql = "select nvl(wlh,''),nvl(gysdm,'') from atpukhtm where so='" + product.PLAN_SO + "' and khmc='福田' ";
                    dt = dataConn.GetTable(sql);
                    if (dt.Rows.Count > 0)
                    {
                        PublicClass.printFT(dt.Rows[0][0].ToString(), dt.Rows[0][1].ToString(),product.SN);
                        return;
                    }
                    //苏州金龙
                    if (product.CUSTOMER_NAME.Contains("苏州金龙"))
                    {
                        PublicClass.printSZJL(product.SN,product.PLAN_SO,product.PLAN_CODE);
                        return;
                    }
                    //中通客车
                    sql = "select * from atpukhtm where so='" + product.PLAN_SO + "' and khmc='中通客车'";
                    dt = dataConn.GetTable(sql);
                    if (dt.Rows.Count > 0)
                    {
                        PublicClass.printZTKC(product.SN, product.PLAN_SO, product.PLAN_CODE);
                        return;
                    }
                }
                if (stationname == "ZF155")
                {
                    //DFAC
                    if (product.CUSTOMER_NAME.Contains("DFAC"))
                    {
                        PublicClass.printDFAC(product.SN, product.PLAN_SO, product.PLAN_CODE);
                        return;
                    }
                    //旅行车
                    if (product.CUSTOMER_NAME.Contains("旅行车"))
                    {
                        PublicClass.printlxc(product.SN, product.PLAN_SO, product.PLAN_CODE);
                        return;
                    }
                    //CPT
                    if (product.CUSTOMER_NAME.Contains("CPT"))
                    {
                        PublicClass.printCPT(product.SN, product.PLAN_SO, product.PLAN_CODE);
                        return;
                    }
                    //徐工
                    string sql = "select nvl(wlh,''),nvl(gysdm,'') from atpukhtm where so='" + product.PLAN_SO + "' and khmc='徐工' ";
                    DataTable dt = dataConn.GetTable(sql);
                    if (dt.Rows.Count > 0)
                    {
                        PublicClass.printxg(dt.Rows[0][0].ToString(), dt.Rows[0][1].ToString(), product.SN);
                        return;
                    }
                    //东风商用车公司
                    if (product.CUSTOMER_NAME.Contains("东风商用车公司") || product.CUSTOMER_NAME.Contains("东风商用车有限公司"))
                    {
                        PublicClass.printDFCVE(product.SN, product.PLAN_SO, product.PLAN_CODE);
                        return;
                    }
                    //东风创普
                    if (product.CUSTOMER_NAME.Contains("东风创普"))
                    {
                        PublicClass.printDFCP(product.SN, product.PLAN_SO, product.PLAN_CODE);
                        return;
                    }
                    //专底
                    if (product.CUSTOMER_NAME.Contains("专底"))
                    {
                        PublicClass.printDFZD(product.SN, product.PLAN_SO, product.PLAN_CODE);
                        return;
                    }
                    //厦门金龙
                    if (product.CUSTOMER_NAME.Contains("厦门金龙"))
                    {
                        PublicClass.printXMJL(product.SN, product.PLAN_SO, product.PLAN_CODE,product.PRODUCT_MODEL);
                        return;
                    }
                    //三一汽车起重机械有限公司
                    if (product.CUSTOMER_NAME.Contains("三一汽车起重机械有限公司"))
                    {
                        PublicClass.printSYQZJ(product.SN, product.PLAN_SO, product.PLAN_CODE, product.PRODUCT_MODEL);
                        return;
                    }
                    //%东风神宇%
                    if (product.CUSTOMER_NAME.Contains("东风神宇"))
                    {
                        PublicClass.printDFSY(product.SN, product.PLAN_SO, product.PLAN_CODE, product.PRODUCT_MODEL);
                        return;
                    }

                }
                if (stationname == "ATPU-U870" || stationname == "XU900") //西区和Z
                {
                    //徐工
                    string sql = "select nvl(wlh,''),nvl(gysdm,'') from atpukhtm where so='" + product.PLAN_SO + "' and khmc='徐工' ";
                    DataTable dt = dataConn.GetTable(sql);
                    if (dt.Rows.Count > 0)
                    {
                        PublicClass.printxg(dt.Rows[0][0].ToString(), dt.Rows[0][1].ToString(), product.SN);
                        return;
                    }
                    //东风商用车公司
                    if (product.CUSTOMER_NAME.Contains("东风商用车公司") || product.CUSTOMER_NAME.Contains("东风商用车有限公司"))
                    {
                        PublicClass.printDFCVW(product.SN, product.PLAN_SO, product.PLAN_CODE);
                        return;
                    }
                    //CPT
                    if (product.CUSTOMER_NAME.Contains("CPT"))
                    {
                        PublicClass.printCPTW(product.SN, product.PLAN_SO, product.PLAN_CODE);
                        return;
                    }
                    //东风创普
                    if (product.CUSTOMER_NAME.Contains("东风创普"))
                    {
                        PublicClass.printDFCP(product.SN, product.PLAN_SO, product.PLAN_CODE);
                        return;
                    }
                    //专底
                    if (product.CUSTOMER_NAME.Contains("专底"))
                    {
                        PublicClass.printDFZD(product.SN, product.PLAN_SO, product.PLAN_CODE);
                        return;
                    }
                    //厦门金龙
                    if (product.CUSTOMER_NAME.Contains("厦门金龙"))
                    {
                        PublicClass.printXMJL(product.SN, product.PLAN_SO, product.PLAN_CODE, product.PRODUCT_MODEL);
                        return;
                    }
                    //三一汽车起重机械有限公司
                    if (product.CUSTOMER_NAME.Contains("三一汽车起重机械有限公司"))
                    {
                        PublicClass.printSYQZJ(product.SN, product.PLAN_SO, product.PLAN_CODE, product.PRODUCT_MODEL);
                        return;
                    }
                    //%东风神宇%
                    if (product.CUSTOMER_NAME.Contains("东风神宇"))
                    {
                        PublicClass.printDFSY(product.SN, product.PLAN_SO, product.PLAN_CODE, product.PRODUCT_MODEL);
                        return;
                    }

                }
                if (stationname == "ATPU-U820")
                {
                    //宇通条码
                    string sql = "select * from atpukhtm where so='" + product.PLAN_SO + "' and khmc='宇通'";
                    DataTable dt = dataConn.GetTable(sql);
                    if (dt.Rows.Count > 0)
                    {
                        //sql = "select dysj,to_char(dysj,'yymmdd') ptime1,to_char(dysj,'yyyy-mm-dd') ptime2 from sjchgnmplt where ghtm='" + product.SN + "' order by rqsj desc";
                        ////sql = "select ptime,to_char(ptime,'yymmdd') ptime1,to_char(ptime,'yyyy-mm-dd') ptime2 from atpudqmpb where lsh='" + product.SN + "'";
                        //DataTable dt1 = dataConn.GetTable(sql);

                        string wlh = dataConn.GetValue("select GET_KHTM('WLH','宇通','" + product.PLAN_SO + "','') from dual ");
                        string gysdm = dataConn.GetValue("select GET_KHTM('GYSDM','宇通','" + product.PLAN_SO + "','') from dual ");
                        //string rqpc = dataConn.GetValue("select GET_KHTM('RQPC','宇通','" + product.PLAN_SO + "','" + dt1.Rows[0][1].ToString() + "') from dual ");
                        string rqpc = dataConn.GetValue("select GET_KHTM('RQPC','宇通','" + product.PLAN_SO + "','" + DateTime.Now.ToString("yyMMdd") + "') from dual ");
                        string rqpc1 = dataConn.GetValue("select GET_KHTM('RQPC','宇通','" + product.PLAN_SO + "','" + DateTime.Now.ToString("yyMMdd") + "') from dual ");
                        string wlh2 = dataConn.GetValue("select GET_KHTM('WLH2','宇通','" + product.PLAN_SO + "','') from dual ");
                        string remark = product.REMARK;
                        //if (dt1.Rows.Count > 0)
                        {
                            PublicClass.PrintTmU820(wlh + gysdm + rqpc, wlh2, DateTime.Now.ToString("yyyy-MM-dd"), gysdm);
                        }
                        //else
                        //{
                        //    //MessageBox.Show("该发动机没有打印铭牌，条码时间按照当前时间", "提示");
                        //    PublicClass.PrintTmU820(wlh + remark.Substring(remark.Length - 1) + gysdm + rqpc1, wlh2 + remark.Substring(remark.Length - 1), DateTime.Now.ToString("yyyy-MM-dd"), gysdm);
                        //}
                        return;
                    }
                    //福田
                    sql = "select nvl(wlh,''),nvl(gysdm,'') from atpukhtm where so='" + product.PLAN_SO + "' and khmc='福田' ";
                    dt = dataConn.GetTable(sql);
                    if (dt.Rows.Count > 0)
                    {
                        PublicClass.printFT(dt.Rows[0][0].ToString(), dt.Rows[0][1].ToString(), product.SN);
                        return;
                    }
                    //江淮条码
                    if (product.CUSTOMER_NAME.Contains("江淮"))
                    {
                        sql = "select nvl(wlh,''),nvl(gysdm,'') from atpukhtm where so='" + product.PLAN_SO + "' and khmc='江淮' ";
                        dt = dataConn.GetTable(sql);
                        if (dt.Rows.Count > 0)
                        {
                            PublicClass.printJh(product.SN + dt.Rows[0][1].ToString() + product.PRODUCT_MODEL);
                            return;
                        }
                        else
                        {
                            PublicClass.printJh(product.SN + "" + product.PRODUCT_MODEL);
                            return;
                        }
                    }
                    //厦工
                    sql = "select wlh from atpukhtm where khmc='厦工' and SO = '" + product.PLAN_SO + "' ";
                    dt = dataConn.GetTable(sql);
                    if (dt.Rows.Count > 0)
                    {
                        PublicClass.printXiag(dt.Rows[0][0].ToString() + "A10001215" + product.SN);
                        return;
                    }
                    //苏州金龙
                    if (product.CUSTOMER_NAME.Contains("苏州金龙"))
                    {
                        PublicClass.printSZJL(product.SN, product.PLAN_SO, product.PLAN_CODE);
                        return;
                    }
                    //中通客车
                    sql = "select * from atpukhtm where so='" + product.PLAN_SO + "' and khmc='中通客车'";
                    dt = dataConn.GetTable(sql);
                    if (dt.Rows.Count > 0)
                    {
                        PublicClass.printZTKC(product.SN, product.PLAN_SO, product.PLAN_CODE);
                        return;
                    }
                }
                if (stationname == "RA410")
                {
                    //CPT
                    if (product.CUSTOMER_NAME.Contains("CPT") && !product.REMARK.Contains("补充产能"))
                    {
                        PublicClass.printCPT(product.SN, product.PLAN_SO, product.PLAN_CODE);
                        return;
                    }
                }

                if (stationname == "ZS35")
                {
                    //分装站点
                        PublicClass.printFZTM(product.SN, product.PLAN_SO, product.PLAN_CODE);
                        return;
                }

                if (LoginInfo.StationInfo.STATION_TYPE == "ST04")
                {
                    //分装站点打印分装虚拟总成条码
                    string sql = "select rmes_id,sn,plan_so,plan_code,pline_code,station_code,sub_zc,check_flag from DATA_SUBPACK where WORK_FLAG='N' and station_code='" + LoginInfo.StationInfo.STATION_CODE + "' and sn='"+product.SN+"' order by start_time ";
                    DataTable dt = dataConn.GetTable(sql);
                    if (dt.Rows.Count > 0)
                    {
                        string rmesid = dt.Rows[0]["RMES_ID"].ToString();
                        string subzc = "";
                        subzc = dt.Rows[0]["SUB_ZC"].ToString();
                        string subItem = "";
                        if (subzc != "")
                        {
                            //判断bom中是否包含该总成零件 
                            //分装总成零件不为空  则打印分装总成零件条码
                            subItem = subzc + "^SUB^" + product.SN;
                            //Print 
                            PublicClass.PrintSub(product.SN, "", subItem);
                            dataConn.ExeSql("update data_subpack set sub_item='" + subItem + "' where rmes_id='" + rmesid + "'");
                        }
                    }
                }

            }
            catch (Exception e)
            {
                MessageBox.Show("条码打印出错!"+e.Message);
            }
        }
        
    }
}
