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
    [PetaPoco.TableName("INTER_ISSUE")]
    public class InterIssueEntity : IEntity
    {
        public string LLGZH { get; set; }
        public string LLGCH { get; set; }
        public string LLCPXH { get; set; }
        public string LLZJDH { get; set; }
        public string LLPC { get; set; }
        public string LLPGDH { get; set; }
        public DateTime LLWGRQ { get; set; }
        public string LLLYDW { get; set; }
        public string LLZPXZ { get; set; }
        public string LLXZZZ { get; set; }
        public int LLXH { get; set; }
        public string LLXMDH { get; set; }
        public string LLPZH { get; set; }
        public string LLLYPC { get; set; }
        public int LLDJSL { get; set; }
        public int LLDTSL { get; set; }
        public int LLPTSL { get; set; }
        public int LLBPSL { get; set; }
        public int LLSL { get; set; }
        public int LLYLSL { get; set; }
        public string LLR { get; set; }
        public int LLSFSL { get; set; }
        public DateTime LLFLRQ { get; set; }
        public string LLFLR { get; set; }
        public string LLXMFL { get; set; }
        public string LLBGY { get; set; }
        public string LLKFDM { get; set; }
        public string LLKDBS { get; set; }
        public int LLDJ { get; set; }
        public string LLDW { get; set; }
        public string LLBZ { get; set; }
        public int LLSPSL { get; set; }
        public string LLSPBS { get; set; }
        public string LLCJYH { get; set; }
        public DateTime LLCJRQ { get; set; }
        public string LLXGYH { get; set; }
        public DateTime LLXGRQ { get; set; }
        public string LLNY { get; set; }
        public DateTime LLSPRQ { get; set; }
        public string LLSPYH { get; set; }
        public int LLJHSL { get; set; }
        public int LLHZ { get; set; }
        public int LLSPSLHZ { get; set; }
        public int LLBBSY { get; set; }
        public string LLXMLX { get; set; }
        public string LLGYDM { get; set; }
        public DateTime LLRQ { get; set; }
        public string LLXMMC { get; set; }
        public string TMBH { get; set; }
        public string PLAN_CODE { get; set; }
        public string LLBS { get; set; }

        //字段名称				字段类型		长度		关键字	是否为空		中文注释
        
    }
}
