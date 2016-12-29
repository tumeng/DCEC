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
    public static class InterIssueFactory
    {
        public static InterIssueEntity getByTMBH(string tmbh)
        {
            return new InterIssueDal().GetByTMBH(tmbh);
        }

        public static List<InterIssueEntity> GetByProjectCodes(string[] projects)
        {
            return new InterIssueDal().GetByProjectCodes(projects);
        }

        public static List<InterIssueEntity> GetAll()
        {
            return new InterIssueDal().GetAll();
        }

        public static List<InterIssueEntity> GetByDate(DateTime date)
        {
            return new InterIssueDal().GetByDate(date);
        }

        public static int Update(InterIssueEntity IIEntity)
        {
            return new InterIssueDal().Update(IIEntity);
        }

        public static List<InterIssueEntity> GetByCtrlStock(DateTime b,DateTime e,string teamCode)
        {
            return new InterIssueDal().GetByCtrlStock(b,e,teamCode);
        }

        public static int ResetLLBS()
        {
            return new InterIssueDal().ResetLLBS();
        }

        public static List<InterIssueEntity> GetByWorkShopCode(string workShopCode)
        {
            return new InterIssueDal().GetByWorkShop(workShopCode);

        }

    }
}
