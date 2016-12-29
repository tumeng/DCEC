using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PetaPoco;
using Rmes.DA.Entity;
#region 北自所Rmes命名空间引用
using Rmes.DA.Base;
using Rmes.DA.Entity;
using Rmes.DA.Dal;
using Rmes.DA.Factory;
#endregion

namespace Rmes.DA.Dal
{
    internal class PlanProcessDal : Base.BaseDalClass
    {
        public List<PlanProcessEntity> GetAll()
        {
            return db.Fetch<PlanProcessEntity>("order by PROCESS_CODE desc");
        }



        public List<PlanProcessEntity> GetByOrderCode(string orderCode)
        {
            return db.Fetch<PlanProcessEntity>("where ORDER_CODE=@0", orderCode);
        }

        public List<PlanProcessEntity> GetByPlan(string PlanCode)
        {
            string order_code = PlanFactory.GetOrderCodeByPlan(PlanCode);
            return db.Fetch<PlanProcessEntity>("WHERE ORDER_CODE=@0 order by process_code", order_code);

        }

        public List<PlanProcessEntity> GetByCreatePeriod(DateTime b, DateTime e)
        {
            string _b = b.ToShortDateString() + " 00:00:00";
            string _e = e.ToShortDateString() + " 23:59:59";
            return db.Fetch<PlanProcessEntity>("where PLAN_CODE in (select PLAN_CODE from DATA_PLAN where create_time between to_date(@0,'yyyy-mm-dd hh24:mi:ss') and to_date(@1,'yyyy-mm-dd hh24:mi:ss')) order by PLAN_CODE,PROCESS_SEQ", _b,_e);
        }
    }
}
