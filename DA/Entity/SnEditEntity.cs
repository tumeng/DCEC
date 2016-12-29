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
    [PetaPoco.TableName("DATA_SN_EDIT")]
    [PetaPoco.PrimaryKey("PLAN_SN")]
    public class SnEditEntity : IEntity
    {
        public string PROJECT_CODE { get; set; }
        public string PROJECT_NAME { get; set; }
        public string PLAN_SO { get; set; }
        public string TEAM_NAME { get; set; }
        public string PLAN_SN { get; set; }
        public string TEMP1 { get; set; }
        public string TEMP2 { get; set; }
        public string TEMP3 { get; set; }
        public string TEMP4 { get; set; }
        public string TEMP5 { get; set; }
        public string PRINT_USER { get; set;}
        public string PRINT_TIME { get; set;}
    }
}
