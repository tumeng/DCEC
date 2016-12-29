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
    [PetaPoco.TableName("DATA_SN_CONTROLS_COMPLETE")]
    [PetaPoco.PrimaryKey("RMES_ID")]
    public class SNControlEntity : IEntity
    {
        public string RMES_ID { get; set; }
        public string SN { get; set; }
        public string COMPANY_CODE { get; set; }
        public string PLAN_CODE { get; set; }
        public string PLINE_CODE { get; set; }
        public string STATION_CODE { get; set; }

        public string CONTROL_NAME { get; set; }
        public string CONTROL_SCRIPT { get; set; }
        public string COMPLETE_FLAG { get; set; }
        public DateTime COMPLETE_TIME { get; set; }
        
    }
}
