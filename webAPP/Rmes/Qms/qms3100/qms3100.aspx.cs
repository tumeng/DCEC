using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxGridView;

//以下引用，如果采用实体类，请全部复制到新页面
using PetaPoco;
using Rmes.DA.Base;
using Rmes.DA.Entity;
using Rmes.DA.Dal;
using Rmes.DA.Factory;
using Rmes.Pub.Data;
using Rmes.Web.Base;
using System.Data;
using DevExpress.Web.ASPxEditors;

/**
 * 功能概述：质检条码管理
 * 作    者：涂猛
 * 创建时间：2014-10-13
 */

namespace Rmes.WebApp.Rmes.Qms.qms3100
{
    public partial class qms3100 : BasePage
    {
        public Database db = DB.GetInstance();

        public dataConn conn = new dataConn();

        private int f = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
           
        }

        protected void ASPxGridView1_CustomDataCallback(object sender, ASPxGridViewCustomDataCallbackEventArgs e)
        {
            string flag = e.Parameters;
            DataTable dt = Session["temp_barCode"] as DataTable;
            switch(flag){
                case "update":
                    try
                    {
                        
                        SeriesBarCodeEntity entity = new SeriesBarCodeEntity()
                        {
                            RMES_ID = dt.Rows[0][0] as string,
                            SERIES_CODE = dt.Rows[0][1] as string,
                            SEQ_1 = dt.Rows[0][2] as string,
                            SEQ_2 = dt.Rows[0][3] as string,
                            SEQ_3 = dt.Rows[0][4] as string,
                            SEQ_4 = dt.Rows[0][5] as string,
                            SEQ_5 = dt.Rows[0][6] as string,
                            SEQ_6 = dt.Rows[0][7] as string,
                            SEQ_7 = dt.Rows[0][8] as string,
                            SEQ_8 = dt.Rows[0][9] as string,
                            SEQ_9 = dt.Rows[0][10] as string,
                            SEQ_10 = dt.Rows[0][11] as string,
                            SEQ_11 = dt.Rows[0][12] as string,
                            SEQ_12 = dt.Rows[0][13] as string,
                            SEQ_13 = dt.Rows[0][14] as string,
                            SEQ_14 = dt.Rows[0][15] as string,
                            SEQ_15 = dt.Rows[0][16] as string,
                            SEQ_16 = dt.Rows[0][17] as string,
                            SEQ_17 = dt.Rows[0][18] as string,
                            SEQ_18 = dt.Rows[0][19] as string,
                            SEQ_19 = dt.Rows[0][20] as string,
                            SEQ_20 = dt.Rows[0][21] as string,
                            SEQ_21 = dt.Rows[0][22] as string,
                            SEQ_22 = dt.Rows[0][23] as string,
                        };
                        if (string.IsNullOrWhiteSpace(entity.RMES_ID))
                            db.Insert(entity);
                        else
                            db.Update(entity);
                        e.Result = "success";
                    }
                    catch (Exception ex)
                    {
                        e.Result = ex.Message;
                    }
                    break;
                case "work":
                    if (DoWork(dt))
                        e.Result = "成功";

                    break;
            }
                
            
        
        }

        protected void ASPxGridView1_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            string _series = ASPxComboBoxSeries.Text as string;
            string id = ASPxComboBoxSeries.Value as string;
            string seqs = e.Parameters;
            if (string.IsNullOrEmpty(seqs)) return;
            if (seqs.EndsWith(",")) seqs = seqs.Substring(0, seqs.Length - 1);
            string[] arraySEQ = seqs.Split(',');
            switch (arraySEQ[0])
            {
                case "check":
                    string series = arraySEQ[1];
                    string sql = "select * from DATA_SERIES_BARCODE where SERIES_CODE='" + _series + "'";
                    DataTable dt = conn.GetTable(sql);
                    Session.Add("temp_barCode", dt);
                    ASPxGridView1.DataSource = dt;
                    ASPxGridView1.DataBind();
                    break;
                case "add":

                    DataTable dt_add = Session["temp_barCode"] as DataTable;
                    string barCodeSEQ = ASPxComboBoxBarSEQ.Text;
                    string[] array = barCodeSEQ.Split('|');
                    int seq = Convert.ToInt32(array[0]);



                    if (dt_add.Rows.Count <= 0)
                    {
                        dt_add.Rows.Add();
                        dt_add.Rows[0][1] = _series;
                        dt_add.Rows[0][2] = id;
                    }
                    string values="";
                    for(int i=1;i<arraySEQ.Length;i++){
                        if (string.IsNullOrWhiteSpace(arraySEQ[i])) continue;
                        values+=arraySEQ[i]+",";

                    }
                    if(values.EndsWith(",")) values=values.Substring(0,values.Length-1);
                    string cellstr = dt_add.Rows[0][seq + 1] as string;
                    if (string.IsNullOrEmpty(cellstr))
                    {
                        cellstr = values;
                    }
                    else
                    {
                        cellstr += "," + values;
                    }
                    dt_add.Rows[0][seq + 1] = cellstr;
                    ASPxGridView gridView = sender as ASPxGridView;
                    gridView.DataSource = dt_add;
                    gridView.DataBind();
                    Session.Add("temp_barCode", dt_add);
                    break;
                case "update":
                    DataTable dt_update = Session["temp_barCode"] as DataTable;
                    string barCodeSEQ_update = ASPxComboBoxBarSEQ.Text;
                    string[] array_update = barCodeSEQ_update.Split('|');
                    int seq_update = Convert.ToInt32(array_update[0]);



                    if (dt_update.Rows.Count <= 0)
                    {
                        dt_update.Rows.Add();
                        dt_update.Rows[0][1] = _series;
                        dt_update.Rows[0][2] = id;
                    }
                    string values_update="";
                    for(int i=1;i<arraySEQ.Length;i++){
                        values_update += arraySEQ[i] + ",";

                    }
                    if (values_update.EndsWith(",")) values_update = values_update.Substring(0, values_update.Length - 1);
                    dt_update.Rows[0][seq_update + 1] = values_update;

                    ASPxGridView1.DataSource = dt_update;
                    ASPxGridView1.DataBind();
                    Session.Add("temp_barCode", dt_update);
                    break;
            }
            
            
        }

        protected void ASPxListBoxBarCode_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            string father = e.Parameter;
            string series = ASPxComboBoxSeries.Value.ToString();
            string pline = ASPxComboBoxPline.SelectedItem.Value.ToString();
            string sql = "select * from CODE_DETECT_BARCODE t where PLINE_CODE='" + pline + "' and SEQ_FATHER='"+father+"' and SEQ_LEVEL='2'";
            DataTable dt = new DataTable();
            dt = conn.GetTable(sql);
            ASPxListBox barCode = sender as ASPxListBox;
            barCode.DataSource = dt;
            barCode.DataBind();
        }

        protected void ASPxComboBoxSeries_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            string pline = e.Parameter;
            string sql = "select RMES_ID,SEQ_NAME from CODE_DETECT_BARCODE t where t.seq_level!='1' start with t.seq_value='1' and t.seq_level='1' and t.pline_code='" + pline + "'connect by prior t.rmes_id=t.seq_father";
            DataTable dt = new DataTable();
            dt = conn.GetTable(sql);
            ASPxComboBox series = sender as ASPxComboBox;
            series.DataSource = dt;
            series.ValueField = "RMES_ID";
            series.TextField = "SEQ_NAME";
            series.DataBind();
        }

        protected void ASPxComboBoxBarSEQ_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            string series = e.Parameter;
            string pline = ASPxComboBoxPline.SelectedItem.Value.ToString();
            string sql = "select RMES_ID,SEQ_VALUE||'|'||SEQ_NAME as SEQ_NAME from CODE_DETECT_BARCODE t where PLINE_CODE='" + pline + "' and SEQ_LEVEL='1' and SEQ_VALUE!=1";
            DataTable dt = new DataTable();
            dt = conn.GetTable(sql);
            ASPxComboBox barSEQ = sender as ASPxComboBox;
            barSEQ.DataSource = dt;
            barSEQ.ValueField = "RMES_ID";
            barSEQ.TextField = "SEQ_NAME";
            barSEQ.DataBind();
        }

        protected string GetCellText(GridViewDataItemTemplateContainer container)
        {
            string cell_text = container.Text;
            string strret ="";
            string[] array = cell_text.Split(',');
            foreach (var a in array)
            {
                DetectBarCodeEntity entity = null;
                try
                {
                    entity = db.First<DetectBarCodeEntity>("where RMES_ID=@0", a);
                }
                catch
                {

                }
                if (entity == null) continue;
                strret += entity.SEQ_NAME + ",";

            }
            if (strret.EndsWith(",")) strret = strret.Substring(0,strret.Length-1);

            if (string.IsNullOrWhiteSpace(strret)) strret = cell_text;

            return strret;
        }

        private bool DoWork(DataTable dt)
        {
            

            DataTable _dt = new DataTable();
            _dt = dt.Clone();
            _dt.Rows.Add();
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                _dt.Rows[0][i] = dt.Rows[0][i];
            }
            _dt.Rows.Add();
            string temp=new string('0',22);
            char[] barcode = temp.ToCharArray();
            DataTable values = ConstructBarCode(2, _dt, barcode);
            values.Rows[values.Rows.Count-1].Delete();
            db.BeginTransaction();
            for (int i = 1; i < values.Rows.Count; i++)
            {
                BarCodeEntity entity = new BarCodeEntity()
                {
                    SERIES_CODE=values.Rows[0][1] as string,
                    BAR_CODE = values.Rows[i][1] as string,
                    SEQ_1 = values.Rows[i][2] as string,
                    SEQ_2 = values.Rows[i][3] as string,
                    SEQ_3 = values.Rows[i][4] as string,
                    SEQ_4 = values.Rows[i][5] as string,
                    SEQ_5 = values.Rows[i][6] as string,
                    SEQ_6 = values.Rows[i][7] as string,
                    SEQ_7 = values.Rows[i][8] as string,
                    SEQ_8 = values.Rows[i][9] as string,
                    SEQ_9 = values.Rows[i][10] as string,
                    SEQ_10 = values.Rows[i][11] as string,
                    SEQ_11 = values.Rows[i][12] as string,
                    SEQ_12 = values.Rows[i][13] as string,
                    SEQ_13 = values.Rows[i][14] as string,
                    SEQ_14 = values.Rows[i][15] as string,
                    SEQ_15 = values.Rows[i][16] as string,
                    SEQ_16 = values.Rows[i][17] as string,
                    SEQ_17 = values.Rows[i][18] as string,
                    SEQ_18 = values.Rows[i][19] as string,
                    SEQ_19 = values.Rows[i][20] as string,
                    SEQ_20 = values.Rows[i][21] as string,
                    SEQ_21 = values.Rows[i][22] as string,
                    SEQ_22 = values.Rows[i][23] as string,
                };
                try
                {
                    List<BarCodeEntity> flag = db.Fetch<BarCodeEntity>("where SERIES_CODE=@0 and BAR_CODE=@1", entity.SERIES_CODE,entity.BAR_CODE);
                    if (flag == null || flag.Count == 0)
                    {
                        db.Insert(entity);
                    }
                    else
                    {
                        continue;
                    }

                }
                catch (Exception ex)
                {
                    continue;
                }
            }
            db.CompleteTransaction();
                return true;
        }

        private DataTable ConstructBarCode(int flag,DataTable dt,char[] barcode)
        {
            if (flag == 24)
            {
                dt.Rows[dt.Rows.Count - 1][1] = new string(barcode);
                dt.Rows.Add();
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    dt.Rows[dt.Rows.Count - 1][i] = dt.Rows[dt.Rows.Count - 2][i];
                }
                return dt;
            }
            else
            {

                //取条码ID 若为空则对应位置的条码号为0
                string seqs = dt.Rows[0][flag] as string;
                if (string.IsNullOrWhiteSpace(seqs))
                {
                    barcode[flag - 2] = '0';
                    dt.Rows[dt.Rows.Count - 1][flag] = "";

                    return ConstructBarCode(flag + 1, dt, barcode);
                }
                else
                {
                    string[] array = seqs.Split(',');

                    foreach (var a in array)
                    {
                        DetectBarCodeEntity entity = null;
                        try
                        {
                            entity = db.First<DetectBarCodeEntity>("where RMES_ID=@0", a);
                        }
                        catch
                        {

                        }
                        if (entity == null)
                        {
                            barcode[flag - 2] = '0';
                            dt.Rows[dt.Rows.Count - 1][flag] = "";

                            ConstructBarCode(flag + 1, dt, barcode);
                        }
                        else
                        {
                            barcode[flag - 2] = entity.SEQ_VALUE.ToCharArray()[0];
                            dt.Rows[dt.Rows.Count - 1][flag] = entity.SEQ_NAME;
                            
                            ConstructBarCode(flag + 1, dt, barcode);
                        }
                    }
                    f++;
                    return dt;
                }
            }

            
        }



    }
}