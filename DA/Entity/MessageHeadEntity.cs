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
//From XYJ
//时间：2013/12/4
//
#endregion


#region 自动生成实体类
namespace Rmes.DA.Entity
{
    [PetaPoco.TableName("CODE_CONFIG_COMMAND")]
    //下面这行请自行填写关键字段并取消注释
    [PetaPoco.PrimaryKey("RMES_ID", sequenceName = "SEQ_RMES_ID")]
    public class MessageHeadEntity : IEntity
    {
        public string RMES_ID { get; set; }

        [PetaPoco.Column("COMMAND_CODE")]
        public string HEAD_CODE { get; set; }

        [PetaPoco.Column("COMMAND_REGEX")]
        public string REGEXSTRING { get; set; }

        public string COMMAND_BODY { get; set; }
        public string REMARK { get; set; }

    }
}
#endregion
