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
//时间：2014/3/8
//
#endregion


#region 自动生成实体类
namespace Rmes.DA.Entity
{
	[PetaPoco.TableName("DATA_PROJECT")]
	//下面这行请自行填写关键字段并取消注释
	[PetaPoco.PrimaryKey("RMES_ID", sequenceName = "SEQ_RMES_ID")]
	public class ProjectEntity : IEntity
	{
		public string RMES_ID{ get; set; }
		public string COMPANY_CODE{ get; set; }
		public string PROJECT_CODE{ get; set; }
		public string PRODUCT_SERIES{ get; set; }
		public string PRODUCT_MODEL{ get; set; }
		public string CUSTOMER_CODE{ get; set; }
		public string CUSTOMER_NAME{ get; set; }
		public string ORDER_CODE{ get; set; }
		public string REMARK{ get; set; }
		public string PROJECT_NAME{ get; set; }
		public string STATUS{ get; set; }
		public int SOURCE_COUNT{ get; set; }
		public int DEST_COUNT{ get; set; }
		public DateTime WORK_TIME{ get; set; }
		public string WORKSHOP_ID{ get; set; }

		//字段名称				字段类型		长度		关键字	是否为空		中文注释
		//RMES_ID				VARCHAR2	50		True	N		RMESID
		//COMPANY_CODE			VARCHAR2	10		False	N		公司
		//PROJECT_CODE			VARCHAR2	50		False	N		工程代号
		//PRODUCT_SERIES		VARCHAR2	50		False	N		产品型号代码(系列）
		//PRODUCT_MODEL			VARCHAR2	50		False	Y		工程产品内部型号（具体配置）
		//CUSTOMER_CODE			VARCHAR2	50		False	Y		工程客户编号
		//CUSTOMER_NAME			VARCHAR2	50		False	Y		工程客户名称
		//ORDER_CODE			VARCHAR2	50		False	Y		工程对应订单号
		//REMARK				VARCHAR2	500		False	Y		备注
		//PROJECT_NAME			VARCHAR2	200		False	Y		
		//STATUS				VARCHAR2	50		False	Y		工程数据情况：N-初始情况，Y-导入已完成
		//SOURCE_COUNT			NUMBER		22		False	Y		中间库数据数量
		//DEST_COUNT			NUMBER		22		False	Y		导入目标库数量
		//WORK_TIME				DATE		7		False	Y		导入日期
		//WORKSHOP_ID			VARCHAR2	50		False	Y		车间代码
	}
}
#endregion
