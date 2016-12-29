using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PetaPoco;

#region 北自所Rmes命名空间引用
using Rmes.DA.Base;
using Rmes.DA.Entity;
using Rmes.DA.Dal;
using Rmes.DA.Factory;
#endregion


namespace Rmes.DA.Entity
{
    [PetaPoco.TableName("CODE_ITEM_LINESIDE")]
    //下面这行请自行填写关键字段并取消注释
    [PetaPoco.PrimaryKey("RMES_ID", sequenceName = "SEQ_RMES_ID")]

    public class ItemLineSideEntity
    {
        public string RMES_ID { get; set; }
        public string COMPANY_CODE { get; set; }
        public string ITEM_CODE { get; set; }
        public string ITEM_NAME { get; set; }
        public string UNIT_CODE { get; set; }
        public string LINESIDE_STORE_CODE { get; set; }
        public int MIN_STOCK_QTY { get; set; }
        public string BATCH_TYPE { get; set; }
        public int STAND_QTY { get; set; }
        public string RESOURCE_STORE { get; set; }
    }
}
