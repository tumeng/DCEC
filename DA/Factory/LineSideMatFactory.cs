using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PetaPoco;
using Rmes.DA.Base;
using Rmes.DA.Entity;
using Rmes.DA.Factory;
using Rmes.DA.Dal;

/// <summary>
/// 作者：yshxia
/// 功能描述：维护线边库存
/// </summary>
/// 
namespace Rmes.DA.Factory
{
    
    public static class LineSideMatFactory
    {
        public static int MS_MODIFY_LINESIDE_MT(string type, string MaterialCode, string gysCode, string locationCode, int linesideNum, string gzdd, string yhdm)
        {
            return new LineSideMatDal().MS_MODIFY_LINESIDE_MT(type, MaterialCode, gysCode, locationCode, linesideNum,gzdd,yhdm);
        }
        public static int PL_INSERT_ATPUSTATUS(string type, string zddm, string zdmc, string ygmc, string note, string status, string ljdm, string gzdd)
        {
            return new LineSideMatDal().PL_INSERT_ATPUSTATUS(type, zddm, zdmc, ygmc, note, status, ljdm, gzdd);
        }
        
    }
    
}

