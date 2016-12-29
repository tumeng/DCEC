using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rmes.DA.Base;
using Rmes.DA.Entity;
using System.Data;


namespace Rmes.DA.Dal
{
    public class OPCTagsDal : BaseDalClass
    {
        public List<OPCTagsEntity> GetAll()
        {
            return db.Fetch<OPCTagsEntity>("");
        }

        public List<OPCTagsEntity> GetLiveAddress()
        {
            return db.Fetch<OPCTagsEntity>("where opc_tag_name='live' order by location_id");
        }
        public List<OPCTagsEntity> GetByLocation(string LocationID)
        {
            List<OPCTagsEntity> ent = db.Fetch<OPCTagsEntity>("where location_id=@0", LocationID);
            return ent;
        }
        public List<OPCTagsEntity> GetByLocationGroup(string LocationID, string GroupID)
        {
            List<OPCTagsEntity> ent = db.Fetch<OPCTagsEntity>("where location_id=@0 and opc_group_id=@1", LocationID, GroupID);
            return ent;
        }
         public List<OPCTagsEntity> GetQualityItems(string LocationID, string GroupID)
        {
            List<OPCTagsEntity> ent = db.Fetch<OPCTagsEntity>("where location_id=@0 and opc_group_id=@1 and quality_flag='1'", LocationID, GroupID);
            return ent;
        }

    }
}
