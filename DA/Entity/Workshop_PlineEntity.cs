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
//时间：2013-12-27
//
#endregion


#region 自动生成实体类
namespace Rmes.DA.Entity
{
	[PetaPoco.TableName("REL_WORKSHOP_PLINE")]
	//下面这行请自行填写关键字段并取消注释
	//[PetaPoco.PrimaryKey("RMES_ID", sequenceName = "SEQ_RMES_ID")]
    public class Workshop_PlineEntity : IEntity
	{
		public string COMPANY_CODE{ get; set; }
		public string WORKSHOP_CODE{ get; set; }
		public string PLINE_CODE{ get; set; }
		public string TEMP01{ get; set; }
		public string TEMP02{ get; set; }

		//字段名称				字段类型		长度		关键字	是否为空		中文注释
		//COMPANY_CODE			VARCHAR2	50		False	False		公司
		//WORKSHOP_CODE			VARCHAR2	50		False	False		工厂
		//PLINE_CODE			VARCHAR2	50		False	False		生产线
		//TEMP01				VARCHAR2	10		False	True		
		//TEMP02				VARCHAR2	10		False	True		
	}
}
#endregion
