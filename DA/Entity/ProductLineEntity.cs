using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
#region ������Rmes�����ռ�����
using Rmes.DA.Base;
using Rmes.DA.Entity;
using Rmes.DA.Dal;
using Rmes.DA.Factory;
#endregion


#region �Զ�����ʵ���๤�����ɣ�by �������Կ�������Ϣ��
//From XYJ
//ʱ�䣺2013/12/4
//
#endregion


#region �Զ�����ʵ����
namespace Rmes.DA.Entity
{
	[PetaPoco.TableName("CODE_PRODUCT_LINE")]
	//����������������д�ؼ��ֶβ�ȡ��ע��
	[PetaPoco.PrimaryKey("RMES_ID", sequenceName = "SEQ_RMES_ID")]
	public class ProductLineEntity : IEntity
	{
		public string RMES_ID{ get; set; }
		public string COMPANY_CODE{ get; set; }
		public string PLINE_CODE{ get; set; }
		public string PLINE_NAME{ get; set; }
		public string PLINE_TYPE_CODE{ get; set; }
        //public string PLINE_PLAN_CODE{ get; set; }
        //public string WORKSHOP_CODE { get; set; }
        //public string DEPT_CODE { get; set; }
        //public string AUTO_TYPE { get; set; }
        public string THIRD_FLAG { get; set; }
        public string STOCK_FLAG { get; set; }
        public string SAP_CODE { get; set; }
		//�ֶ�����				�ֶ�����		����		�ؼ���	�Ƿ�Ϊ��		����ע��
		//RMES_ID				VARCHAR2	30		True	N		RMESID
		//COMPANY_CODE			VARCHAR2	10		False	N		��˾
		//PLINE_CODE			VARCHAR2	30		False	N		�����ߴ���
		//PLINE_NAME			VARCHAR2	30		False	N		����������
		//PLINE_TYPE_CODE		VARCHAR2	30		False	Y		����������
		//PLINE_PLAN_CODE		VARCHAR2	10		False	Y		�ƻ�����
        //@override
            public override bool Equals(object o){
                ProductLineEntity p = (ProductLineEntity)o;
                if (p == null) return false;
                if (this.RMES_ID == p.RMES_ID)
                {
                    return true;
                }
                else
                {
                    return false;

                }
            }
	}
}
#endregion
