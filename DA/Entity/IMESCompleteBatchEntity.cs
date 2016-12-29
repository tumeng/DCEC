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










#region 自动生成实体类
namespace Rmes.DA.Entity
{
	[PetaPoco.TableName("IMES_DATA_COMPLETE_BATCH")]
	//下面这行请自行填写关键字段并取消注释
	//[PetaPoco.PrimaryKey("RMES_ID", sequenceName = "SEQ_RMES_ID")]
	public class IMESCompleteBatchEntity 
	{
		public string AUFNR{ get; set; }
		public string WERKS{ get; set; }
		public string MATNR{ get; set; }
		public string CHARG{ get; set; }
		public string GAMNG{ get; set; }
		public string PONSR{ get; set; }
		public string CHART{ get; set; }
		public string NAME1{ get; set; }
		public string VALUE{ get; set; }
		public string STATUS{ get; set; }

		//字段名称				字段类型		长度		关键字	是否为空		中文注释
		//AUFNR					VARCHAR2	30		False	False		生产订单号
		//WERKS					VARCHAR2	20		False	False		工厂
		//MATNR					VARCHAR2	30		False	True		生产物料编码
		//CHARG					VARCHAR2	30		False	True		批次
		//GAMNG					VARCHAR2	30		False	True		数量
		//PONSR					VARCHAR2	30		False	True		行项目号
		//CHART					VARCHAR2	30		False	True		批次特性编码
		//NAME1					VARCHAR2	30		False	True		批次特性描述
		//VALUE					VARCHAR2	30		False	True		批次特性值
		//STATUS				VARCHAR2	10		False	True		
	}
}
#endregion
