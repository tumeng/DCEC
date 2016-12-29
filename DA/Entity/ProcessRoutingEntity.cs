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
//ʱ�䣺2013/12/5
//
#endregion


namespace Rmes.DA.Entity
{
	[PetaPoco.TableName("DATA_PROCESS_ROUTING")]
	//����������������д�ؼ��ֶβ�ȡ��ע��
	[PetaPoco.PrimaryKey("RMES_ID", sequenceName = "SEQ_RMES_ID")]
	public class ProcessRoutingEntity : IEntity
	{
		public string RMES_ID{ get; set; }
		public string PROJECT_CODE{ get; set; }
		public string PROJECT_NAME{ get; set; }
		public string WORKUNIT_CODE{ get; set; }
		public string ROUTING_CODE{ get; set; }
		public string ROUTING_NAME{ get; set; }
		public string PROCESS_CODE{ get; set; }
		public string PROCESS_NAME{ get; set; }
		public string PARENT_ROUTING_CODE{ get; set; }
		public int PROCESS_SEQ{ get; set; }
		public string ROUTING_LEVEL{ get; set; }
		public string ROUTING_IMPFLAG{ get; set; }
		public string PROCESS_IMPFLAG{ get; set; }
		public int ROUTING_CYCLE{ get; set; }
		public string ROUTING_CYCLE_UNIT{ get; set; }
        public string FLAG { get; set; }

		//�ֶ�����				�ֶ�����		����		�ؼ���	�Ƿ�Ϊ��		����ע��
		//RMES_ID				VARCHAR2	50		True	N		
		//PROJECT_CODE			VARCHAR2	50		False	N		���̺Ż�ƻ��ţ����ݾ�����Ŀȷ��)
		//PROJECT_NAME			VARCHAR2	200		False	Y		��������

		//WORKUNIT_CODE			VARCHAR2	50		False	N		������Ԫ���루��λ��ʵ�������ߣ������´�ƻ��ĵļ���

		//ROUTING_CODE			VARCHAR2	50		False	Y		���մ��루���պ͹������ֻ����д����֮һ��
		//ROUTING_NAME			VARCHAR2	200		False	Y		��������

		//PROCESS_CODE			VARCHAR2	50		False	Y		�������

		//PROCESS_NAME			VARCHAR2	200		False	Y		��������

		//PARENT_ROUTING_CODE	VARCHAR2	50		False	Y		�ϼ����մ���

		//PROCESS_SEQ			NUMBER		22		False	Y		װ�����

		//ROUTING_LEVEL			VARCHAR2	50		False	Y		�㼶��

		//ROUTING_IMPFLAG		VARCHAR2	10		False	Y		��Ҫ���ձ�ʶ

		//PROCESS_IMPFLAG		VARCHAR2	10		False	Y		�ؼ������ʶ

		//ROUTING_CYCLE			NUMBER		22		False	Y		��ʱ����

		//ROUTING_CYCLE_UNIT	VARCHAR2	50		False	Y		��ʱ���λ

        //FLAG              	VARCHAR2	10		False	Y		���չ������A-���սڵ� B-װ�乤�� C-��������

	}
}

