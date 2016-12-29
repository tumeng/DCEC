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
/// 功能描述：获取工艺工序装机提示相关数据
/// </summary>
/// 
namespace Rmes.DA.Factory
{
    public static class ProcessNoteFactory
    {
        public static List<ProcessNoteEntity> GetAll()
        {
            return new ProcessNoteDal().GetAll();
        }
        public static ProcessNoteEntity GetByKey(string strID)
        {
            return new ProcessNoteDal().GetByKey(strID);
        }

        public static List<ProcessNoteEntity> GetSingleByKey(string RmesID)
        {
            return new ProcessNoteDal().GetSingleByKey(RmesID);
        }

        public static List<ProcessNoteEntity> GetByProcessProduct(string ComanyCode, string PlineCode, string ProcessCode, string ProductSeries)
        {
            return new ProcessNoteDal().GetByProcessProduct(ComanyCode, PlineCode, ProcessCode, ProductSeries);
        }
    }
}