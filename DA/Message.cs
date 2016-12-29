using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rmes.DA.Base;
using Rmes.DA.Entity;
using Rmes.DA.Factory;
using Rmes.DA.Dal;

namespace Rmes.DA.Factory
{
    public static class MessageFactory
    {
        public static List<MessageEntity> GetByCompanyCodePlineCodeStationCode(string CompanyCode, string PlineCode, string StationCode)
        {
            return new MessageDal().GetByCompanyCodePlineCodeStationCode(CompanyCode, PlineCode, StationCode);
        }
    }
}
namespace Rmes.DA.Dal
{
    internal class MessageDal : BaseDalClass
    {
        public List<MessageEntity> GetByCompanyCodePlineCodeStationCode(string CompanyCode, string PlineCode, string StationCode)
        {
            return db.Fetch<MessageEntity>("WHERE COMPANY_CODE=@0 AND PLINE_CODE=@1 AND STATION_ID=@2", CompanyCode, PlineCode, StationCode);
        }
    }
}
namespace Rmes.DA.Entity
{
    [PetaPoco.TableName("VW_REL_MESSAGE_STATION")]
    public class MessageEntity : IEntity
    {

        public string COMPANY_CODE { get; set; }
        public string PLINE_CODE { get; set; }
        public string STATION_ID { get; set; }
        public string MESSAGE_ID { get; set; }
        public string MESSAGE_NAME { get; set; }
        public string HEAD_CODE { get; set; }
    }
}