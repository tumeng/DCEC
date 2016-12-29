using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rmes.DA.Base;
using Rmes.DA.Entity;
using Rmes.DA.Factory;
using Rmes.DA.Dal;


///add by liuzhy 2014/03/08
namespace Rmes.DA.Factory
{
    public static class ProjectFactory
    {
        public static List<ProjectEntity> GetByWorkShop(string workshopID)
        {
            return new ProjectDal().FindBySql<ProjectEntity>("where WORKSHOP_ID=@0",workshopID);
        }

        public static bool SaveProjectDetail(ProjectDetailEntity entity)
        {
            return new ProjectDal().SaveProjectDetail(entity);
        }

        public static bool SaveProject(string ProjectCode, string ProjectName, string ProjectXilie,  string UserID)
        {
            List<WorkShopEntity> workshops = WorkShopFactory.GetUserWorkShops(UserID);
            if (workshops.Count <= 0) return false;
            ProjectEntity ent1 = new ProjectEntity()
            {
                COMPANY_CODE = workshops[0].COMPANY_CODE,
                PROJECT_CODE = ProjectCode,
                PROJECT_NAME = ProjectName,
                WORKSHOP_ID = workshops[0].RMES_ID,
                PRODUCT_SERIES = ProjectXilie,
                STATUS = "N"
            };
            try
            {
                return new ProjectDal().SaveProject(ent1);
            }
            catch(Exception ex)
            {
                Rmes.Public.ErrorHandle.EH.LASTMSG = ex.Message;
                return false;
            }
        }
        public static ProjectEntity GetByProjectCode(string ProjectCode)
        {
            return new ProjectDal().GetByProjectcode(ProjectCode);
        }
        public static List<ProjectDetailEntity> GetDetailsByProjectCode(string project_code)
        {
            return new ProjectDal().FindBySql<ProjectDetailEntity>("where PROJECT_CODE=@0",project_code);
        }
    }
}
