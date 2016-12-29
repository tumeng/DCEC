using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PetaPoco;

namespace Rmes.DA.Base
{
    public static class BaseControlFactory
    {
        public static List<ConfigFormEntity> GetFormConfigFromStationCode(string stationcode)
        {
            return new BaseControlDal().GetFormConfigFromStationCode(stationcode);
        }

        public static List<ConfigFormEntity> GetFormConfigFromFormID(string id)
        {
            return new BaseControlDal().GetFormConfigFromFormID(id);
        }

        public static List<ConfigFormEntity> GetFormConfigAll()
        {
            return new BaseControlDal().FindAll<ConfigFormEntity>();
        }

        public static List<ConfigControlsEntity> GetFormControlConfigByFormID(string formid)
        {
            return new BaseControlDal().FindBySql<ConfigControlsEntity>("WHERE FORMID=@0", formid);
        }

        public static bool isFormExist(string formid)
        {
            return new BaseControlDal().checkformid(formid);
        }
        public static StationFormConfigEntity GetStationFormConfig(string stationid, string formid)
        {
            List<StationFormConfigEntity> ls = new BaseControlDal().FindBySql<StationFormConfigEntity>("WHERE MAINFORM_ID=@0 AND STATION_ID=@1", formid, stationid);
            if (ls.Count > 0) return ls[0];
            return null;
        }
        public static List<StationFormConfigEntity> GetStationFromConfigByStationID(string stationid)
        {
            return new BaseControlDal().GetStationFromConfigByStationID(stationid);
        }

        public static bool SaveStationFormConfig(StationFormConfigEntity stationformconfig)
        {
            return new BaseControlDal().SaveStationMainformConfig(stationformconfig);
        }

        public static void SaveFormInfo(ConfigFormEntity enti)
        {
            new BaseControlDal().SaveConfigFormEntity(enti);
        }
        public static void SaveFormControlsInfo(ConfigControlsEntity enti)
        {
            new BaseControlDal().SaveConfigControlsEntity(enti);
        }
        public static int RemoveFormControlsInfo(ConfigControlsEntity enti)
        {
            return new BaseControlDal().Remove(enti);
        }
    }
    internal class BaseControlDal : BaseDalClass
    {
        public List<ConfigFormEntity> GetFormConfigFromStationCode(string stationcode)
        {
            return db.Fetch<ConfigFormEntity>("WHERE FORMID IN (SELECT MAINFORM_ID FROM code_config_station_mainform WHERE STATION_ID=@0)", stationcode);
        }

        public List<ConfigFormEntity> GetFormConfigFromFormID(string id)
        {
            return db.Fetch<ConfigFormEntity>("WHERE FORMID=@0", id);
        }

        public List<StationFormConfigEntity> GetStationFromConfigByStationID(string stationid)
        {
            return db.Fetch<StationFormConfigEntity>("WHERE STATION_ID=@0", stationid);
        }

        public bool checkformid(string formid)
        {
            int count = db.ExecuteScalar<int>("select count(*) from CODE_CONFIG_MAINFORM where FORMID=@0", formid);
            return count > 0;
        }

        public bool SaveStationMainformConfig(StationFormConfigEntity entity)
        {
            int retInt = -1;
            object retObj = null;
            if (db.Exists<StationFormConfigEntity>(entity.ID))
                retInt = db.Update(entity);
            else
                retObj = db.Insert(entity);
            if (retInt > 0 || retObj != null) return true;
            return false;
        }
        public void SaveConfigFormEntity(ConfigFormEntity entity)
        {
            if (db.Exists<ConfigFormEntity>(entity.ID))
                db.Update(entity);
            else
                db.Insert(entity);
        }
        public void SaveConfigControlsEntity(ConfigControlsEntity entity)
        {
            if (db.Exists<ConfigControlsEntity>(entity.ID))
                db.Update(entity);
            else
                db.Insert(entity);
        }
    }

    [PetaPoco.TableName("CODE_CONFIG_MAINFORM")]
    [PetaPoco.PrimaryKey("ID", sequenceName = "SEQ_RMES_ID")]
    public class ConfigFormEntity : IEntity
    {
        public int ID { get; set; }
        public string FORMID { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int Top { get; set; }
        public int Left { get; set; }
        public bool FullScreen { get; set; }
        public bool Resizable { get; set; }

        [PetaPoco.Column("ASSEMBLE_FILE")]
        public string AssembleFile { get; set; }

        [PetaPoco.Column("ASSEMBLE_STRING")]
        public string AssembleString { get; set; }
    }

    //CODE_CONFIG_CONTROLS
    [PetaPoco.TableName("CODE_CONFIG_CONTROLS")]
    [PetaPoco.PrimaryKey("ID", sequenceName = "SEQ_RMES_ID")]
    public class ConfigControlsEntity : IEntity
    {
        public int ID { get; set; }
        public string FORMID { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int Top { get; set; }
        public int Left { get; set; }
        public bool FullScreen { get; set; }
        public bool Resizable { get; set; }

        [PetaPoco.Column("ASSEMBLE_FILE")]
        public string AssembleFile { get; set; }

        [PetaPoco.Column("ASSEMBLE_STRING")]
        public string AssembleString { get; set; }
    }

    [PetaPoco.TableName("CODE_CONFIG_STATION_MAINFORM")]
    [PetaPoco.PrimaryKey("ID", sequenceName = "SEQ_RMES_ID")]
    public class StationFormConfigEntity : IEntity
    {
        public int ID { get; set; }
        public string STATION_ID { get; set; }
        public string MAINFORM_ID { get; set; }
        public string GROUP_ID { get; set; }
    }

}
