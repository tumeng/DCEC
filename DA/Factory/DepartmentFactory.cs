using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rmes.DA.Base;
using Rmes.DA.Entity;
using Rmes.DA.Factory;
using Rmes.DA.Dal;

/// <summary>
/// 作者：caoly
/// 功能描述：获取分装站点相关数据
/// </summary>
/// 
namespace Rmes.DA.Factory
{
    public static class DepartmentFactory
    {
        public static List<DepartmentEntity> GetAll()
        {
            return new DepartmentDal().GetAll();
        }
        public static DepartmentEntity GetByKey(string strID)
        {
            return new DepartmentDal().GetByKey(strID);
        }

        public static List<DepartmentEntity> GetSingleByKey(string RmesID)
        {
            return new DepartmentDal().GetSingleByKey(RmesID);
        }
    }
}