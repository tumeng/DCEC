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
	[PetaPoco.TableName("CODE_QUALITY_ITEM")]
	//����������������д�ؼ��ֶβ�ȡ��ע��
	[PetaPoco.PrimaryKey("RMES_ID", sequenceName = "SEQ_RMES_ID")]
	public class QualityItemEntity : IEntity
	{
		public string COMPANY_CODE{ get; set; }
		public string PLINE_CODE{ get; set; }
		public string SERIES_CODE{ get; set; }
		public string QUALITY_ITEM_CODE{ get; set; }
		public string QUALITY_ITEM_NAME{ get; set; }
		public string QUALITY_ITEM_DESC{ get; set; }
		public string TEMP01{ get; set; }
		public string RMES_ID{ get; set; }

		//�ֶ�����				�ֶ�����		����		�ؼ���	�Ƿ�Ϊ��		����ע��
		//COMPANY_CODE			VARCHAR2	10		False	False		��˾
		//PLINE_CODE			VARCHAR2	30		False	False		������
		//SERIES_CODE			VARCHAR2	30		False	False		ϵ��
		//QUALITY_ITEM_CODE		VARCHAR2	30		False	False		����������Ŀ����
		//QUALITY_ITEM_NAME		VARCHAR2	300		False	False		����������Ŀ����
		//QUALITY_ITEM_DESC		VARCHAR2	300		False	True		����������Ŀ����
		//TEMP01				VARCHAR2	30		False	True		
		//RMES_ID				VARCHAR2	50		True	False		RMES_ID
	}


    /// <summary>
    /// ��Entity�����洢��׼��������ProcessCodeΪ��⹤����ϼ����պš�
    /// ��Rmes�������ⱻ��Ϊ��׼����
    /// </summary>
    [PetaPoco.TableName("QMS_STANDARD_ITEM")]
    //����������������д�ؼ��ֶβ�ȡ��ע��
    [PetaPoco.PrimaryKey("RMES_ID", sequenceName = "SEQ_RMES_ID")]
    public class QualityStandardItem : IEntity
    {
        public string ProcessCode { get; set; }
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public string ItemDescription { get; set; }
        public string MinValue { get; set; }
        public string MaxValue { get; set; }
        public string StandardValue { get; set; }
        public string UnitName { get; set; }
        public string UnitType { get; set; }    //N = ���֣� T = �ַ����� B=�����ͣ�F=�ļ��洢·����D=��ʽ�����ݱ�
        public string Ordering { get; set; }    //�����ַ���
        public string RMES_ID { get; set; }
    }
    
    /// <summary>
    /// �����ж༶�ʼ����ݵ���ҵ����Entity������ʾ�༶֮������
    /// </summary>
    [PetaPoco.TableName("QMS_DISPLAY_GROUP")]
    public class QualityDisplayGroupItem : IEntity
    {
        public string GroupCode { get; set; }
        public string ParentCode { get; set; }
        public string ItemCode { get; set; }
        public string Ordering { get; set; } //�����ַ���
    }
    
    /// <summary>
    /// ����ʵ�ʴ洢ĳ������ĳ���β�Ʒ�������������
    /// </summary>
   [PetaPoco.TableName("DATA_SN_QUALITY")]
    //����������������д�ؼ��ֶβ�ȡ��ע��
    [PetaPoco.PrimaryKey("RMES_ID", sequenceName = "SEQ_RMES_ID")]
    public class QualitySnItem : IEntity
    {
        public string RMES_ID { get; set; }
        public string BatchNo { get; set; }
        public string ProcessCode { get; set; }
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public string ItemDescription { get; set; }
        public double MinValue { get; set; }
        public double MaxValue { get; set; }
        public string CurrentValue { get; set; } 
        public int CurrentResult { get; set; }     //�Ƿ�ϸ��־��True = �ϸ� False = ���ϸ����������Զ������������жϣ�Ҳ�����ֶ����á�
        public string UnitName { get; set; }
        public string UnitType { get; set; }
        public string Ordering { get; set; }    //�����ַ���
        public string TIMESTAMP1 { get; set; }
        public DateTime WORK_TIME { get; set; }
        public string USER_ID { get; set; }
        public string TEST_EQUIPMENT { get; set; }
        public string FAULT_TYPE { get; set; }
        public string TEMP { get; set; }
        public string URL { get; set; }
    }
   [PetaPoco.TableName("DATA_SN_QUALITY_ITEM")]
   // [PetaPoco.PrimaryKey("REMS_ID")]
   public class QualitySnItemMat : IEntity
   {
       public string SN { get; set; }
       public string PROCESS_CODE { get; set; }
       public string ITEM_NAME { get; set; }
       public string TEST_TIME { get; set; }
       public string TEST_USER { get; set; }
       public string TEST_PART { get; set; }
       public string TEST_RESULT { get; set; }
   }
   [PetaPoco.TableName("CODE_QUALITY_TYPE")]
   public class QualityType : IEntity
   {
       public string COMPANY_CODE { get; set; }
       public string FAULT_CODE { get; set; }
       public string FAULT_NAME { get; set; }
       public string RMES_ID { get; set; }
   }
   [PetaPoco.TableName("QMS_FILE_BLOB")]
   [PetaPoco.PrimaryKey("RMES_ID", sequenceName = "SEQ_RMES_ID")]
   public class FileBlobEntity : IEntity
   {
       public string RMES_ID { get; set; }
       public string FILE_NAME { get; set; }
       public Byte[] FILE_BLOB { get; set; }
       public DateTime CREAT_TIME { get; set; }
       public string USER_ID { get; set; }
   }
}
#endregion
