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
    public static class Workshop_PlineFactory
    {
        public static List<Workshop_PlineEntity> GetByWorkshopCode(string workshopcode)
        {
            return new Workshop_PlineDal().GetByWorkshopCode(workshopcode);
        }

        public static List<Workshop_PlineEntity> GetByPlineCode(string PlineCode)
        {
            Workshop_PlineEntity EN = new Workshop_PlineDal().GetByPlineCode1(PlineCode);
            List<Workshop_PlineEntity> List = new Workshop_PlineDal().GetByWorkshopCode(EN.WORKSHOP_CODE);
            return List;
        }


        //public static List<Workshop_PlineEntity> GetByPlineCode(string plineCode)
        //{
        //    return new Workshop_PlineDal().GetByPlineCode(plineCode);

        //}
        public static Workshop_PlineEntity GetByPline(string PlineCode)//得到车间生产线一条记录
        {
            return new Workshop_PlineDal().GetByPlineCode1(PlineCode);
        }
    }
}
