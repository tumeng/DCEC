using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rmes.Web.Base;
using Rmes.Pub.Data;
using Rmes.DA.Base;
using Rmes.DA.Entity;
using Rmes.DA.Factory;

namespace Rmes.WebApp.Rmes.Login
{
    public partial class relogin : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }
        protected void btnOk_Click1(object sender, EventArgs e)
        {
            string uname = txtUserName.Text;
            string upass = txtPass.Text;
            loginManager lm = new loginManager();
            bool login = lm.ReLoginIn(uname, upass, Request.UserHostAddress, Session.SessionID, "");
            if (lm.theLoginFlag.Equals("0"))
            {
                System.Web.Security.FormsAuthentication.RedirectFromLoginPage(lm.getUserCode(), false);
            }
        }
    }
}