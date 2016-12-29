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
    internal class DetectDataDal : BaseDalClass
    {
        /// <summary>
        /// 函数说明：返回List
        /// </summary>
        /// <returns>List<DetectDataEntity></returns>
        public List<DetectDataEntity> GetAll()
        {
            return db.Fetch<DetectDataEntity>("");
        }
        public List<DetectDataEntity> GetByUser(string userid, string proid)
        {
            return db.Fetch<DetectDataEntity>(" where pline_code in (select pline_id from vw_user_role_program where user_id=@0 and program_code=@1 )",userid,proid);
        }
        /// <summary>
        /// 函数说明：返回Entity
        /// </summary>
        /// <param name="RMES_ID"></param>
        /// <returns>DetectDataEntity</returns>
        public DetectDataEntity GetByKey(string RmesID)
        {
            return db.First<DetectDataEntity>("WHERE RMES_ID=@0", RmesID);
        }

        /// <summary>
        /// 函数说明：返回List
        /// </summary>
        /// <param name="RMES_ID"></param>
        /// <returns>List<DetectDataEntity></returns>
        public List<DetectDataEntity> GetSingleByKey(string RmesID)
        {
            return db.Fetch<DetectDataEntity>("WHERE RMES_ID=@0", RmesID);
        }
   
        public List<DetectDataEntity> GetByDetectCode(string CompanyCode, string DecectCode)
        {
            return db.Fetch<DetectDataEntity>("where company_code=@0 and detect_data_code=@1", CompanyCode, DecectCode);
        }

        public List<DetectDataEntity> GetByWorkunit(string CompanyCode, string WorkunitCode)
        {
            return db.Fetch<DetectDataEntity>("where company_code=@0 and workunit_code=@1", CompanyCode, WorkunitCode);
        }
             

    }
}
#endregion