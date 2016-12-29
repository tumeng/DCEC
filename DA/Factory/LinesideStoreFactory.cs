using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rmes.DA.Base;
using Rmes.DA.Entity;
using Rmes.DA.Factory;
using Rmes.DA.Dal;

/// <summary>
/// 作者：limm
/// 功能描述：线边库存操作
/// </summary>
/// 

namespace Rmes.DA.Factory
{
    public static class LinesideStoreFactory
    {
      

          /// <returns>LineSideStoreEntity</returns>
        public static LineSideStoreEntity GetByWKUnit(string CompanyCode, string WorkUnitCode)
        {
            return new LineSideStoreDal().GetByWKUnit(CompanyCode, WorkUnitCode);
        }

        
        public static List<LineSideStoreEntity> GetAll()
        {
            return new LineSideStoreDal().GetAll();
        }


        public static List<LineSideStoreEntity> GetByPline(string plineCode)
        {
            return new LineSideStoreDal().GetByPline(plineCode);
        }

        public static List<LineSideStoreEntity> GetResourceStore()
        {
            return new LineSideStoreDal().GetResourceStore();
        }

        public static List<LineSideStoreEntity> GetMaterialStore()
        {
            return new LineSideStoreDal().GetMaterialStore();
        }

        public static List<LineSideStoreEntity> GetLineSideStore()
        {
            return new LineSideStoreDal().GetLineSideStore();
        }

        public static LineSideStoreEntity GetByStoreCode(string CompanyCode, string storeCode)
        {
            return new LineSideStoreDal().GetByStoreCode(CompanyCode,storeCode);
        }
    }
}
