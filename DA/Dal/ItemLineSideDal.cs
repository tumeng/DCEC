using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
#region 北自所Rmes命名空间引用
using Rmes.DA.Base;
using Rmes.DA.Entity;
using Rmes.DA.Dal;
using Rmes.DA.Factory;
#endregion

namespace Rmes.DA.Dal
{
    internal class ItemLineSideDal:BaseDalClass
    {
        public List<ItemLineSideEntity> GetAll()
        {
            return db.Fetch<ItemLineSideEntity>("");
        }

        public bool Insert(ItemLineSideEntity entity)
        {
            try
            {
                db.Insert("CODE_ITEM_LINESIDE", "RMES_ID", entity);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool Update(ItemLineSideEntity entity)
        {
            try
            {
                db.Update(entity);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool Delete(ItemLineSideEntity entity)
        {
            try
            {
                db.Delete(entity);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public ItemLineSideEntity GetByID(string id)
        {
            try
            {
                return db.First<ItemLineSideEntity>("where RMES_ID=@0", id);
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}
