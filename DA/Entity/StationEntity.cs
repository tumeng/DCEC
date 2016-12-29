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


#region 自动生成实体类工具生成，by 北自所自控中心信息部
//From XYJ
//时间：2013/12/4
//
#endregion


#region 自动生成实体类
namespace Rmes.DA.Entity
{
	[PetaPoco.TableName("CODE_STATION")]
	//下面这行请自行填写关键字段并取消注释
	//[PetaPoco.PrimaryKey("RMES_ID", sequenceName = "SEQ_RMES_ID")]
	public class StationEntity : IEntity
	{
		public string RMES_ID{ get; set; }
		public string COMPANY_CODE{ get; set; }
		public string PLINE_CODE{ get; set; }
		public string STATION_CODE{ get; set; }
		public string STATION_NAME{ get; set; }
		public string STATION_TYPE{ get; set; }
		public string STATION_AREA{ get; set; }
        public int STATION_SEQ { get; set; }
        public string STATION_REMARK { get; set; }

		//字段名称				字段类型		长度		关键字	是否为空		中文注释
		//RMES_ID				VARCHAR2	30		False	Y		
		//COMPANY_CODE			VARCHAR2	10		False	N		
		//PLINE_CODE			VARCHAR2	30		False	Y		
		//STATION_CODE			VARCHAR2	30		False	N		
		//STATION_NAME			VARCHAR2	30		False	N		
		//STATION_TYPE_CODE		VARCHAR2	30		False	N		
		//STATION_AREA_CODE		VARCHAR2	30		False	N		
	}
    [PetaPoco.TableName("REL_STATION_USER")]
    //下面这行请自行填写关键字段并取消注释
    //[PetaPoco.PrimaryKey("RMES_ID", sequenceName = "SEQ_RMES_ID")]
    public class RELStationEntity : IEntity
    {
        public string RMES_ID { get; set; }
        public string COMPANY_CODE { get; set; }
        public string PLINE_CODE { get; set; }
        public string STATION_CODE { get; set; }
        public string USER_ID { get; set; }

        //字段名称				字段类型		长度		关键字	是否为空		中文注释
        //RMES_ID				VARCHAR2	30		False	Y		
        //COMPANY_CODE			VARCHAR2	10		False	N		
        //PLINE_CODE			VARCHAR2	30		False	Y		
        //STATION_CODE			VARCHAR2	30		False	N		
        //STATION_NAME			VARCHAR2	30		False	N		
        //STATION_TYPE_CODE		VARCHAR2	30		False	N		
        //STATION_AREA_CODE		VARCHAR2	30		False	N		
    }
}
#endregion
