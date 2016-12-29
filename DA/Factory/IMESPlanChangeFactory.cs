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

namespace Rmes.DA.Factory
{
    public static class IMESPlanChangeFactory
    {
        public static void Insert(IMESPlanChangeEntity changes)
        {
            IMESPlanChangeDal dal= new IMESPlanChangeDal();
            List<SAPMessageTransEntity> temp = DB.GetInstance().Fetch<SAPMessageTransEntity>("where MESSAGE_CODE = '0048' and HANDLE_FLAG!='0'");
            if (temp != null || temp.Count > 0) return;
            dal.Insert(changes);
            SAPMessageTransEntity msgEntity = new SAPMessageTransEntity
            {
                MESSAGE_CODE = "0048",
                WORK_DATE = DateTime.Now,
                HANDLE_FLAG = "0",
            };
            DB.GetInstance().Insert(msgEntity);
        }
    }
}
