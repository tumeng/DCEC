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
    internal class LineSideStoreDal : BaseDalClass
    {
        /// <summary>
        /// 函数说明：返回List
        /// </summary>
        /// <returns>List<LineSideStoreEntity></returns>
        public List<LineSideStoreEntity> GetAll()
        {
            return db.Fetch<LineSideStoreEntity>("");
        }

        /// <summary>
        /// 函数说明：返回Entity
        /// </summary>
        /// <param name="RMES_ID"></param>
        /// <returns>LineSideStoreEntity</returns>
        public LineSideStoreEntity GetByKey(string RmesID)
        {
            return db.First<LineSideStoreEntity>("WHERE RMES_ID=@0", RmesID);
        }

        /// <summary>
        /// 函数说明：返回List
        /// </summary>
        /// <param name="RMES_ID"></param>
        /// <returns>List<LineSideStoreEntity></returns>
        public List<LineSideStoreEntity> GetSingleByKey(string RmesID)
        {
            return db.Fetch<LineSideStoreEntity>("WHERE RMES_ID=@0", RmesID);
        }


        public List<LineSideStoreEntity> GetResourceStore()
        {
            return db.Fetch<LineSideStoreEntity>("where STORE_TYPE='1'");
        }

        public List<LineSideStoreEntity> GetLineSideStore()
        {
            return db.Fetch<LineSideStoreEntity>("where STORE_TYPE='0'");
        }

        public List<LineSideStoreEntity> GetMaterialStore()
        {
            return db.Fetch<LineSideStoreEntity>("where STORE_TYPE='1'");
        }

        public LineSideStoreEntity GetByWKUnit(string CompanyCode, string WorkUnitCode)
        {
            try
            {
                return db.First<LineSideStoreEntity>("WHERE COMPANY_CODE=@0 AND workunit_code=@1", CompanyCode, WorkUnitCode);
            }
            catch
            {
                return null;
            }
        }

        public LineSideStoreEntity GetByStoreCode(string CompanyCode, string storeCode)
        {
            try
            {
                return db.First<LineSideStoreEntity>("WHERE COMPANY_CODE=@0 AND STORE_CODE=@1", CompanyCode, storeCode);
            }
            catch
            {
                return null;
            }
        }

        public List<LineSideStoreEntity> GetByPline(string plineCode)
        {
            return db.Fetch<LineSideStoreEntity>("where PLINE_CODE=@0",plineCode);
        }

    }
}
#endregion