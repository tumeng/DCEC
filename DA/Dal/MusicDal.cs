using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rmes.DA.Base;
using Rmes.DA.Entity;
using Rmes.DA.Dal;
using Rmes.DA.Factory;


namespace Rmes.DA.Dal
{
    internal class MusicDal
    {
        public List<MusicEntity> GetAllMusic()
        {
            PetaPoco.Database db = DB.GetInstance();
            return db.Fetch<MusicEntity>("");
        }
        public MusicEntity GetByPrimaryKey1(string ANDON_TIME)
        {
            PetaPoco.Database db = DB.GetInstance();
            List<MusicEntity> Mulist = db.Fetch<MusicEntity>("where ANDON_TIME=@0", ANDON_TIME);
            if (Mulist.Count < 1)
                return null;
            else
                return Mulist[0];
        }
    }
}
