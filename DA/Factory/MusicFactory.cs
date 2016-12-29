using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rmes.DA.Base;
using Rmes.DA.Entity;
using Rmes.DA.Dal;

namespace Rmes.DA.Factory
{
    public static class MusicFactory
    {
        public static List<MusicEntity> GetAllMusic()
        {
            return new MusicDal().GetAllMusic();
        }
        public static MusicEntity GetByAndonTime(string ANDON_TIME)
        {
            return new MusicDal().GetByPrimaryKey1(ANDON_TIME);
        }
    }
}
