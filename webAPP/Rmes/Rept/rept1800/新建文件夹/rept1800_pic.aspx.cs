using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rmes.Pub.Data;

namespace Rmes.WebApp.Rmes.Rept.rept1800
{
    public partial class rept1800_pic : System.Web.UI.Page
    {
        private dataConn dc = new dataConn();
        protected void Page_Load(object sender, EventArgs e)
        {
            userManager theUserManager = (userManager)Session["theUserManager"];
            string companyCode = theUserManager.getCompanyCode();
            string urls = Request["Pic"].ToString();
            string[] _urls = urls.Split('|');
            string plinecode = "E";
            if (_urls[_urls.Length - 1] != null)
            {
                if (_urls[_urls.Length - 1] != "")
                    plinecode = _urls[_urls.Length - 1];
                    //plinecode = dc.GetValue("select pline_code from code_product_line where rmes_id='" + _urls[_urls.Length - 1] + "'");
            }
            string sql = "";
            string Iurl = "";
            if (_urls[0] == "N")
            {
                sql = "select INTERNAL_VALUE FROM CODE_INTERNAL WHERE COMPANY_CODE='" + companyCode
                   + "' AND INTERNAL_TYPE_CODE='PATH' AND INTERNAL_CODE='PROCESSNOTEPATH'";
                Iurl = dc.GetValue(sql);
                for (int i = 1; i < _urls.Length-1; i++)
                {
                    string name = "ima" + i.ToString();
                    Image ima = new Image();
                    //ima.ImageUrl = "\\\\192.168.113.137\\mes共享\\FILES\\PROCESSNOTEPIC\\" + plinecode + "\\" + _urls[i].Split('\\')[_urls[i].Split('\\').Length - 1];
                    ima.ImageUrl = @Iurl + "\\" + plinecode + "\\" + _urls[i].Split('\\')[_urls[i].Split('\\').Length - 1];
                    ima.DataBind();
                    GraphPlaceHolder.Controls.Add(ima);
                }
            }
            else if (_urls[0] == "A")
            {
                sql = "select INTERNAL_VALUE FROM CODE_INTERNAL WHERE COMPANY_CODE='" + companyCode
                   + "' AND INTERNAL_TYPE_CODE='PATH' AND INTERNAL_CODE='QUALITYALERTPATH'";
                Iurl = dc.GetValue(sql);
                for (int i = 1; i < _urls.Length; i++)
                {
                    string name = "ima" + i.ToString();
                    Image ima = new Image();
                    //ima.ImageUrl = "\\\\192.168.113.137\\mes共享\\FILES\\QUALITYALERTPIC\\" + plinecode + "\\" + _urls[i].Split('\\')[_urls[i].Split('\\').Length - 1];
                    ima.ImageUrl = @Iurl + "\\" + plinecode + "\\" + _urls[i].Split('\\')[_urls[i].Split('\\').Length - 1]; 
                    ima.DataBind();
                    GraphPlaceHolder.Controls.Add(ima);
                }
            }

        }
    }
}