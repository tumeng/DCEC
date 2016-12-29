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
    public static class FaultTreeFactory
    {
        public static List<FaultTreeEntity> GetAll()
        {
            return new FaultTreeDal().GetAll();
        }
        public static FaultTreeEntity GetByKey(string faultCode)
        {
            return new FaultTreeDal().GetByKey(faultCode);
        }

        public static List<FaultTreeEntity> GetByLever(int faultLevel)
        {
            return new FaultTreeDal().GetByLever(faultLevel);
        }

        public static List<FaultTreeEntity> GetNextLever(string codeFather)
        {
            return new FaultTreeDal().GetNextLever(codeFather);
        }
    }
}
namespace Rmes.DA.Dal
{
    internal class FaultTreeDal : BaseDalClass
    {
        public List<FaultTreeEntity> GetAll()
        {
            return db.Fetch<FaultTreeEntity>("");
        }
        public FaultTreeEntity GetByKey(string faultCode)
        {
            return db.First<FaultTreeEntity>("WHERE FAULT_CODE=@0", faultCode);
        }

        public List<FaultTreeEntity> GetByLever(int faultLevel)
        {
            return db.Fetch<FaultTreeEntity>("WHERE FAULT_LEVEL=@0 ORDER BY FAULT_INDEX", faultLevel);
        }

        public List<FaultTreeEntity> GetNextLever(string codeFather)
        {
            return db.Fetch<FaultTreeEntity>("WHERE FAULT_CODE_FATHER=@0 ORDER BY FAULT_INDEX", codeFather);
        }

    }
}
