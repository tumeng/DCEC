using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rmes.DA.Entity;
using Rmes.DA.Base;
using Rmes.DA.Dal;

namespace Rmes.DA.Factory
{
    public static class QualityFactory
    {
        public static List<QualitySnItem> GetAll()
        {
            return new QualityDal().GetAll();
        }
        public static List<QualitySnItem> GetProductProcessItems(string ProductBatchCode, string ProcessCode)
        {
            return new QualityDal().GetProductInfoAllByBatch(ProductBatchCode, ProcessCode);
        }
        public static List<QualitySnItem> GetSnQuality(string sn, string ProcessCode)//DATA_SN_QUALITY中检验过的项目
        {
            return new QualityDal().GetProductInfoByBatchProcess(sn, ProcessCode);
        }
        public static List<QualityStandardItem> GetItemsOfProcess(string ProcessCode)
        {
            return new QualityDal().GetStandardByProcess(ProcessCode);
        }
        public static bool SaveItemOfProduct(QualitySnItem item, bool autoQualityStatus)
        {
            try
            {
                QualityDal dal = new QualityDal();
                if (autoQualityStatus)
                {
                    switch (item.UnitType)
                    {
                        case "N":
                            double result = Convert.ToDouble(item.CurrentValue);
                            item.CurrentResult = (result > item.MinValue && result < item.MaxValue) ? 1 : 0;
                            break;
                        case "B":
                            item.CurrentResult = item.CurrentValue.ToUpper().Equals("TRUE") ? 1 : 0;
                            break;
                        default:
                            item.CurrentResult = 1;
                            break;
                    }
                }
                //List<QualitySnItem> testItem = dal.FindBySql<QualitySnItem>("where ItemCode=@0 and ProcessCode=@1 and BatchNo=@2 ", item.ItemCode, item.ProcessCode, item.BatchNo);
                //if(testItem.Count>0)
                //{
                //    item.RMES_ID = testItem[0].RMES_ID;
                //    dal.Update(item);
                //}
                //else
                dal.Insert(item);
            }
            catch (Exception ex)
            {
                Rmes.Public.ErrorHandle.EH.LASTMSG = ex.Message;
                return false;
            }
            return true;
        }
        public static void SaveItemMat(QualitySnItemMat mat)
        {
            new QualityDal().SaveItemMat(mat);
        }
        public static List<QualityType> GetAllType()
        {
            return new QualityDal().GetAllType();
        }
        public static QualityType GetByTypeName(string TypeName)
        {
            return new QualityDal().GetByTypeName(TypeName);
        }
        public static FileBlobEntity GetByRMESID(string RMESID)
        {
            return new QualityDal().GetByRMESID(RMESID);
        }
        public static QualityStandardItem GetStandard(string RMES_ID)
        {
            return new QualityDal().GetStandard(RMES_ID);
        }
    }       
}
