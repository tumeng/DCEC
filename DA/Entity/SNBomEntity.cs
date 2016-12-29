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
    [PetaPoco.TableName("DATA_SN_BOM")]
    [PetaPoco.PrimaryKey("RMES_ID")]
    public class SNBomEntity : IEntity
    {
        public string SN { get; set; }
        public string COMPANY_CODE { get; set; }
        public string PLAN_CODE { get; set; }
        public string PLINE_CODE { get; set; }
        public string LOCATION_CODE { get; set; }
        public string PROCESS_CODE { get; set; }
        public string ITEM_CODE { get; set; }
        public string ITEM_NAME { get; set; }
        public string ITEM_BATCH { get; set; }
        public int ITEM_QTY { get; set; }
        public string VENDOR_CODE { get; set; }
        public string USER_ID { get; set; }
        public DateTime WORK_TIME { get; set; }
        public int COMPLETE_QTY { get; set; }
        public string ITEM_CLASS_CODE { get; set; }
        public string RMES_ID { get; set; }
        public string CONFIRM_FLAG { get; set; }
        public string HANDLE_FLAG { get; set; }
        public string WORKSHOP_CODE { get; set; }
        public string ORDER_CODE { get; set; }

        //字段名称				字段类型		长度		关键字	是否为空		中文注释
        //SN					VARCHAR2	30		False	False		
        //COMPANY_CODE			VARCHAR2	20		False	False		
        //PLAN_CODE				VARCHAR2	30		False	True		
        //PLINE_CODE			VARCHAR2	30		False	False		
        //LOCATION_CODE			VARCHAR2	30		False	False		
        //PROCESS_CODE			VARCHAR2	30		False	False		
        //ITEM_CODE				VARCHAR2	30		False	False		
        //ITEM_NAME				VARCHAR2	30		False	True		
        //ITEM_BATCH			VARCHAR2	10		False	True		
        //ITEM_QTY				NUMBER		22		False	False		
        //VENDOR_CODE			VARCHAR2	30		False	True		
        //USER_ID				VARCHAR2	30		False	True		
        //WORK_TIME				DATE		7		False	True		
        //COMPLETE_QTY			NUMBER		22		False	True		
        //ITEM_CLASS_CODE		VARCHAR2	10		False	True		重要零件标识
        //RMES_ID				VARCHAR2	30		False	True		
        //CONFIRM_FLAG			VARCHAR2	30		False	True		

    }
}
