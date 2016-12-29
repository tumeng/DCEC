using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rmes.DA.Base;
using Rmes.DA.Entity;
using Rmes.DA.Factory;
using Rmes.DA.Dal;

/// <summary>
/// 作者：limm
/// 功能描述：物料基础信息相关操作
/// </summary>
/// 
namespace Rmes.DA.Factory
{
    public static class ItemFactory
    {
        public static ItemEntity GetByItem(string CompanyCode, string ItemCode)
        {
            return new ItemDal().GetByItem(CompanyCode, ItemCode);
        }
        public static List<ItemEntity> GetAll()
        {
            return new ItemDal().GetAll();
        }

        public static string Check(string itemCode)
        {
            return "A";
        }
    }
}
