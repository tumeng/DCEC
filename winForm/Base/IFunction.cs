using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rmes.DA.Base;
using Rmes.DA.Entity;
using Rmes.DA.Factory;

namespace Rmes.WinForm.Base
{
    /// <summary>
    /// Function接口，用于扩展功能的调用。开发者：倪晓光
    /// </summary>
    public interface IFunction
    {
        void Execute(ProductInfoEntity ProductInfo,UserEntity UserInfo,CompanyEntity CompanyInfo,ProductLineEntity ProductLineInfo,StationEntity StationInfo,ShiftEntity ShiftInfo,TeamEntity TeamInfo,string IP,string WorkDate,Rmes.WinForm.Base.RMESEventArgs e);
    }
}
