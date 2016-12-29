using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PetaPoco;

#region 北自所Rmes命名空间引用
using Rmes.DA.Base;
using Rmes.DA.Entity;
using Rmes.DA.Dal;
using Rmes.DA.Factory;
#endregion
namespace Rmes.DA.Entity
{
    [PetaPoco.TableName("DATA_PLAN_BATCHQTY")]
    //下面这行请自行填写关键字段并取消注释
    [PetaPoco.PrimaryKey("RMES_ID", sequenceName = "SEQ_RMES_ID")]
    public class PlanBatchQTYEntity
    {
        public string RMES_ID { get; set; }
        public string COMPANY_CODE { get; set; }
        public string PLINE_CODE { get; set; }
        public string STATION_CODE { get; set; }
        public string PLAN_CODE { get; set; }
        public int BATCH_QTY { get; set; }
        public DateTime WORK_DATE { get; set; }
    }

  //  rmes_id      VARCHAR2(30) not null,
  //company_code VARCHAR2(20) not null,
  //pline_code   VARCHAR2(20) not null,
  //plan_code    VARCHAR2(30) not null,
  //station_code VARCHAR2(30),
  //batch_qty    NUMBER,
  //work_date    DATE not null
}
