using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Rmes.Pub.Data;
using Oracle.DataAccess.Client;

public partial class Rmes_Qms_OperateAjax : System.Web.UI.Page
{
    private string type = string.Empty;
    private string thesql = string.Empty;

    private string fault_group_table = "code_fault_group";
    comboInfo cmbinfo = new comboInfo();

    protected void Page_Load(object sender, EventArgs e)
    {
        this.type = Request.QueryString["type"] != null ? Request.QueryString["type"].ToString() : string.Empty;
        switch (this.type)
        {
            case "getfaults":
                this.GetFaultsByVinAndSta();
                break;
            case "getfaulststree":
                this.GetFaultsTree();
                break;
            case "getfc":
                this.GetFaultsChildren();
                break;
            case "getstopinf":
                this.GetStopInformation();
                break;
            case "gettable":
                this.GetTable();
                break;
            case "delfaulttree":
                this.delfaulttree();
                break;
            case "addfault":
                this.addfault();
                break;
        }
    }
    private void delfaulttree()
    {
        string nodeid = Request.QueryString["id"] == null ? "" : Request.QueryString["id"].ToString();
        if (nodeid == string.Empty)
        { 
            Response.Write("error:节点ID为空，无法删除。");
            Response.End();
        }

        string[] sp = nodeid.Split('_');
        int N = sp.Length;

        switch (N)
        {
            case 2:
                cmbinfo.GroupCode = sp[0];
                cmbinfo.PlaceCode = sp[1];
                break;
            case 3:
                cmbinfo.GroupCode = sp[0];
                cmbinfo.PlaceCode = sp[1];
                cmbinfo.PartCode = sp[2];
                break;
            case 4:
                cmbinfo.GroupCode = sp[0];
                cmbinfo.PlaceCode = sp[1];
                cmbinfo.PartCode = sp[2];
                cmbinfo.FaultCode = sp[3];
                break;
            default:
                Response.Write("error:根节点不能删除。");
                Response.End();
                break;
        }

            dataConn con = new dataConn();
            con.OpenConn();

            string strSQL = "select * from " + fault_group_table + " where fault_group_code='" + sp[0] + "'";
            con.theComd.CommandType = CommandType.Text;
            con.theComd.CommandText = strSQL;

            OracleDataReader dr = con.theComd.ExecuteReader();
            if (dr.Read())
            {
                cmbinfo.CompanyCode = dr["company_code"].ToString();
                cmbinfo.PLineCode = dr["pline_type_code"].ToString();
            }
            dr.Close();

            if (cmbinfo.CompanyCode == string.Empty || cmbinfo.PlaceCode == string.Empty || cmbinfo.GroupCode == string.Empty || cmbinfo.PLineCode == string.Empty)
            {
                Response.Write("error:缺乏关键信息，无法删除。");
                Response.End();
            }
            string[] strSQLs = new string[3];
            int M = 0;
            if (cmbinfo.FaultCode != string.Empty)
            {
                strSQL = "delete from RMES_REL_FAULTPART_FAULT where company_code='" + cmbinfo.CompanyCode + "' and pline_type_code='" + cmbinfo.PLineCode + "' and fault_group_code='" + cmbinfo.GroupCode + "' and fault_place_code='" + cmbinfo.PlaceCode + "' and fault_part_code='" + cmbinfo.PartCode + "' and fault_code='" + cmbinfo.FaultCode + "'";
                con.theComd.CommandText = strSQL;
                M += con.theComd.ExecuteNonQuery();
            }
            else
            {
                if (cmbinfo.PartCode != string.Empty)
                {
                    strSQL = "delete from RMES_REL_FAULTPART_FAULT where company_code='" + cmbinfo.CompanyCode + "' and pline_type_code='" + cmbinfo.PLineCode + "' and fault_group_code='" + cmbinfo.GroupCode + "' and fault_place_code='" + cmbinfo.PlaceCode + "' and fault_part_code='" + cmbinfo.PartCode + "'";
                    con.theComd.CommandText = strSQL;
                    M += con.theComd.ExecuteNonQuery();

                    strSQL = "delete from RMES_REL_FAULTPLACE_FAULTPART where company_code='" + cmbinfo.CompanyCode + "' and pline_type_code='" + cmbinfo.PLineCode + "' and fault_group_code='" + cmbinfo.GroupCode + "' and fault_place_code='" + cmbinfo.PlaceCode + "' and fault_part_code='" + cmbinfo.PartCode + "'";
                    con.theComd.CommandText = strSQL;
                    M += con.theComd.ExecuteNonQuery();

                }
                else
                {
                    strSQL = "delete from RMES_REL_FAULTPART_FAULT where company_code='" + cmbinfo.CompanyCode + "' and pline_type_code='" + cmbinfo.PLineCode + "' and fault_group_code='" + cmbinfo.GroupCode + "' and fault_place_code='" + cmbinfo.PlaceCode + "'";
                    con.theComd.CommandText = strSQL;
                    M += con.theComd.ExecuteNonQuery();

                    strSQL = "delete from RMES_REL_FAULTPLACE_FAULTPART where company_code='" + cmbinfo.CompanyCode + "' and pline_type_code='" + cmbinfo.PLineCode + "' and fault_group_code='" + cmbinfo.GroupCode + "' and fault_place_code='" + cmbinfo.PlaceCode + "'";
                    con.theComd.CommandText = strSQL;
                    M += con.theComd.ExecuteNonQuery();

                    strSQL = "delete from RMES_REL_GROUP_FAULTPLACE where company_code='" + cmbinfo.CompanyCode + "' and fault_group_code='" + cmbinfo.GroupCode + "' and fault_place_code='" + cmbinfo.PlaceCode + "'";
                    con.theComd.CommandText = strSQL;
                    M += con.theComd.ExecuteNonQuery();

                }
            
            }

            string retinfo = "success:成功删除" + M.ToString() + "条记录。";

            //string html = "<div id=\"dialog-confirm\" title=\"确认删除?\"><p><span class=\"ui-icon ui-icon-alert\"></span>"+retinfo+"</p></div>";
            //string script = "<script>$(function() { $( \"#dialog:ui-dialog\" ).dialog( \"destroy\" );$( \"#dialog-confirm\" ).dialog({resizable: false,height:210,modal: true,buttons: {\"确定删除\": function() {alert(\"成功删除！\");$( this ).dialog( \"close\" );},\"取消\": function() {$( this ).dialog( \"close\" );}}});});</script>";
            Response.Expires = -1;
            Response.Write(retinfo);
            Response.End();
    }
    private void GetTable()
    {
        string id = Request.QueryString["id"] as string;
        string outType = Request.QueryString["out"] as string;
        string pstart = Request.QueryString["pstart"] as string;
        string psize = Request.QueryString["psize"] as string;
        string filter = Request.QueryString["filter"] as string;


        if (id == null || id == string.Empty) Response.End();

        if (outType == null || outType == string.Empty) outType = "html";

        string strReturn = "";
        string strSQL = "";

        if (id == "faultplace")
        {
            string subSQL = "select t.rowid as id,t.fault_place_code as 代码,t.fault_place_name as 名称,b.pline_type_name as 生产线类型,a.company_name as 所属公司,t.delete_flag as 已删除,rownum as sq from code_fault_place t " +
                            "left join code_company a on a.company_code=t.company_code " +
                            "left join code_pline_type b on b.pline_type_code = t.pline_type_code ";
            if (filter == null || filter == string.Empty)
                filter = "";
            else
            {
                string[] f = filter.Split(',');
                filter = "";
                if (f[0] != string.Empty) filter += "and  t.fault_place_code like '%" + f[0] + "%' ";
                if (f[1] != string.Empty) filter += "and  t.fault_place_name like '%" + f[1] + "%' ";
                if (f[2] != string.Empty) filter += "and  b.pline_type_name like '%" + f[2] + "%' ";
                if (f[3] != string.Empty) filter += "and  a.company_name like '%" + f[3] + "%' ";

            }
            if (filter.StartsWith("and")) filter = " where " + filter.Substring(3, filter.Length - 3);
            subSQL = subSQL + filter + " order by t.rowid";

            strSQL = "select * from("+subSQL+") ";
            string where = "where ";
            if (pstart == null || pstart == string.Empty)
                pstart = "0";
            if (psize == null || psize == string.Empty)
                psize = (Convert.ToInt32(pstart)+16).ToString();
            else
                psize = (Convert.ToInt32(pstart) + Convert.ToInt32(psize)).ToString();
            where += " sq > " + pstart + " and sq <" + psize;
            strSQL = strSQL + where;
        }
        dataConn con = new dataConn();
        con.OpenConn();

        con.theComd.CommandType = CommandType.Text;
        con.theComd.CommandText = strSQL;
        OracleDataReader dr = con.theComd.ExecuteReader();

        if(outType=="html")
        {
            strReturn = "";
            while(dr.Read())
                strReturn += "<tr><td><input type=\"checkbox\" id=\"" + dr[0].ToString() + "\" /></td><td>" + dr[1].ToString() + "</td><td>" + dr[2].ToString() + "</td><td>" + dr[3].ToString() + "</td><td>" + dr[4].ToString() + "</td><td>" + (("1" == dr[5].ToString()) ? "是" : "&nbsp") + "</td></tr>";
        }
        strReturn += "";
        Response.Write(strReturn);
        Response.End();
    
    }
    private void addfault()
    {
        string strTemp = Request.QueryString["fault"];
        if (strTemp == null || strTemp == string.Empty) Response.End();
        string[] str = strTemp.Split(',');
        for(int i=0;i<str.Length;i++)
            str[i] = str[i].TrimStart().TrimEnd();
        if (str[0] == string.Empty || str[1] == string.Empty) Response.End();
        string[] code = new string[4];
        string companycode = "";
        string plinecode = "";

        dataConn con = new dataConn();
        con.OpenConn();

        con.theComd.CommandType = CommandType.Text;


        string strSQL = "select company_code from code_fault_group t where t.fault_group_name='"+str[0]+"'";
        con.theComd.CommandText = strSQL;
        companycode = con.theComd.ExecuteScalar().ToString();

        strSQL = "select pline_type_code from code_fault_group t where t.fault_group_name='" + str[0] + "'";
        con.theComd.CommandText = strSQL;
        plinecode = con.theComd.ExecuteScalar().ToString();

        //fault_group
        strSQL = "select fault_group_code from code_fault_group t where t.fault_group_name='" + str[0] + "'";
        con.theComd.CommandText = strSQL;
        code[0] = con.theComd.ExecuteScalar().ToString();

        //fault_place
        strSQL = "select fault_place_code from code_fault_place t where t.fault_place_name='" + str[1] + "'";
        con.theComd.CommandText = strSQL;
        code[1] = con.theComd.ExecuteScalar() as string;
        if (code[1]==null || code[1] == string.Empty)
        {
            strSQL = "select max(fault_place_code) from code_fault_place t";
            con.theComd.CommandText = strSQL;
            object obj = con.theComd.ExecuteScalar();
            strTemp = obj as string;
            int n=1;
            int m = 3;
            if (strTemp != null)
                n = Convert.ToInt32(strTemp.Substring(m, strTemp.Length - m)) + 1;
            code[1] = "FPC" + string.Format("{0:D" + (strTemp.Length - m).ToString() + "}", n);
            strSQL = "insert into code_fault_place (company_code,fault_place_code,fault_place_name,pline_type_code) values('" + 
                companycode + "','" + code[1] + "','" + str[1] + "','" + plinecode + "')";
            con.theComd.CommandText = strSQL;
            if (con.theComd.ExecuteNonQuery() != 1) code[1] = "";
        }

        if (str[2] != string.Empty && code[1]!="")
        {
            //fault_part
            strSQL = "select fault_part_code from code_fault_part t where t.fault_part_name='" + str[2] + "'";
            con.theComd.CommandText = strSQL;
            code[2] = con.theComd.ExecuteScalar() as string;
            if (code[2] == null || code[2] == string.Empty)
            {
                strSQL = "select max(fault_part_code) from code_fault_part t";
                con.theComd.CommandText = strSQL;
                object obj = con.theComd.ExecuteScalar();
                strTemp = obj as string;
                int n = 1;
                int m = 4;
                if (strTemp != null)
                    n = Convert.ToInt32(strTemp.Substring(m, strTemp.Length - m)) + 1;
                code[2] = "FPPC" + string.Format("{0:D" + (strTemp.Length - m).ToString() + "}", n);
                strSQL = "insert into code_fault_part (company_code,fault_part_code,fault_part_name,pline_type_code,fault_place_code) values('" +
                    companycode + "','" + code[2] + "','" + str[2] + "','" + plinecode + "','" + code[1] + "')";
                con.theComd.CommandText = strSQL;
                if (con.theComd.ExecuteNonQuery() != 1) code[2] = "";
            }

            if (str[3] != string.Empty && code[2]!="")
            {
                //fault
                strSQL = "select fault_code from code_fault t where t.fault_name='" + str[3] + "'";
                con.theComd.CommandText = strSQL;
                code[3] = con.theComd.ExecuteScalar() as string;
                if (code[3] == null || code[3] == string.Empty)
                {
                    strSQL = "select max(fault_code) from code_fault t";
                    con.theComd.CommandText = strSQL;
                    object obj = con.theComd.ExecuteScalar();
                    strTemp = obj as string;
                    int n = 1;
                    int m = 2;
                    if (strTemp != null)
                        n = Convert.ToInt32(strTemp.Substring(m, strTemp.Length - m)) + 1;
                    code[3] = "FC" + string.Format("{0:D" + (strTemp.Length - m).ToString() + "}", n);
                    strSQL = "insert into code_fault (company_code,fault_code,fault_name,pline_type_code) values('" +
                        companycode + "','" + code[3] + "','" + str[3] + "','" + plinecode + "')";
                    con.theComd.CommandText = strSQL;
                    if (con.theComd.ExecuteNonQuery() != 1) code[3] = "";
                }
            }
        }
        int N = 0;
        string retCode = "", retName = "";
        if (companycode!=string.Empty && plinecode!=string.Empty && code[0] != string.Empty && code[1]!=string.Empty)
        {
            ///插入group_place关系表
            strSQL = "insert into RMES_REL_GROUP_FAULTPLACE (company_code,fault_group_code,fault_place_code) values('" 
                + companycode + "','" + code[0] + "','" + code[1] + "')";
            con.theComd.CommandText = strSQL;
            try
            {
                N += con.theComd.ExecuteNonQuery();
            }
            catch
            {
                N += 0;
            }
            retCode = code[0] + "_" + code[1]; retName = str[1];

            if (code[2]!=null && code[2] != string.Empty)
            {
                strSQL = "insert into RMES_REL_FAULTPLACE_FAULTPART (company_code,pline_type_code,fault_group_code,fault_place_code,fault_part_code) values('" 
                    + companycode + "','" + plinecode + "','" + code[0] + "','" + code[1] + "','" + code[2] + "')";
                con.theComd.CommandText = strSQL;
                try
                {
                    N += con.theComd.ExecuteNonQuery();
                }
                catch
                {
                    N += 0;
                }
                retCode += "_" + code[2]; retName = str[2];
                if (code[3]!=null && code[3] != string.Empty)
                {
                    strSQL = "insert into RMES_REL_FAULTPART_FAULT (company_code,pline_type_code,fault_group_code,fault_place_code,fault_part_code,fault_code) values('"
                        + companycode + "','" + plinecode + "','" + code[0] + "','" + code[1] + "','" + code[2] + "','" + code[3] + "')";
                    con.theComd.CommandText = strSQL;
                    try
                    {
                        N += con.theComd.ExecuteNonQuery();
                    }
                    catch
                    {
                        N += 0;
                    }
                    retCode += "_" + code[3]; retName = str[3];
                }
            }
            
        }
        string strRet = "error:" + code[0] + "," + code[1] + "," + code[2] + "," + code[3]+","+N.ToString();
        if (N > 0) strRet = "success:"+retCode+","+retName;
        Response.Expires = -1;
        Response.Write(strRet);
        Response.End();
    }
    private void GetStopInformation()
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.AppendHeader("Cache-Control", "must-revalidate");
        string scode = Request.QueryString["scode"] != null ? Request.QueryString["scode"].ToString().Replace("_","-") : string.Empty;
        string sta = Request.QueryString["sta"] != null ? Request.QueryString["sta"].ToString() : string.Empty;
        string end = Request.QueryString["end"] != null ? Request.QueryString["end"].ToString() : string.Empty;
        this.thesql ="select t.andon_alert_time as t1,t.andon_answer_time as t2,t.location_code as station from data_andon_alert t where ";
        this.thesql += " not (t.andon_alert_time>to_date('" + end + "','yyyy-mm-dd hh24:mi:ss') or t.andon_answer_time<to_date('" + sta + "','yyyy-mm-dd hh24:mi:ss')) and t.company_code='01' and t.location_code='" + scode + "' ORDER BY T.ANDON_ALERT_TIME ASC";
        dataConn dc = new dataConn(thesql);
        DataTable dt = dc.GetTable();
        string res = string.Empty;
        if (dt.Rows.Count != 0)
        {
            res = "<table border=1 cellspacing=0 cellpadding=0 ><tr><td align=center>序号</td><td align=center>起始时间</td><td align=center>截止时间</td><td align=center>时间间隔</td></tr>";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                res += "<tr><td>" + Convert.ToString(i + 1) + "</td><td>" + dt.Rows[i]["t1"].ToString() + "</td><td>" + dt.Rows[i]["t2"].ToString() + "</td>";
                DateTime dt1 = Convert.ToDateTime(dt.Rows[i]["t1"].ToString());
                DateTime dt2 = Convert.ToDateTime(dt.Rows[i]["t2"].ToString());
                System.TimeSpan st = dt2.Subtract(dt1);
                int aa = (int)Math.Floor(st.TotalHours);
                string tmt = string.Empty;
                if (aa != 0 && tmt == string.Empty)
                    tmt += aa.ToString() + "小时";
                if (st.Minutes != 0 || (tmt != string.Empty && st.Minutes == 0))
                    tmt += st.Minutes.ToString() + "分";
                if (st.Seconds != 0 || (tmt != string.Empty && st.Seconds == 0))
                    tmt += st.Seconds.ToString() + "秒";
                res += "<td>" + tmt + "</td></tr>";
            }
            res += "</table>";
        }
        else
            res = "无停线记录";
        Response.Write(res);
    }
    private void GetFaultsChildren()
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.AppendHeader("Cache-Control", "must-revalidate");

        string strlist = Request.QueryString["str"]!=null?Request.QueryString["str"].ToString():string.Empty;
        string ids = string.Empty;
        string names = string.Empty;
        if (strlist != string.Empty)
        {
            char[] sep = { ',' };
            char[] sep1 = { '_' };
            string[] arr = strlist.Split(sep);
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] != string.Empty)
                {
                    if (arr[i].Substring(0, 2) == "A_" || arr[i].Substring(0, 2) == "B_" || arr[i].Substring(0, 2) == "C_" || arr[i].Substring(0, 2) == "D_")
                    {
                        //char[] sep1 ={ '|' };
                        //string[] brr = arr[i].Substring(2).Split(sep1);
                        {
                            for (int j = 0; j < arr.Length; j++)
                            {
                                if (arr[j].IndexOf(arr[i]) >= 0 && arr[j] != arr[i])
                                    arr[j] = string.Empty;
                            }
                            switch (arr[i].Substring(0, 2))
                            {
                                case "A_":
                                    thesql = "select distinct a.fault_code ID,a.fault_name Name from code_fault a where a.fault_code in (select b.fault_code from rmes_rel_faultpart_fault b";
                                    thesql += " where b.fault_part_code in (select c.fault_part_code from rmes_rel_faultplace_faultpart c where c.fault_place_code in (select d.fault_place_code";
                                    thesql += " from rmes_rel_group_faultplace d where d.fault_group_code in (select e.fault_group_code from rmes_rel_station_faultgroup e where e.fault_station_code='" + arr[i].Split(sep1)[arr[i].Split(sep1).Length - 1].ToString() + "'))))";
                                    break;
                                case "B_":
                                    thesql = "select distinct a.fault_code ID,a.fault_name Name from code_fault a where a.fault_code in (select b.fault_code from rmes_rel_faultpart_fault b";
                                    thesql += " where b.fault_part_code in (select c.fault_part_code from rmes_rel_faultplace_faultpart c where c.fault_place_code in (select d.fault_place_code";
                                    thesql += " from rmes_rel_group_faultplace d where d.fault_group_code ='" + arr[i].Split(sep1)[arr[i].Split(sep1).Length - 1].ToString() + "')))";
                                    break;
                                case "C_":
                                    thesql = "select distinct a.fault_code ID,a.fault_name Name from code_fault a where a.fault_code in (select b.fault_code from rmes_rel_faultpart_fault b";
                                    thesql += " where b.fault_part_code in (select c.fault_part_code from rmes_rel_faultplace_faultpart c where c.fault_place_code ='" + arr[i].Split(sep1)[arr[i].Split(sep1).Length - 1].ToString() + "'))";
                                    break;
                                case "D_":
                                    thesql = "select distinct a.fault_code ID,a.fault_name Name from code_fault a where a.fault_code in (select b.fault_code from rmes_rel_faultpart_fault b";
                                    thesql += " where b.fault_part_code ='" + arr[i].Split(sep1)[arr[i].Split(sep1).Length - 1].ToString() + "')";
                                    break;
                            }
                            dataConn dc = new dataConn(thesql);
                            DataTable dt = dc.GetTable();
                            for (int m = 0; m < dt.Rows.Count; m++)
                            {
                                if (ids == string.Empty)
                                    ids = dt.Rows[m]["ID"].ToString();
                                else
                                {
                                    string tids = "," + ids + ",";
                                    if (tids.IndexOf("," + dt.Rows[m]["ID"].ToString() + ",") == -1)
                                        ids += "," + dt.Rows[m]["ID"].ToString();
                                }
                                if (names == string.Empty)
                                    names = dt.Rows[m]["Name"].ToString();
                                else
                                {
                                    string tnames = "," + names + ",";
                                    if (tnames.IndexOf(","+dt.Rows[m]["Name"].ToString()+",") ==-1)
                                        names += "," + dt.Rows[m]["Name"].ToString();
                                }
                            }
                        }
                    }
                    else if (arr[i].Substring(0, 2) == "E_")
                    {
                        //char[] sep1 ={ '|' };
                        //string[] brr = arr[i].Substring(2).Split(sep1);
                        thesql = "select distinct a.fault_code ID,a.fault_name Name from code_fault a where a.fault_code='" + arr[i].Split(sep1)[arr[i].Split(sep1).Length - 1].ToString() + "'";
                        dataConn dc = new dataConn(thesql);
                        DataTable dt = dc.GetTable();
                        for (int m = 0; m < dt.Rows.Count; m++)
                        {
                            if (ids == string.Empty)
                                //ids = dt.Rows[m]["ID"].ToString();
                                ids = arr[i].Split(sep1)[arr[i].Split(sep1).Length - 1].ToString();
                            else
                            {
                                string tids = "," + ids + ",";
                                //if (tids.IndexOf("," + dt.Rows[m]["ID"].ToString() + ",") == -1)
                                if (tids.IndexOf("," + arr[i].Split(sep1)[arr[i].Split(sep1).Length - 1].ToString() + ",") == -1)
                                    ids += "," + arr[i].Split(sep1)[arr[i].Split(sep1).Length - 1].ToString();
                            }
                            if (names == string.Empty)
                                names = dt.Rows[m]["Name"].ToString();
                            else
                            {
                                string tnames = "," + names + ",";
                                if (tnames.IndexOf("," + dt.Rows[m]["Name"].ToString() + ",") == -1)
                                    names += "," + dt.Rows[m]["Name"].ToString();
                            }
                        }
                    }
                }
            }
        }
        Response.Write(ids+"|"+names);
    }
    private void GetFaultsByVinAndSta()
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.AppendHeader("Cache-Control", "must-revalidate");

        Response.ContentType = "text/xml";
        Response.Charset = "utf-8";
        string outmsg = "<?xml version=\"1.0\" encoding=\"utf-8\"?><root>";
        string vin = Request.QueryString["vin"] != null ? Request.QueryString["vin"].ToString() : string.Empty;
        string sta = Request.QueryString["sta"] != null ? Request.QueryString["sta"].ToString() : string.Empty;
        string con = Request.QueryString["con"] != null ? Request.QueryString["con"].ToString() : string.Empty;
        if (vin != string.Empty && sta != string.Empty)
        {
            sta = this.base64Decode(sta);
            this.thesql = "select s.station_name stationname,m.fault_place_name,n.fault_part_name,p.fault_name,t.fault_key,q.work_time,q.quality_status,r.station_name from data_vin_fault t left join code_fault_place m on m.fault_place_code=t.fault_place_code";
            this.thesql +=" left join code_fault_part n on n.fault_part_code=t.fault_part_code left join code_fault p on p.fault_code=t.fault_code left join data_quality q on q.sn=t.sn and q.fault_key=t.fault_key and q.delete_flag =0";
            this.thesql += " and t.delete_flag=0 ";
            this.thesql += " left join code_station s on s.station_code=t.station_code";
            this.thesql += " left join code_station r on r.station_code=q.station_code where t.delete_flag=0 and t.SN='" + vin + "' AND r.station_name='" + sta.Trim() + "'";
            this.thesql += " ORDER BY WORK_TIME ASC";
            dataConn dc = new dataConn(thesql);
            DataTable dt = dc.GetTable();
            int fk = Convert.ToInt32(con);
            for (int i = 1; i <= fk; i++)
            {
                string tstr = "<item value=\""+i.ToString()+"\"";
                string mstr = string.Empty;
                string vstr = string.Empty;
                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    if (dt.Rows[j]["FAULT_KEY"].ToString() == i.ToString())
                    {
                        if (mstr == string.Empty)
                        {
                            mstr = " qua=\"" + dt.Rows[j]["QUALITY_STATUS"].ToString() + "\" sta=\"" + dt.Rows[j]["STATION_NAME"].ToString() + "\" time=\"" + dt.Rows[j]["WORK_TIME"].ToString() + "\"";
                            tstr += mstr;
                        }
                        vstr += "<fault place=\"[" + dt.Rows[j]["stationname"] + "]" + dt.Rows[j]["FAULT_PLACE_NAME"].ToString() + "\" part=\"" + dt.Rows[j]["FAULT_PART_NAME"].ToString() + "\" faultt=\"" + dt.Rows[j]["FAULT_NAME"] + "\" />";
                    }
                }
                tstr +=">";
                tstr +=vstr;
                tstr += "</item>";
                outmsg += tstr;
            }
        }
        outmsg += "</root>";
        this.Response.Write(outmsg);
    }
    private void GetFaultsTree()
    {
        Response.Charset = "utf-8";
        Response.Expires = -1;

        string ids = Request.QueryString["id"] != null ? Request.QueryString["id"].ToString() : string.Empty;
        string output = Request.QueryString["out"] != null ? Request.QueryString["out"].ToString() : "xml";

        if (ids == "undefined") Response.End();
        int n = 0;

        if (ids == string.Empty)    ///建立树的根节点。
            this.thesql = "select distinct t.fault_group_code as id,a.fault_group_name as name from rmes_rel_station_faultgroup t left join code_fault_group a on a.fault_group_code = t.fault_group_code where t.company_code='C2' order by a.fault_group_name";
        else
        {
            char[] sep1={'_'};
            string[] arr = ids.Split(sep1);
            n = arr.Length;
            switch (n)
            {
                case 1:
                    //strSQL = "select ";
                    this.thesql = "select t.fault_place_code as id,a.fault_place_name as name from rmes_rel_group_faultplace t left join code_fault_place a on a.fault_place_code = t.fault_place_code where t.company_code='C2' and t.fault_group_code='"+arr[0]+"' order by a.fault_place_name";
                    break;
                case 2:
                    this.thesql = "select t.fault_part_code as id,a.fault_part_name as name from rmes_rel_faultplace_faultpart t left join code_fault_part a on a.fault_part_code = t.fault_part_code where t.company_code='C2' and t.pline_type_code in (select x.pline_type_code from code_fault_group x where x.fault_group_code='"+arr[0]+"') and t.fault_group_code='" + arr[0] + "' and t.fault_place_code='" + arr[1] + "' order by a.fault_part_name";
                    break;
                case 3:
                    this.thesql = "select t.fault_code as id,a.fault_name as name from rmes_rel_faultpart_fault t left join code_fault a on a.fault_code = t.fault_code where t.company_code='C2' and t.pline_type_code in (select x.pline_type_code from code_fault_group x where x.fault_group_code='"+arr[0]+"') and t.fault_group_code='"+arr[0]+"' and t.fault_place_code='"+arr[1]+"' and t.fault_part_code = '"+arr[2]+"' order by a.fault_name";
                    break;
                default:
                    this.thesql = "";
                    break;
            }
        }
        string retStr = "";

        if (this.thesql != string.Empty)
        {
            dataConn dc = new dataConn();
            dc.OpenConn();
            dc.theComd.CommandType = CommandType.Text;
            dc.theComd.CommandText = thesql;

            OracleDataReader dr = dc.theComd.ExecuteReader();
            string prefix = (ids == string.Empty) ? "" : (ids + "_");


            if(output=="xml")
            {
                Response.ContentType = "text/xml";

                System.Xml.XmlDocument docXML = new System.Xml.XmlDocument();
                System.Xml.XmlElement allnode = docXML.CreateElement("root");
                while (dr.Read())
                {
                    System.Xml.XmlElement emper = docXML.CreateElement("item");
                    emper = docXML.CreateElement("item");
                    emper.SetAttribute("id", prefix + dr["id"].ToString());
                    emper.InnerXml = "<content><name id=\"" + prefix + dr["id"].ToString() + "\">" + dr["name"].ToString() + "</name></content>";

                    if (n != 3)
                        emper.SetAttribute("state", "closed");
                    allnode.AppendChild(emper);
                }
                if (allnode != null && allnode.ChildNodes.Count > 0)
                {
                    docXML.AppendChild(allnode);
                    retStr = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" + docXML.InnerXml;
                    docXML.RemoveAll();
                    docXML = null;
                }
            }
            if (output == "json")
            {
                Response.ContentType = "text/html";

                string name = "", id = "";
                retStr = "";
                while (dr.Read())
                {
                    id = dr["id"].ToString();
                    name = dr["name"].ToString();

                    retStr += "{ \"attr\":{\"id\":\""+ prefix + id+"\"},\"data\":{\"title\":\""+name+"\"} },\r\n";
                }

                //if (retStr.EndsWith(",")) retStr = retStr.Substring(0, retStr.Length - 1);
                retStr = "[ \r\n"+ retStr+" ]";
            }

            dc.theConn.Close();
        }
        Response.Write(retStr);
        Response.End();
    }
    private void insertleaf(System.Xml.XmlElement source, System.Xml.XmlElement[] emp, string DepID, int n)
    {
        source.AppendChild(emp[n]);
    }
    private void insertnode(System.Xml.XmlElement[] source, string parID, int n)
    {
        for (int j = 0; j < n; j++)
            if ((parID.ToString()) == source[j].GetAttribute("id").ToString())
                source[j].AppendChild(source[n]);
    }
    protected void iniSearch(System.Xml.XmlElement el, string text, string id)
    {

        el.SetAttribute("text", text.ToString());
        el.SetAttribute("id", id.ToString());
        el.SetAttribute("im0", "books_close.gif");
        el.SetAttribute("im1", "books_open.gif");
        el.SetAttribute("im2", "books_close.gif");
        el.SetAttribute("child", "1");
    }
    protected void in0(System.Xml.XmlElement el, string text, string id)
    {
        el.SetAttribute("text", text.ToString());
        el.SetAttribute("id", id.ToString());
        el.SetAttribute("im0", "book.gif");
        el.SetAttribute("im1", "bookopen.gif");
        el.SetAttribute("im2", "book.gif");
        el.SetAttribute("child", "1");
    }
    protected void in1(System.Xml.XmlElement el, string text, string id, string sex)
    {
        el.SetAttribute("text", text.ToString());
        el.SetAttribute("id", id.ToString());
        if (sex == "false")
            el.SetAttribute("im0", "file.gif");
        else
            el.SetAttribute("im0", "file.gif");
        el.SetAttribute("child", "0");
    }

    #region 解密由base64加密的信息
    public string base64Decode(string data)
    {
        try
        {
            System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
            System.Text.Decoder utf8Decode = encoder.GetDecoder();
            byte[] todecode_byte = Convert.FromBase64String(data);
            int charCount = utf8Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
            char[] decoded_char = new char[charCount];
            utf8Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
            string result = new String(decoded_char);
            //result=result.Replace("&amp;","&");
            return result;
        }
        catch (Exception e)
        {
            throw new Exception("Error in base64Decode" + e.Message);
        }
    }
    #endregion

    public class comboInfo
    {
        public string CompanyCode = "";
        public string PLineCode = "";
        public string GroupCode = "";
        public string PlaceCode = "";
        public string PartCode = "";
        public string FaultCode = "";

    }

}
