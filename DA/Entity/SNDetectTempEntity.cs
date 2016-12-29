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
//时间：2013-12-8
//
#endregion



#region 自动生成实体类
namespace Rmes.DA.Entity
{
	[PetaPoco.TableName("DATA_SN_DETECT_TEMP")]
	//下面这行请自行填写关键字段并取消注释
	[PetaPoco.PrimaryKey("RMES_ID", sequenceName = "SEQ_RMES_ID")]
	public class SNDetectTempEntity : IEntity
	{
        public string RMES_ID{ get; set; }
        public string COMPANY_CODE	{ get; set; }
        public string PLAN_CODE{ get; set; }
        public string SN { get; set; }
        public string WORKSHOP_CODE{ get; set; }
        public string PLINE_CODE { get; set; }
        public string STATION_CODE { get; set; }
        public string PROCESS_CODE{ get; set; }
        public string PROCESS_NAME{ get; set; }
        public string DETECT_MAT_CODE{ get; set; }
        public string DETECT_ITEM_SEQ{ get; set; }
        public string DETECT_ITEM_CODE{ get; set; }
        public string DETECT_ITEM_DESC{ get; set; }
        public int DETECT_QUAL_VALUE{ get; set; }
        public string DETECT_QUAN_VALUE	{ get; set; }
        public string MAX_VALUE{ get; set; }
        public string MIN_VALUE{ get; set; }
        public string DETECT_QTY{ get; set; }
        public string REMARK{ get; set; }
        public string ORDER_CODE{ get; set; }
        public string QUALITY_FLAG{ get; set; }
        public string FAULT_CODE { get; set; }
        public string USER_ID{ get; set; }
        public string DETECT_FLAG { get; set; }
        	
		
		
	}
}
#endregion
