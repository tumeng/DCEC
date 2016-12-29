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
//时间：2016/8/8
//
#endregion

namespace Rmes.DA.Entity
{
    [PetaPoco.TableName("ATPUTXKZB")]
    public class AtpuEntity : IEntity
    {
        public string ZDMC { get; set; }
        public string GZDD { get; set; }
    }

}
