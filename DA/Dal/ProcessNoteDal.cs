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
    internal class ProcessNoteDal : BaseDalClass
    {
        /// <summary>
        /// 函数说明：返回List
        /// </summary>
        /// <returns>List<ProcessNoteEntity></returns>
        public List<ProcessNoteEntity> GetAll()
        {
            return db.Fetch<ProcessNoteEntity>("");
        }

        /// <summary>
        /// 函数说明：返回Entity
        /// </summary>
        /// <param name="RMES_ID"></param>
        /// <returns>ProcessNoteEntity</returns>
        public ProcessNoteEntity GetByKey(string RmesID)
        {
            return db.First<ProcessNoteEntity>("WHERE RMES_ID=@0", RmesID);
        }

        /// <summary>
        /// 函数说明：返回List
        /// </summary>
        /// <param name="RMES_ID"></param>
        /// <returns>List<ProcessNoteEntity></returns>
        public List<ProcessNoteEntity> GetSingleByKey(string RmesID)
        {
            return db.Fetch<ProcessNoteEntity>("WHERE RMES_ID=@0", RmesID);
        }

        public List<ProcessNoteEntity> GetByProcessProduct(string ComanyCode,string PlineCode,string ProcessCode,string ProductSeries)
        {
            string SQL = "WHERE company_code=@0 and pline_code=@1 and process_code=@2 and product_series=@3";
            return db.Fetch<ProcessNoteEntity>(SQL, ComanyCode, PlineCode, ProcessCode,ProductSeries);
        }


    }
}
#endregion