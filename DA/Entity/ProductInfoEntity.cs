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


#region 自动生成实体类工具生成，by 北自所自控中心信息部
//From XYJ
//时间：2013/12/4
//
#endregion


#region 自动生成实体类
namespace Rmes.DA.Entity
{
    [PetaPoco.TableName("VW_DATA_PLAN_SN")]
    public class ProductInfoEntity : IEntity
    {
        public string COMPANY_CODE { get; set; }
        public string PLINE_CODE { get; set; }
        public string PLAN_CODE { get; set; }
        public string PRODUCT_MODEL { get; set; }
        public string PRODUCT_SERIES { get; set; }
        public string CUSTOMER_CODE { get; set; }
        public string CUSTOMER_NAME { get; set; }
        public string REMARK { get; set; }
        public string PLAN_SO { get; set; }
        public string SN { get; set; }
        public string PLAN_BATCH { get; set; }
        public string RUN_FLAG { get; set; }
        public string ORDER_CODE { get; set; }
        public string IS_VALID { get; set; }
        public string LQ_FLAG { get; set; }
        public string PLAN_TYPE { get; set; }
        public string ROUNTING_REMARK { get; set; }
        public string UNITNO { get; set; }
       //b.create_time,
       //b.begin_time,
       //b.end_date,
       //b.account_date,
       //b.online_qty,
       //b.offline_qty,
       //b.plan_type,
       //b.sn_flag,
       //b.bom_flag,
       //b.item_flag,
       //b.third_flag,
       //b.product_model,
    }
}
#endregion
