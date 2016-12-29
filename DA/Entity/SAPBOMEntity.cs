using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PetaPoco;

#region 北自所Rmes命名空间引用
using Rmes.DA.Base;
using Rmes.DA.Entity;
using Rmes.DA.Dal;
using Rmes.DA.Factory;
#endregion

namespace Rmes.DA.Entity
{
    [PetaPoco.TableName("ISAP_DATA_PLAN_BOM")]
    //下面这行请自行填写关键字段并取消注释
    //[PetaPoco.PrimaryKey("ITEM_CODE", sequenceName = "SEQ_RMES_ID")]
    public class SAPBOMEntity
    {
        public string AUFNR { get; set; }
        public string WERKS { get; set; }
        public string MATNR { get; set; }
        public string POSNR { get; set; }
        public string SUBMAT { get; set; }
        public string MATKT { get; set; }
        public string MENGE { get; set; }
        public string EINHEIT { get; set; }
        public string LGORT_F { get; set; }
        public string LGORT { get; set; }
        public string CHARG { get; set; }
        public string BDTER { get; set; }
        public string BDZTP { get; set; }
        public string VORNR { get; set; }
        public string LTXA1 { get; set; }
        public string ID { get; set; }
        public string BATCH { get; set; }
        public string SERIAL { get; set; }
        public string PRIND { get; set; }
        public string DOCNUM { get; set; }
        public string DUMPS { get; set; }
        
    }

//    comment on column SAP_DATA_BOM.aufnr
//  is '生产订单号';
//comment on column SAP_DATA_BOM.werks
//  is '工厂';
//comment on column SAP_DATA_BOM.matnr
//  is '生产物料编码';
//comment on column SAP_DATA_BOM.posnr
//  is '组件顺序号';
//comment on column SAP_DATA_BOM.submat
//  is '组件物料编码';
//comment on column SAP_DATA_BOM.matkt
//  is '组件物料描述';
//comment on column SAP_DATA_BOM.menge
//  is '组件需求量';
//comment on column SAP_DATA_BOM.einheit
//  is '组件单位';
//comment on column SAP_DATA_BOM.lgort_f
//  is '来源库';
//comment on column SAP_DATA_BOM.lgort
//  is '线边库';
//comment on column SAP_DATA_BOM.charg
//  is '批次';
//comment on column SAP_DATA_BOM.bdter
//  is '组件需求日期';
//comment on column SAP_DATA_BOM.bdztp
//  is '组件需求时间';
//comment on column SAP_DATA_BOM.vornr
//  is '工序步骤';
//comment on column SAP_DATA_BOM.ltxa1
//  is '工序名称';
}
