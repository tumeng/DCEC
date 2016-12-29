using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rmes.DA.Base;
using Rmes.DA.Entity;

namespace Rmes.DA.Dal
{
    internal class AlineDal : BaseDalClass
    {
        public int MW_CREATE_ALINE(string type, string CompanyCode, string PlineCode, string AlineCode, string AlineName)
        {
            return db.Execute("call MW_CREATE_ALINE(@0,@1,@2,@3,@4)", type, CompanyCode, PlineCode, AlineCode, AlineName);
        }
        
    }
}
