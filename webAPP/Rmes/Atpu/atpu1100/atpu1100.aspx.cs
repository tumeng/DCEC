using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PetaPoco;
using Rmes.DA.Base;
using Rmes.DA.Entity;
using Rmes.DA.Dal;
using Rmes.DA.Factory;
using Rmes.Pub.Data;
using Rmes.Web.Base;
using System.Data;
using DevExpress.Web.ASPxGridView;

/**
 * 功能概述：FR参数查询
 * 作者：游晓航
 * 创建时间：2016-06-20
 */
namespace Rmes.WebApp.Rmes.Atpu.atpu1100
{
    public partial class atpu1100 : BasePage
    {
        private dataConn dc = new dataConn();
        PubCs thePubCs = new PubCs();
        protected void Page_Load(object sender, EventArgs e)
        {
            DataTable dt = dc.GetTable("select * from  ATPUFRNAMEPLATE");
            ASPxGridView1.DataSource = dt;
            ASPxGridView1.DataBind();

        }
    }
}