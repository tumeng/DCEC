using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PetaPoco;
using Rmes.DA.Base;
using Rmes.DA.Entity;
using Rmes.DA.Factory;
using Rmes.DA.Dal;
namespace Rmes.DA.Factory
{
    public static class SAPPlanFactory
    {
        public static List<SAPPlanEntity> GetAll()
        {
            return new SAPPlanDal().GetAll();
        }

        public static bool Insert(SAPPlanEntity entity)
        {
            return new SAPPlanDal().Insert(entity); 
        }

        public static SAPPlanEntity GetByOrderCode(string orderCode)
        {
            return new SAPPlanDal().GetByOrderCode(orderCode);
        }

        public static List<SAPPlanEntity> GetByProjectCode(string projectCode)
        {
            return new SAPPlanDal().GetByProjectCode(projectCode);
        }

        public static bool CancleOrder(string orderCode)
        {
            BaseDalClass dal = new SAPPlanDal();
            dal.DataBase.BeginTransaction();
            try
            {
                dal.DataBase.Execute("delete from ISAP_DATA_PLAN where AUFNR=@0", orderCode);
                dal.DataBase.Execute("delete from ISAP_DATA_PLAN_SN where AUFNR=@0", orderCode);
                dal.DataBase.Execute("delete from ISAP_DATA_PLAN_BOM where AUFNR=@0", orderCode);
                dal.DataBase.Execute("delete from ISAP_DATA_PLAN_PROCESS where AUFNR=@0", orderCode);
                dal.DataBase.Execute("delete from ISAP_DATA_PLAN_TOOLS where AUFNR=@0", orderCode);
                dal.DataBase.Execute("delete from DATA_PLAN_TEMP where PLAN_CODE=@0", orderCode);
                dal.DataBase.Execute("delete from DATA_PLAN where ORDER_CODE=@0", orderCode);
                dal.DataBase.Execute("delete from DATA_PLAN_BOM where ORDER_CODE=@0", orderCode);
                dal.DataBase.Execute("delete from DATA_PLAN_STANDARD_BOM where ORDER_CODE=@0", orderCode);
                dal.DataBase.Execute("delete from DATA_PLAN_SN where ORDER_CODE=@0", orderCode);
                dal.DataBase.Execute("delete from DATA_PLAN_PROCESS where ORDER_CODE=@0", orderCode);
                dal.DataBase.Execute("delete from DATA_PLAN_PROCESS_MAIN where ORDER_CODE=@0", orderCode);
                dal.DataBase.CompleteTransaction();
                return true;
            }
            catch(Exception e){
                dal.DataBase.AbortTransaction();
                return false;
            }
        }
    }
}
