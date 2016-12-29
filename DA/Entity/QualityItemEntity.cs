using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
#region 北自所Rmes命名空间引用
using Rmes.DA.Base;
using Rmes.DA.Entity;
using Rmes.DA.Dal;
using Rmes.DA.Factory;
#endregion


#region 自动生成实体类工具生成，数据库："ORCL
//From XYJ
//时间：2013-12-8
//
#endregion



#region 自动生成实体类
namespace Rmes.DA.Entity
{
	[PetaPoco.TableName("CODE_QUALITY_ITEM")]
	//下面这行请自行填写关键字段并取消注释
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

		//字段名称				字段类型		长度		关键字	是否为空		中文注释
		//COMPANY_CODE			VARCHAR2	10		False	False		公司
		//PLINE_CODE			VARCHAR2	30		False	False		生产线
		//SERIES_CODE			VARCHAR2	30		False	False		系列
		//QUALITY_ITEM_CODE		VARCHAR2	30		False	False		质量检验项目代码
		//QUALITY_ITEM_NAME		VARCHAR2	300		False	False		质量检验项目名称
		//QUALITY_ITEM_DESC		VARCHAR2	300		False	True		质量检验项目描述
		//TEMP01				VARCHAR2	30		False	True		
		//RMES_ID				VARCHAR2	50		True	False		RMES_ID
	}


    /// <summary>
    /// 该Entity用来存储标准质量检查项，ProcessCode为检测工序的上级工艺号。
    /// 在Rmes里，质量检测被视为标准工序。
    /// </summary>
    [PetaPoco.TableName("QMS_STANDARD_ITEM")]
    //下面这行请自行填写关键字段并取消注释
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
        public string UnitType { get; set; }    //N = 数字， T = 字符串， B=布尔型，F=文件存储路径，D=格式化数据表
        public string Ordering { get; set; }    //排序字符串
        public string RMES_ID { get; set; }
    }
    
    /// <summary>
    /// 对于有多级质检数据的企业，该Entity用来显示多级之间数据
    /// </summary>
    [PetaPoco.TableName("QMS_DISPLAY_GROUP")]
    public class QualityDisplayGroupItem : IEntity
    {
        public string GroupCode { get; set; }
        public string ParentCode { get; set; }
        public string ItemCode { get; set; }
        public string Ordering { get; set; } //排序字符串
    }
    
    /// <summary>
    /// 用来实际存储某个或者某批次产品的质量检测数据
    /// </summary>
   [PetaPoco.TableName("DATA_SN_QUALITY")]
    //下面这行请自行填写关键字段并取消注释
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
        public int CurrentResult { get; set; }     //是否合格标志，True = 合格， False = 不合格，理论上是自动根据上下限判断，也可以手动设置。
        public string UnitName { get; set; }
        public string UnitType { get; set; }
        public string Ordering { get; set; }    //排序字符串
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
