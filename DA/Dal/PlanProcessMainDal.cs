using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PetaPoco;
using Rmes.DA.Entity;


namespace Rmes.DA.Dal
{
    internal class PlanProcessMainDal : Base.BaseDalClass
    {
        public List<PlanProcessMainEntity> GetAll()
        {
            return db.Fetch<PlanProcessMainEntity>("");
        }

        public List<PlanProcessMainEntity> GetUnHandledByOrderCode(string orderCode)
        {
            return db.Fetch<PlanProcessMainEntity>("where ORDER_CODE=@0 and HANDLED_FLAG!='F'",orderCode);
        }

        public List<PlanProcessMainEntity> GetByOrderCode(string orderCode)
        {
            return db.Fetch<PlanProcessMainEntity>("where ORDER_CODE=@0", orderCode);
        }

        public PlanProcessMainEntity GetByID(string rmesID)
        {
            try
            {
                return db.First<PlanProcessMainEntity>("where RMES_ID=@0", rmesID);
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public PlanProcessMainEntity GetByOrderCodeAndProcessCode(string orderCode,string processCode)
        {
            try
            {
                return db.First<PlanProcessMainEntity>("where ORDER_CODE=@0 and PROCESS_CODE=@1",orderCode,processCode);
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public List<PlanProcessMainEntity> GetByIDs(string[] id)
        {
            try
            {
                return db.Fetch<PlanProcessMainEntity>("where RMES_ID in (@r)", new { r = id });
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}
