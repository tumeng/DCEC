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
    public static class KeyWorklogFactory
    {
        public static void InsertLog(string userID, string workType)
        {
            KeyWorklogEntity entity = new KeyWorklogEntity
            {
                USER_ID=userID,
                WORK_TYPE=workType,
                CREATE_TIME=DateTime.Now
            };
            KeyWorklogDal dal = new KeyWorklogDal();
            dal.Insert(entity);
        }

        public static void InsertLog(KeyWorklogEntity entity)
        {
            KeyWorklogDal dal = new KeyWorklogDal();
            dal.Insert(entity);
        }
    }
}
