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
//时间：2013/12/17
//
#endregion


#region 自动生成实体类
namespace Rmes.DA.Entity
{
    [PetaPoco.TableName("VW_LINESIDE_AUTO_ISSUE")]
    //下面这行请自行填写关键字段并取消注释
    
    public class LinesideAutoIssueEntity : IEntity
    {
        public string RMES_ID { get; set; }
        public string COMPANY_CODE { get; set; }
        public string LINESIDE_STORE_CODE { get; set; }
        public string RESOURCE_STORE { get; set; }
        public string ITEM_CODE { get; set; }
        public string ITEM_NAME { get; set; }
        public float ITEM_QTY { get; set; }
        public float MIN_STOCK_QTY { get; set; }
        public float STAND_QTY { get; set; }
        public string UNIT_CODE { get; set; }
        public string BATCH_TYPE { get; set; }
        public DateTime LAST_ISSUE_DATE { get; set; }
        
    }
}
#endregion
