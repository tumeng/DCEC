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


#region 自动生成实体类工具生成，数据库："orcl
//From XYJ
//时间：2014/7/24
//
#endregion




#region 自动生成实体类
namespace Rmes.DA.Entity
{
	[PetaPoco.TableName("ISAP_DATA_PLAN")]
	//下面这行请自行填写关键字段并取消注释
	[PetaPoco.PrimaryKey("ID", sequenceName = "SEQ_RMES_ID")]
	public class SAPPlanEntity 
	{
		public string AUFNR{ get; set; }
		public string AUART{ get; set; }
		public string ADESC{ get; set; }
		public string WERKS{ get; set; }
		public string MATNR{ get; set; }
		public string GAMNG{ get; set; }
		public string GMEIN{ get; set; }
		public string GSTRS{ get; set; }
		public string GSUZS{ get; set; }
		public string GLTRS{ get; set; }
		public string GLUZS{ get; set; }
		public string FEVOR{ get; set; }
		public string FNAME{ get; set; }
		public string DISPO{ get; set; }
		public string DNAME{ get; set; }
		public string PROJN{ get; set; }
		public string CHARG{ get; set; }
		public string PRUEFLOS{ get; set; }
		public string LGORT{ get; set; }
		public string TEXT1{ get; set; }
		public string CUSER{ get; set; }
		public string CNAME{ get; set; }
		public string CDATE{ get; set; }
		public string ORFLG{ get; set; }
		public string ID{ get; set; }
		public string BATCH{ get; set; }
		public string SERIAL{ get; set; }
		public string PRIND{ get; set; }
		public string DOCNUM{ get; set; }

		//字段名称				字段类型		长度		关键字	是否为空		中文注释
		//AUFNR					VARCHAR2	20		False	False		生产订单号
		//AUART					VARCHAR2	30		False	False		生产订类型 ZP01：标准生产订单

		//ADESC					VARCHAR2	100		False	False		订单类型描述
		//WERKS					VARCHAR2	30		False	True		工厂8101:园区 8102:基地
		//MATNR					VARCHAR2	30		False	True		生产物料编码
		//GAMNG					VARCHAR2	30		False	False		生产订单数量
		//GMEIN					VARCHAR2	30		False	True		单位
		//GSTRS					VARCHAR2	30		False	True		生产订单开始日期（基本排产）
		//GSUZS					VARCHAR2	30		False	True		生产订单开始时间（基本排产）
		//GLTRS					VARCHAR2	30		False	True		生产订单完成日期（基本排产）
		//GLUZS					VARCHAR2	30		False	True		生产订单完成时间（基本排产）
		//FEVOR					VARCHAR2	30		False	True		生产管理员编码
		//FNAME					VARCHAR2	30		False	True		生产管理员名称
		//DISPO					VARCHAR2	30		False	True		MRP计划员编码
		//DNAME					VARCHAR2	30		False	True		MRP计划员名称
		//PROJN					VARCHAR2	30		False	True		WBS要素（项目合同号）
		//CHARG					VARCHAR2	30		False	True		收货批次
		//PRUEFLOS				VARCHAR2	30		False	True		检验批
		//LGORT					VARCHAR2	30		False	True		入库地点
		//TEXT1					VARCHAR2	255		False	True		生产订单抬头文本
		//CUSER					VARCHAR2	30		False	True		创建者编码
		//CNAME					VARCHAR2	30		False	True		创建者名称
		//CDATE					VARCHAR2	30		False	True		创建日期
		//ORFLG					VARCHAR2	10		False	True		重启订单
		//ID					VARCHAR2	20		False	False		ID号（每记录唯一号）
		//BATCH					VARCHAR2	20		False	True		批号（任务号）
		//SERIAL				VARCHAR2	20		False	True		预备存时间戳
		//PRIND					VARCHAR2	1		False	True		读取状态
		//DOCNUM				VARCHAR2	16		False	True		
	}
}
#endregion
