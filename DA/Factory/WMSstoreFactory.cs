using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rmes.DA.Base;
using Rmes.DA.Entity;
using Rmes.DA.Factory;
using Rmes.DA.Dal;


namespace Rmes.DA.Factory
{
    public static class WMSstoreFactory
    {
        public static List<WMSstoreEntity> GetAll()
        {
            return new WMSstoreDal().GetAll();

        }
        public static WMSstoreEntity GetByItemCode(string itemCode)
        {
            return new WMSstoreDal().GetByItemCode(itemCode);

        }


        
    }
}
