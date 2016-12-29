﻿using System;
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
    [PetaPoco.TableName("DATA_SN_BOM_ITEMSN")]
    //下面这行请自行填写关键字段并取消注释
    [PetaPoco.PrimaryKey("RMES_ID", sequenceName = "SEQ_RMES_ID")]
    public class SNBomItemSnEntity : IEntity
    {
        public string RMES_ID { get; set; }
        public string ITEM_SN { get; set; }
        public DateTime WORK_TIME { get; set; }
        public string BOM_RMES_ID { get;set;}
        public string ITEM_VENDOR { get; set; }
    }

    [PetaPoco.TableName("DATA_SN_BOM_ITEMBATCH")]
    //下面这行请自行填写关键字段并取消注释
    [PetaPoco.PrimaryKey("RMES_ID", sequenceName = "SEQ_RMES_ID")]
    public class SNBomItemBatchEntity : IEntity
    {
        public string RMES_ID { get; set; }
        public string ITEM_SN { get; set; }
        public DateTime WORK_TIME { get; set; }
        public string BOM_RMES_ID { get; set; }
        public string ITEM_VENDOR { get; set; }
        public string BARCODE { get; set; }
    }

}
#endregion
