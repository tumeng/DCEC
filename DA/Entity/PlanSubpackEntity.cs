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
    [PetaPoco.TableName("DATA_SUBPACK")]
    [PetaPoco.PrimaryKey("RMES_ID", sequenceName = "SEQ_RMES_ID")]
    public class PlanSubpackEntity : IEntity
    {
        public string RMES_ID { get; set; }
        public string COMPANY_CODE { get; set; }
        public string PLINE_CODE { get; set; }
        public string STATION_CODE { get; set; }
        public string PLAN_CODE { get; set; }
        public string SN { get; set; }
        public string WORK_FLAG { get; set; }
        public DateTime WORK_TIME { get; set; }
        public string USER_ID { get; set; }


    }
}
