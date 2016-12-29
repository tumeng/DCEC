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


#region 自动生成实体类工具生成，by 北自所自控中心信息部
//From XYJ
//时间：2013/12/4
//
#endregion


#region 自动生成实体类
namespace Rmes.DA.Entity
{
    [PetaPoco.TableName("CODE_ITEM")]
    //下面这行请自行填写关键字段并取消注释
    [PetaPoco.PrimaryKey("ITEM_CODE", sequenceName = "SEQ_RMES_ID")]
   
        public class ItemEntity : IEntity
    {
        public string RMES_ID { get; set; }
        public string COMPANY_CODE { get; set; }
        public string ITEM_CODE { get; set; }
        public string ITEM_NAME { get; set; }
        public string ITEM_NAME_EN { get; set; }
        public string ITEM_TYPE { get; set; }
        public string ITEM_CLASS_CODE { get; set; }
        public string MIN_PACK_QTY { get; set; }
        public string MANAGE_FLAG { get; set; }
        public string TEMP01 { get; set; }
        public string ENABLED_FLAG { get; set; }
        public string UNIT_CODE { get; set; }
        public string ITEM_SPEC { get; set; }
        public string ITEM_MODEL { get; set; }
        public string GENERAL_FLAG { get; set; }
        public string ABC_FLAG { get; set; }
        public string WORKSHOP_CODE { get; set; }
        public string STORE_ID { get; set; }


    }
}
#endregion
