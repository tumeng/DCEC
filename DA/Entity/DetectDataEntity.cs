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
	[PetaPoco.TableName("CODE_DETECT")]
	//����������������д�ؼ��ֶβ�ȡ��ע��
	[PetaPoco.PrimaryKey("RMES_ID", sequenceName = "SEQ_RMES_ID")]
	public class DetectDataEntity : IEntity
	{
        public string RMES_ID { get; set; }
		public string COMPANY_CODE{ get; set; }
		public string PLINE_CODE{ get; set; }
		public string PRODUCT_SERIES{ get; set; }
		public string DETECT_CODE{ get; set; }
		public string DETECT_NAME{ get; set; }
		public string DETECT_TYPE{ get; set; }
        public double DETECT_STANDARD { get; set; }
        public double DETECT_MAX { get; set; }
        public double DETECT_MIN { get; set; }
		public string DETECT_UNIT{ get; set; }
        public string ASSOCIATION_TYPE { get; set; }
        public DateTime INPUT_TIME { get; set; }
        public string INPUT_PERSON { get; set; }
        //public string DETECT_DESC { get; set; }
        ////public string TEMP01{ get; set; }
        ////public string TEMP02{ get; set; }
        //public string TEMP03{ get; set; }


		//�ֶ�����				�ֶ�����		����		�ؼ���	�Ƿ�Ϊ��		����ע��
		//COMPANY_CODE			VARCHAR2	30		True	False		��˾����
		//PLINE_CODE			VARCHAR2	30		True	False		�����ߴ���
		//PRODUCT_SERIES		VARCHAR2	30		True	False		ϵ�д���
		//DETECT_DATA_CODE		VARCHAR2	30		True	False		������ݴ���
		//DETECT_DATA_NAME		VARCHAR2	30		False	False		�����������
		//DETECT_DATA_TYPE		VARCHAR2	1		False	False		����������ͣ�0 �����ͣ�1 �Ƶ��ͣ�
		//DETECT_DATA_STANDARD	NUMBER		22		False	True		������ݱ�׼ֵ
		//DETECT_DATA_UP		NUMBER		22		False	True		�����������
		//DETECT_DATA_DOWN		NUMBER		22		False	True		�����������
		//DETECT_DATA_UNIT		VARCHAR2	30		False	True		������ݵ�λ
		//TEMP01				VARCHAR2	30		False	True		�����ֶ�һ
		//TEMP02				VARCHAR2	30		False	True		�����ֶζ�
		//TEMP03				VARCHAR2	30		False	True		�����ֶ���
	}
}
#endregion
