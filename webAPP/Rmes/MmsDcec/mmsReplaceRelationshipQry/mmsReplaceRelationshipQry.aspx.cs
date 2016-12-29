using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rmes.Pub.Data;

namespace Rmes.WebApp.Rmes.MmsDcec.mmsReplaceRelationshipQry
{
    public partial class mmsReplaceRelationshipQry : System.Web.UI.Page
    {
        private dataConn dc = new dataConn();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["str"] != null)
                setCondition();
        }

        protected void CmdExport_Click(object sender, EventArgs e)
        {
            //导出
            ASPxGridView1.DataSource = dc.GetTable(Session["str"].ToString());
            ASPxGridView1.DataBind();
            ASPxGridViewExporter1.WriteXlsToResponse("零件替换关系");
        }

        protected void CmdOne_Click(object sender, EventArgs e)
        {
            //一对一替换
            Session["str"] = "select so,oldpart,newpart,pefile,createuser,to_char(usetime,'yyyy-mm-dd') usetime,to_char(endtime,'yyyy-mm-dd') endtime,createtime,settype,thgroup,sl from sjbomthset where settype='0' order by so,oldpart";
            setCondition();
        }

        protected void CmdMuti_Click(object sender, EventArgs e)
        {
            //多对多替换
            Session["str"] = "select so,oldpart,newpart,pefile,createuser,to_char(usetime,'yyyy-mm-dd') usetime,to_char(endtime,'yyyy-mm-dd') endtime,createtime,settype,thgroup ,sl  from sjbomthset where settype='1' order by so,oldpart";
            setCondition();
        }

        protected void cmdMutiExpire_Click(object sender, EventArgs e)
        {
            //到期多对多替换
            Session["str"] = "select so,oldpart,newpart,pefile,createuser,to_char(usetime,'yyyy-mm-dd') usetime,to_char(endtime,'yyyy-mm-dd') endtime,createtime,settype,thgroup,sl from sjbomthset where settype='1'  and endtime>sysdate-1  and endtime<sysdate+16 order by so,oldpart";
            setCondition();
        }

        protected void cmdOneExpire_Click(object sender, EventArgs e)
        {
            Session["str"] = "select so,oldpart,newpart,pefile,createuser,to_char(usetime,'yyyy-mm-dd') usetime,to_char(endtime,'yyyy-mm-dd') endtime,createtime,settype,thgroup,sl from sjbomthset where settype='0' and endtime>sysdate-1 and endtime<sysdate+16 order by so,oldpart";
            setCondition();
        }

        private void setCondition()
        {
            ASPxGridView1.DataSource = dc.GetTable(Session["str"].ToString());
            ASPxGridView1.DataBind();
        }
    }
}