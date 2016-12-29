using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rmes.DA.Base;
using Rmes.DA.Entity;
using Rmes.DA.Factory;
using Rmes.DA.Dal;


namespace Rmes.DA.Factory
{
    public static class ProductFactory
    {
        public static ProductEntity GetByCompanyCodeSN(string CompanyCode, string SN)
        {
            return new ProductDal().GetByCompanyCodeSN(CompanyCode, SN);
        }
        public static List<ProductSnEntity> GetCurrentSN(string CompanyCode, string PlineCode, string StationCode)
        {
            return new ProductDal().GetCurrentSN(CompanyCode, PlineCode, StationCode);
        }

        public static void UpdateStationFlag(string CompanyCode, string PlineCode, string StationCode, string PlanCode, string SN, string CompleteFlag)
        {
            new ProductDal().UpdateStationFlag(CompanyCode, PlineCode, StationCode, PlanCode, SN, CompleteFlag);
        }

        public static void HandleStationComplete(string CompanyCode, string PlineCode, string StationCode, string PlanCode, string SN, string CompleteFlag)
        {
            new ProductDal().HandleStationComplete(CompanyCode, PlineCode, StationCode, PlanCode, SN, CompleteFlag);

        }

    }
}
namespace Rmes.DA.Dal
{
    internal class ProductDal : BaseDalClass
    {
        public ProductEntity GetByCompanyCodeSN(string CompanyCode, string SN)
        {
            return db.First<ProductEntity>("WHERE COMPANY_CODE=@0 AND SN=@1", CompanyCode, SN);
        }
        public List<ProductSnEntity> GetCurrentSN(string CompanyCode, string PlineCode, string StationCode)
        {
            return db.Fetch<ProductSnEntity>("where company_code=@0 and pline_code=@1 and station_code=@2 and complete_flag in('A','B')", CompanyCode, PlineCode, StationCode);
        }
        public void UpdateStationFlag(string CompanyCode, string PlineCode, string StationCode, string PlanCode, string SN, string CompleteFlag)
        {

        }
        public void HandleStationComplete(string CompanyCode, string PlineCode, string StationCode, string PlanCode, string SN, string CompleteFlag)
        {
            string SQL = "update data_sn_station_complete set complete_flag=@0 where company_code=@1 and pline_code=@2 and station_code=@3" +
                        "and plan_code=@4 and sn=@5";
            db.Execute(SQL, CompanyCode, PlineCode, StationCode, PlanCode, SN, CompleteFlag);


            db.Execute("call PL_HANDLE_STATION_COMPLETE(@0,@1,@2,@3,@4,@5)", CompanyCode, PlineCode, StationCode, PlanCode, SN, CompleteFlag);
        }

    }
}
namespace Rmes.DA.Entity
{
    [PetaPoco.TableName("VW_DATA_PLAN_SN_UNITNO")]
    public class ProductEntity : IEntity
    {
        public string COMPANY_CODE { get; set; }
        public string PLINE_CODE { get; set; }
        public string PLAN_ID { get; set; }
        public string ORDER_ID { get; set; }
        public string PLAN_CODE { get; set; }
        public string PLAN_MODEL { get; set; }
        public string CUSTOMER_NAME { get; set; }
        public string NOTE_MARK { get; set; }
        public string SO_DESCRIPTION { get; set; }
        public string SN { get; set; }
        public string UNIT_NO { get; set; }
    }
}