using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rmes.DA.Entity
{
    [PetaPoco.TableName("DATA_EMERGENCY_STORE2LINE")]
    //下面这行请自行填写关键字段并取消注释
    [PetaPoco.PrimaryKey("RMES_ID", sequenceName = "SEQ_RMES_ID")]
    public class EmergencyStore2LineEntity
    {
        public string AUFNR { get; set; }
        public string WERKS { get; set; }
        public string VORNR { get; set; }
        public string SUBMAT { get; set; }
        public string MATKT { get; set; }
        public string MENGE { get; set; }
        public string TLGORT { get; set; }
        public string SLGORT { get; set; }
        public string CHARG { get; set; }
        public string EXFLG { get; set; }
        public string RMES_ID { get; set; }
        public string BATCH { get; set; }
        public string SERIAL { get; set; }
        public string PRIND { get; set; }
        public string DOCNUM { get; set; }
        public DateTime WKDT { get; set; }
        public string CREATE_USER_ID { get; set; }
        //public string MATKT { get; set; }
    }
    //AUFNR	VARCHAR2(30)	N			生产订单号
    //WERKS	VARCHAR2(20)	N			工厂
    //VORNR	VARCHAR2(30)	Y			工序步骤
    //SUBMAT	VARCHAR2(30)	N			组件物料编码
    //MENGE	VARCHAR2(30)	Y			组件使用数量
    //TLGORT	VARCHAR2(30)	N			组件线边仓
    //SLGORT	VARCHAR2(30)	Y			来源仓库
    //CHARG	VARCHAR2(30)	Y			组件批次
    //EXFLG	VARCHAR2(30)	Y			额外领料标志
    //ID	VARCHAR2(20)	N			ID号（每记录唯一号）
    //BATCH	VARCHAR2(20)	Y			批号（任务号）
    //SERIAL	VARCHAR2(20)	Y			预备存时间戳
    //PRIND	VARCHAR2(1)	Y			读取状态
    //DOCNUM	VARCHAR2(16)	Y			

}