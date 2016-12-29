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
	[PetaPoco.TableName("DATA_PLAN_SN")]
	//下面这行请自行填写关键字段并取消注释
	[PetaPoco.PrimaryKey("RMES_ID", sequenceName = "SEQ_RMES_ID")]
	public class PlanSnEntity : IEntity
	{
		public string RMES_ID{ get; set; }
		public string COMPANY_CODE{ get; set; }
		public string PLINE_CODE{ get; set; }
		public string PLAN_CODE{ get; set; }
		public string SN{ get; set; }
		public string SN_FLAG{ get; set; }
        public DateTime CREATE_TIME { get; set; }

		//字段名称				字段类型		长度		关键字	是否为空		中文注释
		//RMES_ID				VARCHAR2	30		True	N		
		//COMPANY_CODE			VARCHAR2	20		False	N		公司
		//PLINE_CODE			VARCHAR2	20		False	N		生产线
		//PLAN_CODE				VARCHAR2	30		False	N		计划代码
		//SN					VARCHAR2	30		False	N		流水号
		//SN_FLAG				VARCHAR2	10		False	Y		是否已上线（Y/N)
		//TEMP02				VARCHAR2	10		False	Y		
        //CREATE_TIME			DATE		7		False	N		SN生成时间
	}
}
#endregion
