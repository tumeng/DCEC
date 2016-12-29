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



#region 自动生成实体类
namespace Rmes.DA.Entity
{
    [PetaPoco.TableName("REL_TEAM_USER")]
    //下面这行请自行填写关键字段并取消注释
    //[PetaPoco.PrimaryKey("RMES_ID", sequenceName = "SEQ_RMES_ID")]
    public class TeamUser_PlineEntity : IEntity
    {
        public string COMPANY_CODE { get; set; }
        public string PLINE_CODE { get; set; }
        public string TEAM_CODE { get; set; }
        public string USER_ID { get; set; }
        public string IS_LEADER { get; set; }
        public string TEMP01 { get; set; }
    }
}
#endregion
