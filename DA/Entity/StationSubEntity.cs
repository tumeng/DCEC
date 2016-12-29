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
//时间：2013-12-08
//
#endregion

#region 自动生成实体类
namespace Rmes.DA.Entity
{
	[PetaPoco.TableName("CODE_STATION_SUB")]
	//下面这行请自行填写关键字段并取消注释
	[PetaPoco.PrimaryKey("RMES_ID", sequenceName = "SEQ_RMES_ID")]
	public class StationSubEntity : IEntity
	{
        public string RMES_ID { get; set; }
		public string COMPANY_CODE{ get; set; }
		public string PLINE_CODE{ get; set; }
		public string STATION_CODE_SUB{ get; set; }
		public string STATION_NAME_SUB{ get; set; }
		public string STATION_CODE{ get; set; }

		//字段名称				字段类型		长度		关键字	是否为空		中文注释
        //RMES_ID				VARCHAR2	30		False	Y		
		//COMPANY_CODE			VARCHAR2	10		False	False		
		//PLINE_CODE			VARCHAR2	30		False	False		所属生产线
		//STATION_CODE_SUB		VARCHAR2	30		False	False		特殊功能代码，如果是分装取点，定义成分装点站点代码
		//STATION_NAME_SUB		VARCHAR2	30		False	False		特殊功能名称
		//STATION_CODE			VARCHAR2	30		False	False		取点站点
	}
}
#endregion
