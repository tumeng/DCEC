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


#region 自动生成实体类工具生成，数据库："XKMES
//From XYJ
//时间：2014-04-19
//
#endregion


#region 自动生成实体类
namespace Rmes.DA.Entity
{
    [PetaPoco.TableName("DATA_XK_BOM_EXCHANGE")]
    //下面这行请自行填写关键字段并取消注释
    [PetaPoco.PrimaryKey("RMES_ID", sequenceName = "SEQ_RMES_ID")]
    public class BomExchangeEntity : IEntity
    {
        public string RMES_ID { get; set; }
        public string PROJECT_CODE { get; set; }
        public string WORK_CODE { get; set; }
        public string ITEM_CODE_FROM { get; set; }
        public string ITEM_NAME_FROM { get; set; }
        public string ITEM_CODE_TO { get; set; }
        public string ITEM_NAME_TO { get; set; }
        public DateTime CREAT_TIME { get; set; }
        public string USER_ID { get; set; }
        public string ENABLE_FLAG { get; set; }
        public int USE_COUNT { get; set; }

        //字段名称				字段类型		长度		关键字	是否为空		中文注释
        //RMES_ID				VARCHAR2	50		False	True		RMES_ID
        //PROJECT_CODE			VARCHAR2	50		False	True		对应工程号
        //WORK_CODE				VARCHAR2	50		False	True		工作号
        //ITEM_CODE_FROM		VARCHAR2	50		False	True		原物料
        //ITEM_NAME_FROM		VARCHAR2	200		False	True		原物料名
        //ITEM_CODE_TO			VARCHAR2	50		False	True		替换物料
        //ITEM_NAME_TO			VARCHAR2	200		False	True		替换物料名
        //CREAT_TIME			DATE		7		False	True		规则创建时间
        //USER_ID				VARCHAR2	50		False	True		创建人
        //ENABLE_FLAG			VARCHAR2	10		False	True		启用标示
        //USE_COUNT				NUMBER		22		False	True		规则使用次数
    }
}
#endregion
