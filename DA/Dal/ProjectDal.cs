using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rmes.DA.Base;
using Rmes.DA.Entity;

namespace Rmes.DA.Dal
{
    internal class ProjectDal:BaseDalClass
    {
        public bool SaveProjectDetail(ProjectDetailEntity ent1)
        {
            db.Save(ent1);
            return true;
        }
        public bool SaveProject(ProjectEntity ent1)
        {
            db.Save(ent1);
            return true;
        }
        public ProjectEntity GetByProjectcode(string projectcode)
        {
            List< ProjectEntity> list = db.Fetch<ProjectEntity>("where PROJECT_CODE=@0", projectcode);
            if (list.Count<1)
                return null;
            else
                return list[0];
        }
    }
}
