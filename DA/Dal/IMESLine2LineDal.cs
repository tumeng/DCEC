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


namespace Rmes.DA.Dal
{
    internal class IMESLine2LineDal:BaseDalClass
    {
        public List<IMESLine2LineEntity> GetByOrderCode(string orderCode)
        {
            return db.Fetch<IMESLine2LineEntity>("where AUFNR=@0", orderCode);

        }
    }
}
