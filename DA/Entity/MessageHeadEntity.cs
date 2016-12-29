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
    [PetaPoco.TableName("CODE_CONFIG_COMMAND")]
    //����������������д�ؼ��ֶβ�ȡ��ע��
    [PetaPoco.PrimaryKey("RMES_ID", sequenceName = "SEQ_RMES_ID")]
    public class MessageHeadEntity : IEntity
    {
        public string RMES_ID { get; set; }

        [PetaPoco.Column("COMMAND_CODE")]
        public string HEAD_CODE { get; set; }

        [PetaPoco.Column("COMMAND_REGEX")]
        public string REGEXSTRING { get; set; }

        public string COMMAND_BODY { get; set; }
        public string REMARK { get; set; }

    }
}
#endregion
