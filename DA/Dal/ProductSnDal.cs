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
    internal class ProductSnDal : BaseDalClass
    {
        /// <summary>
        /// 函数说明：返回List
        /// </summary>
        /// <returns>List<ProductSnEntity></returns>
        public List<ProductSnEntity> GetAll()
        {
            return db.Fetch<ProductSnEntity>("");
        }

        /// <summary>
        /// 函数说明：返回Entity
        /// </summary>
        /// <param name="RMES_ID"></param>
        /// <returns>ProductSnEntity</returns>
        public ProductSnEntity GetByKey(string RmesID)
        {
            return db.First<ProductSnEntity>("WHERE RMES_ID=@0", RmesID);
        }

        /// <summary>
        /// 函数说明：返回List
        /// </summary>
        /// <param name="RMES_ID"></param>
        /// <returns>List<ProductSnEntity></returns>
        public List<ProductSnEntity> GetSingleByKey(string RmesID)
        {
            return db.Fetch<ProductSnEntity>("WHERE RMES_ID=@0", RmesID);
        }

        public List<ProductSnEntity> GetCurrentSN(string CompanyCode, string PlineCode, string StationCode)
        {
            string SQL = "where company_code=@0 and pline_code=@1 and station_code=@2 and complete_flag ='A'";
            return db.Fetch<ProductSnEntity>(SQL, CompanyCode, PlineCode, StationCode);
        }


        public void HandleStationComplete(string CompanyCode, string PlineCode, string StationCode, string PlanCode, string SN, string QualityStatus)
        {
            //string SQL = "update data_sn_station_complete set complete_flag=@0 where company_code=@1 and pline_code=@2 and station_code=@3" +
            //            "and plan_code=@4 and sn=@5";
            //db.Execute(SQL, CompanyCode, PlineCode, StationCode, PlanCode, SN, CompleteFlag);
            db.Execute("call PL_HANDLE_STATION_COMPLETE(@0,@1,@2,@3,@4,@5)", CompanyCode, PlineCode, StationCode, PlanCode, SN, QualityStatus);
        }

        public void HandleStationPause(string CompanyCode, string PlineCode, string StationCode, string PlanCode, string SN, string QualityStatus)
        {
            //string SQL = "update data_sn_station_complete set complete_flag=@0 where company_code=@1 and pline_code=@2 and station_code=@3" +
            //            "and plan_code=@4 and sn=@5";
            //db.Execute(SQL, CompanyCode, PlineCode, StationCode, PlanCode, SN, CompleteFlag);
            db.Execute("call PL_HANDLE_STATION_PAUSE(@0,@1,@2,@3,@4,@5)", CompanyCode, PlineCode, StationCode, PlanCode, SN, QualityStatus);
        }

    }
}
#endregion