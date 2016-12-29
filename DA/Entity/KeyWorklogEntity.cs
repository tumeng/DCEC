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
    [PetaPoco.TableName("DATA_KEY_WORKLOG")]
    //下面这行请自行填写关键字段并取消注释
    [PetaPoco.PrimaryKey("RMES_ID", sequenceName = "SEQ_RMES_ID")]
    public class KeyWorklogEntity : IEntity
    {
        public string RMES_ID { get; set; }
        public string WORK_TYPE { get; set; }
        public string CONTENT_LOG1 { get; set; }
        public string CONTENT_LOG2 { get; set; }
        public string CONTENT_LOG3 { get; set; }
        public DateTime CREATE_TIME { get; set; }
        public string USER_ID { get; set; }
        public string USER_IP { get; set; }
        public string DELETE_FLAG { get; set; }
        public int AFFECT_ROWS { get; set; }
        

        //字段名称				字段类型		长度		关键字	是否为空		中文注释
        //RMES_ID				VARCHAR2	50		False	True		RMES_ID
        //WORK_TYPE				VARCHAR2	50		False	True		用户筛选的类别：直接汉字写，例如：物料替换
        //CONTENT_LOG1			VARCHAR2	500		False	True		操作1
        //CONTENT_LOG2			VARCHAR2	500		False	True		操作2
        //CONTENT_LOG3			VARCHAR2	500		False	True		操作3
        //CREATE_TIME			DATE		7		False	True		创建时间
        //USER_ID				VARCHAR2	50		False	True		操作人
        //USER_IP				VARCHAR2	50		False	True		IP地址
        //DELETE_FLAG			VARCHAR2	10		False	True		删除标记（用于控制显示，实际日志不允许删除）默认显示：N；删除：D
        //AFFECT_ROWS			NUMBER		22		False	True		受影响行数
    }
}
#endregion