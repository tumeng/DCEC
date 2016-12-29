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
	[PetaPoco.TableName("CODE_COMPANY")]
	//����������������д�ؼ��ֶβ�ȡ��ע��
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

		//�ֶ�����				�ֶ�����		����		�ؼ���	�Ƿ�Ϊ��		����ע��
		//COMPANY_CODE			VARCHAR2	30		True	N		����
		//COMPANY_NAME			VARCHAR2	30		False	N		����
		//COMPANY_NAME_BRIEF	VARCHAR2	30		False	N		���
		//COMPANY_NAME_EN		VARCHAR2	30		False	N		Ӣ��
		//COMPANY_WEBSITE		VARCHAR2	30		False	N		��ַ
		//COMPANY_ADDRESS		VARCHAR2	50		False	Y		ͨѶ��ַ
	}
}
#endregion
