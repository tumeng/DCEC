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
	[PetaPoco.TableName("CODE_USER")]
	//下面这行请自行填写关键字段并取消注释
	[PetaPoco.PrimaryKey("USER_ID", sequenceName = "SEQ_RMES_ID")]
	public class UserEntity : IEntity
	{
		public string COMPANY_CODE{ get; set; }
		public string USER_ID{ get; set; }
		public string USER_CODE{ get; set; }
		public string USER_NAME{ get; set; }
        public string USER_NAME_EN { get; set; }
		public string USER_SEX{ get; set; }
		public string USER_PASSWORD{ get; set; }
		public string USER_OLD_PASSWORD{ get; set; }
		public string USER_AUTHORIZED_IP{ get; set; }
		public int USER_MAXNUM{ get; set; }
		public string VALID_FLAG{ get; set; }
		public string LOCK_FLAG{ get; set; }
		public string USER_EMAIL{ get; set; }
		public string USER_TEL{ get; set; }
        public string USER_QQ { get; set; }
        public string USER_WECHAT { get; set; }
        public string USER_DEPT_CODE { get; set; }
        public string USER_TYPE { get; set; }
		//字段名称				字段类型		长度		关键字	是否为空		中文注释
		//COMPANY_CODE			VARCHAR2	10		True	N		公司
		//USER_ID				VARCHAR2	30		True	N		用户唯一号
		//USER_CODE				VARCHAR2	30		False	N		代码
		//USER_NAME				VARCHAR2	30		False	N		名称
		//USER_SEX				VARCHAR2	1		False	N		F女士，M男士
		//USER_PASSWORD			VARCHAR2	30		False	N		密码
		//USER_OLD_PASSWORD		VARCHAR2	30		False	Y		原密码
		//USER_AUTHORIZED_IP	VARCHAR2	20		False	Y		授权IP
		//USER_MAXNUM			NUMBER		22		False	N		最大登录数
		//VALID_FLAG			VARCHAR2	1		False	N		是否有效
		//LOCK_FLAG				NUMBER		22		False	Y		是否锁定，为3证明锁定
		//USER_EMAIL			VARCHAR2	30		False	Y		邮箱
		//USER_TEL				VARCHAR2	30		False	Y		电话
	}
}
#endregion
