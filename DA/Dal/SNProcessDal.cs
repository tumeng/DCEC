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
    internal class SNProcessDal : BaseDalClass
    {
        /// <summary>
        /// 函数说明：返回List
        /// </summary>
        /// <returns>List<SNProcessEntity></returns>
        public List<SNProcessEntity> GetAll()
        {
            return db.Fetch<SNProcessEntity>("order by plan_code,pline_code,location_code,routing_code,nvl(process_code,0)");
        }

        /// <summary>
        /// 函数说明：返回Entity
        /// </summary>
        /// <param name="RMES_ID"></param>
        /// <returns>SNProcessEntity</returns>
        public SNProcessEntity GetByKey(string RmesID)
        {
            return db.First<SNProcessEntity>("WHERE RMES_ID=@0", RmesID);
        }

        /// <summary>
        /// 函数说明：返回List
        /// </summary>
        /// <param name="RMES_ID"></param>
        /// <returns>List<SNProcessEntity></returns>
        public List<SNProcessEntity> GetSingleByKey(string RmesID)
        {
            return db.Fetch<SNProcessEntity>("WHERE RMES_ID=@0", RmesID);
        }
        


    }
}
#endregion