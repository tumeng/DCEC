using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rmes.DA.Base;
using Rmes.DA.Entity;
using Rmes.DA.Factory;
using Rmes.DA.Dal;


namespace Rmes.DA.Entity
{
    [PetaPoco.TableName("DATA_LINESIDE_STOCK")]
    [PetaPoco.PrimaryKey("RMES_ID", sequenceName = "SEQ_RMES_ID")]
    public class LineSideStockEntity 
    {
        public string RMES_ID { get; set; }
        public string COMPANY_CODE { get; set; }
        public string WORKSHOP_CODE { get; set; }
        public string PLINE_CODE { get; set; }
        public string LOCATION_CODE { get; set; }
        public string ITEM_CODE { get; set; }
        public string ITEM_NAME { get; set; }
        public string VENDOR_CODE { get; set; }
        public string ITEM_BATCH { get; set; }
        public float ITEM_QTY { get; set; }
        public string FACTORY_CODE { get; set; }
        public string STORE_CODE { get; set; }
        public string STORE_NAME { get; set; }
        public DateTime RECENT_IN_DATE { get; set; }

    }

  //  company_code  VARCHAR2(30) not null,
  //pline_code    VARCHAR2(30),
  //location_code VARCHAR2(30),
  //item_code     VARCHAR2(30) not null,
  //item_batch    VARCHAR2(30),
  //item_qty      NUMBER,
  //vendor_code   VARCHAR2(30),
  //rmes_id       VARCHAR2(50) not null,
  //factory_code  VARCHAR2(30),
  //store_code    VARCHAR2(30) not null
}
