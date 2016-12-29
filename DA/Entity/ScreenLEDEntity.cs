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
    [PetaPoco.TableName("CODE_LED")]

    //[PetaPoco.PrimaryKey("RMES_ID", sequenceName = "SEQ_RMES_ID")]
    public class ScreenLEDEntity : IEntity
    {
        public string SCREENNAME { get; set; }
        public string IPADDRESS { get; set; }
        public string PORT { get; set; }
        public int WIDTH { get; set; }
        public int HEIGHT { get; set; }
        public string CONTROLER { get; set; }
        public string SCREENTYPE { get; set; }
        public string POSITION { get; set; }
        public string STATUS { get; set; }
        public DateTime LASTUPDATE { get; set; }
        public int CADDRESS { get; set; }
        public string PLINE_CODE { get; set; }


		//字段名称				字段类型		长度		关键字	是否为空		中文注释
		//SCREENNAME			VARCHAR2	30		False	N		
		//IPADDRESS				VARCHAR2	30		False	N		
		//PORT					VARCHAR2	30		False	N		
		//WIDTH					NUMBER		22		False	Y		
		//HEIGHT				NUMBER		22		False	Y		
		//CONTROLER				VARCHAR2	50		False	Y		
		//SCREENTYPE			VARCHAR2	20		False	Y		
		//POSITION				VARCHAR2	100		False	Y		
		//STATUS				VARCHAR2	4		False	Y		
		//LASTUPDATE			DATE		7		False	Y		
	}
}
#endregion
