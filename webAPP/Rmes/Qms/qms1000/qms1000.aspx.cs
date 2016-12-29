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
using System.Collections.Generic;
using Rmes.Web.Base;

public partial class Rmes_Qms_Qms1000_qms1000 : Rmes.Web.Base.BasePage
{
    string thesql = "";
    protected void Page_Load(object sender, EventArgs e)
    {
    }
    protected void ASPxTreeList1_VirtualModeNodeCreating(object sender, DevExpress.Web.ASPxTreeList.TreeListVirtualModeNodeCreatingEventArgs e)
    {
        string[] ttt = e.NodeObject as string[];
        e.NodeKeyValue = ttt[0];
        e.IsLeaf = (ttt[0].Split('_').Length==4)?true:false;
        e.SetNodeValue("name", ttt[1]);
    }
    protected void ASPxTreeList1_VirtualModeCreateChildren(object sender, DevExpress.Web.ASPxTreeList.TreeListVirtualModeCreateChildrenEventArgs e)
    {
        string ids = "";
        int n = 0;
        if (e.NodeObject==null)
        {
            this.thesql = "select distinct t.fault_group_code as id,a.fault_group_name as name from rmes_rel_station_faultgroup t left join code_fault_group a on a.fault_group_code = t.fault_group_code where t.company_code='01' order by a.fault_group_name";
        }
        else
        {
            ids = ((string[])e.NodeObject)[0];
            char[] sep1 = { '_' };
            string[] arr = ids.Split(sep1);
            n = arr.Length;
            switch (n)
            {
                case 1:
                    this.thesql = "select t.fault_place_code as id,a.fault_place_name as name from rmes_rel_group_faultplace t left join code_fault_place a on a.fault_place_code = t.fault_place_code where t.company_code='01' and t.fault_group_code='" + arr[0] + "' order by a.fault_place_name";
                    break;
                case 2:
                    this.thesql = "select t.fault_part_code as id,a.fault_part_name as name from rmes_rel_faultplace_faultpart t left join code_fault_part a on a.fault_part_code = t.fault_part_code where t.company_code='01' and t.pline_type_code in (select x.pline_type_code from code_fault_group x where x.fault_group_code='" + arr[0] + "') and t.fault_group_code='" + arr[0] + "' and t.fault_place_code='" + arr[1] + "' order by a.fault_part_name";
                    break;
                case 3:
                    this.thesql = "select t.fault_code as id,a.fault_name as name from rmes_rel_faultpart_fault t left join code_fault a on a.fault_code = t.fault_code where t.company_code='01' and t.pline_type_code in (select x.pline_type_code from code_fault_group x where x.fault_group_code='" + arr[0] + "') and t.fault_group_code='" + arr[0] + "' and t.fault_place_code='" + arr[1] + "' and t.fault_part_code = '" + arr[2] + "' order by a.fault_name";
                    break;
                default:
                    this.thesql = "";
                    break;
            }
        }
        //string retStr = "";
        List<string[]> children = new List<string[]>();

        if (this.thesql != string.Empty)
        {
            dataConn dc = new dataConn();
            dc.OpenConn();
            dc.theComd.CommandType = CommandType.Text;
            dc.theComd.CommandText = thesql;

            OracleDataReader dr = dc.theComd.ExecuteReader();
            string prefix = (ids == string.Empty) ? "" : (ids + "_");
            while (dr.Read())
            {
                    children.Add(new string[] {prefix+dr["id"].ToString(),dr["name"].ToString()});
            }
            dr.Close();
            e.Children = children;
        }        
    }
}