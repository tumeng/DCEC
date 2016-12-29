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


#region �Զ�����ʵ���๤�����ɣ����ݿ⣺"ORCL
//From XYJ
//ʱ�䣺2013-12-8
//
#endregion



#region �Զ�����ʵ����
namespace Rmes.DA.Entity
{
	[PetaPoco.TableName("DATA_SN_DETECT_TEMP")]
	//����������������д�ؼ��ֶβ�ȡ��ע��
	[PetaPoco.PrimaryKey("RMES_ID", sequenceName = "SEQ_RMES_ID")]
	public class SNDetectTempEntity : IEntity
	{
        public string RMES_ID{ get; set; }
        public string COMPANY_CODE	{ get; set; }
        public string PLAN_CODE{ get; set; }
        public string SN { get; set; }
        public string WORKSHOP_CODE{ get; set; }
        public string PLINE_CODE { get; set; }
        public string STATION_CODE { get; set; }
        public string PROCESS_CODE{ get; set; }
        public string PROCESS_NAME{ get; set; }
        public string DETECT_MAT_CODE{ get; set; }
        public string DETECT_ITEM_SEQ{ get; set; }
        public string DETECT_ITEM_CODE{ get; set; }
        public string DETECT_ITEM_DESC{ get; set; }
        public int DETECT_QUAL_VALUE{ get; set; }
        public string DETECT_QUAN_VALUE	{ get; set; }
        public string MAX_VALUE{ get; set; }
        public string MIN_VALUE{ get; set; }
        public string DETECT_QTY{ get; set; }
        public string REMARK{ get; set; }
        public string ORDER_CODE{ get; set; }
        public string QUALITY_FLAG{ get; set; }
        public string FAULT_CODE { get; set; }
        public string USER_ID{ get; set; }
        public string DETECT_FLAG { get; set; }
        	
		
		
	}
}
#endregion
