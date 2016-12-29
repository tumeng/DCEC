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
//时间：2013/12/4
//
#endregion


#region 自动生成实体类
namespace Rmes.DA.Entity
{
	[PetaPoco.TableName("DATA_FAULT_CHECKLIST")]
	//下面这行请自行填写关键字段并取消注释
	[PetaPoco.PrimaryKey("RMES_ID", sequenceName = "SEQ_RMES_ID")]
	public class FaultListEntity : IEntity
	{
		public string RMES_ID{ get; set; }
		public string COMPANY_CODE{ get; set; }
		public string SN{ get; set; }
		public string FAULT_CODE{ get; set; }
		public string PLINE_CODE{ get; set; }
		public string STATION_CODE{ get; set; }
		public string TEAM_CODE{ get; set; }
		public string SHIFT_CODE{ get; set; }
		public string EMPLOYEE_CODE{ get; set; }
		public DateTime WORK_TIME{ get; set; }
		public string REPAIR_FLAG{ get; set; }
		public string DELETE_FLAG{ get; set; }

		//字段名称				字段类型		长度		关键字	是否为空		中文注释
		//RMES_ID				VARCHAR2	50		True	N		
		//COMPANY_CODE			VARCHAR2	30		False	N		公司
		//SN					VARCHAR2	30		False	N		SN
		//FAULT_CODE			VARCHAR2	30		False	N		缺陷代码
		//PLINE_CODE			VARCHAR2	30		False	N		生产线
		//STATION_CODE			VARCHAR2	30		False	N		站点
		//TEAM_CODE				VARCHAR2	30		False	N		班组
		//SHIFT_CODE			VARCHAR2	30		False	N		班次
		//EMPLOYEE_CODE			VARCHAR2	30		False	N		操作工
		//WORK_TIME				DATE		7		False	Y		
		//REPAIR_FLAG			VARCHAR2	1		False	Y		修复标记
		//DELETE_FLAG			VARCHAR2	1		False	Y		删除标记
	}
}
#endregion
