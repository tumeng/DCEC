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
    [PetaPoco.TableName("DATA_PLAN_PROCESS")]
    //下面这行请自行填写关键字段并取消注释
    [PetaPoco.PrimaryKey("RMES_ID", sequenceName = "SEQ_RMES_ID")]
    public class PlanProcessEntity
    {
        public string RMES_ID { get; set; }
        public string PLAN_CODE { get; set; }
        public string WORKUNIT_CODE { get; set; }
        public string PROCESS_CODE { get; set; }
        public string PROCESS_NAME { get; set; }
        public int PROCESS_SEQ { get; set; }
        public string ROUTING_IMPFLAG { get; set; }
        public string PROCESS_IMPFLAG { get; set; }
        public string FLAG { get; set; }
        public string PLAN_SO { get; set; }
        public string PLAN_QTY { get; set; }
        public string PROCESS_START_DATE { get; set; }
        public string PROCESS_START_TIME { get; set; }
        public string STANDARD_MACHINE_WORKTIME { get; set; }
        public string MACHINE_WORKTIME_UNIT { get; set; }
        public string STANDARD_MAN_WORKTIME { get; set; }
        public string MAN_WORKTIME_UNIT { get; set; }
        public string RESTART_PROCESS_FLAG { get; set; }
        public string PLINE_CODE { get; set; }
        public string PROCESS_CTRL_CODE { get; set; }
        public string ORDER_CODE { get; set; }
        public string WORKSHOP_CODE { get; set; }
        public string COMPLETE_FLAG { get; set; }
    }
}
