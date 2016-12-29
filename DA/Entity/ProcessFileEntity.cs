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


#region 自动生成实体类工具生成，by 北自所自控中心信息部
//From XYJ
//时间：2014/04/18
//
#endregion


#region 自动生成实体类
namespace Rmes.DA.Entity
{
    [PetaPoco.TableName("DATA_PROCESS_FILE")]
    //下面这行请自行填写关键字段并取消注释
    [PetaPoco.PrimaryKey("RMES_ID", sequenceName = "SEQ_RMES_ID")]

    public class ProcessFileEntity : IEntity
    {
        public string RMES_ID { get; set; }
        public string COMPANY_CODE { get; set; }
        public string PLINE_CODE { get; set; }
        public string PROCESS_CODE { get; set; }
        public string FILE_NAME { get; set; }
        public string PRODUCT_SERIES { get; set; }
        public string FILE_URL { get; set; }
        public string FILE_TYPE { get; set; }
        public DateTime INPUT_TIME { get; set; }
        public string INPUT_PERSON { get; set; }

        //字段名称				字段类型		长度		关键字	是否为空		中文注释
        //RMES_ID				VARCHAR2	50		True	N		
        //COMPANY_CODE			VARCHAR2	30		False	False		
        //PLINE_CODE			VARCHAR2	30		False	False		
        //PROCESS_CODE			VARCHAR2	30		False	False		
        //FILE_NAME				VARCHAR2	500		False	False		文件路径名、URL等文件来源信息
        //PRODUCT_SERIES		VARCHAR2	50		False	True		
        //FILE_URL				VARCHAR2	1000		False	True		
        //FILE_TYPE				VARCHAR2	10		False	True		文件类型A:作业指导书，B:质量检查卡 C工艺图纸
    }
}
#endregion
