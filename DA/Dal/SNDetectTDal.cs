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
    internal class SNDetectDal:BaseDalClass
    {
        /// <summary>
        /// 函数说明：返回List
        /// </summary>
        /// <returns>List<DATA_SN_DETECTEntity></returns>
        public List<SNDetectEntity> GetAll()
        {
            return db.Fetch<SNDetectEntity>("");
        }

        /// <summary>
        /// 函数说明：返回Entity
        /// </summary>
        /// <param name="RMES_ID"></param>
        /// <returns>DATA_SN_DETECTEntity</returns>
        public SNDetectEntity GetByKey(string RmesID)
        {
            return db.First<SNDetectEntity>("WHERE RMES_ID=@0", RmesID);
        }
        public List<SNDetectdDataEntity> GetBySn(string sn)
        {
            return db.Fetch<SNDetectdDataEntity>("where SN =@0", sn);
        }
        /// <summary>
        /// 函数说明：返回List
        /// </summary>
        /// <param name="RMES_ID"></param>
        /// <returns>List<DATA_SN_DETECTEntity></returns>
        public List<SNDetectEntity> GetSingleByKey(string RmesID)
        {
            return db.Fetch<SNDetectEntity>("WHERE RMES_ID=@0", RmesID);
        }
        public List<SNDetectdDataEntity> GetDetectDataAuto(string pCompanyCode, string pPlineCode, string pFromDate, string pToDate)
        {
            return db.Fetch<SNDetectdDataEntity>("where company_code=@0 and pline_code=@1 and work_time between to_date(@2,'yyyymmdd') and to_date(@3,'yyyymmdd')",
                                                    pCompanyCode, pPlineCode, pFromDate, pToDate);

        }
        public  List<SNDetectdDataEntity> GetByPlineCodeSn(string pCompanyCode, string pPlineCode, string pSn)
        {
            return db.Fetch<SNDetectdDataEntity>("where company_code=@0 and pline_code=@1 and sn=@2",pCompanyCode, pPlineCode, pSn);

        }

    }
}
