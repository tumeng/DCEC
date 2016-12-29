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
    class TeamUser_PlineDal :BaseDalClass
    {
        /// <summary>
        /// 函数说明：返回List
        /// </summary>
        /// <returns>List<TeamUser_PlineEntity></returns>
        public List<TeamUser_PlineEntity> GetByUserID(string userID)
        {
            return db.Fetch<TeamUser_PlineEntity>("where USER_ID=@0", userID);
        }
    }
}
