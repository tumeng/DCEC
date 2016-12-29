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
	[PetaPoco.TableName("DATA_FAULT_CHECKLIST")]
	//����������������д�ؼ��ֶβ�ȡ��ע��
	[PetaPoco.PrimaryKey("RMES_ID", sequenceName = "SEQ_RMES_ID")]
	public class FaultListEntity : IEntity
	{
		public string RMES_ID{ get; set; }
		public string COMPANY_CODE{ get; set; }
		public string SN{ get; set; }
		public string FAULT_CODE{ get; set; }
		public string PLINE_CODE{ get; set; }
		public string STATION_CODE{ get; set; }
		public string TEAM_CODE{ get; set; }
		public string SHIFT_CODE{ get; set; }
		public string EMPLOYEE_CODE{ get; set; }
		public DateTime WORK_TIME{ get; set; }
		public string REPAIR_FLAG{ get; set; }
		public string DELETE_FLAG{ get; set; }

		//�ֶ�����				�ֶ�����		����		�ؼ���	�Ƿ�Ϊ��		����ע��
		//RMES_ID				VARCHAR2	50		True	N		
		//COMPANY_CODE			VARCHAR2	30		False	N		��˾
		//SN					VARCHAR2	30		False	N		SN
		//FAULT_CODE			VARCHAR2	30		False	N		ȱ�ݴ���
		//PLINE_CODE			VARCHAR2	30		False	N		������
		//STATION_CODE			VARCHAR2	30		False	N		վ��
		//TEAM_CODE				VARCHAR2	30		False	N		����
		//SHIFT_CODE			VARCHAR2	30		False	N		���
		//EMPLOYEE_CODE			VARCHAR2	30		False	N		������
		//WORK_TIME				DATE		7		False	Y		
		//REPAIR_FLAG			VARCHAR2	1		False	Y		�޸����
		//DELETE_FLAG			VARCHAR2	1		False	Y		ɾ�����
	}
}
#endregion
