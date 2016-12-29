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
//ʱ�䣺2014/3/10
//
#endregion


#region �Զ�����ʵ����
namespace Rmes.DA.Entity
{
	[PetaPoco.TableName("DATA_PLAN_BOM")]
	//����������������д�ؼ��ֶβ�ȡ��ע��
    [PetaPoco.PrimaryKey("RMES_ID", sequenceName = "SEQ_RMES_ID")]
	public class PlanBomEntity : IEntity
	{
        public string RMES_ID { get; set; }
		public string COMPANY_CODE{ get; set; }
		public string PLAN_CODE{ get; set; }
		public string PLINE_CODE{ get; set; }
		public string LOCATION_CODE{ get; set; }
		public string PROCESS_CODE{ get; set; }
		public int PROCESS_SEQUENCE{ get; set; }
		public string ITEM_CODE{ get; set; }
		public string ITEM_NAME{ get; set; }
		public float ITEM_QTY{ get; set; }
		public string ITEM_CLASS_CODE{ get; set; }
		public string VENDOR_CODE{ get; set; }
		public DateTime CREATE_TIME{ get; set; }
		public string USER_CODE{ get; set; }
		public string FLAG{ get; set; }
        public string TEAM_CODE { get; set; }
        public string PRODUCT_TYPE { get; set; }
        public string FACTORY { get; set; }
        public string RESOURCE_STORE { get; set; }
        public string LINESIDE_STOCK_CODE { get; set; }
        public string ORDER_CODE { get; set; }
        public string VIRTUAL_ITEM_CODE { get; set; }

		//�ֶ�����				�ֶ�����		����		�ؼ���	�Ƿ�Ϊ��		����ע��
        //RMES_ID				VARCHAR2	30		True	N		
		//COMPANY_CODE			VARCHAR2	30		False	N		
		//PLAN_CODE				VARCHAR2	50		False	N		
		//PLINE_CODE			VARCHAR2	50		False	N		
		//LOCATION_CODE			VARCHAR2	50		False	N		
		//PROCESS_CODE			VARCHAR2	50		False	N		
		//PROCESS_SEQUENCE		NUMBER		22		False	Y		
		//ITEM_CODE				VARCHAR2	50		False	N		
		//ITEM_NAME				VARCHAR2	50		False	Y		
		//ITEM_QTY				NUMBER		22		False	N		
		//ITEM_CLASS_CODE		VARCHAR2	10		False	Y		
		//VENDOR_CODE			VARCHAR2	50		False	Y		
		//CREATE_TIME			DATE		7		False	Y		
		//USER_CODE				VARCHAR2	30		False	Y		
		//FLAG					VARCHAR2	1		False	Y		'N'��δ�·����ϵ�/'B'���·����ϵ�����δ����/'R'�������ϵ�δ����/'Y'�������
        //TEAM_CODE				VARCHAR2	50		False	N		���϶�Ӧ�����ϰ���
	}
}
#endregion
