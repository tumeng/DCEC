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
	[PetaPoco.TableName("ISAP_DATA_PLAN")]
	//����������������д�ؼ��ֶβ�ȡ��ע��
	[PetaPoco.PrimaryKey("ID", sequenceName = "SEQ_RMES_ID")]
	public class SAPPlanEntity 
	{
		public string AUFNR{ get; set; }
		public string AUART{ get; set; }
		public string ADESC{ get; set; }
		public string WERKS{ get; set; }
		public string MATNR{ get; set; }
		public string GAMNG{ get; set; }
		public string GMEIN{ get; set; }
		public string GSTRS{ get; set; }
		public string GSUZS{ get; set; }
		public string GLTRS{ get; set; }
		public string GLUZS{ get; set; }
		public string FEVOR{ get; set; }
		public string FNAME{ get; set; }
		public string DISPO{ get; set; }
		public string DNAME{ get; set; }
		public string PROJN{ get; set; }
		public string CHARG{ get; set; }
		public string PRUEFLOS{ get; set; }
		public string LGORT{ get; set; }
		public string TEXT1{ get; set; }
		public string CUSER{ get; set; }
		public string CNAME{ get; set; }
		public string CDATE{ get; set; }
		public string ORFLG{ get; set; }
		public string ID{ get; set; }
		public string BATCH{ get; set; }
		public string SERIAL{ get; set; }
		public string PRIND{ get; set; }
		public string DOCNUM{ get; set; }

		//�ֶ�����				�ֶ�����		����		�ؼ���	�Ƿ�Ϊ��		����ע��
		//AUFNR					VARCHAR2	20		False	False		����������
		//AUART					VARCHAR2	30		False	False		���������� ZP01����׼��������

		//ADESC					VARCHAR2	100		False	False		������������
		//WERKS					VARCHAR2	30		False	True		����8101:԰�� 8102:����
		//MATNR					VARCHAR2	30		False	True		�������ϱ���
		//GAMNG					VARCHAR2	30		False	False		������������
		//GMEIN					VARCHAR2	30		False	True		��λ
		//GSTRS					VARCHAR2	30		False	True		����������ʼ���ڣ������Ų���
		//GSUZS					VARCHAR2	30		False	True		����������ʼʱ�䣨�����Ų���
		//GLTRS					VARCHAR2	30		False	True		��������������ڣ������Ų���
		//GLUZS					VARCHAR2	30		False	True		�����������ʱ�䣨�����Ų���
		//FEVOR					VARCHAR2	30		False	True		��������Ա����
		//FNAME					VARCHAR2	30		False	True		��������Ա����
		//DISPO					VARCHAR2	30		False	True		MRP�ƻ�Ա����
		//DNAME					VARCHAR2	30		False	True		MRP�ƻ�Ա����
		//PROJN					VARCHAR2	30		False	True		WBSҪ�أ���Ŀ��ͬ�ţ�
		//CHARG					VARCHAR2	30		False	True		�ջ�����
		//PRUEFLOS				VARCHAR2	30		False	True		������
		//LGORT					VARCHAR2	30		False	True		���ص�
		//TEXT1					VARCHAR2	255		False	True		��������̧ͷ�ı�
		//CUSER					VARCHAR2	30		False	True		�����߱���
		//CNAME					VARCHAR2	30		False	True		����������
		//CDATE					VARCHAR2	30		False	True		��������
		//ORFLG					VARCHAR2	10		False	True		��������
		//ID					VARCHAR2	20		False	False		ID�ţ�ÿ��¼Ψһ�ţ�
		//BATCH					VARCHAR2	20		False	True		���ţ�����ţ�
		//SERIAL				VARCHAR2	20		False	True		Ԥ����ʱ���
		//PRIND					VARCHAR2	1		False	True		��ȡ״̬
		//DOCNUM				VARCHAR2	16		False	True		
	}
}
#endregion
