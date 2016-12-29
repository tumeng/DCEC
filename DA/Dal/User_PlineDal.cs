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

namespace Rmes.DA.Dal
{
    public class User_PlineDal : BaseDalClass
    {
        /// <summary>
        /// 函数说明：返回List
        /// </summary>
        /// <returns>List<User_PlineEntity></returns>
        public List<User_PlineEntity> GetAll()
        {
            return db.Fetch<User_PlineEntity>("");
        }

        /// <summary>
        /// 函数说明：返回Entity
        /// </summary>
        /// <param name="RMES_ID"></param>
        /// <returns>User_PlineEntity</returns>
        public User_PlineEntity GetByKey(string RmesID)
        {
            return db.First<User_PlineEntity>("WHERE RMES_ID=@0", RmesID);
        }

        /// <summary>
        /// 函数说明：返回List
        /// </summary>
        /// <param name="RMES_ID"></param>
        /// <returns>List<User_PlineEntity></returns>
        public List<User_PlineEntity> GetSingleByKey(string RmesID)
        {
            return db.Fetch<User_PlineEntity>("WHERE RMES_ID=@0", RmesID);
        }

        public List<User_PlineEntity> GetByUserID(string userID)
        {
            return db.Fetch<User_PlineEntity>("where USER_ID=@0", userID);
        }

    }
}
