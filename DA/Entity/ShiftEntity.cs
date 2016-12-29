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
	[PetaPoco.TableName("CODE_SHIFT")]
	//����������������д�ؼ��ֶβ�ȡ��ע��
	[PetaPoco.PrimaryKey("RMES_ID", sequenceName = "SEQ_RMES_ID")]
	public class ShiftEntity : IEntity
	{
		public string RMES_ID{ get; set; }
		public string COMPANY_CODE{ get; set; }
		public string PLINE_CODE{ get; set; }
		public string SHIFT_CODE{ get; set; }
		public string SHIFT_NAME{ get; set; }
		public string BEGIN_TIME{ get; set; }
		public string END_TIME{ get; set; }
		public string IS_CROSS_DAY{ get; set; }
        public int SHIFT_MANHOUR { get; set; }
		public string TEMP01{ get; set; }

		//�ֶ�����				�ֶ�����		����		�ؼ���	�Ƿ�Ϊ��		����ע��
		//RMES_ID				VARCHAR2	30		False	Y		
		//COMPANY_CODE			VARCHAR2	10		True	N		
		//PLINE_CODE			VARCHAR2	30		False	N		
		//SHIFT_CODE			VARCHAR2	30		True	N		
		//SHIFT_NAME			VARCHAR2	30		False	N		
		//BEGIN_TIME			VARCHAR2	10		False	N		
		//END_TIME				VARCHAR2	10		False	N		
		//IS_CROSS_DAY			VARCHAR2	1		False	N		
		//ACCOUNT_TIME			NUMBER		22		False	Y		���㹤ʱ��Сʱ��
		//TEMP01				VARCHAR2	10		False	Y		
	}
}
#endregion
