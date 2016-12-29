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


#region �Զ�����ʵ����
namespace Rmes.DA.Entity
{
	[PetaPoco.TableName("IMES_DATA_PLAN_CHANGE")]
	//����������������д�ؼ��ֶβ�ȡ��ע��
	[PetaPoco.PrimaryKey("ID", sequenceName = "SEQ_RMES_ID")]
	public class IMESPlanChangeEntity 
	{
		public string AUFNR{ get; set; }
		public string WERKS{ get; set; }
		public string VORNR{ get; set; }
		public string STOP{ get; set; }
		public string TEXT{ get; set; }
		public string ID{ get; set; }
		public string BATCH{ get; set; }
		public string SERIAL{ get; set; }
		public string PRIND{ get; set; }
		public string DOCNUM{ get; set; }

		//�ֶ�����				�ֶ�����		����		�ؼ���	�Ƿ�Ϊ��		����ע��
		//AUFNR					VARCHAR2	20		False	False		����������
		//WERKS					VARCHAR2	30		False	True		����
		//VORNR					VARCHAR2	30		False	False		������
		//STOP					VARCHAR2	30		False	True		״̬ 1����ͣ 2��ֹͣ
		//TEXT					VARCHAR2	30		False	True		ԭ��
		//ID					VARCHAR2	20		False	True		ΨһID
		//BATCH					VARCHAR2	20		False	True		���ţ�����ţ�
		//SERIAL				VARCHAR2	20		False	True		SERIAL	N	VARCHAR2(20)	Y			Ԥ����ʱ���
		//PRIND					VARCHAR2	1		False	True		��ȡ״̬
		//DOCNUM				VARCHAR2	16		False	True		
	}
}
#endregion
