using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using PetaPoco;
using Rmes.DA.Base;
using Rmes.DA.Entity;
using Rmes.DA.Dal;
using Rmes.DA.Factory;
using Rmes.Pub.Data;
using DevExpress.Web.ASPxEditors;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxClasses;
using Rmes.Pub.Data;
using System.Collections.Generic;
using Rmes.Web.Base;

/**
 * 功能概述：跨线模式定义
 * 作者：杨少霞
 * 创建时间：2016-07-13
 **/

//namespace Rmes.WebApp.Rmes.Epd.epd1A00

public partial class epd1A001 : BasePage
    {
        private dataConn dc = new dataConn();
        private PubCs pc = new PubCs();
        public string theCompanyCode;
        protected void Page_Load(object sender, EventArgs e)
        {
            userManager theUserManager = (userManager)Session["theUserManager"];
            theCompanyCode = theUserManager.getCompanyCode();

            string sqlOld = "select RMES_ID,PLINE_NAME from CODE_PRODUCT_LINE where COMPANY_CODE = '" + theCompanyCode + "' order by PLINE_NAME";
            //dropPlineCode.DataSource = dc.GetTable(sqlOld);
            //dropPlineCode.DataBind();



            if (!IsPostBack)
            {

            }
            //setCondition();
        }
    }
