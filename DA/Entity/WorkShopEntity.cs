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
//时间：2013/12/20
//
#endregion


#region 自动生成实体类
namespace Rmes.DA.Entity
{
	[PetaPoco.TableName("CODE_WORKSHOP")]
	//下面这行请自行填写关键字段并取消注释
	[PetaPoco.PrimaryKey("RMES_ID", sequenceName = "SEQ_RMES_ID")]
	public class WorkShopEntity : IEntity
	{
		public string RMES_ID{ get; set; }
		public string COMPANY_CODE{ get; set; }
		public string WORKSHOP_CODE{ get; set; }
		public string WORKSHOP_NAME{ get; set; }
		public string WORKSHOP_ADDRESS{ get; set; }

		//字段名称				字段类型		长度		关键字	是否为空		中文注释
		//RMES_ID				VARCHAR2	50		False	Y		RMES_ID
		//COMPANY_CODE			VARCHAR2	30		False	N		公司代码
		//WORKSHOP_CODE			VARCHAR2	50		True	N		工厂代码
		//WORKSHOP_NAME			VARCHAR2	50		False	N		工厂名称
		//WORKSHOP_ADDRESS		VARCHAR2	100		False	Y		工厂地址
	}
}
#endregion
