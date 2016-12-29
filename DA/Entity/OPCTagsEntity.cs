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


namespace Rmes.DA.Entity
{
    [PetaPoco.TableName("CODE_CONFIG_OPC_TAGS")]
    //下面这行请自行填写关键字段并取消注释
    [PetaPoco.PrimaryKey("OPC_ITEM_NAME")]
    public class OPCTagsEntity : IEntity
    {
        public string OPC_ITEM_NAME { get; set; }
        public string PLC_ADDRESS { get; set; }
        public string OPC_TAG_NAME { get; set; }
        public string LOCATION_ID { get; set; }
        public string OPC_GROUP_ID { get; set; }
        public string OPC_GROUP_NAME { get; set; }
        public string ITEM_SCRIPT { get; set; }
        public string QUALITY_FLAG { get; set; }


    }
}
