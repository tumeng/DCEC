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
    internal class ProductControlDal : BaseDalClass
    {
        /// <summary>
        /// 函数说明：返回List
        /// </summary>
        /// <returns>List<ControlsSnEntity></returns>
        public List<ProductControlEntity> GetAll()
        {
            return db.Fetch<ProductControlEntity>("");
        }

        /// <summary>
        /// 函数说明：返回Entity
        /// </summary>
        /// <param name="RMES_ID"></param>
        /// <returns>ControlsSnEntity</returns>
        public ProductControlEntity GetByKey(string RmesID)
        {
            return db.First<ProductControlEntity>("WHERE RMES_ID=@0", RmesID);
        }

        /// <summary>
        /// 函数说明：返回List
        /// </summary>
        /// <param name="RMES_ID"></param>
        /// <returns>List<ControlsSnEntity></returns>
        public List<ProductControlEntity> GetSingleByKey(string RmesID)
        {
            return db.Fetch<ProductControlEntity>("WHERE RMES_ID=@0", RmesID);
        }

        public List<ProductControlEntity> GetCurrentCompleteInfo(string CompanyCode, string PlineCode, string StationCode, string PlanCode, string SN)
        {
            return db.Fetch<ProductControlEntity>("where company_code=@0 and pline_code=@1 and station_code=@2", CompanyCode, PlineCode, StationCode);
        }
       
        public int HandleControlComplete(string CompanyCode, string PLineCode, string StationCode, string PlanCode, string SN, string ControlID, string CompleteFlag)
        {
            try
            {
                if (CsGlobalClass.DGSM)
                { return 0; }
                else
                {
                    db.Execute("call PL_HANDLE_CONTROL_COMPLETE(@0,@1,@2,@3,@4,@5,@6)", CompanyCode, PLineCode, StationCode, PlanCode, SN, ControlID, CompleteFlag);
                    return 0;
                }
            }
            catch
            {
                return 1;
            }
        }

        public int MW_COMPARE_GHTMBOM(string CompanyCode, string PLineCode, string StationCode, string PlanCode, string SN, string ControlID, string CompleteFlag)
        {
            try
            {
                db.Execute("call MW_COMPARE_GHTMBOM(@0,@1,@2,@3,@4,@5,@6)", CompanyCode, PLineCode, StationCode, PlanCode, SN, ControlID, CompleteFlag);
                return 0;
            }
            catch
            {
                return 1;
            }
        }

    }
}
#endregion