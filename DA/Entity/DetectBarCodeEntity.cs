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
    [PetaPoco.TableName("CODE_DETECT_BARCODE")]
    //下面这行请自行填写关键字段并取消注释
    [PetaPoco.PrimaryKey("RMES_ID", sequenceName = "SEQ_RMES_ID")]
    public class DetectBarCodeEntity
    {
        public string RMES_ID { get; set; }
        //public string COMPANY_CODE { get; set; }

        public string SEQ_VALUE { get; set; }
        public string SEQ_NAME { get; set; }
        public string SEQ_FATHER { get; set; }
        public string SEQ_LEVEL { get; set; }
        public string PLINE_CODE { get; set; }
        public string PRODUCT_SERIES { get; set; }
    }
}
