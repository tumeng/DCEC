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
	[PetaPoco.TableName("IMES_DATA_PLAN_CHANGE")]
	//下面这行请自行填写关键字段并取消注释
	[PetaPoco.PrimaryKey("ID", sequenceName = "SEQ_RMES_ID")]
	public class IMESPlanChangeEntity 
	{
		public string AUFNR{ get; set; }
		public string WERKS{ get; set; }
		public string VORNR{ get; set; }
		public string STOP{ get; set; }
		public string TEXT{ get; set; }
		public string ID{ get; set; }
		public string BATCH{ get; set; }
		public string SERIAL{ get; set; }
		public string PRIND{ get; set; }
		public string DOCNUM{ get; set; }

		//字段名称				字段类型		长度		关键字	是否为空		中文注释
		//AUFNR					VARCHAR2	20		False	False		生产订单号
		//WERKS					VARCHAR2	30		False	True		工厂
		//VORNR					VARCHAR2	30		False	False		工序步骤
		//STOP					VARCHAR2	30		False	True		状态 1：暂停 2：停止
		//TEXT					VARCHAR2	30		False	True		原因
		//ID					VARCHAR2	20		False	True		唯一ID
		//BATCH					VARCHAR2	20		False	True		批号（任务号）
		//SERIAL				VARCHAR2	20		False	True		SERIAL	N	VARCHAR2(20)	Y			预备存时间戳
		//PRIND					VARCHAR2	1		False	True		读取状态
		//DOCNUM				VARCHAR2	16		False	True		
	}
}
#endregion
