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


#region 自动生成实体类工具生成，数据库："tumeng
//From XYJ
//时间：2014/5/28
//
#endregion



#region 自动生成实体类
namespace Rmes.DA.Entity
{
    [PetaPoco.TableName("CODE_PROCESS")]
    //下面这行请自行填写关键字段并取消注释
    [PetaPoco.PrimaryKey("RMES_ID", sequenceName = "SEQ_RMES_ID")]
    public class ProcessEntity
    {
        public string RMES_ID { get; set; }
        public string COMPANY_CODE { get; set; }
        public string PLINE_CODE { get; set; }
        public string PROCESS_CODE { get; set; }
        public string PROCESS_CODE_SAP { get; set; }
        public string PROCESS_CODE_ORG { get; set; }
        public string PROCESS_NAME { get; set; }
        public int PROCESS_MANHOUR { get; set; }
        //public string FACTORY_CODE{ get; set; }
        public string WORKUNIT_CODE { get; set; }
        //public string PROCESS_SEQ{ get; set; }
        //public string WORKUNIT_NAME{ get; set; }
        public string WORKSHOP_CODE { get; set; }

        //字段名称				字段类型		长度		关键字	是否为空		中文注释
        //RMES_ID				VARCHAR2	30		False	True		RMESID
        //COMPANY_CODE			VARCHAR2	10		False	False		公司
        //PLINE_CODE			VARCHAR2	30		False	True		生产线
        //PROCESS_CODE			VARCHAR2	30		False	False		工序代码
        //PROCESS_NAME			VARCHAR2	200		False	False		工序名称
        //PROCESS_MANHOUR		NUMBER		22		False	True		工时 （以秒为单位）
        //FACTORY_CODE			VARCHAR2	10		False	True		工厂
        //WORKUNIT_CODE			VARCHAR2	30		False	True		工作中心编号
        //PROCESS_SEQ			VARCHAR2	10		False	True		工序顺序号
        //WORKUNIT_NAME			VARCHAR2	30		False	True		工作中心名称

    }
}
#endregion
