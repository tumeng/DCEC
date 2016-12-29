using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rmes.DA.Base;
using Rmes.DA.Dal;
using Rmes.DA.Entity;

namespace Rmes.DA.Factory
{
    public static class MenuItemFactory
    {
        public static List<MenuItemEntity> GetAll()
        {
            return new MenuItemDal().FindAll<MenuItemEntity>();
        }
        public static List<MenuItemEntity> GetByCompany(string companycode)
        {
            return new MenuItemDal().FindBySql<MenuItemEntity>(" WHERE COMPANY_CODE=@0 ORDER BY MENU_INDEX ASC",companycode);
        }
        public static object AddNew(MenuItemEntity entity)
        {
            return new MenuItemDal().Insert(entity);
        }
        public static int Remove(MenuItemEntity entity)
        {
            return RemoveByKey(entity.MENU_CODE);
        }
        public static int RemoveByKey(object MenuItemCode)
        {
            MenuItemDal dal = new MenuItemDal();
            List<MenuItemEntity> sonItems = dal.GetMenuItemByFatherCode(MenuItemCode);
            if (sonItems.Count > 0)
            {
                Rmes.Public.ErrorHandle.EH.LASTMSG = "该菜单项还有子节点，无法直接删除。请先删除子节点";
                return -1;
            }
           return  dal.RemoveByKey<MenuItemEntity>(MenuItemCode);
        }
        public static int Update(MenuItemEntity entity)
        {
            return new MenuItemDal().Update(entity);
        }
    }
}
