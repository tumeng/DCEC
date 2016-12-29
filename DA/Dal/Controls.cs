using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rmes.DA.Base;

using Rmes.DA.Entity;
using Rmes.DA.Dal;
using Rmes.DA.Factory;


namespace Rmes.DA.Factory
{
    public static class ControlFactory
    {

        public static List<ProductControlEntity> GetCurrentCompleteInfo(string CompanyCode, string PlineCode, string StationCode, string PlanCode, string SN)
        {
            return new ProductControlDal().GetCurrentCompleteInfo(CompanyCode, PlineCode, StationCode, PlanCode, SN);

        }
        public static void HandleControlComplete(string CompanyCode, string PLineCode, string StationCode, string PlanCode, string SN, string ControlID, string CompleteFlag)
        {
            new ProductControlDal().HandleControlComplete(CompanyCode, PLineCode, StationCode, PlanCode, SN, ControlID, CompleteFlag);

        }
    }

}
namespace Rmes.DA.Dal
{
    public class ProductControlDal : BaseDalClass
    {

        public List<ProductControlEntity> GetCurrentCompleteInfo(string CompanyCode, string PlineCode, string StationCode, string PlanCode, string SN)
        {
            return db.Fetch<ProductControlEntity>("where company_code=@0 and pline_code=@1 and station_code=@2", CompanyCode, PlineCode, StationCode);
        }
        public void UpdateStationFlag(string CompanyCode, string PLineCode, string StationCode, string PlanCode, string SN, string ControlID, string CompleteFlag)
        {

            string SQL = "update data_sn_control_complete set complete_flag=@0 where companycode=@1 and station_code=@2 and plan_code=@3 and sn=@4 and control_id=@5";
            db.Execute(SQL, CompanyCode, PLineCode, StationCode, PlanCode, SN, ControlID);
        }
        public int HandleControlComplete(string CompanyCode, string PLineCode, string StationCode, string PlanCode, string SN, string ControlID, string CompleteFlag)
        {
            try
            {
                db.Execute("call PL_HANDLE_CONTROL_COMPLETE(@0,@1,@2,@3,@4,@5,@6)", CompanyCode, PLineCode, StationCode, PlanCode, SN, ControlID, CompleteFlag);
                return 0;
            }
            catch
            {
                return 1;
            }

        }
    }

}
