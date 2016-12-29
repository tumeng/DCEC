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


#region �Զ�����ʵ���๤�����ɣ����ݿ⣺"orcl
//From XYJ
//ʱ�䣺2014/7/24
//
#endregion





#region �Զ�����ʵ����
namespace Rmes.DA.Entity
{
	[PetaPoco.TableName("ISAP_DATA_PLAN_PROCESS")]
	//����������������д�ؼ��ֶβ�ȡ��ע��
	[PetaPoco.PrimaryKey("ID", sequenceName = "SEQ_RMES_ID")]
	public class SAPPlanProcessEntity 
	{
		public string AUFNR{ get; set; }
		public string WERKS{ get; set; }
		public string MATNR{ get; set; }
		public string VORNR{ get; set; }
		public string LTXA1{ get; set; }
		public string ARBPL{ get; set; }
		public string STEUS{ get; set; }
		public string MGVRG{ get; set; }
		public string SSAVD{ get; set; }
		public string SSAVZ{ get; set; }
		public string VGW02{ get; set; }
		public string VGE02{ get; set; }
		public string VGW03{ get; set; }
		public string VGE03{ get; set; }
		public string OPFLG{ get; set; }
		public string ID{ get; set; }
		public string BATCH{ get; set; }
		public string SERIAL{ get; set; }
		public string PRIND{ get; set; }
		public string DOCNUM{ get; set; }


        public override bool Equals(object o)
        {
            if (o == null) return false;
            if (o.GetType().Equals(this.GetType()) == false) return false;
            SAPPlanProcessEntity spe = (SAPPlanProcessEntity)o;
            if (o == null) return false;
            if (this.ID == spe.ID) return true;
            else return false;
        }

		//�ֶ�����				�ֶ�����		����		�ؼ���	�Ƿ�Ϊ��		����ע��
		//AUFNR					VARCHAR2	30		False	False		����������
		//WERKS					VARCHAR2	20		False	True		����
		//MATNR					VARCHAR2	20		False	True		�������ϱ���
		//VORNR					VARCHAR2	30		False	True		������
		//LTXA1					VARCHAR2	50		False	True		��������
		//ARBPL					VARCHAR2	30		False	True		��������
		//STEUS					VARCHAR2	30		False	True		���������
		//MGVRG					VARCHAR2	30		False	True		��������
		//SSAVD					VARCHAR2	30		False	True		����ʼ����
		//SSAVZ					VARCHAR2	30		False	True		����ʼʱ��
		//VGW02					VARCHAR2	30		False	True		��׼������ʱ
		//VGE02					VARCHAR2	30		False	True		��׼������ʱ��λ��MIN:����
		//VGW03					VARCHAR2	30		False	True		��׼�˹���ʱ
		//VGE03					VARCHAR2	30		False	True		��׼�˹���ʱ��λ��MIN:����
		//OPFLG					VARCHAR2	30		False	True		��������
		//ID					VARCHAR2	20		False	False		ID�ţ�ÿ��¼Ψһ�ţ�
		//BATCH					VARCHAR2	20		False	True		���ţ�����ţ�
		//SERIAL				VARCHAR2	20		False	True		Ԥ����ʱ���
		//PRIND					VARCHAR2	1		False	True		��ȡ״̬
		//DOCNUM				VARCHAR2	16		False	True		
	}
}
#endregion
