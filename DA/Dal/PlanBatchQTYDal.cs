using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PetaPoco;

#region 北自所Rmes命名空间引用
using Rmes.DA.Base;
using Rmes.DA.Entity;
using Rmes.DA.Dal;
using Rmes.DA.Factory;
#endregion

namespace Rmes.DA.Dal
{
    public class PlanBatchQTYDal:BaseDalClass
    {
        public PlanBatchQTYEntity GetByStation(string StationCode)
        {
            List<PlanBatchQTYEntity> ent = db.Fetch<PlanBatchQTYEntity>("where station_code=@0", StationCode);
            if (ent.Count == 0) return null;
            return ent.First<PlanBatchQTYEntity>();
        }

        public void UpdatePlanBatchQty(string PlineCode, string StationCode, string PlanCode,string VItemCode,int BatchQty)
        {
            db.Execute("call PL_HANDLE_PLAN_BATCHQTY(@0,@1,@2,@3,@4)", PlineCode, StationCode,PlanCode,VItemCode,BatchQty);
        }

    }
}
