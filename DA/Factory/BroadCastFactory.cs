using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rmes.DA.Base;
using Rmes.DA.Dal;
using Rmes.DA.Entity;
using Rmes.DA.Factory;
using System.Threading;

namespace Rmes.DA.Factory
{
   public class BroadCastFactory
    {
    
        AndonAlertEntity AndonEntity;
        Thread td;
        //public void  startsend()
        //{
        //    if (td == null)
        //        td = new Thread(this.speaking);
        //    // td.IsAlive
        //    td.Start();
        //}
        List<string> str=new List<string>();
        public BroadCastFactory(AndonAlertEntity AEntity)
        {
            AndonEntity = AEntity;
        }
        public List<string> speaking()
        {
            List<AndonAlertEntity> list =AndonFactory.GetByPline(AndonEntity.PLINE_CODE);
           // LocationEntity LocationEn = dal1.GetByLocationCode(AndonEntity.LOCATION_CODE);
            if (AndonEntity.REPORT_FLAG == "Y")
            {
                
                foreach (AndonAlertEntity list1 in list)
                {
                    StationEntity Stationen = StationFactory.GetByPrimaryKey(list1.LOCATION_CODE);
                    str.Add(Stationen.STATION_NAME +  list1.ANDON_ALERT_CONTENT);
                }
                return str;
            }
            else return null;
        }
    }
}
