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
	[PetaPoco.TableName("CODE_DETECT")]
	//下面这行请自行填写关键字段并取消注释
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


		//字段名称				字段类型		长度		关键字	是否为空		中文注释
		//COMPANY_CODE			VARCHAR2	30		True	False		公司代码
		//PLINE_CODE			VARCHAR2	30		True	False		生产线代码
		//PRODUCT_SERIES		VARCHAR2	30		True	False		系列代码
		//DETECT_DATA_CODE		VARCHAR2	30		True	False		检测数据代码
		//DETECT_DATA_NAME		VARCHAR2	30		False	False		检测数据名称
		//DETECT_DATA_TYPE		VARCHAR2	1		False	False		检测数据类型（0 计量型，1 计点型）
		//DETECT_DATA_STANDARD	NUMBER		22		False	True		检测数据标准值
		//DETECT_DATA_UP		NUMBER		22		False	True		检测数据上限
		//DETECT_DATA_DOWN		NUMBER		22		False	True		检测数据下限
		//DETECT_DATA_UNIT		VARCHAR2	30		False	True		检测数据单位
		//TEMP01				VARCHAR2	30		False	True		备用字段一
		//TEMP02				VARCHAR2	30		False	True		备用字段二
		//TEMP03				VARCHAR2	30		False	True		备用字段三
	}
}
#endregion
