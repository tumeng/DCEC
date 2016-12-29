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


#region 自动生成实体类工具生成，数据库："XE
//From XYJ
//时间：2013-12-16
//
#endregion





#region 自动生成实体类
namespace Rmes.DA.Entity
{
	[PetaPoco.TableName("DATA_PLAN_STANDARD_BOM")]
	//下面这行请自行填写关键字段并取消注释
    [PetaPoco.PrimaryKey("RMES_ID", sequenceName = "SEQ_RMES_ID")]
	public class PlanStandardBOMEntity : IEntity
	{
        public string RMES_ID { get; set; }
		public string COMPANY_CODE{ get; set; }
		public string PLAN_CODE{ get; set; }
		public string PLINE_CODE{ get; set; }
		public string LOCATION_CODE{ get; set; }
		public string PROCESS_CODE{ get; set; }
		public string ITEM_SEQ { get; set; }
		public string ITEM_CODE{ get; set; }
		public string ITEM_NAME{ get; set; }
		public float ITEM_QTY{ get; set; }
		public string ITEM_CLASS_CODE{ get; set; }
		public string VENDOR_CODE{ get; set; }
		public DateTime CREATE_TIME{ get; set; }
		public string USER_CODE{ get; set; }
        public string VIRTUAL_ITEM_CODE { get; set; }
        public string WORKSHOP_CODE { get; set; }
        public string STORE_ID { get; set; }
        public string ORDER_CODE { get; set; }
        public string WORKUNIT_CODE { get; set; }
        public string ITEM_UNIT { get; set; }
        public string LINESIDE_STOCK_CODE { get; set; }
		
	}
}
#endregion
