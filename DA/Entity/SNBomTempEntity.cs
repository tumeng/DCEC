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
    [PetaPoco.TableName("DATA_SN_BOM_TEMP")]
    [PetaPoco.PrimaryKey("RMES_ID", sequenceName = "SEQ_RMES_ID")]
    public class SNBomTempEntity : IEntity
    {

        public string RMES_ID { get; set; }
        public string SN { get; set; }
        public string COMPANY_CODE { get; set; }
        public string PLAN_CODE { get; set; }
        public string PLINE_CODE { get; set; }
        public string LOCATION_CODE { get; set; }
        public string PROCESS_CODE { get; set; }
        public string ITEM_CODE { get; set; }
        public string ITEM_NAME { get; set; }
        public string ITEM_BATCH { get; set; }
        public float ITEM_QTY { get; set; }
        public string ITEM_CLASS { get; set; }
        public string ITEM_TYPE { get; set; }
        public string VENDOR_CODE { get; set; }
        public string CONFIRM_FLAG { get; set; }
        public int COMPLETE_QTY { get; set; }
        public DateTime CREATE_TIME { get; set; }
        public string REPLACE_FLAG { get; set; }
        public string STATION_CODE { get; set; }
        public string STATION_NAME { get; set; }
        public string BARCODE { get; set; }
        public string CREATE_USERID { get; set; }
    }
    [PetaPoco.TableName("RSTBOMQD")]
    public class RstBomQdEntity : IEntity
    {
        public string GWMC { get; set; }
        public string COMP { get; set; }
        public string UDESC { get; set; }
        public string QTY { get; set; }
        public string GXMC { get; set; }
        public string ZDMC { get; set; }
        public string ZDDM { get; set; }
        public string GYSMC { get; set; }
    }

}
