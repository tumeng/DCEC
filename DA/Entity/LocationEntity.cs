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
	[PetaPoco.TableName("CODE_LOCATION")]
	//����������������д�ؼ��ֶβ�ȡ��ע��
	[PetaPoco.PrimaryKey("RMES_ID", sequenceName = "SEQ_RMES_ID")]
	public class LocationEntity : IEntity
	{
		public string RMES_ID{ get; set; }
		public string COMPANY_CODE{ get; set; }
		public string PLINE_CODE{ get; set; }
		public string LOCATION_CODE{ get; set; }
		public string LOCATION_NAME{ get; set; }
		public int LOCATION_SEQ{ get; set; }
		public int LOCATION_MANHOUR{ get; set; }

		//�ֶ�����				�ֶ�����		����		�ؼ���	�Ƿ�Ϊ��		����ע��
		//RMES_ID				VARCHAR2	30		False	Y		
		//COMPANY_CODE			VARCHAR2	10		True	N		
		//PLINE_CODE			VARCHAR2	30		False	Y		
		//LOCATION_CODE			VARCHAR2	30		True	N		
		//LOCATION_NAME			VARCHAR2	30		False	N		
		//LOCATION_SEQ			NUMBER		22		False	N		��λ���
		//LOCATION_MANHOUR		NUMBER		22		False	N		
	}
}
#endregion
