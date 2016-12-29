using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PetaPoco;

#region 北自所Rmes命名空间引用
using Rmes.DA.Base;
using Rmes.DA.Entity;
using Rmes.DA.Dal;
using Rmes.DA.Factory;
#endregion


namespace Rmes.DA.Entity
{
    [PetaPoco.TableName("DATA_MESSAGE_OPCNSTATION")]
    //下面这行请自行填写关键字段并取消注释
    [PetaPoco.PrimaryKey("RMES_ID", sequenceName = "SEQ_RMES_ID")]
    public class OPCMessageEntity : IEntity
    {
        public string RMES_ID { get; set; }
        
        public string MESSAGE_CODE { get; set; }
        public string MESSAGE_VALUE { get; set; }
        public string FROM_ID { get; set; }
        public string TO_ID { get; set; }
        public string MESSAGE_FLAG { get; set; }
        public DateTime SEND_TIME { get; set; }
        public DateTime READ_TIME { get; set; }
        public DateTime REPLY_TIME { get; set; }
    }
    [PetaPoco.TableName("CODE_CONFIG_OPC_STATIONS")]
    public class OPCStationEntity : IEntity
    {
        public string RMES_ID { get; set; }
        public string COMPANY_CODE { get; set; }
        public string PLINE_CODE { get; set; }
        public string LOCATION_CODE { get; set; }
        public string IP_ADDRESS { get; set; }
        public string OPC_GROUP_NAME { get; set; }
        public string HEART_LIVE_ADDRESS { get; set; }
        public string STATION_CODE { get; set; }
        public string DETECT_DATA { get; set; }
    }
}
