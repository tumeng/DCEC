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
    [PetaPoco.TableName("DATA_LINESIDEITEM_STORE2LINE")]
    //下面这行请自行填写关键字段并取消注释
    [PetaPoco.PrimaryKey("RMES_ID", sequenceName = "SEQ_RMES_ID")]

    public class ItemLineSideStore2LineEntity
    {
        public string RMES_ID { get; set; }
        public string COMPANY_CODE { get; set; }
        public string WORKSHOP { get; set; }
        public string ITEM_CODE { get; set; }
        public string ITEM_NAME { get; set; }
        public int ITEM_QTY { get; set; }
        public string T_LINESIDESTORE { get; set; }
        public string S_LINESIDESTORE { get; set; }
        public DateTime CREATE_TIME { get; set; }
        public string CREATE_USER_ID { get; set; }
        
    }
}
