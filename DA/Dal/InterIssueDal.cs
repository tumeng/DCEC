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
    internal class InterIssueDal : BaseDalClass
    {
        /// <summary>
        /// 函数说明：返回Entity 若取不到则返回null 注意进行空判断
        /// </summary>
        /// <param name="tmbh"></param>
        /// <returns>InterIssueEntity</returns>
        public InterIssueEntity GetByTMBH(string tmbh)
        {
            try
            {

                return db.First<InterIssueEntity>("WHERE TMBH=@0", tmbh);
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public List<InterIssueEntity> GetByProjectCodes(string[] projects)
        {
            return db.Fetch<InterIssueEntity>("where LLGCH in (@t) and LLBS not in ('B','E') and plan_code is not null", new { t = projects });
        }

        public List<InterIssueEntity> GetAll()
        {
            //return db.Fetch<InterIssueEntity>("where LLBS='N' order by TMBH"); 
            return db.Fetch<InterIssueEntity>("order by TMBH DESC");
        }

        public int Update(InterIssueEntity IIEntity)
        {
            return db.Update("INTER_ISSUE", "TMBH", IIEntity);
        }

        public List<InterIssueEntity> GetByDate(DateTime date)
        {
            string _date = date.ToShortDateString();
            string _end = _date + " 23:59:59";
            return db.Fetch<InterIssueEntity>("where LLRQ between to_date(@0,'yyyy-mm-dd') and to_date(@1,'yyyy-mm-dd hh24:mi:ss'） and LLBS='N'", _date, _end);

        }

        public int ResetLLBS()
        {
            return db.Update<InterIssueEntity>("set LLBS='W' where LLBS='E'");
        }

        public List<InterIssueEntity> GetByCtrlStock(DateTime b, DateTime e, string teamCode)
        {
            string _b = b.ToShortDateString();
            string _e = e.ToShortDateString() + " 23:59:59";
            return db.Fetch<InterIssueEntity>("where LLRQ between to_date(@0,'yyyy-mm-dd') and to_date(@1,'yyyy-mm-dd hh24:mi:ss'） and LLBS in ('N','R') and LLZPXZ=@2", _b, _e, teamCode);
        }

        public List<InterIssueEntity> GetByWorkShop(string workShopCode)
        {
            return db.Fetch<InterIssueEntity>("where LLLYDW=@0",workShopCode); 
        }
    }
}
