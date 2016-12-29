using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rmes.DA.Entity;
using Rmes.DA.Base;
using Rmes.DA.Dal;

namespace Rmes.DA.Factory
{
    public static class AndonFactory
    {
        public static  void SaveAndonAlert(AndonAlertEntity entity1)
        {
            new AndonDal().SaveAndonAlert(entity1);
        }
        public static List<AndonAlertEntity> GetByPline(string PLINE_CODE)
        {
            return new AndonAlertDal().ReadByPlineCode(PLINE_CODE);
        }
        public static AndonAlertEntity GetByPrimaryKey(string RMES_ID)
        {
            return new AndonAlertDal().ReadByPrimaryKey(RMES_ID);
        }
        public static int Update(AndonAlertEntity en)
        {
            return new AndonAlertDal().Update(en);
        }
        public static List<AndonAlertEntity> GetByTime()//选出登陆车间一天之内的andon信息
        {
            List<AndonAlertEntity> all=new  AndonAlertDal().GetByTime();
            List<AndonAlertEntity> andon=new List<AndonAlertEntity>();
            for (int i = 0; i < all.Count; i++)
            {
                if (LoginInfo.StationInfo.PLINE_CODE == all[i].PLINE_CODE)
                    andon.Add(all[i]);
            }
            return andon;
        }
        public static AndonAlertEntity GetAutoLine(string LOCATION_CODE)
        {
            List<AndonAlertEntity> andon = new AndonAlertDal().GetAutoLine(LOCATION_CODE);
            return andon.First<AndonAlertEntity>();
        }
    }
}
