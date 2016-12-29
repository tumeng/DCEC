using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PetaPoco;

#region 北自所Rmes命名空间引用
using Rmes.DA.Base;
using Rmes.DA.Entity;
using Rmes.DA.Dal;
using Rmes.DA.Factory;
#endregion

namespace Rmes.DA.Dal
{
    public class OPCMessageDal : BaseDalClass
    {
        /// <summary>
        /// 函数说明：获取所有记录
        /// </summary>
        /// <returns></returns>
        public List<OPCMessageEntity> GetAll()
        {
            return db.Fetch<OPCMessageEntity>("");
        }
        public OPCMessageEntity GetMyMessage()
        {
            string station_code = LoginInfo.StationInfo.RMES_ID;
            string SQL = "where to_id=@0 and message_flag='0'";
            List<OPCMessageEntity> lst = db.Fetch<OPCMessageEntity>(SQL,station_code);
            if (lst.Count > 0) return lst.First<OPCMessageEntity>();
            else return null;
        }
        public void UpdateMessage(OPCMessageEntity ent)
        {
            //if (db.Exists<OPCMessageEntity>(ent))
            //{
                db.Update(ent);
            //}
            //else
            //{
            //    db.Insert(ent);
            //}
        }
        public OPCMessageEntity GetByKey(string RmesID)
        {
            List<OPCMessageEntity> ent = db.Fetch<OPCMessageEntity>("where rmes_id=@0", RmesID);
            if (ent.Count == 0) return null;
            return ent.First<OPCMessageEntity>();
        }

        public List<OPCStationEntity> GetOPCStations()
        {
            return db.Fetch<OPCStationEntity>("order by LOCATION_CODE ASC");
        }
    }
}
