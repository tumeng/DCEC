using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using Rmes.WinForm.Base;
using Rmes.DA.Base;
using Rmes.DA.Entity;
using Rmes.DA.Factory;
using Rmes.Pub.Data1;

namespace Rmes.WinForm.Controls
{
    public partial class ctrlQualityDetect :BaseControl
    {
        public string PlanCode = "", SN = "", RmesID = "", PlanSo = "",Fdjxl="",oldvalue="",Gylx="";//
        public string CompanyCode = "", WorkshopCode = "", UserID = "", UserCode = "", PlineCode = "", StationCode = "", PlineID = "", StationID = "", StationName = "", stationcode_fx = "", stationname_fx="";
        //public dataConn dc = new dataConn();
        public ctrlQualityDetect()
        {
            InitializeComponent();
            
            CompanyCode = LoginInfo.CompanyInfo.COMPANY_CODE;
            //WorkshopCode = LoginInfo.WorkShopInfo.WORKSHOP_CODE;
            PlineID = LoginInfo.ProductLineInfo.RMES_ID;
            PlineCode = LoginInfo.ProductLineInfo.PLINE_CODE;
            StationID = LoginInfo.StationInfo.RMES_ID;
            StationCode = LoginInfo.StationInfo.STATION_CODE;
            StationName = LoginInfo.StationInfo.STATION_NAME;
            UserID = LoginInfo.UserInfo.USER_ID;
            UserCode = LoginInfo.UserInfo.USER_CODE;
            GridQuality.AutoGenerateColumns = false;
            GridQuality.RowHeadersVisible = false;
            //InitQualityGrid();
            //InitQuality();
            this.RMesDataChanged += new Rmes.WinForm.Base.RMesEventHandler(ctrQualityCheck_RMesDataChanged);
            //this.Visible = false;

        }
        public ctrlQualityDetect(string sn1)
        {
            //用于检测数据录入
            InitializeComponent();

            CompanyCode = LoginInfo.CompanyInfo.COMPANY_CODE;
            //WorkshopCode = LoginInfo.WorkShopInfo.WORKSHOP_CODE;
            PlineID = LoginInfo.ProductLineInfo.RMES_ID;
            PlineCode = LoginInfo.ProductLineInfo.PLINE_CODE;
            StationID = LoginInfo.StationInfo.RMES_ID;
            StationCode = LoginInfo.StationInfo.STATION_CODE;
            StationName = LoginInfo.StationInfo.STATION_NAME;
            UserID = LoginInfo.UserInfo.USER_ID;
            UserCode = LoginInfo.UserInfo.USER_CODE;
            GridQuality.AutoGenerateColumns = false;
            GridQuality.RowHeadersVisible = false;
            //InitQualityGrid();
            InitQuality(sn1);
            this.RMesDataChanged += new Rmes.WinForm.Base.RMesEventHandler(ctrQualityCheck_RMesDataChanged);
            //this.Visible = false;
            timer1.Start();
        }
        private void InitQuality(string sn1)
        {
            SN = sn1;
            if (SN != "")
            {
                ProductInfoEntity product = ProductInfoFactory.GetByCompanyCodeSNSingle(LoginInfo.CompanyInfo.COMPANY_CODE, SN);
                if (product == null) return;
                PlanCode = product.PLAN_CODE;
                PlanSo = product.PLAN_SO;
                Fdjxl = product.PRODUCT_SERIES;
                Gylx = product.ROUNTING_REMARK;
                stationcode_fx = StationCode;
                stationname_fx = StationName;
                //check中 显示BOM信息和检测数据
                SNDetectTempFactory.InitQualitDetectList(CompanyCode, PlineID, StationID, PlanCode, SN, UserCode, StationID);
                string sql = "select * from data_sn_detect_data_TEMP where station_code='" + StationCode + "' and sn='" + SN + "' and plan_code='" + PlanCode + "' and detect_name!='LJTM' order by location_code,detect_seq ";
                DataTable dt = dataConn.GetTable(sql);

                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.NewRow();
                    dt.Rows.Add(dr);
                }

                this.GridQuality.DataSource = dt;
                ShowQualityList();
                if (GridQuality.Rows.Count > 0)
                {
                    GetFocus(0);
                }
            }
        }
        private void InitQualityGrid()
        {
            DataGridViewComboBoxColumn dgvComboBoxColumn = GridQuality.Columns["colQualVal"] as DataGridViewComboBoxColumn;
            DataTable dt = new DataTable();
            dt.Columns.Add("DisplayField", typeof(string));
            dt.Columns.Add("ValueField", typeof(int));

            
            dt.Rows.Add("不合格", 0);
            dt.Rows.Add("未检测", -1);
            dt.Rows.Add("合格", 1);
            dgvComboBoxColumn.DataSource = dt;
            dgvComboBoxColumn.DisplayMember = "DisplayField";
            dgvComboBoxColumn.ValueMember = "ValueField";


        }

        void ctrQualityCheck_RMesDataChanged(object obj, Rmes.WinForm.Base.RMESEventArgs e)
        {
            string stationID = LoginInfo.StationInfo.RMES_ID;
            RMESEventArgs arg = new RMESEventArgs();
            arg.MessageHead = "";
            arg.MessageBody = "";
            if (e.MessageHead == null) return;
            //if (e.MessageHead == "WORK" || e.MessageHead=="MESLL")
            //    this.Visible = false;
            //else if (e.MessageHead == "QUA")
            //    this.Visible = true;
            //else
            if (e.MessageHead.ToString() == "SN")
            {
                SN = e.MessageBody.ToString();
                ProductInfoEntity product = ProductInfoFactory.GetByCompanyCodeSNSingle(LoginInfo.CompanyInfo.COMPANY_CODE, SN);
                if (product == null) return;
                PlanCode = product.PLAN_CODE;
                PlanSo = product.PLAN_SO;
                Fdjxl = product.PRODUCT_SERIES;
                Gylx = product.ROUNTING_REMARK;
                //this.GridQuality.DataSource = SNDetectTempFactory.GetBySNStation(SN, StationCode);
                //StationEntity station = StationFactory.GetByKey(StationID);
                //List<SNDetectTempEntity> temp = SNDetectTempFactory.GetBySNStation(SN, StationID);
                //foreach (var t in temp)
                //{
                    
                //    int i=GridQuality.Rows.Add();
                //    GridQuality.Rows[i].Cells["colRmesID"].Value = t.RMES_ID;
                //    GridQuality.Rows[i].Cells["colDetectCode"].Value = t.DETECT_ITEM_CODE;
                //    GridQuality.Rows[i].Cells["colDetectDesc"].Value = t.DETECT_ITEM_DESC;
                //    GridQuality.Rows[i].Cells["colQuanVal"].Value = t.DETECT_QUAN_VALUE;
                //    //GridQuality.Rows[i].Cells["colDetectRequire"].Value = t.QUAN_VALUE_REQUIRE;
                //    //GridQuality.Rows[i].Cells["colImage"].Value = t.IMAGE_FILE;
                //    GridQuality.Rows[i].Cells["colRemark"].Value = t.REMARK;
                //    GridQuality.Rows[i].Cells["colDataUp"].Value = t.MAX_VALUE;
                //    GridQuality.Rows[i].Cells["colDataDown"].Value = t.MIN_VALUE;
                //    GridQuality.Rows[i].Cells["colDetectFlag"].Value = t.DETECT_FLAG;
                //    ////DataGridViewComboBoxCell c = (DataGridViewComboBoxCell)(GridQuality.Rows[i].Cells["colFaultCode"]);
                //    ////List<DetectErrorItemEntity> errors = DetectErrorItemFactory.GetByDetectItemCode(station.WORKUNIT_CODE,t.DETECT_ITEM_CODE);
                //    ////c.DataSource = errors;
                //    ////c.DisplayMember = "ERROR_ITEM_NAME";
                //    ////c.ValueMember = "RMES_ID";

                //}
                //if (GridQuality.Rows.Count > 0)
                //{
                //    RMESEventArgs args = new RMESEventArgs();
                //    args.MessageHead = "QUACTRL";
                //    args.MessageBody = "";
                //    SendDataChangeMessage(args);
                //    ShowQualityList();
                //}
            }
            else if (e.MessageHead.ToString() == "PLAN")
            {
                //PlanCode = e.MessageBody.ToString();
                //SNDetectTempFactory.InitQualitDetectList(CompanyCode, PlineID, StationID, PlanCode, SN, UserID);
                ////this.GridQuality.DataSource = SNDetectTempFactory.GetBySNStation(SN, StationID);
                //StationEntity station = StationFactory.GetByKey(StationID);
                //List<SNDetectTempEntity> temp = SNDetectTempFactory.GetBySNStation(SN, StationID);
                //foreach (var t in temp)
                //{

                //    int i = GridQuality.Rows.Add();
                //    GridQuality.Rows[i].Cells["colRmesID"].Value = t.RMES_ID;
                //    GridQuality.Rows[i].Cells["colDetectCode"].Value = t.DETECT_ITEM_CODE;
                //    GridQuality.Rows[i].Cells["colDetectDesc"].Value = t.DETECT_ITEM_DESC;
                //    GridQuality.Rows[i].Cells["colQuanVal"].Value = t.DETECT_QUAN_VALUE;
                //    //GridQuality.Rows[i].Cells["colDetectRequire"].Value = t.QUAN_VALUE_REQUIRE;
                //    //GridQuality.Rows[i].Cells["colImage"].Value = t.IMAGE_FILE;
                //    GridQuality.Rows[i].Cells["colRemark"].Value = t.REMARK;
                //    GridQuality.Rows[i].Cells["colDataUp"].Value = t.MAX_VALUE;
                //    GridQuality.Rows[i].Cells["colDataDown"].Value = t.MIN_VALUE;
                //    GridQuality.Rows[i].Cells["colDetectFlag"].Value = t.DETECT_FLAG;
                //    //DataGridViewComboBoxCell c = (DataGridViewComboBoxCell)(GridQuality.Rows[i].Cells["colFaultCode"]);
                //    //List<DetectErrorItemEntity> errors = DetectErrorItemFactory.GetByDetectItemCode(station.WORKUNIT_CODE, t.DETECT_ITEM_CODE);
                //    //c.DataSource = errors;
                //    //c.DisplayMember = "ERROR_ITEM_NAME";
                //    //c.ValueMember = "RMES_ID";

                //}
                //if (GridQuality.Rows.Count > 0)
                //{
                //    RMESEventArgs args = new RMESEventArgs();
                //    args.MessageHead = "QUACTRL";
                //    args.MessageBody = "";
                //    SendDataChangeMessage(args);
                //    ShowQualityList();
                //}
            }
            else if (e.MessageHead.ToString() == "FOCUSDETECT")
            {
                //指定站点先防错 后采集数据
                if (GridQuality.Rows.Count > 0)
                {
                    if (StationName == "Z100" || StationName == "Z110" || StationName == "Z120")
                    {
                        GetFocus(0);
                    }
                }
            }
            else if (e.MessageHead == "SCP" || e.MessageHead == "OFFLINE")//下线处理data_sn_detect
            {
                //this.GridQuality.DataSource = null;
                //ProductDataFactory.QualityControlComplete(CompanyCode, PlineCode, StationCode, PlanCode, SN);
            }
            else if (e.MessageHead == "SHOWDETECT")
            {
                SN = e.MessageBody.ToString();
                ProductInfoEntity product = ProductInfoFactory.GetByCompanyCodeSNSingle(LoginInfo.CompanyInfo.COMPANY_CODE, SN);
                if (product == null) return;
                PlanCode = product.PLAN_CODE;
                PlanSo = product.PLAN_SO;
                Fdjxl = product.PRODUCT_SERIES;
                Gylx = product.ROUNTING_REMARK;
                stationcode_fx = StationCode;
                stationname_fx = StationName;
                //check中 显示BOM信息和检测数据
                SNDetectTempFactory.InitQualitDetectList(CompanyCode, PlineID, StationID, PlanCode, SN, UserCode, StationID);
                string sql = "select * from data_sn_detect_data_TEMP where station_code='" + StationCode + "' and sn='" + SN + "' and plan_code='" + PlanCode + "' and detect_name!='LJTM' order by location_code,detect_seq ";
                DataTable dt = dataConn.GetTable(sql);
                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.NewRow();
                    dt.Rows.Add(dr);
                }
                this.GridQuality.DataSource = dt;
                ShowQualityList();
                if (GridQuality.Rows.Count > 0)
                {
                    PlanSnFactory.InitStationControl(CompanyCode, PlineID, StationID, product.PLAN_CODE, product.SN, "Rmes.WinForm.Controls.ctrlQualityDetect");
                    if (StationName == "Z100" || StationName == "Z110" || StationName == "Z120")
                    {
                        //指定站点先防错后采集数据
                    }
                    else
                    {
                        GetFocus(0);
                    }
                }
                else
                {
                    arg.MessageHead = "INIT";
                    arg.MessageBody = "";
                    SendDataChangeMessage(arg);
                }
                SendBomConfirm2SN();
            }
            else if (e.MessageHead == "SHOWDETECTFX1")
            {
                SN = e.MessageBody.ToString();
                ProductInfoEntity product = ProductInfoFactory.GetByCompanyCodeSNSingle(LoginInfo.CompanyInfo.COMPANY_CODE, SN);
                if (product == null) return;
                PlanCode = product.PLAN_CODE;
                PlanSo = product.PLAN_SO;
                Fdjxl = product.PRODUCT_SERIES;
                Gylx = product.ROUNTING_REMARK;
                stationcode_fx = StationCode;
                stationname_fx = StationName;
                //check中 显示BOM信息和检测数据
                //返修站点无需在往temp表中更新数据，在返修发动机上线时已经获取了历史记录，20161106
                //如果需要实时检测数据，则要对检测数据进行删除和新增，在选择对应站点是进行处理
                //SNDetectTempFactory.InitQualitDetectList(CompanyCode, PlineID, StationID, PlanCode, SN, UserCode, StationID);
                string sql = "select * from data_sn_detect_data_TEMP where sn='" + SN + "' and plan_code='" + PlanCode + "' and detect_name!='LJTM' order by location_code,detect_seq ";
                DataTable dt = dataConn.GetTable(sql);

                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.NewRow();
                    dt.Rows.Add(dr);
                }

                this.GridQuality.DataSource = dt;
                ShowQualityList();
                if (GridQuality.Rows.Count > 0)
                {
                    //PlanSnFactory.InitStationControl(CompanyCode, PlineID, StationID, product.PLAN_CODE, product.SN, "Rmes.WinForm.Controls.ctrlQualityDetect");
                    if (StationName == "Z100" || StationName == "Z110" || StationName == "Z120")
                    {
                        //指定站点先防错后采集数据
                    }
                    else
                    {
                        GetFocus(0);
                    }
                }
                else
                {
                    arg.MessageHead = "INIT";
                    arg.MessageBody = "";
                    SendDataChangeMessage(arg);
                }
                //SendBomConfirm2SN();
            }
            else if (e.MessageHead == "SHOWDETECT_FX")
            {
                try
                {
                    //SN = e.MessageBody.ToString();
                    string item_info = e.MessageBody.ToString();//消息体是sn^stationcode^stationname
                    string[] cmd_info = item_info.Split('^');
                    SN = cmd_info[0];
                    stationcode_fx = cmd_info[1];
                    stationname_fx = cmd_info[2];
                    StationEntity ent_st = StationFactory.GetBySTATIONCODE(stationcode_fx);
                    string station_id1 = ent_st.RMES_ID;

                    ProductInfoEntity product = ProductInfoFactory.GetByCompanyCodeSNSingle(LoginInfo.CompanyInfo.COMPANY_CODE, SN);
                    if (product == null) return;
                    PlanCode = product.PLAN_CODE;
                    PlanSo = product.PLAN_SO;
                    Fdjxl = product.PRODUCT_SERIES;
                    Gylx = product.ROUNTING_REMARK;
                    //check中 显示BOM信息和检测数据
                    SNDetectTempFactory.InitQualitDetectList(CompanyCode, PlineID, station_id1, PlanCode, SN, UserCode, StationID);
                    string sql = "select * from data_sn_detect_data_TEMP where station_code='" + stationcode_fx + "' and sn='" + SN + "' and plan_code='" + PlanCode + "' and detect_name!='LJTM' order by location_code,detect_seq ";
                    DataTable dt = dataConn.GetTable(sql);

                    if (dt.Rows.Count > 0)
                    {
                        DataRow dr = dt.NewRow();
                        dt.Rows.Add(dr);
                    }

                    this.GridQuality.DataSource = dt;
                    ShowQualityList();
                    if (GridQuality.Rows.Count > 0)
                    {
                        GetFocus(0);
                    }
                    else
                    {
                        arg.MessageHead = "INIT";
                        arg.MessageBody = "";
                        SendDataChangeMessage(arg);
                    }
                    SendBomConfirm2SN();
                }
                catch
                { }
            }
        }

       
        private string GetGridValue(DataGridView dgv,int rowid, string colname)
        {
            object cell_val = dgv.Rows[rowid].Cells[colname].Value;
            if (cell_val == null) return "";
            else return cell_val.ToString();
        }
        private void GetFocus(int rowid)
        {
            try
            {
                //this.GridQuality.Rows[rowid].Cells["colDetectValue"].Selected = true;
                //SendKeys.Send("+{Home}");
                this.GridQuality.CurrentCell = GridQuality.Rows[rowid].Cells["colDetectValue"];
                GridQuality.Focus();
                GridQuality.BeginEdit(false);
            }
            catch
            {}
        }
        private bool HandleDetectData(int RowID)
        {
            string detectcode = "", detectname = "", remartk = "", rmesid = "", faultcode = "", detectvalue = "", detectstandard = "", detectup = "";
            string detectdown = "", detecttype = "", detectunit = "", detectseq = "", detectflag = "";
            string pl = "";
            bool control_completed = true;
            RMESEventArgs args = new RMESEventArgs();

            if (RowID == -1) return false;

            pl = dataConn.GetValue("select GET_FDJPL('" + PlanSo + "') from dual ");

            detectcode = GetGridValue(GridQuality, RowID, "colDetectCode");
            detectname = GetGridValue(GridQuality, RowID, "colDetectDesc");
            //detectvalue = GetGridValue(GridQuality, RowID, "colDetectValue");
            detectvalue = GridQuality.CurrentCell.EditedFormattedValue.ToString();

            string olddetectvalue = "";
            try
            {
                olddetectvalue = GetGridValue(GridQuality, RowID, "colDetectValue");
            }
            catch
            { }

            detectstandard = GetGridValue(GridQuality, RowID, "colDetectStandard");
            detectup = GetGridValue(GridQuality, RowID, "colDataUp");
            detectdown = GetGridValue(GridQuality, RowID, "colDataDown");
            detecttype = GetGridValue(GridQuality, RowID, "colDetectType");
            detectunit = GetGridValue(GridQuality, RowID, "colDetectUnit");
            detectseq = GetGridValue(GridQuality, RowID, "colDetectSeq");
            detectflag = GetGridValue(GridQuality, RowID, "colDetectFlag");
            rmesid = GetGridValue(GridQuality, RowID, "colRmesID");
            remartk = GetGridValue(GridQuality, RowID, "colRemark");
            GridQuality.Rows[RowID].Cells["colDetectValue"].Value = detectvalue;
            //detecttype 0 计量型，1 计点型，2文本型，3零件型
            if (detectvalue == "") return false;
            if (detecttype == "0")
            {
                try
                {
                    if (detectvalue == "" || Convert.ToDouble(detectvalue) < Convert.ToDouble(detectdown) || Convert.ToDouble(detectvalue) > Convert.ToDouble(detectup))
                    {
                        MessageBox.Show("输入内容" + detectvalue + "错误！超出上下限", "提示");
                        //GridQuality.Rows[RowID].Cells["colDetectDesc"].Style.BackColor = Color.Red;
                        //GetFocus(RowID);
                        return false;
                    }
                }
                catch
                {
                    MessageBox.Show("输入内容格式错误！", "提示");
                    //GridQuality.Rows[RowID].Cells["colDetectDesc"].Style.BackColor = Color.Red;
                    //GetFocus(RowID);
                    return false;
                }
            }
            else if (detecttype == "1") //1 计点型
            {
                if (detectvalue != "1" && detectvalue != "0")
                {
                    MessageBox.Show("0、1限定,输入错误！", "提示");
                    //GridQuality.Rows[RowID].Cells["colDetectDesc"].Style.BackColor = Color.Red;
                    //GetFocus(RowID);
                    return false;
                }
            }
            else //2文本型，3零件型
            {
            }
            if (!checkTm(detectvalue)) //判断录入值的格式 如果只有一个^ 则表示条码格式错误
            {
                MessageBox.Show("扫描条码格式有误！", "提示");
                //GridQuality.Rows[RowID].Cells["colDetectDesc"].Style.BackColor = Color.Red;
                //GetFocus(RowID);
                return false;
            }
            //喷油器二维码扫描验证
            string ljdm = "";
            
            {
                if (StationName == "Z430" && detectname.Contains("喷油器") && ((Fdjxl == "ISD" && (pl == "4.5" || pl == "6.7")) || (Fdjxl == "QSB" && (pl == "4.5" || pl == "6.7"))))
                {
                    string sql = "select a.item_code from data_sn_bom_temp a where a.station_name='ZS20' and a.sn='" + SN + "' and a.item_name='喷油器' and rownum=1";
                    //string sql = "select item_code  from data_plan_standard_bom where plan_code='" + PlanCode + "' and item_name ='喷油器' and rownum=1 ";
                    ljdm = dataConn.GetValue(sql);

                    if (detectvalue == "0" || ljdm.Contains(detectvalue.Substring(13, 7)))
                    {
                    }
                    else
                    {
                        MessageBox.Show("二维码" + detectvalue + "与分装喷油器不符！", "提示");
                        //GridQuality.Rows[RowID].Cells["colDetectDesc"].Style.BackColor = Color.Red;
                        //SendKeys.Send("+{Home}");

                        //GetFocus(RowID);
                        return false;
                    }
                }
                if (StationName == "Z490" && detectname.Contains("ISB-QSB喷油器条码"))
                {
                    string trimGs = "";
                    trimGs = dataConn.GetValue("select gs from copy_engine_property WHERE so='" + PlanSo + "'");
                    if (trimGs == "")
                    {
                        MessageBox.Show("缸数为空，请反馈给BOM维护员", "提示");
                        return false;
                    }
                    //string sql = "select a.item_code from data_sn_bom_temp a where a.station_name='Z490' and a.sn='" + SN + "' and a.item_name='喷油器' and rownum=1";
                    //string sql = "select item_code  from data_plan_standard_bom where plan_code='" + PlanCode + "' and location_code='Z490'  and item_name ='喷油器' and rownum=1 ";
                    string sql = "select comp from rstbomqd where zddm='"+LoginInfo.StationInfo.STATION_CODE+"' and udesc='喷油器' and rownum=1 ";
                    string ljdm11 = dataConn.GetValue(sql);
                    if (detectvalue == "0" || (ljdm11.Contains(detectvalue.Substring(5, 5)) && detectvalue.Length == 49))
                    {
                        try
                        {
                            PublicClass.Output_Trim_File(SN, trimGs, detectname.Substring(detectname.Length - 1, 1), detectvalue.Substring(20).ToUpper());
                        }
                        catch { }
                    }
                    else
                    {
                        MessageBox.Show("二维码" + detectvalue + "内容有误，重新扫描！", "提示");
                        return false;
                    }
                }
            }
            //SendKeys.Send("{End}");	SendKeys.Send("+{Home}");
            //二维码扫描验证
            if (StationName == "ZF155" && detectname == "二维码")
            {
                if (detectvalue == "0" || detectvalue.Contains(SN))
                { }
                else
                {
                    MessageBox.Show("二维码" + detectvalue + "与发动机流水号不匹配！", "提示");
                    //GridQuality.Rows[RowID].Cells["colDetectDesc"].Style.BackColor = Color.Red;
                    //SendKeys.Send("+{Home}");
                    //GetFocus(RowID);
                    return false;
                }
            }
            if (detectname == "分装零件流水号条码")
            {
                if (detectvalue == "0" || detectvalue.Contains(SN))
                { }
                else
                {
                    MessageBox.Show("分装零件流水号条码" + detectvalue + "与发动机流水号不匹配！", "提示");
                    return false;
                }
            }
            //TRIME补偿码校验
            string trimValue = "";
            if (detectname.StartsWith("TRIME"))
            {
                trimValue = checkETrim(detectvalue, "A");
                if (trimValue.Length != 16)
                {
                    MessageBox.Show(trimValue, "提示");
                    //GridQuality.Rows[RowID].Cells["colDetectDesc"].Style.BackColor = Color.Red;
                    //GetFocus(RowID);
                    return false;
                }
                else
                {
                    detectvalue = trimValue;
                }
            }
            string str1 = checkETrim("A", "B");
            if (str1 != "OK")
            {
                MessageBox.Show(str1, "提示");
                //GridQuality.Rows[RowID].Cells["colDetectDesc"].Style.BackColor = Color.Red;
                //GetFocus(RowID);
                return false;
            }
            if (detectvalue == "EXIT" || detectvalue == "" || detectvalue.StartsWith("69") || detectvalue.StartsWith("BC00") || detectvalue.StartsWith("BZ00") || detectvalue == StationName)
            {
                detectvalue = "";
            }
            //验证合法后
            if (detectvalue != "" && olddetectvalue != detectvalue)
            {
                SNDetectTempFactory.HandleDetectData(CompanyCode, rmesid, detectvalue, detectvalue, faultcode, remartk, UserCode, stationcode_fx, stationname_fx);

                GridQuality.Rows[RowID].Cells["colDetectFlag"].Value = "Y";
                GridQuality.Rows[RowID].DefaultCellStyle.BackColor = Color.FromArgb(0, 255, 0);
            }
            for (int i = 0; i < GridQuality.Rows.Count; i++)
            {
                if (GridQuality.Rows[i].Cells["colDetectFlag"].Value.ToString() != "Y" && GridQuality.Rows[i].Cells["colDetectFlag"].Value.ToString() != "")
                {
                    control_completed = false;
                    break;
                }
            }
            //if (control_completed)
            //{
            //    args.MessageHead = "CCP";
            //    args.MessageBody = SN + "^Rmes.WinForm.Controls.ctrlQualityDetect^B";
            //    UiFactory.CallDataChanged(this, args);
            //    return true;
            //}
            //else
            //{
            //    args.MessageHead = "CCP";
            //    args.MessageBody = SN + "^Rmes.WinForm.Controls.ctrlQualityDetect^A";
            //    UiFactory.CallDataChanged(this, args);
            //}
            try
            {
                string gcSaveData1 = SN + ";" + "0" + ";" + detectname + ";" + detectvalue + ";" + detectvalue + ";" + LoginInfo.StationInfo.STATION_NAME + ";" + " " + ";" + LoginInfo.ProductLineInfo.PLINE_CODE + ";" + Gylx + ";" + detectcode;
                //string gcSaveData1 = SN + ";" + "0" + ";" + "AB类零件防错" + ";" + ItemCode + "^" + VendorCode + "^" + BatchCode + ";" + " " + ";" + stationname + ";" + " " + ";" + PlineCode + ";" + gcXlh1 + ";" + "AB";
                PublicClass.Output_DayData(LoginInfo.StationInfo.STATION_NAME, SN, "sjclb", gcSaveData1);
            }
            catch { } 
            return true;
            //GetFocus(RowID + 1);
        }

        private void SendBomConfirm2SN()
        {
            RMESEventArgs args = new RMESEventArgs();
            bool control_completed = true;
            for (int i = 0; i < GridQuality.Rows.Count; i++)
            {
                if (GridQuality.Rows[i].Cells["colDetectFlag"].Value.ToString() != "Y" && GridQuality.Rows[i].Cells["colDetectFlag"].Value.ToString() != "")
                //if (GridQuality.Rows[i].Cells["colDetectFlag"].Value.ToString() != "Y")
                {
                    control_completed = false;
                    break;
                }
            }
            if (control_completed)
            {
                args.MessageHead = "CCP";
                args.MessageBody = SN+"^Rmes.WinForm.Controls.ctrlQualityDetect^B";
                UiFactory.CallDataChanged(this, args);
            }
            else
            {
                args.MessageHead = "CCP";
                args.MessageBody = SN + "^Rmes.WinForm.Controls.ctrlQualityDetect^A";
                UiFactory.CallDataChanged(this, args);
            }
        }

        private string checkETrim(string trimvalue,string flag1)
        {
            try
            {
                if (flag1 == "A")
                {
                    if (trimvalue.Length == 49)
                    {
                        return trimvalue.Substring(20, 30);
                    }
                    if (trimvalue.Length == 30)
                    {
                        return trimvalue;
                    }
                    return "Trim补偿码不合法！";
                }
                else if (flag1 == "B")
                {
                    int z = 0;
                    for (int k = 0; k < GridQuality.Rows.Count; k++)
                    {
                        string str3 = GridQuality.Rows[k].Cells["colDetectDesc"].Value.ToString();
                        if (str3.StartsWith("TRIME"))
                        {
                            z++;
                        }
                    }
                    if (z < 2)
                    {
                        return "OK";
                    }

                    for (int i = 0; i < GridQuality.Rows.Count; i++)
                    {

                        string str1 = GridQuality.Rows[i].Cells["colDetectValue"].Value.ToString();
                        string str11 = GridQuality.Rows[i].Cells["colDetectDesc"].Value.ToString();
                        if (str11.StartsWith("TRIME") && str1!="")
                        {
                            int w = 0;

                            for (int k = i + 1; k < GridQuality.Rows.Count; k++)
                            {
                                string str2 = GridQuality.Rows[k].Cells["colDetectValue"].Value.ToString();
                                string str21 = GridQuality.Rows[k].Cells["colDetectDesc"].Value.ToString();
                                if (str1 == str2 && str21.StartsWith("TRIME") && str2!="")
                                {
                                    w++;
                                }
                            }
                            if (w > 0)
                            {
                                return "Trim补偿码有重复！";
                            }
                        }
                    }
                    return "OK";
                }
                else
                {
                    return "OK";
                }
            }
            catch
            {
                return "ERROR";
            }
        }
        private bool checkTm(string thisTm)
        {
            int s = 0;
            thisTm = thisTm.Replace('|','^');
            for (int i = 0; i < thisTm.Length; i++)
            {
                if (thisTm.Substring(i, 1) == "^")
                {
                    s++;
                }
            }
            if (s == 1)
                return false;
            else
                return true;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return;

            //if (e.ColumnIndex == 0) HandleDetectData(e.RowIndex);
            
        }

        private void ShowQualityList()
        {
               
            for (int j = 0; j < GridQuality.Rows.Count; j++)
            {
                string detect_flag = GetGridValue(GridQuality, j, "colDetectFlag");
                //string qual_val = GetGridValue(GridQuality, j,"colQualVal");
                if (detect_flag != "Y")
                    GridQuality.Rows[j].DefaultCellStyle.BackColor = Color.White;
                else
                    GridQuality.Rows[j].DefaultCellStyle.BackColor = Color.FromArgb(0, 255, 0);
                GridQuality.Rows[j].DefaultCellStyle.ForeColor = Color.FromArgb(0,0,0);
            }
        }

        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //if (GridQuality.Columns[e.ColumnIndex].Name == "colFaulCode")
            //{
            //    if (GridQuality.Rows[e.RowIndex].Cells["colUnitType"].Value.ToString() == "F")
            //    {
            //        OpenFileDialog MyFileDialog = new OpenFileDialog();//创建打开对话框对象
            //        MyFileDialog.ShowDialog();//显示打开对话框
            //        if (MyFileDialog.FileName.Trim() != "")//判断是否选择了文件
            //        {
            //            FileStream FStream = new FileStream(MyFileDialog.FileName, FileMode.Open, FileAccess.Read);//创建FileStream对象
            //            string fileName = MyFileDialog.FileName;
            //            string fileNamesub = MyFileDialog.SafeFileName;
            //            BinaryReader BReader = new BinaryReader(FStream);//创建BinaryReader读取对象
            //            byte[] bytes = BReader.ReadBytes((int)FStream.Length);//读取二进制
            //            GridQuality.Rows[e.RowIndex].Cells["colCurrentValue"].Value = fileNamesub;
            //            //quality.CurrentValue = fileName;
            //            FileBlobEntity BE = new FileBlobEntity()
            //            {
            //                FILE_BLOB = bytes,
            //                FILE_NAME = fileNamesub,
            //                USER_ID = LoginInfo.UserInfo.USER_CODE,
            //                CREAT_TIME = DateTime.Now
            //            };
            //            PetaPoco.Database db = DB.GetInstance();
            //            db.Insert("QMS_FILE_BLOB", "RMES_ID", BE);

            //            //FileStream fs = new FileStream(@fileName, FileMode.CreateNew, FileAccess.Write, FileShare.Read);
            //            //StreamWriter sw = new StreamWriter(fs);
            //            //sw.Write(bytes);
            //            FStream.Close();//关闭数据流
            //        }
            //    }
            //}
        }

        private void GridQuality_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (e.KeyChar != 13)
            //    return;
            //string rmesid = GridQuality.SelectedRows[0].ToString();
            //int rowid = GridQuality.SelectedRows[0].Index;
            //HandleDetectData(rowid);
        }

        private void GridQuality_CellParsing(object sender, DataGridViewCellParsingEventArgs e)
        {
            //try
            //{
            //    int row = GridQuality.CurrentCell.RowIndex;
            //    int column = GridQuality.CurrentCell.ColumnIndex;
            //    string rmesid = GridQuality.Rows[row].Cells[0].ToString();
            //    HandleDetectData(row);
            //}
            //catch(Exception e1)
            //{
            //    MessageBox.Show(e1.Message);
            //}
        }

        private void GridQuality_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            //20160819-1
            //try
            //{
            //    int row = GridQuality.CurrentCell.RowIndex;
            //    int column = GridQuality.CurrentCell.ColumnIndex;
            //    string rmesid = GridQuality.Rows[row].Cells[0].ToString();
            //    HandleDetectData(row);
            //}
            //catch (Exception e1)
            //{
            //    MessageBox.Show(e1.Message);
            //}
            ///////////////////20160819-2
            //GridQuality.Rows[e.RowIndex].Cells["colDetectValue"].Value = "";
            //GetFocus(e.RowIndex);
        }

        private void GridQuality_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            try
            {
                //if (GridQuality.Rows[row].Cells["colDetectFlag"].Value == "Y" && GridQuality.Rows[row].Cells["colDetectValue"].Value == GridQuality.CurrentCell.EditedFormattedValue.ToString())
                //{
                //    return;
                //}
                //if (e.RowIndex == GridQuality.Rows.Count-1 && GridQuality.Rows[row].Cells["colDetectFlag"].Value.ToString() == "Y")
                //{
                //    return;
                //}
                int row = GridQuality.CurrentCell.RowIndex;
                int column = GridQuality.CurrentCell.ColumnIndex;

                //if (GridQuality.Rows[row].Cells["colDetectValue"].Value.ToString() == oldvalue)
                //{
                //    GetFocus(row);
                //    e.Cancel = false;
                //}
                //oldvalue = GridQuality.Rows[row].Cells["colDetectValue"].Value.ToString();

                if (GridQuality.Columns[e.ColumnIndex].Name == "colDetectValue")
                {

                    string rmesid = GridQuality.Rows[row].Cells[0].ToString();
                    if (rmesid != "" && row != (GridQuality.Rows.Count-1))
                    {
                        try
                        {
                            if (GridQuality.CurrentCell.EditedFormattedValue.ToString() == "")
                            {
                                GridQuality.Rows[row].Cells["colDetectValue"].Value = "";
                                GridQuality.Rows[row].Cells["colDetectDesc"].Style.BackColor = Color.Red;
                                GridQuality.Rows[row].Cells["colDetectFlag"].Value = "N";
                                //SendKeys.Send("+{HOME}");
                                //SendKeys.Send("+{DEL}");
                                //SendBomConfirm2SN();
                                RMESEventArgs args = new RMESEventArgs();
                                args.MessageHead = "CCP";
                                args.MessageBody = SN + "^Rmes.WinForm.Controls.ctrlQualityDetect^A";
                                UiFactory.CallDataChanged(this, args);
                                e.Cancel = true;
                                return;
                            }
                        }
                        catch
                        { }
                        if (!HandleDetectData(row))
                        {
                            //GridQuality.ClearSelection();
                            //System.Threading.Thread.Sleep(500);
                            GridQuality.Rows[row].Cells["colDetectValue"].Value = "";
                            GridQuality.Rows[row].Cells["colDetectDesc"].Style.BackColor = Color.Red;
                            GridQuality.Rows[row].Cells["colDetectFlag"].Value = "N";
                            //GetFocus(row);
                            //SendKeys.Send("+{TAB}");
                            //SendKeys.Send("+{TAB}");
                            SendKeys.Send("+{HOME}");
                            SendKeys.Send("+{DEL}");
                            SendBomConfirm2SN();
                            e.Cancel = true;
                            //GridQuality.CancelEdit();
                        }
                        else
                        {

                            GridQuality.Rows[row].Cells["colDetectDesc"].Style.BackColor = Color.FromArgb(0, 255, 0);
                            SendBomConfirm2SN();
                            e.Cancel = false;
                        }
                    }
                }
            }
            catch (Exception e1)
            {
                e.Cancel = true;
                MessageBox.Show(e1.Message);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            ShowQualityList();
            if (GridQuality.Rows.Count > 0)
            {
                if (StationName == "Z100" || StationName == "Z110" || StationName == "Z120")
                { }
                else
                {
                    GetFocus(0);
                    SendKeys.Send("+{TAB}");
                    SendKeys.Send("{TAB}");
                }
            }
        }

       
        
    }
}
