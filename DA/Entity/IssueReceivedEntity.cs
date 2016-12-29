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


#region 自动生成实体类工具生成，数据库："xkmes
//From XYJ
//时间：2014-01-11
//
#endregion

#region 自动生成实体类
namespace Rmes.DA.Entity
{
    [PetaPoco.TableName("DATA_ISSUE_RECEIVED")]
    //下面这行请自行填写关键字段并取消注释
    [PetaPoco.PrimaryKey("RMES_ID", sequenceName = "SEQ_RMES_ID")]
    public class IssueReceivedEntity : IEntity
    {
        public string RMES_ID { get; set; }
        public string COMPANY_CODE { get; set; }
        public string PLAN_CODE { get; set; }
        public string WORKSHOP_CODE { get; set; }
        public string PLINE_CODE { get; set; }
        public string LOCATION_CODE { get; set; }
        public string ITEM_CODE { get; set; }
        public string ITEM_NAME { get; set; }
        public string ITEM_BATCH { get; set; }
        public string VENDOR_CODE { get; set; }
        public float ITEM_QTY { get; set; }
        public DateTime WORK_TIME { get; set; }
        public string USER_ID { get; set; }
        public string ISSUE_CODE { get; set; }
        public string SEND_CODE { get; set; }
        public string LINESIDE_STOCK_CODE { get; set; }
        public string STORE_ID { get; set; }

    }

        //字段名称				字段类型		长度		关键字	是否为空		中文注释
//        RMES_ID       VARCHAR2(50),
//        PLAN_CODE     VARCHAR2(50),
//        COMPANY_CODE  VARCHAR2(30),
//        WORKSHOP_CODE VARCHAR2(50),
//        PLINE_CODE    VARCHAR2(30),
//        LOCATION_CODE VARCHAR2(30),
//        ITEM_CODE     VARCHAR2(30),
//        ITEM_NAME     VARCHAR2(50),
//        ITEM_BATCH    VARCHAR2(50),
//        ITEM_QTY      NUMBER default 0,
//        VENDOR_CODE   VARCHAR2(30),
//        USER_ID       VARCHAR2(30),
//        WORK_TIME     DATE,
//        ISSUE_CODE    VARCHAR2(30),
//        SEND_CODE     VARCHAR2(50),
//        USER_ID_ISSUE VARCHAR2(30),
//        USER_ID_STORE VARCHAR2(30)
//}
}
#endregion
