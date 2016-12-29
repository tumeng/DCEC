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
	[PetaPoco.TableName("ISAP_DATA_PLAN_PROCESS")]
	//下面这行请自行填写关键字段并取消注释
	[PetaPoco.PrimaryKey("ID", sequenceName = "SEQ_RMES_ID")]
	public class SAPPlanProcessEntity 
	{
		public string AUFNR{ get; set; }
		public string WERKS{ get; set; }
		public string MATNR{ get; set; }
		public string VORNR{ get; set; }
		public string LTXA1{ get; set; }
		public string ARBPL{ get; set; }
		public string STEUS{ get; set; }
		public string MGVRG{ get; set; }
		public string SSAVD{ get; set; }
		public string SSAVZ{ get; set; }
		public string VGW02{ get; set; }
		public string VGE02{ get; set; }
		public string VGW03{ get; set; }
		public string VGE03{ get; set; }
		public string OPFLG{ get; set; }
		public string ID{ get; set; }
		public string BATCH{ get; set; }
		public string SERIAL{ get; set; }
		public string PRIND{ get; set; }
		public string DOCNUM{ get; set; }


        public override bool Equals(object o)
        {
            if (o == null) return false;
            if (o.GetType().Equals(this.GetType()) == false) return false;
            SAPPlanProcessEntity spe = (SAPPlanProcessEntity)o;
            if (o == null) return false;
            if (this.ID == spe.ID) return true;
            else return false;
        }

		//字段名称				字段类型		长度		关键字	是否为空		中文注释
		//AUFNR					VARCHAR2	30		False	False		生产订单号
		//WERKS					VARCHAR2	20		False	True		工厂
		//MATNR					VARCHAR2	20		False	True		生产物料编码
		//VORNR					VARCHAR2	30		False	True		工序步骤
		//LTXA1					VARCHAR2	50		False	True		工序名称
		//ARBPL					VARCHAR2	30		False	True		工作中心
		//STEUS					VARCHAR2	30		False	True		工序控制码
		//MGVRG					VARCHAR2	30		False	True		生产数量
		//SSAVD					VARCHAR2	30		False	True		工序开始日期
		//SSAVZ					VARCHAR2	30		False	True		工序开始时间
		//VGW02					VARCHAR2	30		False	True		标准机器工时
		//VGE02					VARCHAR2	30		False	True		标准机器工时单位，MIN:分钟
		//VGW03					VARCHAR2	30		False	True		标准人工工时
		//VGE03					VARCHAR2	30		False	True		标准人工工时单位，MIN:分钟
		//OPFLG					VARCHAR2	30		False	True		重启工序
		//ID					VARCHAR2	20		False	False		ID号（每记录唯一号）
		//BATCH					VARCHAR2	20		False	True		批号（任务号）
		//SERIAL				VARCHAR2	20		False	True		预备存时间戳
		//PRIND					VARCHAR2	1		False	True		读取状态
		//DOCNUM				VARCHAR2	16		False	True		
	}
}
#endregion
