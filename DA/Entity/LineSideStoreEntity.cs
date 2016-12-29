using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rmes.DA.Entity
{
    [PetaPoco.TableName("CODE_STORE")]
    //下面这行请自行填写关键字段并取消注释
    //[PetaPoco.PrimaryKey("ID", sequenceName = "SEQ_RMES_ID")]
    public class LineSideStoreEntity
    {
        public string RMES_ID { get; set; }
        public string COMPANY_CODE { get; set; }
        public string PLINE_CODE { get; set; }
        public string WORKSHOP_CODE { get; set; }
        public string WORKUNIT_CODE { get; set; }
        public string STORE_CODE { get; set; }
        public string STORE_NAME { get; set; }
        public string STORE_TYPE { get; set; }
    }
		

}
