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


#region �Զ�����ʵ���๤�����ɣ����ݿ⣺"XE
//From XYJ
//ʱ�䣺2013-12-27
//
#endregion


#region �Զ�����ʵ����
namespace Rmes.DA.Entity
{
	[PetaPoco.TableName("REL_WORKSHOP_PLINE")]
	//����������������д�ؼ��ֶβ�ȡ��ע��
	//[PetaPoco.PrimaryKey("RMES_ID", sequenceName = "SEQ_RMES_ID")]
    public class Workshop_PlineEntity : IEntity
	{
		public string COMPANY_CODE{ get; set; }
		public string WORKSHOP_CODE{ get; set; }
		public string PLINE_CODE{ get; set; }
		public string TEMP01{ get; set; }
		public string TEMP02{ get; set; }

		//�ֶ�����				�ֶ�����		����		�ؼ���	�Ƿ�Ϊ��		����ע��
		//COMPANY_CODE			VARCHAR2	50		False	False		��˾
		//WORKSHOP_CODE			VARCHAR2	50		False	False		����
		//PLINE_CODE			VARCHAR2	50		False	False		������
		//TEMP01				VARCHAR2	10		False	True		
		//TEMP02				VARCHAR2	10		False	True		
	}
}
#endregion
