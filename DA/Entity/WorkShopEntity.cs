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
//ʱ�䣺2013/12/20
//
#endregion


#region �Զ�����ʵ����
namespace Rmes.DA.Entity
{
	[PetaPoco.TableName("CODE_WORKSHOP")]
	//����������������д�ؼ��ֶβ�ȡ��ע��
	[PetaPoco.PrimaryKey("RMES_ID", sequenceName = "SEQ_RMES_ID")]
	public class WorkShopEntity : IEntity
	{
		public string RMES_ID{ get; set; }
		public string COMPANY_CODE{ get; set; }
		public string WORKSHOP_CODE{ get; set; }
		public string WORKSHOP_NAME{ get; set; }
		public string WORKSHOP_ADDRESS{ get; set; }

		//�ֶ�����				�ֶ�����		����		�ؼ���	�Ƿ�Ϊ��		����ע��
		//RMES_ID				VARCHAR2	50		False	Y		RMES_ID
		//COMPANY_CODE			VARCHAR2	30		False	N		��˾����
		//WORKSHOP_CODE			VARCHAR2	50		True	N		��������
		//WORKSHOP_NAME			VARCHAR2	50		False	N		��������
		//WORKSHOP_ADDRESS		VARCHAR2	100		False	Y		������ַ
	}
}
#endregion
