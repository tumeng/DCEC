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
	[PetaPoco.TableName("DATA_SN_STATION_COMPLETE")]
	//����������������д�ؼ��ֶβ�ȡ��ע��
	[PetaPoco.PrimaryKey("RMES_ID", sequenceName = "SEQ_RMES_ID")]
	public class ProductSnEntity : IEntity
	{
		public string RMES_ID{ get; set; }
		public string SN{ get; set; }
		public string COMPANY_CODE{ get; set; }
		public string PLAN_CODE{ get; set; }
		public string PLINE_CODE{ get; set; }
		public string STATION_CODE{ get; set; }
		public string COMPLETE_FLAG{ get; set; }
		public DateTime COMPLETE_TIME{ get; set; }
		public string USER_ID{ get; set; }

		//�ֶ�����				�ֶ�����		����		�ؼ���	�Ƿ�Ϊ��		����ע��
		//RMES_ID				VARCHAR2	30		False	N		
		//SN					VARCHAR2	30		False	N		SN
		//COMPANY_CODE			VARCHAR2	20		False	N		��˾
		//PLAN_CODE				VARCHAR2	30		False	N		�ƻ�����
		//PLINE_CODE			VARCHAR2	30		False	N		������
		//STATION_CODE			VARCHAR2	10		False	Y		վ����
		//COMPLETE_FLAG			VARCHAR2	10		False	Y		���״̬A������ B��Ԥ�깤 C�����깤
		//COMPLETE_TIME			DATE		7		False	Y		���ʱ��
		//USER_ID				VARCHAR2	30		False	Y		
	}
}
#endregion
