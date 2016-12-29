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
    internal class SNControlDal : BaseDalClass
    {
        /// <summary>
        /// 函数说明：返回List
        /// </summary>
        /// <returns>List<ControlsSnEntity></returns>
        public List<SNControlEntity> GetAll()
        {
            return db.Fetch<SNControlEntity>("");
        }

        public List<SNControlEntity> GetByPlanStatonSn(string CompanyCode, string PlineCode, string StationCode,string PlanCode,string Sn)
        {
            string SQL = "where company_code=@0   and pline_code=@1 and station_code =@2 and PLAN_CODE=@3 and sn=@4  and complete_flag='A'";
            return db.Fetch<SNControlEntity>(SQL, CompanyCode, PlineCode, StationCode, PlanCode, Sn);
        }
        public List<SNControlEntity> GetByPlanStaton(string CompanyCode, string PlineCode, string StationCode, string PlanCode)
        {
            string SQL = "where company_code=@0   and pline_code=@1 and station_code =@2 and PLAN_CODE=@3 and complete_flag='A'";
            return db.Fetch<SNControlEntity>(SQL, CompanyCode, PlineCode, StationCode, PlanCode);
        }

    }
}
#endregion