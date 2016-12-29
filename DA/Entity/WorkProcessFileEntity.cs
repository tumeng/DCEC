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
	[PetaPoco.TableName("DATA_PROCESS_FILE")]
	//����������������д�ؼ��ֶβ�ȡ��ע��
	//[PetaPoco.PrimaryKey("RMES_ID", sequenceName = "SEQ_RMES_ID")]
	public class WorkProcessFileEntity : IEntity
	{
        public string RMES_ID { get; set; }
		public string COMPANY_CODE{ get; set; }
		public string PLINE_CODE{ get; set; }
		public string PROCESS_CODE{ get; set; }
        public string FILE_NAME { get; set; }
        public string FILE_URL { get; set; }
        public string PRODUCT_SERIES { get; set; }
        public string FILE_TYPE { get; set; }

		//�ֶ�����				�ֶ�����		����		�ؼ���	�Ƿ�Ϊ��		����ע��
		//COMPANY_CODE			VARCHAR2	30		False	N		
		//PLINE_CODE			VARCHAR2	30		False	N		
		//PROCESS_CODE			VARCHAR2	30		False	N		
		//FILE_NAME				VARCHAR2	500		False	N		�ļ�·������URL���ļ���Դ��Ϣ
		//PRODUCT_SERIES		VARCHAR2	50		False	Y		
	}
}
#endregion
