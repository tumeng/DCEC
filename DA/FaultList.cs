using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rmes.DA.Base;
using Rmes.DA.Entity;
using Rmes.DA.Dal;
using Rmes.DA.Factory;


namespace Rmes.DA.Factory
{
    public static class FaultListFactory
    {
        public static List<FaultListEntity> GetAll()
        {
            return new FaultListDal().GetAll();
        }
        public static FaultListEntity GetByKey(string rmesID)
        {
            return new FaultListDal().GetByKey(rmesID);
        }

        public static List<FaultListEntity> GetBySN(string sn)
        {
            return new FaultListDal().GetBySN(sn);
        }

        public static void Save(FaultListEntity fault)
        {
            FaultListDal dal = new FaultListDal();

            dal.Insert(fault);

            //if (DB.GetInstance().IsNew(fault))
            //{
            //    dal.Insert(fault);
            //}
            //else
            //{
            //    dal.Update(fault);
            //}
        }

        public static void Delete(string keyID)
        {
            FaultListDal dal = new FaultListDal();
            dal.RemoveByKey<FaultListEntity>(keyID);
        }


    }
}
namespace Rmes.DA.Dal
{
    internal class FaultListDal : BaseDalClass
    {
        public List<FaultListEntity> GetAll()
        {
            return db.Fetch<FaultListEntity>("");
        }
        public FaultListEntity GetByKey(string rmesID)
        {
            return db.First<FaultListEntity>("WHERE RMES_ID=@0", rmesID);
        }

        public List<FaultListEntity> GetBySN(string snCode)
        {
            return db.Fetch<FaultListEntity>("WHERE SN=@0", snCode);
        }
    }
}
