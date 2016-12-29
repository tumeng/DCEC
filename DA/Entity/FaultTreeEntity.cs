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
	[PetaPoco.TableName("CODE_FAULT_TREE")]
	//下面这行请自行填写关键字段并取消注释
	[PetaPoco.PrimaryKey("RMES_ID", sequenceName = "SEQ_RMES_ID")]
	public class FaultTreeEntity : IEntity
	{
		public string RMES_ID{ get; set; }
		public string COMPANY_CODE{ get; set; }
		public string FAULT_CODE{ get; set; }
		public string FAULT_NAME{ get; set; }
		public string FAULT_CODE_FATHER{ get; set; }
		public int FAULT_LEVEL{ get; set; }
		public int FAULT_INDEX{ get; set; }
		public string FAULT_REMARK{ get; set; }

		//字段名称				字段类型		长度		关键字	是否为空		中文注释
		//RMES_ID				VARCHAR2	30		True	N		自增序列
		//COMPANY_CODE			VARCHAR2	10		False	N		公司代码
		//FAULT_CODE			VARCHAR2	30		False	N		缺陷代码，各层一致
		//FAULT_NAME			VARCHAR2	30		False	N		缺陷名称
		//FAULT_CODE_FATHER		VARCHAR2	30		False	Y		对应上层代码
		//FAULT_LEVEL			NUMBER		22		False	Y		缺陷层级
		//FAULT_INDEX			NUMBER		22		False	Y		缺陷排序
		//FAULT_REMARK			VARCHAR2	50		False	Y		对应型号
	}
}
#endregion
