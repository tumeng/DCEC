using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rmes.DA.Entity
{
    [PetaPoco.TableName("REL_USER_WORKUNIT")]
    //下面这行请自行填写关键字段并取消注释
    //[PetaPoco.PrimaryKey("USER_ID", sequenceName = "SEQ_RMES_ID")]
    public class User_WorkUnitEntity
    {
        public string COMPANY_CODE { get; set; }
        public string USER_ID { get; set; }
        public string WORKSHOP_CODE { get; set; }
        public string WORKUNIT_CODE { get; set; }
        public string PLINE_CODE { get; set; }
    }
}
