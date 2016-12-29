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
using Rmes.Pub.Data1;
using System.Collections;
using System.IO;

namespace Rmes.WinForm.Controls
{
    public partial class ctrlShowBom : BaseControl
    {
        public string SN,So,productmodel;
        public string planCode,orderCode;
        public string companyCode, PlineCode, Plineid, stationCode, stationtype,WorkunitCode, LinesideStock, stationname, pline_type, Station_Code, stationcode_fx, stationname_fx;
        private string oldVendorCode;
        public string VItemCode;
        public bool IsABC = false;
        //dataConn dc = new dataConn();
        public ctrlShowBom()
        {
            InitializeComponent();
            stationCode = LoginInfo.StationInfo.RMES_ID;
            Station_Code = LoginInfo.StationInfo.STATION_CODE;
            stationtype = LoginInfo.StationInfo.STATION_TYPE;
            PlineCode = LoginInfo.ProductLineInfo.PLINE_CODE;
            Plineid = LoginInfo.ProductLineInfo.RMES_ID;
            companyCode = LoginInfo.CompanyInfo.COMPANY_CODE;
            stationname = LoginInfo.StationInfo.STATION_NAME;
            pline_type = LoginInfo.ProductLineInfo.PLINE_TYPE_CODE;
            planCode = "";
            dgvBom.AutoGenerateColumns = false;
            dgvBom.RowHeadersVisible = false;
            dgvBom.DataSource = null;
            this.RMesDataChanged += new Rmes.WinForm.Base.RMesEventHandler(ctrlShowBom_RmesDataChanged);
        }
        public ctrlShowBom(string sn11)
        {
            //用于现场待处理物料数据录入
            InitializeComponent();
            stationCode = LoginInfo.StationInfo.RMES_ID;
            Station_Code = LoginInfo.StationInfo.STATION_CODE;
            stationtype = LoginInfo.StationInfo.STATION_TYPE;
            PlineCode = LoginInfo.ProductLineInfo.PLINE_CODE;
            Plineid = LoginInfo.ProductLineInfo.RMES_ID;
            companyCode = LoginInfo.CompanyInfo.COMPANY_CODE;
            stationname = LoginInfo.StationInfo.STATION_NAME;
            pline_type = LoginInfo.ProductLineInfo.PLINE_TYPE_CODE;
            planCode = "";
            dgvBom.AutoGenerateColumns = false;
            dgvBom.RowHeadersVisible = false;
            dgvBom.DataSource = null;
            ProductInfoEntity product = ProductInfoFactory.GetByCompanyCodeSNSingle(LoginInfo.CompanyInfo.COMPANY_CODE, sn11);
            if (product == null) return;
            planCode = product.PLAN_CODE;
            productmodel = product.PRODUCT_MODEL;
            So = product.PLAN_SO;
            SN = sn11;
            InitBomItemList_Show(Station_Code, stationname);
            dgvBom.ClearSelection();
        }
        public ctrlShowBom(string plancode11, string stationcode11)
        {
            //用于双击计划弹出的BOM查看
            InitializeComponent();
            companyCode = LoginInfo.CompanyInfo.COMPANY_CODE;
            PlineCode = LoginInfo.ProductLineInfo.PLINE_CODE;

            dgvBom.AutoGenerateColumns = false;
            dgvBom.RowHeadersVisible = false;
            dgvBom.DataSource = null;
            string so1 = dataConn.GetValue("select plan_so from data_plan where plan_code='"+plancode11+"' and rownum=1");
            //根据返修工作站选择的站点加载对应bom
            string fdjxl = dataConn.GetValue("select GET_FDJXL('" + So + "') from dual");
            //获取rstbomqd rstbomts
            SNBomTempFactory.ShowBomDCEC_CL(companyCode, plancode11, PlineCode, stationcode11, "", fdjxl, so1, stationcode11);

            dgvBom.DataSource = null;
            /////modify by thl 20161026 关联temp表 显示历史记录
            //string sql = "select a.gwmc, case when instr(a.comp,'#')=1 then substr(a.comp,2) else a.comp end comp ,a.udesc,a.qty,a.gxmc,b.station_name zdmc,b.station_code zddm,a.gysmc,nvl(b.complete_qty,a.qty) complete_qty,func_get_itemclass((select pline_code from vw_code_station where station_name=zdmc and rownum=1),'','',comp) item_class,nvl(b.item_type,'C')  item_type,b.replace_flag,b.confirm_flag,b.rmes_id from rstbomqd a "
            //    + " left join data_sn_bom_TEMP b   "
            //    + " on  a.gwmc=b.location_code "
            //    + " and a.gxmc=b.PROCESS_CODE "
            //    + " and replace(a.comp,'#','')=b.item_code  "
            //    + " and b.sn='" + SN + "' and b.plan_code='" + planCode + "' and b.pline_code='" + PlineCode + "' "
            //    + " where a.zddm='" + stationcode11 + "'  order by a.gwmc,a.gxmc,replace(a.comp,'#','')  "; //and b.rmes_id is not null
            string sql = "select a.gwmc, case when instr(a.comp,'#')=1 then substr(a.comp,2) else a.comp end comp ,a.udesc,a.qty,a.gxmc,'' zdmc,'' zddm,a.gysmc,a.qty complete_qty,func_get_itemclass('" + PlineCode + "','','',comp) item_class,FUNC_GET_ITEMTYPE('" + PlineCode + "','" + stationcode11 + "',a.gwmc,replace(a.comp,'#','')) item_type,'' replace_flag,'N' confirm_flag,'' rmes_id from rstbomqd a "
                    + " where a.zddm='" + stationcode11 + "'  order by a.gwmc,a.gxmc,replace(a.comp,'#','')  "; //and b.rmes_id is not null
            
            DataTable dt = dataConn.GetTable(sql);

            dgvBom.DataSource = dt;
            for (int i = 0; i < dgvBom.Rows.Count; i++)
            {
                SetBomRowStatus(i);
            }
            timer1.Enabled = true;
            timer1.Start();
            dgvBom.ClearSelection();
        }

        //private void InitBomItemList()
        //{

        //    string fdjxl = dataConn.GetValue("select GET_FDJXL('"+So+"') from dual");
        //    //获取rstbomqd rstbomts
        //    SNBomTempFactory.ShowBomDCEC(companyCode, planCode, PlineCode, Station_Code, SN, fdjxl, So);
        //    //得到当前站点的bom以及装机提示
        //    RMESEventArgs arg = new RMESEventArgs();
        //    arg.MessageHead = "SHOWZJTS";
        //    arg.MessageBody = SN;
        //    SendDataChangeMessage(arg);
        //    //特殊站点装机提示
        //    if (stationname == "ZF130" && SN.Length >= 8)
        //    {
        //        string zjts = "";
        //        zjts = dataConn.GetValue("select PL_QRY_QZDSJ('" + SN + "','Z635','ZF130') from dual");
        //        arg.MessageHead = "ZJTSADD";
        //        arg.MessageBody = zjts;
        //        SendDataChangeMessage(arg);
        //    }
        //    //装机图片显示
        //    if (stationname != "ZF245")
        //    {
        //        ArrayList wjpath = new ArrayList();
        //        string wjlj = CsGlobalClass.WJLJ;
        //        string sql12 = "select wjpath from rstbomts where zddm='" + Station_Code + "' and wjpath is not null ";
        //        DataTable dt12 = dataConn.GetTable(sql12);
        //        for (int i = 0; i < dt12.Rows.Count; i++)
        //        {
        //            string[] s = dt12.Rows[i][0].ToString().Split('$');
        //            for (int j = 0; j < s.Length; j++)
        //            {
        //                string path1 = wjlj + "\\" + PlineCode + "\\" + s[j];
        //                if (File.Exists(path1))
        //                {
        //                    if (!wjpath.Contains(path1))
        //                    {
        //                        wjpath.Add(path1);
        //                    }
        //                }
        //            }
        //        }
        //        if (wjpath.Count > 0)
        //        {
        //            FrmShowProcessPic ps = new FrmShowProcessPic();
        //            ps.StartPosition = FormStartPosition.Manual;
        //            ps.Location = new Point(0, 400);
        //            ps.Show();
        //        }
        //        //arg.MessageHead = "SHOWPICTURE";
        //        //arg.MessageBody = SN;
        //        //SendDataChangeMessage(arg);
        //    }
        //    dgvBom.DataSource = null;
        //    string sql = "select a.gwmc, case when instr(a.comp,'#')=1 then substr(a.comp,2) else a.comp end comp ,a.udesc,a.qty,a.gxmc,a.zdmc,a.zddm,a.gysmc,b.complete_qty,b.item_class,b.item_type,b.replace_flag,b.confirm_flag,b.rmes_id from rstbomqd a " 
        //            + " left join data_sn_bom_TEMP b   "
        //            + " on  a.gwmc=b.location_code "
        //            + " and a.gxmc=b.PROCESS_CODE "
        //            + " and replace(a.comp,'#','')=b.item_code  "
        //            + " and b.sn='" + SN + "' and b.plan_code='" + planCode + "' and b.pline_code='"+PlineCode+"' "
        //            + " where a.zddm='" + Station_Code + "' and b.rmes_id is not null order by a.gwmc,a.gxmc  ";
        //    DataTable dt = dataConn.GetTable(sql);
        //    //for (int i = 0; i < dt.Rows.Count; i++)
        //    //{
        //    //    string comp = dt.Rows[i]["COMP"].ToString();
        //    //    comp = comp.Substring(comp.IndexOf("#")+1,comp.Length);
        //    //    dt.Rows[i]["COMP"] = comp ;
        //    //}
        //    dgvBom.DataSource = dt; 
        //    for (int i = 0; i < dgvBom.Rows.Count; i++)
        //    {
        //        SetBomRowStatus(i);
        //    }
        //    //生成防错文件 
        //    string sql1 = "select a.gwmc,a.comp ,a.udesc,a.qty,a.gxmc,a.gysmc from rstbomqd a "
        //                + " where a.zddm='" + Station_Code + "' order by a.gwmc,a.gxmc  ";
        //    DataTable dt1 = dataConn.GetTable(sql1);
        //    PublicClass.Output_DayData_FangCuo(dt1, stationname, SN, productmodel, So);
        //}
        private void InitBomItemList(string stationcode1,string stationname1)
        {
            //根据返修工作站选择的站点加载对应bom
            string fdjxl = dataConn.GetValue("select GET_FDJXL('" + So + "') from dual");
            //获取rstbomqd rstbomts
            SNBomTempFactory.ShowBomDCEC(companyCode, planCode, PlineCode, stationcode1, SN, fdjxl, So, Station_Code);
            //得到当前站点的bom以及装机提示
            RMESEventArgs arg = new RMESEventArgs();
            arg.MessageHead = "SHOWZJTS";
            arg.MessageBody = SN + "^" + stationcode1 + "^" + stationname1;
            SendDataChangeMessage(arg);
            //显示质量警示
            arg.MessageHead = "SHOWQUATS";
            arg.MessageBody = SN + "^" + stationcode1 + "^" + stationname1;
            SendDataChangeMessage(arg);
            //特殊站点装机提示
            if (stationname1 == "ZF130" && SN.Length >= 8)
            {
                string zjts = "";
                zjts = dataConn.GetValue("select PL_QRY_QZDSJ('" + SN + "','Z635','ZF130') from dual");
                arg.MessageHead = "ZJTSADD";
                arg.MessageBody = zjts;
                SendDataChangeMessage(arg);
            }
            //装机图片显示
            if (stationname1 != "ZF245")
            {
                ArrayList wjpath = new ArrayList();
                string wjlj = dataConn.GetValue("select INTERNAL_VALUE FROM CODE_INTERNAL WHERE COMPANY_CODE='" + companyCode
            + "' AND INTERNAL_TYPE_CODE='PATH' AND INTERNAL_CODE='PROCESSNOTEPATH'");
                string sql12 = "select wjpath from rstbomts where zddm='" + stationcode1 + "' and wjpath is not null ";
                DataTable dt12 = dataConn.GetTable(sql12);
                for (int i = 0; i < dt12.Rows.Count; i++)
                {
                    string[] s = dt12.Rows[i][0].ToString().Split('$');
                    for (int j = 0; j < s.Length; j++)
                    {
                        string path1 = wjlj + "\\" + PlineCode + "\\" + s[j];
                        if (File.Exists(path1))
                        {
                            if (!wjpath.Contains(path1))
                            {
                                wjpath.Add(path1);
                            }
                        }
                    }
                }
                if (wjpath.Count > 0)
                {
                    FrmShowProcessPic ps = new FrmShowProcessPic();
                    ps.StartPosition = FormStartPosition.Manual;
                    ps.Location = new Point(0, 400);
                    ps.Show();
                }
                //arg.MessageHead = "SHOWPICTURE";
                //arg.MessageBody = SN;
                //SendDataChangeMessage(arg);
            }
            dgvBom.DataSource = null;
            //20160928修改 改为采用原逻辑 不关联sn_bom_temp表
            //string sql = "select a.gwmc, case when instr(a.comp,'#')=1 then substr(a.comp,2) else a.comp end comp ,a.udesc,a.qty,a.gxmc,b.station_name zdmc,b.station_code zddm,a.gysmc,nvl(b.complete_qty,a.qty) complete_qty,func_get_itemclass((select pline_code from vw_code_station where station_name=zdmc and rownum=1),'','',comp) item_class,b.item_type,b.replace_flag,b.confirm_flag,b.rmes_id from rstbomqd a "
            //        + " left join data_sn_bom_TEMP b   "
            //        + " on  a.gwmc=b.location_code "
            //        + " and a.gxmc=b.PROCESS_CODE "
            //        + " and replace(a.comp,'#','')=b.item_code  "
            //        + " and b.sn='" + SN + "' and b.plan_code='" + planCode + "' and b.pline_code='" + PlineCode + "' "
            //        + " where a.zddm='" + stationcode1 + "'  order by a.gwmc,a.gxmc  "; //and b.rmes_id is not null

            ////modify by thl 20161026 关联temp表 显示历史记录
            //string sql = "select a.gwmc, case when instr(a.comp,'#')=1 then substr(a.comp,2) else a.comp end comp ,a.udesc,a.qty,a.gxmc,b.station_name zdmc,b.station_code zddm,a.gysmc,0 complete_qty,func_get_itemclass((select pline_code from vw_code_station where station_name=zdmc and rownum=1),'','',comp) item_class,FUNC_GET_ITEMTYPE('" + PlineCode + "','" + stationcode1 + "',a.gwmc,replace(a.comp,'#','')) item_type,b.replace_flag,'N' confirm_flag,b.rmes_id from rstbomqd a "
            //    + " left join data_sn_bom_TEMP b   "
            //    + " on  a.gwmc=b.location_code "
            //    + " and a.gxmc=b.PROCESS_CODE "
            //    + " and replace(a.comp,'#','')=b.item_code  "
            //    + " and b.sn='" + SN + "' and b.plan_code='" + planCode + "' and b.pline_code='" + PlineCode + "' "
            //    + " where a.zddm='" + stationcode1 + "'  order by a.gwmc,a.gxmc  ";//and b.rmes_id is not null

            //modify by thl 20161026 关联temp表 显示历史记录  20161101注释 改为旧vb逻辑
            string sql = "select a.gwmc, case when instr(a.comp,'#')=1 then substr(a.comp,2) else a.comp end comp ,a.udesc,a.qty,a.gxmc,b.station_name zdmc,b.station_code zddm,a.gysmc,nvl(b.complete_qty,a.qty) complete_qty,func_get_itemclass((select pline_code from vw_code_station where station_name=zdmc and rownum=1),'',a.gwmc,comp) item_class,FUNC_GET_ITEMTYPE('" + PlineCode + "','" + stationcode1 + "',a.gwmc,replace(a.comp,'#','')) item_type,case when instr(a.comp,'#')=1 then 'A' else 'N' end replace_flag,b.confirm_flag,b.rmes_id from rstbomqd a "
                + " left join data_sn_bom_TEMP b   "
                + " on  a.gwmc=b.location_code "
                + " and a.gxmc=b.PROCESS_CODE "
                + " and replace(a.comp,'#','')=b.item_code  "
                + " and b.sn='" + SN + "' and b.plan_code='" + planCode + "' and b.pline_code='" + PlineCode + "' left join vw_rel_station_location d on a.gwmc=d.location_code "
                + " where a.zddm='" + stationcode1 + "' and d.station_code='" + stationcode1 + "' order by d.location_flag,a.gwmc,a.gxmc,replace(a.comp,'#','')  ";//and b.rmes_id is not null
            DataTable dt = dataConn.GetTable(sql);
            //string sql = "select a.gwmc, case when instr(a.comp,'#')=1 then substr(a.comp,2) else a.comp end comp ,a.udesc,a.qty,a.gxmc,a.zdmc,a.zddm,a.gysmc,0 complete_qty,func_get_itemclass((select pline_code from vw_code_station where station_name=a.zdmc and rownum=1),'','',comp) item_class,FUNC_GET_ITEMTYPE('" + PlineCode + "','" + stationcode1 + "',a.gwmc,replace(a.comp,'#','')) item_type,b.replace_flag,'N' confirm_flag,b.rmes_id from rstbomqd a "
            //    + " left join data_sn_bom_TEMP b   "
            //    + " on  a.gwmc=b.location_code "
            //    + " and a.gxmc=b.PROCESS_CODE "
            //    + " and replace(a.comp,'#','')=b.item_code  "
            //    + " and b.sn='" + SN + "' and b.plan_code='" + planCode + "' and b.pline_code='" + PlineCode + "' left join vw_rel_station_location d on a.gwmc=d.location_code "
            //    + " where a.zddm='" + stationcode1 + "' and d.station_code='" + stationcode1 + "' order by d.location_flag,a.gwmc,a.gxmc,replace(a.comp,'#','')  ";//and b.rmes_id is not null
            //DataTable dt = dataConn.GetTable(sql);
            //for (int i = 0; i < dt.Rows.Count; i++)
            //{
            //    string comp = dt.Rows[i]["COMP"].ToString();
            //    comp = comp.Substring(comp.IndexOf("#")+1,comp.Length);
            //    dt.Rows[i]["COMP"] = comp ;
            //}
            dgvBom.DataSource = dt;
            for (int i = 0; i < dgvBom.Rows.Count; i++)
            {
                try
                {
                    if (dgvBom.Rows[i].Cells["colItemType"].Value.ToString() == "A" || dgvBom.Rows[i].Cells["colItemType"].Value.ToString() == "B" || dgvBom.Rows[i].Cells["colItemType"].Value.ToString() == "D")
                        IsABC = true;
                }
                catch
                { }
                SetBomRowStatus(i);
            }
            //生成防错文件 
            string sql1 = "select a.gwmc,a.comp ,a.udesc,a.qty,a.gxmc,a.gysmc from rstbomqd a "
                        + " where a.zddm='" + stationcode1 + "' order by a.gwmc,a.gxmc  ";
            DataTable dt1 = dataConn.GetTable(sql1);
            PublicClass.Output_DayData_FangCuo(dt1, stationname1, SN, productmodel, So);
        }
        private void InitBomItemListLQ(string stationcode1, string stationname1,ProductInfoEntity productnew, ProductInfoEntity productold)
        {
            //根据返修工作站选择的站点加载对应bom
            string fdjxl = dataConn.GetValue("select GET_FDJXL('" + productold.PLAN_SO + "') from dual");
            //获取rstbomqd rstbomts
            SNBomTempFactory.ShowBomDCECLQ(companyCode, productold.PLAN_CODE, productold.PLINE_CODE, stationcode1, SN, fdjxl, productold.PLAN_SO, Station_Code, productnew);
            //得到当前站点的bom以及装机提示
            RMESEventArgs arg = new RMESEventArgs();
            arg.MessageHead = "SHOWZJTS";
            arg.MessageBody = SN + "^" + stationcode1 + "^" + stationname1;
            SendDataChangeMessage(arg);
            //显示质量警示
            arg.MessageHead = "SHOWQUATS";
            arg.MessageBody = SN + "^" + stationcode1 + "^" + stationname1;
            SendDataChangeMessage(arg);
            //特殊站点装机提示
            if (stationname1 == "ZF130" && SN.Length >= 8)
            {
                string zjts = "";
                zjts = dataConn.GetValue("select PL_QRY_QZDSJ('" + SN + "','Z635','ZF130') from dual");
                arg.MessageHead = "ZJTSADD";
                arg.MessageBody = zjts;
                SendDataChangeMessage(arg);
            }
            //装机图片显示
            if (stationname1 != "ZF245")
            {
                ArrayList wjpath = new ArrayList();
                string wjlj = dataConn.GetValue("select INTERNAL_VALUE FROM CODE_INTERNAL WHERE COMPANY_CODE='" + companyCode
            + "' AND INTERNAL_TYPE_CODE='PATH' AND INTERNAL_CODE='PROCESSNOTEPATH'");
                string sql12 = "select wjpath from rstbomts where zddm='" + stationcode1 + "' and wjpath is not null ";
                DataTable dt12 = dataConn.GetTable(sql12);
                for (int i = 0; i < dt12.Rows.Count; i++)
                {
                    string[] s = dt12.Rows[i][0].ToString().Split('$');
                    for (int j = 0; j < s.Length; j++)
                    {
                        string path1 = wjlj + "\\" + productold.PLINE_CODE + "\\" + s[j];
                        if (File.Exists(path1))
                        {
                            if (!wjpath.Contains(path1))
                            {
                                wjpath.Add(path1);
                            }
                        }
                    }
                }
                if (wjpath.Count > 0)
                {
                    FrmShowProcessPic ps = new FrmShowProcessPic("", productold.PLINE_CODE);
                    ps.StartPosition = FormStartPosition.Manual;
                    ps.Location = new Point(0, 400);
                    ps.Show();
                }
            }
            dgvBom.DataSource = null;

            //modify by thl 20161026 关联temp表 显示历史记录  20161101注释 改为旧vb逻辑
            string sql = "select a.gwmc, case when instr(a.comp,'#')=1 then substr(a.comp,2) else a.comp end comp ,a.udesc,a.qty,a.gxmc,b.station_name zdmc,b.station_code zddm,a.gysmc,nvl(b.complete_qty,a.qty) complete_qty,func_get_itemclass('" + productold.PLINE_CODE + "','',a.gwmc,comp) item_class,FUNC_GET_ITEMTYPE('" + productold.PLINE_CODE + "','" + stationcode1 + "',a.gwmc,replace(a.comp,'#','')) item_type,case when instr(a.comp,'#')=1 then 'A' else 'N' end replace_flag,b.confirm_flag,b.rmes_id from rstbomqd a "
                + " left join data_sn_bom_TEMP b   "
                + " on  a.gwmc=b.location_code "
                + " and a.gxmc=b.PROCESS_CODE "
                + " and replace(a.comp,'#','')=b.item_code  "
                + " and b.sn='" + SN + "' and b.plan_code='" + planCode + "' and b.pline_code='" + PlineCode + "' left join vw_rel_station_location d on a.gwmc=d.location_code "
                + " where a.zddm='" + stationcode1 + "' and d.station_code='" + stationcode1 + "' order by d.location_flag,a.gwmc,a.gxmc,replace(a.comp,'#','')  ";//and b.rmes_id is not null
            DataTable dt = dataConn.GetTable(sql);

            dgvBom.DataSource = dt;
            for (int i = 0; i < dgvBom.Rows.Count; i++)
            {
                try
                {
                    if (dgvBom.Rows[i].Cells["colItemType"].Value.ToString() == "A" || dgvBom.Rows[i].Cells["colItemType"].Value.ToString() == "B" || dgvBom.Rows[i].Cells["colItemType"].Value.ToString() == "D")
                        IsABC = true;
                }
                catch
                { }
                SetBomRowStatus(i);
            }
            //生成防错文件 
            string sql1 = "select a.gwmc,a.comp ,a.udesc,a.qty,a.gxmc,a.gysmc from rstbomqd a "
                        + " where a.zddm='" + stationcode1 + "' order by a.gwmc,a.gxmc  ";
            DataTable dt1 = dataConn.GetTable(sql1);
            PublicClass.Output_DayData_FangCuo(dt1, stationname1, SN, productmodel, So);
        }
        private void InitBomItemListFx(string stationcode1, string stationname1)
        {
            //根据返修工作站选择的站点加载对应bom
            string fdjxl = dataConn.GetValue("select GET_FDJXL('" + So + "') from dual");
            //获取rstbomqd rstbomts
            SNBomTempFactory.ShowBomDCEC_CL(companyCode, planCode, PlineCode, stationcode1, SN, fdjxl, So, Station_Code);
            //得到当前站点的bom以及装机提示
            RMESEventArgs arg = new RMESEventArgs();
            arg.MessageHead = "SHOWZJTS";
            arg.MessageBody = SN + "^" + stationcode1 + "^" + stationname1;
            SendDataChangeMessage(arg);
            //显示质量警示
            arg.MessageHead = "SHOWQUATS";
            arg.MessageBody = SN + "^" + stationcode1 + "^" + stationname1;
            SendDataChangeMessage(arg);
            //特殊站点装机提示
            if (stationname1 == "ZF130" && SN.Length >= 8)
            {
                string zjts = "";
                zjts = dataConn.GetValue("select PL_QRY_QZDSJ('" + SN + "','Z635','ZF130') from dual");
                arg.MessageHead = "ZJTSADD";
                arg.MessageBody = zjts;
                SendDataChangeMessage(arg);
            }
            //装机图片显示
            if (stationname1 != "ZF245")
            {
                ArrayList wjpath = new ArrayList();
                string wjlj = dataConn.GetValue("select INTERNAL_VALUE FROM CODE_INTERNAL WHERE COMPANY_CODE='" + companyCode
            + "' AND INTERNAL_TYPE_CODE='PATH' AND INTERNAL_CODE='PROCESSNOTEPATH'");
                string sql12 = "select wjpath from rstbomts where zddm='" + stationcode1 + "' and wjpath is not null ";
                DataTable dt12 = dataConn.GetTable(sql12);
                for (int i = 0; i < dt12.Rows.Count; i++)
                {
                    string[] s = dt12.Rows[i][0].ToString().Split('$');
                    for (int j = 0; j < s.Length; j++)
                    {
                        string path1 = wjlj + "\\" + PlineCode + "\\" + s[j];
                        if (File.Exists(path1))
                        {
                            if (!wjpath.Contains(path1))
                            {
                                wjpath.Add(path1);
                            }
                        }
                    }
                }
                if (wjpath.Count > 0)
                {
                    FrmShowProcessPic ps = new FrmShowProcessPic();
                    ps.StartPosition = FormStartPosition.Manual;
                    ps.Location = new Point(0, 400);
                    ps.Show();
                }
            }
            dgvBom.DataSource = null;
            //modify by thl 20161026 关联temp表 显示历史记录  20161101注释 改为旧vb逻辑
            string sql = "select a.gwmc, case when instr(a.comp,'#')=1 then substr(a.comp,2) else a.comp end comp ,a.udesc,a.qty,a.gxmc,b.station_name zdmc,b.station_code zddm,a.gysmc,nvl(b.complete_qty,a.qty) complete_qty,func_get_itemclass((select pline_code from vw_code_station where station_name=zdmc and rownum=1),'',a.gwmc,comp) item_class,FUNC_GET_ITEMTYPE('" + PlineCode + "','" + stationcode1 + "',a.gwmc,replace(a.comp,'#','')) item_type,case when instr(a.comp,'#')=1 then 'A' else 'N' end replace_flag,b.confirm_flag,b.rmes_id from rstbomqd a "
                + " left join data_sn_bom_TEMP b   "
                + " on  a.gwmc=b.location_code "
                + " and a.gxmc=b.PROCESS_CODE "
                + " and replace(a.comp,'#','')=b.item_code  "
                + " and b.sn='" + SN + "' and b.plan_code='" + planCode + "' and b.pline_code='" + PlineCode + "'  "
                + " where a.zddm='" + stationcode1 + "' order by a.gwmc,a.gxmc,replace(a.comp,'#','')  ";//and b.rmes_id is not null
            DataTable dt = dataConn.GetTable(sql);
            dgvBom.DataSource = dt;
            for (int i = 0; i < dgvBom.Rows.Count; i++)
            {
                try
                {
                    if (dgvBom.Rows[i].Cells["colItemType"].Value.ToString() == "A" || dgvBom.Rows[i].Cells["colItemType"].Value.ToString() == "B" || dgvBom.Rows[i].Cells["colItemType"].Value.ToString() == "D")
                        IsABC = true;
                }
                catch
                { }
                SetBomRowStatus(i);
            }
            //生成防错文件 
            string sql1 = "select a.gwmc,a.comp ,a.udesc,a.qty,a.gxmc,a.gysmc from rstbomqd a "
                        + " where a.zddm='" + stationcode1 + "' order by a.gwmc,a.gxmc  ";
            DataTable dt1 = dataConn.GetTable(sql1);
            PublicClass.Output_DayData_FangCuo(dt1, stationname1, SN, productmodel, So);
        }
        private void InitBomItemList_Show(string stationcode1, string stationname1)
        {
            //根据返修工作站选择的站点加载对应bom
            string fdjxl = dataConn.GetValue("select GET_FDJXL('" + So + "') from dual");
            //获取rstbomqd rstbomts
            SNBomTempFactory.ShowBomDCEC_CL(companyCode, planCode, PlineCode, stationcode1, SN, fdjxl, So, Station_Code);

            dgvBom.DataSource = null;
            //string sql = "select a.gwmc, case when instr(a.comp,'#')=1 then substr(a.comp,2) else a.comp end comp ,a.udesc,a.qty,a.gxmc,b.station_name zdmc,b.station_code zddm,a.gysmc,b.complete_qty,func_get_itemclass((select pline_code from vw_code_station where station_name=zdmc and rownum=1),'','',comp) item_class,b.item_type,b.replace_flag,b.confirm_flag,b.rmes_id from rstbomqd a "
            //        + " left join data_sn_bom_TEMP b   "
            //        + " on  a.gwmc=b.location_code "
            //        + " and a.gxmc=b.PROCESS_CODE "
            //        + " and replace(a.comp,'#','')=b.item_code  "
            //        + " and b.sn='" + SN + "' and b.plan_code='" + planCode + "' and b.pline_code='" + PlineCode + "' "
            //        + " where a.zddm='" + stationcode1 + "'  order by a.gwmc,a.gxmc  "; //and b.rmes_id is not null

            ////modify by thl 20161026 关联temp表 显示历史记录 
            //    string sql = "select a.gwmc, case when instr(a.comp,'#')=1 then substr(a.comp,2) else a.comp end comp ,a.udesc,a.qty,a.gxmc,b.station_name zdmc,b.station_code zddm,a.gysmc,0 complete_qty,func_get_itemclass((select pline_code from vw_code_station where station_name=zdmc and rownum=1),'','',comp) item_class,FUNC_GET_ITEMTYPE('E','','',replace(a.comp,'#','')) item_type,b.replace_flag,'N' confirm_flag,b.rmes_id from rstbomqd a "
        //+ " left join data_sn_bom_TEMP b   "
        //+ " on  a.gwmc=b.location_code "
        //+ " and a.gxmc=b.PROCESS_CODE "
        //+ " and replace(a.comp,'#','')=b.item_code  "
        //+ " and b.sn='" + SN + "' and b.plan_code='" + planCode + "' and b.pline_code='" + PlineCode + "' "
        //+ " where a.zddm='" + stationcode1 + "'  order by a.gwmc,a.gxmc  "; //and b.rmes_id is not null

            //modify by thl 20161026 关联temp表 显示历史记录 20161101注释 改为旧vb逻辑
            string sql = "select a.gwmc, case when instr(a.comp,'#')=1 then substr(a.comp,2) else a.comp end comp ,a.udesc,a.qty,a.gxmc,b.station_name zdmc,b.station_code zddm,a.gysmc,nvl(b.complete_qty,a.qty) complete_qty,func_get_itemclass((select pline_code from vw_code_station where station_name=zdmc and rownum=1),'','',comp) item_class,FUNC_GET_ITEMTYPE('" + PlineCode + "','" + stationcode1 + "',a.gwmc,replace(a.comp,'#','')) item_type,b.replace_flag,b.confirm_flag,b.rmes_id from rstbomqd a "
                + " left join data_sn_bom_TEMP b   "
                + " on  a.gwmc=b.location_code "
                + " and a.gxmc=b.PROCESS_CODE "
                + " and replace(a.comp,'#','')=b.item_code  "
                + " and b.sn='" + SN + "' and b.plan_code='" + planCode + "' and b.pline_code='" + PlineCode + "' "
                + " where a.zddm='" + stationcode1 + "'  order by a.gwmc,a.gxmc,replace(a.comp,'#','')  "; //and b.rmes_id is not null
            DataTable dt = dataConn.GetTable(sql);
            //string sql = "select a.gwmc, case when instr(a.comp,'#')=1 then substr(a.comp,2) else a.comp end comp ,a.udesc,a.qty,a.gxmc,a.zdmc,a.zddm,a.gysmc,0 complete_qty,func_get_itemclass((select pline_code from vw_code_station where station_name=zdmc and rownum=1),'','',comp) item_class,FUNC_GET_ITEMTYPE('" + PlineCode + "','" + stationcode1 + "',a.gwmc,replace(a.comp,'#',''))  item_type,b.replace_flag,'N' confirm_flag,b.rmes_id from rstbomqd a "
            //    + " left join data_sn_bom_TEMP b   "
            //    + " on  a.gwmc=b.location_code "
            //    + " and a.gxmc=b.PROCESS_CODE "
            //    + " and replace(a.comp,'#','')=b.item_code  "
            //    + " and b.sn='" + SN + "' and b.plan_code='" + planCode + "' and b.pline_code='" + PlineCode + "' "
            //    + " where a.zddm='" + stationcode1 + "'  order by a.gwmc,a.gxmc,replace(a.comp,'#','')  "; //and b.rmes_id is not null
            //DataTable dt = dataConn.GetTable(sql);
            dgvBom.DataSource = dt;
            for (int i = 0; i < dgvBom.Rows.Count; i++)
            {
                SetBomRowStatus(i);
            }
        }

        private void InitBomItemList_CL(string stationcode1, string stationname1)
        {
            //根据返修工作站选择的站点加载对应bom
            string fdjxl = dataConn.GetValue("select GET_FDJXL('" + So + "') from dual");
            //获取rstbomqd rstbomts
            SNBomTempFactory.ShowBomDCEC_CL(companyCode, planCode, PlineCode, stationcode1, "", fdjxl, So, Station_Code);
            //得到当前站点的bom以及装机提示
            RMESEventArgs arg = new RMESEventArgs();
            arg.MessageHead = "SHOWZJTS";
            arg.MessageBody = "" + "^" + stationcode1 + "^" + stationname1;
            SendDataChangeMessage(arg);

            dgvBom.DataSource = null;
            string sql = "select a.gwmc, case when instr(a.comp,'#')=1 then substr(a.comp,2) else a.comp end comp ,a.udesc,a.qty,a.gxmc,'' zdmc,'' zddm,a.gysmc,'' complete_qty,'' item_class,'' item_type,'' replace_flag,'' confirm_flag,'' rmes_id from rstbomqd a "
                    + " where a.zddm='" + stationcode1 + "'  order by a.gwmc,a.gxmc,replace(a.comp,'#','')  "; //and b.rmes_id is not null
            DataTable dt = dataConn.GetTable(sql);
            dgvBom.DataSource = dt;
            for (int i = 0; i < dgvBom.Rows.Count; i++)
            {
                SetBomRowStatus(i);
            }
        }

        void ctrlShowBom_RmesDataChanged(object obj, Rmes.WinForm.Base.RMESEventArgs e)
        {
            RMESEventArgs arg = new RMESEventArgs();
            arg.MessageHead = "";
            arg.MessageBody = "";
            if (e.MessageHead == null) return;
            //if (e.MessageHead == "MESLL")
            //{
            //    this.Visible = false;
            //    return;
            //}
            //if (e.MessageHead == "SHOWBOM")
            //{
            //    this.Visible = true;
            //}
            //if (e.MessageHead.ToString() == "WORK" || e.MessageHead.ToString() == "QUA")
            //{
            //    this.Visible = true;
            //    return;
            //}
            if (e.MessageHead.Equals("SN"))
            {
                string sn = e.MessageBody as string;
                ProductInfoEntity product = ProductInfoFactory.GetByCompanyCodeSNSingle(LoginInfo.CompanyInfo.COMPANY_CODE, sn);
                if (product == null) return;
                planCode = product.PLAN_CODE;
                orderCode = product.ORDER_CODE;
                productmodel = product.PRODUCT_MODEL;
                So = product.PLAN_SO;
                SN = product.SN;
                groupBox1.BackColor = System.Drawing.SystemColors.Control;
                dgvBom.BackgroundColor = System.Drawing.SystemColors.AppWorkspace;
                //SNBomTempFactory.InitSnBomTemp(companyCode, planCode,stationCode, SN);
                //InitBomItemList();
                //dgvBom.ClearSelection();
            }
            //else if (e.MessageHead.Equals("PLAN"))
            //{
            //    planCode = e.MessageBody as string;
            //    PlanEntity plan = PlanFactory.GetByKey(planCode);
            //    if (plan == null) return;

            //    orderCode = plan.ORDER_CODE;
            //    So = plan.PLAN_SO;

            //    SN = plan.PLAN_BATCH;

            //    SNBomTempFactory.InitSnBomTemp(companyCode, planCode, stationCode, SN);
            //    InitBomItemList();
            //    //dgvBom.DataSource = SNBomFactory.ShowBom(companyCode, planCode, plineCode, stationCode);

            //    dgvBom.ClearSelection();
            //}
            else if (e.MessageHead.Equals("SHOWBOM"))
            {
                SN = e.MessageBody as string;
                groupBox1.BackColor = System.Drawing.SystemColors.Control;
                dgvBom.BackgroundColor = System.Drawing.SystemColors.AppWorkspace;
                ProductInfoEntity product = ProductInfoFactory.GetByCompanyCodeSNSingle(LoginInfo.CompanyInfo.COMPANY_CODE, SN);
                if (product == null) return;
                planCode = product.PLAN_CODE;
                productmodel = product.PRODUCT_MODEL;
                So = product.PLAN_SO;
                stationcode_fx = Station_Code;
                stationname_fx = stationname;
                IsABC = false;
                InitBomItemList(Station_Code, stationname);
                dgvBom.ClearSelection();
                if (dgvBom.Rows.Count > 0 && IsABC)
                {
                    PlanSnFactory.InitStationControl(companyCode, Plineid, stationCode, product.PLAN_CODE, product.SN, "Rmes.WinForm.Controls.ctrlShowBom");
                    //dataConn.ExeSql("update data_sn_controls_complete set complete_flag='A' where station_code='" + stationCode + "' and control_name='Rmes.WinForm.Controls.ctrlShowBom' and sn='" + product.SN + "' and plan_code='" + product.PLAN_CODE + "' ");
                }
                if (stationtype != "ST05")
                {
                    SendBomConfirm2SN();
                }
                //arg.MessageHead = "SHOWBOMOK";
                //arg.MessageBody = SN;
                //SendDataChangeMessage(arg);
            }
            else if (e.MessageHead.Equals("SHOWBOMLQ"))
            {
                SN = e.MessageBody as string;
                groupBox1.BackColor = System.Drawing.SystemColors.Control;
                dgvBom.BackgroundColor = System.Drawing.SystemColors.AppWorkspace;
                ProductInfoEntity product = ProductInfoFactory.GetByCompanyCodeSNSingle(LoginInfo.CompanyInfo.COMPANY_CODE, SN);
                if (product == null) return;
                string oldplancode = "";
                oldplancode = dataConn.GetValue("select jhmcold from atpujhsn where jhmc='"+product.PLAN_CODE+"'  and is_valid='N' and rownum=1");
                if (oldplancode == "")
                    return;

                ProductInfoEntity productold = ProductInfoFactory.GetByCompanyCodeSNSingleLQ(LoginInfo.CompanyInfo.COMPANY_CODE, SN, oldplancode);
                planCode = product.PLAN_CODE;
                productmodel = product.PRODUCT_MODEL;
                So = product.PLAN_SO;
                //柳汽取对应的原计划的信息
                
                stationcode_fx = Station_Code;
                stationname_fx = stationname;
                IsABC = false;
                InitBomItemListLQ(Station_Code, stationname, product, productold);
                dgvBom.ClearSelection();
                if (dgvBom.Rows.Count > 0 && IsABC)
                {
                    PlanSnFactory.InitStationControl(companyCode, Plineid, stationCode, product.PLAN_CODE, product.SN, "Rmes.WinForm.Controls.ctrlShowBom");
                    //dataConn.ExeSql("update data_sn_controls_complete set complete_flag='A' where station_code='" + stationCode + "' and control_name='Rmes.WinForm.Controls.ctrlShowBom' and sn='" + product.SN + "' and plan_code='" + product.PLAN_CODE + "' ");
                }
                if (stationtype != "ST05")
                {
                    SendBomConfirm2SN();
                }
                //arg.MessageHead = "SHOWBOMOK";
                //arg.MessageBody = SN;
                //SendDataChangeMessage(arg);
            }
            else if (e.MessageHead.Equals("CL_PLAN"))
            {
                //按计划显示bom
                string[] txt = e.MessageBody.ToString().Split('^');
                string Cl_plan = txt[0];
                string Cl_so = txt[1];
                string Cl_pmodel = txt[2];

                planCode = Cl_plan;
                productmodel = Cl_pmodel;
                So = Cl_so;
                stationcode_fx = Station_Code;
                stationname_fx = stationname;
                InitBomItemList_CL(Station_Code, stationname);
                dgvBom.ClearSelection();

                //arg.MessageHead = "SHOWBOMOK";
                //arg.MessageBody = SN;
                //SendDataChangeMessage(arg);
            }
            else if (e.MessageHead.Equals("SHOWBOMFX1"))
            {
                SN = e.MessageBody as string;
                groupBox1.BackColor = System.Drawing.SystemColors.Control;
                dgvBom.BackgroundColor = System.Drawing.SystemColors.AppWorkspace;
                ProductInfoEntity product = ProductInfoFactory.GetByCompanyCodeSNSingle(LoginInfo.CompanyInfo.COMPANY_CODE, SN);
                if (product == null) return;
                planCode = product.PLAN_CODE;
                productmodel = product.PRODUCT_MODEL;
                So = product.PLAN_SO;
                stationcode_fx = Station_Code;
                stationname_fx = stationname;
                IsABC = false;
                InitBomItemListFx(Station_Code, stationname);
                dgvBom.ClearSelection();
                //if (dgvBom.Rows.Count > 0 && IsABC)
                //{
                //    PlanSnFactory.InitStationControl(companyCode, Plineid, stationCode, product.PLAN_CODE, product.SN, "Rmes.WinForm.Controls.ctrlShowBom");
                //    dataConn.ExeSql("update data_sn_controls_complete set complete_flag='A' where station_code='" + stationCode + "' and control_name='Rmes.WinForm.Controls.ctrlShowBom' and sn='" + product.SN + "' and plan_code='" + product.PLAN_CODE + "' ");
                //}
                //if (stationtype != "ST05")
                //{
                //    SendBomConfirm2SN();
                //}
                //arg.MessageHead = "SHOWBOMOK";
                //arg.MessageBody = SN;
                //SendDataChangeMessage(arg);
            }
            else if (e.MessageHead.Equals("SHOWBOM_FX"))
            {
                string item_info = e.MessageBody.ToString();//消息体是sn^stationcode^stationname
                string[] cmd_info = item_info.Split('^');
                SN = cmd_info[0];
                stationcode_fx = cmd_info[1];
                stationname_fx = cmd_info[2];
                ProductInfoEntity product = ProductInfoFactory.GetByCompanyCodeSNSingle(LoginInfo.CompanyInfo.COMPANY_CODE, SN);
                if (product == null) return;
                planCode = product.PLAN_CODE;
                productmodel = product.PRODUCT_MODEL;
                So = product.PLAN_SO;
                InitBomItemList(stationcode_fx, stationname_fx);
                dgvBom.ClearSelection();
                groupBox1.BackColor = System.Drawing.SystemColors.Control;
                dgvBom.BackgroundColor = System.Drawing.SystemColors.AppWorkspace;
                //SendBomConfirm2SN();
            }
            else if (e.MessageHead.ToString() == "ITEM")
            {
                string item_info = e.MessageBody.ToString();//消息体是四段码 最后是扫描的内容
                string[] cmd_info = item_info.Split('^');
                string item_code = cmd_info[0];
                string vendor_code = cmd_info[1];
                string batch_code = cmd_info[2];
                string smcontent = cmd_info[3];
                for (int i = 4; i < cmd_info.Length; i++)
                {
                    smcontent = smcontent +"^"+ cmd_info[i];
                }

                groupBox1.BackColor = System.Drawing.SystemColors.Control;
                dgvBom.BackgroundColor = System.Drawing.SystemColors.AppWorkspace;
                BomItemConfirm("^",item_code, vendor_code, batch_code,"",smcontent);
            }
            else if (e.MessageHead.ToString() == "FAULTITEM")
            {
                groupBox1.BackColor = Color.Red;
                dgvBom.BackgroundColor = Color.Red;
            }
            else if (e.MessageHead == "SCP" || e.MessageHead == "OFFLINE") //下线处理data_sn_bom
            {
                //modify by thl 20161101 下线处理清空BOM  暂时取消，会导致现场看不到BOM清单
                //this.dgvBom.DataSource = null;
                //modify by thl 20161212 下线点对应的操作改到发动机第一次入库处执行
                if (LoginInfo.ProductLineInfo.PLINE_CODE == "CL")
                {
                    ProductDataFactory.BomControlComplete(companyCode, PlineCode, Station_Code, planCode, SN);
                    ProductDataFactory.QualityControlComplete(companyCode, PlineCode, Station_Code, planCode, SN);
                }
            }
            else if (e.MessageHead == "SUBCHECK")
            {

            }
            else if (e.MessageHead == "REFRESHPLAN") //如果不存在需要扫描的零件 更新零件信息和控件状态
            {

            }
            //else if (e.MessageHead == "PAUSE")
            //{
            //    this.dgvBom.DataSource = null;
            //    return;
            //}
            //else if (e.MessageHead == "BATCHQTY" || e.MessageHead == "VITEM")
            //{
            //    VItemCode = e.MessageBody.ToString();
            //    InitBomItemList();
            //}
        }

        private void dgvBom_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //if (e.RowIndex < 0) return;

            //if (dgvBom.Columns[e.ColumnIndex].Name != "btnConfirmFlag") return;
            //string confirm_flag = dgvBom.Rows[e.RowIndex].Cells["colConfirmFlag"].Value.ToString();
            //if (confirm_flag != "N")
            //{
            //    MessageBox.Show("此项BOM物料已经确认！");
            //    return;
            //}

            //string strItemQty = dgvBom.Rows[e.RowIndex].Cells["colQty"].Value.ToString();
            //string RmesID = dgvBom.Rows[e.RowIndex].Cells["colRmesID"].Value.ToString();
            //string item_code=dgvBom.Rows[e.RowIndex].Cells["colItemCode"].Value.ToString();
            
            //object obj_vendor_code = dgvBom.Rows[e.RowIndex].Cells["colVendorCode"].Value;
            //object obj_batch_code = dgvBom.Rows[e.RowIndex].Cells["colItemBatch"].Value;
            //string item_class_code = dgvBom.Rows[e.RowIndex].Cells["colItemClassCode"].Value == null ? 
            //                         "" : dgvBom.Rows[e.RowIndex].Cells["colItemClassCode"].Value.ToString();
            //float complete_qty = float.Parse(strItemQty);
            //string vendor_code = obj_vendor_code == null ? "" : obj_vendor_code.ToString();
            //string batch_code = obj_batch_code == null ? "" : obj_batch_code.ToString();
            //if (item_class_code == "A" && obj_batch_code == null)
            //{
            //    frmBomItemEdit frmBomItem = new frmBomItemEdit(RmesID);
            //    frmBomItem.ShowDialog();
            //    return;
            //}
                       
            //SNBomTempFactory.HandleBomItemComplete(companyCode, RmesID, vendor_code, batch_code, complete_qty);
            //LineSideStockFactory.OutOfStorage(item_code, vendor_code, batch_code, LinesideStock, PlineCode, complete_qty);
            //dgvBom.Rows[e.RowIndex].Cells["colCompleteQty"].Value = complete_qty;
            //dgvBom.Rows[e.RowIndex].Cells["colConfirmFlag"].Value = "Y";
            //SetBomRowStatus(e.RowIndex);
            //SendBomConfirm2SN();
        }

        /// <summary>
        /// 更新物料装配情况
        /// </summary>
        /// <param name="RmesID">该参数必须为RmesID或者是“^”符号。为“^”符号时候，根据ITEM_CODE更新数据，否则根据RmesID更新--注意，有时候ITEM_CODE可能重复，因此有此用法</param>
        /// <param name="ItemCode">物料编号</param>
        /// <param name="VendorCode">供应商代码</param>
        /// <param name="BatchCode">批次号</param>
        /// <param name="CompleteQty">数量</param>
        private void BomItemConfirm(string RmesID,string ItemCode,string VendorCode,string BatchCode,string CompleteQty,string barcode)
        {
            //扫描零件 判断录入
            string dgv_item_code = "",dgv_item_name = "", rmes_id = "", vendor_code = "", confirm_flag = "", replaceflag = "", item_type = "C", locationcode = "", processcode = "", stationcode = "", stationname = "";
            float complete_qty = 0,item_qty=0;
            object obj_vendor_code, obj_item_type;
                      
            for (int i = 0; i < dgvBom.Rows.Count; i++)
            {
                rmes_id = (dgvBom.Rows[i].Cells["colRmesid"].Value == null) ? "" : dgvBom.Rows[i].Cells["colRmesid"].Value.ToString();
                if (rmes_id == "" || rmes_id == null)
                    continue;
                dgv_item_code = (dgvBom.Rows[i].Cells["colItemCode"].Value == null) ? "" : dgvBom.Rows[i].Cells["colItemCode"].Value.ToString();
                dgv_item_name = (dgvBom.Rows[i].Cells["colItemName"].Value == null) ? "" : dgvBom.Rows[i].Cells["colItemName"].Value.ToString();
                confirm_flag = (dgvBom.Rows[i].Cells["colConfirmFlag"].Value == null) ? "" : dgvBom.Rows[i].Cells["colConfirmFlag"].Value.ToString();
                locationcode = (dgvBom.Rows[i].Cells["colGwmc"].Value == null) ? "" : dgvBom.Rows[i].Cells["colGwmc"].Value.ToString();
                processcode = (dgvBom.Rows[i].Cells["colGxmc"].Value == null) ? "" : dgvBom.Rows[i].Cells["colGxmc"].Value.ToString();
                stationcode = (dgvBom.Rows[i].Cells["colzddm"].Value == null) ? "" : dgvBom.Rows[i].Cells["colzddm"].Value.ToString();
                stationname = (dgvBom.Rows[i].Cells["colZdmc"].Value == null) ? "" : dgvBom.Rows[i].Cells["colZdmc"].Value.ToString();

                obj_vendor_code = dgvBom.Rows[i].Cells["colGys"].Value;
                if (obj_vendor_code == null) vendor_code = ""; else vendor_code = obj_vendor_code.ToString();

                //obj_item_class = dgvBom.Rows[i].Cells["colItemClass"].Value;//零件级别 1-5
                obj_item_type = dgvBom.Rows[i].Cells["colItemType"].Value; //零件类别 ABC 
                //if (obj_item_class == null) item_class_code = "0"; else item_class_code = obj_item_class.ToString();
                if (obj_item_type == null) item_type = "C"; else item_type = obj_item_type.ToString();

                replaceflag = dgvBom.Rows[i].Cells["colReplaceFlag"].Value.ToString();

                //“^”符号用来处理特殊的，根据ITEM_CODE来更新的物料，否则就会根据RmesID来更新数据。
                if (ItemCode == dgv_item_code ) //未完全扫描完的零件   、、取消 && confirm_flag != "Y"条件，适用于返修站点对bom的更改  
                {
                        //判断虚拟分装总成零件  虚拟分装总成号^SUB^发动机流水号
                        if (VendorCode == "SUB")
                        {
                            if (item_type == "A")
                            {
                                if (BatchCode != SN) //校验流水号
                                {
                                    MessageBox.Show("分装总成零件对应流水号与当前发动机不一致", "提示");
                                    return;
                                }
                            }
                            else
                            {
                                string plancode1 = dataConn.GetValue("select plan_code from data_plan_sn where sn='"+BatchCode+"' and is_valid='Y' ");
                                if (plancode1 != planCode) //校验计划号
                                {
                                    MessageBox.Show("分装总成零件对应计划与当前发动机不一致", "提示");
                                    return;
                                }
                            }
                        }
                    item_qty = float.Parse(dgvBom.Rows[i].Cells["colQty"].Value.ToString());
                    int num = 0;
                    if (item_type == "A")//如果是流水号管理则弹出物料消耗明细编辑窗口
                    {
                        //校验零件是否重复 
                        complete_qty = float.Parse(dgvBom.Rows[i].Cells["colCompleteQty"].Value.ToString()) + 1;

                        if (complete_qty > item_qty)
                        {
                            CsGlobalClass.QXSJSM = false;
                            //20161103 A类零件扫描数量大于完成数量 则弹出界面操作员选择需要替换的零件
                            frmBomItemEdit frmBomItem = new frmBomItemEdit(rmes_id, VendorCode, BatchCode, "A", barcode);
                            frmBomItem.Show();
                            complete_qty = complete_qty - 1;
                            //if (CsGlobalClass.QXSJSM)
                            //    return;
                            //生成防错文件 Output_DayData
                            //string gcXlh1 = dataConn.GetValue("select product_series from data_product where sn='" + SN + "' and rownum=1");
                            //string gcSaveData1 = SN + ";" + "0" + ";" + "AB类零件防错" + ";" + ItemCode + "^" + VendorCode + "^" + BatchCode + ";" + " " + ";" + stationname + ";" + " " + ";" + PlineCode + ";" + gcXlh1 + ";" + "AB";
                            //PublicClass.Output_DayData(stationname, SN, "sjclb", gcSaveData1);
                            return; 
                            //groupBox1.BackColor = Color.Red;
                            //dgvBom.BackgroundColor = Color.Red;
                            //return;
                        }
                    }
                    else if (item_type == "B") //如果是批量管理的物料
                    {
                        complete_qty = float.Parse(dgvBom.Rows[i].Cells["colQty"].Value.ToString());
                        float complete_qty1 = float.Parse(dgvBom.Rows[i].Cells["colCompleteQty"].Value.ToString());
                        if (complete_qty1 >= complete_qty)//系统已有记录 则提示更新还是插入
                        {
                            CsGlobalClass.QXSJSM = false;
                            if (dataConn.GetValue("select count(1) from data_sn_bom_itembatch where bom_rmes_id='" + rmes_id + "' and item_vendor='" + VendorCode + "' and item_sn='" + BatchCode + "' ").ToString() != "0")
                                return;
                            frmBomItemEdit frmBomItem = new frmBomItemEdit(rmes_id, VendorCode, BatchCode, "B", barcode);
                            frmBomItem.Show();
                            //if (CsGlobalClass.QXSJSM)
                            //    return;
                            //生成防错文件 Output_DayData
                            //string gcXlh1 = dataConn.GetValue("select product_series from data_product where sn='" + SN + "' and rownum=1");
                            //string gcSaveData1 = SN + ";" + "0" + ";" + "AB类零件防错" + ";" + ItemCode + "^" + VendorCode + "^" + BatchCode + ";" + " " + ";" + stationname + ";" + " " + ";" + PlineCode + ";" + gcXlh1 + ";" + "AB";
                            //PublicClass.Output_DayData(stationname, SN, "sjclb", gcSaveData1);
                            return;
                            //DialogResult drt = MessageBox.Show("该零件已记录历史批次，选择'是'新增批次，选择'否'更新批次", "提示", MessageBoxButtons.YesNo);
                            //if (drt == DialogResult.Yes)
                            //{
                            //    num = 1;
                            //}
                        }
                    }
                    else if (item_type == "D")//如果是流水号管理则弹出物料消耗明细编辑窗口
                    {
                        //校验零件是否重复 
                        complete_qty = float.Parse(dgvBom.Rows[i].Cells["colQty"].Value.ToString());
                        float complete_qty1 = float.Parse(dgvBom.Rows[i].Cells["colCompleteQty"].Value.ToString());
                        if (complete_qty1 > 0)
                        {
                            CsGlobalClass.QXSJSM = false;
                            //D类单批次不重复零件 替换
                            frmBomItemEdit frmBomItem = new frmBomItemEdit(rmes_id, VendorCode, BatchCode, "D", barcode);
                            frmBomItem.Show();
                            //if (CsGlobalClass.QXSJSM)
                            //    return;
                            //complete_qty = complete_qty - 1;
                            //生成防错文件 Output_DayData
                            //string gcXlh1 = dataConn.GetValue("select product_series from data_product where sn='" + SN + "' and rownum=1");
                            //string gcSaveData1 = SN + ";" + "0" + ";" + "AB类零件防错" + ";" + ItemCode + "^" + VendorCode + "^" + BatchCode + ";" + " " + ";" + stationname + ";" + " " + ";" + PlineCode + ";" + gcXlh1 + ";" + "AB";
                            //PublicClass.Output_DayData(stationname, SN, "sjclb", gcSaveData1);
                            return;

                            //groupBox1.BackColor = Color.Red;
                            //dgvBom.BackgroundColor = Color.Red;
                            //return;
                        }
                    }
                    //如果是C类以下物料
                    else //if (string.IsNullOrEmpty(CompleteQty))//如果数量为空，则默认为BOM数量就是实际装配数量
                    {
                        //不允许扫描非AB类零件
                        groupBox1.BackColor = Color.Red;
                        dgvBom.BackgroundColor = Color.Red;
                        return;
                        //complete_qty = float.Parse(dgvBom.Rows[i].Cells["colQty"].Value.ToString());
                    }
                    //if (stationname == "Z490" && dgv_item_name == "喷油器")
                    //{
                    //    string trimGs = "";
                    //    trimGs = dataConn.GetValue("select gs from copy_engine_property WHERE so='" + So + "'");
                    //    try
                    //    {
                    //        PublicClass.Output_Trim_File(SN, trimGs, complete_qty.ToString(), barcode.Substring(20).ToUpper());
                    //    }
                    //    catch { }
                    //}

                    //else
                    //{
                    //    if (float.Parse(CompleteQty) > item_qty)
                    //    {
                    //        DialogResult drt = MessageBox.Show("使用数量大于BOM数量，要继续吗？", "提示", MessageBoxButtons.YesNo);
                    //        if (drt == DialogResult.No) return;
                    //    }
                    //    complete_qty = float.Parse(CompleteQty);
                    //}

                    SNBomTempFactory.HandleBomItemComplete(companyCode, rmes_id, ItemCode, VendorCode, BatchCode, complete_qty, stationname,barcode,num);
                    //扣减线边库存
                    //LineSideStockFactory.OutOfStorage(ItemCode, vendor_code, BatchCode, LinesideStock, PlineCode, complete_qty);

                    dgvBom.Rows[i].Cells["colCompleteQty"].Value = complete_qty.ToString();
                    if (complete_qty >= item_qty)
                        dgvBom.Rows[i].Cells["colConfirmFlag"].Value = "Y";
                    //string sql = "select a.gwmc, case when instr(a.comp,'#')=1 then substr(a.comp,2) else a.comp end comp ,a.udesc,a.qty,a.gxmc,a.zdmc,a.zddm,a.gysmc,b.complete_qty,b.item_class,b.item_type,b.replace_flag,b.confirm_flag from rstbomqd a "
                    //        + " left join data_sn_bom_temp b   "
                    //        + " on  a.gwmc=b.location_code "
                    //        + " and a.gxmc=b.PROCESS_CODE "
                    //        + " and a.comp=b.item_code  "
                    //        + " and b.sn='" + SN + "' and b.plan_code='" + planCode + "' and b.pline_code='" + PlineCode + "' "
                    //        + " where a.zddm='" + Station_Code + "' order by a.gwmc,a.gxmc  ";
                    //DataTable dt = dataConn.GetTable(sql);
                    //dgvBom.DataSource = dt; 

                    //生成防错文件 Output_DayData
                    string gcXlh = dataConn.GetValue("select product_series from data_product where sn='" + SN + "' and rownum=1");
                    string gcSaveData = SN + ";" + "0" + ";" + "AB类零件防错" + ";" + ItemCode + "^" + VendorCode + "^" + BatchCode + ";" + " " + ";" + stationname + ";" + " " + ";" + PlineCode + ";" + gcXlh + ";" + "AB";
                    PublicClass.Output_DayData(stationname, SN, "sjclb", gcSaveData);
                    SetBomRowStatus(i);
                    if (stationtype != "ST05")
                    {
                        SendBomConfirm2SN();
                    }
                }
            }
        }

        private void SendBomConfirm2SN()
        {
            bool send2sn=true;
            for (int i = 0; i < dgvBom.Rows.Count; i++)
            {
                string confirm_flag = dgvBom.Rows[i].Cells["colConfirmFlag"].Value.ToString();
                string type1 = dgvBom.Rows[i].Cells["colItemType"].Value.ToString();
                if (confirm_flag != "Y" && (type1 == "A" || type1 == "B" || type1 == "D"))
                {
                    send2sn = false;
                    break;
                }
            }
            if (send2sn)//如果BOM项已全部确认，则发送完工汇报给SN模块
            {
                RMESEventArgs args = new RMESEventArgs();
                args.MessageHead = "CCP";
                args.MessageBody = SN+"^Rmes.WinForm.Controls.ctrlShowBom^B";
                UiFactory.CallDataChanged(this, args);
                if (stationname == "Z100" || stationname == "Z110" || stationname == "Z120")
                {
                    //指定站点先防错 后采集数据
                    args.MessageHead = "FOCUSDETECT";
                    args.MessageBody = "";
                    UiFactory.CallDataChanged(this, args);
                }
            }
        }

        private void SetBomRowStatus(int RowID)
        {
            Color color = Color.White;
            Color color1 = Color.White;

            if (RowID % 2 == 0) //奇数行显示浅蓝色
            {
                color = Color.FromArgb(208, 247, 252);//Color.FromArgb(144,220,238)  Color.FromArgb(144, 220, 238)
            }
            string item_class_code="",item_type="";
            string vendercode = "";
            float ItemQty = 0;
            try
            {
                ItemQty = float.Parse(dgvBom.Rows[RowID].Cells["colQty"].Value.ToString());//零件数量
            }
            catch { }
            float CompleteQty = 0;
            try
            {
                CompleteQty = float.Parse(dgvBom.Rows[RowID].Cells["colCompleteQty"].Value.ToString());//完成数量
            }
            catch
            { }
            string confirm_flag = "N";//确认标识
            if (dgvBom.Rows[RowID].Cells["colConfirmFlag"].Value == null) confirm_flag = "N"; else confirm_flag = dgvBom.Rows[RowID].Cells["colConfirmFlag"].Value.ToString();
            string replace_flag = "N";
            if (dgvBom.Rows[RowID].Cells["colReplaceFlag"].Value == null) replace_flag = "N"; else replace_flag = dgvBom.Rows[RowID].Cells["colReplaceFlag"].Value.ToString();
            //string replace_flag = dgvBom.Rows[RowID].Cells["colReplaceFlag"].Value.ToString();//替换标识

            object vender = dgvBom.Rows[RowID].Cells["colGys"].Value;//供应商
            if (vender == null) vendercode = ""; else vendercode = vender.ToString();

            object obj_item_class = dgvBom.Rows[RowID].Cells["colItemClass"].Value;//零件级别
            if (obj_item_class == null) item_class_code = "0"; else item_class_code = obj_item_class.ToString();

            object obj_item_type= dgvBom.Rows[RowID].Cells["colItemType"].Value;//零件级别
            if (obj_item_type == null) item_type = "C"; else item_type = obj_item_type.ToString();


            switch (item_class_code)
            {
                case "1":
                    color = Color.FromArgb(0, 255, 0);//FromArgb(153, 204, 51, 0)
                    break;
                case "2":
                    color = Color.FromArgb(0, 255, 0);
                    break;
                case "3":
                    color = Color.FromArgb(0, 255, 0);
                    break;
                case "4":
                    color = Color.FromArgb(0, 255, 0);
                    break;
                case "5":
                    color = Color.FromArgb(0, 255, 0);
                    break;
                default :
                    break;
            }
            if(replace_flag=="A")
            {
                color = Color.Yellow;//替换零件显示黄色
            }
            if (replace_flag == "B" || replace_flag == "Y")
            {
                color = Color.Yellow;//临时措施替换零件显示黄色
            }
            if (vendercode != "")
            {
                color = Color.Yellow;//指定供应商零件显示绿色  改为红色
            }
            for (int i = 0; i < dgvBom.Columns.Count; i++)
            {
                dgvBom.Rows[RowID].Cells[i].Style.BackColor = color;
                //dgvBom.Rows[RowID].Cells["colConfirmFlag"].Value = confirm_flag;
            }
            //dgvBom.Rows[RowID].Cells["btnConfirmFlag"].Value = confirm_flag == "N" ? "确认" : "";
            //dgvBom.Rows[RowID].Cells["btnConfirmFlag"].Style.ForeColor = Color.LightGray;
            //color = dgvBom.Rows[RowID].DefaultCellStyle.ForeColor;
            color = dgvBom.Rows[RowID].Cells[0].Style.BackColor;
            color1 = dgvBom.Rows[RowID].Cells[0].Style.BackColor;
            if (confirm_flag == "Y" && item_type != "C" ) //需要录入的零件 第一列颜色为洋红色
            {
                //color = Color.Gray;

                //if (CompleteQty < ItemQty) color = Color.Yellow;
                //else if (CompleteQty == ItemQty) color = Color.FromArgb(255, 0, 255, 0);
                //else color = Color.Red;
            }
            else if (item_type == "A" || item_type == "B" || item_type == "D")
            {
                if (CompleteQty < ItemQty) color = Color.Magenta; //需要录入的零件 第一列颜色为洋红色 Color.FromArgb(255, 0, 255, 0)
            }
            if ((item_type == "A" || item_type == "B") || item_type == "D")//&& (CompleteQty < ItemQty)
            {
                color1 = Color.Magenta; //需要录入的零件 第一列颜色为洋红色 Color.FromArgb(255, 0, 255, 0) 
            }
            dgvBom.Rows[RowID].Cells["colItemCode"].Style.BackColor = color;
            dgvBom.Rows[RowID].Cells["colItemName"].Style.BackColor = color1;
            //dgvBom.Rows[RowID].Cells["colItemCode"].Style.ForeColor = color;
        }

        private void dgvBom_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //if (e.RowIndex < 0) return;
            //string rmes_id = dgvBom.Rows[e.RowIndex].Cells["colRmesID"].Value.ToString();
            //frmBomItemEdit frmBomItem = new frmBomItemEdit(rmes_id);
            //frmBomItem.ShowDialog();
        }

        private void dgvBom_MouseClick(object sender, MouseEventArgs e)
        {
            //if (e.Button.Equals(MouseButtons.Right))
            //    mnuBOM.Show(dgvBom,new Point(e.X, e.Y));
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            timer1.Stop();
            for (int i = 0; i < dgvBom.Rows.Count; i++)
            {
                SetBomRowStatus(i);
            }
            dgvBom.ClearSelection();
        }

       

        //private void msbBatchConfirm_Click(object sender, EventArgs e)
        //{
        //    List<SNBomTempEntity> lst_bom = dgvBom.DataSource as List<SNBomTempEntity>;
        //    List<SNBomTempEntity> ds = (from p in lst_bom
        //                            where p.CONFIRM_FLAG != "Y"
        //                            orderby p.ITEM_CODE
        //                            select p).ToList<SNBomTempEntity>();
        //    frmBomItemMng frmBomItem = new frmBomItemMng(planCode, SN, ds);
        //    frmBomItem.ShowDialog();
        //}

        //private void msbBomNew_Click(object sender, EventArgs e)
        //{
        //    frmBomNew frmBomNew1 = new frmBomNew(planCode,SN, stationCode);
        //    frmBomNew1.ShowDialog();
        //}

       

        

        

    }
}
