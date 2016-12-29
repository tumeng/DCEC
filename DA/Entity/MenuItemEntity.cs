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
//时间：2013/12/6
//
#endregion


#region 自动生成实体类
namespace Rmes.DA.Entity
{
	[PetaPoco.TableName("CODE_MENU")]
	//下面这行请自行填写关键字段并取消注释
	[PetaPoco.PrimaryKey("MENU_CODE", sequenceName = "SEQ_RMES_ID")]
	public class MenuItemEntity : IEntity
	{
		public string COMPANY_CODE{ get; set; }
		public string MENU_CODE{ get; set; }
		public string MENU_NAME{ get; set; }
		public string MENU_NAME_EN{ get; set; }
		public string MENU_CODE_FATHER{ get; set; }
		public int MENU_LEVEL{ get; set; }
		public int MENU_INDEX{ get; set; }
		public string LEAF_FLAG{ get; set; }
		public string PROGRAM_CODE{ get; set; }

		//字段名称				字段类型		长度		关键字	是否为空		中文注释
		//COMPANY_CODE			VARCHAR2	10		True	N		公司
		//MENU_CODE				VARCHAR2	30		True	N		代码
		//MENU_NAME				VARCHAR2	30		False	N		名称
		//MENU_NAME_EN			VARCHAR2	60		False	N		英文名称
		//MENU_CODE_FATHER		VARCHAR2	30		False	Y		父菜单代码
		//MENU_LEVEL			NUMBER		22		False	N		级别
		//MENU_INDEX			NUMBER		22		False	N		索引
		//LEAF_FLAG				VARCHAR2	1		False	N		是否叶子
		//PROGRAM_CODE			VARCHAR2	100		False	Y		对应程序代码
	}
}
#endregion
