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
//时间：2013/12/17
//
#endregion


#region 自动生成实体类
namespace Rmes.DA.Entity
{
	[PetaPoco.TableName("DATA_ANDON_ALERT")]
	//下面这行请自行填写关键字段并取消注释
	[PetaPoco.PrimaryKey("RMES_ID", sequenceName = "SEQ_RMES_ID")]
	public class AndonAlertEntity : IEntity
	{
		public string COMPANY_CODE{ get; set; }
		public string PLINE_CODE{ get; set; }
		public string LOCATION_CODE{ get; set; }
		public string ANDON_TYPE_CODE{ get; set; }
		public string ANDON_ALERT_CONTENT{ get; set; }
		public DateTime ANDON_ALERT_TIME{ get; set; }
		public DateTime ANDON_INPUT_TIME{ get; set; }
		public DateTime ANDON_ANSWER_TIME{ get; set; }
		public string REPORT_FLAG{ get; set; }
		public string ANSWER_FLAG{ get; set; }
		public string STOP_FLAG{ get; set; }
		public string MATERIAL_CODE{ get; set; }
		public string EMPLOYEE_CODE{ get; set; }
        public string RMES_ID { get; set; }
        public string TEAM_CODE { get; set; }

		//字段名称				字段类型		长度		关键字	是否为空		中文注释
		//COMPANY_CODE			VARCHAR2	30		False	N		公司代码
		//PLINE_CODE			VARCHAR2	30		False	N		生产线代码
		//LOCATION_CODE			VARCHAR2	30		False	N		工位代码
		//ANDON_TYPE_CODE		VARCHAR2	10		False	Y		andon呼叫类别
		//ANDON_ALERT_CONTENT	VARCHAR2	30		False	Y		andon呼叫内容
		//ANDON_ALERT_TIME		DATE		7		False	Y		andon呼叫时间
		//ANDON_INPUT_TIME		DATE		7		False	Y		andon输入时间？
		//ANDON_ANSWER_TIME		DATE		7		False	Y		andon响应时间
		//REPORT_FLAG			VARCHAR2	1		False	N		是否播报
		//ANSWER_FLAG			VARCHAR2	1		False	Y		是否得到响应
		//STOP_FLAG				VARCHAR2	1		False	N		停线标志
		//MATERIAL_CODE			VARCHAR2	50		False	Y		物料代码
		//EMPLOYEE_CODE			VARCHAR2	30		False	Y		呼叫人员代码
	}
}
#endregion
