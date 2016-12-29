using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
#region ������Rmes�����ռ�����
using Rmes.DA.Base;
using Rmes.DA.Entity;
using Rmes.DA.Dal;
using Rmes.DA.Factory;
#endregion


#region �Զ�����ʵ���๤�����ɣ�by �������Կ�������Ϣ��
//From XYJ
//ʱ�䣺2013/12/4
//
#endregion


#region �Զ�����ʵ����
namespace Rmes.DA.Entity
{
	[PetaPoco.TableName("CODE_USER")]
	//����������������д�ؼ��ֶβ�ȡ��ע��
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
		//�ֶ�����				�ֶ�����		����		�ؼ���	�Ƿ�Ϊ��		����ע��
		//COMPANY_CODE			VARCHAR2	10		True	N		��˾
		//USER_ID				VARCHAR2	30		True	N		�û�Ψһ��
		//USER_CODE				VARCHAR2	30		False	N		����
		//USER_NAME				VARCHAR2	30		False	N		����
		//USER_SEX				VARCHAR2	1		False	N		FŮʿ��M��ʿ
		//USER_PASSWORD			VARCHAR2	30		False	N		����
		//USER_OLD_PASSWORD		VARCHAR2	30		False	Y		ԭ����
		//USER_AUTHORIZED_IP	VARCHAR2	20		False	Y		��ȨIP
		//USER_MAXNUM			NUMBER		22		False	N		����¼��
		//VALID_FLAG			VARCHAR2	1		False	N		�Ƿ���Ч
		//LOCK_FLAG				NUMBER		22		False	Y		�Ƿ�������Ϊ3֤������
		//USER_EMAIL			VARCHAR2	30		False	Y		����
		//USER_TEL				VARCHAR2	30		False	Y		�绰
	}
}
#endregion
