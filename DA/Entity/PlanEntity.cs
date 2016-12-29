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
	[PetaPoco.TableName("DATA_PLAN")]
	//����������������д�ؼ��ֶβ�ȡ��ע��
	[PetaPoco.PrimaryKey("RMES_ID", sequenceName = "SEQ_RMES_ID")]
	public class PlanEntity : IEntity
	{
		public string RMES_ID{ get; set; }
		public string COMPANY_CODE{ get; set; }
		public string PLINE_CODE{ get; set; }
        public string PLINE_NAME { get; set; }
        public int    PLAN_SEQ { get; set; }
		public string PLAN_CODE{ get; set; }
		public string PLAN_SO{ get; set; }
		public string PRODUCT_SERIES{ get; set; }
		public string PRODUCT_MODEL{ get; set; }
		public string PLAN_TYPE{ get; set; }
		public string PLAN_BATCH{ get; set; }

        public string CREATE_USERID { get; set; }
        public string CREATE_USERNAME { get; set; }

		public DateTime CREATE_TIME{ get; set; }
		public DateTime BEGIN_DATE{ get; set; }
		public DateTime END_DATE{ get; set; }
		public DateTime BEGIN_TIME{ get; set; }
		public DateTime END_TIME{ get; set; }
        public DateTime ACCOUNT_DATE { get; set; }

		public int PLAN_QTY{ get; set; }
		public int ONLINE_QTY{ get; set; }
		public int OFFLINE_QTY{ get; set; }
		public string SN_FLAG{ get; set; }
		public string CONFIRM_FLAG{ get; set; }
		public string BOM_FLAG{ get; set; }
		public string ITEM_FLAG{ get; set; }
		public string RUN_FLAG{ get; set; }
        public string THIRD_FLAG { get; set; }
        public string STOCK_FLAG { get; set; }
        public string THIRD_RECEIVE_FLAG { get; set; }
        public string STOCK_RECEIVE_FLAG { get; set; }

        public string ROUNTING_SITE { get; set; }
        public string ROUNTING_REMARK { get; set; }

		public string CUSTOMER_CODE{ get; set; }
        public string CUSTOMER_NAME { get; set; }
		public string REMARK{ get; set; }
        public string ROUNTING_CODE { get; set; }
        public string ORDER_CODE { get; set; }
        public string LQ_FLAG { get; set; }
        public string IS_BOM { get; set; }
	}
}
#endregion
