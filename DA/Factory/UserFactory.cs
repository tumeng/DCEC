using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rmes.DA.Base;
using Rmes.DA.Entity;
using Rmes.DA.Dal;

namespace Rmes.DA.Factory
{
    public static class UserFactory
    {
        /// <summary>
        /// 用于工作站客户端登录，系统登录判断，根据登录时候的信息，判断登录是否有效，请根据实际需要修改登录逻辑
        /// </summary>
        /// <param name="ucode">用户代码</param>
        /// <param name="upass">用户密码（初期不加密）</param>
        /// <param name="companycode">公司代码</param>
        /// <param name="plinecode">生产线代码</param>
        /// <param name="stationcode">站点代码</param>
        /// <param name="shiftcode">班次代码</param>
        /// <param name="groupcode">班组代码</param>
        /// <param name="othercode">备用代码</param>
        /// <param name="ip">客户端IP</param>
        /// <param name="workdate">登录时间</param>
        /// <returns>返回登录是否成功，true为成功，false不成功</returns>
        public static bool GetByFormAuth(string ucode, string upass, string companycode,string workshopcode, string plinecode, string stationcode, 
                                      string shiftcode, string teamcode, string othercode, string ip, string workdate)
        {
            if (string.IsNullOrWhiteSpace(ucode))
            {
                Rmes.Public.ErrorHandle.EH.LASTMSG = "用户名不能为空";
                return false;
            }
            //if (string.IsNullOrWhiteSpace(upass))
            //{
            //    Rmes.Public.ErrorHandle.EH.LASTMSG = "密码不能为空";
            //    return false;
            //}
            if (string.IsNullOrWhiteSpace(companycode))
            {
                Rmes.Public.ErrorHandle.EH.LASTMSG = "公司不能为空";
                return false;
            }
            //if (string.IsNullOrWhiteSpace(workshopcode))
            //{
            //    Rmes.Public.ErrorHandle.EH.LASTMSG = "工厂不能为空";
            //    return false;
            //}
            if (string.IsNullOrWhiteSpace(plinecode))
            {
                Rmes.Public.ErrorHandle.EH.LASTMSG = "生产线不能为空";
                return false;
            }
            if (string.IsNullOrWhiteSpace(stationcode))
            {
                Rmes.Public.ErrorHandle.EH.LASTMSG = "站点不能为空";
                return false;
            }
            if (string.IsNullOrWhiteSpace(shiftcode))
            {
                Rmes.Public.ErrorHandle.EH.LASTMSG = "班次不能为空";
                return false;
            }
            if (string.IsNullOrWhiteSpace(teamcode))
            {
                Rmes.Public.ErrorHandle.EH.LASTMSG = "班组不能为空";
                return false;
            }

            //List<UserEntity> users = new UserDal().FindByAuth(ucode, upass);
            List<UserEntity> users = new UserDal().FindByAuthUser(ucode);
            if (users.Count == 1)
            {
                LoginInfo.UserInfo = users.First();
                LoginInfo.CompanyInfo = CompanyFactory.GetByKey(companycode);
                LoginInfo.WorkShopInfo = null;
                LoginInfo.ProductLineInfo = ProductLineFactory.GetByKey(plinecode);
                LoginInfo.StationInfo = StationFactory.GetBySTATIONCODE(stationcode);
                LoginInfo.TeamInfo = TeamFactory.GetByTeamCode(teamcode);
                LoginInfo.ShiftInfo = ShiftFactory.GetBySCode(shiftcode);


                List<RELStationEntity> stat1 = ProductLineFactory.GetByUS(LoginInfo.StationInfo.RMES_ID);
                if(stat1.Count>0)
                {
                    List<RELStationEntity> stat2 = ProductLineFactory.GetByUS1(LoginInfo.UserInfo.USER_ID, LoginInfo.StationInfo.RMES_ID);
                    if (stat2.Count == 0)
                    {
                        Rmes.Public.ErrorHandle.EH.LASTMSG = "您没有此站点的权限，请联系管理员！";
                        return false;
                    }
                }

                //List<ProductLineEntity> plines = ProductLineFactory.GetByUserIDCS(LoginInfo.UserInfo.USER_ID,LoginInfo.StationInfo.RMES_ID);
                //ProductLineEntity p = LoginInfo.ProductLineInfo;
                //if (!plines.Contains(p))
                //{
                //    Rmes.Public.ErrorHandle.EH.LASTMSG = "您没有此产线的权限，请联系管理员！";
                //    return false;
                //}
                //if (!LoginInfo.TeamInfo.PLINE_CODE.Equals(LoginInfo.ProductLineInfo.PLINE_CODE))
                //{
                //    Rmes.Public.ErrorHandle.EH.LASTMSG = "班组和生产线不对应。";
                //    return false;
                //}
                //if (!LoginInfo.ShiftInfo.PLINE_CODE.Equals(LoginInfo.ProductLineInfo.PLINE_CODE))
                //{
                //    Rmes.Public.ErrorHandle.EH.LASTMSG = "班次和生产线不对应。";
                //    return false;
                //}
                if (!LoginInfo.StationInfo.PLINE_CODE.Equals(LoginInfo.ProductLineInfo.RMES_ID))
                {
                    Rmes.Public.ErrorHandle.EH.LASTMSG = "所选站点和生产线不对应。";
                    return false;
                }
                if (DB.ReadConfigServer("LOGIN_CLIENT_VERIFY_TEAM_USER").Equals("TRUE"))
                {
                    var _s = (from t in TeamFactory.GetByUserID(LoginInfo.UserInfo.USER_ID)
                              select t.RMES_ID).ToList<string>();
                    if (!_s.Contains(LoginInfo.TeamInfo.RMES_ID))
                    {
                        Rmes.Public.ErrorHandle.EH.LASTMSG = "登录身份与班组不匹配。";
                        return false;
                    }
                }

                LoginInfo.IP = ip;

                //LoginInfo.WorkDate = Convert.ToDateTime(workdate).ToString("yyyy-MM-dd");
                LoginInfo.WorkDate = ProductLineFactory.GetWorkdate(LoginInfo.CompanyInfo.COMPANY_CODE,LoginInfo.ProductLineInfo.PLINE_CODE,LoginInfo.ShiftInfo.SHIFT_CODE);
                return true;
            }
            else
            {
                Rmes.Public.ErrorHandle.EH.LASTMSG = "您输入的用户名/密码不正确。";
            }
            return false;
        }

        public static List<UserEntity> GetAll()
        {
            return new UserDal().GetAll();
        }

        public static List<UserEntity> FindByAuth(string usercode, string password)
        {
            return new UserDal().FindByAuth(usercode, password);

        }
        public static List<IEntity> GetSome(int index, int size, string OrderPropertyName)
        { return null; }
        public static List<IEntity> GetBySql(string sql)
        { return null; }

        public static IEntity GetByKey(object keyvalue)
        { return null; }

        public static IEntity New()
        { return null; }
        public static int Save(IEntity entity)
        { return 0; }
        public static void Remove(IEntity entity)
        { }
        public static void RemoveByKey(Object PrimaryKey)
        { }

        public static RmesExtendInfo GetExtendInfo(IEntity entity)
        { return null; }
        public static UserEntity GetByID(string USERID)
        {
            return new UserDal().GetByID(USERID);
        }

        public static UserEntity GetByUserCode(string userCode)
        {
            return new UserDal().GetByUserCode(userCode);
        }
    }
}


