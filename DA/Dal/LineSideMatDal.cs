using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rmes.DA.Base;
using Rmes.DA.Entity;

namespace Rmes.DA.Dal
{
    internal class LineSideMatDal : BaseDalClass
    {
        public int MS_MODIFY_LINESIDE_MT(string type, string MaterialCode, string gysCode, string locationCode, int linesideNum, string gzdd, string yhdm)
        {
            return db.Execute("call MS_MODIFY_LINESIDE_MT(@0,@1,@2,@3,@4,@5,@6)", type, MaterialCode, gysCode, locationCode, linesideNum, gzdd, yhdm);
        }
        public int PL_INSERT_ATPUSTATUS(string type, string zddm, string zdmc, string ygmc, string note, string status, string ljdm, string gzdd)
        {
            return db.Execute("call PL_INSERT_ATPUSTATUS(@0,@1,@2,@3,@4,@5,@6,@7)", type, zddm, zdmc, ygmc, note, status,ljdm, gzdd);
        }
        
    }
}
