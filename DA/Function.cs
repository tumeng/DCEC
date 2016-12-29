using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rmes.DA.Base;
using Rmes.DA.Entity;
using Rmes.DA.Factory;
using Rmes.DA.Dal;

/// <summary>
/// 功能实体类，开发者：倪晓光
/// </summary>

namespace Rmes.DA.Factory
{
    public static class FuntionFactory
    {
        /// <summary>
        /// 通过站点和消息头获得所有要执行的功能
        /// </summary>
        /// <param name="StationID"></param>
        /// <param name="HeadCode"></param>
        /// <returns></returns>
        public static List<FunctionEntity> GetByStationHeadCode(string StationID, string HeadCode)
        {
            return new FuntionDal().GetByStationHeadCode(StationID, HeadCode);
        }
    }
}
namespace Rmes.DA.Dal
{
    internal class FuntionDal : BaseDalClass
    {
        /// <summary>
        /// 通过站点和消息头获得所有要执行的功能
        /// </summary>
        /// <param name="StationID"></param>
        /// <param name="HeadCode"></param>
        /// <returns></returns>
        public List<FunctionEntity> GetByStationHeadCode(string StationID, string HeadCode)
        {
            return db.Fetch<FunctionEntity>("WHERE STATION_CODE=@0 AND HEAD_CODE=@1", StationID, HeadCode);
        }
    }
}
namespace Rmes.DA.Entity
{
    [PetaPoco.TableName("VW_REL_FUNCTION_STATION")]
    public class FunctionEntity : IEntity
    {
        public string STATION_CODE { get; set; }
        public string FUNCTION_ID { get; set; }
        public string FUNCTION_NAME { get; set; }
        public string HEAD_CODE { get; set; }
        public string ASSEMBLE_FILE { get; set; }
    }
}