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
    [PetaPoco.TableName("DATA_ISSUE_DETAIL")]
    //下面这行请自行填写关键字段并取消注释
    //[PetaPoco.PrimaryKey("RMES_ID", sequenceName = "SEQ_RMES_ID")]
    public class IssueDetailEntity : IEntity
    {
        public string RMES_ID { get; set; }
        public string COMPANY_CODE { get; set; }
        public string ISSUE_CODE { get; set; }
        public string PLINE_CODE { get; set; }
        public string LOCATION_CODE { get; set; }
        public string PROJECT_CODE { get; set; }
        public string USER_ID_STORE { get; set; }
        public string USER_ID_ISSUE { get; set; }
        public string ITEM_CODE { get; set; }
        public string ITEM_NAME { get; set; }
        public int ITEM_QTY { get; set; }
        public string VENDOR_CODE { get; set; }

        //字段名称				字段类型		长度		关键字	是否为空		中文注释
        //RMES_ID				VARCHAR2	50		False	True		RMESID
        //COMPANY_CODE			VARCHAR2	30		False	False		公司
        //ISSUE_CODE			VARCHAR2	30		False	False		发料单号
        //PLINE_CODE			VARCHAR2	30		False	False		生产线
        //LOCATION_CODE			VARCHAR2	30		False	False		工位
        //PROJECT_CODE			VARCHAR2	30		False	True		对应工程号
        //USER_ID_STORE			VARCHAR2	30		False	False		保管员
        //USER_ID_ISSUE			VARCHAR2	30		False	False		发料员
        //ITEM_CODE				VARCHAR2	30		False	False		零件代码
        //ITEM_NAME				VARCHAR2	30		False	True		零件名称
        //ITEM_QTY				NUMBER		22		False	False		数量
        //VENDOR_CODE			VARCHAR2	30		False	True		供应商
    }
}
#endregion
