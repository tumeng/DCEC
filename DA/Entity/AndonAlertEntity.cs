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
//ʱ�䣺2013/12/17
//
#endregion


#region �Զ�����ʵ����
namespace Rmes.DA.Entity
{
	[PetaPoco.TableName("DATA_ANDON_ALERT")]
	//����������������д�ؼ��ֶβ�ȡ��ע��
	[PetaPoco.PrimaryKey("RMES_ID", sequenceName = "SEQ_RMES_ID")]
	public class AndonAlertEntity : IEntity
	{
		public string COMPANY_CODE{ get; set; }
		public string PLINE_CODE{ get; set; }
		public string LOCATION_CODE{ get; set; }
		public string ANDON_TYPE_CODE{ get; set; }
		public string ANDON_ALERT_CONTENT{ get; set; }
		public DateTime ANDON_ALERT_TIME{ get; set; }
		public DateTime ANDON_INPUT_TIME{ get; set; }
		public DateTime ANDON_ANSWER_TIME{ get; set; }
		public string REPORT_FLAG{ get; set; }
		public string ANSWER_FLAG{ get; set; }
		public string STOP_FLAG{ get; set; }
		public string MATERIAL_CODE{ get; set; }
		public string EMPLOYEE_CODE{ get; set; }
        public string RMES_ID { get; set; }
        public string TEAM_CODE { get; set; }

		//�ֶ�����				�ֶ�����		����		�ؼ���	�Ƿ�Ϊ��		����ע��
		//COMPANY_CODE			VARCHAR2	30		False	N		��˾����
		//PLINE_CODE			VARCHAR2	30		False	N		�����ߴ���
		//LOCATION_CODE			VARCHAR2	30		False	N		��λ����
		//ANDON_TYPE_CODE		VARCHAR2	10		False	Y		andon�������
		//ANDON_ALERT_CONTENT	VARCHAR2	30		False	Y		andon��������
		//ANDON_ALERT_TIME		DATE		7		False	Y		andon����ʱ��
		//ANDON_INPUT_TIME		DATE		7		False	Y		andon����ʱ�䣿
		//ANDON_ANSWER_TIME		DATE		7		False	Y		andon��Ӧʱ��
		//REPORT_FLAG			VARCHAR2	1		False	N		�Ƿ񲥱�
		//ANSWER_FLAG			VARCHAR2	1		False	Y		�Ƿ�õ���Ӧ
		//STOP_FLAG				VARCHAR2	1		False	N		ͣ�߱�־
		//MATERIAL_CODE			VARCHAR2	50		False	Y		���ϴ���
		//EMPLOYEE_CODE			VARCHAR2	30		False	Y		������Ա����
	}
}
#endregion
