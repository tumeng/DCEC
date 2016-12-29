using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rmes.DA.Base;
using Rmes.DA.Entity;
using Rmes.DA.Factory;
using Rmes.DA.Dal;
using PetaPoco;

namespace Rmes.DA.Factory
{
    public static class IssueReceivedFactory
    {
        public static string Insert(IssueReceivedEntity entity)
        {
            return new IssueReceivedDal().Insert(entity);
        }
        public static IssueReceivedEntity GetByRMESID(string rmesid)
        {
            return new IssueReceivedDal().GetByRmesID(rmesid);
        }

        public static string IssueReceived(IssueReceivedEntity entity)
        {
            IssueReceivedDal IRDal = new IssueReceivedDal();
            IRDal.DataBase.BeginTransaction();
            string rmesid = IRDal.Insert(entity);
            LineSideStockFactory.Storage(entity.ITEM_CODE,entity.VENDOR_CODE,"",entity.LOCATION_CODE,entity.PLINE_CODE,entity.ITEM_QTY);
            IRDal.DataBase.CompleteTransaction();
            return rmesid;
        }

        public static List<IssueReceivedEntity> GetByDate(DateTime date)
        {
            return new IssueReceivedDal().GetByDate(date);
        }

        public static List<IssueReceivedEntity> GetByCtrlStock(DateTime b, DateTime e, string teamCode)
        {
            return new IssueReceivedDal().GetByCtrlStock(b, e, teamCode);
        }

        public static List<IssueReceivedEntity> GetAll()
        {
            return new IssueReceivedDal().GetAll();
        }

        public static List<IssueReceivedEntity> GetByDetailCode(string detailCode)
        {
            return new IssueReceivedDal().GetByDetailCode(detailCode);
        }

        public static List<IssueReceivedEntity> GetByWorkShopID(string workShopID)
        {
            return new IssueReceivedDal().GetByWorkShopID(workShopID);
        }
    }
}
