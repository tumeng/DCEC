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
    [PetaPoco.TableName("DATA_MESSAGE_SAP_TRANS")]
    //下面这行请自行填写关键字段并取消注释
    [PetaPoco.PrimaryKey("RMES_ID", sequenceName = "SEQ_RMES_ID")]
    public class SAPMessageTransEntity
    {
        public string RMES_ID { get; set; }
        public string MESSAGE_CODE { get; set; }
        public string MESSAGE_NAME { get; set; }
        public string HANDLE_FLAG { get; set; }
        public DateTime WORK_DATE { get; set; }
        public DateTime HANDLE_DATE { get; set; }
    }
//DATA_MESSAGE_SAP_TRANS

//RMES_ID	VARCHAR2(50)	N			
//MESSAGE_CODE	VARCHAR2(30)	N			类型类型
//MESSAGE_NAME	VARCHAR2(50)	Y			
//HANDLE_FLAG	VARCHAR2(10)	Y	'0'		处理标志（0：未处理 1：正在处理2：已处理）
//WORK_DATE	DATE	Y	sysdate		发送时间
//HANDLE_DATE	DATE	Y			处理时间
}
