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
//时间：2013/12/5
//
#endregion


namespace Rmes.DA.Entity
{
	[PetaPoco.TableName("DATA_PROCESS_ROUTING")]
	//下面这行请自行填写关键字段并取消注释
	[PetaPoco.PrimaryKey("RMES_ID", sequenceName = "SEQ_RMES_ID")]
	public class ProcessRoutingEntity : IEntity
	{
		public string RMES_ID{ get; set; }
		public string PROJECT_CODE{ get; set; }
		public string PROJECT_NAME{ get; set; }
		public string WORKUNIT_CODE{ get; set; }
		public string ROUTING_CODE{ get; set; }
		public string ROUTING_NAME{ get; set; }
		public string PROCESS_CODE{ get; set; }
		public string PROCESS_NAME{ get; set; }
		public string PARENT_ROUTING_CODE{ get; set; }
		public int PROCESS_SEQ{ get; set; }
		public string ROUTING_LEVEL{ get; set; }
		public string ROUTING_IMPFLAG{ get; set; }
		public string PROCESS_IMPFLAG{ get; set; }
		public int ROUTING_CYCLE{ get; set; }
		public string ROUTING_CYCLE_UNIT{ get; set; }
        public string FLAG { get; set; }

		//字段名称				字段类型		长度		关键字	是否为空		中文注释
		//RMES_ID				VARCHAR2	50		True	N		
		//PROJECT_CODE			VARCHAR2	50		False	N		工程号或计划号，根据具体项目确定)
		//PROJECT_NAME			VARCHAR2	200		False	Y		工程名称

		//WORKUNIT_CODE			VARCHAR2	50		False	N		生产单元代码（工位其实是生产线，就是下达计划的的级别）

		//ROUTING_CODE			VARCHAR2	50		False	Y		工艺代码（工艺和工序代码只能填写其中之一）
		//ROUTING_NAME			VARCHAR2	200		False	Y		工艺名称

		//PROCESS_CODE			VARCHAR2	50		False	Y		工序代码

		//PROCESS_NAME			VARCHAR2	200		False	Y		工序名称

		//PARENT_ROUTING_CODE	VARCHAR2	50		False	Y		上级工艺代码

		//PROCESS_SEQ			NUMBER		22		False	Y		装配次序

		//ROUTING_LEVEL			VARCHAR2	50		False	Y		层级？

		//ROUTING_IMPFLAG		VARCHAR2	10		False	Y		重要工艺标识

		//PROCESS_IMPFLAG		VARCHAR2	10		False	Y		关键工序标识

		//ROUTING_CYCLE			NUMBER		22		False	Y		工时定额

		//ROUTING_CYCLE_UNIT	VARCHAR2	50		False	Y		工时定额单位

        //FLAG              	VARCHAR2	10		False	Y		工艺工序类别：A-工艺节点 B-装配工序 C-质量工序

	}
}

