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

namespace Rmes.DA.Dal
{
    internal class SNDetectTempDal : BaseDalClass
    {
        public List<SNDetectTempEntity> GetAll()
        {
            return db.Fetch<SNDetectTempEntity>("");
        }

        /// <summary>
        /// 函数说明：返回Entity
        /// </summary>
        /// <param name="RMES_ID"></param>
        /// <returns>ControlsSnEntity</returns>
        public SNDetectTempEntity GetByKey(string RmesID)
        {
            return db.First<SNDetectTempEntity>("WHERE RMES_ID=@0", RmesID);
        }

        /// <summary>
        /// 函数说明：返回List
        /// </summary>
        /// <param name="RMES_ID"></param>
        /// <returns>List<ControlsSnEntity></returns>
        public List<SNDetectTempEntity> GetSingleByKey(string RmesID)
        {
            return db.Fetch<SNDetectTempEntity>("WHERE RMES_ID=@0", RmesID);
        }

        public List<SNDetectTempEntity> GetBySNStation(string sn, string StationCode)
        {
            return db.Fetch<SNDetectTempEntity>("where station_code=@0 and SN=@1", StationCode, sn);
        }

        public List<DetectDataEntity> GetByDetectCode(string CompanyCode, string DecectCode)
        {
            return db.Fetch<DetectDataEntity>("where company_code=@0 and detect_data_code=@1", CompanyCode, DecectCode);
        }
        
        public void InitQualitDetectList(string CompanyCode, string PlineCode, string StationCode, string PlanCode, string SN, string UserID,string stationid1)
        {
            db.Execute("call PL_INIT_DETECT_TEMP(@0,@1,@2,@3,@4,@5,@6)", CompanyCode, PlineCode, StationCode, PlanCode, SN, UserID,stationid1);
        }

        public void HandleDetectData(string CompanyCode, string RmesID, string QuanValue,string QualValue,string faultCode,string Remark,string UserID,string stationcode,string stationname)
        {
            db.Execute("call PL_HANDLE_DETECT_COMPLETE(@0,@1,@2,@3,@4,@5,@6)", CompanyCode, RmesID, QuanValue, Remark, UserID,stationcode,stationname);
        }



    }
}
