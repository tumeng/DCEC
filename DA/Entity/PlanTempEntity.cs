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

namespace Rmes.DA.Entity
{
    [PetaPoco.TableName("DATA_PLAN_TEMP")]
    //下面这行请自行填写关键字段并取消注释
    [PetaPoco.PrimaryKey("RMES_ID", sequenceName = "SEQ_RMES_ID")]
    public class PlanTempEntity
    {
        public string RMES_ID { get; set; }
        public string COMPANY_CODE { get; set; }
        public string PLINE_CODE { get; set; }
        public int PLAN_SEQUENCE { get; set; }
        public string PLAN_CODE { get; set; }
        public string PLAN_SO { get; set; }
        public string PLAN_TYPE_CODE { get; set; }
        public string PLAN_BATCH { get; set; }
        public DateTime CREATE_TIME { get; set; }
        public DateTime BEGIN_DATE { get; set; }
        public DateTime END_DATE { get; set; }
        public DateTime BEGIN_TIME { get; set; }
        public DateTime END_TIME { get; set; }
        public int PLAN_QTY { get; set; }
        public string SN_FLAG { get; set; }
        public string PROJECT_CODE { get; set; }
        public string CREATE_USER_ID { get; set; }
        public string HANDLE_FLAG { get; set; }
        public int HANDLED_QTY { get; set; }
        public string INSTORE_CODE { get; set; }
        public string WORKSHOP_CODE { get; set; }
        public string SERIES_CODE { get; set; }
        public string DETECT_BARCODE { get; set; }
        public string BARCODE_FLAG { get; set; }
        public string WBS_CODE { get; set; }
    }
}
