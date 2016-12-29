using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rmes.Pub.Data1;
using Rmes.DA.Base;
using Rmes.DA.Entity;
using Rmes.DA.Factory;
using System.Data;
using System.IO;
using System.Windows.Forms;
using System.Diagnostics;
using System.Reflection;
using Microsoft.VisualBasic;
using Microsoft.Office.Interop.Excel;
using Rmes.DA.Procedures;

namespace Rmes.WinForm.Base
{
    public class PublicClass
    {
        //private static dataConn dc = new dataConn();
        private static Timer timer1 = new Timer();
        private static string Psn = "", Pso = "";

        public static string IsFr(string plancode,string so,string sn,string plinecode)
        {
            try
            {
                string str1 = "0";
                str1 = dataConn.GetValue("select is_fr('"+plancode+"','"+so+"','"+plinecode+"','"+sn+"','"+LoginInfo.StationInfo.STATION_NAME+"','"+LoginInfo.UserInfo.USER_CODE+"') from dual ");
                return str1;
            }
            catch
            {
                return "0";
            }
        }
        public static string[] GetLines(string fileName, Encoding encoding)
        {

            // 我不清楚文本文件有多少行

            List<string> listStr = new List<string>();

            // 读取每一行 FileStream，读一个字节，读一个字节片段

            // 不清楚一行到哪里结束，并不知道一行有多少个字节

            // 要读的是文本文件，文本文件中\r\n表示的是回车看到\r\n即可认定是一行

            List<byte> listByte = new List<byte>();

            using (FileStream file = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            {

                int data = -1;
                int olddata = -1;
                do
                {

                    // 循环会读取所有的数据

                    data = file.ReadByte();

                    // 判断是不是\r或\n

                    // 由于读到最后一行结束的时候没有\r\n

                    // 因此最后一行不会加到listAtr中，所以加上判断 data == -1

                    // 表示文档结束的时候也将前面的内容加到listStr中

                    //if ((char)data == '\r' ||(char)data == '\n' || data == -1)// 
                    if (((char)data == '\n' && (char)olddata == '\r') || data == -1)// 
                    {

                        // 做一些处理

                        if (listByte.Count == 0)
                        {

                            continue;

                        }

                        // 如果出现乱码判断listByte开头是否是ef ff 等字符

                        string temp = encoding.GetString(listByte.ToArray());

                        listStr.Add(temp);

                        listByte.Clear();
                        olddata = -1;
                    }
                    else
                    {

                        // 不是\r\n的时候表示仍然是一行数据，将其加到listByte中

                        listByte.Add((byte)data);
                        olddata = data;
                    }

                }
                while (data != -1);

            }

            return listStr.ToArray();

        }
        public static void InsertSLData(string sn)
        {
            bool file1Exist = false, file2Exist = false, theflag = false;
            string theFile1 = @"d:\cysdata\"+DateTime.Now.ToString("yyyyMMdd")+"泄漏试验.txt";
            string theFile2 = @"d:\cysdata\" + DateTime.Now.AddDays(-1).ToString("yyyyMMdd") + "泄漏试验.txt";
            string theFile = "";
            ProductInfoEntity product = ProductInfoFactory.GetByCompanyCodeSNSingle(LoginInfo.CompanyInfo.COMPANY_CODE, sn);//获取sn信息
            if (product == null) return;

            if (File.Exists(theFile1))
            {
                file1Exist = true;
            }
            if (File.Exists(theFile2))
            {
                file2Exist = true;
            }
            if (file1Exist == false && file2Exist == false)
                return;
            if (file1Exist)
            {
                theFile = theFile1;

                //StreamReader Ts1;
                //Ts1 = File.OpenText(theFile);
                //string line1;
                //while ((line1 = Ts1.ReadLine()) != null) //Encoding.GetEncoding("gb2312")
                //{

                //}
                string[] filelist = File.ReadAllLines(theFile,Encoding.Default);
                filelist = GetLines(theFile,Encoding.Default);
                for (int i = filelist.Length - 1; i >= 0; i--) //倒序读取文本
                {
                    string line = filelist[i].Replace((char)13, ' ').Trim();
                    if (line != "")
                    {
                        string[] strAry = line.Split(';');
                        if (sn == strAry[0])
                        {
                            string sql = "insert into atpuslsjb(ghtm,comnum,slsj) values('" + sn + "','com1','" + line + "') ";
                            dataConn.ExeSql(sql);
                            sql = "insert into data_sn_detect_data_temp (rmes_id,sn,company_code,plan_code,pline_code,location_code,station_code,product_series,detect_code,detect_name,detect_value,user_id,work_time,station_name,detect_type,detect_flag,detect_seq)"
                                  + " values(seq_rmes_id.nextval,'" + sn + "','" + LoginInfo.CompanyInfo.COMPANY_CODE + "','" + product.PLAN_CODE + "','" + LoginInfo.ProductLineInfo.PLINE_CODE + "','','" + LoginInfo.StationInfo.STATION_CODE + "','" + product.ROUNTING_REMARK + "','SJE036','水套系统泄漏量','"+strAry[4]+"','"+LoginInfo.UserInfo.USER_CODE+"',sysdate,'"+LoginInfo.StationInfo.STATION_NAME+"','3','Y','0'    )";
                            dataConn.ExeSql(sql);
                            sql = "insert into data_sn_detect_data_temp (rmes_id,sn,company_code,plan_code,pline_code,location_code,station_code,product_series,detect_code,detect_name,detect_value,user_id,work_time,station_name,detect_type,detect_flag,detect_seq)"
                                  + " values(seq_rmes_id.nextval,'" + sn + "','" + LoginInfo.CompanyInfo.COMPANY_CODE + "','" + product.PLAN_CODE + "','" + LoginInfo.ProductLineInfo.PLINE_CODE + "','','" + LoginInfo.StationInfo.STATION_CODE + "','" + product.ROUNTING_REMARK + "','SJE035','机油系统泄漏量','" + strAry[6] + "','" + LoginInfo.UserInfo.USER_CODE + "',sysdate,'" + LoginInfo.StationInfo.STATION_NAME + "','3','Y','0'    )";
                            dataConn.ExeSql(sql);
                            sql = "insert into data_sn_detect_data_temp (rmes_id,sn,company_code,plan_code,pline_code,location_code,station_code,product_series,detect_code,detect_name,detect_value,user_id,work_time,station_name,detect_type,detect_flag,detect_seq)"
                                  + " values(seq_rmes_id.nextval,'" + sn + "','" + LoginInfo.CompanyInfo.COMPANY_CODE + "','" + product.PLAN_CODE + "','" + LoginInfo.ProductLineInfo.PLINE_CODE + "','','" + LoginInfo.StationInfo.STATION_CODE + "','" + product.ROUNTING_REMARK + "','SJE037','互漏系统泄漏量','" + strAry[2] + "','" + LoginInfo.UserInfo.USER_CODE + "',sysdate,'" + LoginInfo.StationInfo.STATION_NAME + "','3','Y','0'    )";
                            dataConn.ExeSql(sql);
                            sql = "insert into data_sn_detect_data_temp (rmes_id,sn,company_code,plan_code,pline_code,location_code,station_code,product_series,detect_code,detect_name,detect_value,user_id,work_time,station_name,detect_type,detect_flag,detect_seq)"
                                  + " values(seq_rmes_id.nextval,'" + sn + "','" + LoginInfo.CompanyInfo.COMPANY_CODE + "','" + product.PLAN_CODE + "','" + LoginInfo.ProductLineInfo.PLINE_CODE + "','','" + LoginInfo.StationInfo.STATION_CODE + "','" + product.ROUNTING_REMARK + "','SJE034','燃油系统泄漏量','" + strAry[8] + "','" + LoginInfo.UserInfo.USER_CODE + "',sysdate,'" + LoginInfo.StationInfo.STATION_NAME + "','3','Y','0'    )";
                            dataConn.ExeSql(sql);
                            theflag = true;
                            break;
                        }
                    }
                }
            }
            if (theflag == false && file2Exist==true)
            {
                theFile = theFile2;
                string[] filelist = File.ReadAllLines(theFile, Encoding.Default);
                filelist = GetLines(theFile, Encoding.Default);
                for (int i = filelist.Length - 1; i >= 0; i--) //倒序读取文本
                {
                    string line = filelist[i].Replace((char)13, ' ').Trim();
                    if (line != "")
                    {
                        string[] strAry = line.Split(';');
                        if (sn == strAry[0])
                        {
                            string sql = "insert into atpuslsjb(ghtm,comnum,slsj) values('" + sn + "','com1','" + line + "') ";
                            dataConn.ExeSql(sql);
                            sql = "insert into data_sn_detect_data_temp (rmes_id,sn,company_code,plan_code,pline_code,location_code,station_code,product_series,detect_code,detect_name,detect_value,user_id,work_time,station_name,detect_type,detect_flag,detect_seq)"
                                  + " values(seq_rmes_id.nextval,'" + sn + "','" + LoginInfo.CompanyInfo.COMPANY_CODE + "','" + product.PLAN_CODE + "','" + LoginInfo.ProductLineInfo.PLINE_CODE + "','','" + LoginInfo.StationInfo.STATION_CODE + "','" + product.ROUNTING_REMARK + "','SJE036','水套系统泄漏量','" + strAry[4] + "','" + LoginInfo.UserInfo.USER_CODE + "',sysdate,'" + LoginInfo.StationInfo.STATION_NAME + "','3','Y','0'    )";
                            dataConn.ExeSql(sql);
                            sql = "insert into data_sn_detect_data_temp (rmes_id,sn,company_code,plan_code,pline_code,location_code,station_code,product_series,detect_code,detect_name,detect_value,user_id,work_time,station_name,detect_type,detect_flag,detect_seq)"
                                  + " values(seq_rmes_id.nextval,'" + sn + "','" + LoginInfo.CompanyInfo.COMPANY_CODE + "','" + product.PLAN_CODE + "','" + LoginInfo.ProductLineInfo.PLINE_CODE + "','','" + LoginInfo.StationInfo.STATION_CODE + "','" + product.ROUNTING_REMARK + "','SJE035','机油系统泄漏量','" + strAry[6] + "','" + LoginInfo.UserInfo.USER_CODE + "',sysdate,'" + LoginInfo.StationInfo.STATION_NAME + "','3','Y','0'    )";
                            dataConn.ExeSql(sql);
                            sql = "insert into data_sn_detect_data_temp (rmes_id,sn,company_code,plan_code,pline_code,location_code,station_code,product_series,detect_code,detect_name,detect_value,user_id,work_time,station_name,detect_type,detect_flag,detect_seq)"
                                  + " values(seq_rmes_id.nextval,'" + sn + "','" + LoginInfo.CompanyInfo.COMPANY_CODE + "','" + product.PLAN_CODE + "','" + LoginInfo.ProductLineInfo.PLINE_CODE + "','','" + LoginInfo.StationInfo.STATION_CODE + "','" + product.ROUNTING_REMARK + "','SJE037','互漏系统泄漏量','" + strAry[2] + "','" + LoginInfo.UserInfo.USER_CODE + "',sysdate,'" + LoginInfo.StationInfo.STATION_NAME + "','3','Y','0'    )";
                            dataConn.ExeSql(sql);
                            sql = "insert into data_sn_detect_data_temp (rmes_id,sn,company_code,plan_code,pline_code,location_code,station_code,product_series,detect_code,detect_name,detect_value,user_id,work_time,station_name,detect_type,detect_flag,detect_seq)"
                                  + " values(seq_rmes_id.nextval,'" + sn + "','" + LoginInfo.CompanyInfo.COMPANY_CODE + "','" + product.PLAN_CODE + "','" + LoginInfo.ProductLineInfo.PLINE_CODE + "','','" + LoginInfo.StationInfo.STATION_CODE + "','" + product.ROUNTING_REMARK + "','SJE034','燃油系统泄漏量','" + strAry[8] + "','" + LoginInfo.UserInfo.USER_CODE + "',sysdate,'" + LoginInfo.StationInfo.STATION_NAME + "','3','Y','0'    )";
                            dataConn.ExeSql(sql);
                            theflag = true;
                            break;
                        }
                    }
                }
            }
        }

        public static string checktishi(string stationname,string so,string sn)
        {
            if (dataConn.GetValue("select count(1) from atpuzdnts t where zdmc='" + stationname + "' ") != "0")
                return "";
            string tishi = ""; //zdmc='" + stationname + "' and
            string sql = "select * from atpuxcsjb where sjbs='1' and gzdd='"+LoginInfo.ProductLineInfo.PLINE_CODE+"'";
            System.Data.DataTable dt3 = dataConn.GetTable(sql);
            if (dt3.Rows.Count > 0)
            {
                for (int i = 0; i < dt3.Rows.Count; i++)
                {
                    if (dt3.Rows[i]["SJDM"].ToString() == "FDJSOBH")//SO变换
                    {
                        try
                        {
                            if (CsGlobalClass.OLDSO != so && CsGlobalClass.OLDSO != "")
                            {
                                tishi = tishi + "员工注意，即将装配新SO！";
                            }
                            else
                            { tishi = tishi + ""; }
                        }
                        catch
                        { }
                    }
                    if (dt3.Rows[i]["SJDM"].ToString() == "FDJJXBH")//机型变换
                    {
                        try
                        {
                            if (CsGlobalClass.OLDSO != so && CsGlobalClass.OLDSO != "")
                            {
                                tishi = tishi + "员工注意，即将装配新机型！";
                            }
                            else
                            { tishi = tishi + ""; }
                        }
                        catch
                        { }
                    }
                    if (dt3.Rows[i]["SJDM"].ToString() == "FDJLSKZ")//漏扫提示
                    {
                        try
                        {
                            sql = "select a.plan_code,b.plan_type from data_product a,data_plan b where a.sn='" + sn + "' and a.plan_code=b.plan_code and b.pline_code='R' and nvl(b.remark,'1') like '%补充产能%'";
                            System.Data.DataTable dt4 = dataConn.GetTable(sql);
                            if (dt4.Rows.Count > 0 && (stationname == "Z940" || stationname == "Z890" || stationname == "ZF5"))
                            {
                                tishi = tishi + "";
                            }
                            else
                            {
                                //改制返修计划不需要控制漏扫 dt4.Rows[0][1].ToString() != "C" && dt4.Rows[0][1].ToString() != "D"
                                if (dt4.Rows.Count > 0)
                                {
                                    if (dt4.Rows[0][1].ToString() != "C" && dt4.Rows[0][1].ToString() != "D")
                                    {
                                        string str1 = "";
                                        ProductDataFactory.PL_CHECK_FDJLS(sn, stationname, LoginInfo.ProductLineInfo.PLINE_CODE, out str1);
                                        if (str1 != "GOOD")
                                        {
                                            if (str1 != "ERROR")
                                            {
                                                str1 = str1.Substring(0, str1.Length - 4);
                                            }
                                            tishi = tishi + "前面站点漏扫,漏扫站点为" + str1;
                                        }
                                    }
                                    else
                                    {
                                        tishi = tishi + "";
                                    }
                                }
                                else
                                {
                                    string str1 = "";
                                    //ProductDataFactory.PL_CHECK_FDJLS(sn, stationname, LoginInfo.ProductLineInfo.PLINE_CODE, out str1);
                                    PL_CHECK_FDJLS sp = new PL_CHECK_FDJLS()
                                    {
                                        GHTM1 = sn,
                                        ZDMC1 = stationname,
                                        GZDD1 = LoginInfo.ProductLineInfo.PLINE_CODE,
                                        OUTSTR1 = ""
                                    };
                                    Rmes.DA.Base.Procedure.run(sp);
                                    str1 = sp.OUTSTR1;
                                    if (str1 != "GOOD")
                                    {
                                        try
                                        {
                                            if (str1 != "ERROR")
                                            {
                                                str1 = str1.Substring(4);
                                            }
                                            tishi = tishi + "前面站点漏扫,漏扫站点为" + str1;
                                        }
                                        catch
                                        { }
                                    }
                                }
                            }
                        }
                        catch
                        { }
                    }
                }
            }
            return tishi;
        }
        public static void TestDataCreate(string sn,string plinecode,string plancode)
        {
            string sql = "delete from ATPUTESTPROGRAM where ghtm='"+sn+"'";
            dataConn.ExeSql(sql);
            string fr = "";//获取发动机上线时的fr
            try
            {
                fr = dataConn.GetValue("select fr from data_sn_fr where sn='" + sn + "' and plan_code='" + plancode + "'");
            }
            catch
            { fr = ""; }
            string cxh ="";
            try
            {
                cxh = dataConn.GetValue("select cxh from atpufrcxh where fr='" + fr + "' and rownum=1 ");
            }
            catch
            {
                cxh = "";
            }
            string sat = "0", brake = "0", batt = "0";
            try
            {
                sat = dataConn.GetValue("select sat from atpufrcxh where fr='" + fr + "' and rownum=1 ");
                try
                {
                    if (sat == "" || sat == null)
                    {
                        sat = "0";
                    }
                }
                catch
                {
                    sat = "0";
                }
            }
            catch
            {
                sat = "0";
            }
            if (dataConn.GetValue("select count(1) from qad_bom where abom_jhdm='" + plancode + "' and replace(abom_comp,'ZZ','')='EB 9039'") != "0")
            {
                brake = "1";
            }
            if (dataConn.GetValue("select count(1) from qad_bom where abom_jhdm='" + plancode + "' and replace(abom_comp,'ZZ','') in ('FV 9557','FV 9560','FV 9497','FV 9498')") != "0")
            {
                batt = "1";
            }
            if (fr != "ERROR")
                dataConn.ExeSql("insert into ATPUTESTPROGRAM (ghtm,fr,cx,gzdd,rqsj,sat,braket,batt) values ('" + sn + "','" + fr + "','" + cxh + "','" + plinecode + "',sysdate,'" + sat + "','" + brake + "','" + batt + "')");
        }
        public static void Handle_Veps(string so,string sn)
        {
            try
            {
                string vname = dataConn.GetValue("select GET_VEPSV('" + so + "') from dual");
                Psn = "";
                Pso = "";
                if (vname == "10")
                    vname = "24";
                StreamWriter Ts1;
                if (!Directory.Exists("d:\\dcecatpu\\vepsv\\"))
                {
                    Directory.CreateDirectory("d:\\dcecatpu\\vepsv\\");
                }
                string TheFileName = "d:\\dcecatpu\\vepsv\\VOLTAGE.txt";
                //if (File.Exists(TheFileName))
                //{
                //    File.Delete(TheFileName);
                //}
                Ts1 = new StreamWriter(TheFileName, false, Encoding.GetEncoding("gb2312"));
                Ts1.WriteLine(vname);
                Ts1.Close();

                TheFileName = "d:\\dcecatpu\\vepsv\\ESN.txt";
                //if (File.Exists(TheFileName))
                //{
                //    File.Delete(TheFileName);
                //}
                Ts1 = new StreamWriter(TheFileName, false, Encoding.GetEncoding("gb2312"));
                Ts1.WriteLine(sn);
                Ts1.Close();
                MessageBox.Show("需要VEPS操作,请连接好数据线和电源线!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Create_Veps(so, sn);

                //timer1.Interval = 100;
                //timer1.Tick += new EventHandler(timer1_Tick);
                Psn = sn;
                Pso = so;
                //timer1.Enabled = true;
                //timer1.Start();

                //string TheFileName = "d:\\dcecatpu\\veps\\" + Pso + ".par";
                System.Threading.Thread.Sleep(3000);
                string TheFileName1 = "d:\\dcecatpu\\veps\\CC.txt";
                if (File.Exists(TheFileName1))
                {
                    if (Pso != "" && Psn != "")
                    {
                        //timer1.Stop();
                        //timer1.Enabled = false;
                        Run_Veps(Pso, Psn);
                        MessageBox.Show("操作完毕!请确认数据线和电源线已经断开!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch(Exception e)
            {
                MessageBox.Show("操作失败!请重新灌录VEPS!"+e.Message, "警告", MessageBoxButtons.OK, MessageBoxIcon.Error);
                
            }
        }
        private static void timer1_Tick(object sender, EventArgs e)
        {
            string TheFileName = "d:\\dcecatpu\\veps\\" + Pso + ".par";
            string TheFileName1 = "d:\\dcecatpu\\veps\\CC.txt";
            if (File.Exists(TheFileName1))
            {
                if (Pso != "" && Psn != "")
                {
                    timer1.Stop();
                    timer1.Enabled = false;
                    Run_Veps(Pso, Psn);
                    MessageBox.Show("操作完毕!请确认数据线和电源线已经断开!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        //veps
        private static void Create_Veps(string so, string sn)
        {
            string thisStr = "";
            thisStr = "S,COMX,1" + (char)13 + (char)10 + "S,MODULE," + "CUMMINS" + (char)13 + (char)10 + (char)13 + (char)10;
            //ProductDataFactory.VEPS_CREATE_SCQBCS();

            System.Data.DataTable dt = dataConn.GetTable("select * from copy_veps_data where so='"+so+"' and 设定值 is not null order by 地址值");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                thisStr = thisStr + "P," + dt.Rows[i][1].ToString() + ",B," + dt.Rows[i][2].ToString();
                thisStr = thisStr + (char)13 + (char)10;
            }
            StreamWriter Ts1;
            if (!Directory.Exists("d:\\dcecatpu\\veps\\"))
            {
                Directory.CreateDirectory("d:\\dcecatpu\\veps\\");
            }
            string TheFileName = "d:\\dcecatpu\\veps\\" + so + ".par";
            string TheFileName1 = "d:\\dcecatpu\\veps\\CC.txt";
            if (File.Exists(TheFileName))
            {
                File.Delete(TheFileName);
            }
            if (File.Exists(TheFileName1))
            {
                File.Delete(TheFileName1);
            }
            Ts1 = new StreamWriter(TheFileName, false, Encoding.GetEncoding("gb2312"));
            Ts1.WriteLine(thisStr);
            Ts1.Close();
            Ts1 = File.CreateText(TheFileName1);
            Ts1.WriteLine("");
            Ts1.Close();
        }

        private static void Run_Veps(string so, string sn)
        {
            try
            {
                string theExeStr = "";
                theExeStr = dataConn.GetValue("select internal_value from code_internal where internal_code='VEPS_PATH'");
                if (theExeStr != "")
                {
                    string path = theExeStr.Substring(0, theExeStr.LastIndexOf("\\"));
                    string run_exe = theExeStr.Substring((theExeStr.LastIndexOf("\\")), theExeStr.Length - theExeStr.LastIndexOf("\\")).Substring(3);

                    string theExeStr1 = " -p d:\\dcecatpu\\veps\\" + so + ".par -v d:\\dcecatpu\\veps\\" + sn + ".ver -r d:\\dcecatpu\\veps\\" + sn + ".vrp -i " + sn;
                    //System.Diagnostics.ProcessStartInfo p = null;
                    //System.Diagnostics.Process Proc;
                    //p = new ProcessStartInfo(run_exe, theExeStr1);
                    //p.WorkingDirectory = path;//设置此外部程序所在windows目录
                    //p.WindowStyle = ProcessWindowStyle.Hidden;//在调用外部exe程序的时候，控制台窗口不弹出
                    //p.UseShellExecute = false;
                    //Proc = System.Diagnostics.Process.Start(p);//调用外部程序
                    //Proc.StartInfo.UseShellExecute = false;

                    string path1 = theExeStr + " " + theExeStr1;
                    Microsoft.VisualBasic.Interaction.Shell(path1, AppWinStyle.NormalFocus);
                }
            }
            catch(Exception e1)
            {
                MessageBox.Show(e1.Message, "提示");
                //MessageBox.Show("执行VEPS程序出错！","提示");
            }
        }
        //防错文件
        public static void Output_DayData_FangCuo(System.Data.DataTable dt, string stationname, string sn, string gcGgxh, string gcSo)
        {   //gwmc,comp,udesc,qty,gxmc,gysmc

            if (LoginInfo.ProductLineInfo.PLINE_CODE == "W" && LoginInfo.StationInfo.STATION_TYPE == "ST02")
            {
                ProductInfoEntity product = ProductInfoFactory.GetByCompanyCodeSNSingle(LoginInfo.CompanyInfo.COMPANY_CODE, sn);
                string fdjxl11 = "";
                try
                {
                    fdjxl11 = product.PRODUCT_SERIES;
                }
                catch
                { }
                string gcSaveStr = "";
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string gcLineStr = "";
                    gcLineStr = dt.Rows[i][0].ToString() + ";" + dt.Rows[i][1].ToString() + ";" + dt.Rows[i][2].ToString() + ";" + dt.Rows[i][3].ToString() + ";" + dt.Rows[i][4].ToString() + ";" + dt.Rows[i][5].ToString();
                    gcSaveStr = gcSaveStr + gcLineStr + (char)13 + (char)10;
                }
                gcSaveStr = fdjxl11 + ";" + gcGgxh + (char)13 + (char)10 + gcSaveStr;
                string TheDateStr = dataConn.GetValue("select to_char(sysdate,'yyyymmddhh24miss') from dual");
                string TheFileName = "d:\\dcecatpu\\fangcuo\\" + sn + TheDateStr + ".txt";
                StreamWriter Ts1;
                if (!Directory.Exists("d:\\dcecatpu\\fangcuo\\"))
                {
                    Directory.CreateDirectory("d:\\dcecatpu\\fangcuo\\");
                }
                if (File.Exists(TheFileName))
                {
                    File.Delete(TheFileName);
                }
                Ts1 = new StreamWriter(TheFileName, false, Encoding.GetEncoding("gb2312"));
                Ts1.WriteLine(gcSaveStr);
                Ts1.Close();
            }
            else
            {
                string gcSaveStr = "";
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string gcLineStr = "";
                    gcLineStr = dt.Rows[i][0].ToString() + ";" + dt.Rows[i][1].ToString() + ":" + dt.Rows[i][2].ToString() + ";" + dt.Rows[i][3].ToString() + ";" + dt.Rows[i][4].ToString() + ";" + dt.Rows[i][5].ToString();
                    gcSaveStr = gcSaveStr + gcLineStr + (char)13 + (char)10;
                }
                gcSaveStr = "\\\\" + ";" + gcGgxh + ";" + gcSo + (char)13 + (char)10 + gcSaveStr;
                string TheDateStr = dataConn.GetValue("select to_char(sysdate,'yyyymmddhh24miss') from dual");
                string TheFileName = "d:\\dcecatpu\\fangcuo\\" + sn + TheDateStr + ".txt";
                StreamWriter Ts1;
                if (!Directory.Exists("d:\\dcecatpu\\fangcuo\\"))
                {
                    Directory.CreateDirectory("d:\\dcecatpu\\fangcuo\\");
                }
                if (File.Exists(TheFileName))
                {
                    File.Delete(TheFileName);
                }
                Ts1 = new StreamWriter(TheFileName, false, Encoding.GetEncoding("gb2312"));
                Ts1.WriteLine(gcSaveStr);
                Ts1.Close();
            }

        }
        //TRIM文件生成 Z490
        public static void Output_Trim_File(string TheSaveLsh, string TotalGs, string TheGs, string TheTrimData)
        {
            try
            {
                string TheFileName = "", TheSaveData = "", TheDateStr = "";
                TheDateStr = dataConn.GetValue("select to_char(sysdate,'yyyymmddhh24mi') from dual ");
                TheSaveData = TheSaveLsh + "," + TotalGs + "," + TheGs + "," + TheTrimData + "," + TheDateStr;
                //TheFileName = @"\\192.168.113.240\Trim\INJECTOR_DATA\" + TheSaveLsh.Substring(0, 6) + @"\" + TheSaveLsh + "." + TheGs;
                //if (!Directory.Exists(@"\\192.168.113.240\Trim\INJECTOR_DATA\" + TheSaveLsh.Substring(0, 6)+"\\"))
                //{
                //    Directory.CreateDirectory(@"\\192.168.113.240\Trim\INJECTOR_DATA\" + TheSaveLsh.Substring(0, 6)+"\\");
                //}
                TheFileName = @"\\192.168.112.144\tranlist\Trim\INJECTOR_DATA\" + TheSaveLsh.Substring(0, 6) + @"\" + TheSaveLsh + "." + TheGs;
                if (!Directory.Exists(@"\\192.168.112.144\tranlist\Trim\INJECTOR_DATA\" + TheSaveLsh.Substring(0, 6) + "\\"))
                {
                    Directory.CreateDirectory(@"\\192.168.112.144\tranlist\Trim\INJECTOR_DATA\" + TheSaveLsh.Substring(0, 6) + "\\");
                }
                if (File.Exists(TheFileName))
                {
                    File.Delete(TheFileName);
                }
                StreamWriter Ts1;
                Ts1 = new StreamWriter(TheFileName, false, Encoding.GetEncoding("gb2312"));
                Ts1.WriteLine(TheSaveData);
                Ts1.Close();

            }
            catch
            {
                MessageBox.Show("请确认Label Matrix的目录,数量是否正确!", "错误提示");
            }
        }

        //AB类零件防错文件
        public static void Output_DayData(string stationname, string sn, string gcType, string gcSaveStr)
        {   
            string TheDateStr = dataConn.GetValue("select to_char(sysdate,'yyyymmdd') from dual");
            string TheFileName = "d:\\dcecatpu\\" + TheDateStr + stationname + gcType + ".txt";
            StreamWriter Ts1;
            if (!Directory.Exists("d:\\dcecatpu\\"))
            {
                Directory.CreateDirectory("d:\\dcecatpu\\");
            }

            if (gcType == "sjjhb")
            {
                if (File.Exists(TheFileName))
                {
                    File.Delete(TheFileName);
                }
                Ts1 = new StreamWriter(TheFileName, false, Encoding.GetEncoding("gb2312"));
                Ts1.WriteLine(gcSaveStr);
                Ts1.Close();
            }
            else
            {
                Ts1 = File.AppendText(TheFileName);
                Ts1.WriteLine(gcSaveStr);
                Ts1.Close();
            }

        }
        //宇通条码打印
        public static void PrintTmZF200(string BB, string AA,string CC,string DD)
        {
            try
            {
                StreamWriter Ts1;
                string TheFileName = "c:\\tmdyyt.txt";
                if (File.Exists(TheFileName))
                {
                    File.Delete(TheFileName);
                }
                Ts1 = new StreamWriter(TheFileName, false, Encoding.GetEncoding("gb2312"));
                Ts1.WriteLine("TM");
                Ts1.WriteLine("\"" + AA + "\"" + "," + "\"" + BB + "\"" + "," + "\"" + CC + "\"" + "," + "\"" + DD + "\"");
                Ts1.WriteLine("\"" + AA + "\"" + "," + "\"" + BB + "\"" + "," + "\"" + CC + "\"" + "," + "\"" + DD + "\"");
                Ts1.WriteLine("\"" + AA + "\"" + "," + "\"" + BB + "\"" + "," + "\"" + CC + "\"" + "," + "\"" + DD + "\"");
                Ts1.WriteLine("\"" + AA + "\"" + "," + "\"" + BB + "\"" + "," + "\"" + CC + "\"" + "," + "\"" + DD + "\"");
                Ts1.Close();
                if (!Directory.Exists("C:\\Program Files\\lmw32\\"))
                {
                    Directory.CreateDirectory("C:\\Program Files\\lmw32\\");
                }
                string path1 = "C:\\Program Files\\lmw32";
                string run_exe = "LMWPRINT";
                string theExeStr1 = " /L=C:\\gttmyt.QDF ";
                System.Diagnostics.ProcessStartInfo p = null;
                System.Diagnostics.Process Proc;
                p = new ProcessStartInfo(run_exe, theExeStr1);
                p.WorkingDirectory = path1;//设置此外部程序所在windows目录
                p.WindowStyle = ProcessWindowStyle.Hidden;//在调用外部exe程序的时候，控制台窗口不弹出

                Proc = System.Diagnostics.Process.Start(p);//调用外部程序
            }
            catch
            {
                MessageBox.Show("请确认Label Matrix的目录,数量是否正确!","错误提示");
            }
        }
        //宇通条码打印
        public static void PrintTmU820(string BB, string AA, string CC, string DD)
        {
            try
            {
                StreamWriter Ts1;
                string TheFileName = "c:\\tmdyyt.txt";
                if (File.Exists(TheFileName))
                {
                    File.Delete(TheFileName);
                }
                Ts1 = new StreamWriter(TheFileName, false, Encoding.GetEncoding("gb2312"));
                Ts1.WriteLine("TM");
                Ts1.WriteLine("\"" + AA + "\"" + "," + "\"" + BB + "\"" + "," + "\"" + CC + "\"" + "," + "\"" + DD + "\"");
                Ts1.WriteLine("\"" + AA + "\"" + "," + "\"" + BB + "\"" + "," + "\"" + CC + "\"" + "," + "\"" + DD + "\"");
                Ts1.WriteLine("\"" + AA + "\"" + "," + "\"" + BB + "\"" + "," + "\"" + CC + "\"" + "," + "\"" + DD + "\"");
                Ts1.WriteLine("\"" + AA + "\"" + "," + "\"" + BB + "\"" + "," + "\"" + CC + "\"" + "," + "\"" + DD + "\"");
                Ts1.Close();
                if (!Directory.Exists("C:\\Program Files\\lmw32\\"))
                {
                    Directory.CreateDirectory("C:\\Program Files\\lmw32\\");
                }
                string path1 = "C:\\Program Files\\lmw32";
                string run_exe = "LMWPRINT";
                string theExeStr1 = " /L=C:\\gttmyt.QDF ";
                System.Diagnostics.ProcessStartInfo p = null;
                System.Diagnostics.Process Proc;
                p = new ProcessStartInfo(run_exe, theExeStr1);
                p.WorkingDirectory = path1;//设置此外部程序所在windows目录
                p.WindowStyle = ProcessWindowStyle.Hidden;//在调用外部exe程序的时候，控制台窗口不弹出

                Proc = System.Diagnostics.Process.Start(p);//调用外部程序
            }
            catch
            {
                MessageBox.Show("请确认Label Matrix的目录,数量是否正确!", "错误提示");
            }
        }
        //东区上线条码打印
        public static void PrintTmFz(string Thislsh, string ThisSo, string ThisYh, string ThisYhdm,string ThisJx,int ThisSl,string ThePlancode)
        {
            try
            {
                string thisFr = dataConn.GetValue("select GET_FR('"+ThisSo+"','"+LoginInfo.ProductLineInfo.PLINE_CODE+"') from dual ");
                StreamWriter Ts1;
                string TheFileName = "C:\\tmdy.txt";
                if (File.Exists(TheFileName))
                {
                    File.Delete(TheFileName);
                }
                Ts1 = new StreamWriter(TheFileName, false, Encoding.GetEncoding("gb2312"));
                Ts1.WriteLine("TM");
                for (int i = 0; i < ThisSl; i++)
                {
                    Ts1.WriteLine(Thislsh + "," + ThisSo + "," + ThisYh + "," + ThisYhdm + "," + ThisJx + "," + thisFr + "," + ThePlancode);
                }
                Ts1.Close();
                if (!Directory.Exists("C:\\Program Files\\lmw32\\"))
                {
                    Directory.CreateDirectory("C:\\Program Files\\lmw32\\");
                }
                string path1 = "C:\\Program Files\\lmw32";
                string run_exe = "LMWPRINT";
                string theExeStr1 = "/L=C:\\gttm.qdf ";
                //string theExeStr1 = "/L=" + @"\\192.168.112.144\mes共享\gttm.qdf ";
                //{
                //    path1 = path1 + "\\LMWPRINT /L=" + "D:\\gttm.qdf";
                //}
                //Microsoft.VisualBasic.Interaction.Shell(path1, AppWinStyle.NormalFocus);


                System.Diagnostics.ProcessStartInfo p = null;
                System.Diagnostics.Process Proc;
                p = new ProcessStartInfo(run_exe, theExeStr1);
                p.WorkingDirectory = path1;//设置此外部程序所在windows目录
                p.WindowStyle = ProcessWindowStyle.Hidden;//在调用外部exe程序的时候，控制台窗口不弹出

                Proc = System.Diagnostics.Process.Start(p);//调用外部程序
                //System.Diagnostics.ProcessStartInfo p = null;
                //System.Diagnostics.Process Proc = new Process();
                //Proc.StartInfo.UseShellExecute = false;
                //Proc.StartInfo.FileName = path1 + "\\" + run_exe;
                //Proc.StartInfo.Arguments = theExeStr1;
                //Proc.StartInfo.CreateNoWindow = true;
                //Proc.Start();

                //p = new ProcessStartInfo(path1 + "\\" + run_exe, theExeStr1);
                //p.UseShellExecute = false;
                //p.CreateNoWindow = true;
                ////p.WorkingDirectory = path1;//设置此外部程序所在windows目录
                ////p.WindowStyle = ProcessWindowStyle.Hidden;//在调用外部exe程序的时候，控制台窗口不弹出

                //Proc = System.Diagnostics.Process.Start(p);//调用外部程序
            }
            catch(Exception e1)
            {
                MessageBox.Show("请确认Label Matrix的目录,数量是否正确!", "错误提示");
            }
        }
        public static void PrintTmFzGb(string Thislsh, string ThisSo, string ThisYh, string ThisYhdm, string ThisJx, int ThisSl,string remark1,string plancode1,string plinecode1)
        {
            try
            {
                if (!remark1.Contains("光板"))
                {
                    return;
                }
                string theGgh = "";
                try
                {
                    theGgh = dataConn.GetValue("select detect_value from data_sn_detect_data where sn='" + Thislsh + "' and plan_code='" + plancode1 + "' and detect_name like '%缸体号%' and pline_code='" + plinecode1 + "' and rownum=1 ");
                }
                catch
                { }
                if (theGgh == "")
                {
                    MessageBox.Show("该发动机缸体号为空！","提示");
                }
                ThisJx = theGgh; 

                StreamWriter Ts1;
                string TheFileName = "c:\\tmdy.txt";
                if (File.Exists(TheFileName))
                {
                    File.Delete(TheFileName);
                }
                Ts1 = new StreamWriter(TheFileName, false, Encoding.GetEncoding("gb2312"));
                Ts1.WriteLine("TM");
                for (int i = 0; i < ThisSl; i++)
                {
                    Ts1.WriteLine(Thislsh + "," + ThisSo + "," + ThisYh + "," + ThisYhdm + "," + ThisJx);
                }
                Ts1.Close();
                if (!Directory.Exists("C:\\Program Files\\lmw32\\"))
                {
                    Directory.CreateDirectory("C:\\Program Files\\lmw32\\");
                }
                string path1 = "C:\\Program Files\\lmw32";
                string run_exe = "LMWPRINT";
                string theExeStr1 = " /L=C:\\gttm.QDF ";
                System.Diagnostics.ProcessStartInfo p = null;
                System.Diagnostics.Process Proc;
                p = new ProcessStartInfo(run_exe, theExeStr1);
                p.WorkingDirectory = path1;//设置此外部程序所在windows目录
                p.WindowStyle = ProcessWindowStyle.Hidden;//在调用外部exe程序的时候，控制台窗口不弹出

                Proc = System.Diagnostics.Process.Start(p);//调用外部程序
            }
            catch (Exception e1)
            {
                MessageBox.Show("请确认Label Matrix的目录,数量是否正确!", "错误提示");
            }
        }
        
        //通用条码打印
        public static void PrintTmFz1(string Thislsh, string ThisSo, string ThisYh, string ThisYhdm, string ThisJx, int ThisSl)
        {
            try
            {
                //string thisFr = dataConn.GetValue("select GET_FR('" + ThisSo + "','" + LoginInfo.ProductLineInfo.PLINE_CODE + "') from dual ");
                StreamWriter Ts1;
                string TheFileName = "c:\\tmdy.txt";
                if (File.Exists(TheFileName))
                {
                    File.Delete(TheFileName);
                }
                Ts1 = new StreamWriter(TheFileName, false, Encoding.GetEncoding("gb2312"));
                Ts1.WriteLine("TM");
                for (int i = 0; i < ThisSl; i++)
                {
                    Ts1.WriteLine(Thislsh + "," + ThisSo + "," + ThisYh + "," + ThisYhdm + "," + ThisJx);
                }
                Ts1.Close();
                if (!Directory.Exists("C:\\Program Files\\lmw32\\"))
                {
                    Directory.CreateDirectory("C:\\Program Files\\lmw32\\");
                }
                string path1 = "C:\\Program Files\\lmw32";
                string run_exe = "LMWPRINT";
                string theExeStr1 = " /L=C:\\gttm.QDF ";
                System.Diagnostics.ProcessStartInfo p = null;
                System.Diagnostics.Process Proc;
                p = new ProcessStartInfo(run_exe, theExeStr1);
                p.WorkingDirectory = path1;//设置此外部程序所在windows目录
                p.WindowStyle = ProcessWindowStyle.Hidden;//在调用外部exe程序的时候，控制台窗口不弹出

                Proc = System.Diagnostics.Process.Start(p);//调用外部程序
            }
            catch (Exception e1)
            {
                MessageBox.Show("请确认Label Matrix的目录,数量是否正确!", "错误提示");
            }
        }
        //isde上线通用条码打印
        public static void PrintTmISDE(string Thisplancode, string Thislsh)
        {
            try
            {
                //string thisFr = dataConn.GetValue("select GET_FR('" + ThisSo + "','" + LoginInfo.ProductLineInfo.PLINE_CODE + "') from dual ");
                StreamWriter Ts1 ;
                string TheFileName = "c:\\tmdy.txt";
                if (File.Exists(TheFileName))
                {
                    File.Delete(TheFileName);
                }
                Ts1 = new StreamWriter(TheFileName, false, Encoding.GetEncoding("gb2312"));
                //Ts1 = File.CreateText(TheFileName);
                //Ts1 = new StreamWriter(TheFileName, false, Encoding.GetEncoding("gb2312"));
                Ts1.WriteLine("TM");
                for (int i = 0; i < 1; i++)
                {
                    if (LoginInfo.ProductLineInfo.PLINE_CODE == "CL")
                    {
                        Ts1.WriteLine("\"" + Thislsh + "\"" + "," + "\"" + Thisplancode + "\"");
                    }
                    else
                    {
                        Ts1.WriteLine("\"" + Thisplancode + "\"" + "," + "\"" + Thislsh + "\"");
                    }
                }
                Ts1.Close();
                if (!Directory.Exists("C:\\Program Files\\lmw32\\"))
                {
                    Directory.CreateDirectory("C:\\Program Files\\lmw32\\");
                }
                string path1 = "C:\\Program Files\\lmw32";
                string run_exe = "LMWPRINT";
                string theExeStr1 = " /L=C:\\gttm.QDF ";
                System.Diagnostics.ProcessStartInfo p = null;
                System.Diagnostics.Process Proc;
                p = new ProcessStartInfo(run_exe, theExeStr1);
                p.WorkingDirectory = path1;//设置此外部程序所在windows目录
                p.WindowStyle = ProcessWindowStyle.Hidden;//在调用外部exe程序的时候，控制台窗口不弹出

                Proc = System.Diagnostics.Process.Start(p);//调用外部程序
            }
            catch (Exception e1)
            {
                MessageBox.Show("请确认Label Matrix的目录,数量是否正确!", "错误提示");
            }
        }
        //通用条码打印
        public static void PrintTmWSx(string Thislsh, string ThisSo, string ThisYh, string ThisYhdm, string ThisJx, int ThisSl)
        {
            try
            {
                string thisFr ="***"+ dataConn.GetValue("select GET_FR('" + ThisSo + "','" + LoginInfo.ProductLineInfo.PLINE_CODE + "') from dual ");
                StreamWriter Ts1;
                string TheFileName = "c:\\tmdy.txt";
                if (File.Exists(TheFileName))
                {
                    File.Delete(TheFileName);
                }
                Ts1 = new StreamWriter(TheFileName, false, Encoding.GetEncoding("gb2312"));
                Ts1.WriteLine("TM");
                for (int i = 0; i < ThisSl; i++)
                {
                    Ts1.WriteLine(Thislsh + "," + ThisSo + "," + ThisYh + "," + thisFr);
                }
                Ts1.Close();
                if (!Directory.Exists("C:\\Program Files\\lmw32\\"))
                {
                    Directory.CreateDirectory("C:\\Program Files\\lmw32\\");
                }
                string path1 = "C:\\Program Files\\lmw32";
                string run_exe = "LMWPRINT";
                string theExeStr1 = " /L=C:\\gttm.QDF ";
                System.Diagnostics.ProcessStartInfo p = null;
                System.Diagnostics.Process Proc;
                p = new ProcessStartInfo(run_exe, theExeStr1);
                p.WorkingDirectory = path1;//设置此外部程序所在windows目录
                p.WindowStyle = ProcessWindowStyle.Hidden;//在调用外部exe程序的时候，控制台窗口不弹出

                Proc = System.Diagnostics.Process.Start(p);//调用外部程序
            }
            catch (Exception e1)
            {
                MessageBox.Show("请确认Label Matrix的目录,数量是否正确!", "错误提示");
            }
        }
        //通用条码打印
        public static void PrintWTmFz(string Thislsh, string ThisSo, string ThisYh, string ThisYhdm, string ThisJx, int ThisSl,string ThePlan)
        {
            try
            {
                string thisFr = dataConn.GetValue("select GET_FR('" + ThisSo + "','" + LoginInfo.ProductLineInfo.PLINE_CODE + "') from dual ");
                StreamWriter Ts1;
                string TheFileName = "c:\\tmdy.txt";
                if (File.Exists(TheFileName))
                {
                    File.Delete(TheFileName);
                }
                Ts1 = new StreamWriter(TheFileName, false, Encoding.GetEncoding("gb2312"));
                Ts1.WriteLine("TM");
                for (int i = 0; i < ThisSl; i++)
                {
                    //Ts1.WriteLine(Thislsh + "," + ThisSo + "," + ThisYh + "," + ThisJx + "," + thisFr + "," + ThisYhdm);
                    Ts1.WriteLine(Thislsh + "," + ThisSo + "," + ThisYh + "," + ThisYhdm + "," + ThisJx + "," + thisFr + "," + ThePlan);
                }
                Ts1.Close();
                if (!Directory.Exists("C:\\Program Files\\lmw32\\"))
                {
                    Directory.CreateDirectory("C:\\Program Files\\lmw32\\");
                }
                string path1 = "C:\\Program Files\\lmw32";
                string run_exe = "LMWPRINT";
                string theExeStr1 = " /L=C:\\gttm.QDF";
                System.Diagnostics.ProcessStartInfo p = null;
                System.Diagnostics.Process Proc;
                p = new ProcessStartInfo(run_exe, theExeStr1);
                p.WorkingDirectory = path1;//设置此外部程序所在windows目录
                p.WindowStyle = ProcessWindowStyle.Hidden;//在调用外部exe程序的时候，控制台窗口不弹出

                Proc = System.Diagnostics.Process.Start(p);//调用外部程序
            }
            catch (Exception e1)
            {
                MessageBox.Show("请确认Label Matrix的目录,数量是否正确!", "错误提示");
            }
        }
        //通用条码打印
        public static void PrintWTmFzSg(string Thislsh, string ThisSo, string ThisYh, string ThisYhdm, string ThisJx, int ThisSl, string ThePlan)
        {
            try
            {
                string thisFr = "***" + dataConn.GetValue("select GET_FR('" + ThisSo + "','" + LoginInfo.ProductLineInfo.PLINE_CODE + "') from dual ");
                StreamWriter Ts1;
                string TheFileName = "c:\\tmdy.txt";
                if (File.Exists(TheFileName))
                {
                    File.Delete(TheFileName);
                }
                Ts1 = new StreamWriter(TheFileName, false, Encoding.GetEncoding("gb2312"));
                Ts1.WriteLine("TM");
                for (int i = 0; i < ThisSl; i++)
                {
                    //Ts1.WriteLine(Thislsh + "," + ThisSo + "," + ThisYh + "," + ThisJx + "," + thisFr + "," + ThisYhdm);
                    Ts1.WriteLine(Thislsh + "," + ThisSo + "," + ThisYh + "," + ThisYhdm + "," + ThisJx + "," + thisFr + "," + ThePlan);
                }
                Ts1.Close();
                if (!Directory.Exists("C:\\Program Files\\lmw32\\"))
                {
                    Directory.CreateDirectory("C:\\Program Files\\lmw32\\");
                }
                string path1 = "C:\\Program Files\\lmw32";
                string run_exe = "LMWPRINT";
                string theExeStr1 = " /L=C:\\gttm.QDF";
                System.Diagnostics.ProcessStartInfo p = null;
                System.Diagnostics.Process Proc;
                p = new ProcessStartInfo(run_exe, theExeStr1);
                p.WorkingDirectory = path1;//设置此外部程序所在windows目录
                p.WindowStyle = ProcessWindowStyle.Hidden;//在调用外部exe程序的时候，控制台窗口不弹出

                Proc = System.Diagnostics.Process.Start(p);//调用外部程序
            }
            catch (Exception e1)
            {
                MessageBox.Show("请确认Label Matrix的目录,数量是否正确!", "错误提示");
            }
        }
        //柳汽上线条码打印
        public static void PrintTmFzSx(string Thislsh, string ThisSo, string ThisYh, string ThisYhdm, string ThisJx, int ThisSl)
        {
            try
            {
                string thisFr = dataConn.GetValue("select GET_FR('" + ThisSo + "','" + LoginInfo.ProductLineInfo.PLINE_CODE + "') from dual ");
                StreamWriter Ts1;
                string TheFileName = "c:\\tmdy.txt";
                if (File.Exists(TheFileName))
                {
                    File.Delete(TheFileName);
                }
                Ts1 = new StreamWriter(TheFileName, false, Encoding.GetEncoding("gb2312"));
                Ts1.WriteLine("TM");
                for (int i = 0; i < ThisSl; i++)
                {
                    Ts1.WriteLine(Thislsh + "," + ThisSo + "," + ThisYh + "," + ThisJx + "," + thisFr);
                    Ts1.WriteLine(Thislsh + "," + ThisSo + "," + ThisYh + "," + ThisJx + "," + thisFr);
                }
                Ts1.Close();
                if (!Directory.Exists("C:\\Program Files\\lmw32\\"))
                {
                    Directory.CreateDirectory("C:\\Program Files\\lmw32\\");
                }
                string path1 = "C:\\Program Files\\lmw32";
                string run_exe = "LMWPRINT";
                string theExeStr1 = " /L=C:\\gttm.QDF";
                System.Diagnostics.ProcessStartInfo p = null;
                System.Diagnostics.Process Proc;
                p = new ProcessStartInfo(run_exe, theExeStr1);
                p.WorkingDirectory = path1;//设置此外部程序所在windows目录
                p.WindowStyle = ProcessWindowStyle.Hidden;//在调用外部exe程序的时候，控制台窗口不弹出

                Proc = System.Diagnostics.Process.Start(p);//调用外部程序
            }
            catch (Exception e1)
            {
                MessageBox.Show("请确认Label Matrix的目录,数量是否正确!", "错误提示");
            }
        }
        //柳汽下线打印
        public static void PrintTmFzLQ(string Thislsh, string ThisSo, string ThisYh, string ThisJx, string printrq, string thePdfj, string thePfbgh, int ThisSl)
        {
            try
            {
                StreamWriter Ts1;
                string TheFileName = "d:\\tmdy.txt";
                if (File.Exists(TheFileName))
                {
                    File.Delete(TheFileName);
                }
                Ts1 = new StreamWriter(TheFileName, false, Encoding.GetEncoding("gb2312"));
                Ts1.WriteLine("TM");
                for (int i = 0; i < ThisSl; i++)
                {
                    Ts1.WriteLine(Thislsh + "," + ThisSo + "," + ThisJx + "," + printrq + "," + ThisYh + "," + thePdfj);
                }
                Ts1.Close();
                if (!Directory.Exists("C:\\Program Files\\lmw32\\"))
                {
                    Directory.CreateDirectory("C:\\Program Files\\lmw32\\");
                }
                string path1 = "C:\\Program Files\\lmw32";
                string run_exe = "LMWPRINT";
                string theExeStr1 = " /L=D:\\gttm.QDF";
                System.Diagnostics.ProcessStartInfo p = null;
                System.Diagnostics.Process Proc;
                p = new ProcessStartInfo(run_exe, theExeStr1);
                p.WorkingDirectory = path1;//设置此外部程序所在windows目录
                p.WindowStyle = ProcessWindowStyle.Hidden;//在调用外部exe程序的时候，控制台窗口不弹出

                Proc = System.Diagnostics.Process.Start(p);//调用外部程序
                dataConn.ExeSql("INSERT INTO ATPUHGZ(LSH,SO,JX,PFDJ,PFBGH,RQ,JYY,XLH)VALUES('" + Thislsh + "','" + ThisSo + "','" + ThisJx + "','" + thePdfj + "','" + thePfbgh + "',sysdate,'','')");
            
                //dataConn.ExeSql("insert into atpuhgzhis(lsh,rqsj,zdmc,czy,note) values('" + Thislsh + "',sysdate,'" + LoginInfo.StationInfo.STATION_NAME+ "','" + ThisYh + "',' ')");
            }
            catch (Exception e1)
            {
                MessageBox.Show("请确认Label Matrix的目录,数量是否正确!", "错误提示");
            }
        }
        //江淮条码打印
        public static void printJh(string AA)
        {
            try
            {
                StreamWriter Ts1;
                string TheFileName = "c:\\tmdyjh.txt";
                if (File.Exists(TheFileName))
                {
                    File.Delete(TheFileName);
                }
                Ts1 = new StreamWriter(TheFileName, false, Encoding.GetEncoding("gb2312"));
                Ts1.WriteLine("TM");
                Ts1.WriteLine("\"" + AA + "\"");
                Ts1.Close();
                if (!Directory.Exists("C:\\Program Files\\lmw32\\"))
                {
                    Directory.CreateDirectory("C:\\Program Files\\lmw32\\");
                }
                string path1 = "C:\\Program Files\\lmw32";
                string run_exe = "LMWPRINT";
                string theExeStr1 = " /L=C:\\gttmjh.QDF";
                System.Diagnostics.ProcessStartInfo p = null;
                System.Diagnostics.Process Proc;
                p = new ProcessStartInfo(run_exe, theExeStr1);
                p.WorkingDirectory = path1;//设置此外部程序所在windows目录
                p.WindowStyle = ProcessWindowStyle.Hidden;//在调用外部exe程序的时候，控制台窗口不弹出

                Proc = System.Diagnostics.Process.Start(p);//调用外部程序
            }
            catch
            {
                MessageBox.Show("请确认Label Matrix的目录,数量是否正确!", "错误提示");
            }
        }
        //厦工条码打印
        public static void printXiag(string AA)
        {
            try
            {
                StreamWriter Ts1;
                string TheFileName = "c:\\tmdyXiag.txt";
                if (File.Exists(TheFileName))
                {
                    File.Delete(TheFileName);
                }
                Ts1 = new StreamWriter(TheFileName, false, Encoding.GetEncoding("gb2312"));
                Ts1.WriteLine("TM");
                Ts1.WriteLine("\"" + AA + "\"");
                Ts1.Close();
                if (!Directory.Exists("C:\\Program Files\\lmw32\\"))
                {
                    Directory.CreateDirectory("C:\\Program Files\\lmw32\\");
                }
                string path1 = "C:\\Program Files\\lmw32";
                string run_exe = "LMWPRINT";
                string theExeStr1 = " /L=C:\\gttmXiag.QDF";
                System.Diagnostics.ProcessStartInfo p = null;
                System.Diagnostics.Process Proc;
                p = new ProcessStartInfo(run_exe, theExeStr1);
                p.WorkingDirectory = path1;//设置此外部程序所在windows目录
                p.WindowStyle = ProcessWindowStyle.Hidden;//在调用外部exe程序的时候，控制台窗口不弹出

                Proc = System.Diagnostics.Process.Start(p);//调用外部程序
            }
            catch
            {
                MessageBox.Show("请确认Label Matrix的目录,数量是否正确!", "错误提示");
            }
        }
        //福田条码打印
        public static void printFT(string wlh,string gysdm,string sn)
        {
            try
            {
                //需要处理的信息 供应商+物料号+上线日期表示的五位批次+流水号后五位表示的流水号
                string thisPch = "";
                try
                {
                    thisPch = dataConn.GetValue("select to_char(work_date,'YYDDD') from data_product where sn='" + sn + "'");
                }
                catch
                {
                    thisPch = "";
                }
                string ThisLsh = sn.Substring(sn.Length-5);
                string tempStr = gysdm + wlh + thisPch + ThisLsh;

                StreamWriter Ts1;
                string TheFileName = "c:\\tmdyft.txt";
                if (File.Exists(TheFileName))
                {
                    File.Delete(TheFileName);
                }
                Ts1 = new StreamWriter(TheFileName, false, Encoding.GetEncoding("gb2312"));
                Ts1.WriteLine("TM");
                Ts1.WriteLine("\"" + tempStr + "\"" + "," + "\"" + wlh + "\"");
                Ts1.Close();
                if (!Directory.Exists("C:\\Program Files\\lmw32\\"))
                {
                    Directory.CreateDirectory("C:\\Program Files\\lmw32\\");
                }
                string path1 = "C:\\Program Files\\lmw32";
                string run_exe = "LMWPRINT";
                string theExeStr1 = " /L=C:\\gttmft.QDF";
                System.Diagnostics.ProcessStartInfo p = null;
                System.Diagnostics.Process Proc;
                p = new ProcessStartInfo(run_exe, theExeStr1);
                p.WorkingDirectory = path1;//设置此外部程序所在windows目录
                p.WindowStyle = ProcessWindowStyle.Hidden;//在调用外部exe程序的时候，控制台窗口不弹出

                Proc = System.Diagnostics.Process.Start(p);//调用外部程序
            }
            catch
            {
                MessageBox.Show("请确认Label Matrix的目录,数量是否正确!", "错误提示");
            }
        }
        //苏州金龙条码打印
        public static void printSZJL(string sn,string so,string plancode)
        {
            try
            {
                string tempStr = "80008-" + sn;
                string wlh = "";
                try
                {
                    wlh = dataConn.GetValue("select khh from atpuplannameplate where bzso='"+so+"' and plan_code='"+plancode+"' and rownum=1");
                }
                catch
                {
                    wlh = "";
                }
                StreamWriter Ts1;
                string TheFileName = "c:\\tmdyszjl.txt";
                if (File.Exists(TheFileName))
                {
                    File.Delete(TheFileName);
                }
                Ts1 = new StreamWriter(TheFileName, false, Encoding.GetEncoding("gb2312"));
                Ts1.WriteLine("TM");
                Ts1.WriteLine("\"" + tempStr + "\"" + "," + "\"" + wlh + "\"");
                Ts1.Close();
                if (!Directory.Exists("C:\\Program Files\\lmw32\\"))
                {
                    Directory.CreateDirectory("C:\\Program Files\\lmw32\\");
                }
                string path1 = "C:\\Program Files\\lmw32";
                string run_exe = "LMWPRINT";
                string theExeStr1 = " /L=C:\\gttmszjl.QDF";
                System.Diagnostics.ProcessStartInfo p = null;
                System.Diagnostics.Process Proc;
                p = new ProcessStartInfo(run_exe, theExeStr1);
                p.WorkingDirectory = path1;//设置此外部程序所在windows目录
                p.WindowStyle = ProcessWindowStyle.Hidden;//在调用外部exe程序的时候，控制台窗口不弹出

                Proc = System.Diagnostics.Process.Start(p);//调用外部程序
            }
            catch
            {
                MessageBox.Show("请确认Label Matrix的目录,数量是否正确!", "错误提示");
            }
        }
        //中通客车
        public static void printZTKC(string sn, string so, string plancode)
        {
            try
            {
                
                string wlh = "";
                try
                {
                    wlh = dataConn.GetValue("select wlh from atpukhtm where so='" + so + "' and khmc='中通客车'");
                }
                catch
                {
                    wlh = "";
                }
                string thisPch = "";
                try
                {
                    thisPch = dataConn.GetValue("select to_char(work_date,'YYMMDD') from data_product where sn='" + sn + "'");
                }
                catch
                {
                    thisPch = "";
                }
                StreamWriter Ts1;
                string TheFileName = "c:\\tmdyztkc.txt";
                if (File.Exists(TheFileName))
                {
                    File.Delete(TheFileName);
                }
                string tempStr = "100895" + thisPch + "00" + wlh;
                Ts1 = new StreamWriter(TheFileName, false, Encoding.GetEncoding("gb2312"));
                Ts1.WriteLine("TM");
                Ts1.WriteLine("\"" + tempStr + "\"" );
                Ts1.Close();
                if (!Directory.Exists("C:\\Program Files\\lmw32\\"))
                {
                    Directory.CreateDirectory("C:\\Program Files\\lmw32\\");
                }
                string path1 = "C:\\Program Files\\lmw32";
                string run_exe = "LMWPRINT";
                string theExeStr1 = " /L=C:\\gttmztkc.QDF";
                System.Diagnostics.ProcessStartInfo p = null;
                System.Diagnostics.Process Proc;
                p = new ProcessStartInfo(run_exe, theExeStr1);
                p.WorkingDirectory = path1;//设置此外部程序所在windows目录
                p.WindowStyle = ProcessWindowStyle.Hidden;//在调用外部exe程序的时候，控制台窗口不弹出

                Proc = System.Diagnostics.Process.Start(p);//调用外部程序
            }
            catch
            {
                MessageBox.Show("请确认Label Matrix的目录,数量是否正确!", "错误提示");
            }
        }
        //ECM防错打印
        public static void printECMFC(string sn, string so)
        {
            try
            {
                string tempStr = "";
                string wlh = "";
                try
                {
                    wlh = dataConn.GetValue("select wlh from atpukhtm where khmc='东风' and so='"+so+"' ");
                }
                catch
                {
                    wlh = "";
                }
                StreamWriter Ts1;
                string TheFileName = "c:\\Data Tag Log File\\tmdyecmfc.txt";
                if (File.Exists(TheFileName))
                {
                    File.Delete(TheFileName);
                }
                Ts1 = new StreamWriter(TheFileName, false, Encoding.GetEncoding("gb2312"));
                Ts1.WriteLine("TM");
                Ts1.WriteLine("\"" + sn + "\"" + "," + "\"" + wlh + "\"");
                Ts1.Close();
                if (!Directory.Exists("C:\\Program Files\\lmw32\\"))
                {
                    Directory.CreateDirectory("C:\\Program Files\\lmw32\\");
                }
                string path1 = "C:\\Program Files\\lmw32";
                string run_exe = "LMWPRINT";
                string theExeStr1 = " /L=C:\\Data Tag Log File\\gttmecmfc.QDF";
                System.Diagnostics.ProcessStartInfo p = null;
                System.Diagnostics.Process Proc;
                p = new ProcessStartInfo(run_exe, theExeStr1);
                p.WorkingDirectory = path1;//设置此外部程序所在windows目录
                p.WindowStyle = ProcessWindowStyle.Hidden;//在调用外部exe程序的时候，控制台窗口不弹出

                Proc = System.Diagnostics.Process.Start(p);//调用外部程序
            }
            catch
            {
                MessageBox.Show("请确认Label Matrix的目录,数量是否正确!", "错误提示");
            }
        }

        //DFAC条码打印
        public static void printDFAC(string sn, string so, string plancode)
        {
            try
            {
                string khh = "";
                try
                {
                    khh = dataConn.GetValue("select khh from atpuplannameplate where bzso='" + so + "' and plan_code='" + plancode + "' and rownum=1");
                }
                catch
                {
                    khh = "";
                }
                string tempStr = "R8911*发动机带离合器总成*" + khh + "*" + DateTime.Now.ToString("yyMMdd") + "*" + sn + "*湖北省襄阳市*1***";
                StreamWriter Ts1;
                string TheFileName = "c:\\tmdydfac.txt";
                if (File.Exists(TheFileName))
                {
                    File.Delete(TheFileName);
                }
                Ts1 = new StreamWriter(TheFileName, false, Encoding.GetEncoding("gb2312"));
                Ts1.WriteLine("TM");
                Ts1.WriteLine("\"" + tempStr + "\"" + "," + "\"" + khh + "\"" + "," + "\"" + DateTime.Now.ToString("yyMMdd") + "\"" + "," + "\"" + so + "\"" + "," + "\"" + sn + "\"");
                Ts1.Close();
                //if (!Directory.Exists("C:\\Program Files\\lm8\\"))
                //{
                //    Directory.CreateDirectory("C:\\Program Files\\lm8\\");
                //}
                string path1 = "C:\\Program Files\\lm8";
                if (!Directory.Exists(path1))
                {
                    path1 = "C:\\Program Files (x86)\\LABEL MATRIX";
                }
                string run_exe = "LMWPRINT";
                string theExeStr1 = " /L=C:\\gttmdfac.QDF";
                System.Diagnostics.ProcessStartInfo p = null;
                System.Diagnostics.Process Proc;
                p = new ProcessStartInfo(run_exe, theExeStr1);
                p.WorkingDirectory = path1;//设置此外部程序所在windows目录
                p.WindowStyle = ProcessWindowStyle.Hidden;//在调用外部exe程序的时候，控制台窗口不弹出

                Proc = System.Diagnostics.Process.Start(p);//调用外部程序
            }
            catch
            {
                MessageBox.Show("请确认Label Matrix的目录,数量是否正确!", "错误提示");
            }
        }
        //DFAC条码打印
        public static void printDFAC1(string sn, string so, string khh, string scrq)
        {
            try
            {
                string tempStr = "R8911*发动机带离合器总成*" + khh + "*" + scrq + "*" + sn + "*湖北省襄阳市*1***";
                StreamWriter Ts1;
                string TheFileName = "c:\\tmdydfac.txt";
                if (File.Exists(TheFileName))
                {
                    File.Delete(TheFileName);
                }
                Ts1 = new StreamWriter(TheFileName, false, Encoding.GetEncoding("gb2312"));
                Ts1.WriteLine("TM");
                Ts1.WriteLine("\"" + tempStr + "\"" + "," + "\"" + khh + "\"" + "," + "\"" + scrq + "\"" + "," + "\"" + so + "\"" + "," + "\"" + sn + "\"");
                Ts1.Close();
                //if (!Directory.Exists("C:\\Program Files\\lm8\\"))
                //{
                //    Directory.CreateDirectory("C:\\Program Files\\lm8\\");
                //}
                string path1 = "C:\\Program Files\\lm8";
                if (!Directory.Exists(path1))
                {
                    path1 = "C:\\Program Files (x86)\\LABEL MATRIX";
                }
                string run_exe = "LMWPRINT";
                string theExeStr1 = " /L=C:\\gttmdfac.QDF";
                System.Diagnostics.ProcessStartInfo p = null;
                System.Diagnostics.Process Proc;
                p = new ProcessStartInfo(run_exe, theExeStr1);
                p.WorkingDirectory = path1;//设置此外部程序所在windows目录
                p.WindowStyle = ProcessWindowStyle.Hidden;//在调用外部exe程序的时候，控制台窗口不弹出

                Proc = System.Diagnostics.Process.Start(p);//调用外部程序
            }
            catch
            {
                MessageBox.Show("请确认Label Matrix的目录,数量是否正确!", "错误提示");
            }
        }
        //旅行车条码打印
        public static void printlxc(string sn, string so, string plancode)
        {
            try
            {
                string tempStr = "R8911*发动机*" + so + "*" + DateTime.Now.ToString("yyMMdd") + "*" + sn + "*湖北襄樊";
                StreamWriter Ts1;
                string TheFileName = "c:\\tmdydfac.txt";
                if (File.Exists(TheFileName))
                {
                    File.Delete(TheFileName);
                }
                Ts1 = new StreamWriter(TheFileName, false, Encoding.GetEncoding("gb2312"));
                Ts1.WriteLine("TM");
                Ts1.WriteLine("\"" + tempStr + "\"" + "," + "\"" + so + "\"" + "," + "\"" + DateTime.Now.ToString("yyMMdd") + "\"" + "," + "\"" + so + "\"" + "," + "\"" + sn + "\"");
                Ts1.Close();
                //if (!Directory.Exists("C:\\Program Files\\lm8\\"))
                //{
                //    Directory.CreateDirectory("C:\\Program Files\\lm8\\");
                //}
                string path1 = "C:\\Program Files\\lm8";
                if (!Directory.Exists(path1))
                {
                    path1 = "C:\\Program Files (x86)\\LABEL MATRIX";
                }
                string run_exe = "LMWPRINT";
                string theExeStr1 = " /L=C:\\gttmdfac.QDF";
                System.Diagnostics.ProcessStartInfo p = null;
                System.Diagnostics.Process Proc;
                p = new ProcessStartInfo(run_exe, theExeStr1);
                p.WorkingDirectory = path1;//设置此外部程序所在windows目录
                p.WindowStyle = ProcessWindowStyle.Hidden;//在调用外部exe程序的时候，控制台窗口不弹出

                Proc = System.Diagnostics.Process.Start(p);//调用外部程序
            }
            catch
            {
                MessageBox.Show("请确认Label Matrix的目录,数量是否正确!", "错误提示");
            }
        }
        //CPT条码打印
        public static void printCPT(string sn, string so, string plancode)
        {
            try
            {
                string tempStr = "C101DCE*发动机总成*" + so + "*" + DateTime.Now.ToString("yyMMdd") + "*" + sn + "*湖北襄阳*1";
                StreamWriter Ts1;
                string TheFileName = "c:\\tmdyCPT.txt";
                if (File.Exists(TheFileName))
                {
                    File.Delete(TheFileName);
                }
                Ts1 = new StreamWriter(TheFileName, false, Encoding.GetEncoding("gb2312"));
                Ts1.WriteLine("TM");
                Ts1.WriteLine("\"" + tempStr + "\"" + "," + "\"" + DateTime.Now.ToString("yyMMdd") + "\"" + "," + "\"" + so + "\"" + "," + "\"" + sn + "\"");
                Ts1.Close();
                //if (!Directory.Exists("C:\\Program Files\\lm8\\"))
                //{
                //    Directory.CreateDirectory("C:\\Program Files\\lm8\\");
                //}
                string path1 = "C:\\Program Files\\lm8";
                if (!Directory.Exists(path1))
                {
                    path1 = "C:\\Program Files (x86)\\LABEL MATRIX";
                }
                string run_exe = "LMWPRINT";
                string theExeStr1 = " /L=C:\\gttmCPT.QDF";
                System.Diagnostics.ProcessStartInfo p = null;
                System.Diagnostics.Process Proc;
                p = new ProcessStartInfo(run_exe, theExeStr1);
                p.WorkingDirectory = path1;//设置此外部程序所在windows目录
                p.WindowStyle = ProcessWindowStyle.Hidden;//在调用外部exe程序的时候，控制台窗口不弹出

                Proc = System.Diagnostics.Process.Start(p);//调用外部程序
            }
            catch
            {
                MessageBox.Show("请确认Label Matrix的目录,数量是否正确!", "错误提示");
            }
        }
        //分装条码
        public static void printFZTM(string sn, string so, string plancode)
        {
            try
            {
                string tempStr = "C101DCE*发动机总成*" + so + "*" + DateTime.Now.ToString("yyMMdd") + "*" + sn + "*湖北襄阳*1";
                StreamWriter Ts1;
                string TheFileName = "c:\\tmdyfztm.txt";
                if (File.Exists(TheFileName))
                {
                    File.Delete(TheFileName);
                }
                Ts1 = new StreamWriter(TheFileName, false, Encoding.GetEncoding("gb2312"));
                Ts1.WriteLine("TM");
                Ts1.WriteLine("\"" + sn + "\"" + "," + "\"" + so + "\"");
                Ts1.Close();
                //if (!Directory.Exists("C:\\Program Files\\lm8\\"))
                //{
                //    Directory.CreateDirectory("C:\\Program Files\\lm8\\");
                //}
                if (!Directory.Exists("C:\\Program Files\\lmw32\\"))
                {
                    Directory.CreateDirectory("C:\\Program Files\\lmw32\\");
                }
                string path1 = "C:\\Program Files\\lmw32";
                string run_exe = "LMWPRINT";
                string theExeStr1 = " /L=C:\\gttmfztm.QDF";
                System.Diagnostics.ProcessStartInfo p = null;
                System.Diagnostics.Process Proc;
                p = new ProcessStartInfo(run_exe, theExeStr1);
                p.WorkingDirectory = path1;//设置此外部程序所在windows目录
                p.WindowStyle = ProcessWindowStyle.Hidden;//在调用外部exe程序的时候，控制台窗口不弹出

                Proc = System.Diagnostics.Process.Start(p);//调用外部程序
            }
            catch
            {
                MessageBox.Show("请确认Label Matrix的目录,数量是否正确!", "错误提示");
            }
        }
        //CPT条码打印
        public static void printCPTW(string sn, string so, string plancode)
        {
            try
            {
                string tempStr = "C101DCE*发动机总成*" + so + "*" + DateTime.Now.ToString("yyMMdd") + "*" + sn + "*湖北襄阳*1";
                StreamWriter Ts1;
                string TheFileName = "c:\\tmdyCPT.txt";
                if (File.Exists(TheFileName))
                {
                    File.Delete(TheFileName);
                }
                Ts1 = new StreamWriter(TheFileName, false, Encoding.GetEncoding("gb2312"));
                Ts1.WriteLine("TM");
                Ts1.WriteLine("\"" + tempStr + "\"" + "," + "\"" + DateTime.Now.ToString("yyMMdd") + "\"" + "," + "\"" + so + "\"" + "," + "\"" + sn + "\"");
                Ts1.Close();
                //if (!Directory.Exists("C:\\Program Files\\lm8\\"))
                //{
                //    Directory.CreateDirectory("C:\\Program Files\\lm8\\");
                //}
                string path1 = "C:\\Program Files\\lm8";
                if (!Directory.Exists(path1))
                {
                    path1 = "C:\\Program Files (x86)\\LABEL MATRIX";
                }
                string run_exe = "LMWPRINT";
                string theExeStr1 = " /L=C:\\gttmCPT.QDF";
                System.Diagnostics.ProcessStartInfo p = null;
                System.Diagnostics.Process Proc;
                p = new ProcessStartInfo(run_exe, theExeStr1);
                p.WorkingDirectory = path1;//设置此外部程序所在windows目录
                p.WindowStyle = ProcessWindowStyle.Hidden;//在调用外部exe程序的时候，控制台窗口不弹出

                Proc = System.Diagnostics.Process.Start(p);//调用外部程序
            }
            catch
            {
                MessageBox.Show("请确认Label Matrix的目录,数量是否正确!", "错误提示");
            }
        }
        //徐工条码打印
        public static void printxg(string wlh, string gysdm, string sn)
        {
            try
            {
                //需要处理的信息 供应商+物料号+上线日期表示的五位批次+流水号后五位表示的流水号
                string thisPch = "", thisJx = "", thisSo = "" ;
                string sql = "select to_char(work_date,'yyyymmdd'),nvl(product_model,''),nvl(plan_so,'') from data_product where sn='" + sn + "' and rownum=1";
                System.Data.DataTable dt = dataConn.GetTable(sql);
                try
                {
                    thisPch = dt.Rows[0][0].ToString();
                    thisJx = dt.Rows[0][1].ToString();
                    thisSo = dt.Rows[0][2].ToString(); 
                }
                catch
                {
                    thisPch = "";
                    thisJx = "";
                    thisSo = "";
                }
                string s="";
                string tempStr = thisJx + "发动机" + s.PadLeft(37 - thisJx.Length);
                string ThisLsh=sn;
                tempStr = "*XCMG1" + wlh + s.PadLeft(3) + tempStr + "101325101053 " + thisPch + ThisLsh + s.PadLeft(2) + thisPch + thisSo + s.PadLeft(145);
                StreamWriter Ts1;
                string TheFileName = "c:\\tmdyxg.txt";
                if (File.Exists(TheFileName))
                {
                    File.Delete(TheFileName);
                }
                Ts1 = new StreamWriter(TheFileName, false, Encoding.GetEncoding("gb2312"));
                Ts1.WriteLine("TM");
                Ts1.WriteLine("\"" + tempStr + "\"" + "," + "\"" + wlh + "\"" + "," + "\"" + thisPch + ThisLsh + "\"");
                Ts1.Close();
                //if (!Directory.Exists("C:\\Program Files\\lm8\\"))
                //{
                //    Directory.CreateDirectory("C:\\Program Files\\lm8\\");
                //}
                string path1 = "C:\\Program Files\\lm8";
                if (!Directory.Exists(path1))
                {
                    path1 = "C:\\Program Files (x86)\\LABEL MATRIX";
                }
                string run_exe = "LMWPRINT";
                string theExeStr1 = " /L=C:\\gttmxg.QDF";
                System.Diagnostics.ProcessStartInfo p = null;
                System.Diagnostics.Process Proc;
                p = new ProcessStartInfo(run_exe, theExeStr1);
                p.WorkingDirectory = path1;//设置此外部程序所在windows目录
                p.WindowStyle = ProcessWindowStyle.Hidden;//在调用外部exe程序的时候，控制台窗口不弹出

                Proc = System.Diagnostics.Process.Start(p);//调用外部程序
            }
            catch
            {
                MessageBox.Show("请确认Label Matrix的目录,数量是否正确!", "错误提示");
            }
        }
        //环保公开信息二维码打印
        public static void PrintHbgkxx(string xzmc, string d20, string d21, string d22, string d24, string d25, string d26, string d27, string d28, string so1)
        {
            try
            {
                string tempStr = xzmc + d20 + d21 + d22 + d24 + d25 + d26 + d27 + d28;
                StreamWriter Ts1;
                string TheFileName = "c:\\tmdyhbxxgk.txt";
                if (File.Exists(TheFileName))
                {
                    File.Delete(TheFileName);
                }
                Ts1 = new StreamWriter(TheFileName, false, Encoding.GetEncoding("gb2312"));
                Ts1.WriteLine("TM");
                Ts1.WriteLine("\"" + tempStr + "\"" + "," + "\"" + so1 + "\"");
                Ts1.Close();
                string path1 = "C:\\Program Files\\lm8";
                if (!Directory.Exists(path1))
                {
                    path1 = "C:\\Program Files (x86)\\LABEL MATRIX";
                }
                string run_exe = "LMWPRINT";
                string theExeStr1 = " /L=C:\\gttmhbxxgk.QDF";
                System.Diagnostics.ProcessStartInfo p = null;
                System.Diagnostics.Process Proc;
                p = new ProcessStartInfo(run_exe, theExeStr1);
                p.WorkingDirectory = path1;//设置此外部程序所在windows目录
                p.WindowStyle = ProcessWindowStyle.Hidden;//在调用外部exe程序的时候，控制台窗口不弹出

                Proc = System.Diagnostics.Process.Start(p);//调用外部程序
            }
            catch
            {
                MessageBox.Show("请确认Label Matrix的目录,数量是否正确!", "错误提示");
            }
        }
        //东风商用车条码打印
        public static void printDFCVE(string sn, string so, string plancode)
        {
            try
            {
                string khh = "";
                try
                {
                    khh = dataConn.GetValue("select khh from atpuplannameplate where bzso='" + so + "' and plan_code='" + plancode + "' and rownum=1");
                }
                catch
                {
                    khh = "";
                }
                string tempStr = "8911*发动机总成*" + khh + "*" + DateTime.Now.ToString("yyMMdd") + "*" + sn + "*湖北襄阳市*1**";

                StreamWriter Ts1;
                string TheFileName = "c:\\tmdydfCV.txt";
                if (File.Exists(TheFileName))
                {
                    File.Delete(TheFileName);
                }
                Ts1 = new StreamWriter(TheFileName, false, Encoding.GetEncoding("gb2312"));
                Ts1.WriteLine("TM");
                Ts1.WriteLine("\"" + tempStr + "\"" + "," + "\"" + khh + "\"" + "," + "\"" + DateTime.Now.ToString("yyMMdd") + "\"" + "," + "\"" + so + "\"" + "," + "\"" + sn + "\"");
                Ts1.Close();
                //if (!Directory.Exists("C:\\Program Files\\lm8\\"))
                //{
                //    Directory.CreateDirectory("C:\\Program Files\\lm8\\");
                //}
                string path1 = "C:\\Program Files\\lm8";
                if (!Directory.Exists(path1))
                {
                    path1 = "C:\\Program Files (x86)\\LABEL MATRIX";
                }
                string run_exe = "LMWPRINT";
                string theExeStr1 = " /L=C:\\gttmdfCV.QDF";
                System.Diagnostics.ProcessStartInfo p = null;
                System.Diagnostics.Process Proc;
                p = new ProcessStartInfo(run_exe, theExeStr1);
                p.WorkingDirectory = path1;//设置此外部程序所在windows目录
                p.WindowStyle = ProcessWindowStyle.Hidden;//在调用外部exe程序的时候，控制台窗口不弹出

                Proc = System.Diagnostics.Process.Start(p);//调用外部程序
            }
            catch
            {
                MessageBox.Show("请确认Label Matrix的目录,数量是否正确!", "错误提示");
            }
        }
        //东风商用车条码打印
        public static void printDFCVW(string sn, string so, string plancode)
        {
            try
            {
                string khh = "";
                try
                {
                    khh = dataConn.GetValue("select khh from atpuplannameplate where bzso='" + so + "' and plan_code='" + plancode + "' and rownum=1");
                }
                catch
                {
                    khh = "";
                }
                string tempStr = "8911*发动机总成*" + khh + "*" + DateTime.Now.ToString("yyMMdd") + "*" + sn + "*湖北襄阳市*1**";

                StreamWriter Ts1;
                string TheFileName = "c:\\tmdydfCV.txt";
                if (File.Exists(TheFileName))
                {
                    File.Delete(TheFileName);
                }
                Ts1 = new StreamWriter(TheFileName, false, Encoding.GetEncoding("gb2312"));
                Ts1.WriteLine("TM");
                Ts1.WriteLine("\"" + tempStr + "\"" + "," + "\"" + khh + "\"" + "," + "\"" + DateTime.Now.ToString("yyMMdd") + "\"" + "," + "\"" + so + "\"" + "," + "\"" + sn + "\"");
                Ts1.Close();
                //if (!Directory.Exists("C:\\Program Files\\lm8\\"))
                //{
                //    Directory.CreateDirectory("C:\\Program Files\\lm8\\");
                //}
                string path1 = "C:\\Program Files\\lm8";
                if (!Directory.Exists(path1))
                {
                    path1 = "C:\\Program Files (x86)\\LABEL MATRIX";
                }
                string run_exe = "LMWPRINT";
                string theExeStr1 = " /L=C:\\gttmdfCV.QDF";
                System.Diagnostics.ProcessStartInfo p = null;
                System.Diagnostics.Process Proc;
                p = new ProcessStartInfo(run_exe, theExeStr1);
                p.WorkingDirectory = path1;//设置此外部程序所在windows目录
                p.WindowStyle = ProcessWindowStyle.Hidden;//在调用外部exe程序的时候，控制台窗口不弹出

                Proc = System.Diagnostics.Process.Start(p);//调用外部程序
            }
            catch
            {
                MessageBox.Show("请确认Label Matrix的目录,数量是否正确!", "错误提示");
            }
        }
        //东风商用车条码打印
        public static void printDFCVE1(string sn, string so, string khh,string scrq)
        {
            try
            {
                string tempStr = "8911*发动机总成*" + khh + "*" + scrq + "*" + sn + "*湖北襄阳市*1**";

                StreamWriter Ts1;
                string TheFileName = "c:\\tmdydfCV.txt";
                if (File.Exists(TheFileName))
                {
                    File.Delete(TheFileName);
                }
                Ts1 = new StreamWriter(TheFileName, false, Encoding.GetEncoding("gb2312"));
                Ts1.WriteLine("TM");
                Ts1.WriteLine("\"" + tempStr + "\"" + "," + "\"" + khh + "\"" + "," + "\"" + scrq + "\"" + "," + "\"" + so + "\"" + "," + "\"" + sn + "\"");
                Ts1.Close();
                //if (!Directory.Exists("C:\\Program Files\\lm8\\"))
                //{
                //    Directory.CreateDirectory("C:\\Program Files\\lm8\\");
                //}
                string path1 = "C:\\Program Files\\lm8";
                if (!Directory.Exists(path1))
                {
                    path1 = "C:\\Program Files (x86)\\LABEL MATRIX";
                }
                string run_exe = "LMWPRINT";
                string theExeStr1 = " /L=C:\\gttmdfCV.QDF";
                System.Diagnostics.ProcessStartInfo p = null;
                System.Diagnostics.Process Proc;
                p = new ProcessStartInfo(run_exe, theExeStr1);
                p.WorkingDirectory = path1;//设置此外部程序所在windows目录
                p.WindowStyle = ProcessWindowStyle.Hidden;//在调用外部exe程序的时候，控制台窗口不弹出

                Proc = System.Diagnostics.Process.Start(p);//调用外部程序
            }
            catch
            {
                MessageBox.Show("请确认Label Matrix的目录,数量是否正确!", "错误提示");
            }
        }
        //东风创普条码打印
        public static void printDFCP(string sn, string so, string plancode)
        {
            try
            {
                string khh = "";
                try
                {
                    khh = dataConn.GetValue("select khh from atpuplannameplate where bzso='" + so + "' and plan_code='" + plancode + "' and rownum=1");
                }
                catch
                {
                    khh = "";
                }
                string tempStr = "R8911*发动机总成*" + khh + "*" + DateTime.Now.ToString("yyMMdd") + "*" + sn + "*湖北襄阳市*1**";

                StreamWriter Ts1;
                string TheFileName = "c:\\tmdydfcp.txt";
                if (File.Exists(TheFileName))
                {
                    File.Delete(TheFileName);
                }
                Ts1 = new StreamWriter(TheFileName, false, Encoding.GetEncoding("gb2312"));
                Ts1.WriteLine("TM");
                Ts1.WriteLine("\"" + tempStr + "\"" + "," + "\"" + khh + "\"" + "," + "\"" + DateTime.Now.ToString("yyMMdd") + "\"" + "," + "\"" + sn + "\"");
                Ts1.WriteLine("\"" + tempStr + "\"" + "," + "\"" + khh + "\"" + "," + "\"" + DateTime.Now.ToString("yyMMdd") + "\"" + "," + "\"" + sn + "\"");
                Ts1.Close();
                //if (!Directory.Exists("C:\\Program Files\\lm8\\"))
                //{
                //    Directory.CreateDirectory("C:\\Program Files\\lm8\\");
                //}
                string path1 = "C:\\Program Files\\lm8";
                if (!Directory.Exists(path1))
                {
                    path1 = "C:\\Program Files (x86)\\LABEL MATRIX";
                }
                string run_exe = "LMWPRINT";
                string theExeStr1 = " /L=C:\\gttmdfcp.QDF";
                System.Diagnostics.ProcessStartInfo p = null;
                System.Diagnostics.Process Proc;
                p = new ProcessStartInfo(run_exe, theExeStr1);
                p.WorkingDirectory = path1;//设置此外部程序所在windows目录
                p.WindowStyle = ProcessWindowStyle.Hidden;//在调用外部exe程序的时候，控制台窗口不弹出

                Proc = System.Diagnostics.Process.Start(p);//调用外部程序
            }
            catch
            {
                MessageBox.Show("请确认Label Matrix的目录,数量是否正确!", "错误提示");
            }
        }
        //东风创普条码打印
        public static void printDFCP1(string sn, string so, string khh, string scrq)
        {
            try
            {
                string tempStr = "R8911*发动机总成*" + khh + "*" + DateTime.Now.ToString("yyMMdd") + "*" + sn + "*湖北襄阳市*1**";

                StreamWriter Ts1;
                string TheFileName = "c:\\tmdydfcp.txt";
                if (File.Exists(TheFileName))
                {
                    File.Delete(TheFileName);
                }
                Ts1 = new StreamWriter(TheFileName, false, Encoding.GetEncoding("gb2312"));
                Ts1.WriteLine("TM");
                Ts1.WriteLine("\"" + tempStr + "\"" + "," + "\"" + khh + "\"" + "," + "\"" + scrq + "\"" + "," + "\"" + sn + "\"");
                Ts1.Close();
                //if (!Directory.Exists("C:\\Program Files\\lm8\\"))
                //{
                //    Directory.CreateDirectory("C:\\Program Files\\lm8\\");
                //}
                string path1 = "C:\\Program Files\\lm8";
                if (!Directory.Exists(path1))
                {
                    path1 = "C:\\Program Files (x86)\\LABEL MATRIX";
                }
                string run_exe = "LMWPRINT";
                string theExeStr1 = " /L=C:\\gttmdfcp.QDF";
                System.Diagnostics.ProcessStartInfo p = null;
                System.Diagnostics.Process Proc;
                p = new ProcessStartInfo(run_exe, theExeStr1);
                p.WorkingDirectory = path1;//设置此外部程序所在windows目录
                p.WindowStyle = ProcessWindowStyle.Hidden;//在调用外部exe程序的时候，控制台窗口不弹出

                Proc = System.Diagnostics.Process.Start(p);//调用外部程序
            }
            catch
            {
                MessageBox.Show("请确认Label Matrix的目录,数量是否正确!", "错误提示");
            }
        }
        //东风专底条码打印
        public static void printDFZD(string sn, string so, string plancode)
        {
            try
            {
                string khh = "";
                try
                {
                    khh = dataConn.GetValue("select khh from atpuplannameplate where bzso='" + so + "' and plan_code='" + plancode + "' and rownum=1");
                }
                catch
                {
                    khh = "";
                }
                string tempStr = "8911*发动机总成*" + khh + "*" + DateTime.Now.ToString("yyMMdd") + "*" + sn + "*湖北襄阳市*1**";

                StreamWriter Ts1;
                string TheFileName = "c:\\tmdydfzd.txt";
                if (File.Exists(TheFileName))
                {
                    File.Delete(TheFileName);
                }
                Ts1 = new StreamWriter(TheFileName, false, Encoding.GetEncoding("gb2312"));
                Ts1.WriteLine("TM");
                Ts1.WriteLine("\"" + tempStr + "\"" + "," + "\"" + DateTime.Now.ToString("yyMMdd") + "\"" + "," + "\"" + so + "\"" + "," + "\"" + sn + "\"");
                Ts1.Close();
                //if (!Directory.Exists("C:\\Program Files\\lm8\\"))
                //{
                //    Directory.CreateDirectory("C:\\Program Files\\lm8\\");
                //}
                string path1 = "C:\\Program Files\\lm8";
                if (!Directory.Exists(path1))
                {
                    path1 = "C:\\Program Files (x86)\\LABEL MATRIX";
                }
                string run_exe = "LMWPRINT";
                string theExeStr1 = " /L=C:\\gttmdfzd.QDF";
                System.Diagnostics.ProcessStartInfo p = null;
                System.Diagnostics.Process Proc;
                p = new ProcessStartInfo(run_exe, theExeStr1);
                p.WorkingDirectory = path1;//设置此外部程序所在windows目录
                p.WindowStyle = ProcessWindowStyle.Hidden;//在调用外部exe程序的时候，控制台窗口不弹出

                Proc = System.Diagnostics.Process.Start(p);//调用外部程序
            }
            catch
            {
                MessageBox.Show("请确认Label Matrix的目录,数量是否正确!", "错误提示");
            }
        }
        //厦门金龙条码打印
        public static void printXMJL(string sn, string so, string plancode,string thisJx)
        {
            try
            {
                string khh = "";
                try
                {
                    khh = dataConn.GetValue("select khh from atpuplannameplate where bzso='" + so + "' and plan_code='" + plancode + "' and rownum=1");
                }
                catch
                {
                    khh = "";
                }
                string tempStr = "XXX@B@60711@" + khh + "@@@1@" + so + " " + sn + "@@@@";
                StreamWriter Ts1;
                string TheFileName = "c:\\tmdyxmjl.txt";
                if (File.Exists(TheFileName))
                {
                    File.Delete(TheFileName);
                }
                Ts1 = new StreamWriter(TheFileName, false, Encoding.GetEncoding("gb2312"));
                Ts1.WriteLine("TM");
                Ts1.WriteLine("\"" + tempStr + "\"" + "," + "\"" + khh + "\"" + "," + "\"" + so + "\"" + "," + "\"" + thisJx + "\"" + "," + "\"" + sn + "\"");
                Ts1.WriteLine("\"" + tempStr + "\"" + "," + "\"" + khh + "\"" + "," + "\"" + so + "\"" + "," + "\"" + thisJx + "\"" + "," + "\"" + sn + "\"");
                Ts1.WriteLine("\"" + tempStr + "\"" + "," + "\"" + khh + "\"" + "," + "\"" + so + "\"" + "," + "\"" + thisJx + "\"" + "," + "\"" + sn + "\"");
                Ts1.Close();
                //if (!Directory.Exists("C:\\Program Files\\lm8\\"))
                //{
                //    Directory.CreateDirectory("C:\\Program Files\\lm8\\");
                //}
                string path1 = "C:\\Program Files\\lm8";
                if (!Directory.Exists(path1))
                {
                    path1 = "C:\\Program Files (x86)\\LABEL MATRIX";
                }
                string run_exe = "LMWPRINT";
                string theExeStr1 = " /L=C:\\gttmxmjl.QDF";
                System.Diagnostics.ProcessStartInfo p = null;
                System.Diagnostics.Process Proc;
                p = new ProcessStartInfo(run_exe, theExeStr1);
                p.WorkingDirectory = path1;//设置此外部程序所在windows目录
                p.WindowStyle = ProcessWindowStyle.Hidden;//在调用外部exe程序的时候，控制台窗口不弹出

                Proc = System.Diagnostics.Process.Start(p);//调用外部程序
            }
            catch
            {
                MessageBox.Show("请确认Label Matrix的目录,数量是否正确!", "错误提示");
            }
        }
        //三一汽车起重机械有限公司
        public static void printSYQZJ(string sn, string so, string plancode, string thisJx)
        {
            try
            {
                string wlh = "";
                try
                {
                    wlh = dataConn.GetValue("select wlh from atpukhtm where so='" + so + "' and rownum=1");
                }
                catch
                {
                    wlh = "";
                }
                string tempStr = wlh + "|2993aa3af9ae91a77e422822773666c0|" + so + "|" + sn;
                StreamWriter Ts1;
                string TheFileName = "c:\\tmdysyqzj.txt";
                if (File.Exists(TheFileName))
                {
                    File.Delete(TheFileName);
                }
                Ts1 = new StreamWriter(TheFileName, false, Encoding.GetEncoding("gb2312"));
                Ts1.WriteLine("TM");
                Ts1.WriteLine("\"" + tempStr + "\"" + "," + "\"" + wlh + "\"" + "," + "\"" + thisJx + "\"" + "," + "\"" + sn + "\"");
                Ts1.Close();
                //if (!Directory.Exists("C:\\Program Files\\lm8\\"))
                //{
                //    Directory.CreateDirectory("C:\\Program Files\\lm8\\");
                //}
                string path1 = "C:\\Program Files\\lm8";
                if (!Directory.Exists(path1))
                {
                    path1 = "C:\\Program Files (x86)\\LABEL MATRIX";
                }
                string run_exe = "LMWPRINT";
                string theExeStr1 = " /L=C:\\gttmSYQZJ.QDF";
                System.Diagnostics.ProcessStartInfo p = null;
                System.Diagnostics.Process Proc;
                p = new ProcessStartInfo(run_exe, theExeStr1);
                p.WorkingDirectory = path1;//设置此外部程序所在windows目录
                p.WindowStyle = ProcessWindowStyle.Hidden;//在调用外部exe程序的时候，控制台窗口不弹出

                Proc = System.Diagnostics.Process.Start(p);//调用外部程序
            }
            catch
            {
                MessageBox.Show("请确认Label Matrix的目录,数量是否正确!", "错误提示");
            }
        }
        //东风神宇
        public static void printDFSY(string sn, string so, string plancode, string thisJx)
        {
            try
            {
                string wlh = "";
                try
                {
                    wlh = dataConn.GetValue("select wlh from atpukhtm where so='" + so + "' and rownum=1");
                }
                catch
                {
                    wlh = "";
                }
                int i = 65 + (Convert.ToInt32(DateTime.Now.ToString("yyyy")) - 2010) % 26;
                string Scrq = ((char)Convert.ToInt32(i.ToString())).ToString() + DateTime.Now.ToString("MM") + DateTime.Now.ToString("dd");
                string tempStr = "FD23*发动机总成*" + so + "*" + Scrq + "*" + sn + "*湖北襄阳*1";
                StreamWriter Ts1;
                string TheFileName = "c:\\tmdydfsy.txt";
                if (File.Exists(TheFileName))
                {
                    File.Delete(TheFileName);
                }
                Ts1 = new StreamWriter(TheFileName, false, Encoding.GetEncoding("gb2312"));
                Ts1.WriteLine("TM");
                Ts1.WriteLine("\"" + tempStr + "\"" + "," + "\"" + so + "\"" + "," + "\"" + Scrq + "\"" + "," + "\"" + sn + "\"");
                Ts1.Close();
                //if (!Directory.Exists("C:\\Program Files\\lm8\\"))
                //{
                //    Directory.CreateDirectory("C:\\Program Files\\lm8\\");
                //}
                string path1 = "C:\\Program Files\\lm8";
                if (!Directory.Exists(path1))
                {
                    path1 = "C:\\Program Files (x86)\\LABEL MATRIX";
                }
                string run_exe = "LMWPRINT";
                string theExeStr1 = " /L=C:\\gttmDFSY.QDF";
                System.Diagnostics.ProcessStartInfo p = null;
                System.Diagnostics.Process Proc;
                p = new ProcessStartInfo(run_exe, theExeStr1);
                p.WorkingDirectory = path1;//设置此外部程序所在windows目录
                p.WindowStyle = ProcessWindowStyle.Hidden;//在调用外部exe程序的时候，控制台窗口不弹出

                Proc = System.Diagnostics.Process.Start(p);//调用外部程序
            }
            catch
            {
                MessageBox.Show("请确认Label Matrix的目录,数量是否正确!", "错误提示");
            }
        }
        //分装条码打印
        public static void PrintSub(string Thislsh, string ThisSo,string item )
        {
            try
            {
                StreamWriter Ts1;
                string TheFileName = "c:\\tmdysub.txt";
                if (File.Exists(TheFileName))
                {
                    File.Delete(TheFileName);
                }
                Ts1 = new StreamWriter(TheFileName, false, Encoding.GetEncoding("gb2312"));
                Ts1.WriteLine("TM");
                Ts1.WriteLine(item);
                Ts1.Close();
                if (!Directory.Exists("C:\\Program Files\\lmw32\\"))
                {
                    Directory.CreateDirectory("C:\\Program Files\\lmw32\\");
                }
                string path1 = "C:\\Program Files\\lmw32";
                string run_exe = "LMWPRINT";
                string theExeStr1 = " /L=C:\\gttmsub.QDF";
                System.Diagnostics.ProcessStartInfo p = null;
                System.Diagnostics.Process Proc;
                p = new ProcessStartInfo(run_exe, theExeStr1);
                p.WorkingDirectory = path1;//设置此外部程序所在windows目录
                p.WindowStyle = ProcessWindowStyle.Hidden;//在调用外部exe程序的时候，控制台窗口不弹出

                Proc = System.Diagnostics.Process.Start(p);//调用外部程序
            }
            catch (Exception e1)
            {
                MessageBox.Show("请确认Label Matrix的目录,数量是否正确!", "错误提示");
            }
        }

        /// <summary>
        /// 清除一个对象的某个事件所挂钩的delegate
        /// </summary>
        /// <param name="ctrl">控件对象</param>
        /// <param name="eventName">事件名称，默认的</param>
        public static void ClearEvents(object ctrl, string eventName = "_EventAll")
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

        //ECM条码打印
        public static void PrintDpTm(string sn,string so,string jx,string unitno,string ecm,string plancode,string frflag)
        {
            try
            {
                string str1="";
                StreamWriter Ts1;
                string TheFileName = "C:\\Data Tag Log File\\ecmtm.txt";
                if (File.Exists(TheFileName))
                {
                    File.Delete(TheFileName);
                }
                Ts1 = new StreamWriter(TheFileName, false, Encoding.GetEncoding("gb2312"));
                Ts1.WriteLine("TM");
                Ts1.WriteLine("\"" + sn + "\"" + "," + "\"" + so.Substring(2, so.Length-2) + "\"" + "," + "\"" + jx + "\"" + "," + "\"" + unitno + "\"" + "," + "\"" + ecm + "\"" + "," + "\"" + plancode + "\"");
                Ts1.Close();
                if (!Directory.Exists("C:\\Program Files\\lmw32\\"))
                {
                    Directory.CreateDirectory("C:\\Program Files\\lmw32\\");
                }
                string path1 = "C:\\Program Files\\lmw32\\";
                string run_exe = "";
                string theExeStr1 = "";

                //从服务器拷贝模板
                //File.Delete("C:\\Data Tag Log File\\ecmtmjjbbd.QDF");
                //File.Delete("C:\\Data Tag Log File\\ecmtmsgbd.QDF");
                //File.Delete("C:\\Data Tag Log File\\ecmtm.QDF");
                //File.Copy(@"\\192.168.112.144\MES共享\模板\manta\Data Tag Log File\ecmtmjjbbd.QDF", @"C:\\Data Tag Log File\ecmtmjjbbd.QDF");
                //File.Copy(@"\\192.168.112.144\MES共享\模板\manta\Data Tag Log File\ecmtmsgbd.QDF", @"C:\\Data Tag Log File\ecmtmsgbd.QDF");
                //File.Copy(@"\\192.168.112.144\MES共享\模板\manta\Data Tag Log File\ecmtm.QDF", @"C:\\Data Tag Log File\ecmtm.QDF");

                if (frflag == "1")
                {
                    str1 = "N";
                    path1 = path1 + "LMWPRINT /L=" + "C:\\Data Tag Log File\\ecmtmjjbbd.QDF";
                }
                else if (frflag == "2")
                {
                    path1 = path1 + "LMWPRINT /L=" + "C:\\Data Tag Log File\\ecmtmsgbd.QDF";
                }
                else
                {
                    path1 = path1 + "LMWPRINT /L=" + "C:\\Data Tag Log File\\ecmtm.QDF";
                }
                Microsoft.VisualBasic.Interaction.Shell(path1,AppWinStyle.NormalFocus);

                string sql = "update sjecmgztm set handle_flag='Y', dyrqsj=sysdate where ghtm='" + sn + "' and gzdd='" + LoginInfo.ProductLineInfo.PLINE_CODE + "' and handle_flag='N'";
                dataConn.ExeSql(sql);

                sql = "select count(*) from atpuecmlsh where ghtm='" + sn + "'";
                string str2 = dataConn.GetValue(sql);
                if (str2 != "0")
                {
                    sql = "update atpuecmlsh set prt1='1',rqsj1=sysdate,ecm='" + ecm + "',unitno='" + unitno + "',ygmc1='" + LoginInfo.UserInfo.USER_NAME + "',zdmc1='" + LoginInfo.StationInfo.STATION_NAME + "',sc=is_jhsc('" + plancode + "','" + sn + "') where ghtm='" + sn + "'";
                }
                else
                {
                    sql = "insert into atpuecmlsh(ghtm,prt1,rqsj1,ecm,unitno,ygmc1,zdmc1,sc,insite_bd) values('" + sn + "','1',sysdate,'" + ecm + "','" + unitno + "','" + LoginInfo.UserInfo.USER_NAME + "','" + LoginInfo.StationInfo.STATION_NAME + "',is_jhsc('" + plancode + "','" + sn + "'),'" + str1 + "')";
                }
                dataConn.ExeSql(sql);
                ProductDataFactory.Station_Start(plancode, sn, "Y");
                Handle_Unitno();

                //File.Delete("C:\\Data Tag Log File\\ecmtmjjbbd.QDF");
                //File.Delete("C:\\Data Tag Log File\\ecmtmsgbd.QDF");
                //File.Delete("C:\\Data Tag Log File\\ecmtm.QDF");
            }
            catch
            {
                MessageBox.Show("请确认Label Matrix的目录,数量是否正确!", "错误提示");
            }
        }
        public static void Handle_Unitno()
        {
            try
            {
                string strFileName = "",thisLsh="",sql="";
                if (!Directory.Exists("D:\\EPAT_DATA_MANTA\\"))
                {
                    Directory.CreateDirectory("D:\\EPAT_DATA_MANTA\\");
                }
                string[] filenames = Directory.GetFileSystemEntries("D:\\EPAT_DATA_MANTA");
                if (filenames.Length == 0)
                {}
                else
                {
                    foreach (string file in filenames)// 遍历所有的文件和目录
                    {
                        strFileName = file;
                        if (strFileName.Substring(0, 4) == "DCEC")
                        {
                            thisLsh = strFileName.Substring(5, 8);
                            sql = "select sn from data_product where sn='"+thisLsh+"' ";
                            System.Data.DataTable dt = dataConn.GetTable(sql);
                            if (dt.Rows.Count > 0)
                            {
                                string Ts1 = File.ReadAllText("D:\\EPAT_DATA_MANTA\\" + strFileName);
                                int j = Ts1.IndexOf(",P,");
                                if (j > 0)
                                {
                                    int i = Ts1.IndexOf("UNIT,");
                                    string s2 = Ts1.Substring(i+5,4);
                                    dataConn.ExeSql("INSERT INTO GHTMUNITNO(GHTM,UNITNO,RQSJ) VALUES('" + thisLsh + "','" + s2 + "',SYSDATE)");
                                }
                            }
                            FileInfo fi = new FileInfo("D:\\EPAT_DATA_MANTA\\" + strFileName);
                            fi.MoveTo(Path.Combine("D:\\EPAT_DATA_MANTA\\R-" + strFileName));

                        }
                    }
                }
            }
            catch
            {
                MessageBox.Show("处理UNITNO出错");
            }
        }


        //ECM条码打印
        public static void PrintDPNP(string pn, string sn, string ec, string dc1, string lsh2, string plancode)
        {
            try
            {
                string sql;
                StreamWriter Ts1;
                string TheFileName = "C:\\ecmnp.txt";
                if (File.Exists(TheFileName))
                {
                    File.Delete(TheFileName);
                }
                Ts1 = new StreamWriter(TheFileName, false, Encoding.GetEncoding("gb2312"));
                Ts1.WriteLine("TM");
                Ts1.WriteLine("" + pn + "" + "," + "" + sn + "" + "," + "" + ec + "" + "," + "" + dc1 + "" + "," + "" + lsh2 + "" );
                Ts1.WriteLine("" + pn + "" + "," + "" + sn + "" + "," + "" + ec + "" + "," + "" + dc1 + "" + "," + "" + lsh2 + "");
                Ts1.Close();
                if (!Directory.Exists("C:\\Program Files\\lmw32\\"))
                {
                    Directory.CreateDirectory("C:\\Program Files\\lmw32\\");
                }
                string path1 = "C:\\Program Files\\lmw32\\";

                path1 = path1 + "LMWPRINT /L=" + "C:\\ecmnp.QDF";
               
                Microsoft.VisualBasic.Interaction.Shell(path1, AppWinStyle.NormalFocus);

                sql = "select count(*) from atpuecmlsh where ghtm='" + lsh2 + "'";
                string str2 = dataConn.GetValue(sql);
                if (str2 != "0")
                {
                    sql = "update atpuecmlsh set prt2='1',rqsj2=sysdate,ygmc2='" + LoginInfo.UserInfo.USER_NAME + "',pn='" + pn + "',sn='" + sn + "',ec='" + ec + "',dc='" + dc1 + "',zdmc2='" + LoginInfo.StationInfo.STATION_NAME + "' where ghtm='" + lsh2 + "'";
                }
                else
                {
                    sql = "insert into atpuecmlsh(ghtm,prt2,rqsj2,ygmc2,pn,sn,ec,dc,zdmc2) values('" + lsh2 + "','1',sysdate,'" + LoginInfo.UserInfo.USER_NAME + "','" + pn + "','" + sn + "','" + ec + "','" + dc1 + "','" + LoginInfo.StationInfo.STATION_NAME + "')";
                }
                dataConn.ExeSql(sql);

            }
            catch
            {
                MessageBox.Show("请确认Label Matrix的目录,数量是否正确!", "错误提示");
            }
        }
        //
        public static void Handle_Ecm(string thisFile)
        {
            string line = "";
            try
            {
                if (File.Exists(thisFile))
                {
                    string s1 = File.ReadAllText(thisFile);
                    string[] filelist = File.ReadAllLines(thisFile, Encoding.Default);
                    for (int i = 0; i < filelist.Length; i++) //倒序读取文本
                    {
                        line = filelist[i];
                        if (line != "" && line !="TM")
                        {
                            string[] strAry = line.Split(',');
                            string msg = "";
                            if (strAry[8].Trim().ToUpper().Contains("CM2220") || strAry[8].Trim().ToUpper().Contains("CM2150"))
                            {
                                msg = "Continental";
                            }
                            else if (strAry[8].Trim().ToUpper().Contains("CM2880"))
                            {
                                msg = "Cummins";
                            }
                            else
                            {
                                msg = "";
                            }
                            string sql = "select count(*) from atpuecmlsh where ghtm='" + strAry[0].Trim() + "'";
                            string str2 = dataConn.GetValue(sql);
                            if (str2 != "0")
                            {
                                sql = "update atpuecmlsh set prt2='1',rqsj2=sysdate,ygmc2='" + LoginInfo.UserInfo.USER_NAME + "',pn='" + strAry[1].Trim() + "',sn='" + strAry[2].Trim() + "',ec='" + strAry[4].Trim() + "',dc='" + strAry[3].Trim() + "',ecm_type='" + strAry[8].Trim() + "',zdmc2='" + LoginInfo.StationInfo.STATION_NAME + "',mfg='" + msg + "' where ghtm='" + strAry[0].Trim() + "' ";
                            }
                            else
                            {
                                sql = "insert into atpuecmlsh(ghtm,prt2,rqsj2,ygmc2,pn,sn,ec,dc,ecm_type,zdmc2,mfg) values('" + strAry[0].Trim() + "','1',sysdate,'" + LoginInfo.UserInfo.USER_NAME + "','" + strAry[1].Trim() + "','" + strAry[2].Trim() + "','" + strAry[4].Trim() + "','" + strAry[3].Trim() + "','" + strAry[8].Trim() + "','" + LoginInfo.StationInfo.STATION_NAME + "','" + msg + "')";
                            }
                            dataConn.ExeSql(sql);



                        }
                    }
                }
                else
                {
                    return;
                }
            }
            catch
            {
                dataConn.ExeSql("insert into ecm_data_log(ecm_data,add_time) values('" + line + "',sysdate)");
            }
        }

        //清单打印
        public static void PrintRstTable(string Caption,string sql,int output,string sn,string so)
        {
            Microsoft.Office.Interop.Excel.Application apps = new Microsoft.Office.Interop.Excel.Application();
            string filename = "C:\\Temp.xls";
            if (File.Exists(filename))
            {
                File.Delete(filename);
            }
            object oMissing = System.Reflection.Missing.Value;

            Workbook _wkbook = apps.Workbooks.Open(filename, 0, true, 5, oMissing, oMissing, true, 1, oMissing, false, false, oMissing, false, oMissing, oMissing);
            Microsoft.Office.Interop.Excel._Worksheet _wksheet = _wkbook.Sheets[Caption];

            int IdxCount = 1;
            _wksheet.Cells[IdxCount, 1] = Caption;
            _wksheet.Range["A1"].Select();
            _wksheet.Range["A1:E1"].MergeCells = true;
            _wksheet.Range["A1:E1"].Font.Name = "楷体_GB2312";
            _wksheet.Range["A1:E1"].Font.FontStyle = "加粗";
            _wksheet.Range["A1:E1"].Font.Size = 16;
            _wksheet.Range["A1:E1"].HorizontalAlignment = HorizontalAlignment.Center;
            _wksheet.Range["A1:E1"].VerticalAlignment = HorizontalAlignment.Center;

            _wksheet.Columns["B:B"].ColumnWidth = 18;
            _wksheet.Columns["C:C"].ColumnWidth = 26;
            _wksheet.Columns["D:D"].ColumnWidth = 6;
            _wksheet.Columns["E:E"].ColumnWidth = 6;
            _wksheet.Range["A3"].Value = "工位";
            _wksheet.Range["B3"].Value = "零件号";
            _wksheet.Range["C3"].Value = "零件名称";
            _wksheet.Range["D3"].Value = "数量";
            _wksheet.Range["E3"].Value = "工序";

            _wksheet.Range["A2:B2"].MergeCells = true;
            _wksheet.Range["C2:E2"].MergeCells = true;

            IdxCount = IdxCount + 1;
            _wksheet.Range["A2"].Value = "流水号：" + sn;
            _wksheet.Range["C2"].Value = "SO号：" + so;
            IdxCount = IdxCount + 1;

            System.Data.DataTable dt = dataConn.GetTable(sql);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                IdxCount = IdxCount + 1;
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    if (!string.IsNullOrEmpty(dt.Rows[i][j].ToString()))
                    {
                        _wksheet.Cells[IdxCount, j + 1] = "'" + dt.Rows[i][j].ToString();
                    }
                }
            }
            _wksheet.Columns["A:A"].EntireColumn.AutoFit();
            _wksheet.Columns["A:A"].ColumnWidth = _wksheet.Columns["A:A"].ColumnWidth + 1;
            IdxCount = IdxCount + 1;
            _wksheet.Cells[IdxCount, 1] = "打印时间为" + DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString();
            IdxCount = IdxCount + 1;
            _wksheet.Cells[IdxCount, 1] = "打印人：" + LoginInfo.UserInfo.USER_NAME;

            if (output == 1)
            {
                _wksheet.PageSetup.PrintGridlines = true;
                _wksheet.PageSetup.CenterFooter = "&9" + (char)10 + "" + (char)10 + "第 &P 页，共 &N 页";

                _wksheet.PageSetup.LeftMargin = apps.InchesToPoints(1.28740157480315);
                _wksheet.PageSetup.RightMargin = apps.InchesToPoints(0.393700787401575);
                _wksheet.PageSetup.TopMargin = apps.InchesToPoints(0.393700787401575);
                _wksheet.PageSetup.BottomMargin = apps.InchesToPoints(0.393700787401575);
                _wksheet.PageSetup.HeaderMargin = apps.InchesToPoints(0.393700787401575);
                _wksheet.PageSetup.FooterMargin = apps.InchesToPoints(0.393700787401575);
                _wksheet.PrintPreview(null);
                _wkbook.Saved = true;
                _wkbook.Close(0, oMissing, oMissing);
                apps.Quit();
                GC.Collect();
            }
            else
            {
                apps.Visible = true;
                _wksheet.Visible = Microsoft.Office.Interop.Excel.XlSheetVisibility.xlSheetVisible;
                _wksheet.Activate();
            }
            _wksheet = null;
            _wkbook = null;
            apps = null;
        }













    }
}
