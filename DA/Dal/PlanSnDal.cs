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

#region
namespace Rmes.DA.Dal
{
    internal class PlanSnDal : BaseDalClass
    {
        /// <summary>
        /// 函数说明：返回List
        /// </summary>
        /// <returns>List<PlanSnEntity></returns>
        public List<PlanSnEntity> GetAll()
        {
            return db.Fetch<PlanSnEntity>("");
        }

        /// <summary>
        /// 函数说明：返回Entity
        /// </summary>
        /// <param name="RMES_ID"></param>
        /// <returns>PlanSnEntity</returns>
        public PlanSnEntity GetByKey(string RmesID)
        {
            return db.First<PlanSnEntity>("WHERE RMES_ID=@0", RmesID);
        }
        public PlanSnEntity GetBySn(string sn)
        {
            try
            {
                return db.First<PlanSnEntity>("where SN=@0 and is_valid='Y' ", sn);
            }
            catch
            {
                return null;
            }
        }
        public PlanSnEntity GetBySnPline(string sn, string pcode)
        {
            try
            {
                return db.First<PlanSnEntity>("where SN=@0 and pline_code=@1 and is_valid='Y' ", sn, pcode);
            }
            catch
            {
                return null;
            }
        }
        /// <summary>
        /// 函数说明：返回List
        /// </summary>
        /// <param name="RMES_ID"></param>
        /// <returns>List<PlanSnEntity></returns>
        public List<PlanSnEntity> GetSingleByKey(string RmesID)
        {
            return db.Fetch<PlanSnEntity>("WHERE RMES_ID=@0", RmesID);
        }

        public List<PlanSnEntity> GetByPlanCode(string planCode)
        {
            PlanEntity ent = PlanFactory.GetByKey(planCode);
            if (ent != null)
            {
                //string order_code = ent.ORDER_CODE;
                string SQL = "WHERE is_valid='Y'  and COMPANY_CODE=@0  and plan_code=@1 ORDER BY SN_FLAG desc,SN";
                return db.Fetch<PlanSnEntity>(SQL,LoginInfo.CompanyInfo.COMPANY_CODE,planCode);
            }
            return null;
        }
        public List<PlanSnEntity> GetByPlanCodedesc(string planCode)
        {
            PlanEntity ent = PlanFactory.GetByKey(planCode);
            if (ent != null)
            {
                //string order_code = ent.ORDER_CODE;
                string SQL = "WHERE is_valid='Y'  and COMPANY_CODE=@0  and plan_code=@1 ORDER BY SN_FLAG desc,SN desc";
                return db.Fetch<PlanSnEntity>(SQL, LoginInfo.CompanyInfo.COMPANY_CODE, planCode);
            }
            return null;
        }
        public List<PlanSnEntity> GetByOrderCode(string orderCode)
        {
            PlanEntity ent = PlanFactory.GetByKey(orderCode);
            if (ent != null)
            {
                string SQL = "WHERE ORDER_CODE=@0 AND COMPANY_CODE=@1 ORDER BY SN";
                return db.Fetch<PlanSnEntity>(SQL, orderCode, LoginInfo.CompanyInfo.COMPANY_CODE);
            }
            return null;
        }

        public List<PlanSnEntity> WebGetByOrderCode(string orderCode)
        {
            return db.Fetch<PlanSnEntity>("where order_code=@0", orderCode);
        }
        
        public void InitStationControl(string CompanyCode, string PlineCode, string StationCode, string PlanCode, string Sn,string controlname)
        {
            if (CsGlobalClass.DGSM)
            { }
            else
            {
                db.Execute("call PL_INIT_STATION_CONTROL(@0,@1,@2,@3,@4,@5)", CompanyCode, PlineCode, StationCode, PlanCode, Sn, controlname);
            }
        }
        public void InitPlanSnStationData(string CompanyCode, string PlineCode, string StationCode, string PlanCode, string Sn,string UserID)
        {
            db.Execute("call PL_INIT_STATION_COMPLETE(@0,@1,@2,@3,@4,@5)", CompanyCode, PlineCode, StationCode, PlanCode, Sn,UserID);
        }
        
    }
}
#endregion