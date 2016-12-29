using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rmes.DA.Entity;
using Rmes.DA.Dal;

namespace Rmes.DA.Factory
{
    public static class ItemLineSideFactory
    {
        public static List<ItemLineSideEntity> GetAll()
        {
            return new ItemLineSideDal().GetAll();
        }

        public static bool Insert(ItemLineSideEntity entity)
        {
            return new ItemLineSideDal().Insert(entity);
        }

        public static bool Update(ItemLineSideEntity entity)
        {
            return new ItemLineSideDal().Update(entity);
        }

        public static bool Delete(ItemLineSideEntity entity)
        {
            return new ItemLineSideDal().Delete(entity);
        }

        public static ItemLineSideEntity GetByID(string id)
        {
            return new ItemLineSideDal().GetByID(id);
        }
    }
}
