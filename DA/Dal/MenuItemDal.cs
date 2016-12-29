using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rmes.DA.Base;
using Rmes.DA.Entity;

namespace Rmes.DA.Dal
{
    internal class MenuItemDal : BaseDalClass
    {
        public List<MenuItemEntity> GetMenuItemByFatherCode(object fathercode)
        {
            List<MenuItemEntity> entities = db.Fetch<MenuItemEntity>("WHERE MENU_CODE_FATHER=@0", fathercode);
            return entities;
        }
    }
}
