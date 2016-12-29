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
	[PetaPoco.TableName("CODE_COMPANY")]
	//下面这行请自行填写关键字段并取消注释
    [PetaPoco.PrimaryKey("COMPANY_CODE", sequenceName = "SEQ_RMES_ID")]
	public class CompanyEntity : IEntity
	{
        public string RMES_ID { get; set; }
		public string COMPANY_CODE{ get; set; }
		public string COMPANY_NAME{ get; set; }
		public string COMPANY_NAME_BRIEF{ get; set; }
		public string COMPANY_NAME_EN{ get; set; }
        public string COMPANY_ADDRESS { get; set; }
		public string COMPANY_WEBSITE{ get; set; }
        public string COMPANY_REMARK { get; set; }

		//字段名称				字段类型		长度		关键字	是否为空		中文注释
		//COMPANY_CODE			VARCHAR2	30		True	N		代码
		//COMPANY_NAME			VARCHAR2	30		False	N		名称
		//COMPANY_NAME_BRIEF	VARCHAR2	30		False	N		简称
		//COMPANY_NAME_EN		VARCHAR2	30		False	N		英文
		//COMPANY_WEBSITE		VARCHAR2	30		False	N		网址
		//COMPANY_ADDRESS		VARCHAR2	50		False	Y		通讯地址
	}
}
#endregion
