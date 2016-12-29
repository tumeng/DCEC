using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rmes.Web.Base;
using Rmes.Pub.Data;
using PetaPoco;
using Rmes.DA.Base;
using Rmes.DA.Factory;
using Rmes.DA.Entity;
using DevExpress.Web.ASPxEditors;

namespace Rmes.WebApp.Rmes.Epd.epd1900
{
    public partial class epd1901 : BasePage
    {
        private dataConn dc = new dataConn();
        PubCs thePubCs = new PubCs();

        public string theCompanyCode;

        public Database db = DB.GetInstance();
        protected void Page_Load(object sender, EventArgs e)
        {
            userManager theUserManager = (userManager)Session["theUserManager"];
            theCompanyCode = theUserManager.getCompanyCode();

            setCondition();
        }

        private void setCondition()
        {
            //绑定表数据
            ASPxGridView1.DataSource = DepartmentFactory.GetAll();
            ASPxGridView1.DataBind();
        }

        protected void ASPxGridView1_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            //判断当前记录是否可以删除
            //string strDelCode = e.Values["PLAN_CODE"].ToString();

            string rmes_id = e.Values["RMES_ID"].ToString();
            DepartmentEntity detEntity = new DepartmentEntity { RMES_ID = rmes_id };

            db.Delete(detEntity);

            setCondition();
            e.Cancel = true;
        }


        protected void ASPxGridView1_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            //插入新纪录，应先做检查是否合法

            DepartmentEntity detEntity = new DepartmentEntity();

            detEntity.COMPANY_CODE = theCompanyCode;
            detEntity.DEPT_CODE = e.NewValues["DEPT_CODE"].ToString().Trim();
            detEntity.DEPT_NAME = e.NewValues["DEPT_NAME"].ToString().Trim();
            detEntity.PARENT_DEPT = e.NewValues["PARENT_DEPT"].ToString().Trim();
            detEntity.DEPT_REMARK = e.NewValues["DEPT_REMARK"].ToString().Trim();

            db.Insert(detEntity);

            e.Cancel = true;
            ASPxGridView1.CancelEdit();
            setCondition();

        }


        protected void ASPxGridView1_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            //修改记录

            string rmes_id = e.OldValues["RMES_ID"].ToString();

            DepartmentEntity detEntity = DepartmentFactory.GetByKey(rmes_id);

            detEntity.DEPT_NAME = e.NewValues["DEPT_NAME"].ToString().Trim();
            detEntity.PARENT_DEPT = e.NewValues["PARENT_DEPT"].ToString().Trim();
            detEntity.DEPT_REMARK = e.NewValues["DEPT_REMARK"].ToString().Trim();

            db.Update(detEntity);

            e.Cancel = true;
            ASPxGridView1.CancelEdit();
            setCondition();

        }
        protected void ASPxGridView1_HtmlEditFormCreated(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewEditFormEventArgs e)
        {
            //创建Editform，对其中某些字段进行属性设置

            if (!ASPxGridView1.IsNewRowEditing && ASPxGridView1.IsEditing)
            {
                ///主键不可以修改
                (ASPxGridView1.FindEditFormTemplateControl("txtDeptCode") as ASPxTextBox).Enabled = false;
            }
        }
    }
}