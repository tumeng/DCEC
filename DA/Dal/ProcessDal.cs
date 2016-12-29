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
    internal class ProcessDal:BaseDalClass
    {
        /// <summary>
        /// 函数说明：返回List
        /// </summary>
        /// <returns>List<ProcessEntity></returns>
        public List<ProcessEntity> GetAll()
        {
            return db.Fetch<ProcessEntity>("");
        }

        /// <summary>
        /// 函数说明：返回Entity
        /// </summary>
        /// <param name="RMES_ID"></param>
        /// <returns>ProcessEntity</returns>
        public ProcessEntity GetByKey(string RmesID)
        {
            return db.First<ProcessEntity>("WHERE RMES_ID=@0", RmesID);
        }

        /// <summary>
        /// 函数说明：返回List
        /// </summary>
        /// <param name="RMES_ID"></param>
        /// <returns>List<ProcessEntity></returns>
        public List<ProcessEntity> GetSingleByKey(string RmesID)
        {
            return db.Fetch<ProcessEntity>("WHERE RMES_ID=@0", RmesID);
        }

        public ProcessEntity GetByWorkUnit(string unitCode)
        {
            try
            {
                return db.First<ProcessEntity>("where WORKUNIT_CODE=@0", unitCode);
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public List<ProcessEntity> GetByPline(string plineCode)
        {
            return db.Fetch<ProcessEntity>("where PLINE_CODE=@0", plineCode);
        }
        public void Insert(ProcessEntity p)
        {
            db.Insert("CODE_PROCESS", "RMES_ID", p);
        }

        public int Update(ProcessEntity p)
        {
            return db.Update(p);
        }

        public void Delete(ProcessEntity p)
        {
            db.Delete(p);
        }
    }
}
