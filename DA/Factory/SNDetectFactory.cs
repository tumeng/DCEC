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

namespace Rmes.DA.Factory
{
    public static class SNDetectFactory
    {
        public static List<SNDetectEntity> GetAll()
        {
            return new SNDetectDal().GetAll();
        }
        public static List<SNDetectdDataEntity> GetBySn(string sn)
        {
            return new SNDetectDal().GetBySn(sn);
        }

        public static List<SNDetectdDataEntity> GetDetectDataAuto(string pCompanyCode, string pPlineCode, string pFromDate, string pToDate)
        {
            return new SNDetectDal().GetDetectDataAuto(pCompanyCode, pPlineCode, pFromDate, pToDate);

        }
        public static List<SNDetectdDataEntity> GetByPlineCodeSn(string pCompanyCode, string pPlineCode,string pSn)
        {
            return new SNDetectDal().GetByPlineCodeSn(pCompanyCode, pPlineCode, pSn);

        }

    }
}
