using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//以下引用，如果采用实体类，请全部复制到新页面
using PetaPoco;
using Rmes.DA.Base;
using Rmes.DA.Entity;
using Rmes.DA.Dal;
using Rmes.DA.Factory;
using Rmes.Pub.Data;
using Rmes.Web.Base;
using System.Data;
using DevExpress.Web.ASPxEditors;
using DevExpress.Web.ASPxClasses;
using DevExpress.Web.ASPxGridView;

namespace Rmes.WebApp.Rmes.Epd.epd4200
{
    public partial class epd4200 : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            BindData();
        }

        protected void BindData()
        {
            List<ItemEntity> allEntity = ItemFactory.GetAll();
            ASPxGridView1.DataSource = allEntity;

            GridViewDataComboBoxColumn classCode = ASPxGridView1.Columns["ITEM_CLASS_CODE"] as GridViewDataComboBoxColumn;
            classCode.PropertiesComboBox.DataSource = allEntity.Select(m => m.ITEM_CLASS_CODE).Distinct().ToList<string>();

            GridViewDataComboBoxColumn type = ASPxGridView1.Columns["ITEM_TYPE"] as GridViewDataComboBoxColumn;
            type.PropertiesComboBox.DataSource = allEntity.Select(m => m.ITEM_TYPE).Distinct().ToList<string>();


            ASPxGridView1.DataBind();
        }
    }
}