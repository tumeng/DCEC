using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rmes.DA.Base;
using Rmes.DA.Dal;
using Rmes.DA.Entity;
using Rmes.DA.Factory;

namespace RMES.MaterialCal
{
    public static class LoginInfo
    {
        static UserEntity _user;
        static WorkShopEntity _workshop;
        static CompanyEntity _company;
        static ProductLineEntity _pline;
        static StationEntity _station;
        static ShiftEntity _shift;
        static TeamEntity _team;
        static string _ip;
        static string _workDate;
        public static UserEntity UserInfo
        {
            get { return _user; }
            set { _user = value; }
        }
        public static CompanyEntity CompanyInfo
        {
            get { return _company; }
            set { _company = value; }
        }
        public static WorkShopEntity WorkShopInfo
        {
            get { return _workshop; }
            set { _workshop = value; }
        }

        public static ProductLineEntity ProductLineInfo
        {
            get { return _pline; }
            set { _pline = value; }
        }
        public static StationEntity StationInfo
        {
            get { return _station; }
            set { _station = value; }
        }
        public static ShiftEntity ShiftInfo
        {
            get { return _shift; }
            set { _shift = value; }
        }
        public static TeamEntity TeamInfo
        {
            get { return _team; }
            set { _team = value; }
        }
        public static string IP
        {
            get { return _ip; }
            set { _ip = value; }
        }
        public static string WorkDate
        {
            get { return _workDate; }
            set { _workDate = value; }
        }
    }

}
