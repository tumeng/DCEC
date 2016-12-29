using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
#region 北自所Rmes命名空间引用
using Rmes.DA.Base;
using Rmes.DA.Entity;
using Rmes.DA.Dal;
using Rmes.DA.Factory;
#endregion


#region 自动生成实体类工具生成，数据库："ORCL
//From XYJ
//时间：2013-12-8
//
#endregion

namespace Rmes.DA.Entity
{
    [PetaPoco.TableName("DATA_SN_COMPLETE_INSTORE")]
    //下面这行请自行填写关键字段并取消注释
    //[PetaPoco.PrimaryKey("RMES_ID", sequenceName = "SEQ_RMES_ID")]
    public class SNCompleteInstoreEntity
    {
        public string PLAN_CODE { get; set; }
        public string WORKSHOP_CODE { get; set; }
        public string PRODUCT_CODE { get; set; }
        public string SN { get; set; }
        public string INSTORE_QTY { get; set; }
        public string STORE_CODE { get; set; }
        public string INSTORE_TYPE_CODE { get; set; }
        public string PLAN_BATCH { get; set; }
        public string HANDLE_FLAG { get; set; }
        public string ORDER_CODE { get; set; }
    }
}
