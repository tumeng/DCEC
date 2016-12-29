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
    [PetaPoco.TableName("DATA_BARCODE")]
    //下面这行请自行填写关键字段并取消注释
    [PetaPoco.PrimaryKey("RMES_ID", sequenceName = "SEQ_RMES_ID")]
    public class BarCodeEntity
    {
        public string RMES_ID { get; set; }
        public string BAR_CODE { get; set; }
        public string SERIES_CODE { get; set; }
        public string SEQ_1 { get; set; }
        public string SEQ_2 { get; set; }
        public string SEQ_3 { get; set; }
        public string SEQ_4 { get; set; }
        public string SEQ_5 { get; set; }
        public string SEQ_6 { get; set; }
        public string SEQ_7 { get; set; }
        public string SEQ_8 { get; set; }
        public string SEQ_9 { get; set; }
        public string SEQ_10 { get; set; }
        public string SEQ_11 { get; set; }
        public string SEQ_12 { get; set; }
        public string SEQ_13 { get; set; }
        public string SEQ_14 { get; set; }
        public string SEQ_15 { get; set; }
        public string SEQ_16 { get; set; }
        public string SEQ_17 { get; set; }
        public string SEQ_18 { get; set; }
        public string SEQ_19 { get; set; }
        public string SEQ_20 { get; set; }
        public string SEQ_21 { get; set; }
        public string SEQ_22 { get; set; }
        public string TEMP1 { get; set; }
        public string TEMP2 { get; set; }
        public string TEMP3 { get; set; }
    }
}
