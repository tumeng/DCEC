using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
#region 北自所Rmes命名空间引用
using Rmes.DA.Base;
using Rmes.DA.Entity;
using Rmes.DA.Dal;
using Rmes.DA.Factory;
#endregion


#region 自动生成实体类工具生成，数据库："ORCL
//From XYJ
//时间：2013-12-08
//
#endregion

namespace Rmes.DA.Dal
{
    internal class AtpuDal : BaseDalClass
    {
        public List<AtpuEntity> GetAll()
        {
            return db.Fetch<AtpuEntity>("");
        }
        public List<AtpuEntity> GetByStationCode(string scode,string plinecode)
        {
            return db.Fetch<AtpuEntity>("WHERE zdmc=@0 and gzdd=@1", scode, plinecode);
        }
    }
}
