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
	[PetaPoco.TableName("DATA_SN_STATION_COMPLETE")]
	//下面这行请自行填写关键字段并取消注释
	[PetaPoco.PrimaryKey("RMES_ID", sequenceName = "SEQ_RMES_ID")]
	public class ProductSnEntity : IEntity
	{
		public string RMES_ID{ get; set; }
		public string SN{ get; set; }
		public string COMPANY_CODE{ get; set; }
		public string PLAN_CODE{ get; set; }
		public string PLINE_CODE{ get; set; }
		public string STATION_CODE{ get; set; }
		public string COMPLETE_FLAG{ get; set; }
		public DateTime COMPLETE_TIME{ get; set; }
		public string USER_ID{ get; set; }

		//字段名称				字段类型		长度		关键字	是否为空		中文注释
		//RMES_ID				VARCHAR2	30		False	N		
		//SN					VARCHAR2	30		False	N		SN
		//COMPANY_CODE			VARCHAR2	20		False	N		公司
		//PLAN_CODE				VARCHAR2	30		False	N		计划代码
		//PLINE_CODE			VARCHAR2	30		False	N		生产线
		//STATION_CODE			VARCHAR2	10		False	Y		站点编号
		//COMPLETE_FLAG			VARCHAR2	10		False	Y		完成状态A：开工 B：预完工 C：终完工
		//COMPLETE_TIME			DATE		7		False	Y		完成时间
		//USER_ID				VARCHAR2	30		False	Y		
	}
}
#endregion
