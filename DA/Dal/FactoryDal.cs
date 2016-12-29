using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rmes.DA.Base;
using Rmes.DA.Entity;
using System.Data;

namespace Rmes.DA.Dal
{
    internal class MessageHeadDal : BaseDalClass
    {
        public List<MessageHeadEntity> GetAll()
        {
            return db.Fetch<MessageHeadEntity>("");
        }
        public MessageHeadEntity GetByString(string InputString)
        {
            List<MessageHeadEntity> enties = db.Fetch<MessageHeadEntity>("WHERE REGEXP_LIKE(@0,COMMAND_REGEX)", InputString);
            if (enties.Count ==1 ) return enties.First<MessageHeadEntity>();
            else return null;
        }
    }
    internal class CompanyDal : BaseDalClass
    {
        public List<CompanyEntity> GetAll()
        {
            return db.Fetch<CompanyEntity>("");
        }

        public CompanyEntity GetByKey(string key)
        {
            return db.First<CompanyEntity>("where COMPANY_CODE=@0", key);
        }
    }
    
    internal class StationDal : BaseDalClass
    {
        public bool Insert(StationEntity s)
        {
            try
            {
                db.Insert("CODE_STATION", "RMES_ID", false, s);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public List<StationEntity> GetAll()
        {
            return db.Fetch<StationEntity>("");
        }
        public StationEntity GetByWorkUnit(string WorkUnitCode)
        {
            List<StationEntity> lst = db.Fetch<StationEntity>("WHERE WORKUNIT_CODE=@0", WorkUnitCode);
            if (lst.Count > 0) return lst[0];
            else return null;
        }
        public List<StationEntity> GetStationsByUserID(string UserID)
        {
            string SQL="select distinct station_code,station_code||'-'||station_name station_name from code_station a where "+
                       "exists(select * from rel_user_workunit b where a.workunit_code=b.workunit_code and b.user_id=@0)";
            List<StationEntity> lst=db.Fetch<StationEntity>(SQL,UserID);
            return lst;

        }
        public StationEntity GetByKey(string stationrmesid)
        {
            List<StationEntity> tempList= db.Fetch<StationEntity>("WHERE RMES_ID=@0", stationrmesid);
            if (tempList.Count > 0)
                return tempList[0];
            else return null;
        }
        public StationEntity GetBySTATIONCODE(string stationrmesid)
        {
            return db.First<StationEntity>("WHERE STATION_CODE=@0", stationrmesid);
        }
        public StationEntity GetBySTATIONNAME(string stationrmesid)
        {
            return db.First<StationEntity>("WHERE STATION_NAME=@0", stationrmesid);
        }
        public StationEntity ReadByPrimaryKey(string RMES_ID)
        {
            List<StationEntity> list1 = db.Fetch<StationEntity>("where RMES_ID=@0", RMES_ID);
            if (list1.Count < 1)
                return null;
            else return list1[0];
        }
        public List<StationEntity> GetByProductLineCode(string plinecode)
        {
            return db.Fetch<StationEntity>("WHERE PLINE_CODE=@0 ORDER BY STATION_AREA_CODE, STATION_CODE", plinecode);
        }
       

    }
    internal class ShiftDal : BaseDalClass
    {
        public List<ShiftEntity> GetAll()
        {
            return db.Fetch<ShiftEntity>("");
        }
        public ShiftEntity GetByKey(string shiftcode)
        {
            return db.First<ShiftEntity>("WHERE RMES_ID=@0", shiftcode);
        }
        public ShiftEntity GetBySCode(string shiftcode)
        {
            return db.First<ShiftEntity>("WHERE SHIFT_CODE=@0", shiftcode);
        }
    }
    internal class TeamDal : BaseDalClass
    {
        public List<TeamEntity> GetAll()
        {
            return db.Fetch<TeamEntity>("");
        }

        public TeamEntity GetByRmesID(string RMESID)
        {
            try
            {
                return db.First<TeamEntity>("where rmes_id=@0", RMESID);
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public List<TeamEntity> GetByRmesID1(string[] RMESID)
        {
            try
            {
                return db.Fetch<TeamEntity>("where RMES_ID in (@t)", new { t = RMESID });
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public List<TeamEntity> GetByPlineCode(string plineCode)
        {
            return db.Fetch<TeamEntity>("where pline_code=@0", plineCode);
        }

        public TeamEntity GetByKey(string key)
        {
            return db.First<TeamEntity>("where RMES_ID=@0", key);
        }
        public TeamEntity GetByTeamCode(string TEAM_CODE)
        {
            try
            {
                return db.First<TeamEntity>("where TEAM_CODE=@0", TEAM_CODE);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
      

    }
    internal class WorkProcessDal : BaseDalClass
    {
        public List<WorkProcessEntity> GetAll()
        {
            return db.Fetch<WorkProcessEntity>("");
        }

        public WorkProcessEntity GetByKey(string key)
        {
            return db.First<WorkProcessEntity>("where PROCESS_CODE=@0", key);
        }

        public int GetSumTime(string plinecode, string stationcode)
        {    //根据站点获取 该站点下所有工序工时总和 以秒为单位
            int num;
            try
            {
                //num = db.ExecuteScalar<int>("select  sum(c.option_time)     from rel_station_location a left join rel_location_option b  on a.pline_code=b.pline_code   and a.location_code = b.location_code left join code_option  c  on a.pline_code=c.pline_code and b.option_code=c.option_code  where a.pline_code=@0 and a.station_code=@1 ", plinecode, stationcode);
                //num = db.ExecuteScalar<int>("select  nvl(sum(c.process_manhour),0) from rel_station_location a left join rel_location_process b on a.pline_code = b.pline_code and a.location_code = b.location_code left join code_process c  on a.pline_code = c.pline_code and b.process_code = c.rmes_id where a.pline_code=@0 and a.station_code=@1 ", plinecode, stationcode);
                num = db.ExecuteScalar<int>("select  nvl(sum(c.location_manhour),0) from rel_station_location a left join code_location c  on a.pline_code = c.pline_code and a.location_code = c.rmes_id where a.pline_code=@0 and a.station_code=@1 and a.location_flag='A'", plinecode, stationcode);
            
            }
            catch
            {
                num = -1;
            }
            return num;
        }

    }
    internal class OptionRecordDal : BaseDalClass
    {
        public List<OptionRecordEntity> GetAll()
        {
            return db.Fetch<OptionRecordEntity>("");
        }

        public OptionRecordEntity GetByKey(string key)
        {
            return db.First<OptionRecordEntity>("where RMES_ID=@0", key);
        }

        public List<OptionRecordEntity> GetOptionList(string plinecode, string stationcode, string sn)
        {
            try
            {
                return db.Fetch<OptionRecordEntity>("select C.OPTION_CODE, C.OPTION_NAME, d.start_time, d.end_time  from rel_station_location a  left join rel_location_option b    on a.pline_code = b.pline_code   and a.location_code = b.location_code  left join code_option c on c.pline_code = a.pline_code and c.option_code = b.option_code  left join option_record d    on d.pline_code = a.pline_code   and d.station_code = a.station_code   and d.option_code = c.option_code   and d.sn=@2  where  a.pline_code = @0  and a.station_code = @1 AND C.OPTION_CODE IS NOT NULL ", plinecode, stationcode, sn);
            }
            catch
            {
                return null;
            }
        }
        public List<OptionRecordEntity> GetOptionLists(string plinecode, string stationcode, string sn)
        {
            try
            {
                return db.Fetch<OptionRecordEntity>("select C.OPTION_CODE, C.OPTION_NAME, d.start_time, d.end_time  from rel_station_location a  left join rel_location_option b    on a.pline_code = b.pline_code   and a.location_code = b.location_code  left join code_option c    on c.pline_code = a.pline_code   and c.option_code = b.option_code  left join option_record d    on d.pline_code = a.pline_code   and d.station_code = a.station_code  and d.sn=@2  where  a.pline_code = @0  and a.station_code = @1 AND C.OPTION_CODE IS NOT NULL ", plinecode, stationcode, sn);
            }
            catch
            {
                return null;
            }
        }
        public void Add(OptionRecordEntity OptionInfo)
        {
            db.Insert(OptionInfo);
        }
    }
    internal class WorkProcessFileDal : BaseDalClass
    {
        public List<WorkProcessFileEntity> GetAll()
        {
            return db.Fetch<WorkProcessFileEntity>("");
        }

        public WorkProcessFileEntity GetByKey(string key)
        {
            return db.First<WorkProcessFileEntity>("where OPTION_CODE=@0", key);
        }

        public List<WorkProcessFileEntity> GetFileName(string plinecode, string processcode, string stationcode, string productseries)
        {
            try
            {
                //string sql = "where pline_code = @0 and process_code in (select process_id from vw_rel_station_process where station_code=@1 ) and upper(product_series) in( select rmes_id from data_rounting_remark where pline_code=@0 and xl=@2) ";
                string sql = "where pline_code = @0 and process_code in (select process_id from vw_rel_station_process where station_code=@1 ) and upper(product_series)=@2 ";

                //string sql = "where pline_code = @0 and process_code in (select process_id from vw_rel_station_process where station_code=@1 ) and upper(product_series) in( select rmes_id from CODE_PRODUCT_SERIES where pline_code=@0 and product_series=@2) ";
                if (string.IsNullOrWhiteSpace(processcode))
                {
                    return db.Fetch<WorkProcessFileEntity>(sql, plinecode,stationcode,productseries.ToUpper());
                }
                else
                {
                    sql = sql + " and process_code = @1 ";
                    return db.Fetch<WorkProcessFileEntity>(sql, plinecode, processcode);
                }
            }
            catch
            {
                return null;
            }
        }
        //select c.file_name from rel_station_location a left join rel_location_option b   on a.pline_code = b.pline_code  and a.location_code = b.location_code left join data_option_file c   on a.pline_code = c.pline_code   and b.option_code = c.option_code  and c.series_code=@2  where a.pline_code = @0  and a.station_code = @1 ", plinecode, stationcode,seriescode);

    }
    internal class BroadcastDal : BaseDalClass
    {
        public List<BroadcastEntity> GetAll()
        {
            return db.Fetch<BroadcastEntity>("");
        }

        public BroadcastEntity GetByKey(string key)
        {
            return db.First<BroadcastEntity>("where RMES_ID=@0", key);
        }

        public BroadcastEntity GetContextByCompanyPline(string companycode, string plinermesid)
        {
            List<BroadcastEntity> be = db.Fetch<BroadcastEntity>("WHERE COMPANY_CODE=@0 AND PLINE_CODE=@1 ORDER BY RMES_ID DESC", companycode,plinermesid);
            if (be.Count > 0) return be[0];
            return null;
        }
    }
}
