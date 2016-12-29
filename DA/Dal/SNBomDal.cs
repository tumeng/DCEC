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
    internal class SNBomDal : BaseDalClass
    {
        /// <summary>
        /// 函数说明：返回List
        /// </summary>
        /// <returns>List<ControlsSnEntity></returns>
        public List<SNBomEntity> GetAll()
        {
            return db.Fetch<SNBomEntity>("");
        }

        
        public List<SNBomEntity> GetByPlanStaton(string CompanyCode, string PlanID, string PlineCode, string StationCode)
        {
            //string SQL = "where company_code=@0 and PLAN_CODE=@1  and pline_code=@2 and location_code in(select location_code from rel_station_location where station_code=@3)";
            //return db.Fetch<SNBomEntity>(SQL, CompanyCode, PlanID, PlineCode, StationCode);
            //西开暂时不按工位显示物料，只显示标准物料，因此把上面的工位代码先屏蔽掉，跑通了再说 by liuzhy 2013 X-MAS
            string SQL = "where company_code=@0 and PLAN_CODE=@1  and pline_code=@2 and location_code =@3";
            return db.Fetch<SNBomEntity>(SQL, CompanyCode, PlanID, PlineCode, PlineCode);
        }

        public List<SNBomEntity> GetByPlanCode(string planCode)
        {
            return db.Fetch<SNBomEntity>("where PLAN_CODE=@0 order by item_code",planCode);
        }

    }
}
#endregion