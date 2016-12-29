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
//时间：2014/3/8
//
#endregion


#region 自动生成实体类
namespace Rmes.DA.Entity
{
	[PetaPoco.TableName("DATA_PROJECT_DETAIL")]
	//下面这行请自行填写关键字段并取消注释
	[PetaPoco.PrimaryKey("RMES_ID", sequenceName = "SEQ_RMES_ID")]
	public class ProjectDetailEntity : IEntity
	{
		public string RMES_ID{ get; set; }
        public string PROJECT_CODE { get; set; }
        public string WORK_CODE { get; set; }
        public string STATUS { get; set; }
        public string USERID { get; set; }
		public DateTime CREATE_TIME{ get; set; }
		public DateTime IMPORT_TIME{ get; set; }
        public string IMPORT_INFO { get; set; }

		//字段名称				字段类型		长度		关键字	是否为空		中文注释
		//RMES_ID				NVARCHAR2		100		True	N		rmesid
		//PROJECT_CODE			NVARCHAR2		100		False	N		工程号
		//WORK_CODE				NVARCHAR2		100		False	N		工作号
		//STATUS				NVARCHAR2		40		False	N		状态，初始为N，导入完成为Y
		//USERID				NVARCHAR2		100		False	N		创建用户
		//CREATE_TIME			DATE		7		False	N		提交时间
		//IMPORT_TIME			DATE		7		False	Y		导入完成时间
		//IMPORT_INFO			NVARCHAR2		1000		False	Y		导入完成信息
	}
}
#endregion
