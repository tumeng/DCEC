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
    internal class PlanSubpackDal : BaseDalClass
    {
        /// <summary>
        /// 函数说明：返回List
        /// </summary>
        /// <returns>List<PlanSubpackEntity></returns>
        public List<PlanSubpackEntity> GetAll()
        {
            return db.Fetch<PlanSubpackEntity>("");
        }

        /// <summary>
        /// 函数说明：返回Entity
        /// </summary>
        /// <param name="RMES_ID"></param>
        /// <returns>PlanSubpackEntity</returns>
        public PlanSubpackEntity GetByKey(string RmesID)
        {
            return db.First<PlanSubpackEntity>("WHERE RMES_ID=@0", RmesID);
        }

        /// <summary>
        /// 函数说明：返回List
        /// </summary>
        /// <param name="RMES_ID"></param>
        /// <returns>List<PlanSubpackEntity></returns>
        public List<PlanSubpackEntity> GetSingleByKey(string RmesID)
        {
            return db.Fetch<PlanSubpackEntity>("WHERE RMES_ID=@0", RmesID);
        }

        public List<PlanSubpackEntity> GetSubpackPlanByStation(string CompanyCode, string PLineCode, string StationCode)
        {
            string SQL = "WHERE COMPANY_CODE=@0 and pline_code=@1  and work_flag='N' and station_code=@2";
            try
            {
                List<PlanSubpackEntity> ListPlan = db.Fetch<PlanSubpackEntity>(SQL, CompanyCode, PLineCode, StationCode);
                if (ListPlan.Count > 0)
                    return ListPlan;
                else
                    return null;
            }
            catch
            {
                return null;
            }
        }

    }
}
#endregion