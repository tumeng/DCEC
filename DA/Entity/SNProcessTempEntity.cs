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
    [PetaPoco.TableName("DATA_SN_PROCESS_TEMP")]
    [PetaPoco.PrimaryKey("RMES_ID", sequenceName = "SEQ_RMES_ID")]
    public class SNProcessTempEntity : IEntity
    {
        public string RMES_ID { get; set; }
        public string COMPANY_CODE { get; set; }

        public string SN { get; set; }
        public string PLAN_CODE { get; set; }
        public string PLINE_CODE { get; set; }
        public string LOCATION_CODE { get; set; }
        public string PROCESS_CODE { get; set; }
        public string QUALITY_STATUS { get; set; }
        public string USER_ID { get; set; }
        public DateTime WORK_DATE { get; set; }
        public DateTime START_TIME { get; set; }
        public DateTime COMPLETE_TIME { get; set; }
        public string PROCESS_NAME { get; set; }
        public string COMPLETE_FLAG { get; set; }
        public string ROUTING_CODE { get; set; }
        public string ROUTING_NAME { get; set; }
        public string PARENT_ROUTING_CODE { get; set; }
        public int PROCESS_SEQ { get; set; }

    }
}
