﻿using System;
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

    [PetaPoco.TableName("IMES_DATA_PLAN_BOM")]
    //下面这行请自行填写关键字段并取消注释
    [PetaPoco.PrimaryKey("ID", sequenceName = "SEQ_RMES_ID")]
    public class IMESPlanBOMEntity
    {
        public string AUFNR { get; set; }
        public string WERKS { get; set; }
        public string MATNR { get; set; }
        public string VORNR { get; set; }
        public string SUBMAT { get; set; }
        public string MENGE { get; set; }
        public string LGORT { get; set; }
        public string CHARG { get; set; }
        public string ID { get; set; }
        public string BATCH { get; set; }
        public string SERIAL { get; set; }
        public string PRIND { get; set; }
        public string DOCNUM { get; set; }
    }
}