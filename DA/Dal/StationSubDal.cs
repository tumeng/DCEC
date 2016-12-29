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


#region 自动生成实体类工具生成，数据库："ORCL
//From XYJ
//时间：2013-12-08
//
#endregion

#region
namespace Rmes.DA.Dal
{
    internal class StationSubDal : BaseDalClass
    {
        /// <summary>
        /// 函数说明：返回List
        /// </summary>
        /// <returns>List<StationSubEntity></returns>
        public List<StationSubEntity> GetAll()
        {
            return db.Fetch<StationSubEntity>("");
        }

        /// <summary>
        /// 函数说明：返回Entity
        /// </summary>
        /// <param name="RMES_ID"></param>
        /// <returns>StationSubEntity</returns>
        public StationSubEntity GetByKey(string RmesID)
        {
            return db.First<StationSubEntity>("WHERE RMES_ID=@0", RmesID);
        }

        /// <summary>
        /// 函数说明：返回List
        /// </summary>
        /// <param name="RMES_ID"></param>
        /// <returns>List<StationSubEntity></returns>
        public List<StationSubEntity> GetSingleByKey(string RmesID)
        {
            return db.Fetch<StationSubEntity>("WHERE RMES_ID=@0", RmesID);
        }

        public void Insert(StationSubEntity entity)
        {
            db.Insert(entity);
        }

        public void Update(StationSubEntity entity)
        {
            db.Update(entity);
        }

        public void Delete(StationSubEntity entity)
        {
            db.Delete(entity);
        }
    }
}
#endregion