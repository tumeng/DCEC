using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Oracle.DataAccess.Client;
using System.Data.Odbc;
using System.IO;
using Rmes.Public;
using System.Xml;
//using Rmes.DA.Factory;
//using Rmes.DA.Base;
//using Rmes.DA.Procedures;

namespace RMES.MaterialCal
{
    public partial class MatCal : Form
    {
        private string theKfPath = "";
        private string theSfPath = "";
        private string theSfXmlPath = "";
        private string theSfXmlRemotePath = "";
        private string theSfXmlBakPath = "";
        private string theSfTxtPath = "";
        private string theSfTxtRemotePath = "";
        private string theSfTxtBakPath = "";
        private string theSfTxtSupplyPath = "";
        private string theSfTxtSupplyRemotePath = "";
        private string theSfTxtSupplyBakPath = "";
        private string theBarCodeRemotePath = "";
        private string theBarCodeLocalPath = "";
        private string theBarCodeLocalPathBak = "";
        private bool sfManualFlag = false;//手动执行标志位
        private string sfManualPlinecode;
        private dataConn dc = new dataConn();
        
        private dataConn2 dcsrm = new dataConn2();
        private dataHandle dh = new dataHandle();

        public MatCal()
        {
            InitializeComponent();
            //get the path
            //System.Configuration.ConfigurationSettings.AppSettings
            theKfPath = RMES.MaterialCal.Properties.Settings.Default.kf_path;
            theSfPath = RMES.MaterialCal.Properties.Settings.Default.sf_path ;
            theSfXmlPath = RMES.MaterialCal.Properties.Settings.Default.sf_xml_path;
            theSfXmlRemotePath = RMES.MaterialCal.Properties.Settings.Default.sf_xml_remote_path;
            theSfXmlBakPath = RMES.MaterialCal.Properties.Settings.Default.sf_xml_bak_path;
            theSfTxtPath = RMES.MaterialCal.Properties.Settings.Default.sf_txt_path;
            theSfTxtRemotePath = RMES.MaterialCal.Properties.Settings.Default.sf_txt_remote_path;
            theSfTxtBakPath = RMES.MaterialCal.Properties.Settings.Default.sf_txt_bak_path;
            theSfTxtSupplyPath = RMES.MaterialCal.Properties.Settings.Default.sf_txt_supply_path;
            theSfTxtSupplyRemotePath = RMES.MaterialCal.Properties.Settings.Default.sf_txt_supply_remote_path;
            theSfTxtSupplyBakPath = RMES.MaterialCal.Properties.Settings.Default.sf_txt_supply_bak_path;
            theBarCodeRemotePath = RMES.MaterialCal.Properties.Settings.Default.barcode_remotepath;
            theBarCodeLocalPath = RMES.MaterialCal.Properties.Settings.Default.barcode_localpath;
            theBarCodeLocalPathBak = RMES.MaterialCal.Properties.Settings.Default.barcode_localpathbak;

            //初始化状态
            btnHandle.Enabled = false;
            sfManualFlag = false;
            initPline();
        }

        private void MatCal_Load(object sender, EventArgs e)
        {
            txtLog.Text = "系统启动时间：" + System.DateTime.Now.ToString();
        }

        private void initPline() //初始化生产线
        {
            try
            {
                //Writetxt("初始化生产线");
                //lstBoxPline.Items.Clear();
                //剔除qad地点为空的
                //string sql = "select gzdd||'--'||qaddd||'--'||decode(runflag,'1','运行','停止') from ms_jit_control where jitflag='SF' order by gzdd ";
                string sql = "select pline_code||'&'||sap_code||'&'||decode(third_flag,'Y','运行','停止') SHOW1,rowid from code_product_line where trim(sap_code) is not null order by third_flag desc,pline_code ";

                dc.setTheSql(sql);
                lstBoxPline.DisplayMember = "SHOW1";
                lstBoxPline.ValueMember = "rowid";
                lstBoxPline.DataSource = dc.GetTable(sql);
            }
            catch(Exception e1)
            {
                Writetxt(e1.Message);
            }
        }

        private void Writetxt(string txt1)
        {
            if (txtLog.Text != "")
                txtLog.Text = txtLog.Text + (char)13 + (char)10 + txt1;
            else
                txtLog.Text = txtLog.Text + txt1;
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            buttonSave1();
        }
        private void buttonSave1()
        {
            try
            {
                StreamWriter sr;
                string theFileName = System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".txt";

                if (!Directory.Exists("C:\\MatCal"))
                {
                    Directory.CreateDirectory("C:\\MatCal");
                }

                if (File.Exists("C:\\MatCal\\" + theFileName))
                {
                    return;
                }
                else
                {
                    sr = File.CreateText("C:\\MatCal\\" + theFileName);
                }
                sr.WriteLine(txtLog.Text);
                sr.Close();
                txtLog.Text = "";
            }
            catch (Exception e1)
            {
                Writetxt(e1.Message);
            }
        }
        private void txtLog_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtLog.Text.Length > 20000)
                {
                    StreamWriter sr;
                    string theFileName = System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".txt";

                    if (!Directory.Exists("C:\\MatCal"))
                    {
                        Directory.CreateDirectory("C:\\MatCal");
                    }

                    if (File.Exists("C:\\MatCal\\" + theFileName))
                    {
                        return;
                    }
                    else
                    {
                        sr = File.CreateText("C:\\MatCal\\" + theFileName);
                    }
                    sr.WriteLine(txtLog.Text);
                    sr.Close();
                    txtLog.Text = "";
                }
                txtLog.SelectionStart = txtLog.Text.Length;
                txtLog.ScrollToCaret();
            }
            catch (Exception e1)
            {
                Writetxt(e1.Message);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            buttonSave1();
        }

        private void checkBoxHandle_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxHandle.Checked)
            {
                btnHandle.Enabled = true;
                TimerSF.Enabled = false;
            }
            else
            {
                btnHandle.Enabled = false;
                //TimerSF.Enabled = true;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                savelog("", "SF", "中止计算", "");
                TimerSF.Enabled = false;
                //重置标识位
                string sql = "update ms_jit_state set runflag='0' where jitflag='SF'";
                dc.ExeSql(sql);
            }
            catch (Exception e1)
            {
                Writetxt(e1.Message);
            }
        }
        private void savelog(string pline,string jitflag,string jitlog,string jituser)
        {
            string sql = "insert into ms_jit_log(gzdd,jitflag,jitlog,jituser,jittime) values('" + pline + "','" + jitflag + "','" + jitlog + "','" + jituser + "',sysdate) ";
            dc.ExeSql(sql);

            Writetxt(System.DateTime.Now.ToString()+":"+pline+jitlog);
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            try
            {
                if (btnRun.Text == "停止")
                {
                    savelog("", "SF", "中止计算", "");
                    TimerSF.Enabled = false;
                    //重置标识位
                    string sql = "update ms_jit_state set runflag='0' where jitflag='SF'";
                    dc.ExeSql(sql);
                    btnRun.Text = "启动";
                    groupBox1.BackColor = Color.Red;
                }
                else
                    if (btnRun.Text == "启动")
                    {
                        startSFjit();
                        btnRun.Text = "停止";
                        groupBox1.BackColor = Color.Green;
                    }
            }
            catch (Exception e1)
            {
                Writetxt(e1.Message);
            }
        }
        private void startSFjit()
        { 
            if(!TimerSF.Enabled)
            {
                TimerSF.Interval=60000;
                TimerSF.Enabled=true;
            }
            savelog("", "SF", "三方计算启动完毕。", "");
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (TimerSF.Enabled)
            {
                groupBox1.BackColor = Color.Green;
            }
            else
            {
                groupBox1.BackColor = Color.Red;
            }
        }

        private void TimerSF_Tick(object sender, EventArgs e)
        {
            try
            {
                //循环处理各个生产线
                string sfArr;

                for (int i = 0; i < lstBoxPline.Items.Count; i++)
                {
                    DataRowView drv = lstBoxPline.Items[i] as DataRowView;
                    if (drv == null)
                    {
                        continue;
                    }
                    string seltext = drv["SHOW1"].ToString();
                    //string seltext = lstBoxPline.Items[i].ToString();
                    char[] charSeparators1 = new char[] { '&' };
                    string[] array1 = seltext.Split(charSeparators1);
                    string plinecode = array1[0];
                    string qadsite = array1[1];
                    string runflag = array1[2];
                    if (runflag == "运行")
                    {
                        if (CheckSfDd(plinecode))//判断生产线计算参数是否齐全
                        {
                            //判断是否有手动提交请求
                            string sql = "select manualflag from ms_jit_manualflag where gzdd='" + plinecode + "' and jitflag='SF' and manualflag='0' ";
                            dc.setTheSql(sql);
                            if (dc.GetTable(sql).Rows.Count > 0)
                            {
                                sfManualFlag = true;
                                sfManualPlinecode = plinecode;
                            }
                            else
                            {
                                sfManualFlag = false;
                                sfManualPlinecode = "";
                            }
                            //处理生产线请求，并判断是否触发自动计算
                            sF_handle(plinecode, qadsite, sfManualFlag, sfManualPlinecode);
                        }
                    }
                }
            }
            catch (Exception e1)
            {
                Writetxt(e1.Message);
            }
        }
        private bool CheckSfDd(string Pcode)
        {
            try
            {
                string selSql = "";
                if (Pcode == "JJE" || Pcode == "JJW")
                {
                    return false;
                }

                selSql = "select parameter_value from ms_jit_parameter where (parameter_value='' or parameter_value is null) and parameter_code like '$THIRD%' and gzdd='" + Pcode + "'";
                dc.setTheSql(selSql);
                dc.GetTable();
                if (dc.GetTable().Rows.Count > 0)
                {
                    savelog(Pcode, "SF", "JIT参数不完整，不参与本次计算!", "");
                    return false;
                }

                //查JIT计算间隔
                selSql = "select nvl(parameter_value,'0') from ms_jit_parameter where parameter_code='$THIRD_JIT_TIME' and gzdd='" + Pcode + "'";
                dc.setTheSql(selSql);

                int sfJitTime = 0;
                sfJitTime = Convert.ToInt32(dc.GetValue());
                if (sfJitTime == 0)
                {
                    savelog(Pcode, "SF", "JIT参数为0，不参与本次计算!", "");
                    return false;
                }
                return true;
            }
            catch (Exception e1)
            {
                Writetxt(e1.Message);
                return false;
            }
        }
        private void sF_handle(string plinecode,string qadsite,bool manualflag,string manualpcode)
        {
            try
            {
                //三方物流处理流程 20160718
                
                //增加单独要料处理，无条件要料
                sF_Single(plinecode, qadsite,manualpcode);//按计划单独要料
                sF_Single_Mat(plinecode, qadsite,manualpcode);//按物料单独要料

                //若无手动计算，判断是否触发自动计算
                if (manualflag == false || (manualflag == true && manualpcode != plinecode))
                {
                    //查JIT上线台数设置
                    string sql = "select parameter_value from ms_jit_parameter where parameter_code='$THIRD_JIT_ONLINE_NUM' and gzdd='" + plinecode + "'";
                    int sfJitNum = Convert.ToInt32(dc.GetValue(sql));//自动计算上线台数

                    sql = "select * from ms_jit_state where jitflag='SF' and gzdd='" + plinecode + "'";
                    if (dc.GetTable(sql).Rows.Count > 0)//有记录即判断,没记录，则认为是第一次计算，无条件启动计算
                    {
                        sql = "select to_char(jittime,'YYYY-MM-DD HH24:MI:SS') from ms_jit_state where jitflag='SF' and gzdd='" + plinecode + "'";
                        dc.setTheSql(sql);
                        if (!String.IsNullOrEmpty(dc.GetValue()))//判断是否为空，如果为空，仍认为是第一次启动
                        {
                            string sfJitLastTime = dc.GetValue();
                            //查上次计算之后上线台数 大线计划A
                            sql = "select count(sn) from data_product a left join data_plan b on a.plan_code=b.plan_code where a.pline_code='" + plinecode + "' and TO_CHAR(a.work_time,'YYYY-MM-DD HH24:MI:SS')>='" + sfJitLastTime + "' and b.plan_type='A' ";
                            int sfJitCountNum =Convert.ToInt32( dc.GetValue(sql));
                            if (sfJitCountNum < sfJitNum)
                            {
                                //无手动计算 也不触发自动计算则返回
                                return;
                            }
                        }
                    }
                }
                //锁定转换状态
                string sql1 = "select runflag from ms_jit_state where jitflag='SF' AND gzdd='" + plinecode + "'";
                if (dc.GetTable(sql1).Rows.Count == 0)
                {
                    sql1 = "insert into ms_jit_state(runflag,jituser,jitflag,gzdd)values('1','','SF','" + plinecode + "')";
                    dc.ExeSql(sql1);
                }
                else
                {
                    sql1 = "select runflag from ms_jit_state where jitflag='SF' AND gzdd='" + plinecode + "'";
                    string runFlag = dc.GetValue(sql1);
                    if (runFlag == "1")//判断计算状态是否锁定
                    {
                        savelog(plinecode, "SF", "计算状态被锁定，不能进行计算!", "");
                        return;
                    }
                    else
                    {
                        sql1 = "update ms_jit_state set runflag='1' where jitflag='SF' AND gzdd='" + plinecode + "'";
                        dc.ExeSql(sql1);
                    }
                }
                string manualflag1="N";
                if(manualflag)
                {manualflag1="Y";}
                else
                {manualflag1="N";}

                string result1="";
                MS_SF_JIT_R sp = new MS_SF_JIT_R()
                {
                    GZQY1 = plinecode,
                    QADSITE1 = qadsite,
                    MANUALFLAG1=manualflag1,
                    RESULT1 = "",
                };
                Procedure.run(sp);
                result1 = sp.RESULT1;

                //dh.PL_CALCULATION_MATERIAL(plinecode, qadsite, manualflag1, out result1);

                //三方运算处理后结果判断
                if (result1 != "NODATA")
                {
                    if (result1 != "SUCCESS")
                    {
                        if (manualflag)
                        {
                            savelog(plinecode, "SF", "计算出错－手动!" + result1, "");
                        }
                        else
                        {
                            savelog(plinecode, "SF", "计算出错－自动!" + result1, "");
                        }
                        TimerSF.Enabled = false;
                    }
                    else
                    {
                        //生成要料单
                        if (manualflag)
                        {
                            savelog(plinecode, "SF", "生成要料单-手动!", "");
                        }
                        else
                        {
                            savelog(plinecode, "SF", "生成要料单-自动!", "");
                        }
                        WriteXmlSf(plinecode,qadsite);
                        CreateTxtFileSfToBarcode(plinecode);
                        CreateTxtFileSupply(plinecode);
                    }
                    string sql3 = "update ms_jit_state set runflag='0',jittime=sysdate where jitflag='SF' AND gzdd='" + plinecode + "'";
                    dc.ExeSql(sql3);
                }
                else
                {
                    string sql3 = "update ms_jit_state set runflag='0' where jitflag='SF' AND gzdd='" + plinecode + "'";
                    dc.ExeSql(sql3);
                }
                if (manualflag)//处理手动提交请求
                {
                    sfManualFlag = false;
                    sql1 = "update ms_jit_manualflag set manualflag='1',jittime=sysdate where gzdd='" + plinecode + "' and jitflag='SF' and manualflag='0' ";
                    dc.ExeSql(sql1);
                }
                //前两次手动启动
                sql1 = "select count(distinct(sf_jit_id_two)) count1 from ms_sfjit_plan_log";
                if (dc.GetValue(sql1) == "1")
                {
                    savelog(plinecode, "SF", "已停止，需手工启动第二次计算，选择手动执行!", "");
                    TimerSF.Enabled = false;
                }
                savelog(plinecode, "SF", "当次计算完毕!", "");
            }
            catch (Exception e1)
            {
                savelog(plinecode, "SF", "处理过程出错，" + e1.Message + "!", "");
                string sql = "update ms_jit_state set runflag='0' where jitflag='SF' AND gzdd='" + plinecode + "'";
                dc.ExeSql(sql);
                if (manualflag)//如果存在手动计算 则置为1  用户重新提交
                {
                    sfManualFlag = false;
                    sql = "update ms_jit_manualflag set manualflag='1',jittime=sysdate where gzdd='" + plinecode + "' and jitflag='SF' and manualflag='0'";
                    dc.ExeSql(sql);
                    Writetxt("手动计算出错，请用户重新提交!");
                    //TimerSF.Enabled = true;
                    checkBoxHandle.Checked = false;
                }
                //Writetxt(e1.Message);
            }
        }

        private void sF_Single(string plinecode,string qadsite,string manualpcode)
        {
            //按计划单独要料
            try
            {
                //单独处理三方按计划单独要料，也就是紧急要料
                //单独要料不参与JITTIME字段的更新
                string runFlag = "" ;
                //锁定转换状态
                string sql1 = "select runflag from ms_jit_state where jitflag='SF' AND gzdd='" + plinecode + "'";
                if (dc.GetTable(sql1).Rows.Count == 0)
                {
                    sql1 = "insert into ms_jit_state(runflag,jituser,jitflag,gzdd)values('1','','SF','" + plinecode + "')";
                    dc.ExeSql(sql1);
                }
                else
                {
                    sql1 = "select runflag from ms_jit_state where jitflag='SF' AND gzdd='" + plinecode + "'";
                    runFlag = dc.GetValue(sql1);
                    if (runFlag == "1")//判断计算状态是否锁定
                    {
                        savelog(plinecode, "SF", "计算状态被锁定，不能进行按计划单独要料计算!", "");
                        return;
                    }
                    else
                    {
                        sql1 = "update ms_jit_state set runflag='1' where jitflag='SF' AND gzdd='" + plinecode + "'";
                        dc.ExeSql(sql1);
                    }
                }
                string result1 = "";
                MS_SF_JIT_SINGLE_R sp = new MS_SF_JIT_SINGLE_R()
                {
                    GZQY1 = plinecode,
                    QADSITE1 = qadsite,
                    RESULT1 = "",
                };
                Procedure.run(sp);
                result1 = sp.RESULT1;
                //dh.PL_CALCULATION_MATERIAL_SINGLEPLAN(plinecode, qadsite, out result1);

                //三方运算处理后结果判断
                if (result1 != "NODATA")
                {
                    if (result1 != "SUCCESS")
                    {
                        savelog(plinecode, "SF", "按计划单独要料计算出错!" + result1, "");
                        TimerSF.Enabled = false;
                    }
                    else
                    {
                        //生成要料单
                        savelog(plinecode, "SF", "按计划单独要料计算完毕!", "");
                        WriteXmlSf(plinecode, qadsite);
                        CreateTxtFileSfToBarcode(plinecode);
                        CreateTxtFileSupply(plinecode);
                    }
                }

                string sql3 = "update ms_jit_state set runflag='0' where jitflag='SF' AND gzdd='" + plinecode + "'";
                dc.ExeSql(sql3);
            }
            catch (Exception e1)
            {
                savelog(plinecode, "SF", "三方按计划单独要料处理过程出错，" + e1.Message + "!", "");
                string sql = "update ms_jit_state set runflag='0' where jitflag='SF' AND gzdd='" + plinecode + "'";
                dc.ExeSql(sql);
                //Writetxt(e1.Message);
            }
        }

        private void sF_Single_Mat(string plinecode,string qadsite,string manualpcode)
        {
            //按物料单独要料
            try
            {
                //单独处理三方按物料单独要料，也就是紧急要料
                //单独要料不参与JITTIME字段的更新
                string runFlag = "";
                //锁定转换状态
                string sql1 = "select runflag from ms_jit_state where jitflag='SF' AND gzdd='" + plinecode + "'";
                if (dc.GetTable(sql1).Rows.Count == 0)
                {
                    sql1 = "insert into ms_jit_state(runflag,jituser,jitflag,gzdd)values('1','','SF','" + plinecode + "')";
                    dc.ExeSql(sql1);
                }
                else
                {
                    sql1 = "select runflag from ms_jit_state where jitflag='SF' AND gzdd='" + plinecode + "'";
                    runFlag = dc.GetValue(sql1);
                    if (runFlag == "1")//判断计算状态是否锁定
                    {
                        savelog(plinecode, "SF", "计算状态被锁定，不能进行按物料单独要料计算!", "");
                        return;
                    }
                    else
                    {
                        sql1 = "update ms_jit_state set runflag='1' where jitflag='SF' AND gzdd='" + plinecode + "'";
                        dc.ExeSql(sql1);
                    }
                }
                string result1 = "";
                MS_SF_JIT_SINGLE_MAT_R sp = new MS_SF_JIT_SINGLE_MAT_R()
                {
                    GZQY1 = plinecode,
                    QADSITE1 = qadsite,
                    RESULT1 = "",
                };
                Procedure.run(sp);
                result1 = sp.RESULT1;
                //dh.PL_CALCULATION_MATERIAL_SINGLEMAT(plinecode, qadsite, out result1);

                //三方运算处理后结果判断
                if (result1 != "NODATA")
                {
                    if (result1 != "SUCCESS")
                    {
                        savelog(plinecode, "SF", "按物料单独要料计算出错!" + result1, "");
                        TimerSF.Enabled = false;
                    }
                    else
                    {
                        //生成要料单
                        savelog(plinecode, "SF", "按物料单独要料计算完毕!", "");
                        WriteXmlSf(plinecode, qadsite);
                        CreateTxtFileSfToBarcode(plinecode);
                        CreateTxtFileSupply(plinecode);
                    }
                }

                string sql3 = "update ms_jit_state set runflag='0' where jitflag='SF' AND gzdd='" + plinecode + "'";
                dc.ExeSql(sql3);
            }
            catch (Exception e1)
            {
                savelog(plinecode, "SF", "三方按物料单独要料处理过程出错，" + e1.Message + "!", "");
                string sql = "update ms_jit_state set runflag='0' where jitflag='SF' AND gzdd='" + plinecode + "'";
                dc.ExeSql(sql);
                //Writetxt(e1.Message);
            }

        }

        private void WriteXmlSf(string plinecode, string qadsite)
        {                
            string thisBillCode = "", thisFileName = "", thisPoNbr = "", thisInvCode = "", thisBgyCode = "",thisFile="";;
            string releaseNo = "", gysCode = "", releaseDate = "", facilityId = "", shipToId = "", supplierId = "", thisInvFlag = "", poNumber = "";
            try
            {
                string purposeCode = "00";
                string shipDeliveryCode = "DL";
                string testYn = "N";
                string unitOfMeasure = "EA";
                string requirementType = "C";
                string requirementFreq = "J";

                string sql = "select gzdd from ms_sfjit_result_current where gzdd='" + plinecode + "'";
                if (dc.GetTable(sql).Rows.Count == 0)
                {
                    savelog(plinecode, "SF", "当次计算没有产生数据!", "");
                    return;
                }
                else
                {
                    savelog(plinecode, "SF", "开始生成XML文件!", "");
                }
                if (!Directory.Exists(theSfXmlPath))
                {
                    Directory.CreateDirectory(theSfXmlPath);
                }
                string theCreateDate = System.DateTime.Now.ToString("yyyyMMdd");
                int countjs = 1;
                sql = "select distinct bill_code from ms_sfjit_result_current where gzdd='" + plinecode + "' order by bill_code";
                DataTable dt = dc.GetTable(sql);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    thisBillCode=dt.Rows[i][0].ToString();
                    thisFileName = thisBillCode;
                    //得到po_nbr、inv_code
                    string sql2 = "select distinct po_nbr,inv_code,bgy_code from ms_sfjit_result_current where gzdd='" + plinecode + "' and bill_code='" + thisBillCode + "' order by po_nbr,inv_code,bgy_code";
                    DataTable dt2 = dc.GetTable(sql2);
                    for (int j = 0; j < dt2.Rows.Count; j++)
                    {
                        thisPoNbr=dt2.Rows[j][0].ToString();
                        thisInvCode = dt2.Rows[j][1].ToString();
                        thisBgyCode = dt2.Rows[j][2].ToString();
                        string sql3 = "select distinct bill_code,gys_code,to_char(create_time,'YYYY-MM-DD'),qadsite,bgy_code,inv_code,inv_flag from ms_sfjit_result_current where gzdd='" + plinecode + "' and bill_code='" + thisBillCode + "' and po_nbr='" + thisPoNbr + "' and inv_code='" + thisInvCode + "' and bgy_code='" + thisBgyCode + "'";
                        DataTable dt3 = dc.GetTable(sql3);
                        if (dt3.Rows.Count > 0)
                        {
                             releaseNo = dt3.Rows[0][0].ToString();
                             gysCode = dt3.Rows[0][1].ToString();
                             releaseDate = dt3.Rows[0][2].ToString();
                             facilityId = dt3.Rows[0][3].ToString();
                             shipToId = dt3.Rows[0][4].ToString();
                             supplierId = dt3.Rows[0][5].ToString();
                             thisInvFlag = dt3.Rows[0][6].ToString();
                             poNumber = thisPoNbr;
                        }
                        else
                        {
                            releaseNo = "";
                            poNumber = "";
                            releaseDate = "";
                            facilityId = "";
                            shipToId = "";
                            supplierId = "";
                            thisInvFlag = "";
                        }
                        if (thisInvFlag == "N")
                        {
                            supplierId = gysCode;
                        }
                        if (plinecode == "R")
                        {
                            facilityId = "DCEC-R";
                        }
                        
                        if(!theSfXmlPath.EndsWith("\\"))
                        {
                            thisFile = theSfXmlPath + "\\" + thisFileName + "(" + gysCode + ")" + "(" + thisInvCode + ")" + "(" + facilityId + ")" + "-" + countjs.ToString() + ".xml";
                        }
                        else
                        {
                            thisFile = theSfXmlPath + thisFileName + "(" + gysCode + ")" + "(" + thisInvCode + ")" + "(" + facilityId + ")" + "-" + countjs.ToString() + ".xml";
                        }
                        
                        if (File.Exists(thisFile))
                        {
                            File.Delete(thisFile);//存在则删除 重新生成
                            //return;
                        }
                        //else
                        //{}
                            //File.Create(thisFile);
                        XmlDocument xd = new XmlDocument();
                        xd.CreateXmlDeclaration("1.0", "UTF-8", "yes");

                        XmlNode root = xd.CreateElement("SupplyWeb_Data");
                        root.AppendChild(root.OwnerDocument.CreateTextNode("\r\n"));
                        xd.AppendChild(root);

                        XmlNode theNode = xd.CreateNode(XmlNodeType.Element, "ShippingSchedule", "");

                        XmlElement theRoot = xd.DocumentElement;

                        theNode.AppendChild(theNode.OwnerDocument.CreateTextNode("\r\n"));
                        theRoot.AppendChild(theNode);

                        XmlNode theNode1 = theNode.OwnerDocument.CreateElement("ShippingSchedule_Header");
                        theNode.AppendChild(theNode1);
                        theNode.AppendChild(theNode.OwnerDocument.CreateTextNode("\r\n"));
                        theNode1.AppendChild(theNode1.OwnerDocument.CreateTextNode("\r\n"));

                        theNode1 = PublicClass.CreateNode(4, theNode1, "release_no", releaseNo, xd);
                        theNode1 = PublicClass.CreateNode(4, theNode1, "release_date", releaseDate, xd);
                        theNode1 = PublicClass.CreateNode(4, theNode1, "purpose_code", purposeCode, xd);
                        theNode1 = PublicClass.CreateNode(4, theNode1, "facility_id", facilityId, xd);
                        theNode1 = PublicClass.CreateNode(4, theNode1, "ship_to_id", shipToId, xd);
                        theNode1 = PublicClass.CreateNode(4, theNode1, "supplier_id", supplierId, xd);
                        theNode1 = PublicClass.CreateNode(4, theNode1, "ship_delivery_code", shipDeliveryCode, xd);
                        theNode1 = PublicClass.CreateNode(4, theNode1, "test_yn", testYn, xd);
                        theNode1 = PublicClass.CreateNode(4, theNode1, "po_number", poNumber, xd);

                        theRoot.AppendChild(theRoot.OwnerDocument.CreateTextNode("\r\n"));
                        //得到物料明细
                        string sql1 = "select material_code,material_name,location_code,to_char(require_time,'YYYY-MM-DD'),to_char(require_time,'HH24:MI:SS'),material_num,emgt_flag,plan_code,assign_flag,part_charger from ms_sfjit_result_current where gzdd='" + plinecode + "' and bill_code='" + thisBillCode + "'  and po_nbr='" + thisPoNbr + "' and inv_code='" + thisInvCode + "' and bgy_code='" + thisBgyCode + "' order by material_code,location_code";
                        DataTable dt1 = dc.GetTable(sql1);
                        for (int m = 0; m < dt1.Rows.Count; m++)
                        {
                            string buyerPartNo = dt1.Rows[m][0].ToString();
                            string buyerPartDesc = dt1.Rows[m][1].ToString();
                            string engineeringLevel = dt1.Rows[m][2].ToString();
                            string requirementDate = dt1.Rows[m][3].ToString();
                            string requirementTime = dt1.Rows[m][4].ToString();
                            string requirementQty = dt1.Rows[m][5].ToString();
                            string emgtCode = dt1.Rows[m][6].ToString();
                            string modelYear = dt1.Rows[m][7].ToString();
                            string thisAssignFlag = dt1.Rows[m][8].ToString();
                            string contactName = dt1.Rows[m][9].ToString();
                            string poLineNum = dt1.Rows.Count.ToString();

                            XmlNode theNode2 = theNode.OwnerDocument.CreateElement("Part_Detail");
                            theNode.AppendChild(theNode2);
                            theNode.AppendChild(theNode.OwnerDocument.CreateTextNode("\r\n"));
                            theNode2.AppendChild(theNode2.OwnerDocument.CreateTextNode("\r\n"));

                            if (thisInvFlag == "Y")
                            {
                                buyerPartNo = gysCode + "#" + buyerPartNo;
                            }
                            if (thisAssignFlag == "Y")
                            {
                                buyerPartNo = buyerPartNo + "#ASSIGN";
                            }
                            theNode2 = PublicClass.CreateNode(4, theNode2, "buyer_part_no", buyerPartNo, xd);
                            theNode2 = PublicClass.CreateNode(4, theNode2, "buyer_part_desc", buyerPartDesc, xd);
                            theNode2 = PublicClass.CreateNode(4, theNode2, "po_number", poNumber, xd);
                            theNode2 = PublicClass.CreateNode(4, theNode2, "po_line_num", poLineNum, xd);
                            theNode2 = PublicClass.CreateNode(4, theNode2, "unit_of_measure", unitOfMeasure, xd);
                            theNode2 = PublicClass.CreateNode(4, theNode2, "model_year", modelYear, xd);
                            theNode2 = PublicClass.CreateNode(4, theNode2, "engineering_level", engineeringLevel, xd);
                            theNode2 = PublicClass.CreateNode(4, theNode2, "contact_name", contactName, xd);

                            XmlNode theNode3 = theNode2.OwnerDocument.CreateElement("Requirement_Detail");
                            theNode2.AppendChild(theNode3);
                            theNode2.AppendChild(theNode2.OwnerDocument.CreateTextNode("\r\n"));
                            theNode3.AppendChild(theNode3.OwnerDocument.CreateTextNode("\r\n"));

                            theNode3 = PublicClass.CreateNode(4, theNode3, "requirement_date", requirementDate, xd);
                            theNode3 = PublicClass.CreateNode(4, theNode3, "requirement_time", requirementTime, xd);
                            theNode3 = PublicClass.CreateNode(4, theNode3, "requirement_qty", requirementQty, xd);
                            theNode3 = PublicClass.CreateNode(4, theNode3, "requirement_type", requirementType, xd);
                            theNode3 = PublicClass.CreateNode(4, theNode3, "requirement_freq", requirementFreq, xd);
                            theNode3 = PublicClass.CreateNode(4, theNode3, "requirement_end_date", requirementDate, xd);
                            theNode3 = PublicClass.CreateNode2(4, theNode3, "flex", thisBillCode, "jit_plancode", xd);
                            theNode3 = PublicClass.CreateNode2(4, theNode3, "flex", emgtCode, "EMGT", xd);

                            theRoot.AppendChild(theRoot.OwnerDocument.CreateTextNode("\r\n"));
                        }
                        
                        xd.Save(thisFile);
                        countjs = countjs + 1;
                        theNode = null;
                    }
                    handleNote2(plinecode,thisBillCode,"SF");
                }
                int count1 = PublicClass.XCopyFile(theSfXmlPath, theSfXmlRemotePath);
                int count2 = PublicClass.XCopyFile(theSfXmlPath, theSfXmlBakPath);
                
                if (count1 > 0 && count2 > 0)
                {
                    PublicClass.DeleteFiles(theSfXmlPath);
                }
                savelog(plinecode, "SF", "生成XML文件完成!", "");
            }
            catch (Exception e1)
            {
                savelog(plinecode, "SF", "写单据号" + thisBillCode + "的数据到SRM中出错," + thisFile + e1.Message, "");
                if(sfManualFlag)
                {
                    sfManualFlag=false;
                    string sql4="update ms_jit_manualflag set manualflag='1',jittime=sysdate where gzdd='" + plinecode + "' and jitflag='SF' and manualflag='0'";
                    dc.ExeSql(sql4);
                }
                dc.ExeSql("update ms_jit_state set runflag='0',jittime=sysdate where jitflag='SF' AND gzdd='" + plinecode + "'");
            }
        }




        private void CreateTxtFileSfToBarcode(string thisDd)
        {
            string theRequireTime = "", theCreateDate = "", thisBillCode = "", thisFile = "";
            StreamWriter Ts1;
            try
            {
                 //生成三方需求的条码接口文件
                string sql = "select gzdd from ms_sfjit_result_current where gzdd='" + thisDd + "'";
                DataTable dt= dc.GetTable(sql);
                if (dt.Rows.Count == 0)
                {
                    savelog(thisDd, "SF", "当次计算没有产生数据，无法生成BARCODE文件", "");
                    return;
                }
                else
                {
                    savelog(thisDd, "SF", "开始写三方数据到条码系统中!", "");
                }
                if (!Directory.Exists(theSfTxtPath))
                {
                    Directory.CreateDirectory(theSfTxtPath+"\\");
                }
                //得到需求时间
                sql = "select to_char(sysdate+to_number(parameter_value)/(24*60),'YYMMDDHH24MISS') from ms_jit_parameter where gzdd='" + thisDd + "' and parameter_code='$THIRD_JIT_TIME'";
                dt=dc.GetTable(sql);
                if (dt.Rows.Count > 0)
                {
                    theRequireTime = dt.Rows[0][0].ToString();
                }
                else
                {
                    DateTime dt0 = System.DateTime.Now;
                    DateTime dt1 = dt0.AddHours(4);
                    theRequireTime = dt1.ToString("yyyyMMddHHmmss");
                }
                //获取单号数据
                sql = "select distinct bill_code from ms_sfjit_result_current where gzdd='" + thisDd + "' order by bill_code";
                dt = dc.GetTable(sql);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    theCreateDate = System.DateTime.Now.ToString("yymmdd");
                    thisBillCode=dt.Rows[i][0].ToString();
                    thisFile = thisBillCode;
                    if (File.Exists(theSfTxtPath + "\\" + thisFile + ".bak"))
                    {
                        File.Delete(theSfTxtPath + "\\" + thisFile + ".bak");
                    }
                    Ts1 = File.CreateText(theSfTxtPath+"\\"+thisFile+".bak");
                    //单据号;日程单号;零件代码;数量;地点;保管员;供应商;生成日期;需求时间;入库截止时间
                    //ESF20090403093139;0030401B;C4899230;27;DCEC-B;B301;21DQ003;2011/01/01 08:21;2011/01/02 14:21:33;2011/01/02 17:21:33
                    Ts1.WriteLine("//单据号;日程单号;零件代码;数量;地点;保管员;供应商;生成日期;需求时间;入库截止时间");
                    string sql2 = "select bill_code,nvl(po_nbr,''),nvl(material_code,''),sum(material_num),nvl(qadsite,''),nvl(bgy_code,''),nvl(gys_code,''),to_char(create_time,'YYYY/MM/DD HH24:MI:SS'),to_char(require_time,'YYYY/MM/DD HH24:MI:SS'),to_char(require_time,'YYYY/MM/DD HH24:MI:SS') from ms_sfjit_result_current where gzdd='" + thisDd + "' and bill_code='" + thisBillCode + "' group by bill_code,po_nbr,material_code,qadsite,bgy_code,gys_code,to_char(create_time,'YYYY/MM/DD HH24:MI:SS'),to_char(require_time,'YYYY/MM/DD HH24:MI:SS'),to_char(require_time,'YYYY/MM/DD HH24:MI:SS')";
                    DataTable dt2 = dc.GetTable(sql2);
                    for (int k = 0; k < dt2.Rows.Count; k++)
                    {
                        string lineStr="";
                        if (!String.IsNullOrEmpty(dt2.Rows[k][0].ToString()))
                        {
                            lineStr = lineStr + dt2.Rows[k][0].ToString() + ";";
                        }
                        else
                        {
                            lineStr = lineStr + ":";
                        }
                        if (!String.IsNullOrEmpty(dt2.Rows[k][1].ToString()))
                        {
                            lineStr = lineStr + dt2.Rows[k][1].ToString() + ";";
                        }
                        else
                        {
                            lineStr = lineStr + ":";
                        }
                        if (!String.IsNullOrEmpty(dt2.Rows[k][2].ToString()))
                        {
                            lineStr = lineStr + dt2.Rows[k][2].ToString() + ";";
                        }
                        else
                        {
                            lineStr = lineStr + ":";
                        }
                        if (!String.IsNullOrEmpty(dt2.Rows[k][3].ToString()))
                        {
                            lineStr = lineStr + dt2.Rows[k][3].ToString() + ";";
                        }
                        else
                        {
                            lineStr = lineStr + ":";
                        }
                        if (!String.IsNullOrEmpty(dt2.Rows[k][4].ToString()))
                        {
                            lineStr = lineStr + dt2.Rows[k][4].ToString() + ";";
                        }
                        else
                        {
                            lineStr = lineStr + ":";
                        }
                        if (!String.IsNullOrEmpty(dt2.Rows[k][5].ToString()))
                        {
                            lineStr = lineStr + dt2.Rows[k][5].ToString() + ";";
                        }
                        else
                        {
                            lineStr = lineStr + ":";
                        }
                        if (!String.IsNullOrEmpty(dt2.Rows[k][6].ToString()))
                        {
                            lineStr = lineStr + dt2.Rows[k][6].ToString() + ";";
                        }
                        else
                        {
                            lineStr = lineStr + ":";
                        }
                        if (!String.IsNullOrEmpty(dt2.Rows[k][7].ToString()))
                        {
                            lineStr = lineStr + dt2.Rows[k][7].ToString() + ";";
                        }
                        else
                        {
                            lineStr = lineStr + ":";
                        }
                        if (!String.IsNullOrEmpty(dt2.Rows[k][8].ToString()))
                        {
                            lineStr = lineStr + dt2.Rows[k][8].ToString() + ";";
                        }
                        else
                        {
                            lineStr = lineStr + ":";
                        }
                        if (!String.IsNullOrEmpty(dt2.Rows[k][9].ToString()))
                        {
                            lineStr = lineStr + dt2.Rows[k][9].ToString();// +";";
                        }
                        else
                        {
                            lineStr = lineStr + "";
                        }
                        //lineStr = lineStr + (char)13 + (char)10;
                        Ts1.WriteLine(lineStr);
                    }

                    Ts1.WriteLine("end" + (char)13 + (char)10);
                    Ts1.Close();

                    if (File.Exists(theSfTxtPath + "\\" + thisFile + ".txt"))
                    {
                        File.Delete(theSfTxtPath + "\\" + thisFile + ".txt");
                    }
                    FileInfo fi = new FileInfo(theSfTxtPath+"\\"+thisFile+".bak");
                    fi.MoveTo(Path.Combine(theSfTxtPath + "\\" + thisFile + ".txt"));
                }
                int count1 = PublicClass.XCopyFile(theSfTxtPath, theSfTxtRemotePath);
                int count2 = PublicClass.XCopyFile(theSfTxtPath, theSfTxtBakPath);

                if (count1 > 0 && count2 > 0)
                {
                    PublicClass.DeleteFiles(theSfTxtPath);
                }
                savelog(thisDd, "SF", "写三方数据到条码系统完成!", "");
            }
            catch (Exception e1)
            {
                savelog(thisDd, "SF", "写三方需求单据号" + thisBillCode + "的数据到条码系统中出错," + e1.Message, "");
                if (sfManualFlag)
                {
                    sfManualFlag = false;
                    dc.ExeSql("update ms_jit_manualflag set manualflag='1',jittime=sysdate where gzdd='" + thisDd + "' and jitflag='SF' and manualflag='0' ");
                }
            }
        }

        private void CreateTxtFileSupply(string thisDd)
        {
            string thisFile = "", thisBillCode="";
            StreamWriter Ts1;
            try
            {
                //生成三方需求的SW文本文件
                string sql = "select gzdd from ms_sfjit_result_current where gzdd='" + thisDd + "'";
                DataTable dt = dc.GetTable(sql);
                if (dt.Rows.Count == 0)
                {
                    savelog(thisDd, "SF", "当次计算没有产生数据，无法生成SW文本文件接口!", "");
                    return;
                }
                else
                {
                    savelog(thisDd, "SF", "开始生成SW文本文件接口!", "");
                }
                if (!Directory.Exists(theSfTxtSupplyPath))
                {
                    Directory.CreateDirectory(theSfTxtSupplyPath + "\\");
                }
                thisFile = "SM01_" + System.DateTime.Now.ToString("yyyyMMddHHmmss");
                if (File.Exists(theSfTxtSupplyPath + "\\" + thisFile + ".txt"))
                {
                    File.Delete(theSfTxtSupplyPath + "\\" + thisFile + ".txt");
                }
                Ts1 = File.CreateText(theSfTxtSupplyPath + "\\" + thisFile + ".txt");
                //MES单号|订单号|订单行号|地点编码|库房编码|物流商编码|供应商编码|需求日期|需求时间|零件号|零件名称(中文)|需求数量|工位|紧急与否|是否特指|计划号
                //ESF20111122082323|10|DCEC-B|B01||100045|2011-11-22|17:00:00|C4988747|输油泵总成|20.00|A420|Y|N|E20111125-02
                sql = "select nvl(bill_code,''),nvl(QADSITE,''),(case inv_flag when 'Y' then inv_code else '' end) a,nvl(GYS_CODE,''),to_char(require_time,'yyyy-mm-dd') b,to_char(require_time,'hh24:mi:ss') c,nvl(MATERIAL_CODE,''),nvl(MATERIAL_NAME,''),nvl(MATERIAL_NUM,''),nvl(LOCATION_CODE,''),nvl(EMGT_FLAG,''),nvl(PLAN_CODE,''),nvl(BGY_CODE,''),nvl(po_nbr,''),nvl(assign_flag,'') from ms_sfjit_result_current where gzdd='" + thisDd + "'  order by require_time ";
                DataTable dt2=dc.GetTable(sql);
                int ihhh = 0;
                for(int k=0;k<dt2.Rows.Count;k++)
                {
                    string lineStr = "";
                    //MES单号
                    if (!String.IsNullOrEmpty(dt2.Rows[k][0].ToString()))
                    {
                        lineStr = lineStr + dt2.Rows[k][0].ToString() + "|";
                        thisBillCode = dt2.Rows[k][0].ToString();
                    }
                    else
                    {
                        lineStr = lineStr + "|";
                    }
                    //订单号
                    if (!String.IsNullOrEmpty(dt2.Rows[k][13].ToString()))
                    {
                        lineStr = lineStr + dt2.Rows[k][13].ToString() + "|";
                    }
                    else
                    {
                        lineStr = lineStr + "|";
                    }
                    //订单行号
                    ihhh = ihhh + 1;
                    lineStr = lineStr + ihhh + "|";
                    //地点编码
                    if (!String.IsNullOrEmpty(dt2.Rows[k][1].ToString()))
                    {
                        lineStr = lineStr + dt2.Rows[k][1].ToString() + "|";
                    }
                    else
                    {
                        lineStr = lineStr + "|";
                    }
                    //库房编码
                    string str6 = "", str1 = "", str2 = "";
                    if (!String.IsNullOrEmpty(dt2.Rows[k][6].ToString()))
                    {
                        str6 = dt2.Rows[k][6].ToString();
                    }
                    if (!String.IsNullOrEmpty(dt2.Rows[k][1].ToString()))
                    {
                        str1 = dt2.Rows[k][1].ToString();
                    }
                    string sql3 = "select UPPER(nvl(IN_CK,'TEMP')) FROM copy_in_mstr where upper(IN_PART)='" + str6 + "' and upper(IN_SITE)='" + str1 + "'";
                    str2 = dc.GetValue(sql3);
                    lineStr = lineStr + str2 + "|";
                    //物流商编码
                    if (!String.IsNullOrEmpty(dt2.Rows[k][2].ToString()))
                    {
                        lineStr = lineStr + dt2.Rows[k][2].ToString() + "|";
                    }
                    else
                    {
                        lineStr = lineStr + "|";
                    }
                    //供应商编码
                    if (!String.IsNullOrEmpty(dt2.Rows[k][3].ToString()))
                    {
                        lineStr = lineStr + dt2.Rows[k][3].ToString() + "|";
                    }
                    else
                    {
                        lineStr = lineStr + "|";
                    }
                    //需求日期
                    if (!String.IsNullOrEmpty(dt2.Rows[k][4].ToString()))
                    {
                        lineStr = lineStr + dt2.Rows[k][4].ToString() + "|";
                    }
                    else
                    {
                        lineStr = lineStr + "|";
                    }
                    //需求时间
                    if (!String.IsNullOrEmpty(dt2.Rows[k][5].ToString()))
                    {
                        lineStr = lineStr + dt2.Rows[k][5].ToString() + "|";
                    }
                    else
                    {
                        lineStr = lineStr + "|";
                    }
                    //零件号
                    if (!String.IsNullOrEmpty(dt2.Rows[k][6].ToString()))
                    {
                        lineStr = lineStr + dt2.Rows[k][6].ToString() + "|";
                    }
                    else
                    {
                        lineStr = lineStr + "|";
                    }
                    //零件名称中文
                    if (!String.IsNullOrEmpty(dt2.Rows[k][7].ToString()))
                    {
                        lineStr = lineStr + dt2.Rows[k][7].ToString() + "|";
                    }
                    else
                    {
                        lineStr = lineStr + "|";
                    }
                    //零件数量
                    if (!String.IsNullOrEmpty(dt2.Rows[k][8].ToString()))
                    {
                        lineStr = lineStr + dt2.Rows[k][8].ToString() + "|";
                    }
                    else
                    {
                        lineStr = lineStr + "|";
                    }
                    //工位
                    if (!String.IsNullOrEmpty(dt2.Rows[k][9].ToString()))
                    {
                        lineStr = lineStr + dt2.Rows[k][9].ToString() + "|";
                    }
                    else
                    {
                        lineStr = lineStr + "|";
                    }
                    //紧急与否
                    if (!String.IsNullOrEmpty(dt2.Rows[k][10].ToString()))
                    {
                        lineStr = lineStr + dt2.Rows[k][10].ToString() + "|";
                    }
                    else
                    {
                        lineStr = lineStr + "|";
                    }                   
                    //是否特指
                    if (!String.IsNullOrEmpty(dt2.Rows[k][14].ToString()))
                    {
                        lineStr = lineStr + dt2.Rows[k][14].ToString() + "|";
                    }
                    else
                    {
                        lineStr = lineStr + "|";
                    }
                    //计划号
                    if (!String.IsNullOrEmpty(dt2.Rows[k][11].ToString()))
                    {
                        lineStr = lineStr + dt2.Rows[k][11].ToString() + "";
                    }
                    else
                    {
                        lineStr = lineStr + "";
                    }
                    //lineStr = lineStr + (char)13 + (char)10;
                    Ts1.WriteLine(lineStr);
                }
                Ts1.Close();
                int count1 = PublicClass.XCopyFile(theSfTxtSupplyPath, theSfTxtSupplyRemotePath);
                int count2 = PublicClass.XCopyFile(theSfTxtSupplyPath, theSfTxtSupplyBakPath);
                
                if (count1 > 0 && count2 > 0)
                {
                    PublicClass.DeleteFiles(theSfTxtSupplyPath);
                }
                savelog(thisDd, "SF", "生成SW文本文件接口完成!", "");
            }
            catch (Exception e1)
            {
                savelog(thisDd, "SF", "生成三方需求单据号" + thisBillCode + "的接口文本文件出错," + e1.Message, "");
            }
        }

        private void handleMobile(String thisMobile, String thisBillCode, String thisMobileNote, String plinecode, String thisJitFlag)
        {
            try
            {
                dataConn1 dcsms = new dataConn1();
                string sql = "insert into sms_atpu(mobile,content) values('" + thisMobile + "','" + thisMobileNote + "')";
                dcsms.ExeSql(sql);
            }
            catch (Exception e1)
            {
                savelog(plinecode, thisJitFlag, "处理单据号" + thisBillCode + "的短信通知过程中出错," + e1.Message, "");
                //Writetxt(e1.Message);
            }
        }

        private void handleMail2(String thisMailAddress, String thisMailSubject, String thisMailBody, String thisBillCode, String thisDd, String thisJitFlag)
        {
            try
            {
                string sql = "insert into ms_jit_mail2(mailaddress,mailsubject,mailbody,mailflag,createtime) values('" + thisMailAddress.Replace("@", "@@") + "','" + thisMailSubject + "','" + thisMailBody + "','N',sysdate)";
                dc.ExeSql(sql);
            }
            catch (Exception e1)
            {
                savelog(thisDd, thisJitFlag, "处理单据号" + thisBillCode + "的邮件通知过程中出错," + e1.Message, "");
                //Writetxt(e1.Message);
            }
        }

        private void handleNote2(string plinecode, string thisBillCode, string thisJitFlag)
        {
            try
            {
                string theSupplier = "", theMobileNote, theMobile, theMailAddress, theMobileNoteInner;
                string sql = "select get_email_content('" + thisBillCode + "') from dual";
                string theMailBody = dc.GetValue(sql);
                sql = "SELECT DISTINCT NVL(INV_CODE,'MES') FROM MS_SFJIT_RESULT_CURRENT WHERE BILL_CODE='" + thisBillCode + "'";
                dc.setTheSql(sql);
                DataTable dt = dc.GetTable(sql);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    theSupplier=dt.Rows[i][0].ToString();
                    string sql1 = "select get_mobile_content('" + thisBillCode + "','" + theSupplier + "','N') from dual ";
                    theMobileNote = dc.GetValue(sql1);
                    //新建连接
                    string sql2 = "select contact_info_tx,ez_contact_method_id from vw_sms where ex_supplier_id_tx='" + theSupplier + "'";
                    DataTable dt2 = dcsrm.GetTable(sql2);
                    for (int j = 0; j < dt2.Rows.Count; j++)
                    {
                        string str1 = dt2.Rows[j][1].ToString();
                        if (str1 == "3")
                        {
                            theMobile = dt2.Rows[j][0].ToString();
                            handleMobile(theMobile,thisBillCode,theMobileNote,plinecode,thisJitFlag);
                        }
                        else
                        {
                            theMailAddress = dt2.Rows[j][0].ToString();
                            handleMail2(theMailAddress,theMobileNote,theMailBody,thisBillCode,plinecode,thisJitFlag);
                        }
                    }
                }
                string sql3 = "select get_mobile_content('" + thisBillCode + "','" + theSupplier + "','Y') from dual";
                theMobileNoteInner = dc.GetValue(sql3);
                string sql4 = "select jitmail from ms_mail where gzdd='" + plinecode + "' and jitflag='" + thisJitFlag + "'";
                DataTable dt4=dc.GetTable(sql4);
                for (int k = 0; k < dt4.Rows.Count; k++)
                {
                    theMailAddress = dt4.Rows[k][0].ToString();
                    handleMail2(theMailAddress, theMobileNoteInner, theMailBody, thisBillCode, plinecode, thisJitFlag);
                }
            }
            catch (Exception e1)
            {
                savelog(plinecode, thisJitFlag, "处理单据号" + thisBillCode + "的通知过程中出错," + e1.Message, "");
                //Writetxt(e1.Message);
            }
        }

        private void lstBoxPline_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (lstBoxPline.SelectedIndex < 0)
                    return;
                DataRowView drv = lstBoxPline.SelectedItem as DataRowView;
                if (drv == null)
                {
                    return;
                }
                string seltext = drv["SHOW1"].ToString();
                //string seltext = lstBoxPline.SelectedItem.ToString();
                char[] charSeparators1 = new char[] { '&' };
                string[] array1 = seltext.Split(charSeparators1);
                if (array1[2] == "运行")
                {
                    if (MessageBox.Show("确定将生产线" + array1[0] + "的计算状态改为停止？", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        string sql = "update code_product_line set third_flag='N' where pline_code='" + array1[0] + "'";
                        dc.ExeSql(sql);
                    }
                }
                else
                    if (array1[2] == "停止")
                    {
                        if (MessageBox.Show("确定将生产线" + array1[0] + "的计算状态改为运行？", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            string sql = "update code_product_line set third_flag='Y' where pline_code='" + array1[0] + "'";
                            dc.ExeSql(sql);
                        }
                    }
                initPline();
            }
            catch (Exception e1)
            {
                Writetxt(e1.Message);
            }
        }

        private void btnHandle_Click(object sender, EventArgs e)
        {
            try
            {
                TimerSF.Enabled = false;
                for (int i = 0; i < lstBoxPline.Items.Count; i++)
                {
                    DataRowView drv = lstBoxPline.Items[i] as DataRowView;
                    if (drv == null)
                    {
                        continue;
                    }
                    string seltext = drv["SHOW1"].ToString();
                    char[] charSeparators1 = new char[] { '&' };
                    string[] array1 = seltext.Split(charSeparators1);
                    string plinecode = array1[0];
                    string qadsite = array1[1];
                    string runflag = array1[2];
                    if (runflag == "运行")
                    {
                        if (CheckSfDd(plinecode))//判断生产线计算参数是否齐全
                        {
                            sfManualFlag = true;
                            sfManualPlinecode = plinecode;
                            sF_handle(plinecode, qadsite, sfManualFlag, sfManualPlinecode);
                        }
                    }
                }
                sfManualFlag = false;
                checkBoxHandle.Checked = false;
                //TimerSF.Enabled = true;
            }
            catch (Exception e1)
            {
                Writetxt(e1.Message);
            }
        }

        private void MatCal_FormClosing(object sender, FormClosingEventArgs e)
        {
            //关闭窗体时触发
            buttonSave1();
            string sql = "update ms_jit_state set runflag='0'";
            dc.ExeSql(sql);
        }

//        private void button1_Click(object sender, EventArgs e)
//        {
//            string path = "D:\\happy.xml";
//            //XmlDocument xd = new XmlDocument();

//            //xd.CreateXmlDeclaration("1.0", "UTF-8", "yes");
////            XmlNode root = xd.CreateElement("NUAA");
////            xd.AppendChild(root);
////            XmlNode xn = xd.CreateElement("CAE");
////            xn.AppendChild(xn.OwnerDocument.CreateTextNode("\r\n"));
////            xn.InnerText = "mytest";
////root.AppendChild(xn);
////            XmlAttribute xa = xd.CreateAttribute("name");
////            xa.Value = "happyhuang";
////            root.Attributes.Append(xa);

////            XmlNode XN1 = xd.CreateElement("CAE1");
////            XN1.AppendChild(XN1.OwnerDocument.CreateTextNode("\r\n"));
////            XN1.InnerText = "MYTEST";

////            root.AppendChild(XN1);

//            //XmlDocument xd = new XmlDocument();
//            //xd.CreateXmlDeclaration("1.0", "UTF-8", "yes");
//            ////xd.AppendChild(xd.OwnerDocument.CreateTextNode("\r\n"));

//            ////XmlNode root = xd.CreateElement("SupplyWeb_Data");
//            ////root.AppendChild(root.OwnerDocument.CreateTextNode("\r\n"));
//            ////xd.AppendChild(root);
//            //XmlNode root = xd.CreateNode(XmlNodeType.Element, "SupplyWeb_Data", "");  
//            //root.AppendChild(root.OwnerDocument.CreateTextNode("\r\n"));
//            //xd.AppendChild(root);

//            //XmlNode theNode = xd.CreateNode(XmlNodeType.Element, "ShippingSchedule", "");
//            //XmlElement theRoot = xd.DocumentElement;

//            //theNode.AppendChild(theNode.OwnerDocument.CreateTextNode("\r\n"));
//            //theRoot.AppendChild(theNode);

//            //XmlNode theNode1 = theNode.OwnerDocument.CreateElement("ShippingSchedule_Header");
//            //theNode.AppendChild(theNode1);
//            //theNode.AppendChild(theNode.OwnerDocument.CreateTextNode("\r\n"));
//            //theNode1.AppendChild(theNode1.OwnerDocument.CreateTextNode("\r\n"));

//            //theNode1 = CreateNode(4, theNode1, "release_no", "1", xd);
//            //theNode1 = CreateNode(4, theNode1, "release_date", "2", xd);
//            //theNode1 = CreateNode(4, theNode1, "purpose_code", "3", xd);
//            //theNode1 = CreateNode(4, theNode1, "facility_id", "4", xd);
//            //theNode1 = CreateNode(4, theNode1, "ship_to_id", "5", xd);
//            //theNode1 = CreateNode(4, theNode1, "supplier_id", "6", xd);
//            //theNode1 = CreateNode(4, theNode1, "ship_delivery_code", "7", xd);
//            //theNode1 = CreateNode(4, theNode1, "test_yn", "8", xd);
//            //theNode1 = CreateNode(4, theNode1, "po_number", "9", xd);

//            //theRoot.AppendChild(theRoot.OwnerDocument.CreateTextNode("\r\n"));
            

//            //xd.Save(path);
//        }

    }
}
