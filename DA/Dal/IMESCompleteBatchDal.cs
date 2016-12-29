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
    public class IMESCompleteBatchDal : BaseDalClass
    {
        /// <summary>
        /// 函数说明：返回List
        /// </summary>
        /// <returns>List<IMESCompleteBatchEntity></returns>
        public List<IMESCompleteBatchEntity> GetAll()
        {
            return db.Fetch<IMESCompleteBatchEntity>("");
        }

        /// <summary>
        /// 函数说明：返回Entity
        /// </summary>
        /// <param name="RMES_ID"></param>
        /// <returns>IMESCompleteBatchEntity</returns>
        public IMESCompleteBatchEntity GetByKey(string RmesID)
        {
            return db.First<IMESCompleteBatchEntity>("WHERE RMES_ID=@0", RmesID);
        }

        /// <summary>
        /// 函数说明：返回List
        /// </summary>
        /// <param name="RMES_ID"></param>
        /// <returns>List<IMESCompleteBatchEntity></returns>
        public List<IMESCompleteBatchEntity> GetSingleByKey(string RmesID)
        {
            return db.Fetch<IMESCompleteBatchEntity>("WHERE RMES_ID=@0", RmesID);
        }

        public object Insert(IMESCompleteBatchEntity entity)
        {
            return DB.GetInstance().Execute("insert into IMES_DATA_COMPLETE_BATCH  VALUES(@0,@1,@2,@3,@4,@5,@6,@7,@8,@9)",
                entity.AUFNR, entity.WERKS, entity.MATNR, entity.CHARG, entity.GAMNG, entity.PONSR, entity.CHART, entity.NAME1, entity.VALUE, entity.STATUS);
        }

    }
}