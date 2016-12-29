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
/// 功能描述：工作站站点工序完成确认相关数据查询及操作
/// </summary>
/// 
namespace Rmes.DA.Factory
{
    public static class SNProcessFactory
    {
        public static List<SNProcessEntity> GetAll()
        {
            return new SNProcessDal().GetAll();
        }
        public static SNProcessEntity GetByKey(string strID)
        {
            return new SNProcessDal().GetByKey(strID);
        }

        public static List<SNProcessEntity> GetSingleByKey(string RmesID)
        {
            return new SNProcessDal().GetSingleByKey(RmesID);
        }

        

    }
}