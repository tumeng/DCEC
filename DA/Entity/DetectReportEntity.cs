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


#region 自动生成实体类工具生成，by 北自所自控中心信息部

#endregion


#region 自动生成实体类
namespace Rmes.DA.Entity
{
    [PetaPoco.TableName("DATA_DETECT_REPORT")]
    //下面这行请自行填写关键字段并取消注释
    [PetaPoco.PrimaryKey("RMES_ID", sequenceName = "SEQ_RMES_ID")]
    public class DetectReportEntity : IEntity
    {
        public string RMES_ID { get; set; }

        public string COMPANY_CODE { get; set; }
        public string PLINE_CODE { get; set; }
        public string ORDER_CODE { get; set; }
        public string SN { get; set; }
        public int PRINT_TIMES { get; set; }
        public DateTime CREATE_DATE { get; set; }
        public DateTime PRINT_DATE { get; set; }
        public string FILE_NAME { get; set; }
    }
}
#endregion
