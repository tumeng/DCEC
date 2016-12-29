using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rmes.DA.Base;
using Rmes.DA.Entity;

namespace Rmes.DA.Dal
{
    internal class ProductLineDal : BaseDalClass
    {
        public List<ProductLineEntity> GetAll()
        {
            return db.Fetch<ProductLineEntity>("");
        }
        public List<ProductLineEntity> GetAllSUB(string dep)
        {
            return db.Fetch<ProductLineEntity>("where PLINE_TYPE_CODE=@0", dep);
        }
        

        public List<ProductLineEntity> GetByDep(string dep)
        {
            return db.Fetch<ProductLineEntity>("where DEPT_CODE=@0",dep);
        }

        public ProductLineEntity GetByKey(string productlinecode)
        {
            return db.First<ProductLineEntity>("WHERE RMES_ID=@0", productlinecode);
        }
        public ProductLineEntity GetByStationCode(string stationcode)
        {
            return db.First<ProductLineEntity>("WHERE RMES_ID in ( select pline_code from code_station where station_code=@0 )", stationcode);
        }
        public List<ProductLineEntity> GetByCompanyCode(string companycode)
        {
            return db.Fetch<ProductLineEntity>("WHERE COMPANY_CODE=@0", companycode);
        }
        public List<ProductLineEntity> GetByUserID(string userid, string programcode) //modify by thl 201760728 生产线权限增加程序代码筛选
        {
            return db.Fetch<ProductLineEntity>("where pline_code in (select pline_code from vw_user_role_program where user_id=@0 and program_code=@1) ORDER BY RMES_ID", userid,programcode);
        }
        public List<ProductLineEntity> GetByUserIDCS(string userid,string ustationcode) //modify by thl 201760728 现场工作站生产线权限根据站点员工定义表获取
        {
            return db.Fetch<ProductLineEntity>("where rmes_id in (select pline_code from REL_STATION_USER where user_id=@0 and station_code=@1) ORDER BY pline_code", userid,ustationcode);
        }
        public List<RELStationEntity> GetByUS(string ustationcode) //modify by thl 201760728 现场工作站生产线权限根据站点员工定义表获取
        {
            return db.Fetch<RELStationEntity>("where station_code=@0 ", ustationcode);
        }
        public List<RELStationEntity> GetByUS1(string userid, string ustationcode) //modify by thl 201760728 现场工作站生产线权限根据站点员工定义表获取
        {
            return db.Fetch<RELStationEntity>("where user_id=@0 and station_code=@1 ", userid,ustationcode);
        }
        public List<ProductLineEntity> GetByUserIDSub(string userid,string programcode)
        {
            return db.Fetch<ProductLineEntity>("where pline_code in (select pline_code from vw_user_role_program where user_id=@0 and program_code=@1) and PLINE_TYPE_CODE='B' ORDER BY RMES_ID", userid, programcode);
        }
        public List<ProductLineEntity> GetByIDs(string[] ids)
        {
            try
            {
                return db.Fetch<ProductLineEntity>("where RMES_ID in (@i) order by RMES_ID", new { i = ids });
            }
            catch
            {
                return null;
            }
        }
        public string GetWorkdate(string company_code, string plinecode, string shiftcode)
        {
            try
            {
                return db.First<string>("select FUNC_GET_WORK_DATE(@0,@1,@2) from dual  ", company_code, plinecode, shiftcode);
            }
            catch
            {
                return DateTime.Now.ToString("yyyy-MM-dd");
            }
        }
    }
}
