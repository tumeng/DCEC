using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rmes.DA.Base;
using Rmes.DA.Entity;
using Rmes.DA.Dal;
using Rmes.DA.Factory;

namespace Rmes.DA.Entity
{
    [PetaPoco.TableName("DATA_ANDON_MUSIC")]
    public class MusicEntity : IEntity
    {
        public string COMPANY_CODE { get; set; }
        public string PLINE_CODE { get; set; }
        public string ANDON_MUSIC { get; set; }
        public string STOP_FLAG { get; set; }
        public string ANDON_TIME { get; set; }

    }
}
