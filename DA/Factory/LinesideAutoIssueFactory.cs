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
/// 功能描述：线边库存操作
/// </summary>
/// 

namespace Rmes.DA.Factory
{
    public static class LinesideAutoIssueFactory
    {
        public static List<LinesideAutoIssueEntity> GetAll()
        {
            return new LinesideAutoIssueDal().GetAll();
        }

        
    }
}

