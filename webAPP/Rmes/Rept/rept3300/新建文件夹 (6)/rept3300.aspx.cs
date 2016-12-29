using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;

using Rmes.Web.Base;
using Rmes.Pub.Data;
using Rmes.DA.Base;
using Rmes.DA.Procedures;

using DevExpress.Web.ASPxEditors;
using DevExpress.Web.ASPxGridView;
using DevExpress.XtraGrid;
using System.Drawing;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraPrinting;
using DevExpress.Web.ASPxGridView.Export;
using Microsoft.Office.Interop.Excel;
using System.Reflection;
using System.IO;
using System.Windows.Forms;

/**
 * 功能概述：改制BOM对比
 * 作者：任海
 * 创建时间：2016-10-14
 */
namespace Rmes.WebApp.Rmes.Rept.rept3300
{
    public partial class rept3300 : BasePage
    {
        private dataConn dc = new dataConn();
        public string theProgramCode;
        public string theCompanyCode;
        private string theUserId, theUserCode;

        protected void Page_Load(object sender, EventArgs e)
        {
            theProgramCode = "rept3300";
            userManager theUserManager = (userManager)Session["theUserManager"];
            theCompanyCode = theUserManager.getCompanyCode();
            theUserId = theUserManager.getUserId();
            theUserCode = theUserManager.getUserCode();
            //test
            //theUserCode = "ZZ098";

            if (!IsPostBack)
            {
            }

            initPlineCode();
            setCondition();

            System.Data.DataTable dt;

            if (Request["opFlag"] == "getEditSeries")
            {
                string result = "";
                string sn = Request["SN"].ToString().Trim();
                string plineCode = Request["PLINE_CODE"].ToString().Trim();

                string sql = "select A.SN,A.PLAN_CODE,B.PLAN_SO,A.PLINE_CODE from DATA_PLAN_SN A  LEFT JOIN DATA_PLAN B ON A.PLAN_CODE=B.PLAN_CODE WHERE  A.SN = '" + sn + "'"

                          + " AND (B.PLAN_TYPE='C' OR PLAN_TYPE ='D' )AND A.PLINE_CODE='" + plineCode + "'  AND A.IS_VALID='Y'";
                    //+ " and ZDMC='管理' "
					//只有返修改制计划的流水号才可以
                    //+ " and PLAN_CODE in (select PLAN_CODE from DATA_PLAN where ( PLAN_TYPE = 'C' OR PLAN_TYPE = 'D') AND PLINE_CODE='" + plineCode + "')";
                dt = dc.GetTable(sql);
                if (dt.Rows.Count > 0)
                {
                    //??或者是取不到list里的数据，或者是已被清空，待测试 前端是用ClientInstanceName取，后台用ID?
                    for (int i = 0; i < listLsh.Items.Count; i++)
                    {
                        //根据“&”分割出SN
                        string[] str = listLsh.Items[i].ToString().Trim().Split("&".ToCharArray());
                        if (sn == str[0])
                        {
                            //为1时前端显示“流水号已录入！”
                            result = "1";
                        }
                    }
                    sn = dt.Rows[0][0].ToString();
                    string planCode = dt.Rows[0][1].ToString().Trim();
                    string planSO = dt.Rows[0][2].ToString().Trim();
                    string plineCode1 = dt.Rows[0][3].ToString();
                    result = sn + "&" + planCode + "&" + planSO + "&" + plineCode1;
                }
                else
                {
                    //为0时前端显示“流水号不存在！”
                    result = "0";
                }

                this.Response.Write(result);
                this.Response.End();
            }
        }

        //初始化gridview
        private void setCondition()
        {
            string sql = "";
            object plineCode = txtPCode.Value;
            if (plineCode != null)
            {
                sql = " SELECT T.*,GET_PARTUSER('" + plineCode.ToString() + "',ABOM_COMP,FUNC_GET_PLANSITE('" + plineCode.ToString() + "','D')) AS BGY "
                    + " FROM RST_GHTM_BOM_COMP T WHERE YHDM = '" + theUserCode + "' ";
            }
            else
            {
                sql = " SELECT T.*,'TEMP' AS BGY FROM RST_GHTM_BOM_COMP T WHERE 1=0 AND YHDM = '" + theUserCode + "' ";
            }
            //若为空，则不能tostring()
            //if (txtSN.Value == null)
            //如果列表中没有数据，即流水号不存在，则不查询数据
            if (listLsh.Items.Count < 1)
            {
                sql += " AND 1=0 ";
            }
            sql += " ORDER BY COMP_FLAG DESC ";

            System.Data.DataTable dt = dc.GetTable(sql);
            ASPxGridView1.DataSource = dt;
            ASPxGridView1.DataBind();
        }
        
        //callback方式刷新gridview
        protected void ASPxGridView1_CustomCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomCallbackEventArgs e)
        {
            //ASPxGridView1.JSProperties.Add("cpCallbackValue", ".txt");
            string s = e.Parameters;
            string[] s1 = s.Split('|');
            if (s1.Length > 1)
            {
                if (s1[0] == "XLS")
                {
                    Microsoft.Office.Interop.Excel.Application apps = new Microsoft.Office.Interop.Excel.Application();
                    string theWriteFile = "Restruct" + System.DateTime.Now.ToString("yyyyMMddHHmmss");
                    string filename = @"C:\tranlist\" + theWriteFile + ".xls";
                    if (System.IO.File.Exists(filename))
                    {
                        try
                        {
                            System.IO.File.Delete(filename);
                        }
                        catch (Exception)
                        {
                            showAlert(this, "请先关闭打开的xls文件！");
                            return;
                        }
                    }
                    String sourcePath = Server.MapPath(Request.ApplicationPath) + "\\Rmes\\File\\改制BOM对比\\gzbomdb.xls";
                    //String sourcePath = Server.MapPath(Request.ApplicationPath) + "\\file\\c\\gzbomdb.xls";
                    String targetPath = filename;// "D:\\DT900\\UP\\lshqd.xls";
                    bool isrewrite = true; // true=覆盖已存在的同名文件,false则反之
                    System.IO.File.Copy(sourcePath, targetPath, isrewrite);

                    object oMissing = System.Reflection.Missing.Value;
                    apps.Visible = false;
                    apps.DisplayAlerts = false;


                    //得到WorkBook对象,可以用两种方式之一:下面的是打开已有的文件 
                    Microsoft.Office.Interop.Excel.Workbook xBook = apps.Workbooks._Open(filename, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing,
                        oMissing, oMissing, oMissing, oMissing, oMissing);
                    //Microsoft.Office.Interop.Excel.Workbook _wbk;
                    //try
                    //{
                    //    //得到WorkBook对象,可以用两种方式之一:下面的是打开已有的文件 
                    //    _wbk = excel.Workbooks._Open(filename, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing,
                    //        oMissing, oMissing, oMissing, oMissing, oMissing);
                    //}
                    //catch
                    //{
                    //    showAlert(this, "找不到路径！");
                    //    Workbooks wbks = excel.Workbooks;
                    //    //若打开已有excel，把“true”替换成该excel的文件路径；
                    //    //注：若新建一个excel文档，“xxx”替换成true即可；不过这里新建的excel文档默认只有一个sheet。
                    //    _wbk = wbks.Add(true);
                    //}

                    //指定要操作的Sheet，两种方式：
                    Microsoft.Office.Interop.Excel.Worksheet xSheet = (Microsoft.Office.Interop.Excel.Worksheet)xBook.Sheets[1];
                    
                    //_wsh.Cells[1, 1] = "
                    //添加行
                    //((Range)_wsh.Rows[11, Missing.Value]).Insert(Missing.Value, XlInsertFormatOrigin.xlFormatFromLeftOrAbove);
                    //添加列
                    //_wsh.get_Range(_wsh.Cells[1, 1], Missing.Value).Insert(Missing.Value, XlInsertShiftDirection.xlShiftToRight);
                    //_wsh.get_Range(_wsh.Cells[1, 1], _wsh.Cells[_wsh.Rows.Count, 1]).Insert(Missing.Value, XlInsertShiftDirection.xlShiftToRight);
                    //设置单元格颜色
                    //((Range)_wsh.Rows[1, Missing.Value]).Interior.ColorIndex = 3;

                    xSheet.Cells[1, 1] = "流水号";
                    xSheet.Cells[1, 2] = "零件号";
                    xSheet.Cells[1, 3] = "零件名称";
                    xSheet.Cells[1, 4] = "数量";
                    xSheet.Cells[1, 5] = "工序";
                    xSheet.Cells[1, 6] = "工位";
                    xSheet.Cells[1, 7] = "计划号";
                    xSheet.Cells[1, 8] = "SO";
                    xSheet.Cells[1, 9] = "地点";
                    xSheet.Cells[1, 10] = "供应商";
                    xSheet.Cells[1, 11] = "类型";
                    xSheet.Cells[1, 12] = "保管员";

                    for (int i = 0; i < ASPxGridView1.VisibleRowCount; i++)
                    {
                        //添加行
                        //object a = ASPxGridView1.GetRow(i);
                        xSheet.Cells[i + 2, 1] = ASPxGridView1.GetRowValues(i, "GHTM");
                        xSheet.Cells[i + 2, 2] = ASPxGridView1.GetRowValues(i, "ABOM_COMP");
                        xSheet.Cells[i + 2, 3] = ASPxGridView1.GetRowValues(i, "ABOM_DESC");
                        xSheet.Cells[i + 2, 4] = ASPxGridView1.GetRowValues(i, "ABOM_QTY");
                        xSheet.Cells[i + 2, 5] = ASPxGridView1.GetRowValues(i, "ABOM_OP");
                        xSheet.Cells[i + 2, 6] = ASPxGridView1.GetRowValues(i, "ABOM_WKCTR");
                        xSheet.Cells[i + 2, 7] = ASPxGridView1.GetRowValues(i, "ABOM_JHDM");
                        xSheet.Cells[i + 2, 8] = ASPxGridView1.GetRowValues(i, "ABOM_SO");
                        xSheet.Cells[i + 2, 9] = ASPxGridView1.GetRowValues(i, "GZDD");
                        xSheet.Cells[i + 2, 10] = ASPxGridView1.GetRowValues(i, "ABOM_GYS");
                        xSheet.Cells[i + 2, 11] = ASPxGridView1.GetRowValues(i, "COMP_FLAG");
                        xSheet.Cells[i + 2, 12] = ASPxGridView1.GetRowValues(i, "BGY");
                        string compFlag = ASPxGridView1.GetRowValues(i, "COMP_FLAG").ToString();
                        switch (compFlag)
                        {
                            case "0":
                                //2代表白色
                                ((Range)xSheet.Rows[i + 2, Missing.Value]).Interior.ColorIndex = 2;
                                break;
                            case "1":
                                //3代表红色
                                ((Range)xSheet.Rows[i + 2, Missing.Value]).Interior.ColorIndex = 3;
                                break;
                            case "2":
                                //10代表不刺眼的绿色
                                ((Range)xSheet.Rows[i + 2, Missing.Value]).Interior.ColorIndex = 10;
                                break;
                            default:
                                return;
                        }
                    }

                    xBook.Save();
                    xBook.Close(oMissing, oMissing, oMissing);
                    apps.Quit();
                    xSheet = null;
                    xBook = null;
                    apps = null;

                    try
                    {
                        ASPxGridView1.JSProperties.Add("cpCallbackValue", theWriteFile + ".xls");
                    }
                    catch { }
                    //}
                    //catch
                    //{
                    //    showAlert(this, "导出失败！");
                    //    return;
                    //}
                }
            }
            else
            {
                //string theWriteFile = "";
                ASPxGridView1.JSProperties.Add("cpCallbackValue", ".xls");

            }

            setCondition();
        }

        //是点击按钮的时候才执行吗？
        //callback方式刷新gridview
        //protected void ASPxGridView1_CustomCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomCallbackEventArgs e)
        //{
        //    //ASPxGridView1.JSProperties.Add("cpCallbackValue", ".txt");
        //    string s = e.Parameters;
        //    string[] s1 = s.Split('|');
        //    if (s1.Length > 1)
        //    {
        //        if (s1[0] == "XLS")
        //        {
        //            //try
        //            //{
        //                //创建一个新的excel文档 
        //                Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
        //                Workbooks wbks = excel.Workbooks;
        //                //若打开已有excel，把“true”替换成该excel的文件路径；
        //                //注：若新建一个excel文档，“xxx”替换成true即可；不过这里新建的excel文档默认只有一个sheet。
        //                _Workbook _wbk = wbks.Add(true);

        //                //取得sheet
        //                Sheets shs = _wbk.Sheets;
        //                //i是要取得的sheet的index
        //                _Worksheet _wsh = (_Worksheet)shs.get_Item(1);

        //                //_wsh.Cells[1, 1] = "
        //                //添加行
        //                //((Range)_wsh.Rows[11, Missing.Value]).Insert(Missing.Value, XlInsertFormatOrigin.xlFormatFromLeftOrAbove);
        //                //添加列
        //                //_wsh.get_Range(_wsh.Cells[1, 1], Missing.Value).Insert(Missing.Value, XlInsertShiftDirection.xlShiftToRight);
        //                //_wsh.get_Range(_wsh.Cells[1, 1], _wsh.Cells[_wsh.Rows.Count, 1]).Insert(Missing.Value, XlInsertShiftDirection.xlShiftToRight);
        //                //设置单元格颜色
        //                //((Range)_wsh.Rows[1, Missing.Value]).Interior.ColorIndex = 3;

        //                _wsh.Cells[1, 1] = "流水号";
        //                _wsh.Cells[1, 2] = "零件号";
        //                _wsh.Cells[1, 3] = "零件名称";
        //                _wsh.Cells[1, 4] = "数量";
        //                _wsh.Cells[1, 5] = "工序";
        //                _wsh.Cells[1, 6] = "工位";
        //                _wsh.Cells[1, 7] = "计划号";
        //                _wsh.Cells[1, 8] = "SO";
        //                _wsh.Cells[1, 9] = "地点";
        //                _wsh.Cells[1, 10] = "供应商";
        //                _wsh.Cells[1, 11] = "类型";
        //                _wsh.Cells[1, 12] = "保管员";

        //                for (int i = 0; i < ASPxGridView1.VisibleRowCount; i++)
        //                {
        //                    //添加行
        //                    //object a = ASPxGridView1.GetRow(i);
        //                    _wsh.Cells[i + 2, 1] = ASPxGridView1.GetRowValues(i, "GHTM");
        //                    _wsh.Cells[i + 2, 2] = ASPxGridView1.GetRowValues(i, "ABOM_COMP");
        //                    _wsh.Cells[i + 2, 3] = ASPxGridView1.GetRowValues(i, "ABOM_DESC");
        //                    _wsh.Cells[i + 2, 4] = ASPxGridView1.GetRowValues(i, "ABOM_QTY");
        //                    _wsh.Cells[i + 2, 5] = ASPxGridView1.GetRowValues(i, "ABOM_OP");
        //                    _wsh.Cells[i + 2, 6] = ASPxGridView1.GetRowValues(i, "ABOM_WKCTR");
        //                    _wsh.Cells[i + 2, 7] = ASPxGridView1.GetRowValues(i, "ABOM_JHDM");
        //                    _wsh.Cells[i + 2, 8] = ASPxGridView1.GetRowValues(i, "ABOM_SO");
        //                    _wsh.Cells[i + 2, 9] = ASPxGridView1.GetRowValues(i, "GZDD");
        //                    _wsh.Cells[i + 2, 10] = ASPxGridView1.GetRowValues(i, "ABOM_GYS");
        //                    _wsh.Cells[i + 2, 11] = ASPxGridView1.GetRowValues(i, "COMP_FLAG");
        //                    _wsh.Cells[i + 2, 12] = ASPxGridView1.GetRowValues(i, "BGY");
        //                    string compFlag = ASPxGridView1.GetRowValues(i, "COMP_FLAG").ToString();
        //                    switch (compFlag)
        //                    {
        //                        case "0":
        //                            //2代表白色
        //                            ((Range)_wsh.Rows[i + 2, Missing.Value]).Interior.ColorIndex = 2;
        //                            break;
        //                        case "1":
        //                            //3代表红色
        //                            ((Range)_wsh.Rows[i + 2, Missing.Value]).Interior.ColorIndex = 3;
        //                            break;
        //                        case "2":
        //                            //10代表不刺眼的绿色
        //                            ((Range)_wsh.Rows[i + 2, Missing.Value]).Interior.ColorIndex = 10;
        //                            break;
        //                        default:
        //                            return;
        //                    }
        //                }

        //                //如果目录不存在则创建目录
        //                //if (!Directory.Exists("C:\\tranlist\\Storage"))
        //                //{
        //                //    Directory.CreateDirectory("C:\\tranlist\\Storage");
        //                //}

        //                //屏蔽掉系统跳出的覆盖文件Alert
        //                excel.DisplayAlerts = false;
        //                _wbk.Saved = true;
        //                //保存到指定目录
        //                //替换点否会报错
        //                string theWriteFile = "Restruct" + System.DateTime.Now.ToString("yyyyMMddHHmmss");
        //                _wbk.SaveAs("C:\\tranlist\\" + theWriteFile + ".xls",
        //                Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange, Missing.Value, Missing.Value, Missing.Value,
        //                Missing.Value, Missing.Value);
        //                //_wbk.SaveCopyAs("C:\\tranlist\\" + theWriteFile + ".xls");
        //                //用@的方法只是不会报错，但\显示不出来
        //                //showAlert(this, @"文件已保存至C:\excel\改制差异清单明细信息导出.xls");
        //                //加一个中文的双引号就不会有什么问题
        //                //showAlert(this, "文件已成功保存！");
        //                //设置回默认值
        //                excel.DisplayAlerts = true;

        //                excel.Quit();

        //                //释放掉多余的excel进程
        //                //??释放有问题，关闭电脑的时候会提示--将这些资源依次释放再设置为空，最后回收内存就不会再有问题
        //                System.Runtime.InteropServices.Marshal.ReleaseComObject(excel);
        //                System.Runtime.InteropServices.Marshal.ReleaseComObject((object)wbks);
        //                System.Runtime.InteropServices.Marshal.ReleaseComObject((object)_wbk);
        //                System.Runtime.InteropServices.Marshal.ReleaseComObject((object)shs);
        //                System.Runtime.InteropServices.Marshal.ReleaseComObject((object)_wsh);

        //                _wsh = null;
        //                shs = null;
        //                _wbk = null;
        //                wbks = null;
        //                excel = null;

        //                GC.Collect(0);

        //                try
        //                {
        //                    ASPxGridView1.JSProperties.Add("cpCallbackValue", theWriteFile + ".xls");
        //                }
        //                catch { }
        //            //}
        //            //catch
        //            //{
        //            //    showAlert(this, "导出失败！");
        //            //    return;
        //            //}
        //        }
        //    }
        //    else
        //    {
        //        //string theWriteFile = "";
        //        ASPxGridView1.JSProperties.Add("cpCallbackValue", ".xls");

        //    }

        //    setCondition();
        //}

        //初始化生产线
        private void initPlineCode()
        {
            string sql = "select distinct a.pline_code,b.pline_name from vw_user_role_program a left join code_product_line b on a.pline_code=b.pline_code where a.user_id='" + theUserId + "' and a.program_code='" + theProgramCode + "' and a.company_code='" + theCompanyCode + "'";
            SqlCode.SelectCommand = sql;
            SqlCode.DataBind();
            
        }

        protected void btnQryDetail_Click(object sender, EventArgs e)
        {
            string oldJhdm;
            string oldSO;
            string newJhdm;
            string newSO;
            string newGhtm;
            string newGzdd;

            if (listLsh.Items.Count >= 1)
            {
                string sql = " DELETE FROM RST_GHTM_BOM_COMP WHERE YHDM = '" + theUserCode + "' ";
                dc.ExeSql(sql);

                for (int i = 0; i < listLsh.Items.Count; i++)
                {
                    //sql = " SELECT GHTM,JHDM,SO FROM RST_GHTM_GZ_LOG WHERE JHDM != NEW_JHDM AND GZRQ IS NOT NULL ";
                    //string[] str = listLsh.Items[i].ToString().Trim().Split("&".ToCharArray());
                    //string ghtm = str[0];
                    //sql += " AND GHTM = '" + ghtm + "' ";
                    //sql += " ORDER BY GZRQ DESC ";
                    //不从原来的RST_GHTM_GZ_LOG里取旧JHDM，从DATA_RECORD里取旧JHDM
                    string[] str = listLsh.Items[i].ToString().Trim().Split("&".ToCharArray());
                    string ghtm = str[0];
                    sql = " SELECT SN,PLAN_CODE,PLAN_SO FROM DATA_RECORD WHERE SN = '" + ghtm + "' ORDER BY WORK_DATE DESC ";

                    System.Data.DataTable dt = dc.GetTable(sql);

                    if (dt.Rows.Count > 0)
                    {
                        oldJhdm = dt.Rows[0][1].ToString();
                        oldSO = dt.Rows[0][2].ToString();
                    }
                    else
                    {
                        sql = " SELECT SN,PLAN_CODE,PLAN_SO FROM DATA_PRODUCT WHERE SN = '" + ghtm + "' ";
                        System.Data.DataTable dt2 = dc.GetTable(sql);
                        if (dt2.Rows.Count > 0)
                        {
                            oldJhdm = dt2.Rows[0][1].ToString();
                            oldSO = dt2.Rows[0][2].ToString();

                        }
                        else
                        {
                            oldJhdm = "";
                            oldSO = "";
                        }
                    }
                    newGhtm = ghtm;
                    newJhdm = str[1];
                    newSO = str[2];
                    newGzdd = str[3];

                    MW_COMPARE_GHTMBOM sp = new MW_COMPARE_GHTMBOM()
                    {
                        GHTM1 = newGhtm,
                        GZDD1 = newGzdd,
                        JHDM1 = oldJhdm,
                        JHSO1 = oldSO,
                        GZFLAG1 = "OLD",
                        GZDD2 = newGzdd,
                        JHDM2 = newJhdm,
                        JHSO2 = newSO,
                        GZFLAG2 = "NEW",
                        YHDM1 = theUserCode 
                        //test
                        //GHTM1 = newGhtm,
                        //GZDD1 = newGzdd, 
                        //JHDM1 = oldJhdm, 
                        //JHSO1 = oldSO,
                        //GZFLAG1 = "NEW", 
                        //GZDD2 = newGzdd, 
                        //JHDM2 = newJhdm, 
                        //JHSO2 = newSO, 
                        //GZFLAG2 = "OLD",
                        //YHDM1 = theUserCode 
                    };
                    Procedure.run(sp);
                }
                setCondition();
            }

        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                //创建一个新的excel文档 
                Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
                Workbooks wbks = excel.Workbooks;
                //若打开已有excel，把“true”替换成该excel的文件路径；
                //注：若新建一个excel文档，“xxx”替换成true即可；不过这里新建的excel文档默认只有一个sheet。
                _Workbook _wbk = wbks.Add(true);

                //取得sheet
                Sheets shs = _wbk.Sheets;
                //i是要取得的sheet的index
                _Worksheet _wsh = (_Worksheet)shs.get_Item(1);

                //_wsh.Cells[1, 1] = "
                //添加行
                //((Range)_wsh.Rows[11, Missing.Value]).Insert(Missing.Value, XlInsertFormatOrigin.xlFormatFromLeftOrAbove);
                //添加列
                //_wsh.get_Range(_wsh.Cells[1, 1], Missing.Value).Insert(Missing.Value, XlInsertShiftDirection.xlShiftToRight);
                //_wsh.get_Range(_wsh.Cells[1, 1], _wsh.Cells[_wsh.Rows.Count, 1]).Insert(Missing.Value, XlInsertShiftDirection.xlShiftToRight);
                //设置单元格颜色
                //((Range)_wsh.Rows[1, Missing.Value]).Interior.ColorIndex = 3;

                _wsh.Cells[1, 1] = "流水号";
                _wsh.Cells[1, 2] = "零件号";
                _wsh.Cells[1, 3] = "零件名称";
                _wsh.Cells[1, 4] = "数量";
                _wsh.Cells[1, 5] = "工序";
                _wsh.Cells[1, 6] = "工位";
                _wsh.Cells[1, 7] = "计划号";
                _wsh.Cells[1, 8] = "SO";
                _wsh.Cells[1, 9] = "地点";
                _wsh.Cells[1, 10] = "供应商";
                _wsh.Cells[1, 11] = "类型";
                _wsh.Cells[1, 12] = "保管员";

                for (int i = 0; i < ASPxGridView1.VisibleRowCount; i++)
                {
                    //添加行
                    //object a = ASPxGridView1.GetRow(i);
                    _wsh.Cells[i + 2, 1] = ASPxGridView1.GetRowValues(i, "GHTM");
                    _wsh.Cells[i + 2, 2] = ASPxGridView1.GetRowValues(i, "ABOM_COMP");
                    _wsh.Cells[i + 2, 3] = ASPxGridView1.GetRowValues(i, "ABOM_DESC");
                    _wsh.Cells[i + 2, 4] = ASPxGridView1.GetRowValues(i, "ABOM_QTY");
                    _wsh.Cells[i + 2, 5] = ASPxGridView1.GetRowValues(i, "ABOM_OP");
                    _wsh.Cells[i + 2, 6] = ASPxGridView1.GetRowValues(i, "ABOM_WKCTR");
                    _wsh.Cells[i + 2, 7] = ASPxGridView1.GetRowValues(i, "ABOM_JHDM");
                    _wsh.Cells[i + 2, 8] = ASPxGridView1.GetRowValues(i, "ABOM_SO");
                    _wsh.Cells[i + 2, 9] = ASPxGridView1.GetRowValues(i, "GZDD");
                    _wsh.Cells[i + 2, 10] = ASPxGridView1.GetRowValues(i, "ABOM_GYS");
                    _wsh.Cells[i + 2, 11] = ASPxGridView1.GetRowValues(i, "COMP_FLAG");
                    _wsh.Cells[i + 2, 12] = ASPxGridView1.GetRowValues(i, "BGY");
                    string compFlag = ASPxGridView1.GetRowValues(i, "COMP_FLAG").ToString();
                    switch (compFlag)
                    {
                        case "0":
                            //2代表白色
                            ((Range)_wsh.Rows[i + 2, Missing.Value]).Interior.ColorIndex = 2;
                            break;
                        case "1":
                            //3代表红色
                            ((Range)_wsh.Rows[i + 2, Missing.Value]).Interior.ColorIndex = 3;
                            break;
                        case "2":
                            //10代表不刺眼的绿色
                            ((Range)_wsh.Rows[i + 2, Missing.Value]).Interior.ColorIndex = 10;
                            break;
                        default:
                            return;
                    }
                }

                //如果目录不存在则创建目录
                if (!Directory.Exists("C:/excel"))
                {
                    Directory.CreateDirectory("C:/excel");
                }

                //屏蔽掉系统跳出的覆盖文件Alert
                excel.DisplayAlerts = false;
                _wbk.Saved = true;
                //保存到指定目录
                //替换点否会报错
                _wbk.SaveAs("C:/excel/改制差异清单明细信息导出.xls",
                Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange, Missing.Value, Missing.Value, Missing.Value,
                Missing.Value, Missing.Value);
                //用@的方法只是不会报错，但\显示不出来
                //showAlert(this, @"文件已保存至C:\excel\改制差异清单明细信息导出.xls");
                //加一个中文的双引号就不会有什么问题
                showAlert(this, "文件已成功保存！");
                //设置回默认值
                excel.DisplayAlerts = true;

                excel.Quit();

                //释放掉多余的excel进程
                //??释放有问题，关闭电脑的时候会提示--将这些资源依次释放再设置为空，最后回收内存就不会再有问题
                System.Runtime.InteropServices.Marshal.ReleaseComObject(excel);
                System.Runtime.InteropServices.Marshal.ReleaseComObject((object)wbks);
                System.Runtime.InteropServices.Marshal.ReleaseComObject((object)_wbk);
                System.Runtime.InteropServices.Marshal.ReleaseComObject((object)shs);
                System.Runtime.InteropServices.Marshal.ReleaseComObject((object)_wsh);

                _wsh = null;
                shs = null;
                _wbk = null;
                wbks = null;
                excel = null;

                GC.Collect(0); 
            }
            catch
            {
                showAlert(this, "导出失败！");
                return;
            }
        }

        protected void ASPxGridView1_HtmlRowPrepared(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewTableRowEventArgs e)
        {
            if (e.VisibleIndex < 0) return;
            string compFlag = e.GetValue("COMP_FLAG").ToString();
            switch (compFlag)
            {
                case "0":
                    e.Row.BackColor = System.Drawing.Color.White;
                    break;
                case "1":
                    e.Row.BackColor = System.Drawing.Color.Red;
                    break;
                case "2":
                    e.Row.BackColor = System.Drawing.Color.Green;
                    break;
                default:
                    return;
            }
        }

        //前端弹出alert消息
        public static void showAlert(System.Web.UI.Page page, string msg)
        {
            page.ClientScript.RegisterStartupScript(page.GetType(), "message", "<script language='javascript' defer>alert('" + msg.ToString() + "');</script>");
        }


    }
}