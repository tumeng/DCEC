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
	[PetaPoco.TableName("CODE_STATION")]
	//����������������д�ؼ��ֶβ�ȡ��ע��
	//[PetaPoco.PrimaryKey("RMES_ID", sequenceName = "SEQ_RMES_ID")]
	public class StationEntity : IEntity
	{
		public string RMES_ID{ get; set; }
		public string COMPANY_CODE{ get; set; }
		public string PLINE_CODE{ get; set; }
		public string STATION_CODE{ get; set; }
		public string STATION_NAME{ get; set; }
		public string STATION_TYPE{ get; set; }
		public string STATION_AREA{ get; set; }
        public int STATION_SEQ { get; set; }
        public string STATION_REMARK { get; set; }

		//�ֶ�����				�ֶ�����		����		�ؼ���	�Ƿ�Ϊ��		����ע��
		//RMES_ID				VARCHAR2	30		False	Y		
		//COMPANY_CODE			VARCHAR2	10		False	N		
		//PLINE_CODE			VARCHAR2	30		False	Y		
		//STATION_CODE			VARCHAR2	30		False	N		
		//STATION_NAME			VARCHAR2	30		False	N		
		//STATION_TYPE_CODE		VARCHAR2	30		False	N		
		//STATION_AREA_CODE		VARCHAR2	30		False	N		
	}
    [PetaPoco.TableName("REL_STATION_USER")]
    //����������������д�ؼ��ֶβ�ȡ��ע��
    //[PetaPoco.PrimaryKey("RMES_ID", sequenceName = "SEQ_RMES_ID")]
    public class RELStationEntity : IEntity
    {
        public string RMES_ID { get; set; }
        public string COMPANY_CODE { get; set; }
        public string PLINE_CODE { get; set; }
        public string STATION_CODE { get; set; }
        public string USER_ID { get; set; }

        //�ֶ�����				�ֶ�����		����		�ؼ���	�Ƿ�Ϊ��		����ע��
        //RMES_ID				VARCHAR2	30		False	Y		
        //COMPANY_CODE			VARCHAR2	10		False	N		
        //PLINE_CODE			VARCHAR2	30		False	Y		
        //STATION_CODE			VARCHAR2	30		False	N		
        //STATION_NAME			VARCHAR2	30		False	N		
        //STATION_TYPE_CODE		VARCHAR2	30		False	N		
        //STATION_AREA_CODE		VARCHAR2	30		False	N		
    }
}
#endregion
