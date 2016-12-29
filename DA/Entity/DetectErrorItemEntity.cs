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


#region 自动生成实体类工具生成，数据库："ORCL
//From XYJ
//时间：2013-12-8
//
#endregion

namespace Rmes.DA.Entity
{
    [PetaPoco.TableName("CODE_DETECT_ERROR_ITEM")]
    //下面这行请自行填写关键字段并取消注释
    [PetaPoco.PrimaryKey("RMES_ID", sequenceName = "SEQ_RMES_ID")]
    public class DetectErrorItemEntity
    {
        public string RMES_ID { get; set; }
        public string COMPANY_CODE { get; set; }
        public string DETECT_ITEM_CODE { get; set; }
        public string DETECT_ITEM_NAME { get; set; }
        public string ERROR_ITEM_CODE { get; set; }
        public string ERROR_ITEM_NAME { get; set; }
        public string PLINE_CODE { get; set; }
        public string WORKUNIT_CODE { get; set; }
    }
}
