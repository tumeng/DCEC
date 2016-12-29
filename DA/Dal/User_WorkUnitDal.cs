using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rmes.DA.Base;
using Rmes.DA.Entity;

namespace Rmes.DA.Dal
{
    internal class User_WorkUnitDal : BaseDalClass
    {
        public void Delete(string companyCode,string userID, string workUnitCode)
        {

            db.Execute("delete from REL_USER_WORKUNIT where USER_ID=@0 and WORKUNIT_CODE=@1 and COMPANY_CODE=@2", userID, workUnitCode,companyCode);
        }

        public void Insert(User_WorkUnitEntity entity)
        {

            db.Insert(entity);
        }

    }
}
