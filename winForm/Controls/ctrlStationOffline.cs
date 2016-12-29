using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Rmes.DA.Base;
using Rmes.DA.Entity;
using Rmes.DA.Factory;
using Rmes.WinForm.Base;
using Rmes.Pub.Data1;
//下线处理控件，接收SN传来的下线消息

namespace Rmes.WinForm.Controls
{
    public partial class ctrlStationOffline : BaseControl
    {
        string CompanyCode, PlineCode,PlanCode;
        //dataConn dc = new dataConn();
        public ctrlStationOffline()
        {
            InitializeComponent();
            CompanyCode = LoginInfo.CompanyInfo.COMPANY_CODE;
            PlineCode = LoginInfo.ProductLineInfo.PLINE_CODE;

            this.RMesDataChanged += new Rmes.WinForm.Base.RMesEventHandler(ctrlStationOffline_RmesDataChanged);
        }

        protected void ctrlStationOffline_RmesDataChanged(object obj, Rmes.WinForm.Base.RMESEventArgs e)
        {
           
            if (e.MessageHead == "OFFLINE")
            {
                    //string[]  txt = e.MessageBody.ToString().Split('^');
                    //string sn = txt[0];
                    //string quality_status = txt[1];
                    string sn = e.MessageBody.ToString();
                    ProductInfoEntity  product = ProductInfoFactory.GetByCompanyCodeSNSingle(CompanyCode, sn);//获取sn信息
                    //PlanSnEntity plansn1 = PlanSnFactory.GetBySn(sn);
                    string plan_code = product.PLAN_CODE;
                    //modify by thl 20161212 下线点对应的操作改到发动机第一次入库处执行
                    if (dataConn.GetValue("select count(1) from data_complete where sn='" + sn + "' and plan_code='" + plan_code + "' and station_code='" + LoginInfo.StationInfo.STATION_CODE + "' ") == "1")
                    {
                        dataConn.ExeSql("UPDATE DATA_PLAN SET OFFLINE_QTY=OFFLINE_QTY+1  WHERE plan_code='" + plan_code + "' ");
                    }
                    if (LoginInfo.ProductLineInfo.PLINE_CODE == "CL")
                    {
                        ProductDataFactory.Station_OffLine(product.PLAN_CODE, sn, "Y");
                    }
             }
            //else if (e.MessageHead == "SN")
            //{
                
            //    RMESEventArgs args = new RMESEventArgs();
            //    args.MessageHead = "OFFCTRL";
            //    args.MessageBody = "";
            //    SendDataChangeMessage(args);
            //}
        }


    }
}
