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

namespace Rmes.DA.Entity
{
    [PetaPoco.TableName("IMES_DATA_LINE2LINE")]
    //下面这行请自行填写关键字段并取消注释
    [PetaPoco.PrimaryKey("ID", sequenceName = "SEQ_RMES_ID")]
    public class IMESLine2LineEntity
    {
        public string AUFNR { get; set; }
        public string WERKS { get; set; }
        public string VORNR { get; set; }
        public string SUBMAT { get; set; }
        public string MENGE { get; set; }
        public string TLGORT { get; set; }
        public string SLGORT { get; set; }
        public string CHARG { get; set; }
        public string ID { get; set; }
        public string BATCH { get; set; }
        public string SERIAL { get; set; }
        public string PRIND { get; set; }
        public string DOCNUM { get; set; }
    }

//    -- Create table
//create table IMES_DATA_LINE2LINE
//(
//  aufnr  VARCHAR2(12),
//  werks  VARCHAR2(4),
//  vornr  VARCHAR2(4),
//  submat VARCHAR2(18),
//  menge  VARCHAR2(13),
//  tlgort VARCHAR2(4),
//  slgort VARCHAR2(4),
//  charg  VARCHAR2(10),
//  id     VARCHAR2(20) not null,
//  batch  VARCHAR2(20),
//  serial VARCHAR2(20),
//  prind  VARCHAR2(1),
//  docnum VARCHAR2(16)
//)
//tablespace USERS
//  pctfree 10
//  initrans 1
//  maxtrans 255
//  storage
//  (
//    initial 64K
//    next 1M
//    minextents 1
//  );
}
