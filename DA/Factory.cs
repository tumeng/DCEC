using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rmes.DA.Base;
using Rmes.DA.Dal;
using Rmes.DA.Entity;
using Rmes.DA.Factory;

namespace Rmes.DA.Factory
{
    public static class CompanyFactory
    {
        public static List<CompanyEntity> GetAll()
        {
            return new CompanyDal().GetAll();
        }

        public static CompanyEntity GetByKey(string CompanyCode)
        {
            try
            {
                return new CompanyDal().GetByKey(CompanyCode);
            }
            catch
            {
                return null;
            }
        }

        public static int RemoveByKey(string companycode)
        {
            CompanyDal dal = new CompanyDal();
            if (!dal.Exsits<CompanyEntity>(companycode))
            {
                Rmes.Public.ErrorHandle.EH.LASTMSG = "没有相应的公司数据，无法删除。";
                return 1;
            }
            //存储过程专属的检查
            if(DB.CheckAllowRemove("CODE_COMPANY","","","","",companycode)) 
            {
                //删除成功
                dal.RemoveByKey<CompanyEntity>(companycode);
                return 0;
            }
            else
            {
                //删除失败
                Rmes.Public.ErrorHandle.EH.ERROR(Rmes.Public.ErrorHandle.EH.LASTMSG);
                return -1;
            }
        }

        public static bool Exsits(string companycode)
        {
            return new CompanyDal().Exsits<CompanyEntity>(companycode);
        }
    }
    public static class MessageHeadFactory
    {
        public static List<MessageHeadEntity> GetAll()
        {
            return new MessageHeadDal().GetAll();
        }
        public static MessageHeadEntity GetByString(string InputString)
        {
            return new MessageHeadDal().GetByString(InputString);
        }
    }
    public static class StationFactory
    {
        public static bool Insert(StationEntity s)
        {
            return new StationDal().Insert(s);
        }

        public static List<StationEntity> GetAll()
        {
            return new StationDal().GetAll();
        }
        public static StationEntity GetByKey(string StationCode)
        {
            return new StationDal().GetByKey(StationCode);
        }
        public static StationEntity GetBySTATIONCODE(string StationCode)
        {
            return new StationDal().GetBySTATIONCODE(StationCode);
        }
        public static StationEntity GetBySTATIONNAME(string StationCode)
        {
            return new StationDal().GetBySTATIONNAME(StationCode);
        }
        public static StationEntity GetByWorkUnit(string WorkUnitCode)
        {
            return new StationDal().GetByWorkUnit(WorkUnitCode);
        }
        public static List<StationEntity> GetStationsByUserID(string UserID)
        {
            return new StationDal().GetStationsByUserID(UserID);
        }

        public static List<StationEntity> GetByProductLine(string PLineCode)
        {
            return new StationDal().GetByProductLineCode(PLineCode);
        }
        
        public static StationEntity GetByPrimaryKey(string RMES_ID)
        {
            return new StationDal().ReadByPrimaryKey(RMES_ID);
        }

    }
    public static class ShiftFactory
    {
        public static List<ShiftEntity> GetAll()
        {
            return new ShiftDal().GetAll();
        }
        public static ShiftEntity GetByKey(string ShiftCode)
        {
            return new ShiftDal().GetByKey(ShiftCode);
        }
        public static ShiftEntity GetBySCode(string ShiftCode)
        {
            return new ShiftDal().GetBySCode(ShiftCode);
        }
    }
    public static class TeamFactory
    {
        public static List<TeamEntity> GetAll()
        {
            return new TeamDal().GetAll();
        }
        public static TeamEntity GetByTeamCode(string TEAM_CODE)
        {
            return new TeamDal().GetByTeamCode(TEAM_CODE);
        }
       
        public static TeamEntity GetByKey(string TeamCode)
        {
            return new TeamDal().GetByKey(TeamCode);
        }
        
        public static List<TeamEntity> GetByPlineCode(string plineCode)
        {
            return new TeamDal().GetByPlineCode(plineCode);
        }

        public static List<TeamEntity> GetByWorkShopID(string workshopRmesID) 
        {
            List<ProductLineEntity> plines = ProductLineFactory.GetByWorkShopID(workshopRmesID);
            List<TeamEntity> allteams = new List<TeamEntity>();
            foreach (ProductLineEntity pe in plines)
            {
                List<TeamEntity> _temp = GetByPlineCode(pe.RMES_ID);
                if (_temp != null && _temp.Count > 0)
                {
                    allteams.AddRange(_temp);
                }
            }
            return allteams;
        }
        public static TeamEntity GetByRmesID(string RMESID)
        {
            return new TeamDal().GetByRmesID(RMESID);
        }
        public static List<TeamEntity> GetByRmesID1(string[] RMESID)
        {
            return new TeamDal().GetByRmesID1(RMESID);
        }

        /// <summary>
        /// 函数说明：返回List，计划员专用
        /// </summary>
        /// <returns>List<ControlsSnEntity></returns>
        public static List<TeamEntity> GetByUserID(string userid)
        {
            List<TeamEntity> teamEntity = new List<TeamEntity>();
            List<TeamUser_PlineEntity> upEntity = TeamUser_PlineFactory.GetByUserID(userid);
            foreach (TeamUser_PlineEntity up in upEntity)
            {
                List<TeamEntity> tempTeamEntity = GetByPlineCode(up.PLINE_CODE);
                for (int i = 0; i < tempTeamEntity.Count; i++)
                {
                    teamEntity.Add(tempTeamEntity[i]);
                }
            }

            return teamEntity;
        }


        /// <summary>
        /// 函数说明：返回List，根据登陆人获取对应班组
        /// </summary>
        /// <returns>List<ControlsSnEntity></returns>
        public static List<TeamEntity> NormalGetByUserID(string userid)
        {
            List<TeamEntity> teamEntity = new List<TeamEntity>();
            List<TeamUserEntity> TUEntity = TeamUserFactory.GetByUserID(userid);
            foreach (TeamUserEntity t in TUEntity)
            {
                TeamEntity tempTeamEntity = GetByRmesID(t.TEAM_CODE);
                teamEntity.Add(tempTeamEntity);
            }
            return teamEntity;
        }
       
    }
    public static class OptionFactory
    {
        public static List<WorkProcessEntity> GetAll()
        {
            return new WorkProcessDal().GetAll();
        }

        public static WorkProcessEntity GetByKey(string OptionCode)
        {
            return new WorkProcessDal().GetByKey(OptionCode);
        }

        public static int GetSumTime(string plinecode, string stationcode)
        {
            return new WorkProcessDal().GetSumTime(plinecode, stationcode);
        }

    }
    public class OptionRecordFactory : IFactory
    {
        public static List<OptionRecordEntity> GetAll()
        {
            return new OptionRecordDal().GetAll();
        }

        public static OptionRecordEntity GetByKey(string RMES_ID)
        {
            return new OptionRecordDal().GetByKey(RMES_ID);
        }

        public static List<OptionRecordEntity> GetOptionList(string plinecode, string stationcode, string sn)
        {
            return new OptionRecordDal().GetOptionList(plinecode, stationcode, sn);
        }

        public static List<OptionRecordEntity> GetOptionLists(string plinecode, string stationcode, string sn)
        {
            return new OptionRecordDal().GetOptionLists(plinecode, stationcode, sn);
        }

        public static void Add(OptionRecordEntity OptionInfo)
        {
            new OptionRecordDal().Add(OptionInfo);
        }
    }
    public static class WorkProcessFileFactory
    {
        public static List<WorkProcessFileEntity> GetAll()
        {
            return new WorkProcessFileDal().GetAll();
        }

        public static WorkProcessFileEntity GetByKey(string OptionCode)
        {
            return new WorkProcessFileDal().GetByKey(OptionCode);
        }

        public static List<WorkProcessFileEntity> GetFileName(string plinecode, string processcode,string stationcode,string productseries)
        {
            return new WorkProcessFileDal().GetFileName(plinecode, processcode, stationcode, productseries);
        }

    }

    public static class BroadcastFactory
    {
        public static List<BroadcastEntity> GetAll()
        {
            return new BroadcastDal().GetAll();
        }

        public static BroadcastEntity GetByKey(string RmesId)
        {
            return new BroadcastDal().GetByKey(RmesId);
        }

        public static BroadcastEntity GetContextByCompanyPline(string companyCode,string plineRmesid)
        {
            try
            {
                return new BroadcastDal().GetContextByCompanyPline(companyCode, plineRmesid);
            }
            catch
            {
                return null;
            }
        }
    }
}

namespace Rmes.DA.Entity
{
    [PetaPoco.TableName("OPTION_RECORD")]
    [PetaPoco.PrimaryKey("RMES_ID", sequenceName = "SEQ_RMES_ID")]
    public class OptionRecordEntity : IEntity
    {
        public string RMES_ID { get; set; }
        public string COMPANY_CODE { get; set; }
        public string PLINE_CODE { get; set; }
        public string STATION_CODE { get; set; }
        public string OPTION_CODE { get; set; }
        public string OPTION_NAME { get; set; }
        public DateTime START_TIME { get; set; }
        public DateTime END_TIME { get; set; }
        public string SN { get; set; }
    }

}