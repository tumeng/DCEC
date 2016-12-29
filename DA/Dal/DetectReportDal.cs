using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PetaPoco;
using Rmes.DA.Entity;

namespace Rmes.DA.Dal
{
    public class DetectReportDal : Base.BaseDalClass
    {
        /// <summary>
        /// 函数说明：获取所有记录
        /// </summary>
        /// <returns></returns>
        public List<DetectReportEntity> GetAll()
        {
            return db.Fetch<DetectReportEntity>("");
        }
        public List<DetectReportEntity> GetByPlineAndDate(string pCompanyCode, string pPlineCode, string pFromDate, string pToDate)
        {
            string SQL = "where company_code=@0 and pline_code = @1 and create_date between to_date(@2,'yyyymmdd') " +
                         "and to_date(@3,'yyyymmdd')";
            List<DetectReportEntity> lst = db.Fetch<DetectReportEntity>(SQL, pCompanyCode, pPlineCode, pFromDate, pToDate);
            if (lst.Count > 0) return lst;
            else return null;
        }
        
        public void UpdateRecord(DetectReportEntity ent)
        {
            db.Update(ent);
            //db.Insert(ent);
            //}
        }
        public void InsertRecord(DetectReportEntity ent)
        {

            db.Insert(ent);

        }
        public DetectReportEntity GetByKey(string RmesID)
        {
            List<DetectReportEntity> ent = db.Fetch<DetectReportEntity>("where rmes_id=@0", RmesID);
            if (ent.Count == 0) return null;
            return ent.First<DetectReportEntity>();
        }

    }
}
