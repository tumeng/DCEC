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
	[PetaPoco.TableName("CODE_FAULT_TREE")]
	//����������������д�ؼ��ֶβ�ȡ��ע��
	[PetaPoco.PrimaryKey("RMES_ID", sequenceName = "SEQ_RMES_ID")]
	public class FaultTreeEntity : IEntity
	{
		public string RMES_ID{ get; set; }
		public string COMPANY_CODE{ get; set; }
		public string FAULT_CODE{ get; set; }
		public string FAULT_NAME{ get; set; }
		public string FAULT_CODE_FATHER{ get; set; }
		public int FAULT_LEVEL{ get; set; }
		public int FAULT_INDEX{ get; set; }
		public string FAULT_REMARK{ get; set; }

		//�ֶ�����				�ֶ�����		����		�ؼ���	�Ƿ�Ϊ��		����ע��
		//RMES_ID				VARCHAR2	30		True	N		��������
		//COMPANY_CODE			VARCHAR2	10		False	N		��˾����
		//FAULT_CODE			VARCHAR2	30		False	N		ȱ�ݴ��룬����һ��
		//FAULT_NAME			VARCHAR2	30		False	N		ȱ������
		//FAULT_CODE_FATHER		VARCHAR2	30		False	Y		��Ӧ�ϲ����
		//FAULT_LEVEL			NUMBER		22		False	Y		ȱ�ݲ㼶
		//FAULT_INDEX			NUMBER		22		False	Y		ȱ������
		//FAULT_REMARK			VARCHAR2	50		False	Y		��Ӧ�ͺ�
	}
}
#endregion
