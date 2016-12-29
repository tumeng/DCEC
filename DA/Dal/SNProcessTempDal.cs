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
    internal class SNProcessTempDal : BaseDalClass
    {
        /// <summary>
        /// 函数说明：返回List
        /// </summary>
        /// <returns>List<SNProcessEntity></returns>
        public List<SNProcessTempEntity> GetAll()
        {
            return db.Fetch<SNProcessTempEntity>("order by plan_code,pline_code,location_code,routing_code,nvl(process_code,0)");
        }

        /// <summary>
        /// 函数说明：返回Entity
        /// </summary>
        /// <param name="RMES_ID"></param>
        /// <returns>SNProcessEntity</returns>
        public SNProcessTempEntity GetByKey(string RmesID)
        {
            return db.First<SNProcessTempEntity>("WHERE RMES_ID=@0", RmesID);
        }

        /// <summary>
        /// 函数说明：返回List
        /// </summary>
        /// <param name="RMES_ID"></param>
        /// <returns>List<SNProcessEntity></returns>
        public List<SNProcessTempEntity> GetSingleByKey(string RmesID)
        {
            return db.Fetch<SNProcessTempEntity>("WHERE RMES_ID=@0", RmesID);
        }

        public List<SNProcessTempEntity> GetCurrentSN(string CompanyCode, string PlineCode, string StationCode)
        {
            return db.Fetch<SNProcessTempEntity>("where company_code=@0 and pline_code=@1 and station_code=@2 and complete_flag in('A','B')", CompanyCode, PlineCode, StationCode);
        }

        public List<SNProcessTempEntity> GetProcessList(string CompanyCode, string PlineCode, string StationCode, string PlanCode, string SN)
        {
            //查看是否已经初始化工序临时表，如果没有进行初始化
            string workunit_code="",SQL="";

            string UserID = LoginInfo.UserInfo.USER_ID;
            db.Execute("call PL_INIT_PROCESS_TEMP(@0,@1,@2,@3,@4,@5)", CompanyCode, PlineCode, StationCode, PlanCode, SN, UserID);
            
            StationEntity ent_station = StationFactory.GetByKey(StationCode);
            //if (ent_station != null) workunit_code = ent_station.WORKUNIT_CODE;
            if (ent_station != null) workunit_code = "";
            SQL = "where plan_code='" + PlanCode + "' and workunit_code='" + workunit_code + "'";
            if (!string.IsNullOrWhiteSpace(SN)) SQL += " and sn='" + SN + "'";
            SQL += " order by PROCESS_CODE";
            return db.Fetch<SNProcessTempEntity>(SQL);

        }

        public bool ProcessNotConfirmed(string CompanyCode, string PlanCode, string Sn, string PlineCode, string Station)
        {
            string SQL = "select count(*) from data_sn_process_temp where company_code=@0 and plan_code=@1  and sn=@2 " +
                         "and pline_code=@3  and complete_flag<>'Y'";
            int count = db.ExecuteScalar<int>(SQL, CompanyCode, PlanCode, Sn, PlineCode, Station);
            return count > 0;
        }

        public void HandleProcessComplete(string RmesID, string StationCode, string CompleteFlag)
        {
            db.Execute("call pl_handle_process_complete(@0,@1,@2)", RmesID, StationCode, CompleteFlag);
        }

    }
}
#endregion