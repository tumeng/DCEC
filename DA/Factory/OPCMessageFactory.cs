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


namespace Rmes.DA.Factory
{
    public static class OPCMessageFactory
    {
        public static List<OPCMessageEntity> GetAll()
        {
            return new OPCMessageDal().GetAll();
        }
        public static OPCMessageEntity GetByKey(string RmesID)
        {
            return new OPCMessageDal().GetByKey(RmesID);
        }
        public static OPCMessageEntity GetMyMessage()
        {
            return new OPCMessageDal().GetMyMessage();
        }
        public static void UpdateMessage(OPCMessageEntity ent)
        {
            new OPCMessageDal().UpdateMessage(ent);
        }
        public static List<OPCStationEntity> GetOPCStations()
        {
            return new OPCMessageDal().GetOPCStations();
        }
    }
}
