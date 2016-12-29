using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rmes.DA.Base;
using Rmes.DA.Entity;

namespace Rmes.DA.Dal
{
    internal class UserDal : BaseDalClass
    {
        public List<UserEntity> FindByID(string userid)
        {
            return db.Fetch<UserEntity>("where CODE=@0", userid);
        }

        public List<UserEntity> FindByAuth(string usercode, string password)
        {
            return db.Fetch<UserEntity>("where USER_CODE=@0 and USER_PASSWORD=@1", usercode, password);
        }
        public List<UserEntity> FindByAuthUser(string usercode)
        {
            return db.Fetch<UserEntity>("where USER_CODE=@0 and rownum=1", usercode);
        }
        public List<UserEntity> FindByAuthUser1(string usercode)
        {
            return db.Fetch<UserEntity>("where USER_CODE=@0 and rownum=1", usercode);
        }
        public List<UserEntity> GetAll()
        {
            return db.Fetch<UserEntity>("");
        }
        public  UserEntity GetByID(string USERID)
        {
            try
            {
                return db.First<UserEntity>("where USER_ID=@0", USERID);
            }
            catch
            {
                return null;
            }
        }
        public UserEntity GetByUserCode(string userCode)
        {
            try
            {
                return db.First<UserEntity>("where USER_CODE=@0 and user_type='B'", userCode.ToString());
            }
            catch
            {
                return null;
            }
        }
    }
}
