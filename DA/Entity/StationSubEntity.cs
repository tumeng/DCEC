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


#region �Զ�����ʵ���๤�����ɣ����ݿ⣺"ORCL
//From XYJ
//ʱ�䣺2013-12-08
//
#endregion

#region �Զ�����ʵ����
namespace Rmes.DA.Entity
{
	[PetaPoco.TableName("CODE_STATION_SUB")]
	//����������������д�ؼ��ֶβ�ȡ��ע��
	[PetaPoco.PrimaryKey("RMES_ID", sequenceName = "SEQ_RMES_ID")]
	public class StationSubEntity : IEntity
	{
        public string RMES_ID { get; set; }
		public string COMPANY_CODE{ get; set; }
		public string PLINE_CODE{ get; set; }
		public string STATION_CODE_SUB{ get; set; }
		public string STATION_NAME_SUB{ get; set; }
		public string STATION_CODE{ get; set; }

		//�ֶ�����				�ֶ�����		����		�ؼ���	�Ƿ�Ϊ��		����ע��
        //RMES_ID				VARCHAR2	30		False	Y		
		//COMPANY_CODE			VARCHAR2	10		False	False		
		//PLINE_CODE			VARCHAR2	30		False	False		����������
		//STATION_CODE_SUB		VARCHAR2	30		False	False		���⹦�ܴ��룬����Ƿ�װȡ�㣬����ɷ�װ��վ�����
		//STATION_NAME_SUB		VARCHAR2	30		False	False		���⹦������
		//STATION_CODE			VARCHAR2	30		False	False		ȡ��վ��
	}
}
#endregion
