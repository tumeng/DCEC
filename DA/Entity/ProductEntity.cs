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
    [PetaPoco.TableName("DATA_PRODUCT")]
    public  class ProductDataEntity:IEntity
    {
        public string  RMES_ID { get; set; }    
        public string  SN     {get;set;}    
        public string  COMPANY_CODE  {get;set;}      
        public string  PLAN_CODE      {get;set;}      
        public string  PLAN_SO       {get;set;}      
        public string  PRODUCT_SERIES {get;set;}      
        public string  PRODUCT_MODEL {get;set;}         
        public string  ORDER_CODE     {get;set;}      
        public string  PLINE_CODE     {get;set;}           
        public string  USER_ID        {get;set;}     
        public DateTime WORK_DATE      {get;set;}      
        public DateTime  WORK_TIME      {get;set;}       
 
    }
}
