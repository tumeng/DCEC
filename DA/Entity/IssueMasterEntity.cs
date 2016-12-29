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


#region 自动生成实体类工具生成，数据库："xkmes
//From XYJ
//时间：2014-01-11
//
#endregion

#region 自动生成实体类
namespace Rmes.DA.Entity
{
    [PetaPoco.TableName("DATA_ISSUE_MASTER")]
    //下面这行请自行填写关键字段并取消注释
    //[PetaPoco.PrimaryKey("RMES_ID", sequenceName = "SEQ_RMES_ID")]
    public class IssueMasterEntity : IEntity
    {
        public string COMPANY_CODE { get; set; }
        public string ISSUE_CODE { get; set; }
        public string PLINE_CODE { get; set; }
        public string PLAN_CODE { get; set; }
        public string PLAN_SO { get; set; }
        public string USER_ID { get; set; }
        public DateTime CREATE_TIME { get; set; }
        public string ISSUE_FLAG { get; set; }
        public string TEMP01 { get; set; }
        public string TEMP02 { get; set; }

        //字段名称				字段类型		长度		关键字	是否为空		中文注释
        //COMPANY_CODE			VARCHAR2	30		True	False		公司
        //ISSUE_CODE			VARCHAR2	30		True	False		发料单号
        //PLINE_CODE			VARCHAR2	30		True	False		生产线
        //PLAN_CODE				VARCHAR2	30		True	False		计划代码
        //PLAN_SO				VARCHAR2	30		False	False		计划SO
        //USER_ID				VARCHAR2	30		False	False		操作员
        //CREATE_TIME			DATE		7		False	False		生成时间
        //ISSUE_FLAG			VARCHAR2	1		False	False		是否发放完毕
        //TEMP01				VARCHAR2	30		False	True		
        //TEMP02				VARCHAR2	30		False	True		
    }
}
#endregion
