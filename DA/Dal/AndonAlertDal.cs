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
    internal class AndonAlertDal:BaseDalClass
    {
        public List<AndonAlertEntity> GetAllAlert()
        {
            return db.Fetch<AndonAlertEntity>("");
        }
        public AndonAlertEntity ReadByPrimaryKey(string RMES_ID)
        {
            List<AndonAlertEntity> list1 = db.Fetch<AndonAlertEntity>("where RMES_ID=@0", RMES_ID);
            if (list1.Count < 1)
                return null;
            else return list1[0];
        }
        public List<AndonAlertEntity> ReadByPlineCode(string PLINE_CODE)
        {
            return db.Fetch<AndonAlertEntity>("where PLINE_CODE=@0 and REPORT_FLAG='Y' order by ANDON_ALERT_TIME", PLINE_CODE);
        }
        public AndonAlertEntity ReadByLocation(string LOCATION_CODE)
        {
            List<AndonAlertEntity> list1 = db.Fetch<AndonAlertEntity>("where LOCATION_CODE=@0", LOCATION_CODE);
            if (list1.Count < 1)
                return null;
            else return list1[0];
        }
        public List<AndonAlertEntity> GetAndon(string CompanyCode, string PlineCode)
        {
            return db.Fetch<AndonAlertEntity>("where COMPANY_CODE=@0 and PLINE_CODE=@1 order by ANDON_ALERT_TIME", CompanyCode, PlineCode);
        }
        public int Update(AndonAlertEntity en)
        {
            return db.Update(en);
        }
        public List<AndonAlertEntity> GetByTime()
        {
            return db.Fetch<AndonAlertEntity>("where trunc(ANDON_ALERT_TIME)>trunc(sysdate-1) ORDER BY ANDON_ALERT_TIME DESC");
        }
        public List<AndonAlertEntity> GetAutoLine(string LOCATION_CODE)//自动线andon信息
        {
            return db.Fetch<AndonAlertEntity>("where ANDON_TYPE_CODE='F' and LOCATION_CODE=@0 order by ANDON_ALERT_TIME DESC", LOCATION_CODE);
        }
    }
}
