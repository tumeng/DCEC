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
	[PetaPoco.TableName("IMES_DATA_COMPLETE_BATCH")]
	//����������������д�ؼ��ֶβ�ȡ��ע��
	//[PetaPoco.PrimaryKey("RMES_ID", sequenceName = "SEQ_RMES_ID")]
	public class IMESCompleteBatchEntity 
	{
		public string AUFNR{ get; set; }
		public string WERKS{ get; set; }
		public string MATNR{ get; set; }
		public string CHARG{ get; set; }
		public string GAMNG{ get; set; }
		public string PONSR{ get; set; }
		public string CHART{ get; set; }
		public string NAME1{ get; set; }
		public string VALUE{ get; set; }
		public string STATUS{ get; set; }

		//�ֶ�����				�ֶ�����		����		�ؼ���	�Ƿ�Ϊ��		����ע��
		//AUFNR					VARCHAR2	30		False	False		����������
		//WERKS					VARCHAR2	20		False	False		����
		//MATNR					VARCHAR2	30		False	True		�������ϱ���
		//CHARG					VARCHAR2	30		False	True		����
		//GAMNG					VARCHAR2	30		False	True		����
		//PONSR					VARCHAR2	30		False	True		����Ŀ��
		//CHART					VARCHAR2	30		False	True		�������Ա���
		//NAME1					VARCHAR2	30		False	True		������������
		//VALUE					VARCHAR2	30		False	True		��������ֵ
		//STATUS				VARCHAR2	10		False	True		
	}
}
#endregion
