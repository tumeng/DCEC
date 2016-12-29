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
//时间：2013-12-08
//
#endregion

namespace Rmes.DA.Dal
{
    internal class SAPBOMDal:BaseDalClass
    {
        public List<SAPBOMEntity> GetByOrderCode(string orderCode)
        {
            return db.Fetch<SAPBOMEntity>("where AUFNR=@0 order by vornr",orderCode);
        }
    }
}
