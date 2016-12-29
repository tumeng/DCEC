using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rmes.DA.Base;
using Rmes.DA.Entity;
using Rmes.DA.Dal;
using Rmes.DA.Factory;

namespace Rmes.DA.Entity
{
    [PetaPoco.TableName("CODE_FAULT")]
    [PetaPoco.PrimaryKey("RMES_ID", sequenceName = "SEQ_RMES_ID")]
    public  class FaultEntity:IEntity
    {
        public string RMES_ID { get; set; }
        public string COMPANY_CODE { get; set; }
        public string FAULT_CODE { get; set; }
        public string FAULT_NAME { get; set; }
        public string FAULT_DESC { get; set; }
        public string FAULT_CLASS { get; set; }
        public string FAULT_TYPE { get; set; }
        public DateTime INPUT_TIME { get; set; }
        public string INPUT_PERSON { get; set; }
        
    }
}
