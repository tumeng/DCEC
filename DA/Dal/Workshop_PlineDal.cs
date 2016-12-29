using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rmes.DA.Base;
using Rmes.DA.Entity;

namespace Rmes.DA.Dal
{
     internal class Workshop_PlineDal:BaseDalClass
    {
         public List<Workshop_PlineEntity> GetByWorkshopCode(string workshopcode)
         {
             WorkShopEntity workshop = new WorkShopDal().GetByNumber(workshopcode);
             return db.Fetch<Workshop_PlineEntity>("WHERE  WORKSHOP_CODE=@0", workshop.RMES_ID);
         }
         public List<Workshop_PlineEntity> GetByPlineCode(string pc)
         {
             return db.Fetch<Workshop_PlineEntity>("where PLINE_CODE=@0",pc);
         }

         public Workshop_PlineEntity GetByPlineCode1(string PlineCode)
         {
             return db.First<Workshop_PlineEntity>("WHERE PLINE_CODE=@0", PlineCode);
         }

    }
}
