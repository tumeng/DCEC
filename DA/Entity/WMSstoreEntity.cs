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
    [PetaPoco.TableName("VW_DATA_STORE_WMS")]
    public class WMSstoreEntity : IEntity
    {
        public string M_ID { get; set; }
        public string MUNIT { get; set; }
        public string MATNAME { get; set; }
        public string MSPECI { get; set; }
        public float NUM { get; set; }
       
    }

}
