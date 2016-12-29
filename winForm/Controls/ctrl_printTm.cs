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

namespace Rmes.WinForm.Controls
{
    public partial class ctrl_printTm : BaseControl
    {
        //条码打印 通用的条码打印界面
        private string CompanyCode, PlineID, PlineCode1, StationID, len_sn, TheSo, TheSn, ThePlancode,stationname, StationName, Station_Code,isde_plancode,isde_sn;
        private string ShiftCode, TeamCode, UserID;
        ProductInfoEntity product;
        //dataConn dc = new dataConn();
        public ctrl_printTm()
        {
            InitializeComponent();
            //this.RMesDataChanged += new Rmes.WinForm.Base.RMesEventHandler(ctrlProductScan_RmesDataChanged);

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
            stationname = LoginInfo.StationInfo.STATION_NAME;
            TheSo = "";
            TheSn = "";
            ThePlancode = "";

        }
        public ctrl_printTm(string plancode12,string sn12)
        {
            InitializeComponent();
            //this.RMesDataChanged += new Rmes.WinForm.Base.RMesEventHandler(ctrlProductScan_RmesDataChanged);

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
            stationname = LoginInfo.StationInfo.STATION_NAME;
            TheSo = "";
            TheSn = "";
            ThePlancode = "";
            isde_plancode = plancode12;
            isde_sn = sn12;

        }
        protected void ctrlProductScan_RmesDataChanged(object obj, Rmes.WinForm.Base.RMESEventArgs e)
        {
            string sn, quality_status = "A";
            RMESEventArgs arg = new RMESEventArgs();
            arg.MessageHead = "";
            arg.MessageBody = "";
            if (e.MessageHead == null) return;

        }

        private void cmdExit_Click(object sender, EventArgs e)
        {
            this.ParentForm.Close();
        }

        private void cmdPrn_Click(object sender, EventArgs e)
        {
            DialogResult drt = MessageBox.Show("是否确认打印？", "提示", MessageBoxButtons.YesNo);
            if (drt == DialogResult.No) return;
            //if (StationName == "ATPU-U800")
            //{
            //    PublicClass.PrintTmFz1(txtLsh.Text.Trim().ToUpper(), txtSo.Text.Trim().ToUpper(), txtYh.Text, txtYHH.Text, txtJx.Text.Trim().ToUpper(), 2);
            //}
            //else if (StationName == "ATPU-F10")
            //{
            //    PublicClass.PrintTmWSx(txtLsh.Text.Trim().ToUpper(), txtSo.Text.Trim().ToUpper(), txtYh.Text, txtYHH.Text, txtJx.Text.Trim().ToUpper(), 2);
            //}
            //else
            PublicClass.PrintWTmFzSg(txtLsh.Text.Trim().ToUpper(), txtSo.Text.Trim().ToUpper(), txtYh.Text, txtYHH.Text, txtJx.Text.Trim().ToUpper(), 2, txtPlan.Text.Trim().ToUpper());
            if (LoginInfo.StationInfo.STATION_NAME == "ATPU-U800" || LoginInfo.StationInfo.STATION_NAME == "EFZSXTM")
            {
                PrntHg(txtLsh.Text.Trim().ToUpper(), txtSo.Text.Trim().ToUpper());
            }

        }

        private void txtLsh_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 13) return;
            if (string.IsNullOrWhiteSpace(txtLsh.Text)) return;
            string sn = txtLsh.Text.Trim().ToUpper();
            if (sn.Length != 8)
            {
                txtLsh.Text = "";
                MessageBox.Show("流水号位数为8位","提示");
                return;
            }
            product = ProductInfoFactory.GetByCompanyCodeSNSingle(CompanyCode, sn);//获取sn信息
            if (product == null)
            {
                txtLsh.Text = "";
                txtSo.Text = "";
                txtPlan.Text = "";
                txtJx.Text = "";
                txtYh.Text = "";
                txtYHH.Text = "";
                txtFr.Text = "";
                MessageBox.Show("流水号不合法", "提示");
                return;
            }
            string thisyhdm = dataConn.GetValue(" select nvl(KHH,'') from atpuplannameplate where bzso='" + product.PLAN_SO + "' and plan_code='" + product.PLAN_CODE + "' ");
            string thisFr = dataConn.GetValue("select GET_FR('" + product.PLAN_SO + "','" + LoginInfo.ProductLineInfo.PLINE_CODE + "') from dual ");
            txtSo.Text = product.PLAN_SO;
            txtPlan.Text = product.PLAN_CODE;
            txtJx.Text = product.PRODUCT_MODEL;
            txtYh.Text = product.CUSTOMER_NAME;
            txtYHH.Text = thisyhdm;
            txtFr.Text = thisFr;
            cmdPrn_Click(cmdPrn,new EventArgs());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.ParentForm.Close();
        }

        private void cmdPrn_Click_1(object sender, EventArgs e)
        {

        }

        private void PrntHg(string lsh1,string so1)
        {
            try
            {
                //20161216增加环保要求的条码打印 判断FR、SC 获取相应信息
                string fr1 = dataConn.GetValue("select get_planfr('" + lsh1 + "','FR') from dual");
                string sc1 = dataConn.GetValue("select get_planfr('" + lsh1 + "','SC') from dual");
                DataTable dthb = dataConn.GetTable("select xzmc,d20,d21,d22,d24,d25,d26,d27,d28 from COPY_PDM_HBGKXX where fr='" + fr1 + "' and sc='" + sc1 + "'");
                if (dthb.Rows.Count > 0)
                {
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

        private void button1_Click_1(object sender, EventArgs e)
        {
            string sn11 = textBox1.Text.Trim().ToUpper();
            ProductInfoEntity product = ProductInfoFactory.GetByCompanyCodeSNSingle(LoginInfo.CompanyInfo.COMPANY_CODE, sn11);
            if (product == null)
            {
                MessageBox.Show("该流水号不合法", "警告", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                Print(product);
            }
            catch(Exception e1)
            {
                MessageBox.Show(e1.Message, "提示");
            }
        }
        private void Print(ProductInfoEntity product)
        {
            try
            {
                if (stationname == "ZF75")
                {
                    //宇通条码
                    string sql = "select * from atpukhtm where so='" + product.PLAN_SO + "' and khmc='宇通'";
                    DataTable dt = dataConn.GetTable(sql);
                    if (dt.Rows.Count > 0)
                    {
                        //sql = "select ptime,to_char(ptime,'yymmdd') ptime1,to_char(ptime,'yyyy-mm-dd') ptime2 from atpudqmpb where lsh='" + product.SN + "'";
                        //DataTable dt1 = dataConn.GetTable(sql);

                        string wlh = dataConn.GetValue("select GET_KHTM('WLH','宇通','" + product.PLAN_SO + "','') from dual ");
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
                        PublicClass.printFT(dt.Rows[0][0].ToString(), dt.Rows[0][1].ToString(), product.SN);
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
                    string sql = "select rmes_id,sn,plan_so,plan_code,pline_code,station_code,sub_zc,check_flag from DATA_SUBPACK where WORK_FLAG='N' and station_code='" + LoginInfo.StationInfo.STATION_CODE + "' and sn='" + product.SN + "' order by start_time ";
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
                MessageBox.Show("条码打印出错!" + e.Message);
            }
        }
    }
}
