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
//ʱ�䣺2013/12/6
//
#endregion


#region �Զ�����ʵ����
namespace Rmes.DA.Entity
{
	[PetaPoco.TableName("CODE_MENU")]
	//����������������д�ؼ��ֶβ�ȡ��ע��
	[PetaPoco.PrimaryKey("MENU_CODE", sequenceName = "SEQ_RMES_ID")]
	public class MenuItemEntity : IEntity
	{
		public string COMPANY_CODE{ get; set; }
		public string MENU_CODE{ get; set; }
		public string MENU_NAME{ get; set; }
		public string MENU_NAME_EN{ get; set; }
		public string MENU_CODE_FATHER{ get; set; }
		public int MENU_LEVEL{ get; set; }
		public int MENU_INDEX{ get; set; }
		public string LEAF_FLAG{ get; set; }
		public string PROGRAM_CODE{ get; set; }

		//�ֶ�����				�ֶ�����		����		�ؼ���	�Ƿ�Ϊ��		����ע��
		//COMPANY_CODE			VARCHAR2	10		True	N		��˾
		//MENU_CODE				VARCHAR2	30		True	N		����
		//MENU_NAME				VARCHAR2	30		False	N		����
		//MENU_NAME_EN			VARCHAR2	60		False	N		Ӣ������
		//MENU_CODE_FATHER		VARCHAR2	30		False	Y		���˵�����
		//MENU_LEVEL			NUMBER		22		False	N		����
		//MENU_INDEX			NUMBER		22		False	N		����
		//LEAF_FLAG				VARCHAR2	1		False	N		�Ƿ�Ҷ��
		//PROGRAM_CODE			VARCHAR2	100		False	Y		��Ӧ�������
	}
}
#endregion
