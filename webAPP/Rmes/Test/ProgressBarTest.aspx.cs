using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Rmes.WebApp.Rmes.Test
{
    public partial class ProgressBarTest : System.Web.UI.Page
    {
        private int state = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            int maxnum = Convert.ToInt32(Request.Params[0]);
            if (Session["State"] != null)
            {
                state = Convert.ToInt32(Session["State"].ToString());
            }
            else
            {
                Session["State"] = 0;
            }
            if (state > 0 && state <= maxnum)
            {
                this.p.Maximum = maxnum;
                this.p.Value = state;

                Page.RegisterStartupScript("", "<script>window.setTimeout('window.form1.submit()',100);</script>");
            }
            if (state >= maxnum)
            {

                
                Page.RegisterStartupScript("", "<script>window.close();</script>");
            }
        }


    }
}