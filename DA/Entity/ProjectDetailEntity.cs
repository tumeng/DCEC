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
//ʱ�䣺2014/3/8
//
#endregion


#region �Զ�����ʵ����
namespace Rmes.DA.Entity
{
	[PetaPoco.TableName("DATA_PROJECT_DETAIL")]
	//����������������д�ؼ��ֶβ�ȡ��ע��
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

		//�ֶ�����				�ֶ�����		����		�ؼ���	�Ƿ�Ϊ��		����ע��
		//RMES_ID				NVARCHAR2		100		True	N		rmesid
		//PROJECT_CODE			NVARCHAR2		100		False	N		���̺�
		//WORK_CODE				NVARCHAR2		100		False	N		������
		//STATUS				NVARCHAR2		40		False	N		״̬����ʼΪN���������ΪY
		//USERID				NVARCHAR2		100		False	N		�����û�
		//CREATE_TIME			DATE		7		False	N		�ύʱ��
		//IMPORT_TIME			DATE		7		False	Y		�������ʱ��
		//IMPORT_INFO			NVARCHAR2		1000		False	Y		���������Ϣ
	}
}
#endregion
