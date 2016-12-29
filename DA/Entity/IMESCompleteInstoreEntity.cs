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
    [PetaPoco.TableName("IMES_DATA_COMPLETE_INSTORE")]
    //下面这行请自行填写关键字段并取消注释
    [PetaPoco.PrimaryKey("ID", sequenceName = "SEQ_RMES_ID")]
    public class IMESCompleteInstoreEntity
    {
        public string AUFNR { get; set; }
        public string WERKS { get; set; }
        public string MATNR { get; set; }
        public string SERNR { get; set; }
        public string GAMNG { get; set; }
        public string LGORT { get; set; }
        public string GRTYP { get; set; }
        public string CHARG { get; set; }
        public string HSDAT { get; set; }
        public string ID { get; set; }
        public string BATCH { get; set; }
        public string SERIAL { get; set; }
        public string PRIND { get; set; }
        public string DOCNUM { get; set; }
    }

//    -- Create table
//create table IMES_DATA_COMPLETE_INSTORE
//(
//  aufnr  VARCHAR2(30) not null,
//  werks  VARCHAR2(20) not null,
//  matnr  VARCHAR2(30),
//  sernr  VARCHAR2(30),
//  gamng  VARCHAR2(30),
//  lgort  VARCHAR2(30) not null,
//  grtyp  VARCHAR2(30),
//  charg  VARCHAR2(30),
//  hsdat  VARCHAR2(10),
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
//    maxextents unlimited
//  );
//-- Add comments to the table 
//comment on table IMES_DATA_COMPLETE_INSTORE
//  is '完工入库汇报(SAP)';
//-- Add comments to the columns 
//comment on column IMES_DATA_COMPLETE_INSTORE.aufnr
//  is '生产订单号';
//comment on column IMES_DATA_COMPLETE_INSTORE.werks
//  is '工厂';
//comment on column IMES_DATA_COMPLETE_INSTORE.matnr
//  is '产品物料编码，包括拆解得到的组件';
//comment on column IMES_DATA_COMPLETE_INSTORE.sernr
//  is '序列号';
//comment on column IMES_DATA_COMPLETE_INSTORE.gamng
//  is '入库数量';
//comment on column IMES_DATA_COMPLETE_INSTORE.lgort
//  is '入库库存地点';
//comment on column IMES_DATA_COMPLETE_INSTORE.grtyp
//  is '收货类型1:成品收货
//2:拆解收货
//3:停线待处理入库
//';
//comment on column IMES_DATA_COMPLETE_INSTORE.charg
//  is '组件消耗批次';
//comment on column IMES_DATA_COMPLETE_INSTORE.hsdat
//  is '生产日期';
//comment on column IMES_DATA_COMPLETE_INSTORE.id
//  is 'ID号（每记录唯一号）';
//comment on column IMES_DATA_COMPLETE_INSTORE.batch
//  is '批号（任务号）';
//comment on column IMES_DATA_COMPLETE_INSTORE.serial
//  is '预备存时间戳';
//comment on column IMES_DATA_COMPLETE_INSTORE.prind
//  is '读取状态';
}
