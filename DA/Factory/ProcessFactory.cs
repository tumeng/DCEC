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

namespace Rmes.DA.Factory
{
    public static class ProcessFactory
    {
        public static ProcessEntity GetByWorkUnit(string unitCode)
        {
            return new ProcessDal().GetByWorkUnit(unitCode);
        }

        public static List<ProcessEntity> GetByPline(string plineCode)
        {
            return new ProcessDal().GetByPline(plineCode);
        }

        public static ProcessEntity GetByID(string id)
        {
            return new ProcessDal().GetByKey(id);
        }
        public static List<ProcessEntity> GetAll()
        {
            return new ProcessDal().GetAll();
        }

        public static void Insert(ProcessEntity p)
        {
            new ProcessDal().Insert(p);
        }

        public static int Update(ProcessEntity p)
        {
            return new ProcessDal().Update(p);
        }
        public static void Delete(ProcessEntity p)
        {
             new ProcessDal().Delete(p);
        }
    }
}
