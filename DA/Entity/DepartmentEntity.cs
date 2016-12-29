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
//时间：2013-12-11
//
#endregion

#region 自动生成实体类
namespace Rmes.DA.Entity
{
    [PetaPoco.TableName("CODE_DEPT")]
    //下面这行请自行填写关键字段并取消注释
    [PetaPoco.PrimaryKey("RMES_ID", sequenceName = "SEQ_RMES_ID")]
    public class DepartmentEntity : IEntity
    {
        public string RMES_ID { get; set; }
        public string COMPANY_CODE { get; set; }
        public string DEPT_CODE { get; set; }
        public string DEPT_NAME { get; set; }
        public string PARENT_DEPT { get; set; }
        public string DEPT_REMARK { get; set; }

        //字段名称				字段类型		长度		关键字	是否为空		中文注释
        //RMES_ID				VARCHAR2	30		False	True		
        //COMPANY_CODE			VARCHAR2	10		False	False		公司
        //DEPT_CODE				VARCHAR2	20		False	False		部门代码
        //DEPT_NAME				VARCHAR2	60		False	False		部门名称
        //PARENT_DEPT		VARCHAR2	10		False	True		上级部门代码
        //DEPT_REMARK			VARCHAR2	10		False	True		备注（职能描述）
    }
}
#endregion