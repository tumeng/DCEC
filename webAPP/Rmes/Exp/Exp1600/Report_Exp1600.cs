using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.IO;
using System.Data;
using System.Linq;
//以下引用，如果采用实体类，请全部复制到新页面
using PetaPoco;
using Rmes.DA.Base;
using Rmes.DA.Entity;
using Rmes.DA.Dal;
using Rmes.DA.Factory;
using Rmes.Pub.Data;
using Rmes.Web.Base;
using System.Collections.Generic;

namespace Rmes.WebApp.Rmes.Exp.Exp1600
{
    public partial class Report_Exp1600 : DevExpress.XtraReports.UI.XtraReport
    {
        public Report_Exp1600(DataTable ds)
        {
            InitializeComponent();
            SetDataBind(ds);
        }
        private void SetDataBind(DataTable ds)
        {
            DataSource = ds;
            List<UserEntity> user = UserFactory.GetAll();
            for (int i = 0; i < ds.Rows.Count; i++)
            {
                string usercode = ds.Rows[i]["CREATE_USER"].ToString();
                List<string> userName = (from s in user where s.USER_ID.Equals(usercode) select s.USER_NAME).ToList();
                if (userName.Count > 0)
                {
                    ds.Rows[i]["CREATE_USER"] = userName[0];
                }

            }
            this.xrTableCell12.DataBindings.Add("Text", DataSource, "ITEM_CODE");
            this.xrTableCell13.DataBindings.Add("Text", DataSource, "ITEM_NAME");
            this.xrTableCell14.DataBindings.Add("Text", DataSource, "ITEM_QTY");
            this.xrTableCell15.DataBindings.Add("Text", DataSource, "CREATE_USER");
            this.xrTableCell16.DataBindings.Add("Text", DataSource, "ORDER_CODE");
            this.xrTableCell17.DataBindings.Add("Text", DataSource, "STORE_CODE_1");
            this.xrTableCell18.DataBindings.Add("Text", DataSource, "STORE_CODE_2");
            this.xrTableCell19.DataBindings.Add("Text", DataSource, "WORK_TIME");
            this.xrTableCell20.DataBindings.Add("Text", DataSource, "PROJECT_CODE");
            this.xrTableCell21.DataBindings.Add("Text", DataSource, "PLAN_SO");
            
        }

    }
}
