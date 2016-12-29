using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rmes.DA.Dal;
using Rmes.DA.Entity;

namespace Rmes.DA.Factory
{
    public static class User_WorkUnitFactory
    {
        public static void Delete(string companyCode, string userID, string workUnitCode)
        {
             new User_WorkUnitDal().Delete(companyCode,userID,workUnitCode);
        }

        public static void Insert(string companyCode, string userID, string workUnitID)
        {
            //User_WorkUnitDal dal = new User_WorkUnitDal();
            //StationEntity station = StationFactory.GetByPrimaryKey(workUnitID);
            //dal.Delete(companyCode, userID, station.WORKUNIT_CODE);
            
            //User_WorkUnitEntity entity = new User_WorkUnitEntity()
            //{
            //    COMPANY_CODE=companyCode,
            //    PLINE_CODE=station.PLINE_CODE,
            //    WORKSHOP_CODE=station.WORKSHOP_CODE,
            //    WORKUNIT_CODE=station.WORKUNIT_CODE,
            //    USER_ID=userID
            //};
            //dal.Insert(entity);
        }
    }
}
