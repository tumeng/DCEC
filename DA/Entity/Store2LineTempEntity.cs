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
    [PetaPoco.TableName("DATA_STORE2LINE_TEMP")]
    [PetaPoco.PrimaryKey("RMES_ID", sequenceName = "SEQ_RMES_ID")]
    public class Store2LineTempEntity
    {
        public string RMES_ID { get; set; }
        public string PLAN_CODE { get; set; }
        public string BATCH { get; set; }
        public string CREATE_USER_ID { get; set; }
    }
}
