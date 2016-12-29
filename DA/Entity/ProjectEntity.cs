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
	[PetaPoco.TableName("DATA_PROJECT")]
	//����������������д�ؼ��ֶβ�ȡ��ע��
	[PetaPoco.PrimaryKey("RMES_ID", sequenceName = "SEQ_RMES_ID")]
	public class ProjectEntity : IEntity
	{
		public string RMES_ID{ get; set; }
		public string COMPANY_CODE{ get; set; }
		public string PROJECT_CODE{ get; set; }
		public string PRODUCT_SERIES{ get; set; }
		public string PRODUCT_MODEL{ get; set; }
		public string CUSTOMER_CODE{ get; set; }
		public string CUSTOMER_NAME{ get; set; }
		public string ORDER_CODE{ get; set; }
		public string REMARK{ get; set; }
		public string PROJECT_NAME{ get; set; }
		public string STATUS{ get; set; }
		public int SOURCE_COUNT{ get; set; }
		public int DEST_COUNT{ get; set; }
		public DateTime WORK_TIME{ get; set; }
		public string WORKSHOP_ID{ get; set; }

		//�ֶ�����				�ֶ�����		����		�ؼ���	�Ƿ�Ϊ��		����ע��
		//RMES_ID				VARCHAR2	50		True	N		RMESID
		//COMPANY_CODE			VARCHAR2	10		False	N		��˾
		//PROJECT_CODE			VARCHAR2	50		False	N		���̴���
		//PRODUCT_SERIES		VARCHAR2	50		False	N		��Ʒ�ͺŴ���(ϵ�У�
		//PRODUCT_MODEL			VARCHAR2	50		False	Y		���̲�Ʒ�ڲ��ͺţ��������ã�
		//CUSTOMER_CODE			VARCHAR2	50		False	Y		���̿ͻ����
		//CUSTOMER_NAME			VARCHAR2	50		False	Y		���̿ͻ�����
		//ORDER_CODE			VARCHAR2	50		False	Y		���̶�Ӧ������
		//REMARK				VARCHAR2	500		False	Y		��ע
		//PROJECT_NAME			VARCHAR2	200		False	Y		
		//STATUS				VARCHAR2	50		False	Y		�������������N-��ʼ�����Y-���������
		//SOURCE_COUNT			NUMBER		22		False	Y		�м����������
		//DEST_COUNT			NUMBER		22		False	Y		����Ŀ�������
		//WORK_TIME				DATE		7		False	Y		��������
		//WORKSHOP_ID			VARCHAR2	50		False	Y		�������
	}
}
#endregion
