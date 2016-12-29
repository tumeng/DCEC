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
    internal class IssueReceivedDal:BaseDalClass
    {
        public string Insert(IssueReceivedEntity entity)
        {
            return db.Insert(entity).ToString();
        }
        public IssueReceivedEntity GetByRmesID(string rmesid)
        {
            try
            {
                return db.First<IssueReceivedEntity>("where RMES_ID=@0",rmesid);
            }
            catch
            {
                return null;
            }
        }

        public List<IssueReceivedEntity> GetAll()
        {
            return db.Fetch<IssueReceivedEntity>("order by DETAIL_CODE");
        }

        public List<IssueReceivedEntity> GetByDate(DateTime date)
        {
            string _date = date.ToShortDateString();
            string _end = _date + " 23:59:59";
            return db.Fetch<IssueReceivedEntity>("where WORK_TIME between to_date(@0,'yyyy-mm-dd') and to_date(@1,'yyyy-mm-dd hh24:mi:ss')", _date, _end);
        }

        public List<IssueReceivedEntity> GetByCtrlStock(DateTime b, DateTime e, string teamCode)
        {
            string _b = b.ToShortDateString();
            string _e = e.ToShortDateString() + " 23:59:59";
            return db.Fetch<IssueReceivedEntity>("where WORK_TIME>to_date(@0,'yyyy-mm-dd') and WORK_TIME<to_date(@1,'yyyy-mm-dd hh24:mi:ss') and TEAM_CODE=@2", _b, _e, teamCode);
        }

        public List<IssueReceivedEntity> GetByDetailCode(string detailCode)
        {
            return db.Fetch<IssueReceivedEntity>("where DETAIL_CODE=@0", detailCode);
        }

        public List<IssueReceivedEntity> GetByWorkShopID(string workShopID)
        {
            return db.Fetch<IssueReceivedEntity>("where WORKSHOP_ID=@0",workShopID);
        }
    }
}
