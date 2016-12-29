using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Office.Interop.Excel;
using Rmes.DA.Entity;
using PetaPoco;
using Rmes.DA.Base;
using Rmes.DA.Factory;


namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            string oraclePath = System.Windows.Forms.Application.StartupPath + @"\OCI";
            string libPath = System.Windows.Forms.Application.StartupPath + @"\Lib";
            string allpaths = oraclePath + ";" + libPath;
            Environment.SetEnvironmentVariable("PATH", allpaths);
            function3();
        }

        //完工入库和物料消耗
        public void function1()
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            dt.Columns.Add("project_code"); 
            dt.Columns.Add("order_code"); 
            dt.Columns.Add("error");

            Microsoft.Office.Interop.Excel.Application apps = new Microsoft.Office.Interop.Excel.Application();


            //string filename = System.Windows.Forms.Application.StartupPath+"\\..\\.." + file_template;
            string filename = @"C:\Users\sony\Desktop\完工合同.xlsx";

            object oMissing = System.Reflection.Missing.Value;

            Workbook _wkbook = apps.Workbooks.Open(filename, 0, true, 5, oMissing, oMissing, true, 1, oMissing, false, false, oMissing, false, oMissing, oMissing);
            Worksheet _wksheet = _wkbook.Sheets["sheet1"];

            for (int i = 1; i < 107; i++)
            {
                string pr="", o="";
                DB.GetInstance().BeginTransaction();
                try
                {
                    Range test = ((Range)_wksheet.Cells[i, 1]);

                    test.Select();
                    string project_code = test.Text;
                    pr = project_code;
                    List<PlanEntity> plans = DB.GetInstance().Fetch<PlanEntity>("where project_code=@0 and run_flag!='F'", project_code);
                    foreach (var p in plans)
                    {
                        o = p.ORDER_CODE;
                        //完工入库
                        PlanSnEntity snEntity = new PlanSnEntity();
                        try
                        {
                            snEntity = DB.GetInstance().First<PlanSnEntity>("where order_code=@0", p.ORDER_CODE);
                        }
                        catch
                        {

                        }
                        SNCompleteInstoreEntity completeEntity = new SNCompleteInstoreEntity
                        {
                            PLAN_CODE = p.PLAN_CODE,
                            HANDLE_FLAG = "0",
                            PRODUCT_CODE = p.PLAN_SO,
                            SN = snEntity == null ? "" : snEntity.SN,
                            INSTORE_QTY = p.PLAN_QTY.ToString(),
                            STORE_CODE = p.INSTORE_CODE,
                            INSTORE_TYPE_CODE = "1",
                            ORDER_CODE = p.ORDER_CODE,
                            WORKSHOP_CODE = p.WORKSHOP_CODE,
                            PLAN_BATCH = ""
                        };
                        DB.GetInstance().Insert(completeEntity);
                        DB.GetInstance().Execute("call SAP_HANDLE_EXPORT_INSTORE()");

                        //物料消耗

                        string sql = string.Format("insert into imes_data_plan_bom(aufnr,werks,matnr,vornr,submat,menge,lgort,id,serial,prind) select aufnr,werks,matnr,vornr,submat,menge,a.lgort,to_char(sysdate,'yymmdd')||to_char(seq_transid.nextval,'fm000000'),to_char(sysdate,'yyyymmddhh24miss'),'0' from isap_data_plan_bom a where aufnr='{0}' and A.DUMPS<>'X'", p.ORDER_CODE);
                        int n = DB.GetInstance().Execute(sql);

                        //计划状态变更
                        p.RUN_FLAG = "F";
                        DB.GetInstance().Update(p);
                    }
                    DB.GetInstance().CompleteTransaction();
                }
                catch(Exception e)
                {
                    dt.Rows.Add(pr,o,e.Message);
                    DB.GetInstance().AbortTransaction();
                }
            }

            MessageBox.Show("Done!");
        }

        public void function2()
        {
            List<IMESCompleteInstoreEntity> entity = DB.GetInstance().Fetch<IMESCompleteInstoreEntity>("where hsdat='20150730'");
            string sql = "insert into imes_data_plan_process(ID,AUFNR,WERKS,VORNR,ARBPL,VGW02,VGE02,VGW03,VGE03,OPFLG,LMNGA,MUSER,prind) select to_char(sysdate,'yymmdd')||to_char(seq_transid.nextval,'fm000000'),aufnr,werks,VORNR,arbpl,VGW02,VGE02,VGW03,VGE03,'X',1,'10000191','3' FROM ISAP_DATA_PLAN_PROCESS A WHERE TO_NUMBER(VGW03)<>0 AND A.AUFNR=@0";
 
            foreach (var e in entity)
            {
                DB.GetInstance().Execute(sql,e.AUFNR);
            }
            MessageBox.Show("Done!");
        }

        public void function3()
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            dt.Columns.Add("project_code"); 
            dt.Columns.Add("order_code"); 
            dt.Columns.Add("error");

            List<string> projectCode = new List<string>();
            projectCode.Add("2015B20059");
            projectCode.Add("2015B20058");
            projectCode.Add("2015G20153");
            projectCode.Add("2015B20118");
            projectCode.Add("2015B20043");
            projectCode.Add("2015B30016");
            string pr = "", o = "";
            foreach(var pc in projectCode)
            {
                DB.GetInstance().BeginTransaction();
                try
                {
                    
                    List<PlanEntity> plans = DB.GetInstance().Fetch<PlanEntity>("where project_code=@0 and run_flag!='F'", pc);
                    foreach (var p in plans)
                    {
                        o = p.ORDER_CODE;
                        //完工入库
                        PlanSnEntity snEntity = new PlanSnEntity();
                        List<SNCompleteInstoreEntity> temp1 = new List<SNCompleteInstoreEntity>();
                        List<IMESPlanBOMEntity> temp2 = new List<IMESPlanBOMEntity>();
                        List<IMESPlanProcessEntity> temp3 = new List<IMESPlanProcessEntity>();
                        try
                        {
                            snEntity = DB.GetInstance().First<PlanSnEntity>("where order_code=@0", p.ORDER_CODE);
                            temp1 = DB.GetInstance().Fetch<SNCompleteInstoreEntity>("where ORDER_CODE=@0", p.ORDER_CODE);
                            temp2 = DB.GetInstance().Fetch<IMESPlanBOMEntity>("where aufnr=@0", p.ORDER_CODE);
                            temp3 = DB.GetInstance().Fetch<IMESPlanProcessEntity>("where aufnr=@0", p.ORDER_CODE);
                        }
                        catch
                        {

                        }
                        if (temp1 == null || temp1.Count == 0)
                        {
                            SNCompleteInstoreEntity completeEntity = new SNCompleteInstoreEntity
                            {
                                PLAN_CODE = p.PLAN_CODE,
                                HANDLE_FLAG = "0",
                                PRODUCT_CODE = p.PLAN_SO,
                                SN = snEntity == null ? "" : snEntity.SN,
                                INSTORE_QTY = p.PLAN_QTY.ToString(),
                                STORE_CODE = p.INSTORE_CODE,
                                INSTORE_TYPE_CODE = "1",
                                ORDER_CODE = p.ORDER_CODE,
                                WORKSHOP_CODE = p.WORKSHOP_CODE,
                                PLAN_BATCH = ""
                            };
                            DB.GetInstance().Insert(completeEntity);
                            DB.GetInstance().Execute("call SAP_HANDLE_EXPORT_INSTORE()");
                        }

                        //物料消耗
                        if (temp2 == null || temp2.Count == 0)
                        {
                            string sql = string.Format("insert into imes_data_plan_bom(aufnr,werks,matnr,vornr,submat,menge,lgort,id,serial,prind) select aufnr,werks,matnr,vornr,submat,menge,a.lgort,to_char(sysdate,'yymmdd')||to_char(seq_transid.nextval,'fm000000'),to_char(sysdate,'yyyymmddhh24miss'),'0' from isap_data_plan_bom a where aufnr='{0}' and A.DUMPS<>'X'", p.ORDER_CODE);
                            int n = DB.GetInstance().Execute(sql);
                        }


                        //工序工时
                        if (temp3 == null || temp3.Count == 0)
                        {
                            string sql = string.Format("insert into imes_data_plan_process(ID,AUFNR,WERKS,VORNR,ARBPL,VGW02,VGE02,VGW03,VGE03,OPFLG,LMNGA,MUSER,prind) select to_char(sysdate,'yymmdd')||to_char(seq_transid.nextval,'fm000000'),aufnr,werks,VORNR,arbpl,VGW02,VGE02,VGW03,VGE03,'X',1,'10000191','0' FROM ISAP_DATA_PLAN_PROCESS A WHERE TO_NUMBER(VGW03)<>0 AND A.AUFNR='{0}'", p.ORDER_CODE);
                            int n = DB.GetInstance().Execute(sql);
                        }

                        //计划状态变更
                        p.RUN_FLAG = "F";
                        DB.GetInstance().Update(p);
                    }
                    DB.GetInstance().CompleteTransaction();
                }
                catch(Exception e)
                {
                    dt.Rows.Add(pr,o,e.Message);
                    DB.GetInstance().AbortTransaction();
                }
            }

            MessageBox.Show("Done!");
            

        }

        public void function4()
        {
            List<PlanTempEntity> plans = DB.GetInstance().Fetch<PlanTempEntity>("where PLINE_CODE=@0 and HANDLE_FLAG='N'","M100");
            PlanTempFactory.CreatePlan(plans.Select(m => m.PLAN_CODE).ToList<string>(), "10000694");
        }

    }
}
