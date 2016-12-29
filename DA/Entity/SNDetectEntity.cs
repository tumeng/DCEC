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

namespace Rmes.DA.Entity
{
    [PetaPoco.TableName("DATA_SN_DETECT")]
    //下面这行请自行填写关键字段并取消注释
    //[PetaPoco.PrimaryKey("RMES_ID", sequenceName = "SEQ_RMES_ID")]
    public class SNDetectEntity 
    {
        public string PLAN_CODE { get; set; }
        public string WORKSHOP_CODE { get; set; }
        public string PROCESS_CODE { get; set; }
        public string PROCESS_NAME { get; set; }
        public string DETECT_MAT_CODE { get; set; }
        public string DETECT_ITEM_SEQ { get; set; }
        public string DETECT_ITEM_CODE { get; set; }
        public string DETECT_ITEM_DESC { get; set; }
        public string DETECT_QUAL_VALUE { get; set; }
        public string DETECT_QUAN_VALUE { get; set; }
        public string MAX_VALUE { get; set; }
        public string MIN_VALUE { get; set; }
        public string QUAN_VALUE_REQ { get; set; }
        public string DETECT_QTY { get; set; }
        public string REMARK { get; set; }
        public string RMES_ID { get; set; }
        public string ORDER_CODE { get; set; }
        public string SN { get; set; }
        public string QUALITY_FLAG { get; set; }
        public string USER_ID { get; set; }
        public string STATION_CODE { get; set; }
        public string COMPANY_CODE { get; set; }
        public string PLINE_CODE { get; set; }
        public string DETECT_DESC { get; set; }
        public string HANDLE_FLAG { get; set; }
    }

    [PetaPoco.TableName("DATA_SN_DETECT_DATA")]
    //下面这行请自行填写关键字段并取消注释
    //[PetaPoco.PrimaryKey("RMES_ID", sequenceName = "SEQ_RMES_ID")]
    public class SNDetectdDataEntity
    {
        public string RMES_ID { get; set; }
        public string SN { get; set; }
        public string COMPANY_CODE { get; set; }
        public string PLINE_CODE { get; set; }
        public string PLAN_CODE { get; set; }
        public string LOCATION_CODE { get; set; }
        public string STATION_CODE { get; set; }
        public string DETECT_DATA_CODE { get; set; }
        public string DETECT_DATA_VALUE { get; set; }
        public string USER_ID { get; set; }
        public DateTime WORK_TIME { get; set; }
        public string DETECT_DATA_NAME { get; set; }

        

    }
}
