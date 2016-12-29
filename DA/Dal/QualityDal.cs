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

namespace Rmes.DA.Dal
{
    internal class QualityDal:BaseDalClass
    {
        public List<QualitySnItem> GetAll()
        {
            return db.Fetch<QualitySnItem>("");
        }
        public List<QualityStandardItem> GetStandardByProcess(string processcode)
        {
            return this.FindBySql<QualityStandardItem>("where processcode=@0",processcode);
        }

        public  List<QualitySnItem> GetProductInfoByBatchProcess(string batchno,string processcode)
        {
            return this.FindBySql<QualitySnItem>("where processcode=@0 and batchno=@1",processcode, batchno);
        }

        public List<QualitySnItem> GetProductInfoAllByBatch(string batchno,string processcode)
        {
            List<QualitySnItem> items1 = this.GetProductInfoByBatchProcess(batchno,processcode);
            
            
                List<QualitySnItem> items2 = this.FindBySql<QualitySnItem>(@"select t.rmes_id,'' BATCHNO,''TEST_EQUIPMENT,''FAULT_TYPE,
                                        t.processcode,t.itemcode,t.itemname,t.itemdescription,t.minvalue,t.maxvalue,t.standardvalue,
                                        null CURRENTVALUE,1 CURRENTRESULT, t.unitname,t.unittype,t.ordering from qms_standard_item t 
                                        where t.processcode=@0", processcode);

                if (items2.Count > 0)
                {
                    items1.InsertRange(0, items2);
                }
                
                
               
            
//            else 
//            {
//                List<string> itemcodes = new List<string>();
//                foreach (QualitySnItem t in items1)
//                {
//                    itemcodes.Add(t.ItemCode);
//                }
//                List<QualitySnItem> items2 = this.FindBySql<QualitySnItem>(@"select '' RMES_ID,'' BATCHNO,
//                                        t.processcode,t.itemcode,t.itemname,t.itemdescription,t.minvalue,t.maxvalue,t.standardvalue,
//                                        null CURRENTVALUE,-1 CURRENTRESULT, t.unitname,t.unittype,t.ordering from qms_standard_item t 
//                                        where processcode=@0 and  itemcode not in (@icodes)", processcode, new { icodes = itemcodes.ToArray() });

//                if (items2.Count > 0)
//                {
//                   // items1.AddRange(items2);
                    
//                    items1.InsertRange(0, items2);
//                }
//            }
            
            return items1;
        }

        public void SaveItemMat(QualitySnItemMat mat)
        {
            db.Insert (mat);
        }
        public List<QualityType> GetAllType()
        {
            return db.Fetch<QualityType>("");
        }
        public QualityType GetByTypeName(string TypeName)
        {
            return db.First<QualityType>("where FAULT_NAME=@0", TypeName);
        }
        public FileBlobEntity GetByRMESID(string rmesid)
        {
            return db.First<FileBlobEntity>("where RMES_ID=@0", rmesid);
        }
        public QualityStandardItem GetStandard(string Rmes_ID)
        {
            return db.First<QualityStandardItem>("where RMES_ID=@0", Rmes_ID);
        }
    }
}
