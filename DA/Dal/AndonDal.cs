using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PetaPoco;
using Rmes.DA.Entity;

namespace Rmes.DA.Dal
{
    internal class AndonDal : Base.BaseDalClass
    {
        public AndonDal()
        { }

        public void SaveAndonAlert(AndonAlertEntity ent1)
        {
            db.Save(ent1);
        }
    }
}
