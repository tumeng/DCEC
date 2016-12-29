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


#region �Զ�����ʵ���๤�����ɣ����ݿ⣺"tumeng
//From XYJ
//ʱ�䣺2014/5/28
//
#endregion



#region �Զ�����ʵ����
namespace Rmes.DA.Entity
{
    [PetaPoco.TableName("CODE_PROCESS")]
    //����������������д�ؼ��ֶβ�ȡ��ע��
    [PetaPoco.PrimaryKey("RMES_ID", sequenceName = "SEQ_RMES_ID")]
    public class ProcessEntity
    {
        public string RMES_ID { get; set; }
        public string COMPANY_CODE { get; set; }
        public string PLINE_CODE { get; set; }
        public string PROCESS_CODE { get; set; }
        public string PROCESS_CODE_SAP { get; set; }
        public string PROCESS_CODE_ORG { get; set; }
        public string PROCESS_NAME { get; set; }
        public int PROCESS_MANHOUR { get; set; }
        //public string FACTORY_CODE{ get; set; }
        public string WORKUNIT_CODE { get; set; }
        //public string PROCESS_SEQ{ get; set; }
        //public string WORKUNIT_NAME{ get; set; }
        public string WORKSHOP_CODE { get; set; }

        //�ֶ�����				�ֶ�����		����		�ؼ���	�Ƿ�Ϊ��		����ע��
        //RMES_ID				VARCHAR2	30		False	True		RMESID
        //COMPANY_CODE			VARCHAR2	10		False	False		��˾
        //PLINE_CODE			VARCHAR2	30		False	True		������
        //PROCESS_CODE			VARCHAR2	30		False	False		�������
        //PROCESS_NAME			VARCHAR2	200		False	False		��������
        //PROCESS_MANHOUR		NUMBER		22		False	True		��ʱ ������Ϊ��λ��
        //FACTORY_CODE			VARCHAR2	10		False	True		����
        //WORKUNIT_CODE			VARCHAR2	30		False	True		�������ı��
        //PROCESS_SEQ			VARCHAR2	10		False	True		����˳���
        //WORKUNIT_NAME			VARCHAR2	30		False	True		������������

    }
}
#endregion
