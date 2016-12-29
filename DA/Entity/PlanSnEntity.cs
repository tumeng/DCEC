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
	[PetaPoco.TableName("DATA_PLAN_SN")]
	//����������������д�ؼ��ֶβ�ȡ��ע��
	[PetaPoco.PrimaryKey("RMES_ID", sequenceName = "SEQ_RMES_ID")]
	public class PlanSnEntity : IEntity
	{
		public string RMES_ID{ get; set; }
		public string COMPANY_CODE{ get; set; }
		public string PLINE_CODE{ get; set; }
		public string PLAN_CODE{ get; set; }
		public string SN{ get; set; }
		public string SN_FLAG{ get; set; }
        public DateTime CREATE_TIME { get; set; }

		//�ֶ�����				�ֶ�����		����		�ؼ���	�Ƿ�Ϊ��		����ע��
		//RMES_ID				VARCHAR2	30		True	N		
		//COMPANY_CODE			VARCHAR2	20		False	N		��˾
		//PLINE_CODE			VARCHAR2	20		False	N		������
		//PLAN_CODE				VARCHAR2	30		False	N		�ƻ�����
		//SN					VARCHAR2	30		False	N		��ˮ��
		//SN_FLAG				VARCHAR2	10		False	Y		�Ƿ������ߣ�Y/N)
		//TEMP02				VARCHAR2	10		False	Y		
        //CREATE_TIME			DATE		7		False	N		SN����ʱ��
	}
}
#endregion
