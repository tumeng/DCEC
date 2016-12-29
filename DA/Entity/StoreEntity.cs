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
	[PetaPoco.TableName("DATA_STORE")]
	//下面这行请自行填写关键字段并取消注释
	//[PetaPoco.PrimaryKey("RMES_ID", sequenceName = "SEQ_RMES_ID")]
	public class StoreEntity 
	{
		public string SN{ get; set; }
		public string PLAN_CODE{ get; set; }
		public string PRODUCT_SERIES{ get; set; }
		public string ORDER_CODE{ get; set; }
		public string PRODUCT_MODEL{ get; set; }
		public string STATION_CODE{ get; set; }
		public string SHIFT_CODE{ get; set; }
		public string TEAM_CODE{ get; set; }
		public string USER_ID{ get; set; }
		public DateTime WORK_DATE{ get; set; }
		public DateTime WORK_TIME{ get; set; }
		public string QUALITY_STATUS{ get; set; }
		public string PLINE_CODE{ get; set; }
		public string COMPANY_CODE{ get; set; }
		public string TEMP01{ get; set; }
		public string TEMP02{ get; set; }
		public string TEMP03{ get; set; }
		public string PLAN_SO{ get; set; }
		public string COMPLETE_FLAG{ get; set; }
		public DateTime START_TIME{ get; set; }
		public DateTime COMPLETE_TIME{ get; set; }
	}
}
#endregion
