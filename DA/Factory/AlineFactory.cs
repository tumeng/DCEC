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
/// 作者：徐莹
/// 功能描述：获取计划相关数据
/// </summary>
/// 
namespace Rmes.DA.Factory
{
    public class ATPU_ACROSSLINEFactory : BaseDalClass
    {

    }
    public static class AlineFactory
    {
        public static int MW_CREATE_ALINE(string type, string CompanyCode, string PlineCode, string AlineCode, string AlineName)
        {
            return new AlineDal().MW_CREATE_ALINE(type, CompanyCode, PlineCode, AlineCode, AlineName);
        }
        
    }
    
}

