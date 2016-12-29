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
    internal class PlanProcessNoteDal : BaseDalClass
    {
        /// <summary>
        /// 函数说明：返回List
        /// </summary>
        /// <returns>List<PlanProcessNoteEntity></returns>
        public List<PlanProcessNoteEntity> GetAll()
        {
            return db.Fetch<PlanProcessNoteEntity>("");
        }

        /// <summary>
        /// 函数说明：返回Entity
        /// </summary>
        /// <param name="RMES_ID"></param>
        /// <returns>PlanProcessNoteEntity</returns>
        public PlanProcessNoteEntity GetByKey(string RmesID)
        {
            return db.First<PlanProcessNoteEntity>("WHERE RMES_ID=@0", RmesID);
        }

        /// <summary>
        /// 函数说明：返回List
        /// </summary>
        /// <param name="RMES_ID"></param>
        /// <returns>List<PlanProcessNoteEntity></returns>
        public List<PlanProcessNoteEntity> GetSingleByKey(string RmesID)
        {
            return db.Fetch<PlanProcessNoteEntity>("WHERE RMES_ID=@0", RmesID);
        }

        public List<PlanProcessNoteEntity> GetByProcessProduct(string ComanyCode, string PlineCode, string ProcessCode, string ProductSeries,string PlanCode)
        {
            string SQL = "WHERE company_code=@0 and pline_code=@1 and process_code=@2 and product_series=@3 and plan_code=@4";
            return db.Fetch<PlanProcessNoteEntity>(SQL, ComanyCode, PlineCode, ProcessCode, ProductSeries,PlanCode);
        }


    }
}
#endregion