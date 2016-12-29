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
    public class User_PlineFactory
    {
        public static List<User_PlineEntity> GetByUserID(string userID)
        {
            return new User_PlineDal().GetByUserID(userID);
        }
    }
    public class TeamUser_PlineFactory
    {
        public static List<TeamUser_PlineEntity> GetByUserID(string userID)
        {
            return new TeamUser_PlineDal().GetByUserID(userID);
        }
    }
}
