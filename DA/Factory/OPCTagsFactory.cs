using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rmes.DA.Base;
using Rmes.DA.Entity;
using Rmes.DA.Factory;
using Rmes.DA.Dal;

namespace Rmes.DA.Factory
{
    public static class OPCTagsFactory
    {
        public static List<OPCTagsEntity> GetAll()
        {
            return new OPCTagsDal().GetAll();
        }
        public static List<OPCTagsEntity> GetLiveAddress()
        {
            return new OPCTagsDal().GetLiveAddress();

        }
        public static List<OPCTagsEntity> GetByLocationGroup(string LocationID, string GroupID)
        {
            return new OPCTagsDal().GetByLocationGroup(LocationID, GroupID);
        }

        public static List<OPCTagsEntity> GetByLocation(string LocationID)
        {
            return new OPCTagsDal().GetByLocation(LocationID);
        }
        public static List<OPCTagsEntity> GetQualityItems(string LocationID, string GroupID)
        {
            return new OPCTagsDal().GetQualityItems(LocationID,GroupID);
        }
    }
}
